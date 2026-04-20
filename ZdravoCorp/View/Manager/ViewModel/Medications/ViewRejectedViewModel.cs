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
using ZdravoCorp.View.Manager.View.Medications;

namespace ZdravoCorp.View.Manager.ViewModel.Medications
{
    public class ViewRejectedViewModel : ObservableObject, ViewModelInterface
    {
        private ObservableCollection<MedicationType> medicationTypes;
        private ObservableCollection<MedicationType> replacementTypes;
        private NewMedicationRequestController controller;
        private MedicationController controllerType;
        private NewMedicationRequest selectedRequest;
        private MedicationType selectedReplacement;
        private MedicationType selectedTable;
        private int selectedIndex;

        public RelayCommand DeleteReplacementCommand { get; set; }

        public RelayCommand ChangeCommand { get; set; }

        public String Comment
        {
            get => selectedRequest.Comment;
            set
            {
                if (value != selectedRequest.Comment)
                {
                    selectedRequest.Comment = value;
                    OnPropertyChanged();
                }
            }
        }

        public String Name
        {
            get => selectedRequest.MedicationType.Name;
            set
            {
                if (value != selectedRequest.MedicationType.Name)
                {
                    selectedRequest.MedicationType.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Manufacturer
        {
            get => selectedRequest.MedicationType.Manufacturer;
            set
            {
                if (value != selectedRequest.MedicationType.Manufacturer)
                {
                    selectedRequest.MedicationType.Manufacturer = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Description
        {
            get => selectedRequest.MedicationType.Description;
            set
            {
                if (value != selectedRequest.MedicationType.Description)
                {
                    selectedRequest.MedicationType.Description = value;
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

        public NewMedicationRequest SelectedRequest
        {
            get => selectedRequest;
            set
            {
                if (value != selectedRequest)
                {
                    selectedRequest = value;
                    OnPropertyChanged();
                }
            }
        }

        public MedicationType SelectedTable
        {
            get => selectedTable;
            set
            {
                if (value != selectedTable)
                {
                    selectedTable = value;
                    OnPropertyChanged();
                }
            }
        }
        public MedicationType SelectedReplacement
        {
            get => selectedReplacement;
            set
            {
                if (value != selectedReplacement)
                {
                    selectedReplacement = value;
                    AddSelected(value);
                    OnPropertyChanged();
                }
            }
        }

        public void AddSelected(MedicationType value)
        {
            bool found = false;
            foreach(MedicationType type in ReplacementTypes)
            {
                if (type.Id == value.Id)
                {
                    found = true;
                }
            }
            if (!found)
            {
                ReplacementTypes.Add(value);
            }
        }

        public ObservableCollection<MedicationType> MedicationTypes
        {
            get => medicationTypes;
            set
            {
                if (value != medicationTypes)
                {
                    medicationTypes = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<MedicationType> ReplacementTypes
        {
            get => replacementTypes;
            set
            {
                if (value != replacementTypes)
                {
                    replacementTypes = value;
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

        

        public ViewRejectedViewModel(NewMedicationRequest selected)
        {
            this.selectedRequest = selected;
            controllerType = new MedicationController();
            controller = new NewMedicationRequestController();
            MedicationTypes = new ObservableCollection<MedicationType>(controllerType.GetAllMedicationType());
            ReplacementTypes = new ObservableCollection<MedicationType>(SelectedRequest.MedicationType.Replacement);

            ChangeCommand = new RelayCommand(o =>
            {
                SelectedRequest.MedicationType.Replacement = new List<MedicationType>(ReplacementTypes);
                controller.Update(new NewMedicationRequest(SelectedRequest, Status.PENDING, "", SelectedRequest.Change));
                CurrentView = new RequestsMedication(new RequestsMedicationViewModel());
            });

            DeleteReplacementCommand = new RelayCommand(o =>
            {
                ReplacementTypes.Remove(SelectedTable);
                SelectedTable = null;
                OnPropertyChanged("ReplacementTypes");
            }, CheckIfSelected);
        }

        private bool CheckIfSelected(object obj)
        {
            return SelectedTable != null;
        }

        public string GetTitle()
        {
            return "Rejected Request";
        }

        public void Update()
        {
            MedicationTypes = new ObservableCollection<MedicationType>(controllerType.GetAllMedicationType());
        }
    }
}
