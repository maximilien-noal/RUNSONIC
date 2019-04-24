using Sega.Sonic3k.Launcher.Views;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Sega.Sonic3k.Launcher.Behaviors
{
    public static class OpenHelpOnClickBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(OpenHelpOnClickBehavior),
                new PropertyMetadata(false, OnIsEnabledPropertyChanged)
            );

        public static bool GetIsEnabled(DependencyObject obj)
        {
            var val = obj.GetValue(IsEnabledProperty);
            return (bool)val;
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        static void OnIsEnabledPropertyChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs args)
        {
            var button = dpo as Button;
            if (button == null)
                return;

            var oldValue = (bool)args.OldValue;
            var newValue = (bool)args.NewValue;

            if (!oldValue && newValue)
            {
                button.Click += OnClick;
            }
            else if (oldValue && !newValue)
            {
                button.PreviewMouseLeftButtonDown -= OnClick;
            }
        }

        static void OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;

            Process.Start("Help.pdf");
            Application.Current.MainWindow.Close();
        }

    }
}
