using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Service.Interfaces
{
    public interface IAppointmentSurveyService : ICrud<AppointmentSurvey>
    {
        Boolean DoneSurvey(Appointment appointment);

        string GetResultsForDoctor(Doctor doctor, DateTime start, DateTime end);
    }
}
