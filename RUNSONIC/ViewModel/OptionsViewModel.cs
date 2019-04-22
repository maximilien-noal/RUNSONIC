using GalaSoft.MvvmLight;
using Sega.Sonic3k.Launcher.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Sega.Sonic3k.Launcher.ViewModel
{
    public class OptionsViewModel : ViewModelBase
    {
        public OptionsViewModel()
        {
        }

        public ICommand Cancel { get; private set; }

        public void CancelExecute()
        {
            
        }
    }
}
