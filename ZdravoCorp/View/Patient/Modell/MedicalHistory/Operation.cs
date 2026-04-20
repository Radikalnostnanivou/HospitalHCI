using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ZdravoCorp.View.Patient.Modell.MedicalHistory
{
    public class Operation : INotifyPropertyChanged
    {
        private String operationType;
        private String doctor;
        private String date;
        private String duration;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public String OperationType
        {
            get => operationType;
            set
            {
                if (value != operationType)
                {
                    operationType = value;
                    OnPropertyChanged("OperationType");
                }
            }
        }
        public String Doctor
        {
            get => doctor;
            set
            {
                if (value != doctor)
                {
                    doctor = value;
                    OnPropertyChanged("Doctor");
                }
            }
        }
        public String Date
        {
            get => date;
            set
            {
                if (value != date)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }
        public String Duration
        {
            get => duration;
            set
            {
                if (value != duration)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }
    }
}
