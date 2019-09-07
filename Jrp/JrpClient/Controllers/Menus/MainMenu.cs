using CitizenFX.Core;
using MenuAPI;

namespace JrpClient.Controllers.Menus
{
    sealed class MainMenu : IMenu
    {
        public Menu Menu { get; set; }
        public MenuItem MenuButton { get; set; }

        public void CreateMenu()
        {
            Menu = new Menu($"{Game.Player.Name}", "Sottotitolo");

            MenuController.AddMenu(Menu);
            MenuController.MenuAlignment = MenuController.MenuAlignmentOption.Right;
        }

        public void CreateMenuItems()
        {

        }
    }
}
