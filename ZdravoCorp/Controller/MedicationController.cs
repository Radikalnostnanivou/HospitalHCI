// File:    MedicineController.cs
// Author:  halid
// Created: Friday, 15 April 2022 20:28:45
// Purpose: Definition of Class MedicineController

using Model;
using System;
using System.Collections.Generic;
using Service;
using Repository;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class MedicationController : ICrud<Medication>
    {
        private IMedicationService service = MedicationService.Instance;
        public void Create(Medication newMedicine)
        {
            service.Create(newMedicine);
        }

        public void Update(Medication medicine)
        {
            service.Update(medicine);
        }

        public void Delete(int identificator)
        {
            service.Delete(identificator);
        }

        public Model.Medication Read(int identificator)
        {
            return service.Read(identificator);
        }

        public List<Medication> GetAll()
        {
            return service.GetAll();
        }

        public void CreateMedicationType(MedicationType newMedicationType)
        {
            service.CreateMedicationType(newMedicationType);
        }

        public void UpdateMedicationType(MedicationType medicationType)
        {
            service.UpdateMedicationType(medicationType);
        }

        public void DeleteMedicationType(int id)
        {
            service.DeleteMedicationType(id);
        }

        public MedicationType ReadMedicationType(int id)
        {
            return service.ReadMedicationType(id);
        }

        public List<MedicationType> GetAllMedicationType()
        {
            return service.GetAllMedicationType();
        }

    }
}