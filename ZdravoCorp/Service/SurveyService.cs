using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.Service.Interfaces;

namespace Service
{
    public class SurveyService : ICrud<Survey> , ISurveyService
    {
        private static SurveyService instance = null;
        public void Create(Survey element)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Survey> GetAll()
        {
            int i = 0;
            List<Survey> surveys = new List<Survey>() { new Survey(i++, "Hospital", SurveyEnum.Hospital) };
            List<Doctor> doctors = DoctorService.Instance.GetAll();
            foreach (Doctor doctor in doctors)
            {
                surveys.Add(new Survey(doctor.Id, doctor.nameSurname, SurveyEnum.Doctor));
            }
            return surveys;
        }

        public Survey Read(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Survey element)
        {
            throw new NotImplementedException();
        }

        public SurveyService()
        {

        }

        public static SurveyService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SurveyService();
                }
                return instance;
            }
        }
    }
}
