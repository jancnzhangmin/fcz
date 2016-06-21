﻿using System;
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
    /// LandCountryside.xaml 的交互逻辑
    /// </summary>
    public partial class LandCountryside : UserControl
    {
        public LandCountryside()
        {
            InitializeComponent();
        }
        Service1Client client = new Service1Client();
        public int landid = 0;
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

        private void nextopen()
        {
            C1.WPF.C1Window newtown = new C1.WPF.C1Window();
            newtown.IsResizable = false;
            newtown.Name = "newtown";
            newtown.Width = 800;
            newtown.Height = 480;
            newtown.Header = "城镇土地";
            newtown.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 250, SystemParameters.PrimaryScreenHeight / 2d - 250, 0, 0);
            newtown.Show();
            newtown.ShowMaximizeButton = false;
            newtown.ShowMinimizeButton = false;
            LandTown newlandtown = new LandTown();
            newlandtown.landid = landid;
            newtown.Content = newlandtown;
        }

  


    }
}
