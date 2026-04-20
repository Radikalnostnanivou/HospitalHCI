// File:    EquipmentRepository.cs
// Author:  halid
// Created: Tuesday, 12 April 2022 12:26:08
// Purpose: Definition of Class EquipmentRepository

using Controller;
using Model;
using System;
using System.Collections.Generic;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class EquipmentTypeRepository : Repository<EquipmentType> , IEquipmentTypeRepository
    {
        private static EquipmentTypeRepository instance = null;
        public EquipmentTypeRepository()
        {
            dataBase.SetPath("..\\..\\Data\\equipmentTypeDB.csv");
            InstantiateIDSet(GetAll());
        }
        public EquipmentType FindEquipmentTypeByName(String name)
        {
            lock (key)
            {
                List<EquipmentType> equipmentTypes = dataBase.GetAll();
                foreach (EquipmentType type in equipmentTypes)
                {
                    if (type.Name == name)
                    {
                        return type;
                    }
                }
                throw new LocalisedException("EquipmentDoesntExists");
            }
        }

        public List<EquipmentType> GetAllEquipmentType()
        {
            lock (key)
            {
                return dataBase.GetAll();
            }
        }

        public override EquipmentType Read(int id)
        {
            lock (key)
            {
                List<EquipmentType> equipmentTypes = GetAll();
                foreach (EquipmentType type in equipmentTypes)
                {
                    if (type.Identifier == id)
                    {
                        return type;
                    }
                }
                throw new LocalisedException("EquipmentDoesntExists");
            }
        }

        public override void Create(EquipmentType newEquipmentType)
        {
            lock (key)
            {
                List<EquipmentType> equipmentTypes = GetAll();
                newEquipmentType.Identifier = GenerateID();
                CheckIfEquipmentNameExists(newEquipmentType.Name, equipmentTypes);
                dataBase.AppendToDB(newEquipmentType);
                idMap.Add(newEquipmentType.Identifier);
            }
        }

        public override void Update(EquipmentType equipmentType)
        {
            lock (key)
            {
                List<EquipmentType> equipmentTypes = GetAll();
                CheckIfNameOfChangedEquipmentTypeExists(equipmentType, equipmentTypes);
                SwapEquipmentByID(equipmentType, equipmentTypes);
                equipmentTypes.Add(equipmentType);
                dataBase.SaveChanges(equipmentTypes);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                List<EquipmentType> equipmentTypes = dataBase.GetAll();
                RemoveEquipmentType(id, equipmentTypes);
                dataBase.SaveChanges(equipmentTypes);
                idMap.Remove(id);
            }
        }

        protected override void InstantiateIDSet(List<EquipmentType> elements)
        {
            lock (key)
            {
                foreach (EquipmentType element in elements)
                {
                    idMap.Add(element.Identifier);
                }
            }
        }

        private void RemoveEquipmentType(int id, List<EquipmentType> types)
        {
            foreach (EquipmentType type in types)
            {
                if (type.Identifier == id)
                {
                    types.Remove(type);
                    return;
                }
            }
            throw new LocalisedException("EquipmentDoesntExists");
        }

        private void CheckIfEquipmentNameExists(string name, List<EquipmentType> types)
        {
            foreach (EquipmentType type in types)
            {
                if (type.Name == name)
                {
                    throw new LocalisedException("EquipmentNameExists");
                }
            }
        }

        private void CheckIfNameOfChangedEquipmentTypeExists(EquipmentType type, List<EquipmentType> types)
        {
            foreach (EquipmentType it in types)
            {
                if (type.Name.Equals(type.Name) && type.Identifier != type.Identifier)
                {
                    throw new LocalisedException("EquipmentNameExists");
                }
            }
        }

        private void SwapEquipmentByID(EquipmentType type, List<EquipmentType> types)
        {
            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].Identifier == type.Identifier)
                {
                    types[i] = type;
                    return;
                }
            }
            throw new LocalisedException("EquipmentDoesntExists");
        }

        public static EquipmentTypeRepository Instance
        {
            get 
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new EquipmentTypeRepository();
                        }
                    }
                }
                return instance;
            }
        }

    }
}