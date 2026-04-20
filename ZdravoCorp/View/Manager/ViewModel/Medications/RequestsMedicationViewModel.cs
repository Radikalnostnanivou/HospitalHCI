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
    public class RequestsMedicationViewModel : ObservableObject, ViewModelInterface
    {
        private ObservableCollection<NewMedicationRequest> medicationRequests;
        private ObservableCollection<NewMedicationRequest> medicationRequestsSearch;
        private String searchBox;
        private NewMedicationRequestController controller;
        private NewMedicationRequest selectedRequest;

        public RelayCommand ViewCommand { get; set; }

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

        public ObservableCollection<NewMedicationRequest> MedicationRequests
        {
            get => medicationRequests;
            set
            {
                if (value != medicationRequests)
                {
                    medicationRequests = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<NewMedicationRequest> MedicationRequestsSearch
        {
            get => medicationRequestsSearch;
            set
            {
                if (value != medicationRequestsSearch)
                {
                    medicationRequestsSearch = value;
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
                MedicationRequests = new ObservableCollection<NewMedicationRequest>(MedicationRequestsSearch.Where(equipment => Regex.IsMatch(equipment.Name, value)));
            }
            else
            {
                MedicationRequests = new ObservableCollection<NewMedicationRequest>(MedicationRequestsSearch);
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

        public RequestsMedicationViewModel()
        {
            controller = new NewMedicationRequestController();
            MedicationRequests = new ObservableCollection<NewMedicationRequest>(controller.GetAll());
            MedicationRequestsSearch = new ObservableCollection<NewMedicationRequest>(controller.GetAll());

            ViewCommand = new RelayCommand(o =>
            {
                if(SelectedRequest.Status == Status.PENDING)
                {
                    CurrentView = new ViewPending(new ViewPendingViewModel(new NewMedicationRequest(SelectedRequest)));
                }
                else if(SelectedRequest.Status == Status.REJECTED) 
                {
                    CurrentView = new ViewRejected(new ViewRejectedViewModel(new NewMedicationRequest(SelectedRequest)));
                }
            }, CheckIfSelected);

        }

        private bool CheckIfSelected(object obj)
        {
            return SelectedRequest != null;
        }

        public string GetTitle()
        {
            return "Requests";
        }

        public void Update()
        {
            MedicationRequests = new ObservableCollection<NewMedicationRequest>(controller.GetAll());
        }
    }
}
