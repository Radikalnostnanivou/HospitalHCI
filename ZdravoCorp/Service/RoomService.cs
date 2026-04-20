/***********************************************************************
 * Module:  RoomController.cs
 * Author:  halid
 * Purpose: Definition of the Class Controller.RoomController
 ***********************************************************************/

using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorp.Repository.Interfaces;
using ZdravoCorp.Service.Interfaces;
using ZdravoCorp.View.Manager.Model.Rooms;

namespace Service
{
    public class RoomService : ICrud<Room> , IRoomService
    {
        private static RoomService instance = null;
        private ActionService actionService;
        private static readonly object key = new object();

        private IRoomRepository repository = RoomRepository.Instance;
        public void Create(Room newRoom)
        {
            repository.Create(newRoom);
        }

        public Room Read(int identifier)
        {
            return repository.Read(identifier);
        }

        public Room ReadRoomByIndex(int index)
        {
            return repository.Read(index);
        }

        public void Update(Room updatedRoom)
        {
            repository.Update(updatedRoom);
        }

        public void Delete(int identifier)
        {
            repository.Delete(identifier);
        }

        public void RenovateRoom(int identifier, DateTime start, DateTime end)
        {
            actionService.CreateAction(new Model.Action(ActionType.renovation, 
                start, new RenovationAction(end, identifier, true)));
        }

        public void CombineRooms(Room combineInto, Room selectedRoom)
        {
            repository.CombineRooms(combineInto, selectedRoom);
        }

        public List<Room> GetRoomsByInternalID(HashSet<int> identifiers)
        {
            return repository.GetRoomsByInternalID(identifiers);
        }

        public List<Room> GetAll()
        {
            return repository.GetAll();
        }

        public void CreateRoomType(Model.RoomType newRoomType)
        {
            RoomTypeRepository.Instance.Create(newRoomType);
        }

        public void UpdateRoomType(Model.RoomType roomType)
        {
            RoomTypeRepository.Instance.Update(roomType);
        }

        public void DeleteRoomType(Model.RoomType roomType)
        {
            RoomTypeRepository.Instance.Delete(roomType);
        }

        public int GetActualCountForEquipment(int id_room, int id_equipment)
        {
            return repository.GetActualCountForEquipment(id_room, id_equipment);
        }

        public void ChangeActualCountOfEquipment(int id_from_room, int id_equipment,int count)
        {
            repository.ChangeActualCountOfEquipment(id_from_room, id_equipment, count);
        }

        public Model.RoomType ReadRoomType(Model.RoomType roomType)
        {
            throw new NotImplementedException();
        }

        public List<RoomType> GetAllRoomType()
        {
            return RoomTypeRepository.Instance.GetAll();
        }


        public ObservableCollection<RoomTypeModel> GetAllRoomTypeView()
        {
            List<RoomType> types = RoomTypeRepository.Instance.GetAll();
            ObservableCollection<RoomTypeModel> result = new ObservableCollection<RoomTypeModel>();
            foreach(RoomType roomType in types)
            {
                result.Add(new RoomTypeModel(roomType.Name));
            }
            return result;
        }

        public void AddEquipment(Equipment equipment, int id)
        {
            repository.AddEquipment(equipment, id);
        }

        public RoomService()
        {
            //actionService = ActionService.Instance;
        }
        public static RoomService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new RoomService();
                        }
                    }
                }
                return instance;
            }
        }
        public Room findFreeRoom(DateTime start, DateTime end) 
        {
            Room freeRoom = null;

            List<Room> rooms = GetAll();
            foreach (Room room in rooms)
            {
                if(room.RoomType.Equals("Storage Room") || room.RoomType.Equals("Waiting Room") || room.RoomType.Equals("Meeting Room"))
                {
                    break;
                }
                List<DateTime> datesStart = new List<DateTime>();
                List<DateTime> datesEnd = new List<DateTime>();

                Boolean appOk = false;
                Boolean renovationOk = false;
                foreach (Appointment a in room.Appointment)
                {
                    datesStart.Add(a.StartDate);
                    datesEnd.Add(a.endDate);
                }
                if(!(datesStart.Contains(start) || datesEnd.Contains(end)))
                {
                    appOk = true;
                }
                if (!room.Renovating)
                {
                    renovationOk = true;
                }
                if(appOk && renovationOk)
                {
                    freeRoom = room;
                }
            }
            
            return freeRoom;
        }

        public void CreateRoomType(RoomTypeModel newRoomType)
        {
            throw new NotImplementedException();
        }

        bool IRoomService.UpdateRoomType(RoomType roomType)
        {
            throw new NotImplementedException();
        }

        bool IRoomService.DeleteRoomType(RoomType roomType)
        {
            throw new NotImplementedException();
        }
    }
}