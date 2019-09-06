using CitizenFX.Core;
using JrpServer.Controllers.Data;
using JrpShared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using static JrpServer.Log;
using static JrpServer.Server;
using static JrpShared.Data.Serialization;

namespace JrpServer.Controllers
{
    sealed class SessionController : IController
    {
        public IDictionary<string, ISession> SessionByHandle;

        public Database Database;

        public void Boot()
        {
            GetInstance().RegisterEventHandler("jrp:playerSpawned", new Action<Player>(OnPlayerSpawned));

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

                CancelEvent();
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

        private async Task<ICharacter> CreateNewCharacter(Player player)
        {
            Character character = null;

            BaseScript.TriggerClientEvent(player, "jrp:createNewCharacter");

            GetInstance().RegisterEventHandler("jrp:notifyCharacterCreation", new Action<string, string>((name, serializedSkin) =>
            {
                if (ValidateCharacterName(ref name))
                {
                    character = new Character(name, GetInstance().Game.Jobs["disoccupato"], DeserializeObject<ISkin>(serializedSkin));

                    WriteToConsole($"Nuovo personaggio registrato {name} da {player.Name}");
                }
                else
                {
                    player.Drop("Si è verificato un errore :(");

                    CancelEvent();
                }
            }));

            while (character == null)
                await BaseScript.Delay(500);

            return character;
        }

        private bool ValidateCharacterName(ref string name)
        {
            string[] credentials = name.Split();

            if (name.Split().Length == 2)
            {
                if (credentials[0].All(char.IsLetter) && credentials[1].All(char.IsLetter))
                {
                    if (credentials[0].Length > 2 && credentials[0].Length < 16 && credentials[1].Length > 2 && credentials[1].Length < 16)
                    {
                        credentials[0] = credentials[0].ToLower();
                        credentials[1] = credentials[1].ToLower();
                        credentials[0] = credentials[0][0].ToString().ToUpper() + credentials[0].Substring(1);
                        credentials[1] = credentials[1][0].ToString().ToUpper() + credentials[1].Substring(1);

                        name = credentials[0] + " " + credentials[1];

                        return true;
                    }
                }
            }

            return false;
        }
    }
}
