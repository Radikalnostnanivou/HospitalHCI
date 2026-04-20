using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Controller;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class VacationRepository : Repository<Vacation> , IVacationRepository
    {
        private static VacationRepository instance = null;

        public VacationRepository()
        {
            dataBase.SetPath("..\\..\\Data\\vacationsDB.csv");
            InstantiateIDSet(GetAll());
        }

        public bool AcceptVacation(Doctor doctor, Vacation vacation)
        {
            vacation.Status = Status.ACCEPTED;
            vacation.Comment = "/";
            Update(vacation);
            return true;
        }

        public bool RejectVacation(Doctor doctor, Vacation vacation, String comment)
        {
            vacation.Status = Status.REJECTED;
            vacation.Comment = comment;
            Update(vacation);
            return true;
        }

        public override Vacation Read(int id)
        {
            lock (key)
            {
                lock (key)
                {
                    CheckIfIDExists(id);
                    return FindVacationByID(GetAll(), id);
                }
            }
        }

        public override void Create(Vacation element)
        {
            lock (key)
            {
                element.Id = GenerateID();
                dataBase.AppendToDB(element);
                idMap.Add(element.Id);
            }
        }

        public override void Update(Vacation element)
        {
            lock (key)
            {
                CheckIfIDExists(element.Id);
                List<Vacation> elements = GetAll();
                SwapVacationByID(elements, element);
                dataBase.SaveChanges(elements);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                List<Vacation> elements = GetAll();
                RemoveVacationByID(elements, id);
                dataBase.SaveChanges(elements);
            }
        }

        protected override void InstantiateIDSet(List<Vacation> elements)
        {
            lock (key)
            {
                foreach (Vacation element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void SwapVacationByID(List<Vacation> elements, Vacation element)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == element.Id)
                {
                    elements[i] = element;
                    return;
                }
            }
            throw new Exception("Vacation doesnt exist");
        }

        private Vacation FindVacationByID(List<Vacation> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    return elements[i];
                }
            }
            throw new Exception("Vacation doesnt exist");
        }

        private void RemoveVacationByID(List<Vacation> elements, int id)
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
            throw new Exception("Vacation doesnt exist");
        }

        private void CheckIfIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new Exception("Vacation doesnt exist");
        }

        public static VacationRepository Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new VacationRepository();
                }
                return instance;
            }
        }
    }
}
