using CitizenFX.Core;
using JrpShared.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using static JrpClient.Client;

namespace JrpClient.Controllers
{
    public enum DispatchType
    {
        PoliceAutomobile = 1,
        PoliceHelicopter = 2,
        FireDepartment = 3,
        SwatAutomobile = 4,
        AmbulanceDepartment = 5,
        PoliceRiders = 6,
        PoliceVehicleRequest = 7,
        PoliceRoadBlock = 8,
        PoliceAutomobileWaitPulledOver = 9,
        PoliceAutomobileWaitCruising = 10,
        Gangs = 11,
        SwatHelicopter = 12,
        PoliceBoat = 13,
        ArmyVehicle = 14,
        BikerBackup = 15
    };

    internal sealed class WorldController : IController
    {
        public void Boot()
        {
            GetInstance().RegisterEventHandler("populationPedCreating", new Action<float, float, float, uint, dynamic>(OnPopulationPedCreated));
            GetInstance().RegisterTickHandler(DisableAI);
            DisableDispatch();
            Game.MaxWantedLevel = 0;
        }

        public void Init()
        {

        }

        public void TeleportToPos(Vector3 pos) => SetPedCoordsKeepVehicle(Game.PlayerPed.Handle, pos.X, pos.Y, pos.Z + World.GetGroundHeight(pos));

        public async Task DisableAI()
        {
            SetPedDensityMultiplierThisFrame(0);
            SetParkedVehicleDensityMultiplierThisFrame(0);
            SetVehicleDensityMultiplierThisFrame(0);
            SetRandomVehicleDensityMultiplierThisFrame(0);
            SetScenarioPedDensityMultiplierThisFrame(0, 0);
            SetSomeVehicleDensityMultiplierThisFrame(0);
        }

        private void DisableDispatch()
        {
            for (int i = 0; i < Enum.GetValues(typeof(DispatchType)).Cast<int>().Max(); i++)
                EnableDispatchService(i, false);
        }

        private void OnPopulationPedCreated(float x, float y, float z, uint model, dynamic setters) => CancelEvent();
    }
}
