using System.Diagnostics;

namespace Plugin.Toolkit.Image.Helpers
{
    internal class ConsoleHelper
    {
        public static void Exception(Exception ex, string message = "")
        {
#if DEBUG
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                if (message != "")
                {
                    Debug.WriteLine($"console: {message}");
                }
                Debug.WriteLine(ex.ToString());
            }
            else
            {
                if (message != "")
                {
                    System.Console.WriteLine($"console: {message}");
                }
                System.Console.WriteLine(ex.ToString());
            }
#endif
        }
    }
}
