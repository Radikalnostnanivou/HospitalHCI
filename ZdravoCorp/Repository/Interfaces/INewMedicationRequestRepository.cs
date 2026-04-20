using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Repository.Interfaces
{
    public interface INewMedicationRequestRepository : ICrud<NewMedicationRequest>
    {
        Boolean AcceptNewMedicationRequest(NewMedicationRequest newMedicationRequest);

        Boolean RejectNewMedicationRequest(NewMedicationRequest newMedicationRequest, String comment);
    }
}
