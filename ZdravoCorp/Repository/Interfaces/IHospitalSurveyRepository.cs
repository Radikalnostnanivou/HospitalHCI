using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Repository.Interfaces
{
    public interface IHospitalSurveyRepository : ICrud<HospitalSurvey>
    {
        List<int> getAllHospitalSurveyIds();

        List<HospitalSurvey> GetSurveysById(List<int> id);
    }
}
