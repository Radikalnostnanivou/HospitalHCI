// File:    AppointmentRepository.cs
// Author:  Dusko
// Created: Sunday, 10 April 2022 22:43:17
// Purpose: Definition of Class AppointmentRepository

using Model;
using System;
using System.Collections.Generic;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class AppointmentRepository : Repository<Appointment> , IAppointmentRepository
    {
        private static AppointmentRepository instance = null;

        public AppointmentRepository()
        {
            dataBase.SetPath("..\\..\\Data\\appointmentsDB.csv");
            InstantiateIDSet(GetAll());
        }

        public List<Appointment> GetAppointmentsById(List<int> id)
        {
            List<Appointment> appointments = GetAll();
            List<Appointment> appById = new List<Appointment>();
            foreach (Appointment appointment in appointments)
            {
                foreach (int i in id)
                {
                    if (appointment.Id == i)
                    {
                        appById.Add(appointment);
                    }
                }
            }
            return appById;
        }

        public override Appointment Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                return FindAppointmentByID(GetAll(), id);
            }
        }

        public override void Create(Appointment element)
        {
            lock (key)
            {
                element.Id = GenerateID();
                dataBase.AppendToDB(element);
                idMap.Add(element.Id);
            }
        }

        public override void Update(Appointment element)
        {
            lock (key)
            {
                CheckIfIDExists(element.Id);
                List<Appointment> elements = GetAll();
                SwapAppointmentByID(elements, element);
                dataBase.SaveChanges(elements);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                List<Appointment> elements = GetAll();
                RemoveAppointmentByID(elements, id);
                dataBase.SaveChanges(elements);
            }
        }

        protected override void InstantiateIDSet(List<Appointment> elements)
        {
            lock (key)
            {
                foreach (Appointment element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void SwapAppointmentByID(List<Appointment> elements, Appointment element)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == element.Id)
                {
                    elements[i] = element;
                    return;
                }
            }
            throw new Exception("Appointment doesnt exist");
        }

        private Appointment FindAppointmentByID(List<Appointment> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    return elements[i];
                }
            }
            throw new Exception("Appointment doesnt exist");
        }

        private void RemoveAppointmentByID(List<Appointment> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    elements.RemoveAt(i);
                    idMap.Remove(id);
                    Doctor d = DoctorRepository.Instance.Read(elements[i].doctor.Id);
                    d.RemoveAppointment(elements[i]);
                    DoctorRepository.Instance.Update(d);
                    Room r = RoomRepository.Instance.Read(elements[i].Room.Identifier);
                    r.RemoveAppointment(elements[i]);
                    RoomRepository.Instance.Update(r);
                    Patient p = PatientRepository.Instance.Read(elements[i].Patient.Id);
                    p.RemoveAppointment(elements[i]);
                    PatientRepository.Instance.Update(p);
                    return;
                }
            }
            throw new Exception("Appointment doesnt exist");
        }

        private void CheckIfIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new Exception("Appointment doesnt exist");
        }

        public static AppointmentRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppointmentRepository();
                }
                return instance;
            }
        }
    }
}