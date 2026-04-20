// File:    PatientController.cs
// Author:  Dusko
// Created: Sunday, 10 April 2022 22:19:13
// Purpose: Definition of Class PatientController

using Model;
using System;
using Service;
using Repository;
using System.Collections.Generic;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class PatientController : ICrud<Patient>
    {
        public IPatientService service = PatientService.Instance;
        public void Create(Patient newPatient)
        {
            service.Create(newPatient);
        }

        public void Update(Patient patient)
        {
            service.Update(patient);
        }

        public void Delete(int patient)
        {
            service.Delete(patient);
        }

        public Patient Read(int patient)
        {
            return service.Read(patient);
        }

        public Patient ReadPatientByJmbg(string jmbg)
        {
            return service.ReadPatientByJmbg(jmbg);
        }

        public List<Patient> GetAll()
        {
            return service.GetAll();
        }

        public Boolean AddPrescription(Model.Patient patient,Prescription newPrescription)
        {
            return service.AddPrescription(patient, newPrescription);
        }

        public void RemoveFromChangedOrCanceledList(Patient patient)
        {
            service.RemoveFromChangedOrCanceledList(patient);
        }

        public int CheckAllergens(Patient patient, Medication medication)
        {
            foreach(MedicationType medicationType in patient.Allergens)
            {
                if(medicationType.Id == medication.MedicationType.Id)
                {
                    return -1;
                }
                else
                {
                    foreach(MedicationType tempMedicationType in medication.MedicationType.Ingredients)
                    {
                        if (medicationType.Id == tempMedicationType.Id)
                            return -2;
                    }
                }
            }
            return 1;
        }
    }
}