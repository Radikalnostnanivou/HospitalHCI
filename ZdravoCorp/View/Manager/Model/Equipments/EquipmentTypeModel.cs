using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Core;

namespace ZdravoCorp.View.Manager.Model.Equipments
{
    public class EquipmentTypeModel : ObservableObject
    {
        private string name;
        private string description;
        private bool disposable;

        public EquipmentTypeModel()
        {
        }

        public EquipmentTypeModel(string name, string description, bool disposable)
        {
            this.name = name;
            this.description = description;
            this.disposable = disposable;
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
    }
}
