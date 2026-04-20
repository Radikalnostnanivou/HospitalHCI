// File:    EquipmentService.cs
// Author:  halid
// Created: Tuesday, 12 April 2022 12:33:08
// Purpose: Definition of Class EquipmentService

using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorp.Repository.Interfaces;
using ZdravoCorp.Service.Interfaces;
using ZdravoCorp.View.Manager.Model.Equipments;
using ZdravoCorp.View.Manager.Model.Rooms;

namespace Service
{
    public class EquipmentService : ICrud<Equipment> , IEquipmentService
    {
        private static EquipmentService instance = null;
        private ActionService actionService;
        private IEquipmentTypeRepository typeRepository = EquipmentTypeRepository.Instance;

        private IRoomService roomService = RoomService.Instance;

        public Boolean CreateEquipment(EquipmentTypeModel type, int count, RoomModel room)
        {
            Equipment equipment = new Equipment(count, count, FindEquipmentTypeByName(type.Name));
            roomService.AddEquipment(equipment, room.Identifier);
            return true;
        }

        public void Create(Equipment equipment)
        {
            throw new NotImplementedException();
        }

        public void Update(Equipment equipment)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Equipment Read(int id)
        {
            throw new NotImplementedException();
        }

        public List<Equipment> GetAll()
        {
            throw new NotImplementedException();
        }

        public void CreateEquipmentType(EquipmentType newEquipmentType)
        {
            typeRepository.Create(newEquipmentType);
        }

        public void UpdateEquipmentType(EquipmentType equipmentType)
        {
            typeRepository.Update(equipmentType);
        }

        public void DeleteEquipmentType(int id)
        {
            typeRepository.Delete(id);
        }

        public EquipmentType ReadEquipmentType(int id)
        {
             return typeRepository.Read(id);
        }

        public EquipmentType FindEquipmentTypeByName(String name)
        {
            return typeRepository.FindEquipmentTypeByName(name);
        }

        public void ChangePositionOfEquipment(DateTime excecutionDate, int idFromRoom, int idToRoom, int idEquipment, int count)
        {
            actionService.CreateAction(new Model.Action(ActionType.changePosition, 
                excecutionDate, new ChangeRoomAction(idToRoom, idFromRoom, idEquipment, count)));
            roomService.ChangeActualCountOfEquipment(idFromRoom, idEquipment, -count);
        }

        public ObservableCollection<EquipmentTypeModel> GetAllEquipmentType()
        {
            List<EquipmentType> types = typeRepository.GetAllEquipmentType();
            ObservableCollection<EquipmentTypeModel> result = new ObservableCollection<EquipmentTypeModel>();
            foreach(EquipmentType it in types)
            {
                result.Add(new EquipmentTypeModel(it.Name, it.Description, it.Disposable));
            }
            return result;
        }

        public ObservableCollection<EquipmentModel> GetAllEquipmentTableVO()
        {
            ObservableCollection<EquipmentModel> result = new ObservableCollection<EquipmentModel>();
            List<Room> rooms = roomService.GetAll();
            foreach(Room room in rooms)
            {
                foreach(Equipment it in room.Equipment)
                {
                    result.Add(new EquipmentModel(it.Count, it.Actual_count,it.EquipmentType.Name, 
                        room.DesignationCode, it.EquipmentType.Description, it.EquipmentType.Disposable, 
                        room.Identifier, it.Identifier));
                }
                
            }

            return result;
        }
        public EquipmentService()
        {
            //actionService = ActionService.Instance;
        }

        public static EquipmentService Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new EquipmentService();
                }
                return instance ;
            }
        }
    }
}