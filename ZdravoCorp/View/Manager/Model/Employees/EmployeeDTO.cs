using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorp.View.Core;

namespace ZdravoCorp.View.Manager.Model.Employees
{
    public class EmployeeDTO : ObservableObject
    {
        public int id;
        private string name;
        private float pay;
        private string type;

        public EmployeeDTO(EmployeeDTO employee)
        {
            this.name = employee.Name;
            this.pay = employee.Pay;
            this.type = employee.Type;
        }

        public EmployeeDTO(int id, string name, float pay, string type)
        {
            this.name = name;
            this.pay = pay;
            this.type = type;
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
        public float Pay
        {
            get => pay;
            set
            {
                if (value != pay)
                {
                    pay = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Type
        {
            get => type;
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
