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
    public class ViewMedicineViewModel : ObservableObject, ViewModelInterface
    {
        private ObservableCollection<MedicationType> medicationTypes;
        private ObservableCollection<MedicationType> replacementTypes;
        private MedicationController controller;
        private NewMedicationRequestController controllerRequest;
        private MedicationType selectedType;
        private MedicationType selectedTable;
        private MedicationType selectedReplacement;
        private int selectedIndex;

        public RelayCommand AddCommand { get; set; }

        public RelayCommand DeleteCommand { get; set; }

        public RelayCommand DeleteReplacementCommand { get; set; }

        public RelayCommand ChangeCommand { get; set; }

        public String Name
        {
            get => selectedType.Name;
            set
            {
                if (value != selectedType.Name)
                {
                    selectedType.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Manufacturer
        {
            get => selectedType.Manufacturer;
            set
            {
                if (value != selectedType.Manufacturer)
                {
                    selectedType.Manufacturer = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Description
        {
            get => selectedType.Description;
            set
            {
                if (value != selectedType.Description)
                {
                    selectedType.Description = value;
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

        public MedicationType SelectedType
        {
            get => selectedType;
            set
            {
                if (value != selectedType)
                {
                    selectedType = value;
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

        public ViewMedicineViewModel(MedicationType selectedType)
        {
            controller = new MedicationController();
            controllerRequest = new NewMedicationRequestController();
            MedicationTypes = new ObservableCollection<MedicationType>(controller.GetAllMedicationType());
            ReplacementTypes = new ObservableCollection<MedicationType>(selectedType.Replacement);
            this.selectedType = selectedType;

            ChangeCommand = new RelayCommand(o =>
            {
                SelectedType.Replacement = new List<MedicationType>(ReplacementTypes);
                controllerRequest.Create(new NewMedicationRequest(SelectedType, Status.PENDING, "", true));
                CurrentView = new View.Medications.Medications(new MedicationsViewModel());
            });

            DeleteReplacementCommand = new RelayCommand(o =>
            {
                ReplacementTypes.Remove(SelectedTable);
                SelectedTable = null;
                OnPropertyChanged("ReplacementTypes");
            }, CheckIfSelected);

            DeleteCommand = new RelayCommand(o =>
            {
                controller.DeleteMedicationType(SelectedType.Id);
                CurrentView = new View.Medications.Medications(new MedicationsViewModel());
            });
        }

        private bool CheckIfSelected(object obj)
        {
            return SelectedTable != null;
        }

        public string GetTitle()
        {
            return "Medication Type";
        }

        public void Update()
        {
            MedicationTypes = new ObservableCollection<MedicationType>(controller.GetAllMedicationType());
        }
    }
}
