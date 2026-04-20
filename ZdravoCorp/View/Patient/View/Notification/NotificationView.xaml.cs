using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ZdravoCorp.View.Patient.View.Notification
{
    /// <summary>
    /// Interaction logic for NotificationView.xaml
    /// </summary>
    public partial class NotificationView : Window, INotifyPropertyChanged
    {
        private List<String> timesOfDay = new List<String>();
        private List<String> periods = new List<String>();
        private ObservableCollection<String> collection = new ObservableCollection<String>();
        public NotificationView()
        {
            InitializeComponent();
            SetTime();
            SetPeriod();
            PickTime.ItemsSource = new ObservableCollection<String>(timesOfDay);
            PickTime.SelectedItem = timesOfDay[16];
            PickPeriod.ItemsSource = new ObservableCollection<String>(periods);
            PickPeriod.SelectedItem = periods[3];
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public void SetTime()
        {
            timesOfDay.Add("00:00");
            timesOfDay.Add("00:30");
            timesOfDay.Add("01:00");
            timesOfDay.Add("01:30");
            timesOfDay.Add("02:00");
            timesOfDay.Add("02:30");
            timesOfDay.Add("03:00");
            timesOfDay.Add("03:30");
            timesOfDay.Add("04:00");
            timesOfDay.Add("04:30");
            timesOfDay.Add("05:00");
            timesOfDay.Add("05:30");
            timesOfDay.Add("06:00");
            timesOfDay.Add("06:30");
            timesOfDay.Add("07:00");
            timesOfDay.Add("07:30");
            timesOfDay.Add("08:00");
            timesOfDay.Add("08:30");
            timesOfDay.Add("09:00");
            timesOfDay.Add("09:30");
            timesOfDay.Add("10:00");
            timesOfDay.Add("10:30");
            timesOfDay.Add("11:00");
            timesOfDay.Add("11:30");
            timesOfDay.Add("12:00");
            timesOfDay.Add("12:30");
            timesOfDay.Add("13:00");
            timesOfDay.Add("13:30");
            timesOfDay.Add("14:00");
            timesOfDay.Add("14:30");
            timesOfDay.Add("15:00");
            timesOfDay.Add("15:30");
            timesOfDay.Add("16:00");
            timesOfDay.Add("16:30");
            timesOfDay.Add("17:00");
            timesOfDay.Add("17:30");
            timesOfDay.Add("18:00");
            timesOfDay.Add("18:30");
            timesOfDay.Add("19:00");
            timesOfDay.Add("19:30");
            timesOfDay.Add("20:00");
            timesOfDay.Add("20:30");
            timesOfDay.Add("21:00");
            timesOfDay.Add("21:30");
            timesOfDay.Add("22:00");
            timesOfDay.Add("22:30");
            timesOfDay.Add("23:00");
            timesOfDay.Add("23:30");
        }
        
        public void SetPeriod()
        {
            periods.Add("1 minut");
            periods.Add("5 minuta");
            periods.Add("10 minuta");
            periods.Add("15 minuta");
            periods.Add("30 minuta");
            periods.Add("45 minuta");
            periods.Add("60 minuta");
            periods.Add("90 minuta");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
