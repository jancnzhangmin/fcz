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
using Microsoft.Reporting.WinForms;
using System.Data;
using System.IO;
using PocclientApplication.ServiceReference;

namespace PocclientApplication
{
    /// <summary>
    /// Report.xaml 的交互逻辑
    /// </summary>
    public partial class Report : UserControl
    {
        public Report()
        {
            InitializeComponent();
        }
      
      
        Service1Client client = new Service1Client();
        private void c1ReportViewer1_Loaded(object sender, RoutedEventArgs e)
        {
            //c1ReportViewer1.LocalReport = AppDomain.CurrentDomain.BaseDirectory + "Report1.rdlc";
            //var uri = new Uri("/PocclientApplication;component/Report1.rdlc", UriKind.Relative);
            //var resource = Application.GetResourceStream(uri);
            //this.c1ReportViewer1.LoadDocument(resource.Stream);
           // int a;
            
        }

        private void report_Loaded(object sender, RoutedEventArgs e)
        {

            LocalReport report = new LocalReport();
            report.ReportPath = "Report1";
            //report.DataSources.Add(new ReportDataSource("DataSet1", client.Selecthouseid(selecthouseid).Tables[0].DefaultView));

    
        }
    }
}
