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
using ZdravoCorp.View.Manager.View.Equipments;

namespace ZdravoCorp.View.Manager.ViewModel.Equipments
{
    public class ReservedEquipmentViewModel : ObservableObject, ViewModelInterface
    {
        private ActionController controller;
        private ObservableCollection<ChangeActionModel> actionTable;
        private ObservableCollection<ChangeActionModel> actionTableSearch;
        private String searchBox;
        private ChangeActionModel selectedAction;

        public RelayCommand ViewReservedCommand { get; set; }

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

        public ObservableCollection<ChangeActionModel> ActionTable
        {
            get => actionTable;
            set
            {
                if (value != actionTable)
                {
                    actionTable = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ChangeActionModel> ActionTableSearch
        {
            get => actionTableSearch;
            set
            {
                if (value != actionTableSearch)
                {
                    actionTableSearch = value;
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
                ActionTable = new ObservableCollection<ChangeActionModel>(ActionTableSearch.Where(equipment => Regex.IsMatch(equipment.Equipment, value)));
            }
            else
            {
                ActionTable = new ObservableCollection<ChangeActionModel>(ActionTableSearch);
            }
        }

        public ChangeActionModel SelectedAction
        {
            get => selectedAction;
            set
            {
                if (value != selectedAction)
                {
                    selectedAction = value;
                    OnPropertyChanged();
                }
            }
        }

        public ReservedEquipmentViewModel()
        {
            controller = new ActionController();
            Update();

            ViewReservedCommand = new RelayCommand(o =>
            {
                CurrentView = new ViewReservedEquipment(new ViewReservedEquipmentViewModel(SelectedAction, this));
            }, checkIfTableRowSelected);
        }

        private bool checkIfTableRowSelected(object arg)
        {
            return SelectedAction != null;
        }

        public string GetTitle()
        {
            return "Reservations";
        }

        public void Update()
        {
            ActionTable = controller.GetAllChangeRoomActions();
            ActionTableSearch = controller.GetAllChangeRoomActions();
        }
    }
}
