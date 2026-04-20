using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Core;

namespace ZdravoCorp.View.Manager.Model.Rooms
{
    public class RoomTypeModel : ObservableObject
    {
        private string name;

        public RoomTypeModel()
        {
        }

        public RoomTypeModel(string name)
        {
            Name = name;
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
    }
}
