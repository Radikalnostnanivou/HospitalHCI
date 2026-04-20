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
    public class RoomTypeRepository : Repository<RoomType> , IRoomTypeRepository
    {
        private static RoomTypeRepository instance = null;
        public RoomTypeRepository()
        {
            dataBase.SetPath("..\\..\\Data\\roomTypesDB.csv");
        }

        public override void Create(RoomType type)
        {
            lock (key)
            {
                List<RoomType> types = GetAll();
                CheckIfRoomTypeExists(types, type);
                dataBase.AppendToDB(type);
            }
        }

        /*
         * Method isn't needed but is requered for abstract class
         */
        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(RoomType type)
        {
            lock (key)
            {
                List<RoomType> types = GetAll();
                RemoveType(types, type);
                dataBase.SaveChanges(types);
            }
        }

        /*
         * Method isn't needed but is requered for abstract class
         */
        public override RoomType Read(int id)
        {
            throw new NotImplementedException();
        }

        /*
         * Method isn't needed but is requered for abstract class
         */
        public override void Update(RoomType type)
        {
            throw new NotImplementedException();
        }

        /*
         * Method isn't needed but is requered for abstract class
         */
        protected override void InstantiateIDSet(List<RoomType> elements)
        {
            throw new NotImplementedException();
        }

        private void CheckIfRoomTypeExists(List<RoomType> elements, RoomType element)
        {
            foreach (RoomType it in elements)
            {
                if (it.Name.Equals(element.Name))
                {
                    throw new LocalisedException("RoomTypeAlreadyExists");
                }
            }
        }

        private void RemoveType(List<RoomType> elements, RoomType element)
        {
            foreach (RoomType type in elements)
            {
                if (element.Name.Equals(type.Name))
                {
                    elements.Remove(type);
                    return;
                }
            }
            throw new LocalisedException("RoomTypeDoesntExists");
        }

        public static RoomTypeRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new RoomTypeRepository();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
