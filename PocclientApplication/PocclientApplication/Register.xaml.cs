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
        public string[] per_list;
        string permsis = "";
        public int loginid = 0;


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


        private void register_Click(object sender, RoutedEventArgs e)
        {




            if (loginid <= 0)
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
                        ischeck();
                        client.Insertlogin(name.Text, login_name.Text, password.Password, permsis);
                    }
                }
            }
                //更新
            else
            {
                ischeck();
                client.Updatalogin(loginid, name.Text, login_name.Text, password.Password, permsis);
            }

            //Button button = sender as Button;
            //Window tarWindow = Window.GetWindow(button);
            //tarWindow.Close();
            C1.WPF.C1Window findfixed = MainWindow.FindChild<C1.WPF.C1Window>(Application.Current.MainWindow, "newRegister");
            if (findfixed != null)
            {
                findfixed.Close();
                
            }
            Userlist kkkk = MainWindow.FindChild<Userlist>(Application.Current.MainWindow, "newUserlist");
            if (kkkk != null)
            {
                kkkk.employeesDataGrid_Loaded(null, null);

            }
            
            

        }

        private void check_all_Click(object sender, RoutedEventArgs e)
        {
            radioButton3.IsChecked = true;
            radioButton4.IsChecked = true;
            radioButton5.IsChecked = true;
            radioButton6.IsChecked = true;
            radioButton7.IsChecked = true;
            radioButton8.IsChecked = true;
            radioButton9.IsChecked = true;
            //radioButton10.IsChecked = true;
            radioButton11.IsChecked = true;


        }


        private void load_check()
        {
           string pem = client.SelectLoginpem(PublicClass.loginid);
           per_list = pem.Split('f');



           foreach (string s in per_list)
            {
                if (s == "3")
                {
                    radioButton3.IsChecked = true;
                }
                if (s == "4")
                {
                    radioButton4.IsChecked = true;
                }
                if (s == "5")
                {
                    radioButton5.IsChecked = true;
                }
                if (s == "6")
                {
                    radioButton6.IsChecked = true;
                }
                if (s == "7")
                {
                    radioButton7.IsChecked = true;
                }
                if (s == "8")
                {
                    radioButton8.IsChecked = true;
                }
                if (s == "9")
                {
                    radioButton9.IsChecked = true;
                }
                //if (s == "10")
                //{
                //    radioButton10.IsChecked = true;
                //}
                if (s == "11")
                {
                    radioButton11.IsChecked = true;
                }
   
                


            }

        }

        private void ischeck()
        {
            string p;

            if ((bool)radioButton3.IsChecked)
            {
                p = "3f";
                permsis += p;
            }
            if ((bool)radioButton4.IsChecked)
            {
                p = "4f";
                permsis += p;
            }
            if ((bool)radioButton5.IsChecked)
            {
                p = "5f";
                permsis += p;
            }
            if ((bool)radioButton6.IsChecked)
            {
                p = "6f";
                permsis += p;
            }
            if ((bool)radioButton7.IsChecked)
            {
                p = "7f";
                permsis += p;
            }
            if ((bool)radioButton8.IsChecked)
            {
                p = "8f";
                permsis += p;
            }
            if ((bool)radioButton9.IsChecked)
            {
                p = "9f";
                permsis += p;
            }
            //if ((bool)radioButton10.IsChecked)
            //{
            //    p = "10f";
            //    permsis += p;
            //}
            if ((bool)radioButton11.IsChecked)
            {
                p = "11f";
                permsis += p;
            }


        }


        private void quanxianbutton(string pem)
        {

            per_list = pem.Split('f');



            foreach (string s in per_list)
            {
                if (s == "3")
                {
                    radioButton3.IsChecked = true;
                }
                if (s == "4")
                {
                    radioButton4.IsChecked = true;
                }
                if (s == "5")
                {
                    radioButton5.IsChecked = true;
                }
                if (s == "6")
                {
                    radioButton6.IsChecked = true;
                }
                if (s == "7")
                {
                    radioButton7.IsChecked = true;
                }
                if (s == "8")
                {
                    radioButton8.IsChecked = true;
                }
                if (s == "9")
                {
                    radioButton9.IsChecked = true;
                }
                //if (s == "10")
                //{
                //    radioButton10.IsChecked = true;
                //}
                if (s == "11")
                {
                    radioButton11.IsChecked = true;
                }




            }
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (loginid > 0)
            {


                var logindata = client.SelectLogin_id(loginid);
                string pems = "";
                for (int i = 0; i < logindata.Tables[0].Rows.Count; i++)
                {
                    name.Text = logindata.Tables[0].Rows[0][i + 1].ToString();
                    login_name.Text = logindata.Tables[0].Rows[0][i + 2].ToString();
                    password.Password = logindata.Tables[0].Rows[0][i + 3].ToString();
                    pems = logindata.Tables[0].Rows[0][i + 4].ToString();


                }
                affirm_password.Password = password.Password;
                quanxianbutton(pems);
            }
        }

    }
}
