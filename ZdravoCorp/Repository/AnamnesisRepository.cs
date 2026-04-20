using Model;
using System;
using System.Collections.Generic;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class AnamnesisRepository : Repository<Anamnesis>, IAnamnesisRepository
    {
        private static AnamnesisRepository instance = null;

        public AnamnesisRepository()
        {
            dataBase.SetPath("..\\..\\Data\\anamnesisDB.csv");
            InstantiateIDSet(GetAll());
        }

        public override Anamnesis Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                return FindAnamnesisByID(GetAll(), id);
            }
        }

        public override void Create(Anamnesis element)
        {
            lock (key)
            {
                element.Id = GenerateID();
                dataBase.AppendToDB(element);
                idMap.Add(element.Id);
            }
        }

        public override void Update(Anamnesis element)
        {
            lock (key)
            {
                CheckIfIDExists(element.Id);
                List<Anamnesis> elements = GetAll();
                SwapAnamnesisByID(elements, element);
                dataBase.SaveChanges(elements);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                List<Anamnesis> elements = GetAll();
                DeleteAnamnesisByID(elements, id);
                dataBase.SaveChanges(elements);
            }
        }

        protected override void InstantiateIDSet(List<Anamnesis> elements)
        {
            lock (key)
            {
                foreach (Anamnesis element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void CheckIfIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new LocalisedException("AnamnesisDoesntExist");
        }

        private Anamnesis FindAnamnesisByID(List<Anamnesis> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    return elements[i];
                }
            }
            throw new LocalisedException("AnamnesisDoesntExist");
        }

        private void SwapAnamnesisByID(List<Anamnesis> elements, Anamnesis element)
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

        private void DeleteAnamnesisByID(List<Anamnesis> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    idMap.Remove(id);
                    elements.RemoveAt(i);
                    return;
                }
            }
            throw new LocalisedException("UserDoesntExist");
        }

        public static AnamnesisRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AnamnesisRepository();
                }
                return instance;
            }
        }
    }
}