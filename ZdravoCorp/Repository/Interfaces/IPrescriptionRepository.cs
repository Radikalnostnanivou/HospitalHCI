using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Repository.Interfaces
{
    public interface IPrescriptionRepository : ICrud<Prescription>
    {
        int CreateAndReturnID(Prescription element);
    }
}
