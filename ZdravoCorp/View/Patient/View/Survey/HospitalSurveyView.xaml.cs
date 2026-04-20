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
using System.Collections.ObjectModel;
using Controller;
using Model;
using System.ComponentModel;

namespace ZdravoCorp.View.Patient.View.Survey
{
    /// <summary>
    /// Interaction logic for HospitalSurveyView.xaml
    /// </summary>
    public partial class HospitalSurveyView : Window, INotifyPropertyChanged
    {
        private Model.Patient patient;
        public HospitalSurveyView(Model.Patient logedPatient)
        {
            InitializeComponent();
            this.DataContext = this;
            ObservableCollection<int> grades = new ObservableCollection<int>();
            grades = SetGrades(grades);
            OverallExperienceCB.ItemsSource = grades;
            ProfesionalismCB.ItemsSource = grades;
            ComfortCB.ItemsSource = grades;
            KindnessCB.ItemsSource = grades;
            TidinessCB.ItemsSource = grades;
            HygieneCB.ItemsSource = grades;
            ModernityCB.ItemsSource = grades;
            EquippedCB.ItemsSource = grades;
            patient = logedPatient;
        }

        private int id;
        private int profesionalism;
        private int comfort;
        private int tidiness;
        private int kindness;
        private int hygiene;
        private int modernity;
        private int equipped;
        private int overallExperience;


        public int Profesionalism
        {
            get => profesionalism;
            set
            {
                if (value != profesionalism)
                {
                    profesionalism = value;
                    OnPropertyChanged("Profesionalism");
                }
            }
        }
        public int Kindness
        {
            get => kindness;
            set
            {
                if (value != kindness)
                {
                    kindness = value;
                    OnPropertyChanged("Kindness");
                }
            }
        }
        public int Comfort
        {
            get => comfort;
            set
            {
                if (value != comfort)
                {
                    comfort = value;
                    OnPropertyChanged("Comfort");
                }
            }
        }
        public int Tidiness
        {
            get => tidiness;
            set
            {
                if (value != tidiness)
                {
                    tidiness = value;
                    OnPropertyChanged("Tidiness");
                }
            }
        }
        public int Modernity
        {
            get => modernity;
            set
            {
                if (value != modernity)
                {
                    modernity = value;
                    OnPropertyChanged("Modernity");
                }
            }
        }
        public int Hygiene
        {
            get => hygiene;
            set
            {
                if (value != hygiene)
                {
                    hygiene = value;
                    OnPropertyChanged("Hygiene");
                }
            }
        }

        public int OverallExperience
        {
            get => overallExperience;
            set
            {
                if (value != overallExperience)
                {
                    overallExperience = value;
                    OnPropertyChanged("OverallExperience");
                }
            }
        }
        public int Equipped
        {
            get => equipped;
            set
            {
                if (value != equipped)
                {
                    equipped = value;
                    OnPropertyChanged("Equipped");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public ObservableCollection<int> SetGrades(ObservableCollection<int> grades)
        {
            grades.Add(1);
            grades.Add(2);
            grades.Add(3);
            grades.Add(4);
            grades.Add(5);
            return grades;
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            HospitalSurveyController hospitalSurveyController = new HospitalSurveyController();
            HospitalSurvey hospitalSurvey = new HospitalSurvey(Profesionalism, Comfort, Tidiness, Kindness, Hygiene, Modernity, Equipped, OverallExperience, patient);
            hospitalSurveyController.Create(hospitalSurvey);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
