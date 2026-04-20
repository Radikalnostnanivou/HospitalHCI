// File:    Manager.cs
// Author:  halid
// Created: Thursday, 14 April 2022 21:31:06
// Purpose: Definition of Class Manager

using Repository;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Manager : Employee, Serializable
    {
        public Manager()
        {
        }

        public Manager(Employee employee) : base(employee)
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