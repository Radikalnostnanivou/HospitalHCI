using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoCorp.Repository.Interfaces;

namespace Repository
{
    public class HospitalSurveyRepository : Repository<HospitalSurvey> , IHospitalSurveyRepository
    {
        private static HospitalSurveyRepository instance = null;

        public HospitalSurveyRepository()
        {
            dataBase.SetPath("..\\..\\Data\\hospitalSurveysDB.csv");
            InstantiateIDSet(GetAll());
        }

        public List<int> getAllHospitalSurveyIds() {
            return idMap.ToList();
        }

        public List<HospitalSurvey> GetSurveysById(List<int> id)
        {
            List<HospitalSurvey> surveys = GetAll();
            List<HospitalSurvey> surveyById = new List<HospitalSurvey>();
            foreach (HospitalSurvey survey in surveys)
            {
               foreach (int i in id)
                {
                    if(survey.Id == i)
                    {
                        surveyById.Add(survey);
                    }
                }
            }
            return surveyById;
        }

        public override HospitalSurvey Read(int id)
        {
            lock (key)
            {
                CheckIfIDExists(id);
                return FindHospitalSurveyByID(GetAll(), id);
            }
        }

        public override void Create(HospitalSurvey element)
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
        public override void Update(HospitalSurvey element)
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

        protected override void InstantiateIDSet(List<HospitalSurvey> elements)
        {
            lock (key)
            {
                foreach (HospitalSurvey element in elements)
                {
                    idMap.Add(element.Id);
                }
            }
        }

        private HospitalSurvey FindHospitalSurveyByID(List<HospitalSurvey> elements, int id)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].Id == id)
                {
                    return elements[i];
                }
            }
            throw new Exception("HospitalSurvey doesnt exist");
        }

        private void CheckIfIDExists(int id)
        {
            if (!idMap.Contains(id))
                throw new Exception("HospitalSurvey doesnt exist");
        }

        public static HospitalSurveyRepository Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new HospitalSurveyRepository();
                }
                return instance ;
            }
        }
    }
}