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
using Controller;

namespace ZdravoCorp.View.Doctor
{
    /// <summary>
    /// Interaction logic for MedicationRequests.xaml
    /// </summary>
    public partial class MedicationRequests : Window
    {

        NewMedicationRequestController newMedicationRequestController = new NewMedicationRequestController();
        Model.Doctor currentDoctor;

        private MedicationController medicationController;

        public ObservableCollection<Model.Medication> MedicineCollection
        {
            get;
            set;
        }

        public ObservableCollection<Model.NewMedicationRequest> requests
        {
            get;
            set;
        }

        public MedicationRequests(Model.Doctor temp)
        {
            InitializeComponent();
            this.DataContext = this;
            currentDoctor = temp;
            UpdateDataGrid();

            medicationController = new MedicationController();
            MedicineCollection = new ObservableCollection<Model.Medication>(medicationController.GetAll());

            MedicineGrid1.DataContext = MedicineCollection;
            this.currentDoctor = temp;
        }

        private void textBlock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(textBlock.Text == "")
            {
                rejectButton.IsEnabled = false;
            }
            else
            {
                rejectButton.IsEnabled = true;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Appointments appointments = new Appointments(currentDoctor);
            this.Close();
            appointments.Show();
        }

        private void rejectButton_Click(object sender, RoutedEventArgs e)
        {
            String comment = textBlock.Text;
            Model.NewMedicationRequest newMedicationRequest = (Model.NewMedicationRequest)MedicineGrid.SelectedItem;
            newMedicationRequestController.RejectNewMedicationRequest(newMedicationRequest, comment);
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            Model.NewMedicationRequest newMedicationRequest = (Model.NewMedicationRequest)MedicineGrid.SelectedItem;
            if(newMedicationRequest.Status == Model.Status.REJECTED)
            {
                MessageBox.Show("This Medication is REJECTED!");
                return;
            }
            newMedicationRequestController.AcceptNewMedicationRequest(newMedicationRequest);
            UpdateDataGrid();
        }

        private void appointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            Appointments appointments = new Appointments(currentDoctor);
            this.Close();
            appointments.Show();
        }

        private void medsButton_Click_1(object sender, RoutedEventArgs e)
        {
            MedicalReport medicalReport= new MedicalReport(currentDoctor);
            this.Close();
            medicalReport.Show();
        }

        private void vacationRequestButton_Click_1(object sender, RoutedEventArgs e)
        {
            VacationRequest vacationRequest = new VacationRequest(currentDoctor);
            this.Close();
            vacationRequest.Show();
        }

        private void kartoniButton_Click_1(object sender, RoutedEventArgs e)
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

        public void UpdateDataGrid()
        {
            requests = new ObservableCollection<Model.NewMedicationRequest>();
            List<Model.NewMedicationRequest> listNewMedicationRequests = newMedicationRequestController.GetAll();
            foreach (Model.NewMedicationRequest request in listNewMedicationRequests)
            {
                requests.Add(request);
            }
            MedicineGrid.DataContext = requests;
            rejectButton.IsEnabled = false;
        }
    }
}
