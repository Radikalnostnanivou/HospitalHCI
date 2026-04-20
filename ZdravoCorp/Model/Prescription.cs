// File:    Prescription.cs
// Author:  halid
// Created: Friday, 15 April 2022 01:02:27
// Purpose: Definition of Class Prescription

using Repository;
using System;
using System.Collections.Generic;
using Controller;

namespace Model
{
    public class Prescription : Serializable
    {
        private int id;
        private int timesADay;
        private int durationDays;
        private int quantity;
        private string description;
        private Medication medication;

        public Prescription()
        { }

        public Prescription(int id)
        {
            this.id = id;
        }

        public Prescription(int medicationId, int timesADay, int numberOfDays, string description)
        {
            MedicationController medicineController = new MedicationController();
            Medication = medicineController.Read(medicationId);
            TimesADay = timesADay;
            DurationDays = numberOfDays;
            Quantity = timesADay * numberOfDays;
            Description = description;
        }


        public Medication Medication { get { return medication; } set { medication = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }  
        public int MedicationID { get { return medication.Id; } set { medication.Id = value; } }
        public string Description { get { return description; } set { description = value; } }  
        public string Namee {  get { return medication.Name; } set { medication.Name = value;}  }
        public int Id { get => id; set => id = value; }
        public int TimesADay { get => timesADay; set => timesADay = value; }
        public int DurationDays { get => durationDays; set => durationDays = value; }

        

        public List<String> ToCSV()
        {
            List<String> result = new List<String>();
            result.Add(id.ToString());
            result.Add(timesADay.ToString());
            result.Add(durationDays.ToString());
            result.Add(quantity.ToString());
            result.Add(description);
            result.Add(medication.Id.ToString());
            return result;
        }

        public void FromCSV(string[] values)
        {
            int i = 0;
            id = int.Parse(values[i++]);
            timesADay = int.Parse(values[i++]);
            durationDays = int.Parse (values[i++]);
            quantity = int.Parse(values[i++]);
            description = values[i++];
            Controller.MedicationController medicationController = new MedicationController();
            Medication = medicationController.Read(int.Parse(values[i++]));
        }
    }
}