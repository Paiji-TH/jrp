using CitizenFX.Core;
using CitizenFX.Core.UI;
using JrpClient.Controllers.Data;
using JrpShared.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using static JrpClient.Client;
using static JrpShared.Helpers.Serialization;

namespace JrpClient.Controllers
{
    internal sealed class GameController : IController
    {
        public Appereance Appereance;

        public void Boot()
        {
            // Used for testing.
            RegisterCommand("respawn", new Action<int, List<object>, string>((source, args, raw) =>
            {
                BaseScript.TriggerEvent("playerSpawned");
            }), true);

            GetInstance().RegisterEventHandler("playerSpawned", new Action(OnPlayerSpawned));
            GetInstance().RegisterEventHandler("jrp:initClient", new Action(OnInitClient));
            GetInstance().RegisterEventHandler("jrp:createNewCharacter", new Action(OnCreateNewCharacter));

            Appereance = new Appereance();
        }

        public void Init()
        {
            GetInstance().GetExports()["spawnmanager"].setAutoSpawn(false);

            GetInstance().RegisterTickHandler(GameplayChanges);

            NetworkSetFriendlyFireOption(true);
            SetCanAttackFriendly(Game.PlayerPed.Handle, true, false);
        }

        public async Task GameplayChanges()
        {
            Game.PlayerPed.CanWearHelmet = false;
            Game.PlayerPed.CanBeDraggedOutOfVehicle = false;
            Game.PlayerPed.CanFlyThroughWindscreen = false;
            Game.PlayerPed.CanSufferCriticalHits = false;
            Game.PlayerPed.DropsWeaponsOnDeath = false;
            Game.Player.IgnoredByEveryone = true;

            SetPlayerCanDoDriveBy(Game.Player.Handle, false);

            SetPlayerHealthRechargeMultiplier(Game.Player.Handle, 0);

            StatSetInt((uint)GetHashKey("MP0_STAMINA"), 100, true);
            StatSetInt((uint)GetHashKey("MP1_STAMINA"), 100, true);
        }

        public async Task<IDictionary<string, ISession>> FetchSessions()
        {
            Screen.LoadingPrompt.Show("In attesa del Server...");

            IDictionary<string, ISession> sessions = null;

            BaseScript.TriggerServerEvent("jrp:fetchSessions", new Action<string>((arg) => sessions = DeserializeObject<IDictionary<string, ISession>>(arg)));

            while (sessions == null)
                await BaseScript.Delay(50);

            Screen.LoadingPrompt.Hide();

            return sessions;
        }

        public async Task<ICharacter> FetchCharacter() => (await FetchSessions())[Game.Player.ServerId.ToString()].Character;

        public bool IsPlayerActive(int serverId)
        {
            foreach (var player in GetInstance().GetPlayers())
            {
                if (player.ServerId == serverId)
                    return true;
            }

            return false;
        }

        private void OnPlayerSpawned()
        {
            Game.Player.CanControlCharacter = false;
            Game.PlayerPed.IsVisible = false;
            Game.PlayerPed.IsInvincible = true;
            Game.PlayerPed.CanRagdoll = false;

            NetworkSetEntityInvisibleToNetwork(Game.PlayerPed.Handle, true);

            BaseScript.TriggerServerEvent("jrp:playerSpawned");

            GetInstance().Init();
            GetInstance().InitMenu();
        }

        private void OnCreateNewCharacter()
        {
            GetInstance().PedCreationMenu.Menu.OpenMenu();
        }

        private async void OnInitClient()
        {
            Game.Player.CanControlCharacter = true;
            Game.PlayerPed.IsVisible = true;
            Game.PlayerPed.IsInvincible = false;
            Game.PlayerPed.CanRagdoll = true;

            NetworkSetEntityInvisibleToNetwork(Game.PlayerPed.Handle, false);

            Appereance.LoadSkin((await FetchCharacter()).Skin);

            //Fetches informations and loads ui
        }
    }
}
