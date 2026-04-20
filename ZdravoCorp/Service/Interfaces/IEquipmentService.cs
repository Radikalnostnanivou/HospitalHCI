using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Manager.Model.Equipments;
using ZdravoCorp.View.Manager.Model.Rooms;

namespace ZdravoCorp.Service.Interfaces
{
    public interface IEquipmentService : ICrud<Equipment>
    {
        Boolean CreateEquipment(EquipmentTypeModel type, int count, RoomModel room);

        void CreateEquipmentType(EquipmentType newEquipmentType);

        void UpdateEquipmentType(EquipmentType equipmentType);

        void DeleteEquipmentType(int id);

        EquipmentType ReadEquipmentType(int id);

        void ChangePositionOfEquipment(DateTime excecutionDate, int idFromRoom, int idToRoom, int idEquipment, int count);

        ObservableCollection<EquipmentTypeModel> GetAllEquipmentType();

        ObservableCollection<EquipmentModel> GetAllEquipmentTableVO();
    }
}
