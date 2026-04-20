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
namespace ZdravoCorp.View.Patient.Appointments
{
    /// <summary>
    /// Interaction logic for ChangeAppointment.xaml
    /// </summary>
    public partial class ChangeAppointment : Window
    {
        public DateTime selectedDate { get; set; }
        public DateTime exDate;
        public Appointment app;
        public Appointment exApp;
        public Model.Doctor doctor { get; set; }
        public DoctorController doctorController;
        public AppointmentController appointmentController;
        public int selectedIndex {get; set;}
        public event PropertyChangedEventHandler PropertyChanged;
        public Model.Doctor SelectedDoctor { get; set; }
        public ObservableCollection<Appointment> AppointmentsCollection
        {
            get;
            set;
        }
        public ObservableCollection<Model.Doctor> DoctorsCollection
        {
            get;
            set;
        }
        public ChangeAppointment(Appointment appointment)
        {
            InitializeComponent();
            this.DataContext = this;
            appointmentController = new AppointmentController();
            doctorController = new DoctorController();
            this.app = appointment;
            exApp = appointment;
           
            Model.Doctor doctorr = doctorController.Read(appointment.doctor.Id);
            int i = 0;
            foreach (Model.Doctor d in doctorController.GetAll())
            {
                if (d.Id == doctorr.Id)
                {
                    DoctorsCB.SelectedIndex = i;
                    break;
                }
                i++;
            }
            DoctorsCollection = new ObservableCollection<Model.Doctor>(doctorController.GetAll());
            DoctorsCB.ItemsSource = DoctorsCollection;
            DateTime dt = appointment.StartDate;
            String s = dt.ToString();
            datePicker.SelectedDate = appointment.StartDate;
            //selectedDate = appointment.startDate;
            this.exDate = appointment.StartDate;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            Model.Doctor doctor = (Model.Doctor)DoctorsCB.SelectedValue;
            DateTime date = (DateTime)datePicker.SelectedDate;
            List<Appointment> apps = new List<Appointment>();
            TimeSpan lessDays = new TimeSpan(-1, 0, 0, 0);
            TimeSpan moreDays = new TimeSpan(2, 0, 0, 0);
            DateTime less = exDate.Date + lessDays;
            DateTime more = exDate.Date + moreDays;
            PatientController patientController = new PatientController();
            Model.Patient patient = patientController.Read(exApp.Patient.Id);
            if (date < less)
            {
                MessageBox.Show("Prvobitan datum mozete pomeriti najvise 1 dan uznazad.\nPrvobitan datum :\t" + exDate.ToString(), "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else if (date > more)
            {
                MessageBox.Show("Prvobitan datum mozete pomeriti najvise 2 dana unapred.\nPrvobitan datum :\t" + exDate.ToString(), "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (DateRB.IsChecked == true)
                {
                    apps = appointmentController.SuggestAppointments(new WantedAppointment(doctor, date, date.AddMinutes(45), false, true, patient));
                    //apps = appointmentController.SuggestAppointments(doctor, date, date.AddMinutes(45), false, true, patient);
                }
                else
                {
                    apps = appointmentController.SuggestAppointments(new WantedAppointment(doctor, date, date.AddMinutes(45), true, true, patient));
                    //apps = appointmentController.SuggestAppointments(doctor, date, date.AddMinutes(45), true, true, patient);

                }
                AppointmentsCollection = new ObservableCollection<Appointment>(apps);
                TableForSuggestedApp.DataContext = AppointmentsCollection;
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (TableForSuggestedApp.SelectedIndex == -1)
            {
                return;
            }
            Appointment appointment = AppointmentsCollection.ElementAt(TableForSuggestedApp.SelectedIndex);
            PatientController patientController = new PatientController();
            Model.Patient patient = patientController.Read(appointment.Patient.Id);
            
            appointment.Id = exApp.Id;
            int i = appointment.Id;
            appointmentController.Update(appointment);

            DoctorController doctorController = new DoctorController();
            Model.Doctor doctor = doctorController.Read(appointment.Doctor.Id);
            doctor.RemoveAppointment(exApp);
            doctor.AddAppointment(appointment);
            doctorController.Update(doctor);
            RoomController roomController = new RoomController();
            Model.Room room = roomController.Read(appointment.Room.Identifier);
            room.RemoveAppointment(exApp);
            room.AddAppointment(appointment);
            roomController.Update(room);
            patient.RemoveAppointment(exApp);
            patient.AddAppointment(appointment);
            patient.ChangedOrCanceledAppointmentsDates.Add(DateTime.Now);
            patientController.Update(patient);
            this.Close();
            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
