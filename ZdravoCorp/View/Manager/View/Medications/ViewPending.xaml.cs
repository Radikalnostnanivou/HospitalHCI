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
using ZdravoCorp.View.Manager.ViewModel.Medications;

namespace ZdravoCorp.View.Manager.View.Medications
{
    /// <summary>
    /// Interaction logic for ViewPending.xaml
    /// </summary>
    public partial class ViewPending : UserControl
    {
        public ViewPending(ViewPendingViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
