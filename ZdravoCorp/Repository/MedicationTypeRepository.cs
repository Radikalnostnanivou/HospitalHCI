using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class MedicationTypeRepository : Repository<MedicationType> , IMedicationTypeRepository
    {
        private static MedicationTypeRepository instance = null;
        private HashSet<string> typeNameMap = new HashSet<string>();
        public MedicationTypeRepository()
        {
            dataBase.SetPath("..\\..\\Data\\medicationTypeDB.csv");
            InstantiateIDSet(GetAll());
        }
        public override void Create(MedicationType element)
        {
            lock (key)
            {
                CheckIfNameExists(element.Name);
                element.Id = GenerateID();
                dataBase.AppendToDB(element);
                typeNameMap.Add(element.Name);
                idMap.Add(element.Id);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                CheckIfTypeIDExists(id);
                List<MedicationType> types = GetAll();
                DeleteTypeByID(types, id);
                dataBase.SaveChanges(types);
            }
        }

        public override MedicationType Read(int id)
        {
            lock (key)
            {
                CheckIfTypeIDExists(id);
                return FindTypeByID(GetAll(), id);
            }
        }

        public override void Update(MedicationType element)
        {
            lock (key)
            {
                CheckIfTypeIDExists(element.Id);
                List<MedicationType> types = GetAll();
                SwapTypesByID(types, element);
                dataBase.SaveChanges(types);
            }
        }

        public new List<MedicationType> GetAll()
        {
            lock (key)
            {
                Dictionary<int, MedicationType> types = base.GetAll()
                    .ToDictionary(keySelector: m => m.Id, elementSelector: m => m);
                InstantiateReplacements(types);
                return types.Values.ToList();
            }
        }

        protected override void InstantiateIDSet(List<MedicationType> elements)
        {
            foreach (MedicationType element in elements)
            {
                idMap.Add(element.Id);
                typeNameMap.Add(element.Name);
            }
        }

        private void CheckIfNameExists(string name)
        {
            if (typeNameMap.Contains(name))
                throw new LocalisedException("MedicationTypeDoesExist");
        }

        private void CheckIfTypeIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new LocalisedException("MedicationTypeDoesntExist");
        }

        private void DeleteTypeByID(List<MedicationType> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    typeNameMap.Remove(elements[i].Name);
                    idMap.Remove(id);
                    elements.RemoveAt(i);
                    return;
                }
            }
            throw new LocalisedException("MedicationTypeDoesntExist");
        }

        private void SwapTypesByID(List<MedicationType> elements, MedicationType element)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == element.Id)
                {
                    elements[i] = element;
                    return;
                }
            }
            throw new LocalisedException("MedicationTypeDoesntExist");
        }

        private void InstantiateReplacements(Dictionary<int, MedicationType> types)
        {
            foreach (MedicationType type in types.Values)
            {
                for (int i = 0; i < type.Replacement.Count; i++)
                {
                    if (types.ContainsKey(type.Replacement[i].Id))
                    {
                        type.Replacement[i] = types[type.Replacement[i].Id];
                    }
                }
            }
        }

        private MedicationType FindTypeByID(List<MedicationType> types, int id)
        {
            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].Id == id)
                {
                    return types[i];
                }
            }
            throw new LocalisedException("MedicationTypeDoesntExist");
        }


        public static MedicationTypeRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new MedicationTypeRepository();
                        }
                    }
                }
                return instance;
            }
        }

    }
}

