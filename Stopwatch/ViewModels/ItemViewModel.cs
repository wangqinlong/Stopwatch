using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Stopwatch
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _labId;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string LabId
        {
            get
            {
                return _labId;
            }
            set
            {
                if (value != _labId)
                {
                    _labId = value;
                    NotifyPropertyChanged("LabId");
                }
            }
        }

        private string _time;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                if (value != _time)
                {
                    _time = value;
                    NotifyPropertyChanged("Time");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}