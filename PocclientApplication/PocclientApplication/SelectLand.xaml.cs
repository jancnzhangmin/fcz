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
      
        private void land_dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
           // land_dataGrid.ItemsSource = client.Selectland().Tables[0].DefaultView;
            //areatotal();
            land_send_date.Text = DateTime.Now.ToString();
            land_dataGrid.ItemsSource = client.Selectlandinquiry(land_user.Text, land_idcardnumber.Text, land_address.Text, DateTime.Parse(oldtime.Text), DateTime.Parse(land_send_date.Text)).Tables[0].DefaultView;
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
                    sum_co += float.Parse(shuju_co.Tables[0].Rows[j][1].ToString());
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
        }

        private void delete_land_Click(object sender, RoutedEventArgs e)
        {
            var t = land_dataGrid.SelectedItem;
            var b = t as DataRowView;
            int s = int.Parse(b.Row[0].ToString());
            client.Deleteland(s);
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
            if (land_dataGrid.Items.Count > 0)
            {
                areatotal();
            }

        }

    


     
    }
}
