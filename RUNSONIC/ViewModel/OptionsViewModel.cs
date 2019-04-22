﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Sega.Sonic3k.Launcher.Model;
using Sega.Sonic3k.Launcher.Serialization;

using System.Windows.Input;

namespace Sega.Sonic3k.Launcher.ViewModel
{
    public class OptionsViewModel : ViewModelBase
    {
        public OptionsViewModel()
        {
            Model = BinarySerializer.LoadFromDisk();
            Accept = new RelayCommand(AcceptExecute);
        }

        private OptionsModel _model = new OptionsModel();

        public OptionsModel Model
        {
            get => _model;
            private set
            {
                Set(() => Model, ref _model, value);
            }
        }

        public ICommand SetNewMusicMode { get; private set; }

        public void AcceptExecute()
        {
            BinarySerializer.SaveToDisk(Model);
        }

        public ICommand Accept { get; private set; }
    }
}
