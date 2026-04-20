/***********************************************************************
 * Module:  Equipment.cs
 * Author:  halid
 * Purpose: Definition of the Class Model.Equipment
 ***********************************************************************/

using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoCorp.View.Manager.Model.Equipments;

namespace Model
{
    public class Equipment : Serializable
    {
        private int count;
        private int actual_count;

        private EquipmentType equipmentType;

        /// <summary>
        /// Property for EquipmentType
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public EquipmentType EquipmentType
        {
            get
            {
                return equipmentType;
            }
            set
            {
                this.equipmentType = value;
            }
        }

        public int Identifier { get => equipmentType.Identifier; set => equipmentType.Identifier = value; }
        public int Count { get => count; set => count = value; }
        public int Actual_count { get => actual_count; set => actual_count = value; }

        public string Name { get => equipmentType.Name; set => equipmentType.Name = value; }

        public Equipment(int identifier, int count, int actual_count)
        {
            this.EquipmentType = new EquipmentType(identifier);
            this.Count = count;
            this.Actual_count = actual_count;
        }

        public Equipment(int count, int actual_count, EquipmentType equipmentType)
        {
            this.count = count;
            this.Actual_count = actual_count;
            this.equipmentType = equipmentType;
        }

        public Equipment(int count, int actual_count, EquipmentTypeModel equipmentType)
        {
            this.count = count;
            this.Actual_count = actual_count;
            this.equipmentType = new EquipmentType(equipmentType);
        }

        public void FromCSV(string[] values)
        {
            count = int.Parse(values[0]);
            actual_count = int.Parse(values[1]);
            equipmentType.FromCSV(values.Skip(2).ToArray());
        }

        public List<String> ToCSV()
        {
            List<String> result = new List<String>();

            result.Add(count.ToString());
            result.Add(actual_count.ToString());
            result = (List<string>) result.Concat(equipmentType.ToCSV());

            return result;
        }
    }
}