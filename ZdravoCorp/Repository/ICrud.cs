using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface ICrud<T>
    {
        T Read(int id);
        void Create(T element);
        void Update(T element);
        void Delete(int id);
        List<T> GetAll();
    }
}
