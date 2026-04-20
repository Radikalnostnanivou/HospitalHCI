using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZdravoCorp.View.Core;

namespace ZdravoCorp.View.Manager.ViewModel.Rooms
{
    public class CombineRoomsViewModel : ObservableObject, ViewModelInterface
    {
        private RoomController roomController;
        private Room selectedRoom;
        private ObservableCollection<Room> roomsOnFloor;
        private Room combineWith;

        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                if (value != selectedRoom)
                {
                    selectedRoom = value;
                    OnPropertyChanged();
                }
            }
        }

        public Room CombineWith
        {
            get { return combineWith; }
            set
            {
                if (value != combineWith)
                {
                    combineWith = value;
                    OnPropertyChanged();
                }
            }
        }

        public String Identifier
        {
            get { return SelectedRoom.DesignationCode; }
            set
            {
                if (value != SelectedRoom.DesignationCode)
                {
                    SelectedRoom.DesignationCode = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Room> RoomsOnFloor
        {
            get { return roomsOnFloor; }
            set
            {
                if (value != roomsOnFloor)
                {
                    roomsOnFloor = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public RelayCommand CombineCommand { get; set; }

        public CombineRoomsViewModel(Room selectedRoom)
        {
            SelectedRoom = selectedRoom;
            roomController = new RoomController();
            roomsOnFloor = new ObservableCollection<Room>(roomController.GetAll().Where(room => room.Floor == SelectedRoom.Floor && room.Identifier != SelectedRoom.Identifier));

            CombineCommand = new RelayCommand(o =>
            {
                roomController.CombineRooms(SelectedRoom, CombineWith);
                CurrentView = new View.Rooms.Rooms(new RoomsViewModel());
            }, CheckIfReady);
        }

        public bool CheckIfReady(object obj)
        {
            return CombineWith != null;
        }

        public string GetTitle()
        {
            return "Combine Rooms";
        }

        public void Update()
        {
            
        }
    }
}
