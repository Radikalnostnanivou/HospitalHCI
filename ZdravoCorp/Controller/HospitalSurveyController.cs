// File:    AppointmenService.cs
// Author:  Dusko
// Created: Sunday, 10 April 2022 22:42:17
// Purpose: Definition of Class AppointmenService

using Model;
using Service;
using System;
using System.Collections.Generic;
using Repository;
using ZdravoCorp.Service.Interfaces;


namespace Controller
{
    public class HospitalSurveyController : ICrud<HospitalSurvey>
    {
        public IHospitalSurveyService service = HospitalSurveyService.Instance;       
        public void Create(HospitalSurvey newSurvey)
        {
            service.Create(newSurvey);
        }

        public HospitalSurvey Read(int id)
        {
            return service.Read(id);
        }

        public void Update(HospitalSurvey survey)
        {
            service.Update(survey);
        }

        public void Delete(int id)
        {
            service.Delete(id);
        }

        public List<HospitalSurvey> GetAll()
        {
            return service.GetAll();
        }
        public List<int> getAllHospitalSurveyIds()
        {
            return service.getAllHospitalSurveyIds();
        }

        public string GetResults(DateTime start, DateTime end)
        {
            return service.GetResults(start, end);
        }
    }
}