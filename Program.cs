using System;
using System.IO;
using System.Windows.Forms;

namespace Vivy
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string sessionPath = "user_session.txt";

            if (File.Exists(sessionPath))
            {
                string savedLogin = File.ReadAllText(sessionPath);
                if (!string.IsNullOrWhiteSpace(savedLogin))
                {
                    Application.Run(new FrmMain(savedLogin));
                    return;
                }
            }

            Application.Run(new FrmLogin()); //
        }
    }
}
