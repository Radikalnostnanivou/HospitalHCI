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
   
    public partial class AddAppointment : Window, INotifyPropertyChanged
    {
        public Appointment app;
        public DoctorController doctorController;
        public AppointmentController appointmentController;
        Model.Patient patient;
        public event PropertyChangedEventHandler PropertyChanged;
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
       
        public AddAppointment(Model.Patient logedPatient)
        {
            InitializeComponent();
            DataContext = this;
            doctorController = new DoctorController();
            appointmentController = new AppointmentController();
            patient = logedPatient;
            AppointmentsCollection = new ObservableCollection<Appointment>();
            DoctorsCollection = new ObservableCollection<Model.Doctor>(doctorController.GetAll());
            DoctorsCB.ItemsSource = DoctorsCollection;
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            Model.Doctor d = (Model.Doctor)DoctorsCB.SelectedItem;
            Model.Doctor doctor = doctorController.Read(d.Id);
            DateTime date = (DateTime)datePicker.SelectedDate;
            List<Appointment> apps;
            date.AddHours((int)doctor.WorkStartTime.Hour);
            date.AddMinutes((int)doctor.WorkStartTime.Minute);
            date.AddSeconds((int)doctor.WorkStartTime.Second);
            if (DateRB.IsChecked == true)
            {
                apps = appointmentController.SuggestAppointments(new WantedAppointment(doctor, date, date.AddMinutes(45), false, true, patient)); //SuggestAppointments(doctor, date, date.AddMinutes(45), false, true, patient);
            }
            else
            {
                apps = appointmentController.SuggestAppointments(new WantedAppointment(doctor, date, date.AddMinutes(45), true, true, patient));
               
            }

            AppointmentsCollection = new ObservableCollection<Appointment>(apps);
            TableForSuggestedApp.DataContext = AppointmentsCollection;

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (TableForSuggestedApp.SelectedIndex == -1)
            {
                return;
            }
            app = AppointmentsCollection.ElementAt(TableForSuggestedApp.SelectedIndex);
            PatientController pc = new PatientController();

            RoomController rc = new RoomController();

            appointmentController.Create(app);
            Room r = app.Room;
            r.AddAppointment(app);
            rc.Update(r);
            Model.Doctor d = app.doctor;
            d.AddAppointment(app);
            doctorController.Update(d);
            Model.Patient p = app.Patient;
            p.AddAppointment(app);
            pc.Update(p);
            this.Close();
        }

    

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
