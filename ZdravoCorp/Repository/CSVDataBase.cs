using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class CSVDataBase<T> : IDataBase<T> where T : Serializable, new()
    {
        protected string dbPath;
        protected Serializer<T> serializer = new Serializer<T>();
        public List<T> GetAll()
        {
            return serializer.FromCSV(dbPath);
        }

        public void AppendToDB(T element)
        {
            serializer.ToCSVAppend(dbPath, new List<T>() { element });
        }

        public void SaveChanges(List<T> elements)
        {
            serializer.ToCSV(dbPath, elements);
        }

        public string GetPath()
        {
            return dbPath;
        }

        public void SetPath(string path)
        {
            this.dbPath = path;
        }
    }
}
