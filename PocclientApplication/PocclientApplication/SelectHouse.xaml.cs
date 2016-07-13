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
using System.Collections;
using System.IO;

namespace PocclientApplication
{
    /// <summary>
    /// SelectHouse.xaml 的交互逻辑
    /// </summary>
    public partial class SelectHouse : UserControl
    {
        public SelectHouse()
        {
            InitializeComponent();
        }

        public int located_id;
             
        public class house
        {
            public string house_id { get; set; }
            public string house_word { get; set; }
            public string house_number { get; set; }
            public string house_owner { get; set; }
            public string house_nature { get; set; }
          
            public string house_idcardnumber { get; set; }
            public string house_located { get; set; }
            public string house_dihao { get; set; }
            public string house_witness { get; set; }
           
            public DateTime house_tianfatime { get; set; }
            public string condition_jianshu { get; set; }
            public string condition_built_area { get; set; }
            public string obl_jianshu { get; set; }
            public string obl_built_area { get; set; }
            public string land_use_area { get; set; }

           //合计
            public string condition_jianshu_total { get; set; }
            public string condition_built_area_total { get; set; }
            public string obl_jianshu_total { get; set; }
            public string obl_built_area_total { get; set; }
            public string land_use_area_total { get; set; }

        }
        public int condition_jianshu;
        public float condition_built_area;
        public int obl_jianshu;
        public float obl_built_area;
        public float land_use_area;
        public List<house> AccountList { get; set; }
        Service1Client client = new Service1Client();
        List<string> selecthouse_id = new List<string>();
        public DataSet shuju_co;
        public DataSet shuju_ob;
        public DataSet shuju_us;

        public static double do_jianshu_co;
        public static double do_area_co;
        public static double do_jianshu_ob;
        public static double do_area_ob;
        public static double do_use_area;
        private void house_grid_Loaded(object sender, RoutedEventArgs e)
        {



            tianfatime.Text =  DateTime.Now.ToString();
           // house_dataGrid.ItemsSource = client.Selecthouse().Tables[0].DefaultView;

            //located.DataContext = client.Selecthouselocated().Tables[0].DefaultView;
            house_dataGrid.LoadingRow += new EventHandler<DataGridRowEventArgs>(house_dataGrid_LoadingRow);
            initselecthouse();
           


        }
        public void  GetDataGridColumnSum(DataGrid datagrid)
        {
            //double result = 0;
            double temp1 = 0;
            double temp2 = 0;
            double temp3 = 0;
            double temp4 = 0;
            double temp5 = 0;
            for (int i = 0; i < datagrid.Items.Count; i++)
            {
                
                SelectHouse.house mySelectedElement = datagrid.Items[i] as SelectHouse.house;
                if (mySelectedElement == null)
                    continue;
                double.TryParse(mySelectedElement.condition_jianshu.ToString(), out temp1);
                do_jianshu_co += temp1;
                double.TryParse(mySelectedElement.condition_built_area.ToString(), out temp2);
                do_area_co += temp2;
                double.TryParse(mySelectedElement.obl_jianshu.ToString(), out temp3);
                do_jianshu_ob += temp3;
                double.TryParse(mySelectedElement.obl_built_area.ToString(), out temp4);
                do_area_ob += temp4;

                double.TryParse(mySelectedElement.land_use_area.ToString(), out temp5);
                do_use_area += temp5;




            }

            //SelectHouse.house mySelectedElement1 = datagrid.Items[datagrid.Items.Count-1] as SelectHouse.house;

            //mySelectedElement1.condition_jianshu_total = do_jianshu_co.ToString();
          
        }


        private void initselecthouse()
        {
            try
            {
                located_id = client.selectlocatedID(located.Text);
            }
            catch { }

            house_dataGrid.ItemsSource = client.Selecthouseinquiry(house_owner.Text, idcardnumber.Text, located.Text, a2, DateTime.Parse(oldtime.Text), DateTime.Parse(tianfatime.Text)).Tables[0].DefaultView;
            if (house_dataGrid.Items.Count > 0)
            {
                areatotal();
            }

            var shuju = client.Selecthouseinquiry(house_owner.Text, idcardnumber.Text, located.Text, a2, DateTime.Parse(oldtime.Text), DateTime.Parse(tianfatime.Text));
            AccountList = new List<house>();
            ArrayList a = new ArrayList();


            for (int i = 0; i < shuju.Tables[0].Rows.Count; i++)
            {
                house house = new house();
                if (i > 0)
                {
                    house.house_id = shuju.Tables[0].Rows[i][0].ToString();
                    house.house_word = shuju.Tables[0].Rows[i][1].ToString();
                    house.house_number = shuju.Tables[0].Rows[i][2].ToString();
                    house.house_owner = shuju.Tables[0].Rows[i][3].ToString();
                    house.house_nature = client.selecenatureID(int.Parse(shuju.Tables[0].Rows[i][4].ToString()));
                    house.house_idcardnumber = shuju.Tables[0].Rows[i][5].ToString();
                    house.house_located = shuju.Tables[0].Rows[i][6].ToString();
                    house.house_dihao = shuju.Tables[0].Rows[i][7].ToString();
                    house.house_witness = shuju.Tables[0].Rows[i][9].ToString();
                    house.house_tianfatime =DateTime.Parse(shuju.Tables[0].Rows[i][15].ToString());
                    condition_jianshu = 0;
                    condition_built_area = 0;
                    obl_jianshu = 0;
                    obl_built_area = 0;
                    land_use_area = 0;
                    house.condition_jianshu = condition_jianshu_co(house.house_id);
                    house.condition_built_area = condition_built_area_co(house.house_id);
                    house.obl_jianshu = obl_jianshu_ob(house.house_id);
                    house.obl_built_area = obl_built_area_ob(house.house_id);
                    house.land_use_area = land_use_area_land(house.house_id);
                    AccountList.Add(house);

                }
                else
                {
                    house.house_id = shuju.Tables[0].Rows[i][0].ToString();
                    house.house_word = shuju.Tables[0].Rows[i][1].ToString();
                    house.house_number = shuju.Tables[0].Rows[i][2].ToString();
                    house.house_owner = shuju.Tables[0].Rows[i][3].ToString();
                    house.house_nature = client.selecenatureID(int.Parse(shuju.Tables[0].Rows[i][4].ToString()));
                    house.house_idcardnumber = shuju.Tables[0].Rows[i][5].ToString();
                    house.house_located = shuju.Tables[0].Rows[i][6].ToString();
                    house.house_dihao = shuju.Tables[0].Rows[i][7].ToString();
                    house.house_witness = shuju.Tables[0].Rows[i][9].ToString();
                    house.house_tianfatime =DateTime.Parse( shuju.Tables[0].Rows[i][15].ToString());

                    condition_jianshu = 0;
                    condition_built_area = 0;
                    obl_jianshu = 0;
                    obl_built_area = 0;
                    land_use_area = 0;
                    house.condition_jianshu = condition_jianshu_co(house.house_id);
                    house.condition_built_area = condition_built_area_co(house.house_id);
                    house.obl_jianshu = obl_jianshu_ob(house.house_id);
                    house.obl_built_area = obl_built_area_ob(house.house_id);
                    house.land_use_area = land_use_area_land(house.house_id);
                    AccountList.Add(house);

                }

               
            }

            //house newhouse = new house();

            //newhouse.house_id = "";
            //newhouse.house_word = "";
            //newhouse.house_number = "";
            //newhouse.house_owner = "";
            //newhouse.house_nature = "";
            //newhouse.house_idcardnumber = "";
            //newhouse.house_located = "";
            //newhouse.house_dihao = "";
            //newhouse.house_witness = "";
            //newhouse.house_tianfatime = "";


            //newhouse.condition_jianshu_total = "123";//do_jianshu_co.ToString();
            //newhouse.condition_built_area_total = "456";// do_area_co.ToString();
            //newhouse.obl_jianshu_total = "789";//do_jianshu_ob.ToString();
            //newhouse.obl_built_area_total = "321";//do_area_ob.ToString();
            //newhouse.land_use_area_total = "654";// do_use_area.ToString();
            //AccountList.Add(newhouse);


            house_dataGrid.ItemsSource = AccountList;

           
            //jianshu_co.Text = GetDataGridColumnSum(house_dataGrid, 10).ToString();
            //area_co.Text = GetDataGridColumnSum(house_dataGrid, 11).ToString();
            //jianshu_ob.Text = GetDataGridColumnSum(house_dataGrid, 12).ToString();
            //area_ob.Text = GetDataGridColumnSum(house_dataGrid, 13).ToString();

             do_jianshu_co=0;
             do_area_co=0;
             do_jianshu_ob=0;
             do_area_ob=0;
             do_use_area = 0;

            GetDataGridColumnSum(house_dataGrid);
            jianshu_co.Text = do_jianshu_co.ToString();
            area_co.Text = do_area_co.ToString();
            jianshu_ob.Text = do_jianshu_ob.ToString();
            area_ob.Text = do_area_ob.ToString();
            use_area.Text = do_use_area.ToString();


        }


        void house_dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private string condition_jianshu_co(string house_id)
        {
            for (int j = 0; j < shuju_co.Tables[0].Rows.Count; j++)
            {
                try
                {
                    if (shuju_co.Tables[0].Rows[j][8].ToString() == house_id)
                    {
                        condition_jianshu += int.Parse(shuju_co.Tables[0].Rows[j][3].ToString());
                    }
                }
                catch { }

            }
            return condition_jianshu.ToString();
        }



        private string condition_built_area_co(string house_id)
        {
            for (int j = 0; j < shuju_co.Tables[0].Rows.Count; j++)
            {
                try
                {
                    if (shuju_co.Tables[0].Rows[j][8].ToString() == house_id)
                    {
                        condition_built_area +=float.Parse(shuju_co.Tables[0].Rows[j][6].ToString());
                    }
                }
                catch { }

            }
            return condition_built_area.ToString();
        }


        private string obl_jianshu_ob(string house_id)
        {
            for (int j = 0; j < shuju_ob.Tables[0].Rows.Count; j++)
            {
                try
                {
                    if (shuju_ob.Tables[0].Rows[j][9].ToString() == house_id)
                    {
                        obl_jianshu += int.Parse(shuju_ob.Tables[0].Rows[j][4].ToString());
                    }
                }
                catch { }

            }
            return obl_jianshu.ToString();
        }

        private string obl_built_area_ob(string house_id)
        {
            for (int j = 0; j < shuju_ob.Tables[0].Rows.Count; j++)
            {
                try
                {
                    if (shuju_ob.Tables[0].Rows[j][9].ToString() == house_id)
                    {
                        obl_built_area += float.Parse(shuju_ob.Tables[0].Rows[j][5].ToString());
                    }
                }
                catch { }

            }
            return obl_built_area.ToString();
        }


        private string land_use_area_land(string house_id)
        {
            for (int j = 0; j < shuju_us.Tables[0].Rows.Count; j++)
            {
                try
                {
                    if (shuju_us.Tables[0].Rows[j][5].ToString() == house_id)
                    {
                        land_use_area += float.Parse(shuju_us.Tables[0].Rows[j][1].ToString());
                    }
                }
                catch { }

            }
            return land_use_area.ToString();
        }

        private void areatotal()
        {
            for (int i = 0; i < house_dataGrid.Items.Count; i++)
            {

                DataGridTemplateColumn templeColumn = house_dataGrid.Columns[0] as DataGridTemplateColumn;
                FrameworkElement s = house_dataGrid.Columns[0].GetCellContent(house_dataGrid.Items[i]);
                // CheckBox tbOper = templeColumn.CellTemplate.FindName("checkbox", s) as CheckBox;
                DataRowView mySelectedElement = (DataRowView)house_dataGrid.Items[i];
                selecthouse_id.Add(mySelectedElement.Row[0].ToString());
            }
            string id = "";
            foreach (var c in selecthouse_id)
            {
                id += "'" + c + "'" + ",";
            }
            id = id.Remove(id.LastIndexOf(","), 1);
            //房屋状况建筑面积
             shuju_co = client.Selecthouselistid(id);
            //var ss=from c in 
            //for (int j = 0; j < shuju_co.Tables[0].Rows.Count; j++)
            //{
            //    try
            //    {
            //        condition_jianshu += int.Parse(shuju_co.Tables[0].Rows[j][3].ToString());
            //    }
            //    catch { }

            //}


            //for (int j = 0; j < shuju_co.Tables[0].Rows.Count; j++)
            //{
            //    try
            //    {
            //        condition_built_area += float.Parse(shuju_co.Tables[0].Rows[j][6].ToString());
            //    }
            //    catch { }

            //}



            ////他项权利建筑面积
              shuju_ob = client.Selecthouselistobid(id);


            //for (int j = 0; j < shuju_ob.Tables[0].Rows.Count; j++)
            //{
            //    try
            //    {
            //        obl_jianshu += int.Parse(shuju_ob.Tables[0].Rows[j][4].ToString());
            //    }
            //    catch { }

            //}

        
            //for (int j = 0; j < shuju_ob.Tables[0].Rows.Count; j++)
            //{
            //    try
            //    {
            //        obl_built_area += float.Parse(shuju_ob.Tables[0].Rows[j][5].ToString());
            //    }
            //    catch { }

            //}



            ////土地摘要土地面积
               shuju_us = client.Selecthouselistusid(id);

            //float sum_us = 0;
            //for (int j = 0; j < shuju_us.Tables[0].Rows.Count; j++)
            //{
            //    try
            //    {
            //        sum_us += float.Parse(shuju_us.Tables[0].Rows[j][1].ToString());
            //    }
            //    catch { }

            //}

            //area_co.Text = sum_co.ToString();
            //area_ob.Text = sum_ob.ToString();
            //area_us.Text = sum_us.ToString();

        }


        private void delete_list_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult confirmToDel = MessageBox.Show("确认要删除所选行吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmToDel == MessageBoxResult.Yes)
            {
                var t = house_dataGrid.SelectedItem;
                SelectHouse.house b = t as SelectHouse.house;
                int s = int.Parse(b.house_id.ToString());
                client.Deletehouse(s);

                house_grid_Loaded(null, null);
            }
            else
            {
                //此处加不删除的操作
            }          
         
            
        }

        private void edit_list_Click(object sender, RoutedEventArgs e)
        {




            var t = house_dataGrid.SelectedItem;
            SelectHouse.house b = t as SelectHouse.house;
            int s = int.Parse(b.house_id.ToString());
            // string edit_num = b.Row[1].ToString();
            //var shuju = client.Selecthouseid(s);


            //for (int i = 0;  i < shuju.Tables[0].Rows.Count; i++)
            //{
            //    string l = shuju.Tables[0].Rows[0][i].ToString();
            //}



            C1.WPF.C1Window newhouse = new C1.WPF.C1Window();
            newhouse.IsResizable = false;
            newhouse.Name = "newhouse";
            newhouse.Width = 1000;
            newhouse.Height = 950;
            newhouse.Header = "房产增加";
            newhouse.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 500, SystemParameters.PrimaryScreenHeight / 2d - 480, 0, 0);
            newhouse.Show();
            newhouse.ShowMaximizeButton = false;
            newhouse.ShowMinimizeButton = false;
            Addhouse newaddhouse = new Addhouse();
            newaddhouse.selecthouseid = s;
            newhouse.Content = newaddhouse;

            newaddhouse.addhouse.Content = "完成修改";






        }

        public string[] per_list;
        //权限
        private void permissions()
        {



            string pem = client.SelectLoginpem(PublicClass.loginid);
            per_list = pem.Split('f');



            foreach (string s in per_list)
            {
                if (s == "4")
                {
                    bianji.Visibility = Visibility.Visible;
                   
                }
                if (s == "5")
                {
                    shanchu.Visibility = Visibility.Visible;
                  
                }
                



            }
        }


        private void baobiao_Click(object sender, RoutedEventArgs e)
        {



            var t = house_dataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());
            PublicClass.reportlandid = s;

            C1.WPF.C1Window newreporthouse = new C1.WPF.C1Window();
            newreporthouse.IsResizable = false;
            newreporthouse.Name = "newreporthouse";
            newreporthouse.Width = 800;
            newreporthouse.Height = 480;
            newreporthouse.Header = "baobiao";
            newreporthouse.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 250, SystemParameters.PrimaryScreenHeight / 2d - 250, 0, 0);
            newreporthouse.Show();
            newreporthouse.ShowMaximizeButton = false;
            newreporthouse.ShowMinimizeButton = false;

            HouseReport newhousereport = new HouseReport();
            newreporthouse.Content = newhousereport;
        }



        string a2 = "";
        private void house_inquiry_Click(object sender, RoutedEventArgs e)
        {
            if (building_structure.Text != "")
            {
                a2 = "";
                quid();
            }
            if (building_structure.Text == "")
            {
                a2 = "";
            }

           


            initselecthouse();

        }

        private void quid()
        {
            DataSet k = client.selectconditionhouse_id(building_structure.Text);
            
            for (int j = 0; j < k.Tables[0].Rows.Count; j++)
            {
                try
                {
                    a2 += "'" + float.Parse(k.Tables[0].Rows[j][0].ToString()) + "'" + ",";
                }
                catch { }

            }


            //for (int i = 0; i < a.Length; i++)
            //{
            //    while (a.IndexOf(a.Substring(i, 1)) != a.LastIndexOf(a.Substring(i, 1)))
            //    {
            //        a = a.Remove(a.LastIndexOf(a.Substring(i, 1)), 1);
            //    }
            //}


          
            //foreach (var c in a)
            //{
            //    a2 += "'" + c + "'" + ",";
            //}

            try
            {
                a2 = a2.Remove(a2.LastIndexOf(","), 1);
            }
            catch { a2 = "0"; }

        }

        private void print_house_Click(object sender, RoutedEventArgs e)
        {
            initselecthouse();
            ExportDataGridSaveAs(true, house_dataGrid);
        }


        /// <summary>
        /// 导出DataGrid数据到Excel
        /// </summary>
        /// <param name="withHeaders">是否需要表头</param>
        /// <param name="grid">DataGrid</param>
        /// <param name="dataBind"></param>
        /// <returns>Excel内容字符串</returns>
        public static string ExportDataGrid(bool withHeaders, System.Windows.Controls.DataGrid grid, bool dataBind)
        {


            //try
            //{
                var strBuilder = new System.Text.StringBuilder();
                var source = (grid.ItemsSource as IList);
                //   if (source == null) return "";
                var headers = new List<string>();
                List<string> bt = new List<string>();
                grid.Columns.RemoveAt(0);
                //grid.Columns.RemoveAt(grid.Columns.Count - 1);
                //grid.Columns.RemoveAt(grid.Columns.Count - 1);
                foreach (var hr in grid.Columns)
                {

                    //   DataGridTextColumn textcol = hr. as DataGridTextColumn;
                    if (hr.Header.ToString() != "编辑" & hr.Header.ToString() != "删除")
                    {
                        headers.Add(hr.Header.ToString());
                    }
                    if (hr is DataGridTextColumn)//列绑定数据
                    {
                        DataGridTextColumn textcol = hr as DataGridTextColumn;
                        if (textcol != null)
                            bt.Add((textcol.Binding as Binding).Path.Path.ToString());		//获取绑定源	  

                    }
                    else if (hr is DataGridTemplateColumn)
                    {
                        if (hr.Header.Equals("操作"))
                            bt.Add("Id");
                    }

                }
                strBuilder.Append(String.Join(",", headers.ToArray())).Append("\r\n");

                var csvRow = new List<string>();
                foreach (var t in grid.Items)
                {

                    var a = t as SelectHouse.house;
                    //house aaa=new house();
                    //for (int i = 1; i < grid.Columns.Count; i++)
                    //{
                        //if (i != 22 & i != 28 & i != 29)
                        //{

            //         house.house_nature = shuju.Tables[0].Rows[0][i + 4].ToString();
            //    house.house_idcardnumber = shuju.Tables[0].Rows[0][i + 5].ToString();
            //    house.house_located = shuju.Tables[0].Rows[0][i + 6].ToString();
            //    house.house_dihao = shuju.Tables[0].Rows[0][i + 7].ToString();
            //    house.house_witness = shuju.Tables[0].Rows[0][i + 9].ToString();
            //    house.house_tianfatime = shuju.Tables[0].Rows[0][i + 11].ToString();
                





            //}
            //house.condition_jianshu = condition_jianshu.ToString();
            //house.condition_built_area = condition_built_area.ToString();
            //house.obl_jianshu = obl_jianshu.ToString();
            //house.obl_built_area = obl_built_area.ToString();


                    string c = a.house_word.ToString() + ',' + a.house_number + ',' + a.house_owner + ',' + a.house_nature + ',' + a.house_idcardnumber + ',' + a.house_located + ',' + a.house_dihao + ',' + a.house_witness + ',' +string.Format("{0:d}", a.house_tianfatime) + ',' + a.condition_jianshu + ',' + a.condition_built_area + ',' + a.obl_jianshu + ',' + a.obl_built_area + ',' + a.land_use_area;
                        csvRow.Add(c);

                        //}
                    //}
                    //csvRow[csvRow.Count()-1] = csvRow.Last() + "\r\n";
                    strBuilder.Append(String.Join(",", csvRow.ToArray())).Append("\r\n");
                    csvRow.Clear();
                }
                //strBuilder.Append(String.Join(",", csvRow.ToArray())).Append("\r\n");
                string heji = "合计" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + do_jianshu_co + ',' + do_area_co + ',' + do_jianshu_ob + ',' + do_area_ob+ ',' + do_use_area;
                csvRow.Add(heji);
                strBuilder.Append(String.Join(",", csvRow.ToArray())).Append("\r\n");

                return strBuilder.ToString();
            //}
            //catch (Exception ex)
            //{
            //    //throw ex;
            //    return "";
            //}
        }

        /// <summary>
        /// 导出DataGrid数据到Excel为CVS文件
        /// 使用utf8编码 中文是乱码 改用Unicode编码
        /// 
        /// </summary>
        /// <param name="withHeaders">是否带列头</param>
        /// <param name="grid">DataGrid</param>
        public static void ExportDataGridSaveAs(bool withHeaders, System.Windows.Controls.DataGrid grid)
        {
            //try
            //{
                string data = ExportDataGrid(true, grid, true);
                var sfd = new Microsoft.Win32.SaveFileDialog
                {
                    DefaultExt = "csv",
                    Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*",
                    FilterIndex = 1
                };
                if (sfd.ShowDialog() == true)
                {
                    using (Stream stream = sfd.OpenFile())
                    {
                        using (var writer = new StreamWriter(stream, System.Text.Encoding.Unicode))
                        {
                            data = data.Replace(",", "\t");
                            writer.Write(data);
                            writer.Close();
                        }
                        stream.Close();
                    }
                }
                MessageBox.Show("导出成功！");
            //}
            //catch (Exception ex)
            //{
            //    //LogHelper.Error(ex);
            //}
        }

        private void house_dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            bianji.Visibility = Visibility.Hidden;
            shanchu.Visibility = Visibility.Hidden;

            permissions();
        }

   


    }
}
