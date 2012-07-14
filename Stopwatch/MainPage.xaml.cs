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
using System.Windows.Threading;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Shell;
using System.Diagnostics;

namespace Stopwatch
{
    public partial class MainPage : PhoneApplicationPage
    {
        private DispatcherTimer _timer = null;
        private bool isStart = false;
        private TimeSpan _time;
        private DateTime prior;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //init time counter
            _time = new TimeSpan(0, 0, 0, 0, 0);

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //prevent sleep
            IsolatedStorageSettings iss = IsolatedStorageSettings.ApplicationSettings;
            if (iss.Contains("prevent"))
            {
                //PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
            else
            {
                //PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Enabled;
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
            }

            this.DataContext = App.ViewModel;
        }

        private void settingMenu_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingPage.xaml", UriKind.Relative));
        }

        private void aboutMenu_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("你确定要重置跑表吗? 这样将丢失所有数据.",
                "确认",MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                //stop timer
                StopTimer();

                //reset _time
                this._time = new TimeSpan(0, 0, 0, 0, 0);
                this.curTime.Text = "00:00:00.000";

                //clear lab items 
                App.ViewModel.Items.Clear();
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!isStart)
            {
                //start timer
                StartTimer();
            }
            else
            {
                //stop timer
                StopTimer();
            }
            
        }

        private void btnLab_Click(object sender, RoutedEventArgs e)
        {
            if(!isStart)
            {
                MessageBox.Show("你还没有开始计时.");
                return;
            }

            //add current time item to view model
            string time = this._time.ToString("g");
            int count = App.ViewModel.Items.Count;
            string id = String.Format("计数 {0}", (++count).ToString());
            ItemViewModel newItem = new ItemViewModel() { LabId = id, Time = time };
            App.ViewModel.Items.Add(newItem);
        }

        private void StopTimer()
        {
            if(_timer != null)
            {
                _timer.Stop();
                _timer = null;
                this.isStart = false;

                //change start button content
                //start color: #CC26ED3B =>  204,38,237,59
                //pause color: #CCEBD018 => 204,235,208,24
                SolidColorBrush sBrush = new SolidColorBrush();
                sBrush.Color = Color.FromArgb(204, 38, 237, 59);
                this.btnStart.Content = "开 始";
                this.btnStart.Background = sBrush;
            }
        }

        private void StartTimer()
        {
            if(_timer == null)
            {
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromMilliseconds(1);
                _timer.Tick += new EventHandler(_timer_Tick);

                this.isStart = true;
                _timer.Start();
                this.prior = DateTime.Now;

                //change start button content
                //start color: #CC26ED3B =>  204,38,237,59
                //pause color: #CCEBD018 => 204,235,208,24
                SolidColorBrush sBrush = new SolidColorBrush();
                sBrush.Color = Color.FromArgb(204, 235, 208, 24);
                this.btnStart.Content = "暂 停";
                this.btnStart.Background = sBrush;
            }
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            //add offset
            this._time = this._time.Add((DateTime.Now).Subtract(this.prior));
            this.prior = DateTime.Now;
            Debug.WriteLine(this._time.ToString("g"));

            //update ui
            Dispatcher.BeginInvoke(() =>
            {
                this.curTime.Text = this._time.ToString("g");
            });
        }

    }
}