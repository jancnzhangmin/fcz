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
using System.Collections;
using System.IO;

namespace PocclientApplication
{
    /// <summary>
    /// SelectLand.xaml 的交互逻辑
    /// </summary>

    public partial class SelectLand : UserControl
    {
        public SelectLand()
        {
            InitializeComponent();
        }
        Service1Client client = new Service1Client();
        List<string> selectland_id = new List<string>();



        public static double double1;
        public static double double2;
        public static double double3;
        public static double double4;
        public static double double5;
        public static double double6;
        public static double double7;
        public static double double8;
        public static double double9;
        public static double double10;
        public static double double11;
        public static double double12;
        public static double double13;
        public static double double14;
        public static double double15;
        public static double double16;
        public static double double17;

        public void landheji()
        {
            double1 = GetDataGridColumnSum(land_dataGrid, 23);
            double2 = GetDataGridColumnSum(land_dataGrid, 24);
            double3 = GetDataGridColumnSum(land_dataGrid, 25);
            double4 = GetDataGridColumnSum(land_dataGrid, 26);
            double5 = GetDataGridColumnSum(land_dataGrid, 30);
            double6 = GetDataGridColumnSum(land_dataGrid, 31);
            double7 = GetDataGridColumnSum(land_dataGrid, 32);
            double8 = GetDataGridColumnSum(land_dataGrid, 33);
            double9 = GetDataGridColumnSum(land_dataGrid, 34);
            double10 = GetDataGridColumnSum(land_dataGrid, 35);
            double11 = GetDataGridColumnSum(land_dataGrid, 36);
            double12 = GetDataGridColumnSum(land_dataGrid, 37);
            double13 = GetDataGridColumnSum(land_dataGrid, 38);
            double14 = GetDataGridColumnSum(land_dataGrid, 39);
            double15 = GetDataGridColumnSum(land_dataGrid, 40);
            double16 = GetDataGridColumnSum(land_dataGrid, 41);
            double17 = GetDataGridColumnSum(land_dataGrid, 42);

        }

        private void land_dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            bianji.Visibility = Visibility.Hidden;
            shanchu.Visibility = Visibility.Hidden;

            permissions();


           // land_dataGrid.ItemsSource = client.Selectland().Tables[0].DefaultView;
            //areatotal();
            land_send_date.Text = DateTime.Now.ToString();
         
            land_dataGrid.ItemsSource = client.Selectlandinquiry(land_user.Text, land_idcardnumber.Text, land_address.Text, DateTime.Parse(oldtime.Text), DateTime.Parse(land_send_date.Text)).Tables[0].DefaultView ;

            landheji();

            //land_dataGrid.ItemsSource = AccountList;
            //DataSet a = new DataSet();
            


        area_co.Text = GetDataGridColumnSum(land_dataGrid, 23).ToString();
        build_area_co.Text = GetDataGridColumnSum(land_dataGrid, 24).ToString();
        right_area_co.Text = GetDataGridColumnSum(land_dataGrid, 25).ToString();
        area_to.Text = GetDataGridColumnSum(land_dataGrid, 30).ToString();
        gengdi_to.Text = GetDataGridColumnSum(land_dataGrid, 31).ToString();
            land_dataGrid.LoadingRow += new EventHandler<DataGridRowEventArgs>(land_dataGrid_LoadingRow);
        }

        void land_dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        public static double GetDataGridColumnSum(DataGrid datagrid, int index)
        {
            double result = 0;
            double temp = 0;
            for (int i = 0; i < datagrid.Items.Count; i++)
            {
                DataRowView mySelectedElement = datagrid.Items[i] as DataRowView;
                if (mySelectedElement == null)
                    continue;
                double.TryParse(mySelectedElement.Row.ItemArray[index].ToString(), out temp);
                result += temp;
            }
            return result;
        }

        private void areatotal()
        {
            for (int i = 0; i < land_dataGrid.Items.Count; i++)
            {

                DataGridTemplateColumn templeColumn = land_dataGrid.Columns[0] as DataGridTemplateColumn;
                FrameworkElement s = land_dataGrid.Columns[0].GetCellContent(land_dataGrid.Items[i]);
                // CheckBox tbOper = templeColumn.CellTemplate.FindName("checkbox", s) as CheckBox;
                DataRowView mySelectedElement = (DataRowView)land_dataGrid.Items[i];
                selectland_id.Add(mySelectedElement.Row[0].ToString());
            }
            string id = "";
            foreach (var c in selectland_id)
            {
                id += "'" + c + "'" + ",";
            }
            id = id.Remove(id.LastIndexOf(","), 1);

            //城镇用地面积
            DataSet shuju_co = client.Selectlandlistcoid(id);
            float sum_co = 0;
            for (int j = 0; j < shuju_co.Tables[0].Rows.Count; j++)
            {
                try
                {
                    sum_co += float.Parse(shuju_co.Tables[0].Rows[j][0].ToString());
                }
                catch { }

            }



            //农村土地面积
            DataSet shuju_to = client.Selectlandlisttoid(id);
            
            float sum_to = 0;
            for (int j = 0; j < shuju_to.Tables[0].Rows.Count; j++)
            {
                try
                {
                    sum_to += float.Parse(shuju_to.Tables[0].Rows[j][1].ToString());
                }
                catch { }

            }

            area_co.Text = sum_co.ToString();
            area_to.Text = sum_to.ToString();

        }

        



        private void edit_land_Click(object sender, RoutedEventArgs e)
        {
            var t = land_dataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());

            C1.WPF.C1Window newland = new C1.WPF.C1Window();
            newland.IsResizable = false;
            newland.Name = "newland";
            newland.Width = 1000;
            newland.Height = 800;
            newland.Header = "使用土地编辑";
            newland.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 500, SystemParameters.PrimaryScreenHeight / 2d - 400, 0, 0);
            newland.Show();
            newland.ShowMaximizeButton = false;
            newland.ShowMinimizeButton = false;
            AddLand newaddland = new AddLand();
            newaddland.landid = s;
            newland.Content = newaddland;

            newaddland.next_page.Content = "完成修改";
        }

        private void delete_land_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult confirmToDel = MessageBox.Show("确认要删除所选行吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmToDel == MessageBoxResult.Yes)
            {
                var t = land_dataGrid.SelectedItem;
                var b = t as DataRowView;
                int s = int.Parse(b.Row[0].ToString());
                client.Deleteland(s);

                land_dataGrid_Loaded(null, null);
            }
            else
            {
            }

            
        }

        private void report_land_Click(object sender, RoutedEventArgs e)
        {
            var t = land_dataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());
            PublicClass.reportlandid = s;

            C1.WPF.C1Window newreportland = new C1.WPF.C1Window();
            newreportland.IsResizable = false;
            newreportland.Name = "newreportland";
            newreportland.Width = 800;
            newreportland.Height = 450;
            newreportland.Header = "土地使用报表";
            newreportland.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 400, SystemParameters.PrimaryScreenHeight / 2d - 230, 0, 0);
            newreportland.Show();
            newreportland.ShowMaximizeButton = false;
            newreportland.ShowMinimizeButton = false;
            Report newl = new Report();
            newl.Width = 800;
            newl.Height = 400;
            newreportland.Content = newl;
        }

      

        private void land_inquiry_Click(object sender, RoutedEventArgs e)
        {
            land_dataGrid.ItemsSource = client.Selectlandinquiry(land_user.Text, land_idcardnumber.Text,land_address.Text,DateTime.Parse(oldtime.Text),DateTime.Parse(land_send_date.Text)).Tables[0].DefaultView;
            //if (land_dataGrid.Items.Count > 0)
            //{
            //    areatotal();
            //}
       

            area_co.Text =GetDataGridColumnSum(land_dataGrid, 23).ToString();
            area_to.Text = GetDataGridColumnSum(land_dataGrid, 30).ToString();
        }


        public string[] per_list;
        //权限
        private void permissions()
        {



            string pem = client.SelectLoginpem(PublicClass.loginid);
            per_list = pem.Split('f');



            foreach (string s in per_list)
            {
                if (s == "7")
                {
                    bianji.Visibility = Visibility.Visible;

                }
                if (s == "8")
                {
                    shanchu.Visibility = Visibility.Visible;

                }




            }
        }





        private void print_excel_Click(object sender, RoutedEventArgs e)
        {
            land_dataGrid.ItemsSource = client.Selectlandinquiry(land_user.Text, land_idcardnumber.Text, land_address.Text, DateTime.Parse(oldtime.Text), DateTime.Parse(land_send_date.Text)).Tables[0].DefaultView;
            landheji();

            //land_dataGrid.ItemsSource = AccountList;
            //DataSet a = new DataSet();



            area_co.Text = GetDataGridColumnSum(land_dataGrid, 23).ToString();
            build_area_co.Text = GetDataGridColumnSum(land_dataGrid, 24).ToString();
            right_area_co.Text = GetDataGridColumnSum(land_dataGrid, 25).ToString();
            area_to.Text = GetDataGridColumnSum(land_dataGrid, 30).ToString();
            gengdi_to.Text = GetDataGridColumnSum(land_dataGrid, 31).ToString();


            ExportDataGridSaveAs(true, land_dataGrid);
        }

        //private static string FormatCsvField(string data)
        //{
        //    return String.Format("\"{0}\"", data.Replace("\"", "\"\"\"").Replace("\n", "").Replace("\r", ""));
        //}
        /// <summary>
        /// 导出DataGrid数据到Excel
        /// </summary>
        /// <param name="withHeaders">是否需要表头</param>
        /// <param name="grid">DataGrid</param>
        /// <param name="dataBind"></param>
        /// <returns>Excel内容字符串</returns>
        public static string ExportDataGrid(bool withHeaders, System.Windows.Controls.DataGrid grid, bool dataBind)
        {


            try
            {
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
                    var a = t as DataRowView;
                    for (int i = 1; i < 43; i++)
                    {
                        if (i !=21& i !=22 & i != 28 & i != 29)
                        {
                            //csvRow.Add(a[i].ToString());
                            if (i == 5 || i == 18)
                            {
                                string g = string.Format("{0:d}", a[i]);
                                csvRow.Add(g);
                            }
                            else
                            {
                                csvRow.Add(a[i].ToString());
                            }

                        }
                    }
                    //csvRow[csvRow.Count()-1] = csvRow.Last() + "\r\n";
                    strBuilder.Append(String.Join(",", csvRow.ToArray())).Append("\r\n");
                    csvRow.Clear();
                }
                string heji = "合计" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' + "" + ',' +  double1 + ',' + double2 + ',' + double3 + ',' + double4 + ',' + "" + ',' + double5 + ',' + double6 + ',' + double7 + ',' + double8 + ',' + double9 + ',' + double10 + ',' + double11 + ',' + double12 + ',' + double13 + ',' + double14 + ',' + double15 + ',' + double16 + ',' + double17;
                //strBuilder.Append(String.Join(",", csvRow.ToArray())).Append("\r\n");
                csvRow.Add(heji);
                strBuilder.Append(String.Join(",", csvRow.ToArray())).Append("\r\n");
                csvRow.Clear();


                return strBuilder.ToString();
           }
             catch (Exception ex)
            {
                //throw ex;
                return "";
            }
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
            try
            {
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
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex);
            }
        }


        private void prin_datagrid_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {



        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch { }
        }


     
    }
}
