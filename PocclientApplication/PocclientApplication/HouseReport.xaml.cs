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
using Microsoft.Reporting.WinForms;

namespace PocclientApplication
{
    /// <summary>
    /// HouseReport.xaml 的交互逻辑
    /// </summary>
    public partial class HouseReport : UserControl
    {
        public HouseReport()
        {
            InitializeComponent();
        }
        Service1Client client = new Service1Client();
        private void report_grid_Loaded(object sender, RoutedEventArgs e)
        {
            ReportDataSource reportDataSource = new ReportDataSource();
            pocdatabaseDataSet news = new pocdatabaseDataSet();
            news.BeginInit();
            //FastReportDataSet dataset = new FastReportDataSet();
            reportDataSource.Name = "house_DataSet"; // Name of the DataSet we set in .rdlc

            reportDataSource.Value = client.Selecthouseid(PublicClass.reportlandid).Tables[0].DefaultView;//news.land ;
            viewerInstance.LocalReport.DataSources.Add(reportDataSource);
            viewerInstance.LocalReport.ReportPath = "HouseReport.rdlc";// Directory.GetCurrentDirectory() + "\\Report1.rdlc";//"Report1.rdlc"; // Path of the rdlc file
            news.EndInit();


            ReportDataSource reportDataSourcecom = new ReportDataSource();
            pocdatabaseDataSet newscom = new pocdatabaseDataSet();
            newscom.BeginInit();
            //FastReportDataSet dataset = new FastReportDataSet();
            reportDataSourcecom.Name = "commonor_DataSet"; // Name of the DataSet we set in .rdlc

            reportDataSourcecom.Value = client.Selecthousecommonorid(PublicClass.reportlandid).Tables[0].DefaultView;//news.land ;
            viewerInstance.LocalReport.DataSources.Add(reportDataSourcecom);
            viewerInstance.LocalReport.ReportPath = "HouseReport.rdlc";// Directory.GetCurrentDirectory() + "\\Report1.rdlc";//"Report1.rdlc"; // Path of the rdlc file
            newscom.EndInit();


            ReportDataSource reportDataSourcecon = new ReportDataSource();
            pocdatabaseDataSet newscon = new pocdatabaseDataSet();
            newscon.BeginInit();
            //FastReportDataSet dataset = new FastReportDataSet();
            reportDataSourcecon.Name = "condition_DataSet"; // Name of the DataSet we set in .rdlc

            reportDataSourcecon.Value = client.Selectconditionid(PublicClass.reportlandid).Tables[0].DefaultView;//news.land ;
            viewerInstance.LocalReport.DataSources.Add(reportDataSourcecon);
            viewerInstance.LocalReport.ReportPath = "HouseReport.rdlc";// Directory.GetCurrentDirectory() + "\\Report1.rdlc";//"Report1.rdlc"; // Path of the rdlc file
            newscon.EndInit();

            ReportDataSource reportDataSourcedee = new ReportDataSource();
            pocdatabaseDataSet newsdee = new pocdatabaseDataSet();
            newsdee.BeginInit();
            //FastReportDataSet dataset = new FastReportDataSet();
            reportDataSourcedee.Name = "deed_tax_DataSet"; // Name of the DataSet we set in .rdlc

            reportDataSourcedee.Value = client.Selecthousedeedtaxid(PublicClass.reportlandid).Tables[0].DefaultView;//news.land ;
            viewerInstance.LocalReport.DataSources.Add(reportDataSourcedee);
            viewerInstance.LocalReport.ReportPath = "HouseReport.rdlc";// Directory.GetCurrentDirectory() + "\\Report1.rdlc";//"Report1.rdlc"; // Path of the rdlc file
            newsdee.EndInit();

            ReportDataSource reportDataSourcecobl = new ReportDataSource();
            pocdatabaseDataSet newsobl = new pocdatabaseDataSet();
            newsobl.BeginInit();
            //FastReportDataSet dataset = new FastReportDataSet();
            reportDataSourcecobl.Name = "obligee_DataSet"; // Name of the DataSet we set in .rdlc

            reportDataSourcecobl.Value = client.Selectobligeeid(PublicClass.reportlandid).Tables[0].DefaultView;//news.land ;
            viewerInstance.LocalReport.DataSources.Add(reportDataSourcecobl);
            viewerInstance.LocalReport.ReportPath = "HouseReport.rdlc";// Directory.GetCurrentDirectory() + "\\Report1.rdlc";//"Report1.rdlc"; // Path of the rdlc file
            newsobl.EndInit();

            ReportDataSource reportDataSourceuse = new ReportDataSource();
            pocdatabaseDataSet newsuse = new pocdatabaseDataSet();
            newsuse.BeginInit();
            //FastReportDataSet dataset = new FastReportDataSet();
            reportDataSourceuse.Name = "use_land_DataSet"; // Name of the DataSet we set in .rdlc

            reportDataSourceuse.Value = client.Selecthouseuselandid(PublicClass.reportlandid).Tables[0].DefaultView;//news.land ;
            viewerInstance.LocalReport.DataSources.Add(reportDataSourceuse);
            viewerInstance.LocalReport.ReportPath = "HouseReport.rdlc";// Directory.GetCurrentDirectory() + "\\Report1.rdlc";//"Report1.rdlc"; // Path of the rdlc file

            //string ccc = @"file:///D:/下载/4-140916111254.jpg";
            //ReportParameter[] param = new ReportParameter[] { new ReportParameter("LogoUrl", ccc) };
            //viewerInstance.LocalReport.SetParameters(new ReportParameter[] { param });




            newsuse.EndInit();

            //pocdatabaseDataSetTableAdapters.landTableAdapter landTableAdapter = new pocdatabaseDataSetTableAdapters.landTableAdapter();


            //ReportParameter[] image = new ReportParameter[1];

            //string path = "file:///" + Server.MapPath("~") + "Images\\image.gif";

            //图片地址 
            //image[0] = new ReportParameter("Image1", ccc); //image1必须和报表参数一致 
            //this.viewerInstance.LocalReport.SetParameters(image);

            //this.viewerInstance.LocalReport.Refresh();

            //landTableAdapter.ClearBeforeFill = true;


            viewerInstance.LocalReport.EnableExternalImages = true;

            ReportParameter params2;


            params2 = new ReportParameter("LogoUrl", "file:///D:/下载/4-140916111254.jpg");//路径全部用”/“


            viewerInstance.LocalReport.SetParameters(new ReportParameter[] { params2 });
            //landTableAdapter.Fill(news.land);
            viewerInstance.RefreshReport();




        }
    }
}
