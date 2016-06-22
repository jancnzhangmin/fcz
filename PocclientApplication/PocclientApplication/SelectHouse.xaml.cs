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

        private void house_grid_Loaded(object sender, RoutedEventArgs e)
        {
            house_dataGrid.ItemsSource = client.Selecthouse().Tables[0].DefaultView;
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

        private void house_inquiry_Click(object sender, RoutedEventArgs e)
        {
            house_dataGrid.ItemsSource = client.Selecthouseinquiry(house_owner.Text, idcardnumber.Text).Tables[0].DefaultView;
        }
    }
}
