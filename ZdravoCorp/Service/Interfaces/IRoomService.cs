using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Manager.Model.Rooms;

namespace ZdravoCorp.Service.Interfaces
{
    public interface IRoomService : ICrud<Room>
    {

        void RenovateRoom(int identifier, DateTime start, DateTime end);

        void CombineRooms(Room combineInto, Room selectedRoom);

        void CreateRoomType(RoomType newRoomType);

        Boolean UpdateRoomType(Model.RoomType roomType);

        Boolean DeleteRoomType(Model.RoomType roomType);

        Model.RoomType ReadRoomType(Model.RoomType roomType);

        List<RoomType> GetAllRoomType();

        int GetActualCountForEquipment(int idRoom, int idEquipment);

        ObservableCollection<RoomTypeModel> GetAllRoomTypeView();

        Room findFreeRoom(DateTime start, DateTime end);

        void AddEquipment(Equipment equipment, int id);

        void ChangeActualCountOfEquipment(int id_from_room, int id_equipment, int count);
    }
}
