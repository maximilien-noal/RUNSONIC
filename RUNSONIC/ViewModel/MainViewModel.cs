using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Sega.Sonic3k.Launcher.Launch;
using Sega.Sonic3k.Launcher.Model;

using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            GameLauncher.LaunchGame(CurrentGame);
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

        public ICommand GoRight { get; private set; }

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