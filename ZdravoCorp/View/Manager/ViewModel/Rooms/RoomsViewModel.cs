using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZdravoCorp.View.Core;
using ZdravoCorp.View.Manager.View;
using ZdravoCorp.View.Manager.View.Rooms;

namespace ZdravoCorp.View.Manager.ViewModel.Rooms
{
    public class RoomsViewModel : ObservableObject, ViewModelInterface
    {
        private RoomController roomController;
        private Room selectedRoom;
        private int selectedIndex;
        private ObservableCollection<Room> roomsCollection;
        private ObservableCollection<Room> roomsCollectionSearch;
        private String searchBox;
        public RelayCommand ViewRoomCommand { get; set; }

        public RelayCommand AddRoomCommand { get; set; }

        public RelayCommand RenovationsCommand { get; set; }

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

        public ObservableCollection<Room> RoomsCollectionSearch
        {
            get => roomsCollectionSearch;
            set
            {
                if (value != roomsCollectionSearch)
                {
                    roomsCollectionSearch = value;
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
                RoomsCollection = new ObservableCollection<Room>(RoomsCollectionSearch.Where(equipment => Regex.IsMatch(equipment.DesignationCode, value)));
            }
            else
            {
                RoomsCollection = new ObservableCollection<Room>(RoomsCollectionSearch);
            }
        }

        public Room SelectedRoom
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
        public ObservableCollection<Room> RoomsCollection
        {
            get => roomsCollection;
            set
            {
                if (value != roomsCollection)
                {
                    roomsCollection = value;
                    OnPropertyChanged();
                }
            }
        }

        public RoomsViewModel()
        {
            roomController = new RoomController();
            RoomsCollection = new ObservableCollection<Room>();
            List<Room> rooms = roomController.GetAll();
            RoomsCollection = new ObservableCollection<Room>(rooms);
            RoomsCollectionSearch = new ObservableCollection<Room>(roomController.GetAll());

            ViewRoomCommand = new RelayCommand(o =>
            {
                CurrentView = new ViewRoom(new ViewRoomViewModel(SelectedRoom));
            }, checkIfTableRowSelected);

            AddRoomCommand = new RelayCommand(o =>
            {
                CurrentView = new AddRoom(new AddRoomViewModel());
            });

            RenovationsCommand = new RelayCommand(o =>
            {
                CurrentView = new RenovatingRooms(new RenovatingRoomsViewModel());
            });
        }

        private bool checkIfTableRowSelected(object arg)
        {
            return SelectedIndex != -1;
        }

        public string GetTitle()
        {
            return "Rooms";
        }

        public void Update()
        {
            List<Room> rooms = roomController.GetAll();
            RoomsCollection = new ObservableCollection<Room>(rooms);
        }
    }
}
