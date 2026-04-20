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
    public class AppointmentSurveyService : ICrud<AppointmentSurvey> , IAppointmentSurveyService
    {
        private static AppointmentSurveyService instance = null;

        public IAppointmentSurveyRepository repository = AppointmentSurveyRepository.Instance;
        
        public void Create(AppointmentSurvey newSurvey)
        {
            repository.Create(newSurvey);
        }

        public AppointmentSurvey Read(int id)
        {
            return repository.Read(id);
        }

        public void Update(AppointmentSurvey survey)
        {
            repository.Update(survey);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public List<AppointmentSurvey> GetAll()
        {
            return repository.GetAll();
        }
        public List<int> getAllAppointmentSurveyIds()
        {
            return repository.getAllAppointmentSurveyIds();
        }

        public string GetResultsForDoctor(Doctor doctor, DateTime start, DateTime end)
        {
            string result = doctor.nameSurname + ":\n\t";
            int count = AppointmentSurvey.ratingsLabel.Count;
            List<AppointmentSurvey> surveysTemp = FindSurveysForDoctor(GetAll(),doctor.Id);
            List<AppointmentSurvey> surveys = new List<AppointmentSurvey>();
            foreach(AppointmentSurvey survey in surveysTemp)
            {
                if(survey.Issued > start && survey.Issued < end)
                {
                    surveys.Add(survey);
                }
            }
            for (int i = 0; i < count; i++)
            {
                List<int> ratings = GetRatingsForSpecificRating(surveys, i);
                result += AppointmentSurvey.ratingsLabel[i] + ":\n\t\t";
                result += "Score: " + GetAvgOfRatings(ratings) + "\n\n\t\t";
                result = CreateTextOfAmmountOfRatings(result, ratings);
            }
            return result;
        }

        private List<AppointmentSurvey> FindSurveysForDoctor(List<AppointmentSurvey> surveys, int id)
        {
            List<AppointmentSurvey> result = new List<AppointmentSurvey>();
            foreach(AppointmentSurvey survey in surveys)
            {
                if(survey.Appointment.Doctor.Id == id)
                {
                    result.Add(survey);
                }
            }
            return result;
        }

        private string CreateTextOfAmmountOfRatings(string result, List<int> ratings)
        {
            for (int i = 0; i < AppointmentSurvey.ratingRangeLabels.Count; i++)
            {
                result += AppointmentSurvey.ratingRangeLabels[i] + ": " + GetCountOfRating(ratings, 5 - i) + "\n\t\t";
            }
            result += "\n\t";
            return result;
        }

        private List<int> GetRatingsForSpecificRating(List<AppointmentSurvey> surveys, int rating)
        {
            List<int> ratings = new List<int>();
            foreach (AppointmentSurvey survey in surveys)
            {
                ratings.Add(survey.ratings[rating]);
            }
            return ratings;
        }

        private float GetCountOfRating(List<int> ratings, int rating)
        {
            int count = 0;
            foreach (int it in ratings)
            {
                if (it == rating)
                {
                    count++;
                }
            }
            return count;
        }

        private float GetAvgOfRatings(List<int> ratings)
        {
            int count = 0;
            float sum = 0;
            foreach (int rating in ratings)
            {
                count++;
                sum += rating;
            }
            return sum / count;
        }

        public AppointmentSurveyService()
        {

        }

        public static AppointmentSurveyService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppointmentSurveyService();
                }
                return instance;
            }
        }
        public Boolean DoneSurvey(Appointment appointment)
        {
            Boolean done = false;
            if (GetAllAppointmentsIds().Contains(appointment.Id))
            {
                done = true;
            }
            return done;
        }
        public List<int> GetAllAppointmentsIds()
        {
            List<int> ids = new List<int>();
            foreach(AppointmentSurvey appointmentSurvey in GetAll())
            {
                ids.Add(appointmentSurvey.Appointment.Id);
            }
            return ids;
        }
    }
}