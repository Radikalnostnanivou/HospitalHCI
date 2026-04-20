using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorp.View.Core;
using ZdravoCorp.View.Manager.Model.Rooms;
using ZdravoCorp.View.Manager.View.Rooms;
using Room = Model.Room;
using RoomType = Model.RoomType;

namespace ZdravoCorp.View.Manager.ViewModel.Rooms
{
    public class AddRoomViewModel : ObservableObject, ViewModelInterface
    {
        private string identifier;
        private float size;
        private int floor;
        private ObservableCollection<RoomTypeModel> types;
        private RoomTypeModel selectedRoomType;
        private RoomController controller;

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

        public int Floor
        {
            get { return floor; }
            set
            {
                if (value != floor)
                {
                    floor = value;
                    OnPropertyChanged();
                }
            }
        }

        public String Identifier
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

        public RoomTypeModel SelectedRoomType
        {
            get { return selectedRoomType; }
            set
            {
                if (value != selectedRoomType)
                {
                    selectedRoomType = value;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand CreateCommand { get; set; }

        public RelayCommand CreateRoomTypeCommand { get; set; }

        public ObservableCollection<RoomTypeModel> Types { get => types; set => types = value; }

        public AddRoomViewModel()
        {
            controller = new RoomController();
            Types = controller.GetAllRoomTypeView();

            CreateCommand = new RelayCommand(o =>
            {
                try
                {
                    controller.Create(new Room(Identifier, Floor, Size, new RoomType(SelectedRoomType), new List<Appointment>(), new List<Equipment>(), new List<Medication>()));
                    CurrentView = new View.Rooms.Rooms(new RoomsViewModel());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, checkIfReady);

            CreateRoomTypeCommand = new RelayCommand(o =>
            {
                CurrentView = new AddRoomType(new AddRoomTypeViewModel());
            });
        }

        private bool checkIfReady(object arg)
        {
            return SelectedRoomType != null && Identifier != "" && Identifier != null && Size > 0;
        }

        public string GetTitle()
        {
            return "Add Room";
        }

        public void Update()
        {
            
        }
    }
}
