using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZdravoCorp.View.Core;
using ZdravoCorp.View.Manager.Model.Employees;
using ZdravoCorp.View.Manager.View.Employees;

namespace ZdravoCorp.View.Manager.ViewModel.Employees
{
    public class EmployeesViewModel : ObservableObject, ViewModelInterface
    {
        private ObservableCollection<EmployeeDTO> employees;
        private ObservableCollection<EmployeeDTO> employeesSearch;
        private String searchBox;
        private EmployeeDTO selectedEmployee;

        public RelayCommand ViewCommand { get; set; }

        public UserControl CurrentView
        {
            get => ContentViewModel.Instance.CurrentView;
            set
            {
                if (value != ContentViewModel.Instance.CurrentView)
                {
                    ContentViewModel.Instance.WindowBrowser.AddWindow(value);
                    ContentViewModel.Instance.CurrentView = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool CheckIfReady(object obj)
        {
            return selectedEmployee != null;
        }

        public ObservableCollection<EmployeeDTO> Employees
        {
            get => employees;
            set
            {
                if (value != employees)
                {
                    employees = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<EmployeeDTO> EmployeesSearch
        {
            get => employeesSearch;
            set
            {
                if (value != employeesSearch)
                {
                    employeesSearch = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SearchBoxText
        {
            get => searchBox;
            set
            {
                if (value != searchBox)
                {
                    ChangeTableSearch(value);
                    searchBox = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ChangeTableSearch(string value)
        {
            if (value == null)
            {
                return;
            }
            else if (value.Length > 0)
            {
                value = "(" + value + ")+";
                Employees = new ObservableCollection<EmployeeDTO>(EmployeesSearch.Where(equipment => Regex.IsMatch(equipment.Name, value)));
            }
            else
            {
                Employees = new ObservableCollection<EmployeeDTO>(EmployeesSearch);
            }
        }

        public EmployeeDTO SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                if (value != selectedEmployee)
                {
                    selectedEmployee = value;
                    OnPropertyChanged();
                }
            }
        }

        public EmployeesViewModel()
        {
            EmployeeDTO e1 = new EmployeeDTO(1 ,"Halid Pasanovic", 100000, "Manager");
            EmployeeDTO e2 = new EmployeeDTO(2, "Petar Petrovic", 101000, "Doctor");
            EmployeeDTO e3 = new EmployeeDTO(3, "Sara Petrovic", 100200, "Doctor");
            EmployeeDTO e4 = new EmployeeDTO(4, "Mitar Petrovic", 109000, "Secretary");
            employees = new ObservableCollection<EmployeeDTO>() { e1, e2, e3, e4};
            employeesSearch = new ObservableCollection<EmployeeDTO>() { e1, e2, e3, e4 };

            ViewCommand = new RelayCommand(o =>
            {
                CurrentView = new ViewEmployee(new ViewEmployeeViewModel(new EmployeeDTO(selectedEmployee)));
            }, CheckIfReady);
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
