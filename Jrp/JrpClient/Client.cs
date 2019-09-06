using CitizenFX.Core;
using JrpClient.Controllers;
using JrpShared.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace JrpClient
{
    sealed class Client : BaseScript
    {
        private static Client Instance;

        public static Client GetInstance() => Instance;

        public ICollection<IController> ControllerList = new Collection<IController>();

        public GameController Game = new GameController();
        public ScreenController Screen = new ScreenController();
        public VehicleController Vehicle = new VehicleController();
        public WorldController World = new WorldController();

        public Client()
        {
            Instance = this;

            ControllerList.Add(Game);
            ControllerList.Add(Screen);
            ControllerList.Add(Vehicle);
            ControllerList.Add(World);

            Boot();
        }

        public void RegisterEventHandler(string eventName, Delegate action) => EventHandlers[eventName] += action;

        public void DeregisterEventHandler(string eventName, Delegate action) => EventHandlers[eventName] -= action;

        public void RegisterTickHandler(Func<Task> onTick) => Tick += onTick;

        public void DeregisterTickHandler(Func<Task> onTick) => Tick -= onTick;

        public ExportDictionary GetExports() => Exports;

        public PlayerList GetPlayers() => Players;

        public void Init()
        {
            foreach (IController controller in ControllerList)
                controller.Init();
        }

        private void Boot()
        {
            foreach (IController controller in ControllerList)
                controller.Boot();
        }
    }
}
