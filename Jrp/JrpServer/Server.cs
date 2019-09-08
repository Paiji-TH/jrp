using CitizenFX.Core;
using JrpServer.Controllers;
using JrpShared.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static JrpServer.Log;

namespace JrpServer
{
    internal sealed class Server : BaseScript
    {
        private static Server Instance;

        public static Server GetInstance() => Instance;

        public ICollection<IController> ControllerList = new Collection<IController>();

        public SessionController Session = new SessionController();
        public GameController Game = new GameController();

        public Server()
        {
            Instance = this;

            ControllerList.Add(Session);
            ControllerList.Add(Game);

            Boot();

            WriteToConsole("Boot Completed");
        }

        public void RegisterEventHandler(string eventName, Delegate action) => EventHandlers[eventName] += action;

        public void DeregisterEventHandler(string eventName, Delegate action) => EventHandlers[eventName] -= action;

        public void RegisterTickHandler(Func<Task> onTick) => Tick += onTick;

        public void DeregisterTickHandler(Func<Task> onTick) => Tick -= onTick;

        public ExportDictionary GetExports() => Exports;

        public PlayerList GetPlayers() => Players;

        private void Boot()
        {
            foreach (IController controller in ControllerList)
                controller.Boot();
        }

        private void Init()
        {
            foreach (IController controller in ControllerList)
                controller.Init();
        }
    }
}
