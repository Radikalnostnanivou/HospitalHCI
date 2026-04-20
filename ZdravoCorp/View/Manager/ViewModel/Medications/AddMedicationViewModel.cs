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

namespace ZdravoCorp.View.Manager.ViewModel.Medications
{
    public class AddMedicationViewModel : ObservableObject, ViewModelInterface
    {
        private ObservableCollection<MedicationType> medicationTypes;
        private ObservableCollection<MedicationType> replacementTypes;
        private NewMedicationRequestController controller;
        private MedicationController controllerType;
        private NewMedicationRequest selectedRequest;
        private MedicationType selectedReplacement;
        private MedicationType selectedTable;
        private string name;
        private string manufacturer;
        private string description;
        private int selectedIndex;

        public RelayCommand DeleteReplacementCommand { get; set; }

        public RelayCommand AddCommand { get; set; }

        public String Name
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
        public String Manufacturer
        {
            get => manufacturer;
            set
            {
                if (value != manufacturer)
                {
                    manufacturer = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Description
        {
            get => description;
            set
            {
                if (value != description)
                {
                    description = value;
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
            foreach (MedicationType type in ReplacementTypes)
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



        public AddMedicationViewModel()
        {
            controllerType = new MedicationController();
            controller = new NewMedicationRequestController();
            MedicationTypes = new ObservableCollection<MedicationType>(controllerType.GetAllMedicationType());
            ReplacementTypes = new ObservableCollection<MedicationType>();

            AddCommand = new RelayCommand(o =>
            {
                SelectedRequest = new NewMedicationRequest(new MedicationType(Name, Manufacturer, Description), Status.PENDING, "", false);
                SelectedRequest.MedicationType.Replacement = new List<MedicationType>(ReplacementTypes);
                controller.Create(SelectedRequest);
                CurrentView = new View.Medications.Medications(new MedicationsViewModel());
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
            return "Add Medication Type";
        }

        public void Update()
        {
            MedicationTypes = new ObservableCollection<MedicationType>(controllerType.GetAllMedicationType());
        }
    }
}
