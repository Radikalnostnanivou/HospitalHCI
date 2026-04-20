using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Core;

namespace ZdravoCorp.View.Manager.Model.Equipments
{
    public class EquipmentModel : ObservableObject
    {
        private int count;
        private int actual_count;
        private string name;
        private string designationCode;
        private string description;
        private bool disposable;
        private int room_identifier;
        private int equipment_identifier;

        public EquipmentModel()
        {
        }

        public EquipmentModel(EquipmentModel model)
        {
            this.count = model.count;
            this.name = model.name;
            Actual_count = model.actual_count;
            this.designationCode = model.designationCode;
            Description = model.description;
            this.disposable = model.disposable;
            this.room_identifier = model.room_identifier;
            this.equipment_identifier = model.equipment_identifier;
        }

        public EquipmentModel(int count, int actual_count, string name, string designationCode, string description, bool disposable, int room_identifier, int equipment_identifier)
        {
            this.count = count;
            this.name = name;
            Actual_count = actual_count;
            this.designationCode = designationCode;
            Description = description;
            this.disposable = disposable;
            this.room_identifier = room_identifier;
            this.equipment_identifier = equipment_identifier;
        }

        public int Count
        {
            get => count;
            set
            {
                if (value != count)
                {
                    count = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Name
        {
            get => name;
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string DesignationCode
        {
            get => designationCode;
            set
            {
                if (value != designationCode)
                {
                    designationCode = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Room_identifier
        {
            get => room_identifier;
            set
            {
                if (value != room_identifier)
                {
                    room_identifier = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Equipment_identifier
        {
            get => equipment_identifier;
            set
            {
                if (value != equipment_identifier)
                {
                    equipment_identifier = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Disposable
        {
            get => disposable;
            set
            {
                if (value != disposable)
                {
                    disposable = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Actual_count
        {
            get => actual_count;
            set
            {
                if (value != actual_count)
                {
                    actual_count = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get => description;
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
