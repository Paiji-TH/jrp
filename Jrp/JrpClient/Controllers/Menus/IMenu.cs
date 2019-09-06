using MenuAPI;
using System.Collections.Generic;

namespace JrpClient.Controllers.Menus
{
    interface IMenu
    {
        Menu Menu { get; }
        ICollection<MenuItem> MenuItems { get; set; }

        void CreateMenu();

        void CreateMenuItems();
    }
}
