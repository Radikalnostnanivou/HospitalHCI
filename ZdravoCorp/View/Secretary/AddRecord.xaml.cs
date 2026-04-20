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
    /// Interaction logic for AddRecord.xaml
    /// </summary>
    public partial class AddRecord : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Model.MedicalRecord> RecordCollection { get; set; }
        public ObservableCollection<String> AllergenCollection { get; set; }
        public ObservableCollection<BloodType> BloodTypeCollection { get; set; }
        public List<BloodType> bloodTypes = new List<BloodType>();
        public MedicalRecordController mrcontroller;
        public MedicalRecord record = new MedicalRecord();
        
        private int id;
        private Model.Patient patient;
        private BloodType bloodType;
        private float height;
        private float weight;
        private List<String> allergens = new List<String>();

        private System.Collections.Generic.List<Report> comments;
        private String description;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<BloodType> getBloodTypes()
        {
            bloodTypes.Add(Model.BloodType.aPlus);
            bloodTypes.Add(Model.BloodType.aMinus);
            bloodTypes.Add(Model.BloodType.bPlus);
            bloodTypes.Add(Model.BloodType.bMinus);
            bloodTypes.Add(Model.BloodType.aBPlus);
            bloodTypes.Add(Model.BloodType.aBMinus);
            bloodTypes.Add(Model.BloodType.oPlus);
            bloodTypes.Add(Model.BloodType.oMinus);
            return bloodTypes;
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public AddRecord(Model.Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            BloodTypeCollection = new ObservableCollection<BloodType>(getBloodTypes());
            BloodTypes.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
            mrcontroller = new MedicalRecordController();
            record.Patient = patient;
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

        private void AddAllergen_Click(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            mrcontroller = new MedicalRecordController();
            List<String> allergens = new List<String>();
            allergens = AllergenList;
            description = Description;
            allergens.Add(description);
            record.Allergens = allergens;
            //mrcontroller.UpdateMedicalRecord(record);
            AllergenCollection = new ObservableCollection<String>(allergens);
            AllergensTable.DataContext = AllergenCollection;
        }


        private void AddComment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateAllergenTable()
        {
            List<String> allergens = mrcontroller.Read(0).Allergens;
           
            
            //foreach (Model.Patient pat in patients)
            //{
            //    AllergenCollection.Add(pat);
            //}
            AllergensTable.DataContext = AllergenCollection;
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            PatientController patientController = new PatientController();
            patientController.Create(record.Patient);
            MedicalRecord mr = new MedicalRecord(height, weight, BloodType, allergens, null, record.Patient);
            mrcontroller = new MedicalRecordController();
            mrcontroller.Create(mr);
            record.Patient.Record = mr;
            PatientController pc = new PatientController();
            pc.Update(record.Patient);
            this.Close();
        }

        private void MedicalRecordCancel_Click(object sender, RoutedEventArgs e)
        {
            PatientController patientController = new PatientController();
            patientController.Delete(record.Patient.Id);
            this.Close();
        }
    }
}
