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
    /// Interaction logic for ChangePatient.xaml
    /// </summary>
    public partial class ChangePatient : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Model.Patient> PatientCollection { get; set; }
        private Model.Patient patient;
        private PatientController patientController;
   

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ChangePatient(Model.Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            this.patient = patient;
            if (patient.Gender == Gender.Male)
            {
                MaleButton.IsChecked = true;

            }
            else
            {
                FemaleButton.IsChecked = true;
            }
            
            patientController = new PatientController();
        }

        public String Username
        {
            get { return patient.Username; }
            set
            {
                if (value != patient.Username)
                {
                    patient.Username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public String Password
        {
            get { return patient.Password; }
            set
            {
                if (value != patient.Password)
                {
                    patient.Password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public String Namee
        {
            get { return patient.Namee; }
            set
            {
                if (value != patient.Namee)
                {
                    patient.Namee = value;
                    OnPropertyChanged("Namee");
                }
            }
        }

        public String Surname
        {
            get { return patient.Surname; }
            set
            {
                if (value != patient.Surname)
                {
                    patient.Surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        public String Jmbg
        {
            get { return patient.Jmbg; }
            set
            {
                if (value != patient.Jmbg)
                {
                    patient.Jmbg = value;
                    OnPropertyChanged("Jmbg");
                }
            }
        }

        public String Email
        {
            get { return patient.Email; }
            set
            {
                if (value != patient.Email)
                {
                    patient.Email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        public String Address
        {
            get { return patient.Address; }
            set
            {
                if (value != patient.Address)
                {
                    patient.Address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        public String PhoneNumber
        {
            get { return patient.PhoneNumber; }
            set
            {
                if (value != patient.PhoneNumber)
                {
                    patient.PhoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
                }
            }
        }

        public Gender Gender
        {
            get { return patient.Gender; }
            set
            {
                if (value != patient.Gender)
                {
                    if (MaleButton.IsChecked == true)
                    {
                        patient.Gender = Gender.Male;
                    }
                    else
                    {
                        patient.Gender = Gender.Female;
                    }
                    OnPropertyChanged("Gender");
                }

            }
        }

        public DateTime DateOfBirth
        {
            get { return patient.DateOfBirth; }
            set
            {
                if (value != patient.DateOfBirth)
                {
                    patient.DateOfBirth = value;
                    OnPropertyChanged("DateOfBirth");
                }
            }
        }


        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (MaleButton.IsChecked == true)
            {
                patient.Gender = Gender.Male;
            }
            else
            {
                patient.Gender = Gender.Female;
            }
            
            //if (!patientController.UpdatePatient(patient))
            //{
            //    MessageBox.Show("Nije uspesno izmenjen element", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //else
            //{
                this.Close();
            //}
        }

        private void MedicalRecordChange_Click(object sender, RoutedEventArgs e)
        {
            
            ZdravoCorp.View.Secretary.ChangeRecord window = new ChangeRecord(patient.Record);
            window.ShowDialog();
            
            this.Close();
            patientController.Update(patient);
        }

        private void CancelChange_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
