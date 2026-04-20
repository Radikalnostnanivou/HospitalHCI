// File:    Secretary.cs
// Author:  halid
// Created: Thursday, 14 April 2022 21:31:18
// Purpose: Definition of Class Secretary

using Repository;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Secretary : Employee, Serializable
    {
        public Secretary()
        {
        }

        public Secretary(Employee employee) : base(employee)
        {

        }

        public List<String> ToCSV()
        {
            return base.ToCSV();
        }

        public void FromCSV(string[] values)
        {
            base.FromCSV(values);
        }
    }
}