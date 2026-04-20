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
    public class ChangeEquipmentPositionViewModel : ObservableObject, ViewModelInterface
    {
        private ObservableCollection<RoomModel> roomsList;
        private EquipmentModel selected;
        private EquipmentController equipmentController;
        private RoomController roomController;
        private int count;
        private int count_max;
        private string time;
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
            get => selected.Name;
            set
            {
                if (value != selected.Name)
                {
                    selected.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Count
        {
            get => count;
            set
            {
                if (value != count)
                {
                    count = value;
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
            get => selected.DesignationCode;
            set
            {
                if (value != selected.DesignationCode)
                {
                    selected.DesignationCode = value;
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

        public EquipmentController EquipmentController { get => equipmentController; set => equipmentController = value; }
        public RoomController RoomController { get => roomController; set => roomController = value; }

        public RelayCommand ChangeCommand { get; set; }
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
        public ChangeEquipmentPositionViewModel(EquipmentModel selected)
        {
            this.selected = selected;
            RoomController = new RoomController();
            EquipmentController = new EquipmentController();
            RoomsList = RoomController.GetAllRoomsVO();
            MaxCount = selected.Actual_count;

            ChangeCommand = new RelayCommand(o =>
            {
                try
                {
                    DateTime executionDate = new DateTime();
                    if (selected.Disposable == false)
                    {
                        executionDate = SelectedDate;
                        executionDate = executionDate.Add(TimeSpan.Parse(Time));
                        equipmentController.ChangePositionOfEquipment(executionDate, selected.Room_identifier, SelectedRoom.Identifier, selected.Equipment_identifier, Count);
                        CurrentView = new View.Equipments.Equipment(new EquipmentViewModel());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, checkIfAllCorrect);
        }

        private bool checkIfAllCorrect(object obj)
        {
            try
            {
                if (selected.Disposable == false)
                {
                    TimeSpan test = TimeSpan.ParseExact(Time, "hh\\:mm", CultureInfo.InvariantCulture);
                }
                if (Count > MaxCount || Count <= 0 || SelectedRoom == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string GetTitle()
        {
            return "Reservation";
        }

        public void Update()
        {
            
        }
    }
}
