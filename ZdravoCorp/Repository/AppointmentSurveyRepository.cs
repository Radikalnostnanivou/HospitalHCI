using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class AppointmentSurveyRepository : Repository<AppointmentSurvey> , IAppointmentSurveyRepository
    {
        private static AppointmentSurveyRepository instance = null;

        public AppointmentSurveyRepository()
        {
            dataBase.SetPath("..\\..\\Data\\appointmentSurveysDB.csv");
            InstantiateIDSet(GetAll());
        }

        public List<int> getAllAppointmentSurveyIds() {
            return idMap.ToList();
        }

        public List<AppointmentSurvey> GetSurveysById(List<int> ids)
        {
            List<AppointmentSurvey> surveys = GetAll();
            List<AppointmentSurvey> surveysById = new List<AppointmentSurvey>();
            foreach (AppointmentSurvey survey in surveys)
            {
               foreach (int i in ids)
                {
                    if(survey.Id == i)
                    {
                        surveysById.Add(survey);
                    }
                }
            }
            return surveysById;
        }

        public override AppointmentSurvey Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                return FindAppointmentSurveyByID(GetAll(), id);
            }
        }

        public override void Create(AppointmentSurvey element)
        {
            lock (key)
            {
                element.Id = GenerateID();
                dataBase.AppendToDB(element);
                idMap.Add(element.Id);
            }
        }

        /*
        * Method isn't needed but is requered for abstract class
        */
        public override void Update(AppointmentSurvey element)
        {
            throw new NotImplementedException();
        }

        /*
        * Method isn't needed but is requered for abstract class
        */
        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public new List<AppointmentSurvey> GetAll()
        {
            List<AppointmentSurvey> surveys = base.GetAll();
            List<Appointment> appointments = AppointmentRepository.Instance.GetAll();
            List<Doctor> doctors = DoctorRepository.Instance.GetAll();
            foreach(AppointmentSurvey survey in surveys)
            {
                foreach(Appointment appointment in appointments)
                {
                    if(survey.Appointment.Id == appointment.Id)
                    {
                        survey.Appointment = appointment;
                        foreach(Doctor doctor in doctors)
                        {
                            if(survey.Appointment.Doctor.Id == doctor.Id)
                            {
                                survey.Appointment.Doctor = doctor;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return surveys;
        }

        protected override void InstantiateIDSet(List<AppointmentSurvey> elements)
        {
            lock (key)
            {
                foreach (AppointmentSurvey element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private void CheckIfIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new Exception("AppointmentSurvey doesnt exist");
        }

        private AppointmentSurvey FindAppointmentSurveyByID(List<AppointmentSurvey> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    return elements[i];
                }
            }
            throw new Exception("AppointmentSurvey doesnt exist");
        } 

        public static AppointmentSurveyRepository Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new AppointmentSurveyRepository();
                }
                return instance ;
            }
        }
    }
}