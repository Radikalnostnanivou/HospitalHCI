using Controller;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoCorp.Service;
using ZdravoCorp.View.Core;
using ZdravoCorp.View.Doctor;
using ZdravoCorp.View.Manager;
using ZdravoCorp.View.Patient;
using ZdravoCorp.View.Secretary;

namespace ZdravoCorp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TimerService timerService;
        private AutoResetEvent autoEvent;
        private bool anotherWindow = false;

        public ObservableCollection<Model.Doctor> DoctorCollection
        {
            get;
            set;
        }
        public MainWindow()
        {
            if (Properties.Settings.Default.ColorMode)
            {
                ThemesController.SetThemeStartup(ThemesController.ThemeTypes.Light);
            }
            else
            {
                ThemesController.SetThemeStartup(ThemesController.ThemeTypes.Dark);
            }
            InitializeComponent();
            autoEvent = new AutoResetEvent(false);
            timerService = new TimerService(autoEvent);
            Thread timer = new Thread(new ThreadStart(timerService.initiate));
            timer.Start();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Drag_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = sender as PasswordBox;
            pb.Tag = (!string.IsNullOrEmpty(pb.Password)).ToString();
        }


        private void CloseWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!anotherWindow)
            {
                autoEvent.Set();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = User.Text;
            string password = PassBox.Password;

            try
            {
                User user = LoginService.Instance.Login(username, password);
                if(user is Model.Manager)
                {
                    View.Manager.Manager managerWindow = new View.Manager.Manager(autoEvent);
                    anotherWindow = true;
                    this.Close();
                    managerWindow.ShowDialog();
                }
                else if (user is Model.Patient patient)
                {

                    if (patient.CanLog)
                    {
                        View.Patient.Patient patientWindow = new View.Patient.Patient(patient);
                        this.Close();
                        //patientWindow.ResizeMode = ResizeMode.NoResize;
                        patientWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Zabranjen pristup nalogu. \nZa vraćanje pristupa, molimo da se obratite sekretaru.", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else if (user is Model.Doctor doctor)
                {
                    Appointments appointmentWindow = new Appointments(doctor);
                    this.Close();
                    appointmentWindow.ShowDialog();
                    return;
                }
                else if (user is Model.Secretary secretary)
                {
                    View.Secretary.Secretary secretaryWindow = new View.Secretary.Secretary(secretary);
                    this.Close();
                    secretaryWindow.ShowDialog();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pogrešan username ili password.\nPokušajte opet.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }   
}
