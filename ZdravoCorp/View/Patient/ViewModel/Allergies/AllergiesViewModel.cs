using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ZdravoCorp.View.Core;

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
namespace ZdravoCorp.View.Patient.ViewModel.Allergies
{
    public class AllergiesViewModel : ObservableObject, INotifyPropertyChanged
    {
        private Window w;
        public AllergiesViewModel(Window newWindow)
        {
            LoadAllergies();
            w = newWindow;
            CancelCommand = new RelayCommand(o =>
            {
                try
                {
                    var myWindow = Window.GetWindow(w);
                    myWindow.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
        public RelayCommand CancelCommand { get; set; }
        public ObservableCollection<Modell.Allergies.Allergies> Allergies { get; set; }
        public void LoadAllergies()
        {
            medicine = "Penicilin\nBrufen 600mg\nProbiotik";
            other = "Jaja\nKopriva\nPolen\nPrašina";
            note = "Pojačana kijavica u poslednjih 5 dana.";
            
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }





        private String medicine;
        private String other;
        private String note;
        public String Other
        {
            get => other;
            set
            {
                if (value != other)
                {
                    other = value;
                    OnPropertyChanged("Other");
                }
            }
        }

        public String Medicine
        {
            get => medicine;
            set
            {
                if (value != medicine)
                {
                    medicine = value;
                    OnPropertyChanged("Medicine");
                }
            }
        }

        public String Note
        {
            get => note;
            set
            {
                if (value != note)
                {
                    note = value;
                    OnPropertyChanged("Note");
                }
            }
        }
    }
}
