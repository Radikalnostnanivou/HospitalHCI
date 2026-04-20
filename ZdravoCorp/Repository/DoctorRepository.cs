// File:    DoctorRepository.cs
// Author:  10
// Created: Monday, 11 April 2022 21:52:49
// Purpose: Definition of Class DoctorRepository

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class DoctorRepository : UserRepository<Doctor> , IDoctorRepository
    {
        private static DoctorRepository instance = null;

        public DoctorRepository()
        {
            dataBase.SetPath("..\\..\\Data\\doctorsDB.csv");
            InstantiateHashSets(GetAll());
        }

        public new List<Doctor> GetAll()
        {
            lock (key)
            {
                List<Doctor> doctors = base.GetAll();
                foreach (Doctor it in doctors)
                {
                    it.Appointment = AppointmentRepository.Instance
                        .GetAppointmentsById(GetIdsOfAppointments(it));
                }
                return doctors;
            }
        }

        public Dictionary<string, Doctor> GetUsernameHashSet()
        {
            return Users;
        }

        public override Doctor Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                return FindDoctorByID(GetAll(), id);
            }
        }

        public override void Create(Doctor element)
        {
            lock (key)
            {
                List<Doctor> elements = GetAll();
                CheckIfDoctorExists(elements, element);
                CheckIfUsernameExists(element.Username);
                element.Id = GenerateID();
                Users.Add(element.Username, element);
                idMap.Add(element.Id);
                dataBase.AppendToDB(element);
            }
        }

        public override void Update(Doctor element)
        {
            lock (key)
            {
                CheckIfIDExists(element.Id);
                List<Doctor> elements = GetAll();
                SwapDoctorByID(elements, element);
                dataBase.SaveChanges(elements);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                List<Doctor> elements = GetAll();
                DeleteDoctorByID(elements, id);
                dataBase.SaveChanges(elements);
            }
        }

        protected override void InstantiateIDSet(List<Doctor> elements)
        {
            lock (key)
            {
                foreach(Doctor element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void InstantiateHashSets(List<Doctor> elements)
        {
            InstantiateIDSet(elements);
            InstantiateUserSet(elements);
        }

        private void CheckIfDoctorExists(List<Doctor> elements, Doctor element)
        {
            foreach (Doctor it in elements)
            {
                if (it.Jmbg.Equals(element.Jmbg))
                {
                    throw new LocalisedException("UserExists");
                }
            }
        }

        private void CheckIfUsernameExists(string username)
        {
            if (Users.ContainsKey(username))
                throw new LocalisedException("UserExists");
        }

        private void CheckIfIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new LocalisedException("UserDoesntExist");
        }

        private Doctor FindDoctorByID(List<Doctor> elements, int id)
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

        private void DeleteDoctorByID(List<Doctor> elements, int id)
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

        private void SwapDoctorByID(List<Doctor> elements, Doctor element)
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

        private List<int> GetIdsOfAppointments(Doctor element)
        {
            List<int> ids = new List<int>();
            foreach (Appointment it in element.Appointment)
            {
                ids.Add(it.Id);
            }
            return ids;
        }

        public static DoctorRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new DoctorRepository();
                        }
                    }
                }
                return instance;
            }
        }

    }
}