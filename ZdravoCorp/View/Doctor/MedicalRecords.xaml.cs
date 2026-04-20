using Controller;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ZdravoCorp.View.Doctor
{
    /// <summary>
    /// Interaction logic for MedicalRecords.xaml
    /// </summary>
    public partial class MedicalRecords : Window
    {
        private ReportController reportController = new ReportController(); 
        private MedicalRecordController medicalRecordController = new MedicalRecordController();
        PatientController patientController = new PatientController();
        private Model.Patient tempPatient;
        private Model.Doctor currentDoctor;

        public ObservableCollection<Model.Report> ReportsCollection
        {
            get;
            set;
        }

        public ObservableCollection<Model.Patient> PatientCollection
        {
            get;
            set;
        }

        public ObservableCollection<Model.Prescription> PrescriptionCollection
        {
            get;
            set;
        }
        public MedicalRecords(Model.Doctor doc)
        {
            InitializeComponent();
            currentDoctor = doc;
            this.DataContext = this;
            ReportsCollection = new ObservableCollection<Model.Report>();
            PatientCollection = new ObservableCollection<Model.Patient>(patientController.GetAll());

            PatientsCB.ItemsSource = PatientCollection;

        }

        private void PatientsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tempPatient = (Model.Patient)PatientsCB.SelectedItem;
            fillReportsGrid(tempPatient);
            FillPrescriptionGrid(tempPatient);
        }

        private void commentButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(PatientsCB.Text))
            {
                MessageBox.Show("No Patient is Selected");
            }
            else
            {
                MedicalReport med = new MedicalReport(tempPatient, currentDoctor,PatientsCB.SelectedIndex);
                this.Close();
                med.Show();


            }
        }

        private void dodajTerapiju_Click(object sender, RoutedEventArgs e)
        {
        }

        private void appointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            Appointments appointmentsWindow = new Appointments(currentDoctor);
            this.Close();
            appointmentsWindow.Show();
        }

        private void requestsButton_Click(object sender, RoutedEventArgs e)
        {
            MedicationRequests medicationRequests = new MedicationRequests(currentDoctor);
            this.Close();
            medicationRequests.Show();
        }

        private void medsButton_Click(object sender, RoutedEventArgs e)
        {
            MedicalReport medicalReport = new MedicalReport(currentDoctor);
            this.Close();
            medicalReport.Show();
        }

        private void vacationRequestButton_Click(object sender, RoutedEventArgs e)
        {
            VacationRequest vacationRequest = new VacationRequest(currentDoctor);
            this.Close();
            vacationRequest.Show();
        }

        private void kartoniButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserWindow user = new UserWindow(currentDoctor);
            user.ShowDialog();
        }

        private void requestsButton_Click_1(object sender, RoutedEventArgs e)
        {
            MedicationRequests medicationRequests = new MedicationRequests(currentDoctor);
            this.Close();
            medicationRequests.Show();
        }

        private void FillPrescriptionGrid(Model.Patient patient)
        {
            PrescriptionCollection = new ObservableCollection<Model.Prescription>();

            if (tempPatient.Prescription.Count > 0)
            {
                foreach (Model.Prescription p in tempPatient.Prescription)
                {
                    PrescriptionCollection.Add(p);
                }
                PrescriptionsGrid.DataContext = PrescriptionCollection;
            }
        }

        private void fillReportsGrid(Model.Patient patient)
        {
            List<Model.MedicalRecord> medicalRecord = medicalRecordController.GetAll();

            foreach (Model.MedicalRecord temp in medicalRecord)
            {
                if (patient.Record.Id == temp.Id)
                {
                    textBox1.Text = temp.Id.ToString();
                    textBox2.Text = temp.Weight.ToString();
                    textBox3.Text = temp.Height.ToString();
                    textBox4.Text = temp.BloodType.ToString();
                    textBox5.Text = patient.Name;
                    textBox6.Text = patient.Surname;

                    List<Model.Report> reports = temp.MedicalReports;
                    ReportsGrid.DataContext = reports;
                }
            }
        }
    }
}
