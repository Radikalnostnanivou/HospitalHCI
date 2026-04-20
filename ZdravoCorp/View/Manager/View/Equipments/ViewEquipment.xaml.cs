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
using ZdravoCorp.View.Core;
using ZdravoCorp.View.Manager.ViewModel.Equipments;

namespace ZdravoCorp.View.Manager.View.Equipments
{
    /// <summary>
    /// Interaction logic for ViewEquipment.xaml
    /// </summary>
    public partial class ViewEquipment : UserControl, WindowInterface
    {
        public ViewEquipment(ViewEquipmentViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }

        public string getTitle()
        {
            return "Equipment";
        }
    }
}
