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
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;

namespace PocclientApplication
{
    /// <summary>
    /// databack.xaml 的交互逻辑
    /// </summary>
    public partial class databack : UserControl
    {
        public databack()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //string saveAway = "d:/pocdatabase.bak";
            //SaveFileDialog sa = new SaveFileDialog();
            //sa.Filter = "数据库备份文件 (.bak)|*.bak";
            //if (sa.ShowDialog() == true)
            //{
            //    saveAway = sa.FileName;
            //}
            //string cmdText = @"backup database pocdatabase to disk='" + saveAway + "'";
            //BakReductSql(cmdText, true);    
        }


        private void BakReductSql(string cmdText, bool isBak)
        {
            //SqlCommand cmdBakRst = new SqlCommand();
            //SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=master;Trusted_Connection=Yes");
            
            //try
            //{
            //    conn.Open();
            //    cmdBakRst.Connection = conn;
            //    cmdBakRst.CommandType = CommandType.Text;
            //    if (!isBak)     //如果是恢复操作
            //    {
            //        string setOffline = "Alter database GroupMessage Set Offline With rollback immediate ";
            //        string setOnline = " Alter database GroupMessage Set Online With Rollback immediate";
            //        cmdBakRst.CommandText = setOffline + cmdText + setOnline;
            //    }
            //    else
            //    {
            //        cmdBakRst.CommandText = cmdText;
            //    }
            //    cmdBakRst.ExecuteNonQuery();
            //    if (!isBak)
            //    {
            //        MessageBox.Show("恭喜你，数据成功恢复为所选文档的状态！", "系统消息");
            //    }
            //    else
            //    {
            //        MessageBox.Show("数据库备份成功！", "系统消息");
            //    }
            //}
            //catch (SqlException sexc)
            //{
            //    MessageBox.Show("失败，可能是对数据库操作失败，原因：" + sexc, "数据库错误消息");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("对不起，操作失败，可能原因：" + ex, "系统消息");
            //}
            //finally
            //{
            //    cmdBakRst.Dispose();
            //    conn.Close();
            //    conn.Dispose();
            //}
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //string openAway = "d:/pocdatabase.bak";//读取文件的路径
            //string cmdText = @"restore database pocdatabase from disk='" + openAway + "'";
            //BakReductSql(cmdText, false);
        }

    }
}
