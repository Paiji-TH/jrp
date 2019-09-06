using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using MenuAPI;

namespace JrpClient.Controllers.Menus
{
    sealed class MainMenu : IMenu
    {
        public Menu Menu { get; }
        public ICollection<MenuItem> MenuItems { get; set; }

        public void CreateMenu()
        {
            Menu menu = new Menu($"{Game.Player.Name}", "Sottotitolo");
            MenuController.AddMenu(menu);
            MenuController.MenuAlignment = MenuController.MenuAlignmentOption.Right;
        }

        public void CreateMenuItems()
        {
            MenuItems = new Collection<MenuItem>();
        }
    }
}
