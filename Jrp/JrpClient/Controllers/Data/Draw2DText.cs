using System;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace JrpClient.Controllers.Data
{
    sealed class Draw2DText
    {
        public float X;
        public float Y;
        public float Size;
        public Action Action;

        public Draw2DText(float x, float y, float size, Action action)
        {
            X = x;
            Y = y;
            Size = size;
            Action = action;
        }

        public async Task GetHandler()
        {
            SetTextFont(0);
            SetTextScale(0, Size);
            SetTextOutline();

            SetTextEntry("STRING");

            Action.Invoke();

            DrawText(X, Y);
        }
    }
}
