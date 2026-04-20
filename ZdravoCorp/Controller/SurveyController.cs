using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class SurveyController : ICrud<Survey>
    {
        public ISurveyService service = SurveyService.Instance;
        public void Create(Survey element)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Survey> GetAll()
        {
            return service.GetAll();
        }

        public Survey Read(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Survey element)
        {
            throw new NotImplementedException();
        }
    }
}
