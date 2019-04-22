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
        private const string Unique = "{8A0CF275-F646-4865-8EBE-D309F328AA68}";

        [STAThread]
        public static void Main()
        {
            if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
            {
                var application = new App();

                application.InitializeComponent();
                application.Run();

                // Allow single instance code to perform cleanup operations
                SingleInstance<App>.Cleanup();
            }
        }

        #region ISingleInstanceApp Members

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            // handle command line arguments of second instance

            // do nothing
            return true;
        }

        #endregion
    }
}
