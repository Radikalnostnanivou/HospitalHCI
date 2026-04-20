using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    internal class ReportRepository : Repository<Report> , IReportRepository
    {
        private static ReportRepository instance = null;

        public ReportRepository()
        {
            dataBase.SetPath("..\\..\\Data\\commentsDB.csv");
            InstantiateIDSet(GetAll());
        }

        public override Report Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                return FindReportRecordByID(GetAll(), id);
            }
        }

        public override void Create(Report element)
        {
            lock (key)
            {
                element.Id = GenerateID();
                dataBase.AppendToDB(element);
                idMap.Add(element.Id);
            }
        }

        public override void Update(Report element)
        {
            lock (key)
            {
                CheckIfIDExists(element.Id);
                List<Report> elements = GetAll();
                SwapReportRecordByID(elements, element);
                dataBase.SaveChanges(elements);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                List<Report> elements = GetAll();
                RemoveReportRecordByID(elements, id);
                dataBase.SaveChanges(elements);
            }
        }

        protected override void InstantiateIDSet(List<Report> elements)
        {
            lock (key)
            {
                foreach (Report element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void CheckIfIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new Exception("Report doesnt exist");
        }

        private void SwapReportRecordByID(List<Report> elements, Report element)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == element.Id)
                {
                    elements[i] = element;
                    return;
                }
            }
            throw new Exception("Report doesnt exist");
        }

        private Report FindReportRecordByID(List<Report> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    return elements[i];
                }
            }
            throw new Exception("Report doesnt exist");
        }

        private void RemoveReportRecordByID(List<Report> elements, int id)
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
            throw new Exception("Report doesnt exist");
        }

        public static ReportRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReportRepository();
                }
                return instance;
            }
        }
    }
}
