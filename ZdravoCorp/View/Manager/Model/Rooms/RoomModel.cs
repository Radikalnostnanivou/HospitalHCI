using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Core;

namespace ZdravoCorp.View.Manager.Model.Rooms
{
    public class RoomModel : ObservableObject
    {
        private int identifier;
        private string designationCode;
        private int floor;
        private float surfaceArea;
        private bool renovating;
        private DateTime renovatedUntil;
        private string type;

        public RoomModel()
        {
        }

        public RoomModel(int identifier, string designationCode, int floor, float surfaceArea, bool renovating, DateTime renovatedUntil, string type)
        {
            Identifier = identifier;
            DesignationCode = designationCode;
            Floor = floor;
            SurfaceArea = surfaceArea;
            Renovating = renovating;
            RenovatedUntil = renovatedUntil;
            Type = type;
        }

        public int Identifier
        {
            get => identifier;
            set
            {
                if (value != identifier)
                {
                    identifier = value;
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
        public float SurfaceArea
        {
            get => surfaceArea;
            set
            {
                if (value != surfaceArea)
                {
                    surfaceArea = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Renovating
        {
            get => renovating;
            set
            {
                if (value != renovating)
                {
                    renovating = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime RenovatedUntil
        {
            get => renovatedUntil;
            set
            {
                if (value != renovatedUntil)
                {
                    renovatedUntil = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Type
        {
            get => type;
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Floor
        {
            get => floor;
            set
            {
                if (value != floor)
                {
                    floor = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
