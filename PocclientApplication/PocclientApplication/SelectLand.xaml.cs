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
      
        private void land_dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            land_dataGrid.ItemsSource = client.Selectland().Tables[0].DefaultView;
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
            newland.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 400, SystemParameters.PrimaryScreenHeight / 2d - 230, 0, 0);
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
            land_dataGrid.ItemsSource = client.Selectlandinquiry(land_user.Text, land_idcardnumber.Text).Tables[0].DefaultView;
        }

    


     
    }
}
