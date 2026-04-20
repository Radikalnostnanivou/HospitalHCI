using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Repository.Interfaces
{
    public interface IVacationRepository : ICrud<Vacation>
    {
        bool AcceptVacation(Doctor doctor, Vacation vacation);

        bool RejectVacation(Doctor doctor, Vacation vacation, String comment);
    }
}
