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
    /// EditPWD.xaml 的交互逻辑
    /// </summary>
    public partial class EditPWD : UserControl
    {
        public EditPWD()
        {
            InitializeComponent();
        }
        Service1Client client = new Service1Client();
        string oldpassword;
        string userright;
        int login_id;
        string user;



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

        private void edit_grid_Loaded(object sender, RoutedEventArgs e)
        {
            var logindata = client.SelectLogin_id(PublicClass.loginid);
         
            for (int i = 0; i < logindata.Tables[0].Rows.Count; i++)
            {
                login_id = int.Parse(logindata.Tables[0].Rows[0][i].ToString());
                name.Text = logindata.Tables[0].Rows[0][i + 1].ToString();
                user = logindata.Tables[0].Rows[0][i + 2].ToString();
                oldpassword = logindata.Tables[0].Rows[0][i + 3].ToString();
                userright = logindata.Tables[0].Rows[0][i + 4].ToString();
            }
        }

        private void quedin_Click(object sender, RoutedEventArgs e)
        {
            if (old_pwd.Password != oldpassword)
            {
                MessageBox.Show("输入旧密码不正确", "提示");
            }
            else
            {
                if (new_pwd.Password == "")
                {
                    MessageBox.Show("密码不能为空", "提示");

                }
                else
                {
                    if (new_pwd.Password != com_pwd.Password)
                    {
                        MessageBox.Show("前后密码不一致", "提示");
                    }
                    else
                    {
                        client.Updatalogin(login_id, name.Text, user, new_pwd.Password, userright);

                        C1.WPF.C1Window findfixed = MainWindow.FindChild<C1.WPF.C1Window>(Application.Current.MainWindow, "editpassw");
                        if (findfixed != null)
                        {
                            findfixed.Close();
                        }
                    }
                }

            }




        }
    }
}
