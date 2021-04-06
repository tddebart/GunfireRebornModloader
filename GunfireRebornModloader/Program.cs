using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GunfireRebornModloader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static Int32 appId = 1217060; // could also pull from reading all appmanifests
        static String gameName = "Gunfire Reborn";
        static String steamPath = Path.Combine(Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam").GetValue("SteamPath").ToString(), "steamapps");
        static String gamePath
        {
            get
            {
                if (File.Exists(Path.Combine(steamPath, $"appmanifest_{appId}.acf")))
                    return Path.Combine(steamPath, "common", gameName) + "\\";
                var lib = File.ReadAllLines(Path.Combine(steamPath, "libraryfolders.vdf"));
                foreach (var line in lib)
                {
                    if (line.Contains(@"\\"))
                    {
                        var path = line.Replace("\t", "").Replace("\\\\", "\\").Split(new char[1] { '"' }, StringSplitOptions.RemoveEmptyEntries)[1];
                        if (File.Exists(Path.Combine(path, $"steamapps\\appmanifest_{appId}.acf")))
                            return Path.Combine(path, "steamapps\\common", gameName) + "\\";
                    }
                }
                throw new Exception("Can't find game");
            }
        }
        static string gameExe = Path.Combine(gamePath, "Gunfire Reborn.exe");
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process.Start(gameExe);
            System.Threading.Thread.Sleep(3000);
            Application.Run(new Form1());
        }
    }
}
