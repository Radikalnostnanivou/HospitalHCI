using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Service.Interfaces
{
    public interface IPrescriptionService : ICrud<Prescription>
    {
        int CreateAndReturnID(Prescription newPrescription, Patient currentPatient);
    }
}
