using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PocclientApplication.ServiceReference;

namespace PocclientApplication
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        Service1Client client = new Service1Client();
        private void login_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (client.SelectLogin(numberTextBox.Text, passwordTextBox.Password) > 0)
                {
                    MainWindow newmainwindw = new MainWindow();
                    Application.Current.MainWindow = newmainwindw;
                    //System.Windows.Application.Current.Shutdown();
                    //this.Visibility = Visibility.Hidden;
                    this.Close();
                    newmainwindw.Show();
                    // Login newlogin = new Login();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void passwordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                login_Click(null, null);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            numberTextBox.Focus();
            if (DateTime.Now > DateTime.Parse("2016-8-10"))
            {
                MessageBox.Show("软件注册时间已到期，请与软件服务商联系");
            }
        }

        private void numberTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                passwordTextBox.Focus();
            }
        }

      
    }
}
