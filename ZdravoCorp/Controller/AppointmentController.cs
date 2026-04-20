// File:    AppointmentController.cs
// Author:  Dusko
// Created: Sunday, 10 April 2022 22:33:59
// Purpose: Definition of Class AppointmentController

using Model;
using System;
using Service;
using System.Collections.Generic;
using Repository;
using ZdravoCorp.Exceptions;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class AppointmentController : ICrud<Appointment>
    {
        public IAppointmentService service = AppointmentService.Instance;
        public void Create(Appointment newAppointment)
        {
            List<Appointment> appointmentList = GetAll();
            foreach (Appointment tempAppointment in appointmentList)
            {
                if(CheckAppointmentsDates(tempAppointment,newAppointment) && CheckOtherAppointmenstInformations(tempAppointment,newAppointment))
                {
                    throw new LocalisedException("Appointment cannot be created,change input data!");
                }
            }
            service.Create(newAppointment);
        }

        public Appointment Read(int appointment)
        {
            return service.Read(appointment);
        }

        public void Update(Appointment appointment)
        {
            service.Update(appointment);
        }

        public void Delete(int appointment)
        {
            service.Delete(appointment);
        }

        public List<Appointment> GetAll()
        {
            return service.GetAll();
        }

        public List<Appointment> GetFutureAppointments()
        {
            return service.GetFutureAppointments();
        }
        public List<Appointment> GetPastAppointments()
        {
            return service.GetPastAppointments();
        }

        public Boolean IsTroll(Appointment appointment)
        {
            return service.IsTroll(appointment);
        }
        public List<Appointment> GetFutureAppointmentsForPatient(Patient patient)
        {
            return service.GetFutureAppointmentsForPatient(patient);
        }
        public List<Appointment> GetPastAppointmentsForPatient(Patient patient)
        {
            return service.GetPastAppointmentsForPatient(patient);
        }
        public List<Appointment> SuggestAppointments(WantedAppointment wantedAppointment)
        {
            return service.SuggestAppointments(wantedAppointment);
        }

        public Boolean CheckDoctorAppointments(Doctor currentDoctor,DateTime vacationStartDate, DateTime vacationEndDate)
        {
            List<Appointment> appointments = GetAll();
            foreach (Appointment appointment in appointments)
            {
                if (appointment.DoctorID == currentDoctor.Id)
                {
                    if (((vacationStartDate.CompareTo(appointment.startDate.Date) < 0)) && (vacationEndDate.CompareTo(appointment.EndDate.Date) > 0))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private Boolean CheckAppointmentsDates(Appointment appointmentOne, Appointment appointmentTwo)
        {
            if ((DateTime.Compare(appointmentOne.StartDate, appointmentTwo.StartDate) < 0 && DateTime.Compare(appointmentOne.EndDate, appointmentTwo.EndDate) > 0)
               || (DateTime.Compare(appointmentOne.StartDate, appointmentTwo.endDate) < 0 && DateTime.Compare(appointmentOne.EndDate, appointmentTwo.StartDate) > 0)
                   || (DateTime.Compare(appointmentOne.StartDate, appointmentTwo.StartDate) < 0 && DateTime.Compare(appointmentOne.EndDate, appointmentTwo.EndDate) > 0)
                       || (DateTime.Compare(appointmentOne.StartDate, appointmentTwo.StartDate) > 0 && DateTime.Compare(appointmentOne.EndDate, appointmentTwo.EndDate) < 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public Boolean CheckOtherAppointmenstInformations(Appointment appointmentOne, Appointment appointmentTwo)
        {
            if (appointmentOne.DoctorID == appointmentTwo.DoctorID || appointmentTwo.RoomID == appointmentOne.RoomID || appointmentTwo.PatientID == appointmentOne.PatientID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}