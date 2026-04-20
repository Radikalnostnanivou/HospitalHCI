using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using ZdravoCorp.Service.Interfaces;
using ZdravoCorp.Repository.Interfaces;

namespace Service
{
    public class NewMedicationRequestService : ICrud<NewMedicationRequest> , INewMedicationRequestService
    {
        private static NewMedicationRequestService instance = null;

        private INewMedicationRequestRepository repository = NewMedicationRequestRepository.Instance;

        public void Create(NewMedicationRequest newMedicationRequest)
        {
            repository.Create(newMedicationRequest);
        }

        public NewMedicationRequest Read(int id)
        {
            return repository.Read(id);
        }

        public void Update(NewMedicationRequest newMedicationRequest)
        {
            repository.Update(newMedicationRequest);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public List<NewMedicationRequest> GetAll()
        {
            return repository.GetAll();
        }

        private bool ExecuteUpdate(NewMedicationRequest newMedicationRequest)
        {
            MedicationRepository.Instance.Update(new Medication(0, newMedicationRequest.MedicationType));
            repository.Delete(newMedicationRequest.Id);
            return true;
        }

        private bool ExecuteCreation(NewMedicationRequest newMedicationRequest)
        {
            MedicationRepository.Instance.Create(new Medication(0, newMedicationRequest.MedicationType));
            repository.Delete(newMedicationRequest.Id);
            return true;
        }

        public Boolean AcceptNewMedicationRequest(NewMedicationRequest newMedicationRequest)
        {
            if (newMedicationRequest.Change)
            {
                return ExecuteUpdate(newMedicationRequest);
            }
            else
            {
                return ExecuteCreation(newMedicationRequest);
            }
        }

        public Boolean RejectNewMedicationRequest(NewMedicationRequest newMedicationRequest, String comment)
        {
            return repository.RejectNewMedicationRequest(newMedicationRequest, comment);
        }

        public static NewMedicationRequestService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NewMedicationRequestService();
                }
                return instance;
            }
        }
    }
}
