using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace ZdravoCorp.View.Patient.Modell.MedicalHistory
{
    public class LabAnalysis : INotifyPropertyChanged
    {
        private String analysisType;
        private String date;
        private String name;
        private String surname;
        private String numberOfErythrocytes;
        private String numberOfLeukocytes;
        private String ok;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public String AnalysisType
        {
            get => analysisType;
            set
            {
                if (value != analysisType)
                {
                    analysisType = value;
                    OnPropertyChanged("AnalysisType");
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
                    OnPropertyChanged("Other");
                }
            }
        }
       
        public String NumberOfErythrocytes
        {
            get => numberOfErythrocytes;
            set
            {
                if (value != numberOfErythrocytes)
                {
                    numberOfErythrocytes = value;
                    OnPropertyChanged("NumberOfErythrocytes");
                }
            }
        }
        public String NumberOfLeukocytes
        {
            get => numberOfLeukocytes;
            set
            {
                if (value != numberOfLeukocytes)
                {
                    numberOfLeukocytes = value;
                    OnPropertyChanged("NumberOfLeukocytes");
                }
            }
        }
        public String Ok
        {
            get => ok;
            set
            {
                if (value != ok)
                {
                    ok = value;
                    OnPropertyChanged("Ok");
                }
            }
        }
        public String Name
        {
            get => name;
            set
            {
                if (value !=name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public String Surname
        {
            get => surname;
            set
            {
                if (value != surname)
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }
    }
}
