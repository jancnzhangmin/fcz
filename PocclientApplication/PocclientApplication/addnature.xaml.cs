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
    /// addnature.xaml 的交互逻辑
    /// </summary>
    public partial class addnature : UserControl
    {
        public addnature()
        {
            InitializeComponent();
        }
        Service1Client client = new Service1Client();

     

        private void addnaturebtn_Click(object sender, RoutedEventArgs e)
        {
            client.Inserthousenature(nature.Text);
        }
    }
}
