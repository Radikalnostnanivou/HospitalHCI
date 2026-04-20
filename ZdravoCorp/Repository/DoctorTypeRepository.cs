using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class DoctorTypeRepository : Repository<DoctorType> , IDoctorTypeRepository
    {
        private static DoctorTypeRepository instance = null;

        public DoctorTypeRepository()
        {
            dataBase.SetPath("..\\..\\Data\\doctorTypesDB.csv");
        }

        public override void Create(DoctorType element)
        {
            lock (key)
            {
                CheckIfTypeExists(GetAll(), element);
                dataBase.AppendToDB(element);
            }
        }

        /*
        * Method isn't needed but is requered for abstract class
        */
        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(DoctorType type)
        {
            lock (key)
            {
                List<DoctorType> types = GetAll();
                RemoveType(types, type);
                dataBase.SaveChanges(types);
            }
        }

        /*
        * Method isn't needed but is requered for abstract class
        */
        public override DoctorType Read(int id)
        {
            throw new NotImplementedException();
        }

        /*
        * Method isn't needed but is requered for abstract class
        */
        public override void Update(DoctorType element)
        {
            throw new NotImplementedException();
        }

        /*
        * Method isn't needed but is requered for abstract class
        */
        protected override void InstantiateIDSet(List<DoctorType> elements)
        {
            throw new NotImplementedException();
        }

        private void CheckIfTypeExists(List<DoctorType> types, DoctorType type)
        {
            foreach (DoctorType it in types)
            {
                if (it.Type.Equals(type.Type))
                {
                    throw new LocalisedException("DoctorTypeExists");
                }
            }
        }

        private void RemoveType(List<DoctorType> types, DoctorType type)
        {
            foreach (DoctorType it in types)
            {
                if (it.Type.Equals(type.Type))
                {
                    types.Remove(it);
                    return;
                }
            }
            throw new LocalisedException("DoctorTypeDoesntExists");
        }

        public static DoctorTypeRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new DoctorTypeRepository();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
