using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoCorp.View.Core;
using System.Collections.ObjectModel;
using ZdravoCorp.View.Patient.View.Survey;
using ZdravoCorp.View.Patient.Modell;
using Controller;
namespace ZdravoCorp.View.Patient.ViewModel.Survey
{
    public class AppointmentSurveyViewModel: ObservableObject
    {
        private ObservableCollection<AppointmentSurveyModel> surveyColelction;
        private AppointmentSurveyController appointmentSurveyController;

        private ObservableCollection<int> grades;

        public AppointmentSurveyViewModel()
        {
            

        }
        public ObservableCollection<int> SetGrades(ObservableCollection<int> grades)
        {
            grades.Add(1);
            grades.Add(2);
            grades.Add(3);
            grades.Add(4);
            grades.Add(5);
            return grades;
        }
        public ObservableCollection<int> Grades { get; set; }


    }
}
