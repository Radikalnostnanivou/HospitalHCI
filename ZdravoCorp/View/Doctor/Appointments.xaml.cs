using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ZdravoCorp.View.Doctor
{
    /// <summary>
    /// Interaction logic for Appointments.xaml
    /// </summary>
    public partial class Appointments : Window
    {

        private AppointmentController appointmentController = new AppointmentController();
        Model.Doctor currentDoctor;
        
        PatientController patientController =  new PatientController();
        RoomController roomController = new RoomController();

        public ObservableCollection<Appointment> appointments
        {
            get;
            set;
        }
        public ObservableCollection<Model.Room> RoomCollection
        {
            get;
            set;
        }
        public ObservableCollection<Model.Patient> PatientCollection
        {
            get;
            set;
        }

        public Appointments(Model.Doctor doctor)
        {
            InitializeComponent();
            this.DataContext = this;
            appointments = new ObservableCollection<Appointment>();
            currentDoctor = doctor;

            UpdateDataGrid();
            PatientCollection = new ObservableCollection<Model.Patient>(patientController.GetAll());
            RoomCollection = new ObservableCollection<Model.Room>(roomController.GetAll());
            RoomsCB.ItemsSource = RoomCollection;
            PatientsCB.ItemsSource = PatientCollection;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            appointmentController = new AppointmentController();

            Appointment temp = (Appointment)AppointmentGrid.SelectedItem;

            if (temp == null)
            {
                MessageBox.Show("Appointment must be checked!");
            }
            else
            {
                appointmentController.Delete(temp.Id);
                MessageBox.Show("Deleted successfully!");
            }
            UpdateDataGrid();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = DateTime.Parse(textBox1.Text);
            DateTime endDate = DateTime.Parse(textBox2.Text);
            appointmentController.Create(new Model.Appointment(startDate, endDate, currentDoctor,
                (Model.Room)RoomsCB.SelectedItem, (Model.Patient)PatientsCB.SelectedItem));
            MessageBox.Show("Novi Appointment Dodat!");
            UpdateDataGrid();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if(AppointmentGrid.SelectedItem == null || textBox1.Text == "" || textBox2.Text == "" || RoomsCB.SelectedIndex == -1 || PatientsCB.SelectedIndex == -1)
            {
                MessageBox.Show("Nije oznacen ni jedan appointment!");
            }
            else
            {
                DateTime startDate = DateTime.Parse(textBox1.Text);
                DateTime endDate = DateTime.Parse(textBox2.Text);
                appointmentController.Update(new Model.Appointment(startDate, endDate, ((Model.Appointment)AppointmentGrid.SelectedItem).Id, currentDoctor,
                    (Model.Room)RoomsCB.SelectedItem, (Model.Patient)PatientsCB.SelectedItem));
                
            }
            UpdateDataGrid();
        }

        private void kartoniButton_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecords meds = new MedicalRecords(currentDoctor);
            this.Close();
            meds.Show();
        }

        private void requestsButton_Click(object sender, RoutedEventArgs e)
        {
            MedicationRequests medicationRequests = new MedicationRequests(currentDoctor);
            this.Close();
            medicationRequests.Show();
        }

        private void vacationRequestButton_Click(object sender, RoutedEventArgs e)
        {
            VacationRequest vacationRequest = new VacationRequest(currentDoctor);
            this.Close();
            vacationRequest.Show();
        }

        private void medsButton_Click(object sender, RoutedEventArgs e)
        {
            ///MedicationsView medicationsView = new MedicationsView(currentDoctor);
            this.Close();
            //medicationsView.Show();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserWindow user = new UserWindow(currentDoctor);
            user.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void AppointmentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonEnabledCheck();
            if(AppointmentGrid.SelectedIndex != -1)
            {
                Appointment appointment = (Appointment)AppointmentGrid.SelectedItem;
                textBox1.Text = appointment.StartDateString;
                textBox2.Text = appointment.EndDateString;
                foreach (Model.Patient patient in PatientCollection)
                {
                    if (patient.Id == appointment.PatientID)
                        PatientsCB.SelectedIndex = PatientCollection.IndexOf(patient);
                }
                foreach (Model.Room room in RoomCollection)
                {
                    if (room.Identifier == appointment.RoomID)
                        RoomsCB.SelectedIndex = RoomCollection.IndexOf(room);
                }
            }
            
        }

        private void UpdateDataGrid()
        {
            List<Appointment> apps = appointmentController.GetAll();
            appointments = new ObservableCollection<Appointment>();
            foreach (Appointment temp in apps)
            {
                if (temp.doctor.Id == currentDoctor.Id)
                {
                    appointments.Add(temp);
                }
            }
            AppointmentGrid.DataContext = appointments;
        }

        private void OdustaniButton_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            RoomsCB.SelectedIndex = -1;
            PatientsCB.SelectedIndex = -1;
            AppointmentGrid.SelectedIndex = -1;
        }

        private void ButtonEnabledCheck()
        {
            if (AppointmentGrid.SelectedIndex == -1)
            {
                UpdateButton.IsEnabled = false;
                CreateButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                OdustaniButton.IsEnabled = false;
            }
            else
            {
                UpdateButton.IsEnabled = true;
                CreateButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                OdustaniButton.IsEnabled = true;

            }
        }
    }
}
