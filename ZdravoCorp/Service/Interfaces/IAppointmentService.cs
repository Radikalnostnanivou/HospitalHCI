using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Service.Interfaces
{
    public interface IAppointmentService : ICrud<Appointment>
    {
        List<Appointment> GetFutureAppointments();

        List<Appointment> GetPastAppointments();

        Boolean IsTroll(Appointment appointment);

        List<Appointment> GetFutureAppointmentsForPatient(Patient patient);

        List<Appointment> GetPastAppointmentsForPatient(Patient patient);

        List<Appointment> SuggestAppointments(WantedAppointment wantedAppointment);
    }
}
