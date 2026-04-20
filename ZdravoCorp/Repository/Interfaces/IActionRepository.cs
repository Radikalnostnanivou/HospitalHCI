using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Repository.Interfaces
{
    public interface IActionRepository : ICrud<Model.Action>
    {
        void SaveActions(List<Model.Action> elements);
    }
}
