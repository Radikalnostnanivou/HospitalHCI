using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Repository.Interfaces
{
    public interface IEquipmentTypeRepository : ICrud<EquipmentType>
    {

        EquipmentType FindEquipmentTypeByName(String name);

        List<EquipmentType> GetAllEquipmentType();
    }
}
