using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Sega.Sonic3k.Launcher.Model;

namespace Sega.Sonic3k.Launcher.Serialization
{
    public static class BinarySerializer
    {
        private const string _fileName = "Sonic3K.BIN";

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public static void SaveToDisk(OptionsModel model)
        {
            Properties.Settings.Default.IsDdrawWrapperDisabled = model.IsDdrawWrapperDisabled;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public static OptionsModel LoadFromDisk()
        {
            var model = new OptionsModel();
            model.IsDdrawWrapperDisabled = Properties.Settings.Default.IsDdrawWrapperDisabled;
            return model;
        }
    }
}
