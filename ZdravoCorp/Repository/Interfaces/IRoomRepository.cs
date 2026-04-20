using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorp.Repository.Interfaces
{
    public interface IRoomRepository : ICrud<Room>
    {
        List<Room> GetRoomsByInternalID(HashSet<int> identifiers);

        void AddEquipment(Equipment equipment, int room_id);

        void CombineRooms(Room combineInto, Room selectedRoom);

        int GetActualCountForEquipment(int id_room, int id_equipment);

        void ChangeActualCountOfEquipment(int id_from_room, int id_equipment, int count);
    }
}
