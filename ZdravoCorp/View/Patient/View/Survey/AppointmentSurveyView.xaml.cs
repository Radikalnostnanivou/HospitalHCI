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
using Controller;
using Model;
using System.ComponentModel;
namespace ZdravoCorp.View.Patient.View.Survey
{
    /// <summary>
    /// Interaction logic for AppointmentSurveyView.xaml
    /// </summary>
    public partial class AppointmentSurveyView : Window, INotifyPropertyChanged
    {
        private Appointment appointment;
        public AppointmentSurveyView(Appointment selectedAppointmnet)
        {
            InitializeComponent();
            this.DataContext = this;
            ObservableCollection<int> grades = new ObservableCollection<int>();
            grades = SetGrades(grades);
            OverallExperienceCB.ItemsSource = grades;
            ProfesionalismCB.ItemsSource = grades;
            ComfortCB.ItemsSource = grades;
            KindnessCB.ItemsSource = grades;
            TidinessCB.ItemsSource = grades;
            RoomComfortCB.ItemsSource = grades;
            WaitingTimeCB.ItemsSource = grades;
            appointment = selectedAppointmnet;
        }
        private int profesionalism;
        private int kindness;
        private int comfort;
        private int tidiness;
        private int waitingTime;
        private int roomComfort;
        private int overallExperience;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public ObservableCollection<int> SetGrades(ObservableCollection<int> grades)
        {
            grades.Add(1);
            grades.Add(2);
            grades.Add(3);
            grades.Add(4);
            grades.Add(5);
            return grades;
        }
        public int Profesionalism
        {
            get => profesionalism;
            set
            {
                if (value != profesionalism)
                {
                    profesionalism = value;
                    OnPropertyChanged("Profesionalism");
                }
            }
        }
        public int Kindness
        {
            get => kindness;
            set
            {
                if (value != kindness)
                {
                    kindness = value;
                    OnPropertyChanged("Kindness");
                }
            }
        }
        public int Comfort
        {
            get => comfort;
            set
            {
                if (value != comfort)
                {
                    comfort = value;
                    OnPropertyChanged("Comfort");
                }
            }
        }
        public int Tidiness
        {
            get => tidiness;
            set
            {
                if (value != tidiness)
                {
                    tidiness = value;
                    OnPropertyChanged("Tidiness");
                }
            }
        }
        public int WaitingTime
        {
            get => waitingTime;
            set
            {
                if (value != waitingTime)
                {
                    waitingTime = value;
                    OnPropertyChanged("WaitingTime");
                }
            }
        }
        public int RoomComfort
        {
            get => roomComfort;
            set
            {
                if (value != roomComfort)
                {
                    roomComfort = value;
                    OnPropertyChanged("RoomComfort");
                }
            }
        }

        public int OverallExperience
        {
            get => overallExperience;
            set
            {
                if (value != overallExperience)
                {
                    overallExperience = value;
                    OnPropertyChanged("OverallExperience");
                }
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            AppointmentSurveyController appointmentSurveyController = new AppointmentSurveyController();
            appointmentSurveyController.Create(new AppointmentSurvey(Profesionalism, Kindness, Comfort, Tidiness, WaitingTime, RoomComfort, OverallExperience, appointment));
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
