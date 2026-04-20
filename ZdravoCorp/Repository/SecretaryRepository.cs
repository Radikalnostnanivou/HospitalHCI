// File:    SecretaryRepository.cs
// Author:  halid
// Created: Saturday, 16 April 2022 01:44:41
// Purpose: Definition of Class SecretaryRepository

using Model;
using System;
using System.Collections.Generic;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class SecretaryRepository : UserRepository<Secretary> , ISecretaryRepository
    {
        private static SecretaryRepository instance = null;
        public SecretaryRepository()
        {
            dataBase.SetPath("..\\..\\Data\\secretariesDB.csv");
            InstantiateHashSets(GetAll());
        }

        public Dictionary<string, Secretary> GetUsernameHashSet()
        {
            return Users;
        }

        public override Secretary Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                return FindSecretaryByID(GetAll(), id);
            }
        }

        public override void Create(Secretary element)
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

        public override void Update(Secretary element)
        {
            lock (key)
            {
                CheckIfIDExists(element.Id);
                CheckIfUsernameExists(element.Username);
                List<Secretary> secretaries = GetAll();
                SwapSecretaryByID(secretaries, element);
                dataBase.SaveChanges(secretaries);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                List<Secretary> secretaries = GetAll();
                DeleteSecretaryByID(secretaries, id);
                dataBase.SaveChanges(secretaries);
            }
        }

        protected override void InstantiateIDSet(List<Secretary> elements)
        {
            lock (key)
            {
                foreach (Secretary element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void InstantiateHashSets(List<Secretary> elements)
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

        private void SwapSecretaryByID(List<Secretary> elements, Secretary element)
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

        private void DeleteSecretaryByID(List<Secretary> elements, int id)
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

        private Secretary FindSecretaryByID(List<Secretary> elements, int id)
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

        public static SecretaryRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new SecretaryRepository();
                        }
                    }
                }
                return instance;
            }
        }

    }
}