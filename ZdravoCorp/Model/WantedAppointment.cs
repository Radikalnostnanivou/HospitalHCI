using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class WantedAppointment
    {
        private Doctor doctor;
        private DateTime start;
        private DateTime end;
        private bool priority;
        private bool firstTime;
        private Patient patient;

        public WantedAppointment(Doctor doctor, DateTime start, DateTime end, bool priority, bool firstTime, Patient patient)
        {
            this.doctor = doctor;
            this.start = start;
            this.end = end;
            this.priority = priority;
            this.firstTime = firstTime;
            this.patient = patient;
        }
        public Doctor Doctor { get => doctor; set => doctor = value; }
        public DateTime Start { get => start; set => start = value; }   
        public DateTime End { get => end; set => end = value; }
        public bool Priority { get => priority; set => priority = value;}
        public bool FirstTime { get => firstTime; set => firstTime = value; }
        public Patient Patient { get => patient; set => patient = value; }
    }
}
