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
using System.Collections.ObjectModel;

namespace ZdravoCorp.View.Doctor
{
    /// <summary>
    /// Interaction logic for VacationRequest.xaml
    /// </summary
    public partial class VacationRequest : Window
    {
        Model.Doctor currentDoctor;
        VacationController vacationController = new VacationController();
        AppointmentController appointmentController = new AppointmentController();

        public VacationRequest(Model.Doctor temp)
        {
            InitializeComponent();
            currentDoctor = temp;
            vacationsGrid.DataContext = currentDoctor.Vacations;
        }

        private void posaljiButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime vacationStartDate = (DateTime)datePicker1.SelectedDate;
            DateTime vacationEndDate = (DateTime)datePicker2.SelectedDate;
            String vacationCause = textBox1.Text;
            if (CheckDate(vacationStartDate, vacationEndDate))
            {
                vacationController.Create(new Vacation(vacationStartDate, vacationEndDate, vacationCause, currentDoctor));
                MessageBox.Show("Uspesno poslat zahtev!");
                this.Close();
            }

        }

        private Boolean CheckDate(DateTime start, DateTime end)
        {
                if ((start.CompareTo(start.AddDays(30)) > 0) || (start.CompareTo(DateTime.Now.AddDays(2)) < 0) || (start.CompareTo(end) >= 0))
                {
                    MessageBox.Show("Datumi nisu dobro uneti");
                    return false;
                }
                else
                {
                    return CheckDoctorAppointments(start, end);
                }
                return true;
        }

        private Boolean CheckDoctorAppointments(DateTime vacationStartDate, DateTime vacationEndDate)
        {
            if(appointmentController.CheckDoctorAppointments(currentDoctor, vacationStartDate, vacationEndDate))
            {
                return CheckSpecialistInVacation(vacationStartDate, vacationEndDate);
            }
            else
            {
                MessageBox.Show("Doktor ima zakazane preglede u ovom vremenskom periodu!");
                return false;
            }
        }

        private Boolean CheckSpecialistInVacation(DateTime vacationStartDate,DateTime vacationEndDate)
        {
            if (vacationController.CheckSpecialistInVacation(currentDoctor,vacationStartDate,vacationEndDate))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Postoji vec doktor sa istom specijalizacijom koji je na odmoru");
                return false;
            }
        }

        private void odustaniButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void appointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            Appointments appointmentWindow = new Appointments(currentDoctor);
            this.Close();
            appointmentWindow.Show();
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

        private void kartoniButton_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecords meds = new MedicalRecords(currentDoctor);
            this.Close();
            meds.Show();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserWindow user = new UserWindow(currentDoctor);
            user.ShowDialog();
        }
    }
}
