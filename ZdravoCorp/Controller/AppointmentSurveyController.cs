// File:    AppointmenService.cs
// Author:  Dusko
// Created: Sunday, 10 April 2022 22:42:17
// Purpose: Definition of Class AppointmenService

using Model;
using Repository;
using System;
using System.Collections.Generic;
using Service;
using Repository;
using ZdravoCorp.Service.Interfaces;

namespace Controller
{
    public class AppointmentSurveyController : ICrud<AppointmentSurvey>
    {
        public IAppointmentSurveyService service = AppointmentSurveyService.Instance;
        public void Create(AppointmentSurvey newSurvey)
        {
            service.Create(newSurvey);
        }

        public AppointmentSurvey Read(int id)
        {
            return service.Read(id);
        }

        public void Update(AppointmentSurvey survey)
        {
            service.Update(survey);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }

        public List<AppointmentSurvey> GetAll()
        {
            return service.GetAll();
        }
       
        public Boolean DoneSurvey(Appointment appointment)
        {
            return service.DoneSurvey(appointment);
        }

        public string GetResultsForDoctor(Doctor doctor, DateTime start, DateTime end)
        {
            return service.GetResultsForDoctor(doctor, start, end);
        }
    }
}