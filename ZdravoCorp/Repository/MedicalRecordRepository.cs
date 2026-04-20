using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class MedicalRecordRepository : Repository<MedicalRecord> , IMedicalRecordRepository
    {
        private static MedicalRecordRepository instance = null;

        public MedicalRecordRepository()
        {
            dataBase.SetPath("..\\..\\Data\\medicalRecordDB.csv");
            InstantiateIDSet(GetAll());
        }

        public override MedicalRecord Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                return FindMedicalRecordByID(GetAll(), id);
            }
        }

        public override void Create(MedicalRecord element)
        {
            lock (key)
            {
                element.Id = GenerateID();
                dataBase.AppendToDB(element);
                idMap.Add(element.Id);
            }
        }

        public override void Update(MedicalRecord element)
        {
            lock (key)
            {
                CheckIfIDExists(element.Id);
                List<MedicalRecord> elements = GetAll();
                SwapMedicalRecordByID(elements, element);
                dataBase.SaveChanges(elements);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                List<MedicalRecord> elements = GetAll();
                RemoveMedicalRecordByID(elements, id);
                dataBase.SaveChanges(elements);
            }
        }

        protected override void InstantiateIDSet(List<MedicalRecord> elements)
        {
            lock (key)
            {
                foreach (MedicalRecord element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void CheckIfIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new Exception("MedicalRecord doesnt exist");
        }

        private void SwapMedicalRecordByID(List<MedicalRecord> elements, MedicalRecord element)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == element.Id)
                {
                    elements[i] = element;
                    return;
                }
            }
            throw new Exception("MedicalRecord doesnt exist");
        }

        private MedicalRecord FindMedicalRecordByID(List<MedicalRecord> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    return elements[i];
                }
            }
            throw new Exception("MedicalRecord doesnt exist");
        }

        private void RemoveMedicalRecordByID(List<MedicalRecord> elements, int id)
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
            throw new Exception("MedicalRecord doesnt exist");
        }

        public static MedicalRecordRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalRecordRepository();
                }
                return instance;
            }
        }
    }
}
