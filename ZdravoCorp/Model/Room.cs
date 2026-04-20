/***********************************************************************
 * Module:  Rooms.cs
 * Author:  halid
 * Purpose: Definition of the Class Model.Rooms
 ***********************************************************************/

using Repository;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Room : Serializable
    {
        private int identifier;
        private String designationCode;
        private int floor;
        private float surfaceArea;
        private bool renovating;
        private DateTime renovatedUntil;

        private List<Appointment> appointment;
        public Room(int id)
        {
            this.identifier = id;
            this.renovating = false;
        }
        /// <summary>
        /// Property for collection of Appointment
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public List<Appointment> Appointment
        {
            get
            {
                if (appointment == null)
                    appointment = new List<Appointment>();
                return appointment;
            }
            set
            {
                RemoveAllAppointment();
                if (value != null)
                {
                    foreach (Appointment oAppointment in value)
                        AddAppointment(oAppointment);
                }
            }
        }

        /// <summary>
        /// Add a new Appointment in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddAppointment(Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appointment == null)
                this.appointment = new List<Appointment>();
            if (!this.appointment.Contains(newAppointment))
                this.appointment.Add(newAppointment);
        }

        /// <summary>
        /// Remove an existing Appointment from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveAppointment(Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (this.appointment != null)
                if (this.appointment.Contains(oldAppointment))
                    this.appointment.Remove(oldAppointment);
        }

        /// <summary>
        /// Remove all instances of Appointment from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllAppointment()
        {
            if (appointment != null)
                appointment.Clear();
        }

        private List<Equipment> equipment;

        /// <summary>
        /// Property for collection of Equipment
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public List<Equipment> Equipment
        {
            get
            {
                if (equipment == null)
                    equipment = new List<Equipment>();
                return equipment;
            }
            set
            {
                RemoveAllEquipment();
                if (value != null)
                {
                    foreach (Equipment oEquipment in value)
                        AddEquipment(oEquipment);
                }
            }
        }

        /// <summary>
        /// Add a new Equipment in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddEquipment(Equipment newEquipment)
        {
            if (newEquipment == null)
                return;
            if (this.equipment == null)
                this.equipment = new List<Equipment>();
            if (!this.equipment.Contains(newEquipment))
                this.equipment.Add(newEquipment);
        }

        /// <summary>
        /// Remove an existing Equipment from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveEquipment(Equipment oldEquipment)
        {
            if (oldEquipment == null)
                return;
            if (this.equipment != null)
                if (this.equipment.Contains(oldEquipment))
                    this.equipment.Remove(oldEquipment);
        }

        public void EditEquipment(Equipment newEquipment)
        {
            if (newEquipment == null)
                return;
            if (this.equipment != null)
                foreach(Equipment oEquipment in this.equipment)
                {
                    if(oEquipment.Identifier == newEquipment.Identifier)
                    {
                        this.equipment.Remove(oEquipment);
                        this.equipment.Add(newEquipment);
                        break;
                    }
                }
        }

        /// <summary>
        /// Remove all instances of Equipment from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllEquipment()
        {
            if (equipment != null)
                equipment.Clear();
        }
        private RoomType roomType;

        /// <summary>
        /// Property for RoomType
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>
        public RoomType RoomType
        {
            get
            {
                return roomType;
            }
            set
            {
                this.roomType = value;
            }
        }
        
        private List<Medication> medication;

        /// <summary>
        /// Property for collection of Medication
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public List<Medication> Medication
        {
            get
            {
                if (medication == null)
                    medication = new List<Medication>();
                return medication;
            }
            set
            {
                RemoveAllMedication();
                if (value != null)
                {
                    foreach (Medication oMedication in value)
                        AddMedication(oMedication);
                }
            }
        }

        public int Identifier { get => identifier; set => identifier = value; }
        public string DesignationCode { get => designationCode; set => designationCode = value; }
        public float SurfaceArea { get => surfaceArea; set => surfaceArea = value; }
        public string RoomTypeString { get => RoomType.Name; set => RoomType.Name = value; }
        public bool Renovating { get => renovating; set => renovating = value; }
        public DateTime RenovatedUntil { get => renovatedUntil; set => renovatedUntil = value; }
        public int Floor { get => floor; set => floor = value; }

        /// <summary>
        /// Add a new Medication in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddMedication(Medication newMedication)
        {
            if (newMedication == null)
                return;
            if (this.medication == null)
                this.medication = new List<Medication>();
            if (!this.medication.Contains(newMedication))
                this.medication.Add(newMedication);
        }

        /// <summary>
        /// Remove an existing Medication from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemoveMedication(Medication oldMedication)
        {
            if (oldMedication == null)
                return;
            if (this.medication != null)
                if (this.medication.Contains(oldMedication))
                    this.medication.Remove(oldMedication);
        }

        /// <summary>
        /// Remove all instances of Medication from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllMedication()
        {
            if (medication != null)
                medication.Clear();
        }

        public Room(String designation, int floor, float surfaceArea, RoomType roomType, List<Appointment> appointment, List<Equipment> equipment, List<Medication> medication)
        {
            DesignationCode = designation;
            this.SurfaceArea = surfaceArea;
            this.Floor = floor;
            this.roomType = roomType;
            this.appointment = appointment;
            this.equipment = equipment;
            this.medication = medication;
            this.renovating = false;
        }

        public Room(String designation, int floor, float surfaceArea, RoomType roomType)
        {
            DesignationCode = designation;
            this.SurfaceArea = surfaceArea;
            this.Floor = floor;
            this.roomType = roomType;
            this.appointment = new List<Appointment>();
            this.equipment = new List<Equipment>();
            this.medication = new List<Medication>();
            this.renovating = false;
        }

        public Room()
        {
        }

        public Room(Room room)
        {
            this.identifier = room.identifier;
            this.designationCode = room.designationCode;
            this.Floor = room.Floor;
            this.surfaceArea = room.surfaceArea;
            this.renovating = room.renovating;
            this.renovatedUntil = room.renovatedUntil;
            Appointment = room.appointment;
            Equipment = room.equipment;
            RoomType = room.roomType;
            Medication = room.medication;
        }

        public List<String> ToCSV()
        {
            List<String> result = new List<String>();

            result.Add(Identifier.ToString());
            result.Add(DesignationCode);
            result.Add(Floor.ToString());
            result.Add(SurfaceArea.ToString());
            result.AddRange(RoomType.ToCSV());
            result.Add(renovating.ToString());
            result.Add(renovatedUntil.ToString());

            result.Add(Appointment.Count.ToString());
            foreach(Appointment it in Appointment)
            {
                result.Add(it.Id.ToString());
            }

            result.Add(Equipment.Count.ToString());
            foreach (Equipment it in Equipment)
            {
                result.Add(it.EquipmentType.Identifier.ToString());
                result.Add(it.Count.ToString());
                result.Add(it.Actual_count.ToString());
            }

            result.Add(Medication.Count.ToString());
            foreach (Medication it in Medication)
            {
                result.Add(it.MedicationType.Id.ToString());
                result.Add(it.Count.ToString());
            }

            return result;
        }

        public void FromCSV(string[] values)
        {
            int i = 0;
            Identifier = int.Parse(values[i++]);
            DesignationCode = values[i++];
            Floor = int.Parse(values[i++]);
            SurfaceArea = float.Parse(values[i++]);
            roomType = new RoomType(values[i++]);
            renovating = Boolean.Parse(values[i++]);
            renovatedUntil = DateTime.Parse(values[i++]);

            int count = int.Parse(values[i++]);
            appointment = new List<Appointment>();

            //Multiplies by 1 because it needs to scan 1 item to get Appointment
            int temp = i + count * 1;
            for (; i < temp; i++)
            {
                appointment.Add(new Appointment(int.Parse(values[i])));
            }

            count = int.Parse(values[i++]);
            equipment = new List<Equipment>();
            //Multiplies by 3 because it needs to scan 3 items to get Equipment
            temp = i + count * 3;
            for (; i < temp; i++)
            {
                equipment.Add(new Equipment(int.Parse(values[i++]), int.Parse(values[i++]), int.Parse(values[i])));
            }

            count = int.Parse(values[i++]);
            medication = new List<Medication>();
            //Multiplies by 2 because it needs to scan 2 items to get Medication
            temp = i + count * 2;
            for (; i < temp; i++)
            {
                medication.Add(new Medication(int.Parse(values[i++]), int.Parse(values[i])));
            }
        }
    }
}