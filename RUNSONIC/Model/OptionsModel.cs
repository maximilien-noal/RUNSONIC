using GalaSoft.MvvmLight;

using Sega.Sonic3k.Launcher.Enums;

namespace Sega.Sonic3k.Launcher.Model
{
    public class OptionsModel : ObservableObject
    {
        private bool _isDdrawWrapperDisabled = false;

        public bool IsDdrawWrapperDisabled
        {
            get => _isDdrawWrapperDisabled;
            set
            {
                Set(() => IsDdrawWrapperDisabled, ref _isDdrawWrapperDisabled, value);
            }
        }

        private MusicMode _musicMode = MusicMode.FMSynth;
        public MusicMode MusicMode
        {
            get => _musicMode;
            set
            {
                Set(() => MusicMode, ref _musicMode, value);
            }
        }

        private bool _noSoundEffects = false;
        public bool NoSoundEffects
        {
            get => _noSoundEffects;
            set
            {
                Set(() => NoSoundEffects, ref _noSoundEffects, value);
            }
        }

        private bool _gfxAlternateMode = false;

        public bool GfxAlternateMode
        {
            get => _gfxAlternateMode;
            set
            {
                Set(() => GfxAlternateMode, ref _gfxAlternateMode, value);
            }
        }
    }
}
