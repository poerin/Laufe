using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Laufe
{
    internal static class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, Keys key);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [STAThread]
        private static void Main()
        {
            System.Diagnostics.Process[] ProcessList = System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName);

            if (ProcessList.Length > 1)
            {
                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Laufe laufe = new Laufe();
            RegisterHotKey(laufe.Handle, 493, 8, Keys.Oemtilde);
            laufe.Opacity = 0;
            laufe.Show();
            Application.Run();
        }
    }
}
