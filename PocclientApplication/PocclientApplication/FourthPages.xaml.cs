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
    /// FourthPages.xaml 的交互逻辑
    /// </summary>
    public partial class FourthPages : UserControl
    {
        public FourthPages()
        {
            InitializeComponent();
        }

        Service1Client client = new Service1Client();
        public int Selectobligeeid = 0;
        public int oblid;

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

        //增加使用土地摘要并完成
        private void complete_Click(object sender, RoutedEventArgs e)
        {
            //client.Inserthouseuseland(use_area.Text, company.Text, card_number.Text, zidihao.Text, PublicClass.houseid);
            //if (Selectobligeeid > 0)
            //{
            //    client.Updataohouseuseland(use_area.Text, company.Text, card_number.Text, zidihao.Text, Selectobligeeid);
            //}



            C1.WPF.C1Window findfixed = MainWindow.FindChild<C1.WPF.C1Window>(Application.Current.MainWindow, "newobligee");
            if (findfixed != null)
            {
                findfixed.Close();
            }

        }
        //增加设定他项权利摘要
        private void obligee_button_Click(object sender, RoutedEventArgs e)
        {
            //if (Selectobligeeid <= 0)
            //{
            //    client.Inserthouseobligee(obligee.Text, type.Text, room_number.Text, jianshu.Text, float.Parse(built_up_area.Text), right_value.Text, duration_right.Text, DateTime.Parse(logout_date.Text), PublicClass.houseid);
            //}
            //else
            //{
            //    client.Updataobligee(obligee.Text, type.Text, room_number.Text, jianshu.Text, float.Parse(built_up_area.Text), right_value.Text, duration_right.Text, DateTime.Parse(logout_date.Text),Selectobligeeid,oblid);
            //}
        }
        //查询设定他项权利摘要
        private void obligee_dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //if (Selectobligeeid > 0)
            //{
            //    obligee_dataGrid.ItemsSource = client.Selectobligee(Selectobligeeid).Tables[0].DefaultView;

            //    var dateob = client.Selecthouseuselandid(Selectobligeeid);

            //    for (int i = 0; i < dateob.Tables[0].Rows.Count; i++)
            //        {
            //            use_area.Text = dateob.Tables[0].Rows[0][i].ToString();
            //            company.Text = dateob.Tables[0].Rows[0][i + 1].ToString();
            //            card_number.Text = dateob.Tables[0].Rows[0][i + 2].ToString();
            //            zidihao.Text = dateob.Tables[0].Rows[0][i + 3].ToString();
                       


            //        }
                
            //}
        }
        ////编辑设定他项权利摘要
        //private void edit_list_Click(object sender, RoutedEventArgs e)
        //{
        //    var t = obligee_dataGrid.SelectedItem;
        //    var b = t as DataRowView;
        //    int s = int.Parse(b.Row[0].ToString());
        //    oblid = s;
        //    var shuju = client.Selectobligeeid(s);
        //    for (int i = 0; i < shuju.Tables[0].Rows.Count; i++)
        //    {
        //        obligee.Text = shuju.Tables[0].Rows[0][i].ToString();
        //        type.Text = shuju.Tables[0].Rows[0][i + 1].ToString();
        //        room_number.Text = shuju.Tables[0].Rows[0][i + 2].ToString();
        //        jianshu.Text = shuju.Tables[0].Rows[0][i + 3].ToString();
        //        built_up_area.Text = shuju.Tables[0].Rows[0][i + 4].ToString();
        //        right_value.Text = shuju.Tables[0].Rows[0][i + 5].ToString();
        //        duration_right.Text = shuju.Tables[0].Rows[0][i + 6].ToString();
        //        logout_date.Text = shuju.Tables[0].Rows[0][i + 7].ToString();

          
        //    }

        //}
        ////删除设定他项权利摘要
        //private void delete_list_Click(object sender, RoutedEventArgs e)
        //{
        //    var t = obligee_dataGrid.SelectedItem;
        //    var b = t as DataRowView;
        //    int s = int.Parse(b.Row[0].ToString());
        //    client.Deleteobligee(s);
        //    obligee_dataGrid_Loaded(null, null);
        //}



    }
}
