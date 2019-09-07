using CitizenFX.Core;
using MenuAPI;
using static JrpClient.Client;
using static CitizenFX.Core.Native.API;

namespace JrpClient.Controllers.Menus
{
    class SessionMenu : IMenu
    {
        public Menu Menu { get; set; }
        public MenuItem MenuButton { get; set; }

        public void CreateMenu()
        {
            Menu = new Menu($"{Game.Player.Name}", "Sottotitolo");
            MenuButton = new MenuItem("Nome", "Descrizione") { Label = "→→→" };

            Menu.OnMenuOpen += new Menu.MenuOpenedEvent(OnMenuOpen);

            MenuController.AddSubmenu(GetInstance().MainMenu.Menu, Menu);
            MenuController.BindMenuItem(GetInstance().MainMenu.Menu, Menu, MenuButton);

            GetInstance().MainMenu.Menu.AddMenuItem(MenuButton);
        }

        public void CreateMenuItems()
        {

        }

        public async void OnMenuOpen(Menu menu)
        {
            menu.ClearMenuItems();

            foreach (var session in await GetInstance().Game.FetchSessions())
            {
                if (GetInstance().Game.IsPlayerActive(int.Parse(session.Key)))
                {
                    if (!string.IsNullOrEmpty(session.Value.Character.Name))
                        menu.AddMenuItem(new MenuItem(session.Value.Character.Name) { Label = session.Key });
                    else
                        menu.AddMenuItem(new MenuItem("Connettendo...") { Label = session.Key });
                }
            }
        }
    }
}
