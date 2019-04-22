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
        private Process _gameProc;
        private System.Windows.Forms.Timer _waitTimer;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Games.Add(new GameModel() { Name = "Sonic 3 & Knuckles", Image = new BitmapImage(new Uri("pack://application:,,,/RUNSONIC;component/Resources/Bitmap107.bmp"))});
            Games.Add(new GameModel() { Argument = "-3", Name = "Sonic 3", Image = new BitmapImage(new Uri("pack://application:,,,/RUNSONIC;component/Resources/Bitmap109.bmp"))});
            Games.Add(new GameModel() { Argument = "-k", Name = "Sonic & Knuckles", Image = new BitmapImage(new Uri("pack://application:,,,/RUNSONIC;component/Resources/Bitmap108.bmp")) });

            CurrentGame = Games.FirstOrDefault();

            Exit = new RelayCommand(ExitExecute);
            GoLeft = new RelayCommand(GoLeftExecute);
            GoRight = new RelayCommand(GoRightExecute);
            Play = new RelayCommand(PlayExecute);
        }

        public ICommand Play { get; private set; }

        private void PlayExecute()
        {
            try
            {
                _gameProc = Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SONIC3K.EXE"), CurrentGame.Argument);
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

        public ICommand Exit { get; private set; }

        private void ExitExecute()
        {
            Application.Current.MainWindow.Close();
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
            set
            {
                Set(() => Games, ref _games, value);
            }
        }

        private GameModel _currentGame = new GameModel();

        public GameModel CurrentGame
        {
            get => _currentGame;
            set
            {
                Set(() => CurrentGame, ref _currentGame, value);
            }
        }
    }
}