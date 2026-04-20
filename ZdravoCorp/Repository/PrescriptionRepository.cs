using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    internal class PrescriptionRepository : Repository<Prescription> , IPrescriptionRepository
    {
        private static PrescriptionRepository instance = null;

        public PrescriptionRepository()
        {
           dataBase.SetPath("..\\..\\Data\\prescriptionDB.csv");
            InstantiateIDSet(GetAll());
        }

        public override Prescription Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                return FindPrescriptionByID(GetAll(), id);
            }
        }

        public override void Create(Prescription element)
        {
            lock (key)
            {
                element.Id = GenerateID();
                dataBase.AppendToDB(element);
                idMap.Add(element.Id);
            }
        }

        public int CreateAndReturnID(Prescription element)
        {
            lock (key)
            {
                element.Id = GenerateID();
                dataBase.AppendToDB(element);
                idMap.Add(element.Id);
                return element.Id;
            }
        }

        public override void Update(Prescription element)
        {
            lock (key)
            {
                CheckIfIDExists(element.Id);
                List<Prescription> elements = GetAll();
                SwapPrescriptionByID(elements, element);
                dataBase.SaveChanges(elements);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {

                CheckIfIDExists(id);
                List<Prescription> elements = GetAll();
                RemovePrescriptionByID(elements, id);
                dataBase.SaveChanges(elements);
            }
        }

        protected override void InstantiateIDSet(List<Prescription> elements)
        {
            lock (key)
            {
                foreach (Prescription element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void CheckIfIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new Exception("Prescription doesnt exist");
        }

        private void SwapPrescriptionByID(List<Prescription> elements, Prescription element)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == element.Id)
                {
                    elements[i] = element;
                    return;
                }
            }
            throw new Exception("Prescription doesnt exist");
        }

        private Prescription FindPrescriptionByID(List<Prescription> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    return elements[i];
                }
            }
            throw new Exception("Prescription doesnt exist");
        }

        private void RemovePrescriptionByID(List<Prescription> elements, int id)
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
            throw new Exception("Prescription doesnt exist");
        }

        public static PrescriptionRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PrescriptionRepository();
                }
                return instance;
            }
        }

    }
}
