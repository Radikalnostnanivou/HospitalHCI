using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Service.Interfaces
{
    public interface IDoctorService : ICrud<Doctor>
    {
        void CreateDoctorType(DoctorType newDoctorType);

        void UpdateDoctorType(DoctorType doctorType);

        void DeleteDoctorType(String doctorType);

        DoctorType ReadDoctorType(String doctorType);

        List<DoctorType> GetAllDoctorType();
    }
}
