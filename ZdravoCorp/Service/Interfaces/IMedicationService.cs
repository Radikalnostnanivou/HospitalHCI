using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Service.Interfaces
{
    public interface IMedicationService : ICrud<Medication>
    {
        void CreateMedicationType(MedicationType newMedicationType);

        void UpdateMedicationType(MedicationType medicationType);

        void DeleteMedicationType(int id);

        MedicationType ReadMedicationType(int id);

        List<MedicationType> GetAllMedicationType();
    }
}
