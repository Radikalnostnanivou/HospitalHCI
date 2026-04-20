using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ZdravoCorp.View.Patient.ViewModel.MedicalHistory
{
    public class ChronicDeseaseViewModel
    {
        public ChronicDeseaseViewModel()
        {
            LoadDeseases();
        }
        public ObservableCollection<Modell.MedicalHistory.ChronicDesease> ChronicDeseases { get; set; }

        public void LoadDeseases() 
        {
            ObservableCollection<Modell.MedicalHistory.ChronicDesease> chronicDeseases 
                = new ObservableCollection<Modell.MedicalHistory.ChronicDesease>();
            chronicDeseases.Add(new Modell.MedicalHistory.ChronicDesease { Desease = "Dijabetes" });
            chronicDeseases.Add(new Modell.MedicalHistory.ChronicDesease { Desease = "Neneaspavanost" });
            ChronicDeseases = chronicDeseases;
        }
    }
}
