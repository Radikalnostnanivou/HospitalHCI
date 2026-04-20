// File:    Comments.cs
// Author:  halid
// Created: Friday, 15 April 2022 01:31:44
// Purpose: Definition of Class Comments

using Repository;
using System;
using System.Collections.Generic;

namespace Model
{
    public class Report : Serializable
    {
        private String comment;

        private int id;

        private Appointment appointment;

        private Doctor doctor;
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public String Comment
        {
            get { return this.comment; }
            set { this.comment = value; }
        }

        public Appointment Appointment
        {
            get { return this.appointment; }
            set { this.appointment = value; }
        }

        public Doctor Doctor
        {
            get { return this.doctor; }
            set { this.doctor = value; }
        }

        public string DoctorID
        {
            get
            {
                return this.doctor.Id.ToString();
            }
        }

        public string AppointmentID
        {
            get
            {
                return Appointment.Id.ToString();
            }
        }

        public DateTime AppointmentDate
        {
            get
            {
                return appointment.StartDate.Date;
            }
        }
        public Report(Appointment app, string kom, Doctor doktor)
        {
            Appointment = app;
            Comment = kom;
            Doctor = doktor;
        }

        public Report() { }


        /// <summary>
        /// Property for Doctor
        /// </summary>
        /// <pdGenerated>Default opposite class property</pdGenerated>

        public void FromCSV(string[] values)
        {
            int i = 0;
            Id = Int32.Parse(values[i++]);
            Comment = values[i++];
            Controller.DoctorController dc = new Controller.DoctorController();
            Doctor = dc.Read(Int32.Parse(values[i++]));
            Controller.AppointmentController ac = new Controller.AppointmentController();
            Appointment = ac.Read(Int32.Parse(values[i++]));
        }

        public List<String> ToCSV()
        {
            List<String> result = new List<String>();
            result.Add(Id.ToString());
            result.Add(Comment);
            result.Add(Doctor.Id.ToString());
            result.Add(Appointment.Id.ToString());
            return result;
        }
    }
}