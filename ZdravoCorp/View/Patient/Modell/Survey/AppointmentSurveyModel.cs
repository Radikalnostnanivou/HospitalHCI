using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Core;
namespace ZdravoCorp.View.Patient.Modell
{
    public class AppointmentSurveyModel: ObservableObject
    {
        private int id;
        private int profesionalism;
        private int kindness;
        private int comfort;
        private int tidiness;
        private int waitingTime;
        private int roomComfort;
        private int overallExperience;

        public AppointmentSurveyModel()
        {
        }

        public AppointmentSurveyModel(int profesionalism, int kindness, int comfort, int tidiness, int waitingTime, int roomComfort, int overallExperience)
        {
            this.profesionalism = profesionalism;
            this.kindness = kindness;
            this.comfort = comfort;
            this.tidiness = tidiness;
            this.waitingTime = waitingTime;
            this.roomComfort = roomComfort;
            this.overallExperience = overallExperience;
        }

        public int Profesionalism
        {
            get => profesionalism;
            set
            {
                if(value != profesionalism)
                {
                    profesionalism = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }
    }
}
