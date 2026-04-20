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
    /// Interaction logic for ChangeRecord.xaml
    /// </summary>
    public partial class ChangeRecord : Window, INotifyPropertyChanged
    {
        public ObservableCollection<MedicalRecord> RecordCollection { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private MedicalRecordController mrcontroller;
        private MedicalRecord medicalRecord;

        private int id;
        private Model.Patient patient;
        private BloodType bloodType;
        private float height;
        private float weight;
        private List<String> allergens = new List<String>();

        private System.Collections.Generic.List<Report> comments;
        private String description;
        public ObservableCollection<String> AllergenCollection;
        MedicalRecord record = new MedicalRecord();

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ChangeRecord(MedicalRecord record)
        {
            InitializeComponent();
            this.DataContext = this;
            this.record = record;
            DataContext = this;
            this.height= record.Height;
            this.weight = record.Weight;
            BloodTypes.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
            this.bloodType = record.BloodType;
            this.allergens = record.Allergens;
            AllergenCollection = new ObservableCollection<String>(allergens);
            AllergensTable.DataContext = AllergenCollection;


            
            mrcontroller = new MedicalRecordController();
        }

        public float Heightt
        {
            get { return height; }
            set
            {
                if (value != height)
                {
                    height = value;
                    OnPropertyChanged("Heightt");
                }
            }
        }
        public float Weight
        {
            get { return weight; }
            set
            {
                if (value != weight)
                {
                    weight = value;
                    OnPropertyChanged("Weight");
                }
            }
        }
        public String Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        public BloodType BloodType
        {
            get { return bloodType; }
            set
            {
                if (value != bloodType)
                {
                    bloodType = value;
                    OnPropertyChanged("BloodType");
                }
            }
        }

        public String btname
        {
            get { return btname; }

        }

        public List<String> AllergenList
        {
            get { return allergens; }
            set
            {
                if (value != allergens)
                {
                    allergens = value;
                    OnPropertyChanged("AllergenList");
                }
            }
        }

        public List<Report> CommentList
        {
            get { return comments; }
            set
            {
                if (value != comments)
                {
                    comments = value;
                    OnPropertyChanged("CommentList");
                }
            }
        }

        private void AllergenAdd_Click(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            mrcontroller = new MedicalRecordController();
            List<String> allergens = record.Allergens;
            AllergenList = allergens;
            description = Description;
            allergens.Add(description);
            
            //mrcontroller.UpdateMedicalRecord(record);
            UpdateAllergenTable(allergens);
            
        }

        private void UpdateAllergenTable(List<String> allergens)
        {
            AllergenCollection = new ObservableCollection<String>(allergens);
            AllergensTable.DataContext = AllergenCollection;
        }

        private void AllergenDelete_Click(object sender, RoutedEventArgs e)
        {
            if (AllergensTable.SelectedIndex == -1)
            {
                return;
            }
            allergens.RemoveAt(AllergensTable.SelectedIndex);
            UpdateAllergenTable(allergens);
        }

        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            PatientController patientController = new PatientController();
            //Model.Patient p = patientController.ReadPatient(record.Patient.Id);
            //patientController.UpdatePatient(p);
            MedicalRecord rec = new MedicalRecord(height, weight, BloodType, allergens, null, record.Patient);
            rec.Id = record.Id;
            mrcontroller.Update(rec);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
