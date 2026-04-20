using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

namespace ZdravoCorp.View.Patient.ViewModel.MedicalHistory
{
    public class OperationViewModel
    {
        public OperationViewModel()
        {
            LoadOperations();
        }

        public ObservableCollection<Modell.MedicalHistory.Operation> Operations { get; set; }

        public void LoadOperations()
        {
            ObservableCollection<Modell.MedicalHistory.Operation> operations 
                = new ObservableCollection<Modell.MedicalHistory.Operation>();
            operations.Add(new Modell.MedicalHistory.Operation { OperationType = "Vadjenje krajnika", Date = "10/9/2020", Doctor = "dr Petar Petrovic", Duration = "3h" });
            operations.Add(new Modell.MedicalHistory.Operation { OperationType = "Vadjenje zuba - 18", Date = "21/6/2021", Doctor = "dr Marko Markovic", Duration = "1h" });
            operations.Add(new Modell.MedicalHistory.Operation { OperationType = "Vadjenje zuba - 28", Date = "10/9/2020", Doctor = "dr Marko Markovic", Duration = "1h 15min" });
            Operations = operations;
        }

    }
}
