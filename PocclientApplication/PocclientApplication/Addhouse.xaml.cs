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
using System.Drawing.Printing;
using System.Drawing.Imaging;
using Microsoft.Reporting.WinForms;

namespace PocclientApplication
{
    /// <summary>
    /// Addhouse.xaml 的交互逻辑
    /// </summary>
    public partial class Addhouse : UserControl
    {
        public Addhouse()
        {
            InitializeComponent();
        }
        Service1Client client = new Service1Client();
        public int selecthouseid = 0;
        public int updatenatureid;
        public int updatelocatedid;

        public int selectcondition = 0;
        public int conditionid=0;

        public int Selecthousecommonorid = 0;
        public int commonorid=0;

        public int Selectobligeeid = 0;
        public int oblid;
        public string visible;


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



        private void addhousegrid_Loaded(object sender, RoutedEventArgs e)
        {


            if (visible == "house")
            {
                printreport_house.Visibility = Visibility.Hidden;
            }
            add_condition.IsEnabled = false;
            add_commonor.IsEnabled = false;
            obligee_button.IsEnabled = false;
        
           
            houselicensingdate.Text = System.DateTime.Now.ToString();
            house_tianfatime.Text = System.DateTime.Now.ToString();
            //deddatetime.Text = System.DateTime.Now.ToString();
            //obl_logout_date.Text = System.DateTime.Now.ToString();

            house_nature.DataContext = client.Selecthousenature().Tables[0].DefaultView;
            house_located.DataContext = client.Selecthouselocated().Tables[0].DefaultView;
            if (selecthouseid > 0)
            {
                add_condition.IsEnabled = true;
                add_commonor.IsEnabled = true;
                obligee_button.IsEnabled = true;

                add_condition.Content = "完成修改";
                add_commonor.Content = "完成修改";
                obligee_button.Content = "完成修改";


                var shuju = client.Selecthouseid(selecthouseid);
                for (int i = 0; i < shuju.Tables[0].Rows.Count; i++)
                {
                    house_word.Text = shuju.Tables[0].Rows[0][i].ToString();
                    house_number.Text = shuju.Tables[0].Rows[0][i + 1].ToString();
                    house_owner.Text = shuju.Tables[0].Rows[0][i + 2].ToString();
                    int natureid = int.Parse(shuju.Tables[0].Rows[0][i + 3].ToString());

                    house_nature.Text = client.selecenatureID(natureid);

                    idcardnumber.Text = shuju.Tables[0].Rows[0][i + 4].ToString();
                    //int locatedid = int.Parse(shuju.Tables[0].Rows[0][i + 5].ToString());
                    house_located.Text = shuju.Tables[0].Rows[0][i + 5].ToString();
                    house_dihao.Text = shuju.Tables[0].Rows[0][i + 6].ToString();
                    house_postscript.Text = shuju.Tables[0].Rows[0][i + 7].ToString();
                    house_witness.Text = shuju.Tables[0].Rows[0][i + 8].ToString();
                    house_proofreader.Text = shuju.Tables[0].Rows[0][i + 9].ToString();
                    house_autograph.Text = shuju.Tables[0].Rows[0][i + 10].ToString();
                    houselicensingdate.Text = shuju.Tables[0].Rows[0][i + 11].ToString();
                    house_office.Text = shuju.Tables[0].Rows[0][i + 12].ToString();
                    house_banzhenren.Text = shuju.Tables[0].Rows[0][i + 13].ToString();
                    house_tianfatime.Text = shuju.Tables[0].Rows[0][i + 14].ToString();
                    house_figure.Text = shuju.Tables[0].Rows[0][i + 15].ToString();

                }
                updatenatureid = client.selecenayureID(house_nature.Text);

                var houdededtax = client.Selecthousedeedtaxid(selecthouseid);
                for (int i = 0; i < houdededtax.Tables[0].Rows.Count; i++)
                {
                    deddatetime.Text = houdededtax.Tables[0].Rows[0][i].ToString();
                    dedprice.Text = houdededtax.Tables[0].Rows[0][i + 1].ToString();
                    dedtype.Text = houdededtax.Tables[0].Rows[0][i + 2].ToString();
                    dedtax_rate.Text = houdededtax.Tables[0].Rows[0][i + 3].ToString();
                    dedamount_money.Text = houdededtax.Tables[0].Rows[0][i + 4].ToString();
                    dedtaxremarks.Text = houdededtax.Tables[0].Rows[0][i + 5].ToString();

                }

                var houseuseland = client.Selecthouseuselandid(selecthouseid);
                for (int i = 0; i < houseuseland.Tables[0].Rows.Count; i++)
                {
                    landuse_area.Text = houseuseland.Tables[0].Rows[0][i].ToString();
                    //landcompany.Text = houseuseland.Tables[0].Rows[0][i + 1].ToString();
                    landcard_number.Text = houseuseland.Tables[0].Rows[0][i + 2].ToString();
                    //landzidihao.Text = houseuseland.Tables[0].Rows[0][i + 3].ToString();
                }




                house_condition_dataGrid.ItemsSource = client.Selectcondition(selecthouseid).Tables[0].DefaultView;
                {
                    var t = house_condition_dataGrid.Items[0];
                    var b = t as DataRowView;
                    if (b.Row[1].ToString() == "")
                    {
                        int s = int.Parse(b.Row[0].ToString());
                        conditionid = s;
                    }
                }
                commonor_dataGrid.ItemsSource = client.Selecthousecommonor(selecthouseid).Tables[0].DefaultView;
                {
                    var t = commonor_dataGrid.Items[0];
                    var b = t as DataRowView;
                    if (b.Row[1].ToString() == "")
                    {
                        int s = int.Parse(b.Row[0].ToString());
                        commonorid = s;
                    }
                }

                obligee_dataGrid.ItemsSource = client.Selectobligee(selecthouseid).Tables[0].DefaultView;
                {
                    var t = obligee_dataGrid.Items[0];
                    var b = t as DataRowView;
                    if (b.Row[1].ToString() == "")
                    {
                        int s = int.Parse(b.Row[0].ToString());
                        oblid = s;
                    }
                }

            }


           
            
          
        }

        private bool checknull()
        {
            bool check_status = true;
            string status_err = "";
            if (house_nature.Text == "")
            {
                check_status = false;
                status_err += "所有权性质 ";
            }

            if (house_word.Text == "")
            {
                check_status = false;
                status_err += "字 ";
            }

            if (house_number.Text == "")
            {
                check_status = false;
                status_err += "号 ";
            }


            if (!check_status)
            {
                MessageBox.Show(status_err + "不能为空！");
            }
            return check_status;

        }
        private void checkmultitable()
        {
            if (client.checkhousenature(house_nature.Text.Trim()) == 0)
            {
                client.Inserthousenature(house_nature.Text.Trim());
            }
            //if (client.checkhouselocated(house_located.Text.Trim()) == 0)
            //{
            //    client.Inserthouselocated(house_located.Text.Trim());
            //}


        }
        private void EnumVisual()
        {
           // Grid g = this.Content as Grid;
            UIElementCollection childrens = addhousegrid.Children;
            foreach (UIElement ui in childrens)
            {
                if (ui is TextBox)
                {
                    (ui as TextBox).Text = "";
                }
            }
            house_nature.Text = "";
            house_located.Text = "";
        }

        ////下一步添加信息
        //private void nextpage()
        //{
        //    C1.WPF.C1Window newcondition = new C1.WPF.C1Window();
        //    newcondition.IsResizable = false;
        //    newcondition.Name = "newcondition";
        //    newcondition.Width = 800;
        //    newcondition.Height = 450;
        //    newcondition.Header = "房产增加";
        //    newcondition.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 250, SystemParameters.PrimaryScreenHeight / 2d - 250, 0, 0);
        //    newcondition.Show();
        //    newcondition.ShowMaximizeButton = false;
        //    newcondition.ShowMinimizeButton = false;
        //    SecondPages newaddcondition = new SecondPages();
        //    newaddcondition.selectcondition = selecthouseid;
        //    newcondition.Content = newaddcondition;

           
        //}
        private void addhouse_Click(object sender, RoutedEventArgs e)
        {
             //int updatenature = client.selecenayureID(house_nature.Text);
            //    int updatelocated = client.selectlocatedID(house_located.Text);




            if (checknull())
            {

                int fanghao = 0;
                try
                {
                    fanghao = client.Selecthouseiddate(house_word.Text.Trim(), house_number.Text.Trim());
                }
                catch { }
                if (fanghao > 0 & selecthouseid ==0)
                {
                    MessageBox.Show("字号已存在!");

                }
                else
                {


                    if (selecthouseid <= 0)
                    {

                        //if (checknull())
                        //{

                        checkmultitable();

                        //}
                        //添加房屋基本信息
                        int nature = client.selecenayureID(house_nature.Text);
                        //int located = client.selectlocatedID(house_located.Text);
                        client.Inserthouse(house_word.Text, house_number.Text, house_owner.Text, nature, idcardnumber.Text, house_located.Text, house_dihao.Text, house_postscript.Text, house_witness.Text, house_proofreader.Text, house_autograph.Text, DateTime.Parse(houselicensingdate.Text), house_office.Text, house_banzhenren.Text, DateTime.Parse(house_tianfatime.Text), house_figure.Text);

                        PublicClass.houseid = client.Selecthouseiddate(house_word.Text.Trim(), house_number.Text.Trim());
                        //添加房屋状况
                        client.Insertcondition(conbuilding_number.Text, conroom_number.Text, conjianshu.Text, conbuilding_structure.Text, concenshu.Text, conbuilt_up_area.Text, conremarks.Text, PublicClass.houseid);
                        //共有权人
                        client.Inserthousencommonor(com_commonor.Text, com_share.Text, com_number.Text, com_comments.Text, PublicClass.houseid);
                        //契税摘要
                        client.Inserthousedeedtax(deddatetime.Text, dedprice.Text, dedtype.Text, dedtax_rate.Text, dedamount_money.Text, dedtaxremarks.Text, PublicClass.houseid);
                        //增加设定他项权利
                        client.Inserthouseobligee(obligee.Text, obl_type.Text, obl_room_number.Text, obl_jianshu.Text, obl_built_up_area.Text, obl_right_value.Text, obl_duration_right.Text, obl_logout_date.Text, PublicClass.houseid);
                        client.Inserthouseuseland(landuse_area.Text, landcard_number.Text, PublicClass.houseid);
                        if (PublicClass.houseid > 0)
                        {  //查询房屋状况
                            house_condition_dataGrid.ItemsSource = client.Selectcondition(PublicClass.houseid).Tables[0].DefaultView;
                            //查询共有人
                            commonor_dataGrid.ItemsSource = client.Selecthousecommonor(PublicClass.houseid).Tables[0].DefaultView;
                            //查询他项权利
                            obligee_dataGrid.ItemsSource = client.Selectobligee(PublicClass.houseid).Tables[0].DefaultView;
                        }


                        add_condition.IsEnabled = true;
                        add_commonor.IsEnabled = true;
                        obligee_button.IsEnabled = true;


                        MessageBoxResult confirmToDel = MessageBox.Show("已保存是否立即打印？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (confirmToDel == MessageBoxResult.Yes)
                        {
                            selecthouseid = PublicClass.houseid;
                            daying();
                        }
                       


                    }
                    else
                    {


                        checkmultitable();
                        //更新房屋基本信息
                        int natureid = client.selecenayureID(house_nature.Text);
                        //int locatedid = client.selectlocatedID(house_located.Text);
                        client.Updatahouse(house_word.Text, house_number.Text, house_owner.Text, natureid, idcardnumber.Text, house_located.Text, house_dihao.Text, house_postscript.Text, house_witness.Text, house_proofreader.Text, house_autograph.Text, DateTime.Parse(houselicensingdate.Text), house_office.Text, house_banzhenren.Text, DateTime.Parse(house_tianfatime.Text), house_figure.Text, selecthouseid);
                        //更新契税摘要
                        client.Updatehousedeedtax(deddatetime.Text, dedprice.Text, dedtype.Text, dedtax_rate.Text, dedamount_money.Text, dedtaxremarks.Text, selecthouseid);

                        //更新使用土地摘要
                        client.Updataohouseuseland(landuse_area.Text, landcard_number.Text, selecthouseid);


                    }

                    EnumVisual();
                }


            }
        }

        //查询房屋状况
        private void house_condition_dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //house_condition_dataGrid.CanUserAddRows = false;
        //    house_condition_dataGrid.ItemsSource = client.Selectcondition(selectcondition).Tables[0].DefaultView;
        }

        //编辑房屋状况
        private void edit_list_Click(object sender, RoutedEventArgs e)
        {
            
            add_condition.IsEnabled = true;
            var t = house_condition_dataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());
            conditionid = s;
            var shuju = client.Selectconditionid(s);
            for (int i = 0; i < shuju.Tables[0].Rows.Count; i++)
            {
                conbuilding_number.Text = shuju.Tables[0].Rows[0][i].ToString();
                conroom_number.Text = shuju.Tables[0].Rows[0][i + 1].ToString();
                conjianshu.Text = shuju.Tables[0].Rows[0][i + 2].ToString();
                conbuilding_structure.Text = shuju.Tables[0].Rows[0][i + 3].ToString();
                concenshu.Text = shuju.Tables[0].Rows[0][i + 4].ToString();
                conbuilt_up_area.Text = shuju.Tables[0].Rows[0][i + 5].ToString();
                conremarks.Text = shuju.Tables[0].Rows[0][i + 6].ToString();


            }
            house_condition_dataGrid.ItemsSource = client.Selectcondition(selecthouseid).Tables[0].DefaultView;
         
        }
        //删除房屋状况
        private void delete_list_Click(object sender, RoutedEventArgs e)
        {
            var t = house_condition_dataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());
            client.Deletecondition(s);
            if (PublicClass.houseid > 0)
            {
                if (client.Selectcondition(PublicClass.houseid).Tables[0].DefaultView.Count == 0)
                {
                    //client.Inserthouseobligee("", "", "", "", "", "", "", "", selecthouseid);
                    client.Insertcondition("", "", "", "", "", "", "", PublicClass.houseid);
                    house_condition_dataGrid.ItemsSource = client.Selectcondition(PublicClass.houseid).Tables[0].DefaultView;
                    t = house_condition_dataGrid.Items[0];
                    b = t as DataRowView;
                    s = int.Parse(b.Row[0].ToString());
                    conditionid = s;
                }
                house_condition_dataGrid.ItemsSource = client.Selectcondition(PublicClass.houseid).Tables[0].DefaultView;
            }
            if (selecthouseid > 0)
            {
                if (client.Selectcondition(selecthouseid).Tables[0].DefaultView.Count == 0)
                {
                    //client.Inserthouseobligee("", "", "", "", "", "", "", "", selecthouseid);
                    client.Insertcondition("", "", "", "", "", "", "", selecthouseid);
                    house_condition_dataGrid.ItemsSource = client.Selectcondition(selecthouseid).Tables[0].DefaultView;
                    t = house_condition_dataGrid.Items[0];
                    b = t as DataRowView;
                    s = int.Parse(b.Row[0].ToString());
                    conditionid = s;
                }
                house_condition_dataGrid.ItemsSource = client.Selectcondition(selecthouseid).Tables[0].DefaultView;
            }
            //house_condition_dataGrid.ItemsSource = client.Selectcondition(PublicClass.houseid).Tables[0].DefaultView;
            //house_condition_dataGrid_Loaded(null, null);
        }

        //修改共有权保持证
        private void commonor_edit_list_Click(object sender, RoutedEventArgs e)
        {
            //add_commonor.Content = "保存";
            add_commonor.IsEnabled = true;
            var t = commonor_dataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());
            commonorid = s;
            var shuju = client.Selecthousecommonorid(s);
            for (int i = 0; i < shuju.Tables[0].Rows.Count; i++)
            {
                com_commonor.Text = shuju.Tables[0].Rows[0][i].ToString();
                com_share.Text = shuju.Tables[0].Rows[0][i + 1].ToString();
                com_number.Text = shuju.Tables[0].Rows[0][i + 2].ToString();
                com_comments.Text = shuju.Tables[0].Rows[0][i + 3].ToString();


            }
            commonor_dataGrid.ItemsSource = client.Selecthousecommonor(selecthouseid).Tables[0].DefaultView;

        }

        //查询共有权证
        private void commonor_dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            commonor_dataGrid.CanUserAddRows = false;
            //commonor_dataGrid.ItemsSource = client.Selecthousecommonor(PublicClass.houseid).Tables[0].DefaultView;
        }
        //删除共有权保持证
        private void commonor_delete_list_Click(object sender, RoutedEventArgs e)
        {
            var t = commonor_dataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());
            client.Deletecommonor(s);
            //third_Loaded(null, null);
            //if (PublicClass.houseid == 0)
            //{
            //    commonor_dataGrid.ItemsSource = client.Selecthousecommonor(selecthouseid).Tables[0].DefaultView;
            //}
            //else
            //{
            //    commonor_dataGrid.ItemsSource = client.Selecthousecommonor(selecthouseid).Tables[0].DefaultView;
            //}
            if (PublicClass.houseid > 0)
            {
                if (client.Selecthousecommonor(PublicClass.houseid).Tables[0].DefaultView.Count == 0)
                {
                    client.Inserthousencommonor("", "", "", "", PublicClass.houseid);
                    commonor_dataGrid.ItemsSource = client.Selecthousecommonor(PublicClass.houseid).Tables[0].DefaultView;
                    t = commonor_dataGrid.Items[0];
                    b = t as DataRowView;
                    s = int.Parse(b.Row[0].ToString());
                    commonorid = s;
                }
                commonor_dataGrid.ItemsSource = client.Selecthousecommonor(PublicClass.houseid).Tables[0].DefaultView;
            }


            if (selecthouseid > 0)
            {
                if (client.Selecthousecommonor(selecthouseid).Tables[0].DefaultView.Count == 0)
                {
                    client.Inserthousencommonor("", "", "", "", selecthouseid);
                    commonor_dataGrid.ItemsSource = client.Selecthousecommonor(selecthouseid).Tables[0].DefaultView;
                    t = commonor_dataGrid.Items[0];
                    b = t as DataRowView;
                    s = int.Parse(b.Row[0].ToString());
                    commonorid = s;
                }
                commonor_dataGrid.ItemsSource = client.Selecthousecommonor(selecthouseid).Tables[0].DefaultView;
            }


            //commonor_dataGrid.ItemsSource = client.Selecthousecommonor(selecthouseid).Tables[0].DefaultView;








            //client.Inserthousencommonor(com_commonor.Text, com_share.Text, com_number.Text, com_comments.Text, PublicClass.houseid);
        }

        private void upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Fdialog = new OpenFileDialog();

            if (Fdialog.ShowDialog().Value)
            {

                using (Stream fs = new FileStream(Fdialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    string message;
                    this.house_figure.Text = Fdialog.SafeFileName;
                    bool result = client.UpLoadFile(Fdialog.SafeFileName, fs.Length, fs, out message);

                    if (result == true)
                    {
                        MessageBox.Show("上传成功！");
                    }
                    else
                    {
                        MessageBox.Show(message);
                    }
                }

            }
        }
        //查询设定他项权利摘要
        private void obligee_dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            obligee_dataGrid.CanUserAddRows = false;
            //if (Selectobligeeid > 0)
            //{
                obligee_dataGrid.ItemsSource = client.Selectobligee(selecthouseid).Tables[0].DefaultView;

                var dateob = client.Selecthouseuselandid(Selectobligeeid);

                for (int i = 0; i < dateob.Tables[0].Rows.Count; i++)
                {
                    landuse_area.Text = dateob.Tables[0].Rows[0][i].ToString();
                    //landcompany.Text = dateob.Tables[0].Rows[0][i + 1].ToString();
                    landcard_number.Text = dateob.Tables[0].Rows[0][i + 2].ToString();
                    //landzidihao.Text = dateob.Tables[0].Rows[0][i + 3].ToString();



                }

            //}
        }

        //编辑设定他项权利摘要
        private void obligee_edit_list_Click(object sender, RoutedEventArgs e)
        {
            obligee_button.IsEnabled = true;
            var t = obligee_dataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());
            oblid = s;
            var shuju = client.Selectobligeeid(s);
            for (int i = 0; i < shuju.Tables[0].Rows.Count; i++)
            {
                obligee.Text = shuju.Tables[0].Rows[0][i].ToString();
                obl_type.Text = shuju.Tables[0].Rows[0][i + 1].ToString();
                obl_room_number.Text = shuju.Tables[0].Rows[0][i + 2].ToString();
                obl_jianshu.Text = shuju.Tables[0].Rows[0][i + 3].ToString();
                obl_built_up_area.Text = shuju.Tables[0].Rows[0][i + 4].ToString();
                obl_right_value.Text = shuju.Tables[0].Rows[0][i + 5].ToString();
                obl_duration_right.Text = shuju.Tables[0].Rows[0][i + 6].ToString();
                obl_logout_date.Text = shuju.Tables[0].Rows[0][i + 7].ToString();


            }

            //if (client.Selectobligee(selecthouseid).Tables[0].DefaultView.Count == 0)
            //{
            //    client.Inserthouseobligee("", "", "", "", "", "", "", "", selecthouseid);
            //}


            obligee_dataGrid.ItemsSource = client.Selectobligee(selecthouseid).Tables[0].DefaultView;

        }
        //删除设定他项权利摘要
        private void obligee_delete_list_Click(object sender, RoutedEventArgs e)
        {
            var t = obligee_dataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());
            client.Deleteobligee(s);
            //obligee_dataGrid_Loaded(null, null);


            if (PublicClass.houseid > 0)
            {
                if (client.Selectobligee(PublicClass.houseid).Tables[0].DefaultView.Count == 0)
                {
                    //client.Inserthouseobligee("", "", "", "", "", "", "", "", se
                    client.Inserthouseobligee("", "", "", "", "", "", "", "", PublicClass.houseid);
                    obligee_dataGrid.ItemsSource = client.Selectobligee(PublicClass.houseid).Tables[0].DefaultView;
                    t = obligee_dataGrid.Items[0];
                    b = t as DataRowView;
                    s = int.Parse(b.Row[0].ToString());
                    oblid = s;

                }
                obligee_dataGrid.ItemsSource = client.Selectobligee(PublicClass.houseid).Tables[0].DefaultView;
            }

            if (selecthouseid > 0)
            {
                if (client.Selectobligee(selecthouseid).Tables[0].DefaultView.Count == 0)
                {
                    //client.Inserthouseobligee("", "", "", "", "", "", "", "", se
                    client.Inserthouseobligee("", "", "", "", "", "", "", "", selecthouseid);
                    obligee_dataGrid.ItemsSource = client.Selectobligee(selecthouseid).Tables[0].DefaultView;
                    t = obligee_dataGrid.Items[0];
                    b = t as DataRowView;
                    s = int.Parse(b.Row[0].ToString());
                    oblid = s;

                }
                obligee_dataGrid.ItemsSource = client.Selectobligee(selecthouseid).Tables[0].DefaultView;
            }



          


            //obligee_dataGrid.ItemsSource = client.Selectobligee(selecthouseid).Tables[0].DefaultView;
        }

        private void add_condition_Click(object sender, RoutedEventArgs e)
        {

            if (selecthouseid > 0 & conditionid == 0)
            {
                client.Insertcondition(conbuilding_number.Text, conroom_number.Text, conjianshu.Text, conbuilding_structure.Text, concenshu.Text, conbuilt_up_area.Text, conremarks.Text, selecthouseid);
                house_condition_dataGrid.ItemsSource = client.Selectcondition(selecthouseid).Tables[0].DefaultView;
            }
            else
            {


                if (conditionid <= 0)
                {
                    client.Insertcondition(conbuilding_number.Text, conroom_number.Text, conjianshu.Text, conbuilding_structure.Text, concenshu.Text, conbuilt_up_area.Text, conremarks.Text, PublicClass.houseid);
                    house_condition_dataGrid.ItemsSource = client.Selectcondition(PublicClass.houseid).Tables[0].DefaultView;
                }
                else
                {
                    client.Updatacondition(conbuilding_number.Text, conroom_number.Text, conjianshu.Text, conbuilding_structure.Text, concenshu.Text, conbuilt_up_area.Text, conremarks.Text, selecthouseid, conditionid);
                    house_condition_dataGrid.ItemsSource = client.Selectcondition(selecthouseid).Tables[0].DefaultView;
                    conditionid = 0;

                }
            }
      
           
         
        }

        private void add_commonor_Click(object sender, RoutedEventArgs e)
        {
            if (selecthouseid > 0 & commonorid == 0)
            {
                client.Inserthousencommonor(com_commonor.Text, com_share.Text, com_number.Text, com_comments.Text, selecthouseid);
                commonor_dataGrid.ItemsSource = client.Selecthousecommonor(selecthouseid).Tables[0].DefaultView;
            }
            else
            {


                if (commonorid <= 0)
                {
                    client.Inserthousencommonor(com_commonor.Text, com_share.Text, com_number.Text, com_comments.Text, PublicClass.houseid);
                    //查询共有人
                    commonor_dataGrid.ItemsSource = client.Selecthousecommonor(PublicClass.houseid).Tables[0].DefaultView;
                }
                else
                {
                    client.Updatacommonor(com_commonor.Text, com_share.Text, com_number.Text, com_comments.Text, selecthouseid, commonorid);
                    commonor_dataGrid.ItemsSource = client.Selecthousecommonor(selecthouseid).Tables[0].DefaultView;
                    commonorid = 0;
                }
            }
           
        }

        private void obligee_button_Click(object sender, RoutedEventArgs e)
        {

            if (selecthouseid > 0 & oblid == 0)
            {
                client.Inserthouseobligee(obligee.Text, obl_type.Text, obl_room_number.Text, obl_jianshu.Text, obl_built_up_area.Text, obl_right_value.Text, obl_duration_right.Text, obl_logout_date.Text, selecthouseid);
                obligee_dataGrid.ItemsSource = client.Selectobligee(selecthouseid).Tables[0].DefaultView;
            }
            else
            {
                if (oblid <= 0)
                {
                    client.Inserthouseobligee(obligee.Text, obl_type.Text, obl_room_number.Text, obl_jianshu.Text, obl_built_up_area.Text, obl_right_value.Text, obl_duration_right.Text, obl_logout_date.Text, PublicClass.houseid);
                    obligee_dataGrid.ItemsSource = client.Selectobligee(PublicClass.houseid).Tables[0].DefaultView;
                    //查询他项权利

                }
                else
                {
                    client.Updataobligee(obligee.Text, obl_type.Text, obl_room_number.Text, obl_jianshu.Text, obl_built_up_area.Text, obl_right_value.Text, obl_duration_right.Text, obl_logout_date.Text, selecthouseid, oblid);
                    obligee_dataGrid.ItemsSource = client.Selectobligee(selecthouseid).Tables[0].DefaultView;
                    oblid = 0;
                }
            }
           
        }



        string reportrdlc;
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        private Stream stream;
      
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
             stream = new FileStream(name + "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
               //"  <PageWidth>29.7cm</PageWidth>" +
               //     "  <PageHeight>21cm</PageHeight>" +
                    //"  <MarginTop>0.5cm</MarginTop>" +
                    //"  <MarginLeft>0.5cm</MarginLeft>" +
                    //  "  <MarginRight>0.5cm</MarginRight>" +
                    // "  <MarginBottom>0.5cm</MarginBottom>" +
            "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            try
            {
                report.Render("Image", deviceInfo, CreateStream, out warnings);

            }
            catch (Exception ex)
            {

                Exception innerEx = ex.InnerException;//取内异常。因为内异常的信息才有用，才能排除问题。  

                while (innerEx != null)
                {

                    MessageBox.Show(innerEx.Message);

                    innerEx = innerEx.InnerException;

                }

            }

            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            //pageImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);

            //指定是否横向打印
            ev.PageSettings.Landscape = false;

            //这里的Graphics对象实际指向了打印机   
            // ev.Graphics.DrawImage(pageImage, 0, 0);
            //m_streams[m_currentPageIndex].Close();
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
        private static PrintDocument fPrintDocument = new PrintDocument();
        /// <summary>  
        /// 获取本机默认打印机名称  
        /// </summary>  
        public static String DefaultPrinter
        {
            get { return fPrintDocument.PrinterSettings.PrinterName; }
        }
        private void Print()
        {
            // const string printerName = DefaultPrinter;
            //"Microsoft Office Document Image Writer";

            if (m_streams == null || m_streams.Count == 0)
                //throw new Exception("Error: no stream to print.");
                MessageBox.Show("报表没有数据");
            // return;

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = DefaultPrinter;

            if (!printDoc.PrinterSettings.IsValid)
            {
                //throw new Exception("Error: cannot find the default printer.");
                MessageBox.Show("错误：找不到默认打印机");
            }

            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

          //  printDoc.DefaultPageSettings.Landscape = true;

            //打印预览                   
            //PrintPreviewDialog ppd = new PrintPreviewDialog();
            // ppd.Document = printDoc;

            StandardPrintController spc = new StandardPrintController();

            printDoc.PrintController = spc;


            m_currentPageIndex = 0;
            printDoc.PrinterSettings.Copies = 1;
            //  printDoc.PrinterSettings.PrintRange=1;
            //if (DialogResult.OK == ppd.ShowDialog())
            //{
            printDoc.Print();
            stream.Close();
            addhouse.IsEnabled = false;
            printreport_house.IsEnabled = false;

            //}

        }



        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }



        public void daying()
        {
            try
            {

                if (printreport_house.Content.ToString() == "打印")
                {
                    try
                    {
                        reportrdlc = "houseReport.rdlc";
                        printreport_house.Content = "第二页";
                        Run();
                    }
                    catch { }
                }
                if (printreport_house.Content.ToString() == "第二页")
                {
                    try
                    {
                        reportrdlc = "houseReport1.rdlc";
                        printreport_house.Content = "第三页";
                        Run();
                    }
                    catch { }
                }
                if (printreport_house.Content.ToString() == "第三页")
                {
                    try
                    {
                        reportrdlc = "houseReport2.rdlc";
                        printreport_house.Content = "第四页";
                        Run();
                    }
                    catch { }
                }
                if (printreport_house.Content.ToString() == "第四页")
                {
                    try
                    {
                        reportrdlc = "houseReport3.rdlc";
                        printreport_house.Content = "第五页";
                        Run();
                    }
                    catch { }
                }



                if (printreport_house.Content.ToString() == "第五页")
                {
                    try
                    {
                        reportrdlc = "houseReport4.rdlc";
                        PublicClass.picturefilename = client.Selecthousepicture(selecthouseid);//this.textBox2.Text;
                        string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\client\";
                        bool issuccess = false;
                        string message = "";
                        Stream filestream = new MemoryStream();
                        long filesize = client.DownLoadFile(PublicClass.picturefilename, out issuccess, out message, out filestream);

                        if (issuccess)
                        {
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            byte[] buffer = new byte[filesize - 1];
                            FileStream fs = new FileStream(path + PublicClass.picturefilename, FileMode.Create, FileAccess.Write);

                            int count = 0;
                            int tt = 0;
                            //for (int i = 0; i  < buffer.Length; i++)
                            //{
                            //fs.Write(buffer, i, i+1);

                            while ((count = filestream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fs.Write(buffer, 0, count);
                                tt++;
                            }
                            //}

                            //清空缓冲区
                            fs.Flush();
                            //关闭流
                            fs.Close();
                            //   MessageBox.Show("下载成功！");

                        }
                        else
                        {

                            // MessageBox.Show(message);

                        }



                        string path1 = System.AppDomain.CurrentDomain.BaseDirectory + @"client\" + PublicClass.picturefilename;//"file:///D:/工作/WCF测试/房产证管理系统/测试版/PocclientApplication/PocclientApplication/bin/Debug/client/2.jpg";
                        string path2 = path1.Replace(@"\", @"/");
                        params2 = new ReportParameter("LogoUrl", "file:///" + path2);

                        Run();
                    }
                    catch { }


                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void printreport_house_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (printreport_house.Content.ToString() == "打印")
                {
                    try
                    {
                        reportrdlc = "houseReport.rdlc";
                        printreport_house.Content = "第二页";
                        Run();
                    }catch{}
                }
                if (printreport_house.Content.ToString() == "第二页")
                {
                    try{
                    reportrdlc = "houseReport1.rdlc";
                    printreport_house.Content = "第三页";
                    Run();
                    }catch{}
                }
                if (printreport_house.Content.ToString() == "第三页")
                {
                    try
                    {
                        reportrdlc = "houseReport2.rdlc";
                        printreport_house.Content = "第四页";
                        Run();
                    }
                    catch { }
                }
                if (printreport_house.Content.ToString() == "第四页")
                {
                    try
                    {
                        reportrdlc = "houseReport3.rdlc";
                        printreport_house.Content = "第五页";
                        Run();
                    }
                    catch { }
                }
             


                if (printreport_house.Content.ToString() == "第五页")
                {
                    try
                    {
                        reportrdlc = "houseReport4.rdlc";
                        PublicClass.picturefilename = client.Selecthousepicture(selecthouseid);//this.textBox2.Text;
                        string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\client\";
                        bool issuccess = false;
                        string message = "";
                        Stream filestream = new MemoryStream();
                        long filesize = client.DownLoadFile(PublicClass.picturefilename, out issuccess, out message, out filestream);

                        if (issuccess)
                        {
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            byte[] buffer = new byte[filesize - 1];
                            FileStream fs = new FileStream(path + PublicClass.picturefilename, FileMode.Create, FileAccess.Write);

                            int count = 0;
                            int tt = 0;
                            //for (int i = 0; i  < buffer.Length; i++)
                            //{
                            //fs.Write(buffer, i, i+1);

                            while ((count = filestream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fs.Write(buffer, 0, count);
                                tt++;
                            }
                            //}

                            //清空缓冲区
                            fs.Flush();
                            //关闭流
                            fs.Close();
                            //   MessageBox.Show("下载成功！");

                        }
                        else
                        {

                            // MessageBox.Show(message);

                        }



                        string path1 = System.AppDomain.CurrentDomain.BaseDirectory + @"client\" + PublicClass.picturefilename;//"file:///D:/工作/WCF测试/房产证管理系统/测试版/PocclientApplication/PocclientApplication/bin/Debug/client/2.jpg";
                        string path2 = path1.Replace(@"\", @"/");
                        params2 = new ReportParameter("LogoUrl", "file:///" + path2);

                        Run();
                    }
                    catch { }


                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }
         ReportParameter params2;
        private void Run()
        {
            LocalReport report = new LocalReport();
            report.ReportPath = reportrdlc;// "Report1.rdlc";//System.Windows.Forms.Application.StartupPath + "//Reportland.rdlc";
            // report.DataSources.Add(new ReportDataSource("DataSet1", client.Selecthouseid(selecthouseid).Tables[0].DefaultView));
            if (reportrdlc == "houseReport.rdlc")
            {
                report.DataSources.Add(new ReportDataSource("house_DataSet", client.Selecthouseid(selecthouseid).Tables[0].DefaultView));
            }
            if ( reportrdlc == "houseReport1.rdlc" || reportrdlc == "houseReport3.rdlc")
            {
                report.DataSources.Add(new ReportDataSource("DataSet1", client.Selecthouseid(selecthouseid).Tables[0].DefaultView));
                int natureid_fromhouse = client.selece_nature_id_fromhouse(selecthouseid);
                ReportParameter nature = new ReportParameter("nature", client.selecenatureID(natureid_fromhouse));
                report.SetParameters(nature);

                //int locatedid_fromhouse = client.selece_located_id_fromhouse(selecthouseid);
                //ReportParameter located = new ReportParameter("located", client.selece_located_id(locatedid_fromhouse));
                //report.SetParameters(located);

                ReportParameter commonor = new ReportParameter("commonor", client.Selecthousecommonortop1(selecthouseid));
                report.SetParameters(commonor);
                report.DataSources.Add(new ReportDataSource("DataSet2", client.Selectcondition(selecthouseid).Tables[0].DefaultView));
            }
            if (reportrdlc == "houseReport2.rdlc" || reportrdlc == "houseReport4.rdlc")
            {
                report.DataSources.Add(new ReportDataSource("DataSet1", client.Selecthousecommonor(selecthouseid).Tables[0].DefaultView));
                report.DataSources.Add(new ReportDataSource("DataSet2", client.Selecthousedeedtaxid(selecthouseid).Tables[0].DefaultView));
                report.DataSources.Add(new ReportDataSource("DataSet3", client.Selectobligee(selecthouseid).Tables[0].DefaultView));
                report.DataSources.Add(new ReportDataSource("DataSet4", client.Selecthouseuselandid(selecthouseid).Tables[0].DefaultView));
                report.DataSources.Add(new ReportDataSource("DataSet5", client.Selecthouseid(selecthouseid).Tables[0].DefaultView));

            }
            report.EnableExternalImages = true;
            if (reportrdlc == "houseReport4.rdlc")
              {
                  report.SetParameters(params2);
              }
        




            //report.DataSources.Add(new ReportDataSource("DataSet3", client.Selectcountrysideiddate(landid).Tables[0].DefaultView));
           

            Export(report);

            // m_currentPageIndex = 0;
            Print();
        }


      
    }
}
