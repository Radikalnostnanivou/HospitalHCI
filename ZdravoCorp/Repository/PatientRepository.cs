// File:    PatientRepository.cs
// Author:  Dusko
// Created: Sunday, 10 April 2022 22:43:43
// Purpose: Definition of Class PatientRepository

using Model;
using System;
using System.Collections.Generic;
using ZdravoCorp.Exceptions;
using Controller;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class PatientRepository : UserRepository<Patient> , IPatientRepository
    {
        private static PatientRepository instance = null;

        public PatientRepository()
        {
            dataBase.SetPath("..\\..\\Data\\patientsDB.csv");
            InstantiateHashSets(GetAll());
        }

        private void InstantiateHashSets(List<Patient> elements)
        {
            InstantiateIDSet(elements);
            InstantiateUserSet(elements);
        }

        public new List<Patient> GetAll()
        {

            List<Patient> patients = base.GetAll();
            foreach (Patient patient in patients)
            {
                patient.Appointment = AppointmentRepository.Instance
                    .GetAppointmentsById(GetIdsOfAppointments(patient));
                patient.Record = MedicalRecordRepository.Instance
                    .Read(patient.Record.Id);
            }
            return patients;
        }

        public Boolean AddPrescription(Model.Patient element, Prescription newPrescription)
        {
            Boolean success = false;
            List<Patient> patients = GetAll();
            foreach (Patient temp in patients)
            {
                if(temp.Id == element.Id)
                {
                    PrescriptionController prescriptionController = new PrescriptionController();
                    element.AddPrescription(newPrescription);
                    Update(element);
                    success = true;
                    break;
                }
            }
            return success;
        }

        public Dictionary<string, Patient> GetUsernameHashSet()
        {
            return Users;
        }

        public Patient Read(string jmbg)
        {
            lock (key)
            {
                List<Patient> patients = GetAll();
                CheckIfJMBGExists(patients, jmbg);
                return FindPatientByJMBG(jmbg);
            }
        }

        public override Patient Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                return FindPatientByID(GetAll(), id);
            }
        }

        public override void Create(Patient element)
        {
            lock (key)
            {
                List<Patient> elements = GetAll();
                CheckIfUsernameExists(element.Username);
                CheckIfJMBGExists(elements, element.Jmbg);
                element.Id = GenerateID();
                Users.Add(element.Username, element);
                idMap.Add(element.Id);
                dataBase.AppendToDB(element);
            }
        }

        public override void Update(Patient element)
        {
            lock (key)
            {
                CheckIfIDExists(element.Id);
                List<Patient> elements = GetAll();
                SwapPatientByID(elements, element);
                dataBase.SaveChanges(elements);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                List<Patient> elements = GetAll();
                DeletePatientByID(elements, id);
                dataBase.SaveChanges(elements);
            }
        }

        protected override void InstantiateIDSet(List<Patient> elements)
        {
            lock (key)
            {
                foreach (Patient element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void CheckIfJMBGExists(List<Patient> elements, string jmbg)
        {
            foreach (Patient it in elements)
            {
                if (it.Jmbg.Equals(jmbg))
                {
                    throw new LocalisedException("UserExists");
                }
            }
        }

        private void CheckIfIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new LocalisedException("UserDoesntExist");
        }

        private void CheckIfUsernameExists(string username)
        {
            if (Users.ContainsKey(username))
                throw new LocalisedException("UserExists");
        }

        private Patient FindPatientByID(List<Patient> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    return elements[i];
                }
            }
            throw new LocalisedException("UserDoesntExist");
        }

        public Patient FindPatientByJMBG(string jmbg)
        {
            List<Patient> patients = GetAll();
            for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].Jmbg.Equals(jmbg))
                {
                    return patients[i];
                }
            }
            throw new LocalisedException("UserDoesntExist");
        }

        private void DeletePatientByID(List<Patient> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    idMap.Remove(id);
                    Users.Remove(elements[i].Username);
                    elements.RemoveAt(i);
                    return;
                }
            }
            throw new LocalisedException("UserDoesntExist");
        }

        private void SwapPatientByID(List<Patient> elements, Patient element)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == element.Id)
                {
                    elements[i] = element;
                    return;
                }
            }
            throw new LocalisedException("UserDoesntExist");
        }

        private List<int> GetIdsOfAppointments(Patient element)
        {
            List<int> ids = new List<int>();
            foreach (Appointment it in element.Appointment)
            {
                ids.Add(it.Id);
            }
            return ids;
        }

        Dictionary<string, Secretary> IPatientRepository.GetUsernameHashSet()
        {
            throw new NotImplementedException();
        }

        public static PatientRepository Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new PatientRepository();
                }
                return instance ;
            }
        }
        
    }
}