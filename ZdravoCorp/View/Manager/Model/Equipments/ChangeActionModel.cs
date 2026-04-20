using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Core;

namespace ZdravoCorp.View.Manager.Model.Equipments
{
    public class ChangeActionModel : ObservableObject
    {
        private int id;
        private DateTime executionDate;
        private int id_incoming_room;
        private int id_outgoing_room;
        private int id_equipment;
        private int count;
        private string incomingRoom;
        private string outgoingRoom;
        private string equipment;

        public ChangeActionModel()
        {

        }

        public ChangeActionModel(ChangeActionModel action)
        {
            Id = action.id;
            Id_incoming_room = action.id_incoming_room;
            Id_outgoing_room = action.id_outgoing_room;
            Id_equipment = action.id_equipment;
            Count = action.count;
            ExecutionDate = action.executionDate;
            IncomingRoom = action.incomingRoom;
            OutgoingRoom = action.outgoingRoom;
            Equipment = action.equipment;
        }

        public ChangeActionModel(int id, DateTime executionDate, int id_incoming_room, int id_outgoing_room, int id_equipment, int count, string incomingRoom, string outgoingRoom, string equipment)
        {
            Id = id;
            Id_incoming_room = id_incoming_room;
            Id_outgoing_room = id_outgoing_room;
            Id_equipment = id_equipment;
            Count = count;
            ExecutionDate = executionDate;
            IncomingRoom = incomingRoom;
            OutgoingRoom = outgoingRoom;
            Equipment = equipment;
        }

        public int Id
        {
            get => id;
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id_incoming_room
        {
            get => id_incoming_room;
            set
            {
                if (value != id_incoming_room)
                {
                    id_incoming_room = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id_outgoing_room
        {
            get => id_outgoing_room;
            set
            {
                if (value != id_outgoing_room)
                {
                    id_outgoing_room = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id_equipment
        {
            get => id_equipment;
            set
            {
                if (value != id_equipment)
                {
                    id_equipment = value;
                    OnPropertyChanged();
                }
            }
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
        public DateTime ExecutionDate
        {
            get => executionDate;
            set
            {
                if (value != executionDate)
                {
                    executionDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ExecutionDateString
        {
            get => executionDate.ToString("g", CultureInfo.GetCultureInfo("en-GB"));
            set
            {
                if (value != executionDate.ToString("g", CultureInfo.GetCultureInfo("en-GB")))
                {
                    executionDate = DateTime.Parse(value);
                    OnPropertyChanged();
                }
            }
        }

        public string IncomingRoom
        {
            get => incomingRoom;
            set
            {
                if (value != incomingRoom)
                {
                    incomingRoom = value;
                    OnPropertyChanged();
                }
            }
        }
        public string OutgoingRoom
        {
            get => outgoingRoom;
            set
            {
                if (value != outgoingRoom)
                {
                    outgoingRoom = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Equipment
        {
            get => equipment;
            set
            {
                if (value != equipment)
                {
                    equipment = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
