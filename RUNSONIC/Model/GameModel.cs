using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Sega.Sonic3k.Launcher.Model
{
    public class GameModel : ObservableObject
    {
        public GameModel(string name, BitmapSource image, string arg)
        {
            Name = name;
            Image = image;
            Argument = arg;
        }

        private BitmapSource _image = null;
        public BitmapSource Image
        {
            get => _image;
            private set
            {
                Set(() => Image, ref _image, value);
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            private set
            {
                Set(() => Name, ref _name, value);
            }
        }

        private string _argument = string.Empty;

        /// <summary>
        /// sonic 3 : -3
        /// sonic 3 & Knuckles : rien
        /// sonic & Knuckles : -k
        /// </summary>
        public string Argument
        {
            get => _argument;
            private set
            {
                Set(() => Argument, ref _argument, value);
            }
        }
    }
}
