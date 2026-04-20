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
    /// Interaction logic for AddGuest.xaml
    /// </summary>
    public partial class AddGuest : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Model.Guest> GuestCollection { get; set; }
        public ObservableCollection<BloodType> BloodTypeCollection { get; set; }
        public List<BloodType> bloodTypes = new List<BloodType>();

        private int id;
        private String name;
        private String surname;
        private BloodType bloodType;

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
        public AddGuest()
        { 
            InitializeComponent();
            this.DataContext = this;
            BloodTypeCollection = new ObservableCollection<BloodType>(getBloodTypes());
            BloodTypes.ItemsSource = Enum.GetValues(typeof(BloodType)).Cast<BloodType>();
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            GuestController guestController = new GuestController();
            guestController.Create(new Model.Guest(0, name, surname, (BloodType)BloodTypes.SelectedItem));
            this.Close();
        }
    }
}
