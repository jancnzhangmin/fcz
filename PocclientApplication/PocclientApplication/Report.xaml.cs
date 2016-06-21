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

          
         
            ReportDataSource reportDataSource = new ReportDataSource();
            pocdatabaseDataSet news = new pocdatabaseDataSet();
            news.BeginInit();
            //FastReportDataSet dataset = new FastReportDataSet();
            reportDataSource.Name = "reportland"; // Name of the DataSet we set in .rdlc

            reportDataSource.Value = client.Selectlanddateto_edit(PublicClass.reportlandid).Tables[0].DefaultView;//news.land ;
            viewerInstance.LocalReport.DataSources.Add(reportDataSource);
            viewerInstance.LocalReport.ReportPath = "Reportland.rdlc";// Directory.GetCurrentDirectory() + "\\Report1.rdlc";//"Report1.rdlc"; // Path of the rdlc file
            news.EndInit();





            ReportDataSource reportDataSource1 = new ReportDataSource();
            pocdatabaseDataSet news1 = new pocdatabaseDataSet();
            news.BeginInit();
            //FastReportDataSet dataset = new FastReportDataSet();
            reportDataSource1.Name = "DataSet1"; // Name of the DataSet we set in .rdlc

            reportDataSource1.Value = client.Selectlandtowniddate(PublicClass.reportlandid).Tables[0].DefaultView;//news.land ;
            viewerInstance.LocalReport.DataSources.Add(reportDataSource1);
            viewerInstance.LocalReport.ReportPath = "Reportland.rdlc";// Directory.GetCurrentDirectory() + "\\Report1.rdlc";//"Report1.rdlc"; // Path of the rdlc file
            news1.EndInit();





            ReportDataSource reportDataSource2 = new ReportDataSource();
            pocdatabaseDataSet news2 = new pocdatabaseDataSet();
            news2.BeginInit();
            //FastReportDataSet dataset = new FastReportDataSet();
            reportDataSource2.Name = "DataSet2"; // Name of the DataSet we set in .rdlc

            reportDataSource2.Value = client.Selectcountrysideiddate(PublicClass.reportlandid).Tables[0].DefaultView;//news.land ;
            viewerInstance.LocalReport.DataSources.Add(reportDataSource2);
            viewerInstance.LocalReport.ReportPath = "Reportland.rdlc";// Directory.GetCurrentDirectory() + "\\Report1.rdlc";//"Report1.rdlc"; // Path of the rdlc file
            news2.EndInit();



            viewerInstance.LocalReport.EnableExternalImages = true;

            ReportParameter params2;


            params2 = new ReportParameter("LogoUrl", "file:///D:/下载/4-140916111254.jpg");//路径全部用”/“


            viewerInstance.LocalReport.SetParameters(new ReportParameter[] { params2 });


            viewerInstance.RefreshReport();
        

    
        }
    }
}
