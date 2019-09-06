using CitizenFX.Core;
using CitizenFX.Core.UI;
using JrpShared.Data;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace JrpClient.Controllers.Data
{
    sealed class Appereance
    {
        public void LoadDefaultComponentVariation()
        {
            ClearPedDecorations(Game.PlayerPed.Handle);
            ClearPedFacialDecorations(Game.PlayerPed.Handle);
            SetPedDefaultComponentVariation(Game.PlayerPed.Handle);
            SetPedHairColor(Game.PlayerPed.Handle, 0, 0);
            SetPedEyeColor(Game.PlayerPed.Handle, 0);
            ClearAllPedProps(Game.PlayerPed.Handle);

            PedHeadBlendData data = Game.PlayerPed.GetHeadBlendData();
            SetPedHeadBlendData(Game.PlayerPed.Handle, data.FirstFaceShape, data.SecondFaceShape, data.ThirdFaceShape, data.FirstSkinTone, data.SecondSkinTone, data.ThirdSkinTone, data.ParentFaceShapePercent, data.ParentSkinTonePercent, data.ParentThirdUnkPercent, data.IsParentInheritance);
        }

        public void SetComponentVariation(ComponentID componentId, int drawableId, int textureId = 0, int paletteId = 0)
        {
            SetPedComponentVariation(Game.PlayerPed.Handle, (int)componentId, drawableId, textureId, paletteId);

            switch (componentId)
            {
                case ComponentID.Hair:
                    SetPedHairColor(Game.PlayerPed.Handle, textureId, paletteId);
                    break;
            }
        }

        public void SetHeadOverlay(OverlayID overlayId, int index, int colorId, int secondColorId, float opacity = 1f)
        {
            SetPedHeadOverlay(Game.PlayerPed.Handle, (int)overlayId, index, opacity);

            switch (overlayId)
            {
                case OverlayID.Eyebrows:
                case OverlayID.FacialHair:
                case OverlayID.ChestHair:
                    SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)overlayId, 1, colorId, secondColorId);
                    break;
                case OverlayID.Blush:
                case OverlayID.Lipstick:
                    SetPedHeadOverlayColor(Game.PlayerPed.Handle, (int)overlayId, 2, colorId, secondColorId);
                    break;
            }
        }

        public void LoadSkin(ISkin skin)
        {
            SetPedHeadBlendData(Game.PlayerPed.Handle, skin.HeadBlendData.FirstFaceShape, skin.HeadBlendData.SecondFaceShape, skin.HeadBlendData.ThirdFaceShape, skin.HeadBlendData.FirstSkinTone, skin.HeadBlendData.SecondSkinTone, skin.HeadBlendData.ThirdSkinTone, skin.HeadBlendData.ParentFaceShapePercent, skin.HeadBlendData.ParentSkinTonePercent, skin.HeadBlendData.ParentThirdUnkPercent, skin.HeadBlendData.IsParentInheritance);

            SetComponentVariation(ComponentID.Face, skin.ComponentsData.Face.DrawableID, skin.ComponentsData.Face.TextureID, skin.ComponentsData.Face.PaletteID);
            SetComponentVariation(ComponentID.Mask, skin.ComponentsData.Mask.DrawableID, skin.ComponentsData.Mask.TextureID, skin.ComponentsData.Mask.PaletteID);
            SetComponentVariation(ComponentID.Hair, skin.ComponentsData.Hair.DrawableID, skin.ComponentsData.Hair.TextureID, skin.ComponentsData.Hair.PaletteID);
            SetComponentVariation(ComponentID.Torso, skin.ComponentsData.Torso.DrawableID, skin.ComponentsData.Torso.TextureID, skin.ComponentsData.Torso.PaletteID);
            SetComponentVariation(ComponentID.Leg, skin.ComponentsData.Leg.DrawableID, skin.ComponentsData.Leg.TextureID, skin.ComponentsData.Leg.PaletteID);
            SetComponentVariation(ComponentID.Parachute, skin.ComponentsData.Parachute.DrawableID, skin.ComponentsData.Parachute.TextureID, skin.ComponentsData.Parachute.PaletteID);
            SetComponentVariation(ComponentID.Shoes, skin.ComponentsData.Shoes.DrawableID, skin.ComponentsData.Shoes.TextureID, skin.ComponentsData.Shoes.PaletteID);
            SetComponentVariation(ComponentID.Accessory, skin.ComponentsData.Accessory.DrawableID, skin.ComponentsData.Accessory.TextureID, skin.ComponentsData.Accessory.PaletteID);
            SetComponentVariation(ComponentID.Undershirt, skin.ComponentsData.Undershirt.DrawableID, skin.ComponentsData.Undershirt.TextureID, skin.ComponentsData.Undershirt.PaletteID);
            SetComponentVariation(ComponentID.Kevlar, skin.ComponentsData.Kevlar.DrawableID, skin.ComponentsData.Kevlar.TextureID, skin.ComponentsData.Kevlar.PaletteID);
            SetComponentVariation(ComponentID.Badge, skin.ComponentsData.Badge.DrawableID, skin.ComponentsData.Badge.TextureID, skin.ComponentsData.Badge.PaletteID);
            SetComponentVariation(ComponentID.Torso2, skin.ComponentsData.Torso2.DrawableID, skin.ComponentsData.Torso2.TextureID, skin.ComponentsData.Torso2.PaletteID);

            SetHeadOverlay(OverlayID.Blemishes, skin.OverlaysData.Blemishes.Index, skin.OverlaysData.Blemishes.Color.ID, skin.OverlaysData.Blemishes.Color.SecondColorID, skin.OverlaysData.Blemishes.Opacity);
            SetHeadOverlay(OverlayID.FacialHair, skin.OverlaysData.FacialHair.Index, skin.OverlaysData.FacialHair.Color.ID, skin.OverlaysData.FacialHair.Color.SecondColorID, skin.OverlaysData.FacialHair.Opacity);
            SetHeadOverlay(OverlayID.Eyebrows, skin.OverlaysData.Eyebrows.Index, skin.OverlaysData.Eyebrows.Color.ID, skin.OverlaysData.Eyebrows.Color.SecondColorID, skin.OverlaysData.Eyebrows.Opacity);
            SetHeadOverlay(OverlayID.Ageing, skin.OverlaysData.Ageing.Index, skin.OverlaysData.Ageing.Color.ID, skin.OverlaysData.Ageing.Color.SecondColorID, skin.OverlaysData.Ageing.Opacity);
            SetHeadOverlay(OverlayID.Makeup, skin.OverlaysData.Makeup.Index, skin.OverlaysData.Makeup.Color.ID, skin.OverlaysData.Makeup.Color.SecondColorID, skin.OverlaysData.Makeup.Opacity);
            SetHeadOverlay(OverlayID.Blush, skin.OverlaysData.Blush.Index, skin.OverlaysData.Blush.Color.ID, skin.OverlaysData.Blush.Color.SecondColorID, skin.OverlaysData.Blush.Opacity);
            SetHeadOverlay(OverlayID.Complexion, skin.OverlaysData.Complexion.Index, skin.OverlaysData.Complexion.Color.ID, skin.OverlaysData.Complexion.Color.SecondColorID, skin.OverlaysData.Complexion.Opacity);
            SetHeadOverlay(OverlayID.SunDamage, skin.OverlaysData.SunDamage.Index, skin.OverlaysData.SunDamage.Color.ID, skin.OverlaysData.SunDamage.Color.SecondColorID, skin.OverlaysData.SunDamage.Opacity);
            SetHeadOverlay(OverlayID.Lipstick, skin.OverlaysData.Lipstick.Index, skin.OverlaysData.Lipstick.Color.ID, skin.OverlaysData.Lipstick.Color.SecondColorID, skin.OverlaysData.Lipstick.Opacity);
            SetHeadOverlay(OverlayID.Freckles, skin.OverlaysData.Freckles.Index, skin.OverlaysData.Freckles.Color.ID, skin.OverlaysData.Freckles.Color.SecondColorID, skin.OverlaysData.Freckles.Opacity);
            SetHeadOverlay(OverlayID.ChestHair, skin.OverlaysData.ChestHair.Index, skin.OverlaysData.ChestHair.Color.ID, skin.OverlaysData.ChestHair.Color.SecondColorID, skin.OverlaysData.ChestHair.Opacity);
            SetHeadOverlay(OverlayID.BodyBlemishes, skin.OverlaysData.BodyBlemishes.Index, skin.OverlaysData.BodyBlemishes.Color.ID, skin.OverlaysData.BodyBlemishes.Color.SecondColorID, skin.OverlaysData.BodyBlemishes.Opacity);
        }

        public ISkin GetCurrentSkin()
        {
            PedHeadBlendData blData = Game.PlayerPed.GetHeadBlendData();

            return new Skin()
            {
                HeadBlendData = new SharedPedHeadBlendData(blData.FirstFaceShape, blData.FirstSkinTone, blData.IsParentInheritance, blData.ParentFaceShapePercent, blData.ParentSkinTonePercent, blData.ParentThirdUnkPercent, blData.SecondFaceShape, blData.SecondSkinTone, blData.ThirdFaceShape, blData.ThirdSkinTone),
                ComponentsData = new PedComponentsData(GetComponent(ComponentID.Face), GetComponent(ComponentID.Mask), GetComponent(ComponentID.Hair), GetComponent(ComponentID.Torso), GetComponent(ComponentID.Leg), GetComponent(ComponentID.Parachute), GetComponent(ComponentID.Shoes), GetComponent(ComponentID.Accessory), GetComponent(ComponentID.Undershirt), GetComponent(ComponentID.Kevlar), GetComponent(ComponentID.Badge), GetComponent(ComponentID.Torso2)),
                OverlaysData = new PedOverlaysData(GetOverlay(OverlayID.Blemishes), GetOverlay(OverlayID.FacialHair), GetOverlay(OverlayID.Eyebrows), GetOverlay(OverlayID.Ageing), GetOverlay(OverlayID.Makeup), GetOverlay(OverlayID.Blush), GetOverlay(OverlayID.Complexion), GetOverlay(OverlayID.SunDamage), GetOverlay(OverlayID.Lipstick), GetOverlay(OverlayID.Freckles), GetOverlay(OverlayID.ChestHair), GetOverlay(OverlayID.BodyBlemishes)),
            };
        }

        public async Task LoadDefaultModel()
        {
            Screen.LoadingPrompt.Show("Caricamento...");

            while (Game.PlayerPed.Model != new Model(PedHash.FreemodeMale01))
            {
                await Game.Player.ChangeModel(new Model(PedHash.FreemodeMale01));

                await BaseScript.Delay(10);
            }

            LoadDefaultComponentVariation();

            Screen.LoadingPrompt.Hide();
        }

        private int GetDrawableVariation(ComponentID componentID) => GetPedDrawableVariation(Game.PlayerPed.Handle, (int)componentID);

        private int GetTextureVariation(ComponentID componentID) => GetPedTextureVariation(Game.PlayerPed.Handle, (int)componentID);

        private int GetPaletteVariation(ComponentID componentID) => GetPedPaletteVariation(Game.PlayerPed.Handle, (int)componentID);

        private Component GetComponent(ComponentID componentID) => new Component(GetDrawableVariation(componentID), GetTextureVariation(componentID), GetPaletteVariation(componentID));

        private Overlay GetOverlay(OverlayID overlayID)
        {
            int index = 0;
            int colorType = 0;
            int firstColor = 0;
            int secondColor = 0;
            float overlayOpacity = 0f;

            GetPedHeadOverlayData(Game.PlayerPed.Handle, (int)overlayID, ref index, ref colorType, ref firstColor, ref secondColor, ref overlayOpacity);

            return new Overlay(index, overlayOpacity, new OverlayColor(firstColor, secondColor));
        }
    }
}
