using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Controller;
using System.Collections.ObjectModel;
using Model;
using System.ComponentModel;

namespace ZdravoCorp.View.Secretary
{
    /// <summary>
    /// Interaction logic for AppointmentsSuggestChange.xaml
    /// </summary>
    public partial class AppointmentSuggestChange : Window
    {
        AppointmentController appointmentController;
        private Model.Room exRoom { get; set; }
        private Model.Appointment exApp { get; set; }
        public ObservableCollection<Model.Appointment> AppointmentsCollection
        {
            get;
            set;
        }
        public AppointmentSuggestChange(List<Model.Appointment> appointments, Model.Room exRoom, Model.Appointment exApp)
        {
            InitializeComponent();
            AppointmentsCollection = new ObservableCollection<Appointment>(appointments);
            SuggestionTable.DataContext = AppointmentsCollection;
            this.exRoom = exRoom;
            this.exApp = exApp;
        }

        private void CancelSuggestionEdit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ChangeAppointment_Click(object sender, RoutedEventArgs e)
        {
            appointmentController = new AppointmentController();
            
            if (SuggestionTable.SelectedIndex == -1)
            {
                return;
            }
            Model.Appointment app = AppointmentsCollection.ElementAt(SuggestionTable.SelectedIndex);
            PatientController pc = new PatientController();

            RoomController rc = new RoomController();

            DoctorController doctorController = new DoctorController();
            DateTime date = app.startDate;
            app.Id = exApp.Id;
            exRoom.RemoveAppointment(exApp);
            rc.Update(exRoom);
            
            appointmentController.Update(app);
            Room r = app.Room;
            r.AddAppointment(app);
            rc.Update(r);
            
            this.Close();

        }

        
    }
}
