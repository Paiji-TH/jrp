﻿using CitizenFX.Core;
using JrpClient.Controllers;
using JrpClient.Controllers.Menus;
using JrpShared.Data;
using MenuAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace JrpClient
{
    internal sealed class Client : BaseScript
    {
        private static Client Instance;

        public static Client GetInstance() => Instance;

        public ICollection<IController> ControllerCollection = new Collection<IController>();
        public ICollection<IMenu> MenuCollection = new Collection<IMenu>();
        public GameController Game = new GameController();
        public ScreenController Screen = new ScreenController();
        public VehicleController Vehicle = new VehicleController();
        public WorldController World = new WorldController();

        public MainMenu MainMenu = new MainMenu();
        public SessionMenu SessionMenu = new SessionMenu();
        public PedCreationMenu PedCreationMenu = new PedCreationMenu();

        public Client()
        {
            Instance = this;

            ControllerCollection.Add(Game);
            ControllerCollection.Add(Screen);
            ControllerCollection.Add(Vehicle);
            ControllerCollection.Add(World);

            MenuCollection.Add(MainMenu);
            MenuCollection.Add(SessionMenu);
            MenuCollection.Add(PedCreationMenu);

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
            foreach (IController controller in ControllerCollection)
                controller.Init();
        }

        public void InitMenu()
        {
            foreach (IMenu menu in MenuCollection)
            {
                menu.CreateMenu();
                menu.CreateMenuItems();
            }
        }

        public void DisableMenuButton(ref MenuItem item)
        {
            item.RightIcon = MenuItem.Icon.LOCK;
            item.Enabled = false;
            item.Description = "Questa opzione non è disponibile.";
        }

        public void EnableMenuButton(ref MenuItem item, string description)
        {
            item.RightIcon = MenuItem.Icon.NONE;
            item.Enabled = true;
            item.Description = description;
        }

        private void Boot()
        {
            foreach (IController controller in ControllerCollection)
                controller.Boot();
        }
    }
}
