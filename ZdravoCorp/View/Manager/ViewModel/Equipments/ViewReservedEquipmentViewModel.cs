using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorp.View.Core;
using ZdravoCorp.View.Manager.Model.Equipments;
using ZdravoCorp.View.Manager.Model.Rooms;
using ZdravoCorp.View.Manager.View.Equipments;
using Equipment = Model.Equipment;

namespace ZdravoCorp.View.Manager.ViewModel.Equipments
{
    public class ViewReservedEquipmentViewModel : ObservableObject, ViewModelInterface
    {
        private ObservableCollection<RoomModel> roomsList;
        private ChangeActionModel selectedAction;
        private RoomController roomController;
        private ActionController actionController;
        private ReservedEquipmentViewModel parent;
        private int starting_count;
        private int count_max;
        private string time;
        private string type;
        private string designationCode;
        private RoomModel selectedRoom;
        private DateTime selectedDate;

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

        public int Count
        {
            get => SelectedAction.Count;
            set
            {
                if (value != SelectedAction.Count)
                {
                    SelectedAction.Count = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MaxCount
        {
            get => count_max;
            set
            {
                if (value != count_max)
                {
                    count_max = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Room
        {
            get => designationCode;
            set
            {
                if (value != designationCode)
                {
                    designationCode = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Time
        {
            get => time;
            set
            {
                if (value != time)
                {
                    time = value;
                    OnPropertyChanged();
                }
            }
        }

        public RoomModel SelectedRoom
        {
            get => selectedRoom;
            set
            {
                if (value != selectedRoom)
                {
                    selectedRoom = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                if (value != selectedDate)
                {
                    selectedDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public ActionController ActionController
        {
            get => actionController;
            set
            {
                if (value != actionController)
                {
                    actionController = value;
                    OnPropertyChanged();
                }
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
        public RoomController RoomController { get => roomController; set => roomController = value; }

        public RelayCommand ChangeCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }
        public ObservableCollection<RoomModel> RoomsList
        {
            get => roomsList;
            set
            {
                if (value != roomsList)
                {
                    roomsList = value;
                    OnPropertyChanged();
                }
            }
        }

        public ViewReservedEquipmentViewModel(ChangeActionModel selected, ReservedEquipmentViewModel parent)
        {
            this.parent = parent;
            SelectedAction =new ChangeActionModel(selected);
            RoomController = new RoomController();
            actionController = new ActionController();

            RoomsList = RoomController.GetAllRoomsVO();
            SelectedDate = SelectedAction.ExecutionDate.Date;
            Room room = roomController.Read(SelectedAction.Id_outgoing_room);
            foreach (Equipment it in room.Equipment)
            {
                if (it.Identifier == SelectedAction.Id_equipment)
                {
                    count_max = it.Actual_count + SelectedAction.Count;
                }
            }
            Type = room.RoomTypeString;
            Room = room.DesignationCode;

            starting_count = SelectedAction.Count;
            Time = SelectedAction.ExecutionDate.TimeOfDay.ToString("hh\\:mm");

            foreach (RoomModel it in roomsList)
            {
                if (it.DesignationCode == SelectedAction.IncomingRoom)
                {
                    SelectedRoom = it;
                    break;
                }
            }

            ChangeCommand = new RelayCommand(o =>
            {
                try
                {
                    TimeSpan temp = TimeSpan.ParseExact(Time, "hh\\:mm", CultureInfo.InvariantCulture);
                    SelectedAction.ExecutionDate = SelectedDate.Add(temp);
                    SelectedAction.Id_incoming_room = SelectedRoom.Identifier;
                    ActionController.UpdateChangeAction(SelectedAction, starting_count - SelectedAction.Count);
                    parent.Update();
                    CurrentView = new ReservedEquipment(new ReservedEquipmentViewModel());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, checkIfAllCorrect);

            DeleteCommand = new RelayCommand(o =>
            {
                try
                {
                    ActionController.DeleteChangeAction(SelectedAction);
                    parent.Update();
                    CurrentView = new ReservedEquipment(new ReservedEquipmentViewModel());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private bool checkIfAllCorrect(object obj)
        {
            try
            {
                if (Count > MaxCount || Count <= 0 || SelectedRoom == null)
                {
                    return false;
                }
                TimeSpan test = TimeSpan.ParseExact(Time, "hh\\:mm", CultureInfo.InvariantCulture);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public string GetTitle()
        {
            return "Change Reservation";
        }

        public void Update()
        {
            
        }
    }
}
