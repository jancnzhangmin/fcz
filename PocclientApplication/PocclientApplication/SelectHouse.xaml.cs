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

        Service1Client client = new Service1Client();
        List<string> selecthouse_id = new List<string>();
        private void house_grid_Loaded(object sender, RoutedEventArgs e)
        {

            tianfatime.Text =  DateTime.Now.ToString();
           // house_dataGrid.ItemsSource = client.Selecthouse().Tables[0].DefaultView;
            house_dataGrid.ItemsSource = client.Selecthouseinquiry(house_owner.Text, idcardnumber.Text, a2, DateTime.Parse(oldtime.Text), DateTime.Parse(tianfatime.Text)).Tables[0].DefaultView;
            //areatotal();
         
         
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
            DataSet shuju_co = client.Selecthouselistid(id);
            float sum_co = 0;
            for (int j = 0; j < shuju_co.Tables[0].Rows.Count; j++)
            {
                try
                {
                    sum_co += float.Parse(shuju_co.Tables[0].Rows[j][6].ToString());
                }
                catch { }

            }



            //他项权利建筑面积
            DataSet shuju_ob = client.Selecthouselistobid(id);

            float sum_ob = 0;
            for (int j = 0; j < shuju_ob.Tables[0].Rows.Count; j++)
            {
                try
                {
                    sum_ob += float.Parse(shuju_ob.Tables[0].Rows[j][5].ToString());
                }
                catch { }

            }



            //土地摘要土地面积
            DataSet shuju_us = client.Selecthouselistusid(id);

            float sum_us = 0;
            for (int j = 0; j < shuju_us.Tables[0].Rows.Count; j++)
            {
                try
                {
                    sum_us += float.Parse(shuju_us.Tables[0].Rows[j][1].ToString());
                }
                catch { }

            }

            area_co.Text = sum_co.ToString();
            area_ob.Text = sum_ob.ToString();
            area_us.Text = sum_us.ToString();

        }


        private void delete_list_Click(object sender, RoutedEventArgs e)
        {
            var t = house_dataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());
            client.Deletehouse(s);
            
        }

        private void edit_list_Click(object sender, RoutedEventArgs e)
        {
            var t = house_dataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());
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
            house_dataGrid.ItemsSource = client.Selecthouseinquiry(house_owner.Text, idcardnumber.Text,a2,DateTime.Parse(oldtime.Text),DateTime.Parse(tianfatime.Text)).Tables[0].DefaultView;
            if (house_dataGrid.Items.Count > 0)
            {
                areatotal();
            }

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

    }
}
