// File:    PatientService.cs
// Author:  Dusko
// Created: Sunday, 10 April 2022 22:41:52
// Purpose: Definition of Class PatientService

using Model;
using System;
using Repository;
using System.Collections.Generic;
using ZdravoCorp.Service.Interfaces;
using ZdravoCorp.Repository.Interfaces;

namespace Service
{
    public class PatientService : ICrud<Patient> , IPatientService
    {
        
        private static PatientService instance = null;

        private IPatientRepository repository = PatientRepository.Instance;
        public void Create(Patient newPatient)
        {
            repository.Create(newPatient);
        }

        public void Update(Patient patient)
        {
            repository.Update(patient);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public Patient Read(int id)
        {
            return repository.Read(id);
        }

        public Patient ReadPatientByJmbg(string jmbg)
        {
            return repository.FindPatientByJMBG(jmbg);
        }

        public List<Patient> GetAll()
        {
            return repository.GetAll();
        }

        public Boolean AddPrescription(Patient patient, Prescription newPrescription)
        {
            return repository.AddPrescription(patient, newPrescription);
        }

        public PatientService()
        {
            
        }

        public static PatientService Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new PatientService();
                }
                return instance ;
            }
        }
        public void RemoveFromChangedOrCanceledList(Patient patient)
        {
            if(patient.ChangedOrCanceledAppointmentsDates == null)
            {
                patient.ChangedOrCanceledAppointmentsDates = new List<DateTime>();
            }
            foreach (DateTime date in patient.ChangedOrCanceledAppointmentsDates)
            {
                if (date < DateTime.Now.AddMonths(-1))
                {
                    patient.ChangedOrCanceledAppointmentsDates.Remove(date);
                    Update(patient);
                }
            }
        }


    }
}