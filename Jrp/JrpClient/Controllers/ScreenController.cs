using CitizenFX.Core.UI;
using JrpClient.Controllers.Data;
using JrpShared.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;
using static JrpClient.Client;

namespace JrpClient.Controllers
{
    internal sealed class ScreenController : IController
    {
        public IDictionary<string, Draw2DText> TextModulesByName;
        public IReadOnlyCollection<string> IPLCollection;

        public void Boot()
        {
            GetInstance().RegisterTickHandler(RemoveHudComponents);

            TextModulesByName = new Dictionary<string, Draw2DText>();
            IPLCollection = new Collection<string>()
            {
                "shr_int",
                "TrevorsTrailerTidy",
                "post_hiest_unload",
                "id2_14_during_door",
                "id2_14_during1",
                "CS3_07_MPGates",
                "RC12B_Default",
            };
        }

        public void Init()
        {
            LoadIPL();
        }

        public void Add2DTextModule(string name, float x, float y, float size, Action text)
        {
            TextModulesByName.Add(name, new Draw2DText(x, y, size, text));

            GetInstance().RegisterTickHandler(TextModulesByName[name].GetHandler);
        }

        public void Remove2DTextModule(string name)
        {
            GetInstance().DeregisterTickHandler(TextModulesByName[name].GetHandler);

            GC.SuppressFinalize(TextModulesByName[name]);

            TextModulesByName.Remove(name);
        }

        public async Task RemoveHudComponents()
        {
            Screen.Hud.HideComponentThisFrame(HudComponent.AreaName);
            Screen.Hud.HideComponentThisFrame(HudComponent.Cash);
            Screen.Hud.HideComponentThisFrame(HudComponent.StreetName);
            Screen.Hud.HideComponentThisFrame(HudComponent.VehicleName);
            Screen.Hud.HideComponentThisFrame(HudComponent.WantedStars);
            Screen.Hud.HideComponentThisFrame(HudComponent.WeaponWheelStats);
            Screen.Hud.HideComponentThisFrame(HudComponent.WeaponIcon);
            Screen.Hud.HideComponentThisFrame(HudComponent.MpCash);
        }

        private void LoadIPL()
        {
            foreach (var ipl in IPLCollection)
            {
                if (!IsIplActive(ipl))
                    RequestIpl(ipl);
            }
        }
    }
}
