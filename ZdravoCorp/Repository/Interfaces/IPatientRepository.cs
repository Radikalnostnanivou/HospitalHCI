using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Repository.Interfaces
{
    public interface IPatientRepository : ICrud<Patient>
    {
        Dictionary<string, Secretary> GetUsernameHashSet();

        Boolean AddPrescription(Model.Patient element, Prescription newPrescription);

        Patient Read(string jmbg);

        Patient FindPatientByJMBG(string jmbg);
    }
}
