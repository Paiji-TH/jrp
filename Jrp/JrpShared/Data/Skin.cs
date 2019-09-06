namespace JrpShared.Data
{
    public sealed class Skin : ISkin
    {
        public SharedPedHeadBlendData HeadBlendData { get; set; }
        public PedComponentsData ComponentsData { get; set; }
        public PedOverlaysData OverlaysData { get; set; }
    }
}
