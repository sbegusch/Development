using System;
using System.Runtime.InteropServices;

namespace myAdminTool.Classes
{
    public static class ConsoleHelper
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        public static bool Show()
        {
            var handle = GetConsoleWindow();
            
            if (IsVisible)
            {
                // Hide
                ShowWindow(handle, SW_HIDE);
                IsVisible = false;
            }
            else
            {
                // Show
                ShowWindow(handle, SW_SHOW);
                IsVisible = true;
            }
            return IsVisible;
        }

        public static bool IsVisible { get; set; }
    }
}
