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
using System.Windows.Shapes;
using ZdravoCorp.View.Core;
namespace ZdravoCorp.View.Patient.View.Allergies
{
    /// <summary>
    /// Interaction logic for AllergiesView.xaml
    /// </summary>
    public partial class AllergiesView : Window, WindowInterface
    {
        public AllergiesView()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.Allergies.AllergiesViewModel(this);
        }
        public string getTitle()
        {
            return "Alergeni";
        }
    }
}
