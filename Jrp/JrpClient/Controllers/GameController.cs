using CitizenFX.Core;
using JrpClient.Controllers.Data;
using JrpShared.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using static JrpClient.Client;
using static JrpShared.Data.Serialization;

namespace JrpClient.Controllers
{
    sealed class GameController : IController
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

        private void OnPlayerSpawned()
        {
            BaseScript.TriggerServerEvent("jrp:playerSpawned");

            GetInstance().Init();
        }

        private void OnCreateNewCharacter()
        {
            BaseScript.TriggerServerEvent("jrp:notifyCharacterCreation", "riCCardo PalleSCHI", SerializeObject(Appereance.GetCurrentSkin()));
        }

        private void OnInitClient()
        {

        }
    }
}
