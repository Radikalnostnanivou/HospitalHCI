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
    /// Interaction logic for AddPatient.xaml
    /// </summary>
    public partial class AddPatient : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Model.Patient> PatientCollection { get; set; }
        private int id;
        private String password;
        private String username;
        private String name;
        private String surname;
        private String jmbg;
        private String email;
        private String address;
        private String phoneNumber;
        private Gender gender;
        private DateTime dateOfBirth;
        private Model.Patient patient;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public AddPatient()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public String Username
        {
            get { return username; }
            set
            {
                if (value != username)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public String Password
        {
            get { return password; }
            set
            {
                if (value != password)
                {
                    password = value;
                    OnPropertyChanged("Password");
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

        public String Email
        {
            get { return email; }
            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        public String Address
        {
            get { return address; }
            set
            {
                if (value != address)
                {
                    address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        public String PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                if (value != phoneNumber)
                {
                    phoneNumber = value;
                    OnPropertyChanged("PhoneNumber");
                }
            }
        }

        public Gender Gender
        {
            get { return gender; }
            set
            {
                if (value != gender)
                {
                    if (MaleButton.IsChecked == true)
                    {
                        gender = Gender.Male;
                    }
                    else
                    {
                        gender = Gender.Female;
                    }
                    OnPropertyChanged("Gender");
                }

            }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                if (value != dateOfBirth)
                {
                    dateOfBirth = value;
                    OnPropertyChanged("DateOfBirth");
                }
            }
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            PatientController patientController = new PatientController();
            if (MaleButton.IsChecked == true)
            {
                gender = Gender.Male;
            }
            else
            {
                gender = Gender.Female;
            }
            patient = new Model.Patient(0, password, username, name, surname, jmbg, email, address, phoneNumber, gender, dateOfBirth, null, null, new List<DateTime>(),null);
            //if (!patientController.CreatePatient(new Model.Patient(0, password, username, name, surname, jmbg, email, address, phoneNumber, gender, dateOfBirth, null, null, new List<DateTime>())))
            //{
            //    MessageBox.Show("Nije uspesno dodat element", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
            //else
            //{
            this.Close();
        }

        private void CreateMedicalRecord_Click(object sender, RoutedEventArgs e)
        {
            patient = new Model.Patient(0, password, username, name, surname, jmbg, email, address, phoneNumber, gender, dateOfBirth, null, null, new List<DateTime>(),null);
            ZdravoCorp.View.Secretary.AddRecord window = new ZdravoCorp.View.Secretary.AddRecord(patient);
            window.ShowDialog();
            this.Close();
            
            
        }
    }
}
