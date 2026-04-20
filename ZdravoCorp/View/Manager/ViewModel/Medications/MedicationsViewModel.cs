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
using ZdravoCorp.View.Manager.View.Medications;

namespace ZdravoCorp.View.Manager.ViewModel.Medications
{
    public class MedicationsViewModel : ObservableObject, ViewModelInterface
    {

        private ObservableCollection<MedicationType> medicationTypes;
        private ObservableCollection<MedicationType> medicationTypesSearch;
        private String searchBox;
        private MedicationController controller;
        private MedicationType selectedType;

        public RelayCommand AddCommand { get; set; }

        public RelayCommand ViewCommand { get; set; }

        public RelayCommand RequestsCommand { get; set; }

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

        public ObservableCollection<MedicationType> MedicationTypesSearch
        {
            get => medicationTypesSearch;
            set
            {
                if (value != medicationTypesSearch)
                {
                    medicationTypesSearch = value;
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
                MedicationTypes = new ObservableCollection<MedicationType>(MedicationTypesSearch.Where(equipment => Regex.IsMatch(equipment.Name, value)));
            }
            else
            {
                MedicationTypes = new ObservableCollection<MedicationType>(MedicationTypesSearch);
            }
        }

        public MedicationsViewModel()
        {
            controller = new MedicationController();
            MedicationTypes = new ObservableCollection<MedicationType>(controller.GetAllMedicationType());
            MedicationTypesSearch = new ObservableCollection<MedicationType>(controller.GetAllMedicationType());

            ViewCommand = new RelayCommand(o =>
            {
                CurrentView = new ViewMedicine(new ViewMedicineViewModel(new MedicationType(SelectedType)));
            }, CheckIfSelected);

            AddCommand = new RelayCommand(o =>
            {
                CurrentView = new AddMedication(new AddMedicationViewModel());
            });

            RequestsCommand = new RelayCommand(o =>
            {
                CurrentView = new RequestsMedication(new RequestsMedicationViewModel());
            });
        }

        private bool CheckIfSelected(object obj)
        {
            return SelectedType != null;
        }

        public string GetTitle()
        {
            return "Medication Types";
        }

        public void Update()
        {

        }
    }
}
