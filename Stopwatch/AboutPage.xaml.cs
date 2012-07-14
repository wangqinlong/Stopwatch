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
using Microsoft.Phone.Tasks;

namespace Stopwatch
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            //send support email
            EmailComposeTask MyEmailComposer = new EmailComposeTask();

            MyEmailComposer.To = "longhunt1989@hotmail.com";
            MyEmailComposer.Subject = "WP7程序-跑表反馈";
            MyEmailComposer.Body = "请输入您的反馈信息:";

            MyEmailComposer.Show();
        }
    }
}