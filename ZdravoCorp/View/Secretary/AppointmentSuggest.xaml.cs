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
    /// Interaction logic for AppointmentSuggest.xaml
    /// </summary>
    public partial class AppointmentSuggest : Window
    {
        AppointmentController appointmentController;
        public ObservableCollection<Model.Appointment> AppointmentsCollection
        {
            get;
            set;
        }
        public AppointmentSuggest(List<Model.Appointment> appointments)
        {
            InitializeComponent();
            AppointmentsCollection = new ObservableCollection<Appointment>(appointments);
            SuggestionTable.DataContext = AppointmentsCollection;
        }

        private void CancelSuggestion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
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

            appointmentController.Create(app);
            Room r = app.Room;
            r.AddAppointment(app);
            rc.Update(r);
            Model.Doctor d = app.doctor;
            d.AddAppointment(app);
            doctorController.Update(d);
            Model.Patient p = app.Patient;
            p.AddAppointment(app);
            pc.Update(p);
            this.Close();
            
        }
    }
}
