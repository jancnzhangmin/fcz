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
        }
        Service1Client client = new Service1Client();

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
                    if (t.Title == "土地使用")
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
                newreport.Title = "土地证";
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
            newreport.Title = "房屋使用";
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
            cabout.IsResizable = false;
            cabout.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 318, SystemParameters.PrimaryScreenHeight / 2d - 174, 0, 0);
            about ccabout = new about();
            cabout.Content = ccabout;
            cabout.Show();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            C1.WPF.C1Window zhuce = new C1.WPF.C1Window();
            zhuce.IsResizable = false;
            zhuce.Height = 250;
            zhuce.Width = 400;
            zhuce.Header = "注册";
            zhuce.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 318, SystemParameters.PrimaryScreenHeight / 2d - 174, 0, 0);
            Register ccabout = new Register();
            zhuce.Content = ccabout;
            zhuce.Show();
        }



    }
}
