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
    public class AddRoomTypeViewModel : ObservableObject, ViewModelInterface
    {
        private RoomController controller;
        private string name;
        public RelayCommand AddRoomTypeCommand { get; set; }

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

        public string Name
        {
            get => name;
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddRoomTypeViewModel()
        {
            controller = new RoomController();

            AddRoomTypeCommand = new RelayCommand(o =>
            {
                try
                {
                    controller.CreateRoomType(new RoomTypeModel(name));
                    CurrentView = new AddRoom(new AddRoomViewModel());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, checkIfReady);
        }

        public bool checkIfReady(object obj)
        {
            return Name != "" && Name != null;
        }

        public string GetTitle()
        {
            return "Add Room Type";
        }

        public void Update()
        {
            
        }
    }
}
