// File:    EquipmentController.cs
// Author:  halid
// Created: Tuesday, 12 April 2022 12:34:26
// Purpose: Definition of Class EquipmentController

using Model;
using Service;
using System;
using Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorp.View.Manager.Model.Equipments;
using ZdravoCorp.View.Manager.Model.Rooms;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class EquipmentController : ICrud<Equipment>
    {
        public IEquipmentService service = EquipmentService.Instance;
        public Boolean CreateEquipment(EquipmentTypeModel type, int count, RoomModel room)
        {
            return service.CreateEquipment(type, count, room);
        }

        public void Create(Equipment equipment)
        {
            throw new NotImplementedException();
        }

        public void Update(Equipment equipment)
        {
            throw new NotImplementedException();
        }

        public void Delete(int identificator)
        {
            throw new NotImplementedException();
        }

        public Equipment Read(int identificator)
        {
            throw new NotImplementedException();
        }

        public List<Equipment> GetAll()
        {
            throw new NotImplementedException();
        }

        public void CreateEquipmentType(EquipmentTypeModel newEquipmentType)
        {
            EquipmentType result = new EquipmentType(newEquipmentType);
            service.CreateEquipmentType(result);
        }

        public void UpdateEquipmentType(EquipmentType equipmentType)
        {
            service.UpdateEquipmentType(equipmentType);
        }

        public void DeleteEquipmentType(int id)
        {
            service.DeleteEquipmentType(id);
        }

        public EquipmentType ReadEquipmentType(int id)
        {
            throw new NotImplementedException();
        }

        public void ChangePositionOfEquipment(DateTime excecutionDate,int idFromRoom,int idToRoom, int idEquipment, int count)
        {
            service.ChangePositionOfEquipment(excecutionDate, idFromRoom, idToRoom, idEquipment, count);
        }

        public ObservableCollection<EquipmentTypeModel> GetAllEquipmentType()
        {
            return service.GetAllEquipmentType(); ;
        }

        public ObservableCollection<EquipmentModel> GetAllEquipmentTableVO()
        {
            return service.GetAllEquipmentTableVO();
        }
    }
}