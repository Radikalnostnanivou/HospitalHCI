using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Service.Interfaces
{
    public interface IAnamnesisService : IService<Anamnesis>
    {
        Anamnesis FindAnamnesisByAppointmentId(int id);
    }
}
