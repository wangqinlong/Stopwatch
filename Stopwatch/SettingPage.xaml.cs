using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;

namespace Stopwatch
{
    public partial class SettingPage : PhoneApplicationPage
    {
        public SettingPage()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(SettingPage_Loaded);
        }

        void SettingPage_Loaded(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings iss = IsolatedStorageSettings.ApplicationSettings;
            if (iss.Contains("prevent"))
            {
                tsPrevent.IsChecked = true;
                tsPrevent.Content = "禁用";
            }
            else
            {
                tsPrevent.IsChecked = false;
                tsPrevent.Content = "启用";
            }
        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings iss = IsolatedStorageSettings.ApplicationSettings;
            if (!iss.Contains("prevent"))
            {
                bool isPrevent = true;
                iss.Add("prevent", isPrevent);
            }

            tsPrevent.Content = "禁用";
        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings iss = IsolatedStorageSettings.ApplicationSettings;
            if (iss.Contains("prevent"))
            {
                iss.Remove("prevent");
            }

            tsPrevent.Content = "启用";
        }
    }
}