using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorp.View.Core;
using ZdravoCorp.View.Manager.Model.Rooms;
using ZdravoCorp.View.Manager.View.Rooms;

namespace ZdravoCorp.View.Manager.ViewModel.Rooms
{
    public class ViewRenovationTrueViewModel : ObservableObject, ViewModelInterface
    {
        private ActionController controller;
        private RenovationActionModel selectedAction;

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

        public RenovationActionModel SelectedAction { get => selectedAction; set => selectedAction = value; }

        public String Identifier
        {
            get { return selectedAction.DesignationCode; }
            set
            {
                if (value != selectedAction.DesignationCode)
                {
                    selectedAction.DesignationCode = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime EndDate
        {
            get { return SelectedAction.ExpirationDate; }
            set
            {
                if (value != SelectedAction.ExpirationDate)
                {
                    SelectedAction.ExpirationDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime StartDate
        {
            get { return SelectedAction.ExecutionDate; }
            set
            {
                if (value != SelectedAction.ExecutionDate)
                {
                    SelectedAction.ExecutionDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand ChangeCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public ViewRenovationTrueViewModel(RenovationActionModel selectedAction)
        {
            controller = new ActionController();
            SelectedAction = new RenovationActionModel(selectedAction);

            ChangeCommand = new RelayCommand(o =>
            {
                try
                {
                    controller.UpdateRenovationAction(SelectedAction);
                    CurrentView = new RenovatingRooms(new RenovatingRoomsViewModel());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            DeleteCommand = new RelayCommand(o =>
            {
                try
                {
                    controller.DeleteRenovationAction(SelectedAction);
                    CurrentView = new RenovatingRooms(new RenovatingRoomsViewModel());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        public string GetTitle()
        {
            return "Change Renovation";
        }

        public void Update()
        {
            
        }
    }
}
