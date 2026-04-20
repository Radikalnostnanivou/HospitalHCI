/***********************************************************************
 * Module:  Equipment.cs
 * Author:  halid
 * Purpose: Definition of the Class Model.Equipment
 ***********************************************************************/

using Repository;
using System;
using System.Collections.Generic;
using ZdravoCorp.View.Manager.Model.Equipments;

namespace Model
{
    public class EquipmentType : Serializable
    {
        private int identifier;

        public EquipmentType(int identifier)
        {
            this.Identifier = identifier;
        }

        public EquipmentType()
        {
        }

        public EquipmentType(EquipmentTypeModel type)
        {
            if (type != null)
            {
                this.name = type.Name;
                this.description = type.Description;
                this.disposable = type.Disposable;
            }
        }

        private String name;
        private String description;
        private bool disposable;

        public int Identifier { get => identifier; set => identifier = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public bool Disposable { get => disposable; set => disposable = value; }

        public void FromCSV(string[] values)
        {
            this.identifier = int.Parse(values[0]);
            this.Name = values[1];
            this.Description = values[2];
            this.Disposable = bool.Parse(values[3]);
        }

        public List<String> ToCSV()
        {
            List<String> result = new List<String>();

            result.Add(identifier.ToString());
            result.Add(Name.ToString());
            result.Add(Description.ToString());
            result.Add(Disposable.ToString());

            return result;
        }
    }
}