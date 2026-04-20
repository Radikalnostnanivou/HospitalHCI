using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Service.Interfaces
{
    public interface IVacationService : ICrud<Vacation>
    {

        Boolean AcceptVacation(Doctor doctor, Vacation vacation);

        Boolean RejectVacation(Doctor doctor, Vacation vacation, String comment);

    }
}
