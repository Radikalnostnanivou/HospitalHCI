using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Repository.Interfaces
{
    public interface IDataBase<T>
    {
        List<T> GetAll();

        void AppendToDB(T element);

        void SaveChanges(List<T> elements);

        string GetPath();

        void SetPath(string path);
    }
}
