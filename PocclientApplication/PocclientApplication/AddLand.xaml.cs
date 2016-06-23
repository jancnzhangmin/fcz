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
using Microsoft.Win32;
using System.IO;
using PocclientApplication.ServiceReference;
using Microsoft.Reporting.WinForms;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Diagnostics;


namespace PocclientApplication
{
    /// <summary>
    /// AddLand.xaml 的交互逻辑
    /// </summary>
    public partial class AddLand : UserControl
    {
        public AddLand()
        {
            InitializeComponent();
        }
        Service1Client client = new Service1Client();
        public int landid = 0;
        public string visible;
        public static T FindChild<T>(DependencyObject parent, string childName)//查找控件
where T : DependencyObject
        {
            if (parent == null) return null;
            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // 如果子控件不是需查找的控件类型 
                T childType = child as T;
                if (childType == null)
                {
                    // 在下一级控件中递归查找 
                    foundChild = FindChild<T>(child, childName);
                    // 找到控件就可以中断递归操作  
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // 如果控件名称符合参数条件 
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // 查找到了控件 
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }
        private void uppicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Fdialog = new OpenFileDialog();

            if (Fdialog.ShowDialog().Value)
            {

                using (Stream fs = new FileStream(Fdialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    string message;
                    this.land_figure.Text = Fdialog.SafeFileName;
                    bool result = client.UpLoadFile(Fdialog.SafeFileName, fs.Length, fs, out message);

                    if (result == true)
                    {
                        MessageBox.Show("上传成功！");
                    }
                    else
                    {
                        MessageBox.Show(message);
                    }
                }

            }
        }


        //添加土地信息
        private void next_page_Click(object sender, RoutedEventArgs e)
        {
            if (checknull())
            {
                //land_date.Text = DateTime.Now.Date.ToShortDateString();
                //string a = DateTime.Parse(land_date.Text).ToShortDateString();
                if (landid <= 0)
                {   //添加基本
                    client.Insertland(land_title.Text, land_word.Text, land_number.Text, land_govermment.Text, DateTime.Parse(land_date.Text), land_user.Text, land_idcardnumber.Text, land_address.Text, land_map.Text, land_ground.Text, land_use.Text, land_use_period.Text, land_east.Text, land_south.Text, land_west.Text, land_north.Text, land_office.Text, DateTime.Parse(land_send_date.Text)
    , land_mangaors.Text, land_licensing.Text, land_remarks.Text, land_changeitems.Text, land_figure.Text);
                    PublicClass.selectlandid = client.Selectlandid(land_word.Text, land_number.Text);
                    //添加城镇
                    client.Insertlandtown(land_area.Text, built_up_area.Text, common_area.Text, sharing_area.Text, land_grade.Text, PublicClass.selectlandid);
                    //添加农村
                    client.Insertcountryside(land_area_country.Text, cultivated_land.Text, dry_land.Text, paddy_field.Text, orchard.Text, forest_land.Text, grassland.Text, inmate_mining.Text, construction_land.Text, homestead_land.Text, traffic_land.Text, water_land.Text, unused_land.Text, PublicClass.selectlandid);

                    EnumVisual();
                    //nextopen();


                }
                else
                {   //更新基本
                    client.Updataland(land_title.Text, land_word.Text, land_number.Text, land_govermment.Text, DateTime.Parse(land_date.Text), land_user.Text, land_idcardnumber.Text, land_address.Text, land_map.Text, land_ground.Text, land_use.Text, land_use_period.Text, land_east.Text, land_south.Text, land_west.Text, land_north.Text, land_office.Text, DateTime.Parse(land_send_date.Text), land_mangaors.Text, land_licensing.Text, land_remarks.Text, land_changeitems.Text, land_figure.Text, landid);
                    //更新城镇
                    client.Updatalandtown(land_area.Text, built_up_area.Text, common_area.Text, sharing_area.Text, land_grade.Text, landid);
                    //更新农村
                    client.Updatacountryside(land_area_country.Text, cultivated_land.Text, dry_land.Text, paddy_field.Text, orchard.Text, forest_land.Text, grassland.Text, inmate_mining.Text, construction_land.Text, homestead_land.Text, traffic_land.Text, water_land.Text, unused_land.Text, landid);
                    //nextopen();
                }
            }

            //C1.WPF.C1Window findfixed = MainWindow.FindChild<C1.WPF.C1Window>(Application.Current.MainWindow, "newAddLand");
            //if (findfixed != null)
            //{
            //    findfixed.Close();
            //}

        }
        private void nextopen()
        {
            C1.WPF.C1Window newcountrysidec1 = new C1.WPF.C1Window();
            newcountrysidec1.IsResizable = false;
            newcountrysidec1.Name = "newcountrysidec1";
            newcountrysidec1.Width = 800;
            newcountrysidec1.Height = 480;
            newcountrysidec1.Header = "农村土地";
            newcountrysidec1.Margin = new Thickness(SystemParameters.PrimaryScreenWidth / 2d - 250, SystemParameters.PrimaryScreenHeight / 2d - 250, 0, 0);
            newcountrysidec1.Show();
            newcountrysidec1.ShowMaximizeButton = false;
            newcountrysidec1.ShowMinimizeButton = false;
            LandCountryside newaddcountryside = new LandCountryside();
            newaddcountryside.landid = landid;
            newcountrysidec1.Content = newaddcountryside;
        }
       
        private void EnumVisual()
        {
           // Grid g = this.Content as Grid;
            UIElementCollection childrens = land.Children;
            foreach (UIElement ui in childrens)
            {
                if (ui is TextBox)
                {
                    (ui as TextBox).Text = "";
                }
            }
           
        }

        private bool checknull()
        {
            bool check_status = true;
            string status_err = "";
            if (land_title.Text == "")
            {
                check_status = false;
                status_err += "佤用 ";
            }

            if (land_word.Text == "")
            {
                check_status = false;
                status_err += "字 ";
            }

            if (land_number.Text == "")
            {
                check_status = false;
                status_err += "号 ";
            }

            if (!check_status)
            {
                MessageBox.Show(status_err + "不能为空！");
            }
            return check_status;

        }

        private void land_Loaded(object sender, RoutedEventArgs e)
        {
            if (visible == "land")
            {
                prinreport.Visibility = Visibility.Hidden;
            }
            
           
            land_date.Text = DateTime.Now.ToString();
            land_send_date.Text = DateTime.Now.ToString();


            if (landid > 0)
            {

                //基本信息
                var landdate = client.Selectlanddateto_edit(landid);
                for (int i = 0; i < landdate.Tables[0].Rows.Count; i++)
                {
                    land_title.Text = landdate.Tables[0].Rows[0][i].ToString();
                    land_word.Text = landdate.Tables[0].Rows[0][i + 1].ToString();
                    land_number.Text = landdate.Tables[0].Rows[0][i + 2].ToString();
                    land_govermment.Text = landdate.Tables[0].Rows[0][i + 3].ToString();
                    land_date.Text = landdate.Tables[0].Rows[0][i + 4].ToString();
                    land_user.Text = landdate.Tables[0].Rows[0][i + 5].ToString();
                    land_idcardnumber.Text = landdate.Tables[0].Rows[0][i + 6].ToString();
                    land_address.Text = landdate.Tables[0].Rows[0][i + 7].ToString();
                    land_map.Text = landdate.Tables[0].Rows[0][i + 8].ToString();
                    land_ground.Text = landdate.Tables[0].Rows[0][i + 9].ToString();
                    land_use.Text = landdate.Tables[0].Rows[0][i + 10].ToString();
                    land_use_period.Text = landdate.Tables[0].Rows[0][i + 11].ToString();
                    land_east.Text = landdate.Tables[0].Rows[0][i + 12].ToString();
                    land_south.Text = landdate.Tables[0].Rows[0][i + 13].ToString();
                    land_west.Text = landdate.Tables[0].Rows[0][i + 14].ToString();
                    land_north.Text = landdate.Tables[0].Rows[0][i + 15].ToString();
                    land_office.Text = landdate.Tables[0].Rows[0][i + 16].ToString();
                    land_send_date.Text = landdate.Tables[0].Rows[0][i + 17].ToString();
                    land_mangaors.Text = landdate.Tables[0].Rows[0][i + 18].ToString();
                    land_licensing.Text = landdate.Tables[0].Rows[0][i + 19].ToString();
                    land_remarks.Text = landdate.Tables[0].Rows[0][i + 20].ToString();
                    land_changeitems.Text = landdate.Tables[0].Rows[0][i + 21].ToString();
                    land_figure.Text = landdate.Tables[0].Rows[0][i + 22].ToString();
                }

                //城镇
                var selectcountrysidedate = client.Selectlandtowniddate(landid);
                for (int t = 0; t < selectcountrysidedate.Tables[0].Rows.Count; t++)
                {
                    land_area.Text = selectcountrysidedate.Tables[0].Rows[0][t].ToString();
                    built_up_area.Text = selectcountrysidedate.Tables[0].Rows[0][t + 1].ToString();
                    common_area.Text = selectcountrysidedate.Tables[0].Rows[0][t + 2].ToString();
                    sharing_area.Text = selectcountrysidedate.Tables[0].Rows[0][t + 3].ToString();
                    land_grade.Text = selectcountrysidedate.Tables[0].Rows[0][t + 4].ToString();


                }

                //农村
                var countryside = client.Selectcountrysideiddate(landid);
                for (int i = 0; i < countryside.Tables[0].Rows.Count; i++)
                {
                    land_area_country.Text = countryside.Tables[0].Rows[0][i].ToString();
                    cultivated_land.Text = countryside.Tables[0].Rows[0][i + 1].ToString();
                    dry_land.Text = countryside.Tables[0].Rows[0][i + 2].ToString();
                    paddy_field.Text = countryside.Tables[0].Rows[0][i + 3].ToString();
                    orchard.Text = countryside.Tables[0].Rows[0][i + 4].ToString();
                    forest_land.Text = countryside.Tables[0].Rows[0][i + 5].ToString();
                    grassland.Text = countryside.Tables[0].Rows[0][i + 6].ToString();
                    inmate_mining.Text = countryside.Tables[0].Rows[0][i + 7].ToString();
                    construction_land.Text = countryside.Tables[0].Rows[0][i + 8].ToString();
                    homestead_land.Text = countryside.Tables[0].Rows[0][i + 9].ToString();
                    traffic_land.Text = countryside.Tables[0].Rows[0][i + 10].ToString();
                    water_land.Text = countryside.Tables[0].Rows[0][i + 11].ToString();
                    unused_land.Text = countryside.Tables[0].Rows[0][i + 12].ToString();
                 }
                
            }
        }



        string reportrdlc;
        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new FileStream(name + "." + fileNameExtension, FileMode.Create);
            m_streams.Add(stream);
            return stream;
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
              "<DeviceInfo>" +
              "  <OutputFormat>EMF</OutputFormat>" +
               //"  <PageWidth>29.7cm</PageWidth>" +
               //     "  <PageHeight>21cm</PageHeight>" +
               //     "  <MarginTop>0.5cm</MarginTop>" +
               //     "  <MarginLeft>0.5cm</MarginLeft>" +
               //       "  <MarginRight>0.5cm</MarginRight>" +
               //      "  <MarginBottom>0.5cm</MarginBottom>" +
            "</DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            try
            {
                report.Render("Image", deviceInfo, CreateStream, out warnings);

            }
            catch (Exception ex)
            {

                Exception innerEx = ex.InnerException;//取内异常。因为内异常的信息才有用，才能排除问题。  

                while (innerEx != null)
                {

                    MessageBox.Show(innerEx.Message);

                    innerEx = innerEx.InnerException;

                }

            }

            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new Metafile(m_streams[m_currentPageIndex]);

            //pageImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);

            //指定是否横向打印
            ev.PageSettings.Landscape = false;
            
            //这里的Graphics对象实际指向了打印机   
            // ev.Graphics.DrawImage(pageImage, 0, 0);
            //m_streams[m_currentPageIndex].Close();
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
        private static PrintDocument fPrintDocument = new PrintDocument();
        /// <summary>  
        /// 获取本机默认打印机名称  
        /// </summary>  
        public static String DefaultPrinter
        {
            get { return fPrintDocument.PrinterSettings.PrinterName; }
        }
        private void Print()
        {
            // const string printerName = DefaultPrinter;
            //"Microsoft Office Document Image Writer";

            if (m_streams == null || m_streams.Count == 0)
                //throw new Exception("Error: no stream to print.");
            MessageBox.Show("报表没有数据");
            // return;

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = DefaultPrinter;

            if (!printDoc.PrinterSettings.IsValid)
            {
                //throw new Exception("Error: cannot find the default printer.");
                MessageBox.Show("错误：找不到默认打印机");
            }

            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            
          //  printDoc.DefaultPageSettings.Landscape = false;

            //打印预览                   
            //PrintPreviewDialog ppd = new PrintPreviewDialog();
            // ppd.Document = printDoc;

            StandardPrintController spc = new StandardPrintController();

            printDoc.PrintController = spc;


            m_currentPageIndex = 0;
            printDoc.PrinterSettings.Copies = 1;
            //  printDoc.PrinterSettings.PrintRange=1;
            //if (DialogResult.OK == ppd.ShowDialog())
            //{
            printDoc.Print();
            //}

        }

        ReportParameter params2;
        private void Run()
        {
            LocalReport report = new LocalReport();
            report.ReportPath = reportrdlc;// "Report1.rdlc";//System.Windows.Forms.Application.StartupPath + "//Reportland.rdlc";
            report.DataSources.Add(new ReportDataSource("DataSet1", client.Selectlanddateto_edit(landid).Tables[0].DefaultView));
            report.DataSources.Add(new ReportDataSource("DataSet2", client.Selectlandtowniddate(landid).Tables[0].DefaultView));
            report.DataSources.Add(new ReportDataSource("DataSet3", client.Selectcountrysideiddate(landid).Tables[0].DefaultView));

        
            report.EnableExternalImages = true;
            if (reportrdlc == "hengtuiReport7.rdlc")
            {
                report.SetParameters(params2);
            }

            Export(report);

            // m_currentPageIndex = 0;
            Print();
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }



        private void prinreport_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                if (prinreport.Content.ToString() == "打印")
                {
                    reportrdlc = "hengtuiReport1.rdlc";
                    prinreport.Content = "第二页";
                    Run();

                }
                if (prinreport.Content.ToString() == "第二页")
                {
                    reportrdlc = "hengtuiReport2.rdlc";
                    prinreport.Content = "第三页";
                    Run();
                }
                if (prinreport.Content.ToString() == "第三页")
                {
                    reportrdlc = "hengtuiReport4.rdlc";
                    prinreport.Content = "第四页";
                    Run();
                }
                if (prinreport.Content.ToString() == "第四页")
                {
                    reportrdlc = "hengtuiReport3.rdlc";
                    prinreport.Content = "第五页";
                    Run();
                }
                if (prinreport.Content.ToString() == "第五页")
                {
                    reportrdlc = "hengtuiReport5.rdlc";
                    prinreport.Content = "第六页";
                    Run();
                }
                if (prinreport.Content.ToString() == "第六页")
                {
                    reportrdlc = "hengtuiReport6.rdlc";
                    prinreport.Content = "第七页";
                    Run();
                }


                if (prinreport.Content.ToString() == "第七页")
                {

                    reportrdlc = "hengtuiReport7.rdlc";
                    PublicClass.picturefilename = client.Selectlandpicture(landid);//this.textBox2.Text;
                    string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\client\";
                    bool issuccess = false;
                    string message = "";
                    Stream filestream = new MemoryStream();
                    long filesize = client.DownLoadFile(PublicClass.picturefilename, out issuccess, out message, out filestream);

                    if (issuccess)
                    {
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        byte[] buffer = new byte[filesize - 1];
                        FileStream fs = new FileStream(path + PublicClass.picturefilename, FileMode.Create, FileAccess.Write);

                        int count = 0;
                        int tt = 0;
                        //for (int i = 0; i  < buffer.Length; i++)
                        //{
                        //fs.Write(buffer, i, i+1);

                        while ((count = filestream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fs.Write(buffer, 0, count);
                            tt++;
                        }
                        //}

                        //清空缓冲区
                        fs.Flush();
                        //关闭流
                        fs.Close();
                        MessageBox.Show("下载成功！");

                    }
                    else
                    {

                        MessageBox.Show(message);

                    }



                    string path1 = System.AppDomain.CurrentDomain.BaseDirectory + @"client\" + PublicClass.picturefilename;//"file:///D:/工作/WCF测试/房产证管理系统/测试版/PocclientApplication/PocclientApplication/bin/Debug/client/2.jpg";
                    string path2 = path1.Replace(@"\", @"/");

                    params2 = new ReportParameter("LogoUrl", "file:///" + path2);//路径全部用”/“file:///" + path2
                    Run();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




          }

        
    }
}
