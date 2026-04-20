using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Service.Interfaces
{
    public interface IPatientService : ICrud<Patient>
    {

        Patient ReadPatientByJmbg(string jmbg);

        Boolean AddPrescription(Model.Patient patient, Prescription newPrescription);

        void RemoveFromChangedOrCanceledList(Patient patient);

    }
}
