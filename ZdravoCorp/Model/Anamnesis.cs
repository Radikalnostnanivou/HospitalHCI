using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Model
{
    public class Anamnesis : Serializable
    {
        private int id;
        private Appointment appointment;
        private String appointmentType;
        private String diagnosis;
        private Prescription prescription;
        private String note;
        public Anamnesis()
        {
        }

        public Anamnesis(Appointment appointment, string appointmentType, string diagnosis, Prescription prescription, String note)
        {
            this.appointment = appointment;
            this.appointmentType = appointmentType;
            this.diagnosis = diagnosis;
            this.prescription = prescription;
            this.note = note;
        }

        public int Id { get => id; set => id = value; }
        public Appointment Appointment { get => appointment; set => appointment = value; }
        public String AppointmentType { get => appointmentType; set => appointmentType = value; }
        public String Diagnosis { get => diagnosis; set => diagnosis = value; }
        public Prescription Prescription { get => prescription; set => prescription = value; }
        public String Note { get => note; set => note = value; }
        public List<String> ToCSV()
        {
        
            List<String> result = new List<String>();
            result.Add(id.ToString());
            result.Add(appointment.Id.ToString());
            result.Add(appointmentType);
            result.Add(diagnosis);
            result.Add(prescription.Id.ToString());
            result.Add(note);
            return result;
        }

        public void FromCSV(string[] values)
        {
        
            int i = 0;
            id = int.Parse(values[i++]);
            Appointment = new Appointment(int.Parse(values[i++]));
            appointmentType = values[i++];
            diagnosis = values[i++];
            prescription = new Prescription(int.Parse(values[i++]));
            note = values[i++];
        }
    }
}
