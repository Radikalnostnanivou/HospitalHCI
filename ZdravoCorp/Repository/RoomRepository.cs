/***********************************************************************
 * Module:  RoomController.cs
 * Author:  halid
 * Purpose: Definition of the Class Controller.RoomController
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class RoomRepository : Repository<Room> , IRoomRepository
    {
        private static RoomRepository instance = null;

        public RoomRepository()
        {
            dataBase.SetPath("..\\..\\Data\\roomsDB.csv");
            InstantiateIDSet(GetAll());
        }

        //Gets Rooms by internal ID
        //Returns List of all rooms thar are found
        //Returns empty List if nothing was found
        public List<Room> GetRoomsByInternalID(HashSet<int> identifiers)
        {
            lock (key)
            {
                List<Room> rooms = GetAll();
                List<Room> result = new List<Room>();
                foreach (Room room in rooms)
                {
                    if (identifiers.Contains(room.Identifier))
                    {
                        result.Add(room);
                    }
                }
                return result;
            }
        }

        public void AddEquipment(Equipment equipment, int room_id)
        {
            lock (key)
            {
                List<Room> rooms = GetAll();
                Room room = FindRoomByID(room_id, rooms);
                AddEquipmentToRoom(room, equipment);
                dataBase.SaveChanges(rooms);
            }
        }

        public void CombineRooms(Room combineInto, Room selectedRoom)
        {
            lock (key)
            {
                MoveEquipment(combineInto, selectedRoom);
                MoveAppointements(combineInto, selectedRoom);
                MoveMedications(combineInto, selectedRoom);
                combineInto.SurfaceArea += selectedRoom.SurfaceArea;
                Delete(selectedRoom.Identifier);
                Update(combineInto);
            }
        }

        public int GetActualCountForEquipment(int id_room, int id_equipment)
        {
            lock (key)
            {
                Room room = Read(id_room);
                foreach (Equipment it in room.Equipment)
                {
                    if (it.Identifier.Equals(id_equipment))
                    {
                        return it.Actual_count;
                    }
                }
                return 0;
            }
        }

        public void ChangeActualCountOfEquipment(int id_from_room, int id_equipment, int count)
        {
            lock (key)
            {
                List<Room> rooms = GetAll();
                Room room = FindRoomByID(id_from_room, rooms);
                ChangeActualCountInRoom(room, count, id_equipment);
                dataBase.SaveChanges(rooms);
            }
        }

        public override Room Read(int id)
        {
            lock (key)
            {
                Dictionary<int, Room> rooms = GetAll()
                    .ToDictionary(keySelector: m => m.Identifier, elementSelector: m => m);
                if (!rooms.ContainsKey(id))
                {
                    throw new LocalisedException("RoomIdDoesntExist");
                }
                return rooms[id];
            }
        }

        public override void Create(Room element)
        {
            lock (key)
            {
                element.Identifier = GenerateID();
                List<Room> rooms = GetAll();
                CheckIfDesignationCodeExists(element.DesignationCode, rooms);
                dataBase.AppendToDB(element);
                idMap.Add(element.Identifier);
            }
        }

        public override void Update(Room element)
        {
            lock (key)
            {
                List<Room> elements = GetAll();
                CheckIfChangedDesignationCodeExists(element, elements);
                ReplaceRoomByID(elements, element);
                dataBase.SaveChanges(elements);
            }
        }

        public override void Delete(int id)
        {
            lock (key)
            {
                List<Room> rooms = GetAll();
                FindAndDeleteRoomByID(id, rooms);
                dataBase.SaveChanges(rooms);
                idMap.Remove(id);
            }
        }

        public new List<Room> GetAll()
        {
            lock (key)
            {
                List<Room> result = base.GetAll();
                Dictionary<int, EquipmentType> types = EquipmentTypeRepository.Instance.GetAllEquipmentType()
                    .ToDictionary(keySelector: m => m.Identifier, elementSelector: m => m);
                Dictionary<int, Appointment> appointments = AppointmentRepository.Instance.GetAll()
                    .ToDictionary(keySelector: m => m.Id, elementSelector: m => m);
                foreach (Room room in result)
                {
                    LoadEquipmentTypesForRoom(room, types);
                    LoadAppointmentsForRoom(room, appointments);
                }
                return result;
            }
        }

        protected override void InstantiateIDSet(List<Room> elements)
        {
            lock (key)
            {
                foreach (Room element in elements)
                {
                    idMap.Add(element.Identifier);
                }
            }
        }

        private void CheckIfChangedDesignationCodeExists(Room updatedRoom, List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                if (updatedRoom.DesignationCode.Equals(room.DesignationCode) && room.Identifier != updatedRoom.Identifier)
                {
                    throw new LocalisedException("RoomDesignationExists");
                }
            }
        }

        private void ReplaceRoomByID(List<Room> rooms, Room room)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (room.Identifier == rooms[i].Identifier)
                {
                    rooms[i] = room;
                    return;
                }
            }
            throw new LocalisedException("RoomIdDoesntExist");
        }

        private void LoadEquipmentTypesForRoom(Room room, Dictionary<int, EquipmentType> types)
        {
            for (int i = 0; i < room.Equipment.Count; i++)
            {
                if (types.ContainsKey(room.Equipment[i].Identifier))
                {
                    room.Equipment[i].EquipmentType = types[room.Equipment[i].Identifier];
                }
            }
        }

        private void LoadAppointmentsForRoom(Room room, Dictionary<int, Appointment> appointments)
        {
            for (int i = 0; i < room.Appointment.Count; i++)
            {
                if (appointments.ContainsKey(room.Appointment[i].Id))
                {
                    room.Appointment[i] = appointments[room.Appointment[i].Id];
                }
            }
        }

        private void CheckIfDesignationCodeExists(string designation, List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                if (room.DesignationCode.Equals(designation))
                {
                    throw new LocalisedException("RoomDesignationExists");
                }
            }
        }

        private void FindAndDeleteRoomByID(int id, List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                if (id == room.Identifier)
                {
                    rooms.Remove(room);
                    return;
                }
            }
            throw new LocalisedException("RoomIdDoesntExist");
        }

        private Room FindRoomByID(int id, List<Room> rooms)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (id == rooms[i].Identifier)
                {
                    return rooms[i];
                }
            }
            throw new LocalisedException("RoomIdDoesntExist");
        }

        private void MoveEquipment(Room into, Room from)
        {
            foreach (Equipment equipment in from.Equipment)
            {
                into.Equipment.Add(equipment);
            }
        }

        private void MoveAppointements(Room into, Room from)
        {
            foreach (Appointment appointment in from.Appointment)
            {
                into.Appointment.Add(appointment);
            }
        }

        private void MoveMedications(Room into, Room from)
        {
            foreach (Medication medication in from.Medication)
            {
                into.Medication.Add(medication);
            }
        }

        private void ChangeActualCountInRoom(Room room, int count, int id_equipment)
        {
            for (int i = 0; i < room.Equipment.Count; i++)
            {
                if (room.Equipment[i].Identifier.Equals(id_equipment))
                {
                    room.Equipment[i].Actual_count += count;
                    return;
                }
            }
            throw new LocalisedException("EquipmentDoesntExist");
        }

        private void AddEquipmentToRoom(Room room, Equipment equipment)
        {
            bool exists = false;
            for (int i = 0; i < room.Equipment.Count; i++)
            {
                if (room.Equipment[i].Identifier == equipment.Identifier)
                {
                    room.Equipment[i].Count += equipment.Count;
                    room.Equipment[i].Actual_count += equipment.Actual_count;
                    exists = true;
                    break;
                }
            }
            if (!exists)
            {
                room.Equipment.Add(equipment);
            }
        }

        public static RoomRepository Instance
        {
            get 
            {
                if (instance == null)
                {
                    lock (key)
                        {
                            if (instance == null)
                            {
                                instance = new RoomRepository();
                            }
                        }
                }
                return instance;
            }
        }

    }
}