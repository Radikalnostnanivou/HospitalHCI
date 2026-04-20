using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoCorp.View.Manager.ViewModel.Surveys;

namespace ZdravoCorp.View.Manager.View.Surveys
{
    /// <summary>
    /// Interaction logic for Surveys.xaml
    /// </summary>
    public partial class Surveys : UserControl
    {
        public Surveys(SurveysViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
