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
using Model;
using Controller;
namespace ZdravoCorp.View.Patient.MedicalRecord
{
    /// <summary>
    /// Interaction logic for Anamnesis.xaml
    /// </summary>
    public partial class Anamnesis : Window
    {
        private Model.Anamnesis anamnesis;
        private AnamnesisController anamnesisController = new AnamnesisController();
        private PatientController patientController = new PatientController();
        private DoctorController doctorController = new DoctorController();
        private MedicationController medicineController = new MedicationController();
        private Appointment appointment;
        public Anamnesis(Model.Appointment appointment)
        {
            InitializeComponent();
            this.DataContext = this;
            anamnesis = anamnesisController.FindAnamnesisByAppointmentId(appointment.Id);
            Patient.Content = patientController.Read(appointment.Patient.Id).Name +" " + patientController.Read(appointment.Patient.Id).Surname;
            Doctor.Content = doctorController.Read(appointment.Doctor.Id).nameSurname;
            Date.Content = appointment.startDate.ToShortDateString();
            DoctorType.Content = doctorController.Read(appointment.Doctor.Id).DoctorType.Type;
            AppointmentType.Content = anamnesis.AppointmentType;
            Diagnosis.Content = anamnesis.Diagnosis;
            this.appointment = appointment;
            Presciption.Content = "Brufen 600mg. 2 puta dnevno.\n Razmak izmedju uzimanja minimum 6 sati.";
            Note.Text = anamnesis.Note;
        }
        private void AppointmentSurvey_Click(object sender, RoutedEventArgs e)
        {
            AppointmentSurveyController appointmentSurveyController = new AppointmentSurveyController();
            
            if (appointmentSurveyController.DoneSurvey(appointment))
            {
                MessageBox.Show("Vec ste popunili anketu za ovaj pregled", "Pregled ocenjen", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            ZdravoCorp.View.Patient.View.Survey.AppointmentSurveyView window = new ZdravoCorp.View.Patient.View.Survey.AppointmentSurveyView(appointment);
            window.ShowDialog();
        }

        private void SaveNote_Click(object sender, RoutedEventArgs e)
        {
            anamnesis.Note = Note.Text;
            anamnesisController.Update(anamnesis);
        }
    }
}
