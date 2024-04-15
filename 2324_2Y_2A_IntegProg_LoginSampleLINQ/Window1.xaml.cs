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

namespace _2324_2Y_2A_IntegProg_LoginSampleLINQ
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        LoginSampleDataContext _lsDC = null;
        public Window1()
        {
            InitializeComponent();

            _lsDC = new LoginSampleDataContext(
                Properties.Settings.Default._2324_1A_LoginSampleConnectionString);

            welcome_label.Content = $"WELCOME, {MainWindow.userName}";
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            string newUserName = userName.Text;

            var userQuery = from user in _lsDC.LoginUsers
                             where user.Name == MainWindow.userName
                             select user;

            if (userQuery.Count() == 1)
            {
                foreach(var login in userQuery)
                {
                    Log log = new Log();
                    login.Name = newUserName;
                    log.Activities = "Change";

                    _lsDC.Logs.InsertOnSubmit(log);
                    _lsDC.SubmitChanges();
                }
                

                welcome_label.Content = $"WELCOME, {newUserName}";
            }
            else
            {
                MessageBox.Show("User not found in the database.");
            }
        }
    }
}
