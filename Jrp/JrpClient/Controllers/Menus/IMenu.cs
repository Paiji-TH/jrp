using MenuAPI;

namespace JrpClient.Controllers.Menus
{
    interface IMenu
    {
        Menu Menu { get; set; }
        MenuItem MenuButton { get; set; }

        void CreateMenu();

        void CreateMenuItems();
    }
}
