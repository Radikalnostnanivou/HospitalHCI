// File:    AppointmentRepository.cs
// Author:  Dusko
// Created: Sunday, 10 April 2022 22:43:17
// Purpose: Definition of Class AppointmentRepository

using Model;
using System;
using System.Collections.Generic;
using Repository;
using ZdravoCorp.Service.Interfaces;
using ZdravoCorp.Repository.Interfaces;

namespace Service
{
    public class AnamnesisService : ICrud<Anamnesis> , IAnamnesisService
    {
        
        private static AnamnesisService instance = null;
        public IAnamnesisRepository service = AnamnesisRepository.Instance;

        public void Create(Anamnesis newAnamnesis)
        {
            service.Create(newAnamnesis);
        }

        public Anamnesis Read(int id)
        {
            return service.Read(id);
        }

        public void Update(Anamnesis anamnesis)
        {
            service.Update(anamnesis);

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Anamnesis> GetAll()
        {
            return service.GetAll();
        }

        public AnamnesisService()
        {

        }

        public static AnamnesisService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AnamnesisService();
                }
                return instance;
            }
        }
        public Anamnesis FindAnamnesisByAppointmentId(int id)
        {
            Anamnesis anamnesis = null;
            List<Anamnesis> anamneses = GetAll();
            foreach(Anamnesis a in anamneses)
            {
                if(a.Appointment.Id == id)
                {
                    anamnesis = a;
                }
            }
            return anamnesis;
        }
    }
}