using CitizenFX.Core;

namespace JrpServer
{
    internal static class Log
    {
        public enum MessageType
        {
            Success,
            Warning,
            Error,
        }

        public enum Color
        {
            Red,
            Green,
            Yellow,
        }

        public static void WriteToConsole(string value, MessageType messageType = MessageType.Success)
        {
            switch (messageType)
            {
                case MessageType.Success:
                    Debug.WriteLine($"[" + "Jrp".ToColor(Color.Yellow) + $"] {value} " + "[Ok]".ToColor(Color.Green));
                    break;
                case MessageType.Warning:
                    Debug.WriteLine($"[" + "Jrp".ToColor(Color.Yellow) + $"] {value} " + "[Warning]".ToColor(Color.Yellow));
                    break;
                case MessageType.Error:
                    Debug.WriteLine($"[" + "Jrp".ToColor(Color.Yellow) + $"] {value} " + "[Error]".ToColor(Color.Red));
                    break;
            }
        }

        public static string ToColor(this string value, Color color)
        {
            switch (color)
            {
                case Color.Red:
                    return $"^1{value}^7";
                case Color.Green:
                    return $"^2{value}^7";
                case Color.Yellow:
                    return $"^3{value}^7";
                default:
                    return string.Empty;
            }
        }
    }
}
