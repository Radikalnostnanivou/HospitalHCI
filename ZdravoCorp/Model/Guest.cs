using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using Repository;

namespace Model
{
    public class Guest: Serializable
    {
        private int id;
        private string name;
        private string surname;
        private BloodType bloodType;

        public Guest()
        {
        }

        public Guest(int id, string name, string surname, BloodType bloodType)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.bloodType = bloodType;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public BloodType BloodType { get => bloodType; set => bloodType = value; }
        public string Surname { get => surname; set => surname = value; }

        public List<String> ToCSV()
        {
            CultureInfo dateTimeFormat = new CultureInfo("en-GB");
            List<String> result = new List<String>();
            result.Add(Id.ToString());
            result.Add(Name);
            result.Add(Surname);
            result.Add(bloodType.ToString());
            return result;
        }

        public void FromCSV(string[] values)
        {
            CultureInfo dateTimeFormat = new CultureInfo("en-GB");
            int i = 0;
            Id = int.Parse(values[i++]);
            Name = values[i++];
            Surname = values[i++];
            switch (values[i++]) {
                case "aPlus":
                    bloodType = BloodType.aPlus;
                    break;
                case "aMinus":
                    bloodType = BloodType.aMinus;
                    break;
                case "bPlus":
                    bloodType = BloodType.bPlus;
                    break;
                case "bMinus":
                    bloodType = BloodType.bMinus;
                    break;
                case "aBPlus":
                    bloodType = BloodType.aBPlus;
                    break;
                case "aBMinus":
                    bloodType = BloodType.aBMinus;
                    break;
                case "oPlus":
                    bloodType = BloodType.oPlus;
                    break;
                case "oMinus":
                    bloodType = BloodType.oMinus;
                    break;
                default:
                    break;

            }
        }
    }
}
