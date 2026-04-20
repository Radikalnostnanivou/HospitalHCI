using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Core;
using ZdravoCorp.View.Manager.Model.Employees;

namespace ZdravoCorp.View.Manager.ViewModel.Employees
{
    public class ViewEmployeeViewModel : ObservableObject, ViewModelInterface
    {
        private EmployeeDTO employee;
        public string Name
        {
            get => employee.Name;
            set
            {
                if (value != employee.Name)
                {
                    employee.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public float Pay
        {
            get => employee.Pay;
            set
            {
                if (value != employee.Pay)
                {
                    employee.Pay = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Type
        {
            get => employee.Type;
            set
            {
                if (value != employee.Type)
                {
                    employee.Type = value;
                    OnPropertyChanged();
                }
            }
        }
        public ViewEmployeeViewModel(EmployeeDTO selected)
        {
            employee = selected;
        }

        public string GetTitle()
        {
            return "Employees";
        }

        public void Update()
        {
            
        }
    }
}
