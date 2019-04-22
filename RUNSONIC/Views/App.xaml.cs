using Sega.Sonic3k.Launcher.InstanceModel;

using System;
using System.Collections.Generic;
using System.Windows;

namespace Sega.Sonic3k.Launcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        private const string _uniqueId = "{8A0CF275-F646-4865-8EBE-D309F328AA68}";

        [STAThread]
        public static void Main()
        {
            if (SingleInstance<App>.InitializeAsFirstInstance(_uniqueId))
            {
                var application = new App();

                application.InitializeComponent();
                application.Run();

                // Allow single instance code to perform cleanup operations
                SingleInstance<App>.Cleanup();
            }
        }

        /// <summary>
        /// Handles command line arguments of second instance
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            // do nothing
            return true;
        }
    }
}
