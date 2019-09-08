using CitizenFX.Core;
using CitizenFX.Core.UI;
using MenuAPI;
using System.Threading.Tasks;
using static JrpClient.Client;
using static JrpShared.Helpers.Common;
using static JrpShared.Helpers.Serialization;

namespace JrpClient.Controllers.Menus
{
    internal sealed class PedCreationMenu : IMenu
    {
        public Menu Menu { get; set; }

        public MenuCheckboxItem GenderCheckbox;
        public MenuItem ExitButton;
        public MenuItem NameButton;
        public string CharacterName;

        public void CreateMenu()
        {
            Menu = new Menu($"Creazione Personggio", "Sottotitolo");

            Menu.OnMenuOpen += new Menu.MenuOpenedEvent(OnMenuOpen);
            Menu.OnMenuClose += new Menu.MenuClosedEvent(OnMenuClose);
            Menu.OnItemSelect += new Menu.ItemSelectEvent(OnItemSelect);
            Menu.OnCheckboxChange += new Menu.CheckboxItemChangeEvent(OnCheckboxChange);

            MenuController.AddSubmenu(GetInstance().MainMenu.Menu, Menu);
        }

        public void CreateMenuItems()
        {
            GenderCheckbox = new MenuCheckboxItem("Cambia Sesso", "È un maschio o una femmina?") { Style = MenuCheckboxItem.CheckboxStyle.Cross, Checked = true };
            ExitButton = new MenuItem("Completa La Creazione", "Descrizione");
            NameButton = new MenuItem("Nome Completo", "Descrizione");

            Menu.AddMenuItem(GenderCheckbox);
            Menu.AddMenuItem(ExitButton);
            Menu.AddMenuItem(NameButton);
        }

        public async void OnMenuOpen(Menu menu)
        {
            MenuController.DisableBackButton = true;

            await ChangeGender(true);

            Game.PlayerPed.IsVisible = true;
        }

        public void OnMenuClose(Menu menu)
        {
            MenuController.DisableBackButton = false;

            ExitButton.Label = string.Empty;
        }

        public async void OnCheckboxChange(Menu menu, MenuCheckboxItem item, int index, bool state)
        {
            switch (item.Text)
            {
                case "Cambia Sesso":
                    await ChangeGender(state);
                    break;
            }
        }

        public async void OnItemSelect(Menu menu, MenuItem item, int index)
        {
            switch (item.Text)
            {
                case "Completa La Creazione":
                    FinalizeCharacterCreation();
                    break;
                case "Nome Completo":
                    await GetCharacterName();
                    break;
            }
        }

        private void FinalizeCharacterCreation()
        {
            if (!string.IsNullOrEmpty(CharacterName))
            {
                if (string.IsNullOrEmpty(ExitButton.Label))
                    ExitButton.Label = "Sei sicuro?";
                else
                {
                    BaseScript.TriggerServerEvent("jrp:notifyCharacterCreation", CharacterName, SerializeObject(GetInstance().Game.Appereance.GetCurrentSkin()));

                    Menu.CloseMenu();
                }
            }
            else
                Screen.ShowNotification("Devi prima ~r~inserire un nome~w~!");
        }

        private async Task GetCharacterName()
        {
            while (true)
            {
                CharacterName = await Game.GetUserInput(25);

                if (ValidateCharacterName(ref CharacterName))
                {
                    NameButton.Label = CharacterName;
                    break;
                }
                else
                    Screen.ShowNotification("Inserisci un ~g~nome completo~w~!");
            }
        }

        private async Task ChangeGender(bool male)
        {
            if (male)
                await GetInstance().Game.Appereance.LoadDefaultModel(PedHash.FreemodeMale01);
            else
                await GetInstance().Game.Appereance.LoadDefaultModel(PedHash.FreemodeFemale01);
        }
    }
}
