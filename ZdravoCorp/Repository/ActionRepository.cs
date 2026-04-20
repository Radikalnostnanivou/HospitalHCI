// File:    ActionRepository.cs
// Author:  halid
// Created: Sunday, 24 April 2022 22:59:45
// Purpose: Definition of Class ActionRepository

using System;
using System.Collections.Generic;
using ZdravoCorp.Utility;
using System.Linq;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    //Sequencial Data Base
    public class ActionRepository : Repository<Model.Action> , IActionRepository
    {
        private static ActionRepository instance = null;
        public ActionRepository()
        {
            dataBase.SetPath("..\\..\\Data\\actionsDB.csv");
            InstantiateIDSet(GetAll());
        }

        public override Model.Action Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                List<Model.Action> actions = GetAll();
                return FindActionByID(id, actions);
            }
        }

        public override void Create(Model.Action element)
        {
            lock (key)
            {
                element.Id = GenerateID();
                List<Model.Action> actions = GetAll();
                AddAction(element, actions);
                dataBase.SaveChanges(actions);
                idMap.Add(element.Id);
            }
        }

        public override void Update(Model.Action element)
        {
            lock (key)
            {
                List<Model.Action> actions = GetAll();
                SwapActions(element, actions);
                SortActionsAscendingAndSaveChanges(actions);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                List<Model.Action> actions = GetAll();
                RemoveActionByID(id, actions);
                dataBase.SaveChanges(actions);
                idMap.Remove(id);
            }
        }

        public void SaveActions(List<Model.Action> elements)
        {
            lock (key)
            {
                dataBase.SaveChanges(elements);
            }
        }

        protected override void InstantiateIDSet(List<Model.Action> elements)
        {
            lock (key)
            {
                foreach (Model.Action element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void SwapActions(Model.Action element, List<Model.Action> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == element.Id)
                {
                    elements[i] = element;
                    return;
                }
            }
            throw new LocalisedException("ActionDoesntExist");
        }

        private void CheckIfIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new LocalisedException("ActionDoesntExist");
        }

        /*
         * Add action sorted by excecution date ascending
        */
        private void AddAction(Model.Action newAction, List<Model.Action> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (DateManipulator.checkIfLaterDate(elements[i].ExecutionDate, newAction.ExecutionDate))
                {
                    elements.Insert(i, newAction);
                    return;
                }
            }
            elements.Add(newAction);
        }

        private void SortActionsAscendingAndSaveChanges(List<Model.Action> elements)
        {
            dataBase.SaveChanges(elements.OrderBy(a => a.ExecutionDate).ToList());
        }

        private Model.Action FindActionByID(int id, List<Model.Action> elements)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    return elements[i];
                }
            }
            throw new LocalisedException("ActionDoesntExist");
        }

        private void RemoveActionByID(int id, List<Model.Action> actions)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i].Id == id)
                {
                    actions.RemoveAt(i);
                    return;
                }
            }
            throw new LocalisedException("ActionDoesntExist");
        }

        public static ActionRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new ActionRepository();
                        }
                    }
                }
                return instance;
            }
        }

    }
}