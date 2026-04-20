using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Model;
using Controller;
using System.Globalization;
using System;

namespace ZdravoCorp.View.Doctor
{
    /// <summary>
    /// Interaction logic for MedicalReport.xaml
    /// </summary>
    public partial class MedicalReport : Window
    {
        Model.Patient currentPatient;
        Model.Doctor currentDoctor;
        AppointmentController appointmentController = new AppointmentController();
        ReportController reportController = new ReportController();
        MedicalRecordController medicalRecordController = new MedicalRecordController();
        MedicationController medicationController = new MedicationController();
        PatientController patientController = new PatientController();
        DoctorController doctorController = new DoctorController();
        RoomController roomController = new RoomController();
        PrescriptionController prescriptionController = new PrescriptionController();


        public MedicalReport(Model.Patient tempPatient, Model.Doctor tempDoctor, int index)
        {
            InitializeComponent();

            currentPatient = new Model.Patient(tempPatient);
            currentDoctor = tempDoctor;
            List<Appointment> list = new List<Appointment>();
            foreach (Appointment app in appointmentController.GetAll())
            {
                if (app.Patient.Id == currentPatient.Id && app.doctor.Id == currentDoctor.Id)
                {
                    list.Add(app);
                }
            }
            AppointmentCB.ItemsSource = list;
            MedicationsCB.ItemsSource = medicationController.GetAll();
            PatientsCB.ItemsSource = patientController.GetAll();
            DoctorsCB.ItemsSource = doctorController.GetAll();
            RoomsCB.ItemsSource = roomController.GetAll();
            PatientsCB.SelectedIndex = index;
        }

        public MedicalReport(Model.Doctor tempDoctor)
        {
            InitializeComponent();

            currentDoctor = tempDoctor;
            List<Appointment> list = new List<Appointment>();
            foreach (Appointment app in appointmentController.GetAll())
            {
                if (app.Patient.Id == currentPatient.Id && app.doctor.Id == currentDoctor.Id)
                {
                    list.Add(app);
                }
            }
            AppointmentCB.ItemsSource = list;
            MedicationsCB.ItemsSource = medicationController.GetAll();
            PatientsCB.ItemsSource = patientController.GetAll();
            DoctorsCB.ItemsSource = doctorController.GetAll();
            RoomsCB.ItemsSource = roomController.GetAll();
        }

        private void medicationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void dodajButton_Click(object sender, RoutedEventArgs e)
        {
            string komentar = textBpx1.Text;
            Appointment appointment = (Appointment)AppointmentCB.SelectedItem;
            Report report = new Report(appointment, komentar, currentDoctor);
            reportController.Create(report);
            currentPatient.Record.MedicalReports.Add(report);
            medicalRecordController.Update(currentPatient.Record);
            patientController.Update(currentPatient);
            this.Close();
        }

        private void addAppointmentToDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            Model.Doctor tempDoctor = (Model.Doctor)DoctorsCB.SelectedItem;
            Model.Room tempRoom = (Model.Room)RoomsCB.SelectedItem;
            DateTime startDate = (DateTime)datePicker.SelectedDate;
            DateTime endDate = (DateTime)datePicker.SelectedDate;
            startDate.AddHours(int.Parse(startTimeHours.Text));
            startDate.AddMinutes(int.Parse(startTimeHours.Text));
            endDate.AddMinutes(startDate.Minute + int.Parse(endTimeMinutes.Text));
            endDate.AddHours(startDate.Hour + int.Parse(endTimeHours.Text));
            bool isEmergency = (bool)CheckBox1.IsChecked;
            appointmentController.Create(new Appointment(startDate, endDate, tempDoctor, tempRoom, currentPatient));
        }

        private void appointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            Appointments appointments = new Appointments(currentDoctor);
            this.Close();
            appointments.Show();
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
            MedicalRecords medicalRecord = new MedicalRecords(currentDoctor);
            this.Close();
            medicalRecord.Show();
        }

        private void MedicationsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medication medication = MedicationsCB.SelectedItem as Medication;
            if(patientController.CheckAllergens(currentPatient,medication) == -1)
            {
                DodajButton2.Visibility = Visibility.Hidden;
                Labela.Visibility = Visibility.Visible;
                Labela.Content = "Pacijent je alergican na ovaj lek!";
            }
            else if(patientController.CheckAllergens(currentPatient, medication) == -2)
            {
                DodajButton2.Visibility = Visibility.Hidden;
                Labela.Visibility = Visibility.Visible;
                Labela.Content = "Pacijent je alergican na sastojak leka!";
            }
            else
            {
                Labela.Visibility = Visibility.Hidden;
                DodajButton2.Visibility = Visibility.Visible;
            }
        }

        private void DodajButton2_Click(object sender, RoutedEventArgs e)
        {
            int newId = prescriptionController.CreateAndReturnID(new Model.Prescription(((Medication)MedicationsCB.SelectedItem).Id, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text), textBlock.Text),currentPatient);
            MessageBox.Show(newId.ToString());
        }
    }
}
