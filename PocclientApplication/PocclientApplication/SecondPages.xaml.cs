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
    /// SecondPages.xaml 的交互逻辑
    /// </summary>
    public partial class SecondPages : UserControl
    {
        public SecondPages()
        {
            InitializeComponent();
        }

        Service1Client client = new Service1Client();
        public int selectcondition = 0;
        public int conditionid;


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
            C1.WPF.C1Window newcommon = new C1.WPF.C1Window();
            newcommon.IsResizable = false;
            newcommon.Name = "newcommon";
            newcommon.Width = 800;
            newcommon.Height = 450;
            newcommon.Header = "房产增加";
            newcommon.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 250, SystemParameters.PrimaryScreenHeight / 2d - 250, 0, 0);
            newcommon.Show();
            newcommon.ShowMaximizeButton = false;
            newcommon.ShowMinimizeButton = false;
            ThirdPages newaddcommon = new ThirdPages();
            newaddcommon.Selecthousecommonorid = selectcondition;
            newcommon.Content = newaddcommon;
        }
        //下一步
        private void nextpage_Click(object sender, RoutedEventArgs e)
        {
            nextpage();


            C1.WPF.C1Window findfixed = MainWindow.FindChild<C1.WPF.C1Window>(Application.Current.MainWindow, "newcondition");
            if (findfixed != null)
            {
                findfixed.Close();
            }

        }
        
        //增加
        //private void add_condition_Click(object sender, RoutedEventArgs e)
        //{
        //    if (selectcondition <= 0)
        //    {
                
        //       // house_condition_dataGrid.ItemsSource = client.Selectcondition(selectcondition).Tables[0].DefaultView;
        //    }
        //    else
        //    {
                
        //    }
        //}
        





    }

}
