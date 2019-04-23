using Sega.Sonic3k.Launcher.Enums;
using Sega.Sonic3k.Launcher.Model;

using System;

namespace Sega.Sonic3k.Launcher.Serialization
{
    public static class OptionsSerializer
    {
        public static void SaveToDisk(OptionsModel model)
        {
            Properties.Settings.Default.IsDdrawWrapperDisabled = model.IsDdrawWrapperDisabled;
            Properties.Settings.Default.MusicMode = model.MusicMode.ToString();
            Properties.Settings.Default.GfxAlternateMode = model.GfxAlternateMode;
            Properties.Settings.Default.NoSoundFx = model.NoSoundEffects;
            Properties.Settings.Default.Save();
        }

        public static OptionsModel LoadFromDisk()
        {
            var model = new OptionsModel();
            model.IsDdrawWrapperDisabled = Properties.Settings.Default.IsDdrawWrapperDisabled;
            model.MusicMode = (MusicMode)Enum.Parse(typeof(MusicMode), Properties.Settings.Default.MusicMode);
            model.GfxAlternateMode = Properties.Settings.Default.GfxAlternateMode;
            model.NoSoundEffects = Properties.Settings.Default.NoSoundFx;
            return model;
        }
    }
}
