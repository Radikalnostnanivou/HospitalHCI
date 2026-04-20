using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
namespace ZdravoCorp.View.Patient.Modell.MedicalHistory
{
    public class ChronicDesease : INotifyPropertyChanged
    {
        private String desease;
        public String Desease
        {
            get => desease;
            set
            {
                if (value != desease)
                {
                    desease = value;
                    OnPropertyChanged("Desease");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
