using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using ZdravoCorp.View.Core;

namespace Model
{
    public class NewMedicationRequest : ObservableObject, Serializable
    {
        private int id;
        private MedicationType medicationType;
        private bool change;
        private Status status;
        private String comment;

        public NewMedicationRequest()
        {
        }

        public NewMedicationRequest(NewMedicationRequest request)
        {
            Id = request.Id;
            MedicationType = request.MedicationType;
            Status = request.Status;
            Comment = request.Comment;
            Change = request.Change;
        }

        public NewMedicationRequest(NewMedicationRequest request, Status status, string comment, bool change)
        {
            Id = request.Id;
            MedicationType = request.MedicationType;
            Status = status;
            Comment = comment;
            Change = change;
        }

        public NewMedicationRequest(MedicationType medicationType, Status status, string comment, bool change)
        {
            this.medicationType = medicationType;
            this.status = status;
            this.comment = comment;
            this.change = change;
        }

        public int Id 
        {
            get => id;
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => medicationType.Name;
            set
            {
                if (value != medicationType.Name)
                {
                    medicationType.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Manufacturer
        {
            get => medicationType.Manufacturer;
            set
            {
                if (value != medicationType.Manufacturer)
                {
                    medicationType.Manufacturer = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get => medicationType.Description;
            set
            {
                if (value != medicationType.Description)
                {
                    medicationType.Manufacturer = value;
                    OnPropertyChanged();
                }
            }
        }
        public MedicationType MedicationType
        {
            get => medicationType;
            set
            {
                if (value != medicationType)
                {
                    medicationType = value;
                    OnPropertyChanged();
                }
            }
        }
        public Status Status
        {
            get => status;
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged();
                }
            }
        }

        public String Comment
        {
            get => comment;
            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Change
        {
            get => change;
            set
            {
                if (value != change)
                {
                    change = value;
                    OnPropertyChanged();
                }
            }
        }

        private string[] ReadInfo(string[] values)
        {
            int i = 0;
            Id = int.Parse(values[i++]);
            Change = bool.Parse(values[i++]);
            Status = (Status)Enum.Parse(typeof(Status), values[i++]);
            Comment = values[i++];
            return values.Skip(i).ToArray();
        }
        public void FromCSV(string[] values)
        {
            values = ReadInfo(values);
            MedicationType = new MedicationType();
            MedicationType.FromCSV(values);
        }

        public List<String> ToCSV()
        {
            List<String> result = new List<String>();
            result.Add(id.ToString());
            result.Add(Change.ToString());
            result.Add(Status.ToString());
            result.Add(Comment);
            result.AddRange(MedicationType.ToCSV());
            return result;
        }
    }
}
