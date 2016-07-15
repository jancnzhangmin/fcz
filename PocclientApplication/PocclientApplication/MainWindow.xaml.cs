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
using System.Data;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Themes;

using Microsoft.Reporting.WinForms;
using System.Collections;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.IO;

namespace PocclientApplication
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += F;
        }

        private void F(object o, System.ComponentModel.CancelEventArgs e)
        {
            beifei();
            //if (MessageBox.Show("关闭", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
            //    e.Cancel = true;
        }
        


        //
        private void beifei()
        {
            
            //StreamWriter test = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + @"\Autobak\", true);
            string a = string.Format("{0:yyyyMMddHHmmss}", System.DateTime.Now);

            string saveAway = System.AppDomain.CurrentDomain.BaseDirectory + Directory.CreateDirectory("Autobak") + @"\"+a+".bak";
            //SaveFileDialog sa = new SaveFileDialog();
            //sa.Filter = "数据库备份文件 (.bak)|*.bak";
            //if (sa.ShowDialog() == true)
            //{
            //    saveAway = sa.FileName;
            //}
            string cmdText = @"backup database pocdatabase to disk='" + saveAway + "'";
            BakReductSql(cmdText, true);
        }


        private void BakReductSql(string cmdText, bool isBak)
        {
            SqlCommand cmdBakRst = new SqlCommand();
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=master;Trusted_Connection=Yes");

            try
            {
                conn.Open();
                cmdBakRst.Connection = conn;
                cmdBakRst.CommandType = CommandType.Text;
                if (!isBak)     //如果是恢复操作
                {
                    string setOffline = "Alter database GroupMessage Set Offline With rollback immediate ";
                    string setOnline = " Alter database GroupMessage Set Online With Rollback immediate";
                    cmdBakRst.CommandText = setOffline + cmdText + setOnline;
                }
                else
                {
                    cmdBakRst.CommandText = cmdText;
                }
                cmdBakRst.ExecuteNonQuery();
                if (!isBak)
                {
                    MessageBox.Show("恭喜你，数据成功恢复为所选文档的状态！", "系统消息");
                }
                else
                {
                    MessageBox.Show("数据库备份成功！", "系统消息");
                }
            }
            catch (SqlException sexc)
            {
                MessageBox.Show("失败，可能是对数据库操作失败，原因：" + sexc, "数据库错误消息");
            }
            catch (Exception ex)
            {
                MessageBox.Show("对不起，操作失败，可能原因：" + ex, "系统消息");
            }
            finally
            {
                cmdBakRst.Dispose();
                conn.Close();
                conn.Dispose();
            }
        }

        private void huanyuan()
        {
            string openAway = "d:/pocdatabase.bak";//读取文件的路径
            string cmdText = @"restore database pocdatabase from disk='" + openAway + "'";
            BakReductSql(cmdText, false);
        }




        

        Service1Client client = new Service1Client();
        public string[] per_list;

        public static T FindChild<T>(DependencyObject parent, string childName)//查找控件
where T : DependencyObject
        {
            if (parent == null) return null;
            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // 如果子控件不是需查找的控件类型 
                T childType = child as T;
                if (childType == null)
                {
                    // 在下一级控件中递归查找 
                    foundChild = FindChild<T>(child, childName);
                    // 找到控件就可以中断递归操作  
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // 如果控件名称符合参数条件 
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // 查找到了控件 
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }

        #region 房屋所有权按钮和事件
        //添加房屋所有权
        private void addhouse_Click(object sender, RoutedEventArgs e)
        {
             //C1.WPF.C1Window findfixed = MainWindow.FindChild<C1.WPF.C1Window>(Application.Current.MainWindow, "newfixed");
             //if (findfixed != null)
             //{
             //    findfixed.IsActive = true;
             //}
             //else
             //{
                 //C1.WPF.C1Window newhouse = new C1.WPF.C1Window();
                 //newhouse.IsResizable = false;
                 //newhouse.Name = "newhouse";
                 //newhouse.Width = 1000;
                 //newhouse.Height = 950;
                 //newhouse.Header = "房产增加";
                 //newhouse.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 250, SystemParameters.PrimaryScreenHeight / 2d - 250, 0, 0);
                 //newhouse.Show();
                 //newhouse.ShowMaximizeButton = false;
                 //newhouse.ShowMinimizeButton = false;
                 //Addhouse newaddhouse = new Addhouse();
                 //newhouse.Content = newaddhouse;
            
             //}






            Addhouse newaddhouse = MainWindow.FindChild<Addhouse>(Application.Current.MainWindow, "newAddHouse");
            if (newaddhouse != null)
                 {
                     foreach (var t in mainpanel.Children)
                     {
                         if (t.Title == "添加房产证")
                         {
                             t.IsActive = true;
                             break;
                         }
                     }
                 }
                 else
                 {
                     ScrollViewer scrollv = new ScrollViewer();
                     scrollv.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                     Addhouse newAddHouse = new Addhouse();
                     //newEffective.Margin = new Thickness(0, 20, 0, 0);
                     LayoutDocument newreport = new LayoutDocument();
                     newAddHouse.visible = "house";
                     //newSelectLande.Width = menu.ActualWidth;
                     newAddHouse.HorizontalAlignment = HorizontalAlignment.Stretch;
                     newAddHouse.HorizontalAlignment = HorizontalAlignment.Center;
                     //newSelectLande.land_dataGrid.Width = menu.ActualWidth;
                     newreport.Title = "添加房产证";
                     newreport.IsActive = true;
                     scrollv.Content = newAddHouse;
                     newreport.Content = scrollv;
                     newAddHouse.Name = "newAddHouse";
                     mainpanel.Children.Add(newreport);

                 }




        }

 

        

        
        #endregion

        #region 土地按钮和事件
        //增加土地
        private void addland_Click(object sender, RoutedEventArgs e)
        {
            //C1.WPF.C1Window newland = new C1.WPF.C1Window();
            //newland.IsResizable = false;
            //newland.Name = "newland";
            //newland.Width = 1000;
            //newland.Height = 800;
            //newland.Header = "土地增加";
            //newland.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 250, SystemParameters.PrimaryScreenHeight / 2d - 250, 0, 0);
            //newland.Show();
            //newland.ShowMaximizeButton = true;
            //newland.ShowMinimizeButton = true;
            
            //AddLand newaddland = new AddLand();
            //newland.Content = newaddland;




            AddLand newaddland = MainWindow.FindChild<AddLand>(Application.Current.MainWindow, "newAddLand");
            if (newaddland != null)
            {
                foreach (var t in mainpanel.Children)
                {
                    if (t.Title == "添加土地使用证")
                    {
                        t.IsActive = true;
                        break;
                    }
                }
            }
            else
            {
                ScrollViewer scrollv = new ScrollViewer();
                scrollv.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                AddLand newAddLand = new AddLand();
                newAddLand.visible = "land";
                //newEffective.Margin = new Thickness(0, 20, 0, 0);
                LayoutDocument newreport = new LayoutDocument();
                //newSelectLande.Width = menu.ActualWidth;
                newAddLand.HorizontalAlignment = HorizontalAlignment.Stretch;
                newAddLand.HorizontalAlignment = HorizontalAlignment.Center;
                //newSelectLande.land_dataGrid.Width = menu.ActualWidth;
                newreport.Title = "添加土地证";
                
                newreport.IsActive = true;
                scrollv.Content = newAddLand;
                newreport.Content = scrollv;
                newAddLand.Name = "newAddLand";
                mainpanel.Children.Add(newreport);
            }











        }

        private void selectland_Click(object sender, RoutedEventArgs e)
        {
            SelectLand findfix = MainWindow.FindChild<SelectLand>(Application.Current.MainWindow, "newSelectLande");
            if (findfix != null)
            {
                foreach (var t in mainpanel.Children)
                {
                    if (t.Title == "土地查询")
                    {
                        t.IsActive = true;
                        break;
                    }
                }
            }
            else
            {
                SelectLand newSelectLande = new SelectLand();
                //newEffective.Margin = new Thickness(0, 20, 0, 0);
                LayoutDocument newreport = new LayoutDocument();
                //newSelectLande.Width = menu.ActualWidth;
                newSelectLande.HorizontalAlignment = HorizontalAlignment.Stretch;
                //newSelectLande.land_dataGrid.Width = menu.ActualWidth;
                newreport.Title = "土地查询";
                newreport.IsActive = true;
                newreport.Content = newSelectLande;
                newSelectLande.Name = "newSelectLande";
                mainpanel.Children.Add(newreport);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
           // Report newl = new Report();
            //HouseReport newreport = new HouseReport();
            //newreport.Title = "报表";
            //newreport.IsActive = true;
            //newreport.Content = newl;
            //newl.Name = "newSelectLande";
            //mainpanel.Children.Add(newreport);

         

    
        }
        #endregion

        

        private void selecthouse_Click(object sender, RoutedEventArgs e)
        {
            SelectHouse newSelectHouse = new SelectHouse();
            //newEffective.Margin = new Thickness(0, 20, 0, 0);
            LayoutDocument newreport = new LayoutDocument();
            newreport.Title = "房屋查询";
            newreport.IsActive = true;
            newreport.Content = newSelectHouse;
            newSelectHouse.Name = "newSelectHouse";
            mainpanel.Children.Add(newreport);
        }

        private void Toolbar_loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }

            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }

        private void help_Click(object sender, RoutedEventArgs e)
        {

        }

        private void memuabout_Click(object sender, RoutedEventArgs e)
        {
            C1.WPF.C1Window cabout = new C1.WPF.C1Window();
            cabout.Name = "about";
            cabout.IsResizable = false;
            cabout.Width = 636;
            cabout.Height = 368;
            cabout.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 318, SystemParameters.PrimaryScreenHeight / 2d - 174, 0, 0);
            about ccabout = new about();
            cabout.Content = ccabout;
            cabout.Show();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            C1.WPF.C1Window zhuce = new C1.WPF.C1Window();
            zhuce.Name = "newRegister";
            zhuce.IsResizable = false;
            zhuce.Height = 300;
            zhuce.Width = 700;
            zhuce.Header = "注册";
            zhuce.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 318, SystemParameters.PrimaryScreenHeight / 2d - 174, 0, 0);
            Register ccabout = new Register();
            zhuce.Content = ccabout;
            zhuce.Show();
        }


        //权限
        private void permissions()
        {

            

            string pem = client.SelectLoginpem(PublicClass.loginid);
            per_list = pem.Split('f');



            foreach (string s in per_list)
            {
                if (s == "3")
                {
                    addhouse.IsEnabled = true;
                    addland.IsEnabled = true;
                }
                if (s == "6")
                {
                    fix_add.IsEnabled = true;
                    fixed_transfers.IsEnabled = true;
                }
                if (s == "9")
                {
                    account.IsEnabled = true;
                }
                //if (s == "10")
                //{
                //    register.IsEnabled = true;
                //}
                if (s == "11")
                {
                    backup.IsEnabled = true;
                }



            }
        }

        private void backup_Click(object sender, RoutedEventArgs e)
        {
            //Backup newBackup = new Backup();
            //LayoutDocument newreport = new LayoutDocument();
            //newreport.Title = "数据管理";
            //newreport.IsActive = true;
            //newreport.Content = newBackup;
            //newBackup.Name = "newBackup";
            //mainpanel.Children.Add(newreport);
            databack newbak = new databack();
            LayoutDocument newdoc = new LayoutDocument();
            newdoc.Title = "数据操作";
            newdoc.IsActive = true;
            newdoc.Content = newbak;
            mainpanel.Children.Add(newdoc);
        }

        private void account_Click(object sender, RoutedEventArgs e)
        {
            Userlist newUserlist = new Userlist();
            LayoutDocument newreport = new LayoutDocument();
            newreport.Title = "用户管理";
            newreport.IsActive = true;
            newreport.Content = newUserlist;
            newUserlist.Name = "newUserlist";
            mainpanel.Children.Add(newreport);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            permissions();
        }

        private void editpwd_Click(object sender, RoutedEventArgs e)
        {
            C1.WPF.C1Window editpassw = new C1.WPF.C1Window();
            editpassw.Name = "editpassw";
            editpassw.IsResizable = false;
            editpassw.Height = 250;
            editpassw.Width = 300;
            editpassw.Header = "修改密码";
            editpassw.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 318, SystemParameters.PrimaryScreenHeight / 2d - 174, 0, 0);
            EditPWD neweditpwd = new EditPWD();
            editpassw.Content = neweditpwd;
            editpassw.Show();
        }

        private void zhuxiao_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Login newlogin = new Login();
            newlogin.Show();
        }




    }
}
