using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using Controller;
using ZdravoCorp.Service.Interfaces;
using ZdravoCorp.Repository.Interfaces;

namespace Service
{
    public class PrescriptionService : ICrud<Prescription> , IPrescriptionService
    {
        private static PrescriptionService instance = null;
        private IPrescriptionRepository repository = PrescriptionRepository.Instance;

        public void Create(Prescription newPrescription)
        {
            repository.Create(newPrescription);
        }

        public void Update(Prescription prescription)
        {
            repository.Update(prescription);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Prescription Read(int id)
        {
            return repository.Read(id);
        }

        public List<Prescription> GetAll()
        {
            return repository.GetAll();
        }

        public int CreateAndReturnID(Prescription newPrescription, Patient currentPatient)
        {
            int newId = repository.CreateAndReturnID(newPrescription);
            PatientController patientController = new PatientController();
            MedicationController medicationController = new MedicationController();
            PrescriptionController prescriptionController = new PrescriptionController();   
            patientController.AddPrescription(currentPatient,prescriptionController.Read(newId));
            newPrescription.Medication.Count = newPrescription.Medication.Count - (newPrescription.TimesADay * newPrescription.Quantity);
            medicationController.Update(newPrescription.Medication);
            return newId;
        }
        public static PrescriptionService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PrescriptionService();
                }
                return instance;
            }
        }
    }
}
