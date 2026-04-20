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
using Model;

namespace ZdravoCorp.View.Doctor
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window, IDisposable
    {
        public UserWindow(Model.Doctor currentDoctor)
        {
            InitializeComponent();
            textBox1.Text = currentDoctor.Name;
            textBox2.Text = currentDoctor.Surname;
            textBox3.Text = currentDoctor.Address;
            textBox4.Text = currentDoctor.Username;
            textBox5.Text = currentDoctor.Email;
        }

        public void Dispose()
        {
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            CloseAllWindows();
            mainWindow.Show();
        }

        public void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Hide();
        }


    }
}
