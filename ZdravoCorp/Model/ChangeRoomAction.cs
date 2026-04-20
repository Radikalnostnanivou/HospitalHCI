// File:    ChangeRoomAction.cs
// Author:  halid
// Created: Sunday, 24 April 2022 22:23:04
// Purpose: Definition of Class ChangeRoomAction

using Repository;
using Service;
using System;
using System.Collections.Generic;
using ZdravoCorp.Exceptions;

namespace Model
{
    public class ChangeRoomAction : IAction
    {
        private int id_incoming_room;
        private int id_outgoing_room;
        private int id_equipment;
        private int count;

        public ChangeRoomAction()
        {
        }

        public ChangeRoomAction(int id_incoming_room, int id_outgoing_room, int id_equipment, int count)
        {
            this.id_incoming_room = id_incoming_room;
            this.id_outgoing_room = id_outgoing_room;
            this.id_equipment = id_equipment;
            this.count = count;
        }

        public int Id_equipment { get => id_equipment; }
        public int Count { get => count; }
        public int Id_incoming_room { get => id_incoming_room; }
        public int Id_outgoing_room { get => id_outgoing_room; }

        public void Execute()
        {
            HashSet<int> idsOfRoom = new HashSet<int> { Id_incoming_room, Id_outgoing_room };
            List<Room> rooms = RoomService.Instance.GetRoomsByInternalID(idsOfRoom);
            CheckIfRoomsLoaded(rooms.Count);
            SetRoomPositions(ref rooms);
            ChangeEquipmentCountIncoming(rooms[0]);
            ChangeEquipmentCountOutgoing(rooms[1]);
            RoomService.Instance.Update(rooms[0]);
            RoomService.Instance.Update(rooms[1]);
        }

        public void FromCSV(string[] values)
        {
            this.id_incoming_room = int.Parse(values[0]);
            this.id_outgoing_room = int.Parse(values[1]);
            this.id_equipment = int.Parse(values[2]);
            this.count = int.Parse(values[3]);
        }

        public List<string> ToCSV()
        {
            List<String> result = new List<String>();
            result.Add(id_incoming_room.ToString());
            result.Add(id_outgoing_room.ToString());
            result.Add(id_equipment.ToString());
            result.Add(count.ToString());

            return result;
        }

        private void CheckIfRoomsLoaded(int count)
        {
            if (count != 2)
                throw new Exception("Loading of rooms for changeRoom action failed");
        }

        /*
         * Sets position of incoming and outgoing room in Iterable
         * First position is incomingRoom
         * Second position is outogingRoom
         */
        private void SetRoomPositions(ref List<Room> rooms)
        {
            List<Room> result = new List<Room>();
            if (rooms[0].Identifier == Id_outgoing_room)
            {
                result.Add(rooms[1]);
                result.Add(rooms[0]);
                rooms = result;
            }
        }

        private void ChangeEquipmentCountOutgoing(Room room)
        {
            foreach (Equipment it in room.Equipment)
            {
                if (it.Identifier == Id_equipment)
                {
                    if (it.Count > Count)
                    {
                        it.Count -= Count;
                        room.EditEquipment(it);
                        return;
                    }
                    else
                    {
                        room.RemoveEquipment(it);
                        return;
                    }
                }
            }
            throw new LocalisedException("EquipmentDoesntExists");
        }

        private void ChangeEquipmentCountIncoming(Room room)
        {
            foreach (Equipment it in room.Equipment)
            {
                if (it.Identifier == Id_equipment)
                {
                    it.Count += Count;
                    it.Actual_count += Count;
                    room.EditEquipment(it);
                    return;
                }
            }
            room.AddEquipment(new Equipment(Count, Count,
                    EquipmentService.Instance.ReadEquipmentType(Id_equipment)));
        }
    }
}