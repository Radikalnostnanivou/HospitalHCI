using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Core;

namespace Model
{
    public class Survey : ObservableObject
    {
        private int id;
        private string name;
        private SurveyEnum surveyType;

        public Survey(int id, string name, SurveyEnum surveyType)
        {
            this.id = id;
            this.name = name;
            this.surveyType = surveyType;
        }

        public int Id
        {
            get => id;
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Name
        {
            get => name;
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }
        public SurveyEnum SurveyType { get => surveyType; set => surveyType = value; }

    }
}
