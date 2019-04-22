using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Sega.Sonic3k.Launcher.Model;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Sega.Sonic3k.Launcher.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly string _workDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private Process _gameProc;
        private System.Windows.Forms.Timer _waitTimer;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Games.Add(new GameModel("Sonic 3 & Knuckles", new BitmapImage(new Uri("pack://application:,,,/RUNSONIC;component/Resources/Bitmap107.bmp")), string.Empty));
            Games.Add(new GameModel("Sonic 3", new BitmapImage(new Uri("pack://application:,,,/RUNSONIC;component/Resources/Bitmap109.bmp")), "-3"));
            Games.Add(new GameModel("Sonic & Knuckles",new BitmapImage(new Uri("pack://application:,,,/RUNSONIC;component/Resources/Bitmap108.bmp")), "-k"));

            CurrentGame = Games.FirstOrDefault();

            GoLeft = new RelayCommand(GoLeftExecute);
            GoRight = new RelayCommand(GoRightExecute);
            Play = new RelayCommand(PlayExecute);
        }

        public ICommand Play { get; private set; }

        private void PlayExecute()
        {
            try
            {
                EnableOrDisableGraphicsWrapper();
                _gameProc = Process.Start(Path.Combine(_workDir, "SONIC3K.EXE"), CurrentGame.Argument);
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
                });
                _gameProc.WaitForInputIdle(5000);
                _waitTimer = new System.Windows.Forms.Timer();
                _waitTimer.Tick += WaitTimer_Tick;
                _waitTimer.Interval = 1000;
                _waitTimer.Enabled = true;
                _waitTimer.Start();
                Application.Current.Exit += delegate { _waitTimer.Dispose(); };

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EnableOrDisableGraphicsWrapper()
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

        private void RenameFile(string ddrawPath, string ddrawDisabledPath)
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

        private void WaitTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                _gameProc.Refresh();
                if (_gameProc.MainWindowHandle == IntPtr.Zero || _gameProc.MainWindowHandle == null)
                {
                    _gameProc.Kill();
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Application.Current.MainWindow.Close();
                    });
                }
            }
            catch
            {
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    Application.Current.MainWindow.Close();
                });
            }
        }

        public ICommand GoLeft { get; private set; }

        public void GoLeftExecute()
        {
            var currentIndex = Games.IndexOf(CurrentGame);
            if(currentIndex == -1)
            {
                return;
            }
            if(currentIndex > 0)
            {
               currentIndex--;
               CurrentGame = Games[currentIndex];
            }
            else
            {
                CurrentGame = Games.LastOrDefault();
            }
        }

        public void GoRightExecute()
        {
            var currentIndex = Games.IndexOf(CurrentGame);
            if (currentIndex == -1)
            {
                return;
            }
            if (currentIndex < Games.Count() - 1)
            {
                currentIndex++;
                CurrentGame = Games[currentIndex];
            }
            else
            {
                CurrentGame = Games.FirstOrDefault();
            }
        }

        public ICommand GoRight { get; private set; }

        private ObservableCollection<GameModel> _games = new ObservableCollection<GameModel>();
        public ObservableCollection<GameModel> Games
        {
            get => _games;
            private set
            {
                Set(() => Games, ref _games, value);
            }
        }

        private GameModel _currentGame = null;

        public GameModel CurrentGame
        {
            get => _currentGame;
            private set
            {
                Set(() => CurrentGame, ref _currentGame, value);
            }
        }
    }
}