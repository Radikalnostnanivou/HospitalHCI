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
    /// Interaction logic for ChangeAppointment.xaml
    /// </summary>
    public partial class ChangeAppointment : Window, INotifyPropertyChanged
    {
        public DoctorController doctorController;
        public PatientController patientController;
        public AppointmentController appointmentController;
        public Model.Appointment appointment;   
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string jmbg;
        private string name;
        private string surname;
        private Model.Doctor doctor { get; set; }
        private DateTime date;
        private Model.Room exRoom { get; set; }
        private Model.Appointment exApp { get; set; }



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

        
        public ChangeAppointment(Model.Appointment appointment)
        {
            InitializeComponent();
            doctorController = new DoctorController();
            patientController = new PatientController();
            appointmentController = new AppointmentController();
            DataContext = this;
            this.appointment = appointment;
            this.name = appointment.Patient.Name;
            this.surname = appointment.Patient.Surname;
            this.doctor = doctorController.Read(appointment.doctor.Id);
            this.jmbg = appointment.Patient.Jmbg;
            int i = 0;
            foreach(Model.Doctor d in doctorController.GetAll())
            {
                if(d.Id == doctor.Id) 
                {
                    Doctors.SelectedIndex = i;
                }
                i++;
            }
            exRoom = appointment.room;
            exApp = appointment;

            this.date = appointment.startDate.Date;
            //DATE.SelectedDate = date;



            DoctorsCollection = new ObservableCollection<Model.Doctor>(doctorController.GetAll());
            PatientsCollection = new ObservableCollection<Model.Patient>(patientController.GetAll());

            Doctors.ItemsSource = DoctorsCollection;
        }

        private void CancelChangeAppointment_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SuggestAppointments_Click(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            Model.Doctor d = (Model.Doctor)Doctors.SelectedItem;
            Model.Doctor doctorr = doctorController.Read(d.Id);
            DateTime date = (DateTime)DATE.SelectedDate;
            if (date < DateTime.Today)
            {
                MessageBox.Show("Nije moguce izabrati datum u proslosti", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            List<Appointment> apps = new List<Appointment>();
            date.AddHours((int)doctorr.WorkStartTime.Hour);
            date.AddMinutes((int)doctorr.WorkStartTime.Minute);
            date.AddSeconds((int)doctorr.WorkStartTime.Second);
            Model.Patient patient = patientController.ReadPatientByJmbg(jmbg);
            apps = appointmentController.SuggestAppointments(new WantedAppointment(doctor, date, date.AddMinutes(45), true, true, patient));

            AppointmentSuggestChange window = new AppointmentSuggestChange(apps, exRoom, exApp);
            window.ShowDialog();
        }
    }
}
