using CitizenFX.Core;
using JrpServer.Controllers.Data;
using JrpShared.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static JrpServer.Log;
using static JrpServer.Server;
using static JrpShared.Helpers.Common;
using static JrpShared.Helpers.Serialization;

namespace JrpServer.Controllers
{
    internal sealed class SessionController : IController
    {
        public IDictionary<string, ISession> SessionByHandle;

        public Database Database;

        public void Boot()
        {
            GetInstance().RegisterEventHandler("jrp:playerSpawned", new Action<Player>(OnPlayerSpawned));
            GetInstance().RegisterEventHandler("jrp:fetchSessions", new Action<Player, NetworkCallbackDelegate>(OnFetchSessions));

            SessionByHandle = new Dictionary<string, ISession>();
            Database = new Database();
        }

        public void Init()
        {

        }

        private async void OnPlayerSpawned([FromSource]Player player)
        {
            WriteToConsole($"{player.Name} Connesso.");

            CreateNewSession(player, new Session(null, PlayerState.Connecting));

            CheckUserRegistration(player);

            await CheckCharacterRegistration(player);
        }

        private void CheckUserRegistration(Player player)
        {
            if (Database.FetchUserId(player) == 0)
            {
                Database.RegisterNewUser(player);

                WriteToConsole($"Nuovo utente registrato {player.Name} {player.Identifiers["license"]}");
            }
        }

        private void CreateNewSession(Player player, ISession session)
        {
            if (SessionByHandle.ContainsKey(player.Handle))
            {
                WriteToConsole($"Non è stato possibile creare una nuova sessione per {player.Name} {player.Handle}", MessageType.Error);

                player.Drop("Si è verificato un errore :(");
            }
            else
            {
                SessionByHandle.Add(player.Handle, session);

                WriteToConsole($"Sessione creata per {player.Name} {player.Handle}");
            }
        }

        private async Task CheckCharacterRegistration(Player player)
        {
            if (Database.FetchCharacterId(Database.FetchUserId(player)) != 0)
            {
                SessionByHandle[player.Handle].Character = Database.FetchCharacter(Database.FetchUserId(player));
                SessionByHandle[player.Handle].State = PlayerState.Connected;

                BaseScript.TriggerClientEvent(player, "jrp:initClient");
            }
            else
            {
                Database.RegisterNewCharacter(Database.FetchUserId(player), await CreateNewCharacter(player));

                await CheckCharacterRegistration(player);
            }
        }

        // TODO Rewrite server side validation.
        private async Task<ICharacter> CreateNewCharacter(Player player)
        {
            Character character = null;

            BaseScript.TriggerClientEvent(player, "jrp:createNewCharacter");

            GetInstance().RegisterEventHandler("jrp:notifyCharacterCreation", new Action<string, string>((name, serializedSkin) =>
            {
                if (ValidateCharacterName(ref name))
                {
                    if (Database.FetchCharacterId(Database.FetchUserId(player)) != 0)
                    {
                        WriteToConsole($"Non è stato possibile registrare un nuovo personaggio per {player.Name} {player.Handle}", MessageType.Error);

                        player.Drop("Si è verificato un errore :(");
                    }
                    else
                    {
                        character = new Character(name, GetInstance().Game.Jobs["disoccupato"], DeserializeObject<ISkin>(serializedSkin));

                        WriteToConsole($"Nuovo personaggio registrato {name} da {player.Name}");
                    }
                }
                else
                    player.Drop("Si è verificato un errore :(");
            }));

            while (character == null)
                await BaseScript.Delay(500);

            return character;
        }

        private void OnFetchSessions([FromSource]Player player, NetworkCallbackDelegate networkCB)
        {
            WriteToConsole($"Sincronizzo sessioni con {player.Name} {player.Handle}");

            networkCB.Invoke(SerializeObject(SessionByHandle));
        }
    }
}
