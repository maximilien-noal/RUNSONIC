using Sega.Sonic3k.Launcher.Model;

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Sega.Sonic3k.Launcher.Launch
{
    public static class GameLauncher
    {
        public static void LaunchGame(GameModel currentGame)
        {
            try
            {
                Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SONIC3K.EXE"), currentGame.Argument);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
