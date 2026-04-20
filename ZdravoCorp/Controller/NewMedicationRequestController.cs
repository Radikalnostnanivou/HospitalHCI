using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Repository;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class NewMedicationRequestController : ICrud<NewMedicationRequest>
    {
        public INewMedicationRequestService service = NewMedicationRequestService.Instance;
        public void Create(NewMedicationRequest newMedicationRequest)
        {
            if(newMedicationRequest == null || newMedicationRequest.Name == null)
            {
                throw new Exception("Incorect request");
            }
            service.Create(newMedicationRequest);
        }

        public NewMedicationRequest Read(int id)
        {
            return service.Read(id);
        }

        public void Update(NewMedicationRequest newMedicationRequest)
        {
            service.Update(newMedicationRequest);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }

        public List<NewMedicationRequest> GetAll()
        {
            return service.GetAll();
        }

        public Boolean AcceptNewMedicationRequest(NewMedicationRequest newMedicationRequest)
        {
            return service.AcceptNewMedicationRequest(newMedicationRequest);
        }

        public Boolean RejectNewMedicationRequest(NewMedicationRequest newMedicationRequest, String comment)
        {
            return service.RejectNewMedicationRequest(newMedicationRequest, comment);
        }
    }
}
