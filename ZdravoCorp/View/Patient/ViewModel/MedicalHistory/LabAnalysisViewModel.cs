using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.ComponentModel;
using ZdravoCorp.View.Core;
namespace ZdravoCorp.View.Patient.ViewModel.MedicalHistory
{
    public class LabAnalysisViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Modell.MedicalHistory.LabAnalysis> LabAnalysis { get; set; }
        public LabAnalysisViewModel()
        {
            LoadLabAnalysis();
            LabResultsCommand = new RelayCommand(o =>
            {
                try
                {
                    View.MedicalHistory.LabResults myWindow = new View.MedicalHistory.LabResults(SelectedTable);
                    myWindow.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        public void LoadLabAnalysis()
        {
            ObservableCollection<Modell.MedicalHistory.LabAnalysis> collection 
                = new ObservableCollection<Modell.MedicalHistory.LabAnalysis>();
            collection.Add(new Modell.MedicalHistory.LabAnalysis { AnalysisType = "Analiza krvi", Date = "1/1/2022", NumberOfErythrocytes="100/100", NumberOfLeukocytes="90/100", Ok="Da", Name = "Jovan", Surname="Jovanovic"});
            collection.Add(new Modell.MedicalHistory.LabAnalysis { AnalysisType = "Analiza urina", Date = "28/3/2022", NumberOfErythrocytes = "41/100", NumberOfLeukocytes = "23/100", Ok = "Ne", Name = "Jovan", Surname = "Jovanovic" });
            LabAnalysis = collection;
        }
        public RelayCommand LabResultsCommand { get; set; }
        private Modell.MedicalHistory.LabAnalysis selectedTable;
        public Modell.MedicalHistory.LabAnalysis SelectedTable
        {
            get => selectedTable;
            set
            {
                if (value != selectedTable)
                {
                    selectedTable = value;
                    OnPropertyChanged("SelectedTable");
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
