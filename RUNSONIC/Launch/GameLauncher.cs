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
        private readonly static string _workDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static Process _gameProc;
        private static System.Windows.Forms.Timer _waitTimer;

        public static void LaunchGame(GameModel currentGame)
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                _gameProc = Process.Start(Path.Combine(_workDir, "SONIC3K.EXE"), currentGame.Argument);
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    Application.Current.MainWindow.Visibility = Visibility.Collapsed;
                });
                _gameProc.WaitForInputIdle(5000);
                StartKillTimer();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Works around a reproductible bug : when the game's window is no more the game exited, but the process still exists
        /// </summary>
        private static void StartKillTimer()
        {
            _waitTimer = new System.Windows.Forms.Timer();
            _waitTimer.Tick += WaitTimer_Tick;
            _waitTimer.Interval = 2000;
            _waitTimer.Enabled = true;
            _waitTimer.Start();
            Application.Current.Exit += delegate { _waitTimer.Dispose(); };
        }

        private static void EnableOrDisableGraphicsWrapper()
        {
            try
            {
                string[] dgVoodooFiles = { "D3DImm.DLL", "DDraw.DLL", "D3D8.dll", "D3D9.dll", "Glide.dll", "Glide2x.dll", "Glide3x.dll" };
                foreach (var path in dgVoodooFiles)
                {
                    string fullPath = Path.Combine(_workDir, path);
                    string fullPathDisabled = Path.Combine(_workDir, string.Format("{0}{1}", Path.GetFileNameWithoutExtension(path), "Disabled.dll"));
                    RenameFile(fullPath, fullPathDisabled);
                }
            }
            catch
            {

            }
        }

        private static void RenameFile(string ddrawPath, string ddrawDisabledPath)
        {
            if (Properties.Settings.Default.IsDdrawWrapperDisabled)
            {
                if (File.Exists(ddrawPath))
                {
                    if (File.Exists(ddrawDisabledPath))
                    {
                        File.Delete(ddrawDisabledPath);
                    }
                    File.Move(ddrawPath, ddrawDisabledPath);
                }
            }
            else
            {
                if (File.Exists(ddrawDisabledPath))
                {
                    if (File.Exists(ddrawPath) == false)
                    {
                        File.Move(ddrawDisabledPath, ddrawPath);
                    }
                }
            }
        }

        private static void WaitTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _gameProc.Refresh();
                if (_gameProc.MainWindowHandle == IntPtr.Zero || _gameProc.MainWindowHandle == null)
                {
                    _gameProc.Kill();
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        //We killed the game's process, time for us to exit.
                        Application.Current.MainWindow.Close();
                    });
                }
            }
            catch
            {
                //the game already exited, there's no need for us any longer.
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    Application.Current.MainWindow.Close();
                });
            }
        }
    }

    
}
