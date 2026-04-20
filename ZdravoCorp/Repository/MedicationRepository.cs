// File:    MedicineRepository.cs
// Author:  halid
// Created: Friday, 15 April 2022 20:54:21
// Purpose: Definition of Class MedicineRepository

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class MedicationRepository : Repository<Medication> , IMedicationRepository
    {
        private static MedicationRepository instance = null;

        public MedicationRepository()
        {
            dataBase.SetPath("..\\..\\Data\\medicationDB.csv");
            InstantiateIDSet(GetAll());
        }
        public override Medication Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                return FindMedicationByID(GetAll(), id);
            }
        }

        public override void Create(Medication element)
        {
            lock (key)
            {
                element.Id = GenerateID();
                dataBase.AppendToDB(element);
                idMap.Add(element.Id);
            }
        }

        public override void Update(Medication element)
        {
            lock (key)
            {
                CheckIfIDExists(element.Id);
                List<Medication> elements = GetAll();
                SwapMedicationByID(elements, element);
                dataBase.SaveChanges(elements);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                List<Medication> elements = GetAll();
                RemoveMedicationByID(elements, id);
                dataBase.SaveChanges(elements);
            }
        }

        protected override void InstantiateIDSet(List<Medication> elements)
        {
            lock (key)
            {
                foreach (Medication element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void CheckIfIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new Exception("Medication doesnt exist");
        }

        private void SwapMedicationByID(List<Medication> elements, Medication element)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == element.Id)
                {
                    elements[i] = element;
                    return;
                }
            }
            throw new Exception("Medication doesnt exist");
        }

        private Medication FindMedicationByID(List<Medication> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    return elements[i];
                }
            }
            throw new Exception("Medication doesnt exist");
        }

        private void RemoveMedicationByID(List<Medication> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    elements.RemoveAt(i);
                    idMap.Remove(id);
                    return;
                }
            }
            throw new Exception("Medication doesnt exist");
        }

        public static MedicationRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicationRepository();
                }
                return instance;
            }
        }
    }
}