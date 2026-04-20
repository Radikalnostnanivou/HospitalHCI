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
using Controller;
using System.Collections.ObjectModel;
using Model;
using System.ComponentModel;

namespace ZdravoCorp.View.Secretary
{
    /// <summary>
    /// Interaction logic for AddAppointment.xaml
    /// </summary>
    public partial class AddAppointment : Window, INotifyPropertyChanged
    {
        public DoctorController doctorController;
        public PatientController patientController;
        public AppointmentController appointmentController;
        public ObservableCollection<Model.Doctor> DoctorsCollection
        {
            get;
            set;
        }
        public ObservableCollection<Model.Patient> PatientsCollection
        {
            get;
            set;
        }
        public ObservableCollection<Model.Appointment> AppointmentsCollection
        {
            get;
            set;
        }

        private string jmbg;
        private string name;
        private string surname;
        private DateTime date;

        public String Jmbg
        {
            get { return jmbg; }
            set
            {
                if (value != jmbg)
                {
                    jmbg = value;
                    OnPropertyChanged("Jmbg");
                }
            }
        }

        public String Namee
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Namee");
                }
            }
        }

        public String Surname
        {
            get { return surname; }
            set
            {
                if (value != surname)
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (value != date)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public AddAppointment()
        {
            InitializeComponent();
            doctorController = new DoctorController();
            patientController = new PatientController();
            appointmentController = new AppointmentController();
            DoctorsCollection = new ObservableCollection<Model.Doctor>(doctorController.GetAll());
            PatientsCollection = new ObservableCollection<Model.Patient>(patientController.GetAll());
           
            Doctors.ItemsSource = DoctorsCollection;

        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void SearchPatient_Click(object sender, RoutedEventArgs e)
        {
            jmbg = JMBG.Text;
            Model.Patient patient = patientController.ReadPatientByJmbg(jmbg);
            NAME.Text = patient.Name;
            SURNAME.Text = patient.Surname;
        }

        private void SuggestAppointments_Click(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            Model.Doctor d = (Model.Doctor)Doctors.SelectedItem;
            Model.Doctor doctor = doctorController.Read(d.Id);
            DateTime date = (DateTime) DATE.SelectedDate;
            if(date < DateTime.Today)
            {
                MessageBox.Show("Nije moguce izabrati datum u proslosti", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            List<Appointment> apps = new List<Appointment>();
            date.AddHours((int)doctor.WorkStartTime.Hour);
            date.AddMinutes((int)doctor.WorkStartTime.Minute);
            date.AddSeconds((int)doctor.WorkStartTime.Second);
            Model.Patient patient = patientController.ReadPatientByJmbg(jmbg);
            if (DateButton.IsChecked == true)
            {
                apps = appointmentController.SuggestAppointments(new WantedAppointment(doctor, date, date.AddMinutes(45), false, true, patient));
                //apps = appointmentController.SuggestAppointments(doctor, date, date.AddMinutes(45), false, true, patient);
            }
            else
            {
               apps = appointmentController.SuggestAppointments(new WantedAppointment(doctor, date, date.AddMinutes(45), true, true, patient));
                //apps = appointmentController.SuggestAppointments(doctor, date, date.AddMinutes(45), true, true, patient);

            }

            
            AppointmentSuggest window = new AppointmentSuggest(apps);
            window.ShowDialog();
        }

        private void CancelAddAppointment_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
