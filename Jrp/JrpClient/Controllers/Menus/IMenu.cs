using MenuAPI;

namespace JrpClient.Controllers.Menus
{
    internal interface IMenu
    {
        Menu Menu { get; set; }

        void CreateMenu();

        void CreateMenuItems();
    }
}
