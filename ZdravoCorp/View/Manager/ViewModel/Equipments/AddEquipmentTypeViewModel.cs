using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorp.View.Core;
using ZdravoCorp.View.Manager.Model.Equipments;
using ZdravoCorp.View.Manager.View;
using ZdravoCorp.View.Manager.View.Equipments;

namespace ZdravoCorp.View.Manager.ViewModel.Equipments
{
    public class AddEquipmentTypeViewModel : ObservableObject, ViewModelInterface
    {
        private string _name;
        private string _description;
        private bool _disposable;
        private EquipmentController equipmentController;
        private AddEquipmentViewModel _parentViewModel;

        public RelayCommand AddViewCommand { get; set; }

        public AddEquipmentViewModel ParentViewModel { get => _parentViewModel; set => _parentViewModel = value; }

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
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool Disposable
        {
            get => _disposable;
            set
            {
                if (value != _disposable)
                {
                    _disposable = value;
                    OnPropertyChanged();
                }
            }
        }
        public AddEquipmentTypeViewModel(AddEquipmentViewModel parent)
        {
            equipmentController = new EquipmentController();
            ParentViewModel = parent;

            AddViewCommand = new RelayCommand(o =>
            {
                try
                {
                    equipmentController.CreateEquipmentType(new EquipmentTypeModel(Name, Description, Disposable));
                    parent.Update();
                    CurrentView = new AddEquipment(parent);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        public string GetTitle()
        {
            return "Add Equipment Type";
        }

        public void Update()
        {
            
        }
    }
}
