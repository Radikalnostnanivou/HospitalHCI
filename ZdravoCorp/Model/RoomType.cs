/***********************************************************************
 * Module:  RoomType.cs
 * Author:  halid
 * Purpose: Definition of the Class Model.RoomType
 ***********************************************************************/

using Repository;
using System;
using System.Collections.Generic;
using ZdravoCorp.View.Manager.Model.Rooms;

namespace Model
{
    public class RoomType : Serializable
    {
        private String name;

        public RoomType()
        {
        }

        public RoomType(string name)
        {
            this.Name = name;
        }

        public RoomType(RoomTypeModel type)
        {
            this.Name = type.Name;
        }

        public string Name { get => name; set => name = value; }

        public void FromCSV(string[] values)
        {
            Name = values[0];
        }

        public List<String> ToCSV()
        {
            return new List<String>() { Name };
        }
    }
}