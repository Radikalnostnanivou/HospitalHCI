// File:    AppointmenService.cs
// Author:  Dusko
// Created: Sunday, 10 April 2022 22:42:17
// Purpose: Definition of Class AppointmenService

using Model;
using Repository;
using System;
using System.Collections.Generic;
using ZdravoCorp.Repository.Interfaces;
using ZdravoCorp.Service.Interfaces;

namespace Service
{
    public class AppointmentService : ICrud<Appointment>, IAppointmentService
    {
        private const int MAX_SUGGESTIONS = 30;
        private const int MAX_ITERATIONS = 10;
        private int numOfIterations = 0;
        private static AppointmentService instance = null;
        public IAppointmentRepository repository = AppointmentRepository.Instance;
        List<Appointment> appointments = new List<Appointment>();
        
        public void Create(Appointment newAppointment)
        {
            repository.Create(newAppointment);
        }

        public Appointment Read(int appointment)
        {
            return repository.Read(appointment);
        }

        public void Update(Appointment appointment)
        {
            repository.Update(appointment);
        }

        public void Delete(int appointment)
        {
            repository.Delete(appointment);
        }

        public List<Appointment> GetAll()
        {
            return repository.GetAll();
        }

        public AppointmentService()
        {

        }

        public static AppointmentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppointmentService();
                }
                return instance;
            }
        }
        
        public List<Appointment> SuggestAppointments(WantedAppointment wantedAppointment)
        {
            if (wantedAppointment.FirstTime)
            {
                numOfIterations = 0;
                appointments = new List<Appointment>();
                wantedAppointment.FirstTime = false;
            }
            if (wantedAppointment.Priority)
            {
                FindAppointmentsWithDoctorPriority(wantedAppointment);
            }
            else
            {
                FindAppointmentsWithDatePriority(wantedAppointment);
            }
            return appointments;
        }

        private void FindAppointmentsWithDoctorPriority(WantedAppointment wantedAppointment)
        {
            DateTime correctStart = SetStartTime(wantedAppointment);
            DateTime correctEnd = correctStart.Date + new TimeSpan(correctStart.Hour, correctStart.Minute + 45, correctStart.Second);
            SuggestAppointmentsForOneWorkingDay(wantedAppointment, correctStart);
            if (appointments.Count < MAX_SUGGESTIONS && numOfIterations < MAX_ITERATIONS) 
            {
                numOfIterations++;
                SetDateTimeForNextDay(wantedAppointment);
                SuggestAppointments(wantedAppointment);
            }
        }

        private void FindAppointmentsWithDatePriority(WantedAppointment wantedAppointment)
        {
            DateTime resetStart = wantedAppointment.Start;
            GoThroughAllDoctors(wantedAppointment, resetStart);
            if (appointments.Count < MAX_SUGGESTIONS && numOfIterations < MAX_ITERATIONS)
            {
                numOfIterations++;
                SetDateTimeForNextDay(wantedAppointment);
                
                SuggestAppointments(wantedAppointment);
            }

        }

        private void GoThroughAllDoctors(WantedAppointment wantedAppointment, DateTime resetStart)
        {
            List<Doctor> doctors = DoctorService.Instance.GetAll();
            foreach (Doctor d in doctors)
            {
                DateTime correctStart = SetStartTimeForDatePriority(resetStart, d);
                DateTime correctEnd = correctStart.Date + new TimeSpan(correctStart.Hour,
                    correctStart.Minute + 45, correctStart.Second);
                SuggestAppointmentsForOneWorkingDay(wantedAppointment, correctStart, d);
            }
        }

        private void SuggestAppointmentsForOneWorkingDay(WantedAppointment wantedAppointment,
            DateTime correctStart, Doctor d = null)
        {
            d = d == null ? wantedAppointment.Doctor : d;
            while ((correctStart.TimeOfDay >= d.workStartTime.TimeOfDay) 
                && (correctStart.AddMinutes(45).TimeOfDay <= d.workEndTime.TimeOfDay))
            {
                if (DoctorService.Instance.IsDoctorFree(wantedAppointment.Doctor.Id, 
                    correctStart, correctStart.AddMinutes(45)))
                {
                    Room room = RoomService.Instance.findFreeRoom(correctStart, correctStart.AddMinutes(45));
                    if (room != null)
                    {
                        wantedAppointment.Doctor = d;
                        AddAppointmentToSuggestedAppointments(correctStart, room, wantedAppointment);
                    }
                }
                correctStart = SetTimeForNextAppointment(correctStart, correctStart.AddMinutes(45));
            }
        }

        public DateTime SetStartTimeForDatePriority(DateTime resetStart, Doctor doctor)
        {
            TimeSpan ts = new TimeSpan(doctor.workStartTime.Hour, doctor.workStartTime.Minute,
                doctor.workStartTime.Second);
            DateTime correctStartTime = resetStart.Date + ts;
            return correctStartTime;
        }

        public DateTime SetStartTime(WantedAppointment wantedAppointment)
        {
            DateTime correctStartTime = wantedAppointment.Start;
            TimeSpan ts = new TimeSpan(wantedAppointment.Doctor.workStartTime.Hour,
                wantedAppointment.Doctor.workStartTime.Minute, wantedAppointment.Doctor.workStartTime.Second);
            correctStartTime = correctStartTime.Date + ts;
            return correctStartTime;
        }

        public void AddAppointmentToSuggestedAppointments(DateTime start, Room room, WantedAppointment wantedAppointment)
        {
            Appointment appointment = new Appointment();
            appointment.Doctor = wantedAppointment.Doctor;
            appointment.StartDate = start;
            appointment.EndDate = start.AddMinutes(45);
            appointment.Room = room;
            appointment.Patient = wantedAppointment.Patient;
            appointments.Add(appointment);
        }

        public DateTime SetTimeForNextAppointment(DateTime start, DateTime end)
        {
            TimeSpan ts2 = new TimeSpan(start.Hour, start.Minute + 45, start.Second);
            TimeSpan ts3 = new TimeSpan(end.Hour, end.Minute + 45, end.Second);
            start = start.Date + ts2;
            end = end.Date + ts3;
            return start;
        }

        public void SetDateTimeForNextDay(WantedAppointment wantedAppointment)
        {
            TimeSpan ts2 = new TimeSpan(1, 0, 0, 0);
            TimeSpan ts3 = new TimeSpan(1, 0, 0, 0);
            wantedAppointment.Start = wantedAppointment.Start.Date + ts2;
            wantedAppointment.End = wantedAppointment.End.Date + ts3;
        }

        public List<Appointment> doctorsAppointments(int id)
        {
            List<Appointment> result = new List<Appointment>();
            foreach (Appointment app in GetAll())
            {
                foreach (Doctor doc in DoctorService.Instance.GetAll())
                {
                    if (doc.Id == id)
                    {
                        result.Add(app);
                    }
                }
            }
            return result;
        }
        public List<Appointment> GetFutureAppointments()
        {
            List<Appointment> appointments = GetAll();
            List<Appointment> futureAppointments = new List<Appointment>();
            foreach(Appointment appointment in appointments)
            {
                if(appointment.StartDate >= DateTime.Now)
                {
                    futureAppointments.Add(appointment);
                }
            }
            return futureAppointments;
        }
        public List<Appointment> GetPastAppointments()
        {
            List<Appointment> appointments = GetAll();
            List<Appointment> pastAppointments = new List<Appointment>();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.EndDate <= DateTime.Now)
                {
                    pastAppointments.Add(appointment);
                }
            }
            return pastAppointments;
        }

        public Boolean IsTroll(Appointment appointment)
        {
            Patient patient = PatientRepository.Instance.Read(appointment.Patient.Id);
            if(patient.ChangedOrCanceledAppointmentsDates == null)
            {
                patient.ChangedOrCanceledAppointmentsDates = new List<DateTime>();
            }
            if(patient.ChangedOrCanceledAppointmentsDates.Count >= 5)
            {
                patient.CanLog = false;
                PatientRepository.Instance.Update(patient);
            }
            return !patient.CanLog;
        }
        
        public List<Appointment> GetFutureAppointmentsForPatient(Patient patient)
        {
            List<Appointment> allFutureAppointments = GetFutureAppointments();
            List<Appointment> futureAppointmentsForPatient = new List<Appointment>();
            foreach(Appointment appointment in allFutureAppointments)
            {
                if(appointment.Patient.Id == patient.Id)
                {
                    futureAppointmentsForPatient.Add(appointment);
                }
            }
            return futureAppointmentsForPatient;
        }

        public List<Appointment> GetPastAppointmentsForPatient(Patient patient)
        {
            List<Appointment> allPastAppointments = GetPastAppointments();
            List<Appointment> pastAppointmentsForPatient = new List<Appointment>();
            foreach (Appointment appointment in allPastAppointments)
            {
                if (appointment.Patient.Id == patient.Id)
                {
                    pastAppointmentsForPatient.Add(appointment);
                }
            }
            return pastAppointmentsForPatient;
        }
    }
}