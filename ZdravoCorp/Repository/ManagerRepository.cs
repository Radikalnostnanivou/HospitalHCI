// File:    ManagerRepository.cs
// Author:  halid
// Created: Saturday, 16 April 2022 01:42:18
// Purpose: Definition of Class ManagerRepository

using Model;
using System;
using System.Collections.Generic;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class ManagerRepository : UserRepository<Manager> , IManagerRepository
    {
        private static ManagerRepository instance = null;

        public ManagerRepository()
        {
            dataBase.SetPath("..\\..\\Data\\managersDB.csv");
            InstantiateHashSets(GetAll());
        }

        public Dictionary<string, Manager> GetUsernameHashSet()
        {
            return Users;
        }

        public override Manager Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                List<Manager> managers = GetAll();
                return FindManagerByID(managers, id);
            }
        }

        public override void Create(Manager element)
        {
            lock (key)
            {
                CheckIfUsernameExists(element.Username);
                element.Id = GenerateID();
                dataBase.AppendToDB(element);
                Users.Add(element.Username, element);
                idMap.Add(element.Id);
            }
        }

        public override void Update(Manager element)
        {
            lock (key)
            {
                CheckIfIDExists(element.Id);
                CheckIfUsernameExists(element.Username);
                List<Manager> elements = GetAll();
                SwapManagerByID(elements, element);
                dataBase.SaveChanges(elements);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                List<Manager> managers = GetAll();
                DeleteManagerByID(managers, id);
                dataBase.SaveChanges(managers);
            }
        }

        protected override void InstantiateIDSet(List<Manager> elements)
        {
            lock (key)
            {
                foreach (Manager element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void InstantiateHashSets(List<Manager> elements)
        {
            InstantiateIDSet(elements);
            InstantiateUserSet(elements);
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

        private Manager FindManagerByID(List<Manager> elements, int id)
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

        private void SwapManagerByID(List<Manager> elements, Manager element)
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

        private void DeleteManagerByID(List<Manager> elements, int id)
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

        public static ManagerRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new ManagerRepository();
                        }
                    }
                }
                return instance;
            }
        }
    }
}