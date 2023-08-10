#region Imports
using CommandLine;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
#endregion

namespace ifcodes.ifwin.Console
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int width, int height, uint uFlags);

        internal class Options
        {
            [Option('t', "title")]
            public string WindowTitle { get; set; }

            [Option('p', "position")]
            public string Position { get; set; }
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                 .WithParsed<Options>(o =>
                 {
                     Screen monitor = GetFirstPortraitScreen();

                     int width = monitor.WorkingArea.Width;
                     int height = monitor.WorkingArea.Height / 2;

                     var x = monitor.WorkingArea.Location.X;
                     var y = monitor.WorkingArea.Location.Y;

                     if (o.Position == "bottom")
                     {
                         y = monitor.WorkingArea.Location.Y + height;
                     }

                     IntPtr windowHandle = FindWindow(null, o.WindowTitle);

                     const uint SWP_SHOWWINDOW = 0x0040;

                     SetWindowPos(windowHandle, IntPtr.Zero, x, y, width, height, SWP_SHOWWINDOW);
                 });

        }

        /// <summary>
        /// Loop through the available screens and determine the first screen
        /// that is in portrait orientation.
        /// </summary>
        /// <returns>The first screen in portrait orientation.</returns>
        static Screen GetFirstPortraitScreen()
        {
            for (int index = 0; index < Screen.AllScreens.Length; index++)
            {
                Screen screen = Screen.AllScreens[index];

                if (screen.Bounds.Height > screen.Bounds.Width)
                {
                    return screen;
                }
            }

            return Screen.PrimaryScreen;
        }
    }
}