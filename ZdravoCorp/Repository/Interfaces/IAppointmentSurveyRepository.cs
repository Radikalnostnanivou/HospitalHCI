using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Repository.Interfaces
{
    public interface IAppointmentSurveyRepository : ICrud<AppointmentSurvey>
    {
        List<int> getAllAppointmentSurveyIds();

        List<AppointmentSurvey> GetSurveysById(List<int> ids);
    }
}
