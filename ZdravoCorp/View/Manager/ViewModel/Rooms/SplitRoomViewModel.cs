using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ZdravoCorp.View.Core;
using ZdravoCorp.View.Manager.View.Rooms;

namespace ZdravoCorp.View.Manager.ViewModel.Rooms
{
    public class SplitRoomViewModel : ObservableObject, ViewModelInterface
    {
        private Room selectedRoom;
        private RoomController roomController;
        private float size;
        private string identifier;

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

        public int Floor
        {
            get { return selectedRoom.Floor; }
            set
            {
                if (value != selectedRoom.Floor)
                {
                    selectedRoom.Floor = value;
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

        public float MaxSize
        {
            get { return SelectedRoom.SurfaceArea; }
            set
            {
                if (value != SelectedRoom.SurfaceArea)
                {
                    SelectedRoom.SurfaceArea = value;
                    OnPropertyChanged();
                }
            }
        }

        public String NewIdentifier
        {
            get { return identifier; }
            set
            {
                if (value != identifier)
                {
                    identifier = value;
                    OnPropertyChanged();
                }
            }
        }

        public float Size
        {
            get { return size; }
            set
            {
                if (value != size)
                {
                    size = value;
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

        public RelayCommand SplitCommand { get; set; }

        public SplitRoomViewModel(Room selectedRoom)
        {
            this.SelectedRoom = selectedRoom;
            roomController = new RoomController();

            SplitCommand = new RelayCommand(o =>
            {
                selectedRoom.SurfaceArea -= Size;
                roomController.Update(selectedRoom);
                roomController.Create(new Room(NewIdentifier, Floor, Size, selectedRoom.RoomType));
                CurrentView = new View.Rooms.Rooms(new Rooms.RoomsViewModel());
            }, checkIfReady);
        }

        private bool checkIfReady(object obj)
        {
            return NewIdentifier != null && !NewIdentifier.Equals("") && Size > 0 && Size < MaxSize;
        }

        public string GetTitle()
        {
            return "Split Room";
        }

        public void Update()
        {
            
        }
    }
}
