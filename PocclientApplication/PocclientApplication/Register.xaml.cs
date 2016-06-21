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
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
        }

        Service1Client client = new Service1Client();

        private void register_Click(object sender, RoutedEventArgs e)
        {
            if (client.SelectLoginname(login_name.Text) == login_name.Text)
            {
                MessageBox.Show("此用户名已存在", "提示");
            }
            else
            {
                if (password.Password != affirm_password.Password)
                {
                    MessageBox.Show("前后密码不一致", "提示");
                }
                else
                {
                    client.Insertlogin(name.Text, login_name.Text, password.Password, "");
                }
            }
        }
    }
}
