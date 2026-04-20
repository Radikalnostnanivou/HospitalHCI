using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using ZdravoCorp.Repository.Interfaces;
using ZdravoCorp.Service.Interfaces;

namespace Service
{
    public class ReportService : ICrud<Report> , IReportService
    {
        private static ReportService instance = null;

        private IReportRepository repository = ReportRepository.Instance;
        public void Create(Report newReport)
        {
            repository.Create(newReport);
        }

        public void Update(Model.Report newReport)
        {
            repository.Update(newReport);

        }

        public void Delete(int id)
        {
            repository.Delete(id);

        }

        public Report Read(int id)
        {
            return repository.Read(id);

        }

        public List<Report> GetAll()
        {
            return repository.GetAll();

        }

        ReportService() 
        { }

        public static ReportService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReportService();
                }

                return instance;
            }
        }

    }
}
