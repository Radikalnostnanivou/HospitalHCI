using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using Service;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class ReportController : ICrud<Report>
    {
        public IReportService service = ReportService.Instance;
        public void Create(Model.Report newComment)
        {
            service.Create(newComment);
        }

        public void Update(Model.Report newComment)
        {
            service.Update(newComment);
        }

        public void Delete(int identificator)
        {
            service.Delete(identificator);

        }

        public Report Read(int identificator)
        {
            return service.Read(identificator);

        }

        public List<Report> GetAll()
        {
            return service.GetAll();

        }

    }
}
