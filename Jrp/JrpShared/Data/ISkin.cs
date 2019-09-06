namespace JrpShared.Data
{
    public struct PedComponentsData
    {
        public Component Face;
        public Component Mask;
        public Component Hair;
        public Component Torso;
        public Component Leg;
        public Component Parachute;
        public Component Shoes;
        public Component Accessory;
        public Component Undershirt;
        public Component Kevlar;
        public Component Badge;
        public Component Torso2;

        public PedComponentsData(Component face, Component mask, Component hair, Component torso, Component leg, Component parachute,
            Component shoes, Component accessory, Component undershirt, Component kevlar, Component badge, Component torso2)
        {
            Face = face;
            Mask = mask;
            Hair = hair;
            Torso = torso;
            Leg = leg;
            Parachute = parachute;
            Shoes = shoes;
            Accessory = accessory;
            Undershirt = undershirt;
            Kevlar = kevlar;
            Badge = badge;
            Torso2 = torso2;
        }
    }

    public struct SharedPedHeadBlendData
    {
        public int FirstFaceShape;
        public int FirstSkinTone;
        public bool IsParentInheritance;
        public float ParentFaceShapePercent;
        public float ParentSkinTonePercent;
        public float ParentThirdUnkPercent;
        public int SecondFaceShape;
        public int SecondSkinTone;
        public int ThirdFaceShape;
        public int ThirdSkinTone;

        public SharedPedHeadBlendData(int firstFaceShape, int firstSkinTone, bool isParentInheritance, float parentFaceShapePercent,
            float parentSkinTonePercent, float parentThirdUnkPercent, int secondFaceShape, int secondSkinTone, int thirdFaceShape, int thirdSkinTone)
        {
            FirstFaceShape = firstFaceShape;
            FirstSkinTone = firstSkinTone;
            IsParentInheritance = isParentInheritance;
            ParentFaceShapePercent = parentFaceShapePercent;
            ParentSkinTonePercent = parentSkinTonePercent;
            ParentThirdUnkPercent = parentThirdUnkPercent;
            SecondFaceShape = secondFaceShape;
            SecondSkinTone = secondSkinTone;
            ThirdFaceShape = thirdFaceShape;
            ThirdSkinTone = thirdSkinTone;
        }
    }

    public struct PedOverlaysData
    {
        public Overlay Blemishes;
        public Overlay FacialHair;
        public Overlay Eyebrows;
        public Overlay Ageing;
        public Overlay Makeup;
        public Overlay Blush;
        public Overlay Complexion;
        public Overlay SunDamage;
        public Overlay Lipstick;
        public Overlay Freckles;
        public Overlay ChestHair;
        public Overlay BodyBlemishes;

        public PedOverlaysData(Overlay blemishes, Overlay facialHair, Overlay eyebrows, Overlay ageing, Overlay makeup,
            Overlay blush, Overlay complexion, Overlay sunDamage, Overlay lipstick, Overlay freckles, Overlay chestHair, Overlay bodyBlemishes)
        {
            Blemishes = blemishes;
            FacialHair = facialHair;
            Eyebrows = eyebrows;
            Ageing = ageing;
            Makeup = makeup;
            Blush = blush;
            Complexion = complexion;
            SunDamage = sunDamage;
            Lipstick = lipstick;
            Freckles = freckles;
            ChestHair = chestHair;
            BodyBlemishes = bodyBlemishes;
        }
    }

    public struct Component
    {
        public int DrawableID;
        public int TextureID;
        public int PaletteID;

        public Component(int drawableID, int textureID, int paletteID)
        {
            DrawableID = drawableID;
            TextureID = textureID;
            PaletteID = paletteID;
        }
    }

    public struct Overlay
    {
        public int Index;
        public float Opacity;
        public OverlayColor Color;

        public Overlay(int index, float opacity, OverlayColor color)
        {
            Index = index;
            Opacity = opacity;
            Color = color;
        }
    }

    public struct OverlayColor
    {
        public int ID;
        public int SecondColorID;

        public OverlayColor(int id, int secondColorID)
        {
            ID = id;
            SecondColorID = secondColorID;
        }
    }

    public enum ComponentID
    {
        Face = 0,
        Mask = 1,
        Hair = 2,
        Torso = 3,
        Leg = 4,
        Parachute = 5,
        Shoes = 6,
        Accessory = 7,
        Undershirt = 8,
        Kevlar = 9,
        Badge = 10,
        Torso2 = 11
    };

    public enum OverlayID
    {
        Blemishes = 0,
        FacialHair = 1,
        Eyebrows = 2,
        Ageing = 3,
        Makeup = 4,
        Blush = 5,
        Complexion = 6,
        SunDamage = 7,
        Lipstick = 8,
        Freckles = 9,
        ChestHair = 10,
        BodyBlemishes = 11
    }

    public interface ISkin
    {
        SharedPedHeadBlendData HeadBlendData { get; set; }
        PedComponentsData ComponentsData { get; set; }
        PedOverlaysData OverlaysData { get; set; }
    }
}
