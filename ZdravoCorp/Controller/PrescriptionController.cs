using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.Service;
using Repository;
using Model;
using Service;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class PrescriptionController : ICrud<Prescription>
    {
        public IPrescriptionService service = PrescriptionService.Instance;
        public void Create(Model.Prescription newPrescription)
        {
            service.Create(newPrescription);
        }

        public void Update(Model.Prescription prescription)
        {
            service.Update(prescription);
        }

        public void Delete(int identificator)
        {
            service.Delete(identificator);
        }

        public Prescription Read(int identificator)
        {
            return service.Read(identificator);
        }

        public int CreateAndReturnID(Prescription newPrescription, Patient currentPatient)
        {
            if ((newPrescription.DurationDays * newPrescription.TimesADay) > newPrescription.Medication.Count)
            {
                throw new LocalisedException("U magacinu ne postoje tolike kolicine leka!");
            }
            return service.CreateAndReturnID(newPrescription,currentPatient);
        }

        public List<Prescription> GetAll()
        {
            return service.GetAll();
        }
    }
}
