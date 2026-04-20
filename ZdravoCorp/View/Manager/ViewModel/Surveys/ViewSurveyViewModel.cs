using Controller;
using Model;
using Service;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Core;

namespace ZdravoCorp.View.Manager.ViewModel.Surveys
{
    public class ViewSurveyViewModel : ObservableObject, ViewModelInterface
    {
        private string label;
        private string description;
        private Survey survey;
        private DateTime start = DateTime.MinValue;
        private DateTime end = DateTime.MaxValue;
        private DateTime start2;
        private DateTime end2;
        private HospitalSurveyController hospitalController = new HospitalSurveyController();
        private AppointmentSurveyController appointmentController = new AppointmentSurveyController();

        public RelayCommand CreateCommand { get; set; }

        public ViewSurveyViewModel(Survey survey,string description, string label)
        {
            this.label = label;
            this.description = description;
            this.survey = survey;

            CreateCommand = new RelayCommand(o =>
            {
                PdfDocument doc = new PdfDocument();
                PdfPage page = doc.Pages.Add();
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 8);
                PdfGraphics graphics = page.Graphics;
                graphics.DrawString(description, font, PdfBrushes.Black, new PointF(0, 0));
                doc.Save("Survey.pdf");
            });
        }

        public string Label
        {
            get => label;
            set
            {
                if (value != label)
                {
                    label = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => description;
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Start
        {
            get => start;
            set
            {
                if (value != start)
                {
                    start = value;
                    ChangeDescription();
                    OnPropertyChanged();
                }
            }
        }
        public DateTime End
        {
            get => end;
            set
            {
                if (value != end)
                {
                    end = value;
                    ChangeDescription();
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Start2
        {
            get => start2;
            set
            {
                if (value != start2)
                {
                    start2 = value;
                    Start = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime End2
        {
            get => end2;
            set
            {
                if (value != end2)
                {
                    end2 = value;
                    End = value;
                    OnPropertyChanged();
                }
            }
        }

        public void ChangeDescription()
        {
            if(label.Equals("Hospital Survey"))
            {
                Description = hospitalController.GetResults(Start, End);
            }
            else
            {
                Description = appointmentController.GetResultsForDoctor(DoctorService.Instance.Read(survey.Id), Start, End);
            }
        }

        public string GetTitle()
        {
            return "ViewSurvey";
        }

        public void Update()
        {
            //throw new NotImplementedException();
        }
    }
}
