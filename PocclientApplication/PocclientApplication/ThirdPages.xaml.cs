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
using Microsoft.Win32;
using System.IO;
using System.Data;

namespace PocclientApplication
{
    /// <summary>
    /// ThirdPages.xaml 的交互逻辑
    /// </summary>
    public partial class ThirdPages : UserControl
    {
        public ThirdPages()
        {
            InitializeComponent();
        }

        Service1Client client = new Service1Client();
        public int Selecthousecommonorid = 0;
        public int commonorid;



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

        //下一步添加信息
        private void nextpage()
        {
            C1.WPF.C1Window newobligee = new C1.WPF.C1Window();
            newobligee.IsResizable = false;
            newobligee.Name = "newobligee";
            newobligee.Width = 800;
            newobligee.Height = 450;
            newobligee.Header = "房产增加";
            newobligee.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 250, SystemParameters.PrimaryScreenHeight / 2d - 250, 0, 0);
            newobligee.Show();
            newobligee.ShowMaximizeButton = false;
            newobligee.ShowMinimizeButton = false;
            FourthPages newaddobligee = new FourthPages();
            newaddobligee.Selectobligeeid = Selecthousecommonorid;
            newobligee.Content = newaddobligee;
        }
        //增加契税摘要并跳转下一页
        private void nextpages_Click(object sender, RoutedEventArgs e)
        {
            
            
            nextpage();


            C1.WPF.C1Window findfixed = MainWindow.FindChild<C1.WPF.C1Window>(Application.Current.MainWindow, "newcommon");
            if (findfixed != null)
            {
                findfixed.Close();
            }

        }
        //增加共有权保持证
        private void add_commonor_Click(object sender, RoutedEventArgs e)
        {
            
        }
        //查询共有权保持证
        private void third_Loaded(object sender, RoutedEventArgs e)
        {
            

        }
        
    }
}
