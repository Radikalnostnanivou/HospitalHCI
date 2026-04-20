/***********************************************************************
 * Module:  Patient.cs
 * Author:  halid
 * Purpose: Definition of the Class Model.Patient
 ***********************************************************************/

using Repository;
using System;
using Controller;
using System.Collections.Generic;

namespace Model
{
    public class Patient : User, Serializable
    {
        private MedicalRecord record;
        public MedicalRecord Record { get { return record; } set { record = value; } }

        private List<MedicationType> allergens;
        public Patient(int id, string password, string username, string name, string surname, string jmbg, string email, string address, string phoneNumber, Gender gender, DateTime dateOfBirth, List<Notification> notification, List<AppointmentSurvey> survey,List<DateTime> changedOrCanceled, List<MedicationType> allergens) : base(id, password, username, name, surname, jmbg, email, address, phoneNumber, gender, dateOfBirth, notification, survey)
        {
            Allergens = allergens;
            this.changedOrCanceledAppointmnetsDates = changedOrCanceled;
            this.canLog = true;
        }
        public Patient(Patient pomocnip)
        {
            Id = pomocnip.Id;
            Password = pomocnip.Password;
            Username = pomocnip.Username;
            Name = pomocnip.Name;
            Surname = pomocnip.Surname;
            Jmbg = pomocnip.Jmbg;
            Email = pomocnip.Email;
            Address = pomocnip.Address;
            PhoneNumber = pomocnip.PhoneNumber;
            this.gender = pomocnip.gender;
            this.dateOfBirth = pomocnip.dateOfBirth;
            this.Record = pomocnip.Record;
            this.appointment = pomocnip.appointment;
            this.notification = pomocnip.notification;
            this.prescription = pomocnip.prescription;
            this.changedOrCanceledAppointmnetsDates = new List<DateTime>();
            this.canLog = true;
            Allergens = pomocnip.Allergens;
        }

        public Patient(int id)
        {
            this.id = id;
        }

        public Patient()
        {
        }

        public string Name
        { get { return this.name; } set { this.name = value; } }

        private List<Appointment> appointment = new List<Appointment>();

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

        public List<MedicationType> Allergens
        {
            get
            {
                if (allergens == null)
                    allergens = new List<MedicationType>();
                return allergens;
            }
            set
            {
                RemoveAllAllergens();
                if (value != null)
                {
                    foreach (MedicationType oMedicationType in value)
                        AddAllergen(oMedicationType);
                }
            }
        }

        public void AddAllergen(MedicationType newMedicationType)
        {
            if (newMedicationType == null)
                return;
            if (this.allergens == null)
                this.allergens = new List<MedicationType>();
            if (!this.allergens.Contains(newMedicationType))
                this.allergens.Add(newMedicationType);
        }

        public void RemoveAllergen(MedicationType oldMedicationType)
        {
            if (oldMedicationType == null)
                return;
            if (this.allergens != null)
                if (this.allergens.Contains(oldMedicationType))
                    this.allergens.Remove(oldMedicationType);
        }

        public void RemoveAllAllergens()
        {
            if (allergens != null)
                allergens.Clear();
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
        private MedicalRecord medicalRecord;
        private List<Prescription> prescription;

        /// <summary>
        /// Property for collection of Prescription
        /// </summary>
        /// <pdGenerated>Default opposite class collection property</pdGenerated>
        public List<Prescription> Prescription
        {
            get
            {
                if (prescription == null)
                    prescription = new List<Prescription>();
                return prescription;
            }
            set
            {
                RemoveAllPrescription();
                if (value != null)
                {
                    foreach (Prescription oPrescription in value)
                        AddPrescription(oPrescription);
                }
            }
        }

        /// <summary>
        /// Add a new Prescription in the collection
        /// </summary>
        /// <pdGenerated>Default Add</pdGenerated>
        public void AddPrescription(Prescription newPrescription)
        {
            if (newPrescription == null)
                return;
            if (this.prescription == null)
                this.prescription = new List<Prescription>();
            if (!this.prescription.Contains(newPrescription))
                this.prescription.Add(newPrescription);
        }

        /// <summary>
        /// Remove an existing Prescription from the collection
        /// </summary>
        /// <pdGenerated>Default Remove</pdGenerated>
        public void RemovePrescription(Prescription oldPrescription)
        {
            if (oldPrescription == null)
                return;
            if (this.prescription != null)
                if (this.prescription.Contains(oldPrescription))
                    this.prescription.Remove(oldPrescription);
        }

        /// <summary>
        /// Remove all instances of Prescription from the collection
        /// </summary>
        /// <pdGenerated>Default removeAll</pdGenerated>
        public void RemoveAllPrescription()
        {
            if (prescription != null)
                prescription.Clear();
        }

        public List<String> ToCSV()
        {
            List<String> result = new List<String>();
            result.Add(Id.ToString());
            result.Add(password);
            result.Add(username);
            result.Add(name);
            result.Add(surname);
            result.Add(jmbg);
            result.Add(email);
            result.Add(address);
            result.Add(phoneNumber);
            result.Add(gender.ToString());
            result.Add(dateOfBirth.ToString());
            

            

            int nf = 0;
            if (notification == null)
            {
                result.Add(nf.ToString());
            }
            else
            {
                result.Add(notification.Count.ToString());
                foreach (Notification n in notification)
                {
                    result.Add(n.DateCreated.ToString());
                    result.Add(n.Content);
                    result.Add(n.User.Id.ToString());
                }
            }


            /*Mozda nije dobro*/

            if(appointment == null)
            {
                result.Add(nf.ToString());
            }
            else
            {
                result.Add(appointment.Count.ToString());

                foreach(Appointment a in appointment)
                {
                    result.Add(a.Id.ToString());
                }
            }


            if (prescription == null)

            {
                result.Add(nf.ToString());
            }
            else
            {
                result.Add(prescription.Count.ToString());
                foreach (Prescription p in prescription)
                {
                    result.Add(p.Id.ToString());
                }
            }
            int i = -1;
            if (record == null) {
                result.Add(i.ToString());
            }
            else
            {
                result.Add(record.Id.ToString());
            }
            if (changedOrCanceledAppointmnetsDates == null)
            {
                result.Add(nf.ToString());
            }
            else
            {
                result.Add(changedOrCanceledAppointmnetsDates.Count.ToString());
                foreach (DateTime date in changedOrCanceledAppointmnetsDates)
                {
                    result.Add(date.ToString());
                }
            }
            result.Add(canLog.ToString());
            if (allergens == null)
            {
                result.Add(nf.ToString());
            }
            else
            {
                result.Add(allergens.Count.ToString());

                foreach (MedicationType medicationType in allergens)
                {
                    result.Add(medicationType.Id.ToString());
                }
            }
            return result;
        }

        public void FromCSV(string[] values)
        {
            int i = 0;
            id = int.Parse(values[i++]);
            password = values[i++];
            username = values[i++];
            name = values[i++];
            surname = values[i++];
            jmbg = values[i++];
            email = values[i++];
            address = values[i++];
            phoneNumber = values[i++];
            if (values[i++] == "Male")
            {
                gender = Gender.Male;
            }
            else
            {
                gender = Gender.Female;
            }
            dateOfBirth = DateTime.Parse(values[i++]);
            int count = int.Parse(values[i++]);
            /*Mozda nije dobro*/
            for (int j = 0; j < count; j++)
            {
                notification.Add(new Notification(DateTime.Parse(values[i++]), values[i++], int.Parse(values[i++])));
            }

            int count2 = int.Parse(values[i++]);
            for(int j = 0; j < count2; j++)
            {
                appointment.Add(new Appointment(int.Parse(values[i])));
                i++;
                //appointment.Add(new Appointment(int.Parse(values[i++])));
            }

            int count3 = int.Parse(values[i++]);
            PrescriptionController pc= new PrescriptionController();
            prescription = new List<Prescription>();
            for (int j = 0; j < count3; j++)
            {
                prescription.Add(pc.Read(int.Parse(values[i++])));
            }
            record = new MedicalRecord(int.Parse(values[i++]));
            int numberOfElementsInList = int.Parse(values[i++]);
            for (int j = 0; j < numberOfElementsInList; j++)
            {
                changedOrCanceledAppointmnetsDates.Add(DateTime.Parse(values[i++]));
            }
            canLog = Boolean.Parse(values[i++]);

            int numberOfAllergens = int.Parse((values[i++]));
            MedicationController medicationController = new MedicationController(); 
            for (int j = 0; j < numberOfAllergens; j++)
            {
                AddAllergen(medicationController.ReadMedicationType(int.Parse(values[i++])));
            }
        }

        private List<DateTime> changedOrCanceledAppointmnetsDates = new List<DateTime>();
        public List<DateTime> ChangedOrCanceledAppointmentsDates { get => changedOrCanceledAppointmnetsDates; set => changedOrCanceledAppointmnetsDates = value; }
        private Boolean canLog;
        public Boolean CanLog { get => canLog; set => canLog = value; }
    }
}