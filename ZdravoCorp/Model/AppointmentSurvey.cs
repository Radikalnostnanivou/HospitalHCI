using Repository;
using System;
using System.Collections.Generic;
using ZdravoCorp.View.Core;
using System.ComponentModel;


namespace Model
{
    public class AppointmentSurvey : ObservableObject ,Serializable
    {
        private int id;
        private DateTime issued;
        private int profesionalism;
        private int kindness;
        private int comfort;
        private int tidiness;
        private int waitingTime;
        private int roomComfort;
        private int overallExperience;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private Appointment appointment;
        public AppointmentSurvey()
        {
        }

        public AppointmentSurvey(int profesionalism, int kindness, int comfort, int tidiness, int waitingTime, int roomComfort, int overallExperience, Appointment appointment)
        {
            this.Issued = DateTime.Now;
            this.profesionalism = profesionalism;
            this.kindness = kindness;
            this.comfort = comfort;
            this.tidiness = tidiness;
            this.waitingTime = waitingTime;
            this.roomComfort = roomComfort;
            this.overallExperience = overallExperience;
            this.appointment = appointment;
        }

        public int Id { get => id; set => id = value; }
         public Appointment Appointment { get => appointment; set => appointment = value; }

        public List<String> ToCSV()
        {
            List<String> result = new List<String>();
            result.Add(id.ToString());
            result.Add(Issued.ToString());
            result.Add(profesionalism.ToString());
            result.Add(kindness.ToString());
            result.Add(comfort.ToString());
            result.Add(tidiness.ToString());
            result.Add(waitingTime.ToString());
            result.Add(roomComfort.ToString());
            result.Add(overallExperience.ToString());
            result.Add(appointment.Id.ToString());
            return result;
        }

        public void FromCSV(string[] values)
        {
            int i = 0;
            id = int.Parse(values[i++]);
            Issued = DateTime.Parse(values[i++]);
            profesionalism = int.Parse(values[i++]);
            kindness = int.Parse(values[i++]);
            comfort = int.Parse(values[i++]);
            tidiness = int.Parse(values[i++]);
            waitingTime = int.Parse(values[i++]);
            roomComfort = int.Parse(values[i++]);
            overallExperience = int.Parse(values[i++]);
            appointment = new Appointment(int.Parse(values[i++]));
        }

        public int Profesionalism
        {
            get => profesionalism;
            set
            {
                if (value != profesionalism)
                {
                    profesionalism = value;
                    OnPropertyChanged("Profesionalism");
                }
            }
        }
        public int Kindness
        {
            get => kindness;
            set
            {
                if (value != kindness)
                {
                    kindness = value;
                    OnPropertyChanged("Kindness");
                }
            }
        }
        public int Comfort
        {
            get => comfort;
            set
            {
                if (value != comfort)
                {
                    comfort = value;
                    OnPropertyChanged("Comfort");
                }
            }
        }
        public int Tidiness
        {
            get => tidiness;
            set
            {
                if (value != tidiness)
                {
                    tidiness = value;
                    OnPropertyChanged("Tidiness");
                }
            }
        }
        public int WaitingTime
        {
            get => waitingTime;
            set
            {
                if (value != waitingTime)
                {
                    waitingTime = value;
                    OnPropertyChanged("WaitingTime");
                }
            }
        }
        public int RoomComfort
        {
            get => roomComfort;
            set
            {
                if (value != roomComfort)
                {
                    roomComfort = value;
                    OnPropertyChanged("RoomComfort");
                }
            }
        }

        public int OverallExperience
        {
            get => overallExperience;
            set
            {
                if (value != overallExperience)
                {
                    overallExperience = value;
                    OnPropertyChanged("OverallExperience");
                }
            }
        }

        public List<int> ratings { get => new List<int> { profesionalism, comfort, tidiness, kindness, overallExperience }; }

        public static List<string> ratingsLabel { get => new List<string> { "Profesionalism", "Comfort", "Tidiness", "Kindness", "OverallExperience" }; }

        public static List<string> ratingRangeLabels { get => new List<string> { "Fives", "Fours", "Threes", "Twos", "Ones" }; }
        public DateTime Issued { get => issued; set => issued = value; }
    }
}