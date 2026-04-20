/***********************************************************************
 * Module:  RoomController.cs
 * Author:  halid
 * Purpose: Definition of the Class Controller.RoomController
 ***********************************************************************/

using Model;
using Service;
using System;
using Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorp.View.Manager.Model.Rooms;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class RoomController : ICrud<Room>
    {
        public IRoomService service = RoomService.Instance;
        public void Create(Room newRoom)
        {
            service.Create(newRoom);
        }

        public Room Read(int identifier)
        {
            return service.Read(identifier);
        }

        public void Update(Room updatedRoom)
        {
            service.Update(updatedRoom);
        }

        public void Delete(int identifier)
        {
            service.Delete(identifier);
        }

        public void RenovateRoom(int identifier, DateTime start, DateTime end)
        {
            service.RenovateRoom(identifier, start, end);
        }

        public void CombineRooms(Room combineInto, Room selectedRoom)
        {
            service.CombineRooms(combineInto, selectedRoom);
        }

        public List<Room> GetAll()
        {
            return service.GetAll();
        }

        public ObservableCollection<RoomModel> GetAllRoomsVO()
        {
            List<Room> types = service.GetAll();
            ObservableCollection<RoomModel> result = new ObservableCollection<RoomModel>();
            foreach (Room it in types)
            {
                result.Add(new RoomModel(it.Identifier, it.DesignationCode, it.Floor, it.SurfaceArea, it.Renovating, it.RenovatedUntil, it.RoomTypeString));
            }
            return result;
        }

        public void CreateRoomType(RoomTypeModel newRoomType)
        {
            service.CreateRoomType(new RoomType(newRoomType));
        }

        public Boolean UpdateRoomType(Model.RoomType roomType)
        {
            throw new NotImplementedException();
        }

        public Boolean DeleteRoomType(Model.RoomType roomType)
        {
            throw new NotImplementedException();
        }

        public Model.RoomType ReadRoomType(Model.RoomType roomType)
        {
            throw new NotImplementedException();
        }

        public List<RoomType> GetAllRoomType()
        {
            throw new NotImplementedException();
        }

        public int GetActualCountForEquipment(int idRoom, int idEquipment)
        {
            return service.GetActualCountForEquipment(idRoom, idEquipment);
        }

        public ObservableCollection<RoomTypeModel> GetAllRoomTypeView()
        {
            return service.GetAllRoomTypeView();

        }
        public Room findFreeRoom(DateTime start, DateTime end)
        {
            return service.findFreeRoom(start, end);

        }
    }
}