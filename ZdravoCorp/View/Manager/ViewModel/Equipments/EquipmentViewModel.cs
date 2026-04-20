using Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZdravoCorp.View.Core;
using ZdravoCorp.View.Manager.Model.Equipments;
using ZdravoCorp.View.Manager.View;
using ZdravoCorp.View.Manager.View.Equipments;

namespace ZdravoCorp.View.Manager.ViewModel.Equipments
{
    public class EquipmentViewModel : ObservableObject, ViewModelInterface
    {
        private ObservableCollection<EquipmentModel> equipmentTable;
        private ObservableCollection<EquipmentModel> equipmentTableSaved;
        private ObservableCollection<EquipmentModel> equipmentTableSearch;
        private ObservableCollection<String> dropDownCollection;
        private String selectedOption;
        private String searchBox;
        private EquipmentController controller;
        private EquipmentModel selectedEquipment;
        private int selectedIndex;

        public RelayCommand AddEquipmentViewCommand { get; set; }

        public RelayCommand ViewEquipmentTypeCommand { get; set; }

        public RelayCommand ViewEquipmentCommand { get; set; }

        public RelayCommand ChangePositionCommand { get; set; }

        public RelayCommand ReservedCommand { get; set; }

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

        public EquipmentViewModel()
        {
            SelectedIndex = -1;
            controller = new EquipmentController();
            DropDownCollection = new ObservableCollection<string>() { "Both", "Disposable", "Reusable" };
            EquipmentTable = controller.GetAllEquipmentTableVO();
            EquipmentTableSaved = controller.GetAllEquipmentTableVO();
            SelectedOption = DropDownCollection[0];

            AddEquipmentViewCommand = new RelayCommand(o =>
            {
                CurrentView = new AddEquipment(new AddEquipmentViewModel());
            });

            ViewEquipmentCommand = new RelayCommand(o =>
            {
                CurrentView = new ViewEquipment(new ViewEquipmentViewModel(SelectedEquipment, this));
            }, checkIfTableRowSelected);

            ViewEquipmentTypeCommand = new RelayCommand(o =>
            {
                CurrentView = new ViewEquipment(new ViewEquipmentViewModel(SelectedEquipment, this));
            }, checkIfTableRowSelected);

            ChangePositionCommand = new RelayCommand(o =>
            {
                CurrentView = new ChangeEquipmentPosition(new ChangeEquipmentPositionViewModel(SelectedEquipment));
            }, checkIfTableRowSelected);

            ReservedCommand = new RelayCommand(o =>
            {
                CurrentView = new ReservedEquipment(new ReservedEquipmentViewModel());
            });
        }

        private bool checkIfTableRowSelected(object arg)
        {
            return SelectedIndex != -1;
        }

        public ObservableCollection<EquipmentModel> EquipmentTable
        {
            get => equipmentTable;
            set
            {
                if (value != equipmentTable)
                {
                    equipmentTable = value;
                    OnPropertyChanged();
                }
            }
        }

        public EquipmentModel SelectedEquipment
        {
            get => selectedEquipment;
            set
            {
                if (value != selectedEquipment)
                {
                    selectedEquipment = value;
                    OnPropertyChanged();
                }
            }
        }

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (value != selectedIndex)
                {
                    selectedIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> DropDownCollection
        {
            get => dropDownCollection;
            set
            {
                if (value != dropDownCollection)
                {
                    dropDownCollection = value;
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
            else if(value.Length > 0)
            {
                value = "(" + value + ")+";
                EquipmentTable = new ObservableCollection<EquipmentModel>(EquipmentTableSearch.Where(equipment => Regex.IsMatch(equipment.Name, value)));
            }
            else
            {
                EquipmentTable = new ObservableCollection<EquipmentModel>(EquipmentTableSearch);
            }
        }

        public string SelectedOption
        {
            get => selectedOption;
            set
            {
                if (value != selectedOption)
                {
                    ChangeTable(value);
                    selectedOption = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ChangeTable(string value)
        {
            if (value == null)
            {
                return;
            }
            else if (value == "Both")
            {
                EquipmentTable = new ObservableCollection<EquipmentModel>(EquipmentTableSaved);
            }
            else if (value == "Disposable")
            {
                EquipmentTable = new ObservableCollection<EquipmentModel>(EquipmentTableSaved.Where(equipment => equipment.Disposable == true));
            } 
            else if(value == "Reusable")
            {
                EquipmentTable = new ObservableCollection<EquipmentModel>(EquipmentTableSaved.Where(equipment => equipment.Disposable == false));
            }
            EquipmentTableSearch = new ObservableCollection<EquipmentModel>(EquipmentTable);
        }

        public ObservableCollection<EquipmentModel> EquipmentTableSaved
        {
            get => equipmentTableSaved;
            set
            {
                if (value != equipmentTableSaved)
                {
                    equipmentTableSaved = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<EquipmentModel> EquipmentTableSearch
        {
            get => equipmentTableSearch;
            set
            {
                if (value != equipmentTableSearch)
                {
                    equipmentTableSearch = value;
                    OnPropertyChanged();
                }
            }
        }

        public string GetTitle()
        {
            return "Equipment";
        }

        public void Update()
        {
            EquipmentTable = controller.GetAllEquipmentTableVO();
        }
    }
}
