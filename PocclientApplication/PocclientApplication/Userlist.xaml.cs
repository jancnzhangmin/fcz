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
using System.Data;
using PocclientApplication.ServiceReference;
using PocclientApplication;

namespace PocclientApplication
{
    /// <summary>
    /// Userlist.xaml 的交互逻辑
    /// </summary>
    public partial class Userlist : UserControl
    {
        public Userlist()
        {
            InitializeComponent();

        }

        Service1Client client = new Service1Client();
        


        private void add_user_Click(object sender, RoutedEventArgs e)
        {
            C1.WPF.C1Window newland = new C1.WPF.C1Window();
            newland.IsResizable = false;
            newland.Name = "newRegister";
            newland.Width = 700;
            newland.Height = 300;
            newland.Header = "增加";
            newland.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 500, SystemParameters.PrimaryScreenHeight / 2d - 400, 0, 0);
            newland.Show();
            newland.ShowMaximizeButton = false;
            newland.ShowMinimizeButton = false;
            Register newaddRegister = new Register();
            newaddRegister.Name = "newRegister";
            newland.Content = newaddRegister;

            //newaddRegister.next_page.Content = "完成修改";


        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {

            var t = employeesDataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());
            string edit_num = b.Row[2].ToString();

            if (edit_num == "admin")
            {
                MessageBox.Show("无法编辑管理员", "提示");
            }
            else
            {
            C1.WPF.C1Window newland = new C1.WPF.C1Window();
            newland.IsResizable = false;
            newland.Name = "newRegister";
            newland.Width = 700;
            newland.Height = 300;
            newland.Header = "编辑";
            newland.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 500, SystemParameters.PrimaryScreenHeight / 2d - 400, 0, 0);
            newland.Show();
            newland.ShowMaximizeButton = false;
            newland.ShowMinimizeButton = false;
            Register newaddRegister = new Register();
            newland.Content = newaddRegister;
            newaddRegister.loginid = s;
           
                employeesDataGrid_Loaded(null, null);

                //Public.user_id = s;

             
               
          
     

            }
        }




        private void delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmToDel = MessageBox.Show("确认要删除所选行吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmToDel == MessageBoxResult.Yes)
            {
                var t = employeesDataGrid.SelectedItem;
                var b = t as DataRowView;
                int s = int.Parse(b.Row[0].ToString());
                string num = b.Row[2].ToString();
                


                if (num == "admin")
                {
                    MessageBox.Show("无法删除管理员", "提示");
                }
                else
                {
                    client.Deletelogin(s);
                }
                employeesDataGrid_Loaded(null, null);
            }
            else
            {
                //此处加不删除的操作
            }

            
        }

        public void employeesDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            employeesDataGrid.ItemsSource = client.Selectloginuser().Tables[0].DefaultView;
            employeesDataGrid.CanUserAddRows = false;

            

            //employeesDataGrid.LoadingRow += new EventHandler<DataGridRowEventArgs>(employeesDataGrid_LoadingRow);
        }


    

        //void employeesDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        //{
        //    e.Row.Header = e.Row.GetIndex() + 1;
        //}
    }
}
