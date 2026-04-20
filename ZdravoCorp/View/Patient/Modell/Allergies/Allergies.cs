using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ZdravoCorp.View.Core;

namespace ZdravoCorp.View.Patient.Modell.Allergies
{
    public class Allergies : INotifyPropertyChanged
    {
        private String other;
        private String medicine;
        private String note;
        //public Allergies(List<String> other, List<String> medicine, string note)
        //{
        //    this.other = other;
        //    this.medicine = medicine;
        //    this.note = note;
        //}
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
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
