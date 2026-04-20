using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorp.View.Core;
using ZdravoCorp.View.Manager.View;
using ZdravoCorp.View.Manager.View.Equipments;
using ZdravoCorp.View.Manager.ViewModel.Employees;
using ZdravoCorp.View.Manager.ViewModel.Equipments;
using ZdravoCorp.View.Manager.ViewModel.Medications;
using ZdravoCorp.View.Manager.ViewModel.Rooms;

namespace ZdravoCorp.View.Manager.ViewModel
{
    public class ContentViewModel : ObservableObject
    {
        private static ContentViewModel instance;
        private static readonly object key = new object();

        private WindowBrowser _windowBrowser;
        private String _title;

        private UserControl _currentView;

        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                if (value != _currentView)
                {
                    ViewModelInterface context = (ViewModelInterface)value.DataContext;
                    Title = context.GetTitle();
                    context.Update();
                    _currentView = value;
                    OnPropertyChanged();
                }
            }
        }

        public WindowBrowser WindowBrowser { get => _windowBrowser; set => _windowBrowser = value; }

        public string Title
        {
            get => _title;
            set
            {
                string title = TranslationSource.Instance[value];
                if (title != _title)
                {
                    _title = title;
                    OnPropertyChanged();
                }
            }
        }

        public RelayCommand MainViewCommand { get; set; }

        public RelayCommand RoomViewCommand { get; set; }

        public RelayCommand EquipmentViewCommand { get; set; }

        public RelayCommand MedicineViewCommand { get; set; }

        public RelayCommand BackViewCommand { get; set; }

        public RelayCommand SettingsCommand { get; set; }

        public RelayCommand EmployeesCommand { get; set; }

        public ContentViewModel()
        {
            WindowBrowser = new WindowBrowser();

            CurrentView = new Home(new MainViewModel());
            WindowBrowser.AddWindow(CurrentView);

            MainViewCommand = new RelayCommand(o =>
            {
                CurrentView = new Home(new MainViewModel());
                WindowBrowser.AddWindow(CurrentView);
            });

            RoomViewCommand = new RelayCommand(o =>
            {
                CurrentView = new View.Rooms.Rooms(new RoomsViewModel());
                WindowBrowser.AddWindow(CurrentView);
            });

            EquipmentViewCommand = new RelayCommand(o =>
            {
                CurrentView = new View.Equipments.Equipment(new EquipmentViewModel());
                WindowBrowser.AddWindow(CurrentView);
            });

            MedicineViewCommand = new RelayCommand(o =>
            {
                CurrentView = new View.Medications.Medications(new MedicationsViewModel());
                WindowBrowser.AddWindow(CurrentView);
            });

            BackViewCommand = new RelayCommand(o =>
            {
                CurrentView = WindowBrowser.BackWindow();
            });

            SettingsCommand = new RelayCommand(o =>
            {
                CurrentView = new Settings(new SettingsViewModel());
                WindowBrowser.AddWindow(CurrentView);
            });

            EmployeesCommand = new RelayCommand(o =>
            {
                CurrentView = new View.Employees.Employees(new EmployeesViewModel());
                WindowBrowser.AddWindow(CurrentView);
            });
        }

        public static ContentViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (key)
                    {
                        if (instance == null)
                        {
                            instance = new ContentViewModel();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
