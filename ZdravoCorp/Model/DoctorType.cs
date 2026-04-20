// File:    DoctorType.cs
// Author:  halid
// Created: Thursday, 14 April 2022 22:30:27
// Purpose: Definition of Class DoctorType

using Repository;
using System;
using System.Collections.Generic;

namespace Model
{
    public class DoctorType : Serializable
    {
        private String type;
        public String Type { get { return type; } set { type = value; } }

        public DoctorType(string type)
        {
            this.type = type;
        }
        public DoctorType()
        {
        }

        public void FromCSV(string[] values)
        {
            Type = values[0];
        }

        public List<String> ToCSV()
        {
            return new List<String>() { Type };
        }
    }
}