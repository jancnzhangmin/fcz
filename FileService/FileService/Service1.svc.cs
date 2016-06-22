using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace FileService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }



        public UpFileResult UpLoadFile(UpFile filedata)
        {

            UpFileResult result = new UpFileResult();

            string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\service\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            byte[] buffer = new byte[filedata.FileSize];

            FileStream fs = new FileStream(path + filedata.FileName, FileMode.Create, FileAccess.Write);

            int count = 0;
            while ((count = filedata.FileStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fs.Write(buffer, 0, count);
            }
            //清空缓冲区
            fs.Flush();
            //关闭流
            fs.Close();

            result.IsSuccess = true;

            return result;

        }

        //下载文件
        public DownFileResult DownLoadFile(DownFile filedata)
        {

            DownFileResult result = new DownFileResult();

            string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\service\" + filedata.FileName;

            if (!File.Exists(path))
            {
                result.IsSuccess = false;
                result.FileSize = 0;
                result.Message = "服务器不存在此文件";
                result.FileStream = new MemoryStream();
                return result;
            }
            Stream ms = new MemoryStream();
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            fs.CopyTo(ms);
            ms.Position = 0;  //重要，不为0的话，客户端读取有问题
            result.IsSuccess = true;
            result.FileSize = ms.Length;
            result.FileStream = ms;

            fs.Flush();
            fs.Close();
            return result;
        }




        //连接数据库
        SqlConnection strCon = new SqlConnection("server=localhost;database=pocdatabase;Trusted_Connection=Yes");
        //  Server=sqlserver服务地址;Database=数据库;Trusted_Connection=Yes;Connect Timeout=90

        //添加数据//

        #region 房屋所有权证
        public int Inserthousenature(string nature)
        {
            try
            {
                strCon.Open();
                string strSql = "INSERT INTO house_nature(nature) VALUES(@nature)";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter housenature = new SqlParameter("@nature", nature);
                cmd.Parameters.Add(housenature);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //删除所有权性质
        public int Deletehousenature(string nature)
        {
            try
            {
                strCon.Open();
                string strSql = "DELETE FROM house_nature WHERE nature = @nature";
                SqlCommand cmd = new SqlCommand(strSql, strCon);
                SqlParameter parn = new SqlParameter("@nature", nature);
                cmd.Parameters.Add(parn);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询所有权性质表
        public DataSet Selecthousenature()
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT nature FROM house_nature ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //查询所有权性质ID
        public int selecenayureID(string strnature)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT house_nature_id FROM house_nature where nature= '" + strnature + "'";
                DataSet ds = new DataSet();

                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                //DataTable dt =DataAdapterFill

                //List<T> aa = new List<T>();

                s.Fill(ds);

                // return ds;
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
                //int i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //根据id查询值,编辑使用
        public string selecenatureID(int strnature)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT nature FROM house_nature where house_nature_id= '" + strnature + "'";
                DataSet ds = new DataSet();

                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                //DataTable dt =DataAdapterFill

                //List<T> aa = new List<T>();

                s.Fill(ds);

                // return ds;
                return ds.Tables[0].Rows[0][0].ToString();
                //int i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //检查所有权性质
        public int checkhousenature(string strnature)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT house_nature_id FROM house_nature where nature= '" + strnature + "'";
                DataSet ds = new DataSet();

                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                //DataTable dt =DataAdapterFill

                //List<T> aa = new List<T>();

                s.Fill(ds);
                int naturecount = ds.Tables[0].Rows.Count;
                // return ds;

                return naturecount;
                //int i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //检查房屋座落
        public int checkhouselocated(string strlocated)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT house_located_id FROM house_located where located= '" + strlocated + "'";
                DataSet ds = new DataSet();

                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                //DataTable dt =DataAdapterFill

                //List<T> aa = new List<T>();

                s.Fill(ds);
                int naturecount = ds.Tables[0].Rows.Count;
                // return ds;

                return naturecount;
                //int i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //增加共有权保持证
        public int Inserthousencommonor(string commonor, string share, string number, string comments, int house_id)
        {
            try
            {
                strCon.Open();
                string strSql = "INSERT INTO house_commonor(commonor,share,number,comments,house_id) VALUES(@commonor,@share,@number,@comments,@house_id)";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter housecommonor = new SqlParameter("@commonor", commonor);
                cmd.Parameters.Add(housecommonor);
                SqlParameter housecommonorshare = new SqlParameter("@share", share);
                cmd.Parameters.Add(housecommonorshare);
                SqlParameter housecommonornumber = new SqlParameter("@number", number);
                cmd.Parameters.Add(housecommonornumber);
                SqlParameter housecommonorcomments = new SqlParameter("@comments", comments);
                cmd.Parameters.Add(housecommonorcomments);
                SqlParameter houseid = new SqlParameter("@house_id", house_id);
                cmd.Parameters.Add(houseid);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询共有人
        public DataSet Selecthousecommonor(int Selecthousecommonor)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT house_commonor_id, commonor,share,number,comments,house_id FROM house_commonor where house_id='" + Selecthousecommonor + "' ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询编辑共有人
        public DataSet Selecthousecommonorid(int id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT  commonor,share,number,comments,house_id FROM house_commonor where house_commonor_id= '" + id + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询其中一个共有人
        public string Selecthousecommonortop1(int houseid)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT top 1 commonor FROM house_commonor where house_id= '" + houseid + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //更新共有人
        public int Updatacommonor(string commonor, string share, string number, string comments, int house_id, int commonorid)
        {
            try
            {
                strCon.Open();
                string strSql = "UPDATE  house_commonor  SET commonor=@commonor,share=@share,number=@number,comments=@comments WHERE  house_id ='" + house_id + "' and house_commonor_id='" + commonorid + "' ";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter housecommonor = new SqlParameter("@commonor", commonor);
                cmd.Parameters.Add(housecommonor);
                SqlParameter housecommonorshare = new SqlParameter("@share", share);
                cmd.Parameters.Add(housecommonorshare);
                SqlParameter housecommonornumber = new SqlParameter("@number", number);
                cmd.Parameters.Add(housecommonornumber);
                SqlParameter housecommonorcomments = new SqlParameter("@comments", comments);
                cmd.Parameters.Add(housecommonorcomments);
                SqlParameter houseid = new SqlParameter("@house_id", house_id);
                cmd.Parameters.Add(houseid);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //删除共有人
        public int Deletecommonor(int strcommonorid)
        {
            try
            {
                strCon.Open();
                string strSql = "DELETE FROM house_commonor WHERE house_commonor_id ='" + strcommonorid + "' ";
                SqlCommand cmd = new SqlCommand(strSql, strCon);
                SqlParameter parn = new SqlParameter("@house_commonor_id", strcommonorid);
                cmd.Parameters.Add(parn);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //增加所有权表
        public int Inserthouse(string house_word, string house_number, string house_owner, int house_nature_id, string house_idcardnumber, int house_located_id, string house_dihao, string house_postscript, string house_witness, string house_proofreader, string house_autograph, DateTime house_licensing_date, string house_office, string house_banzhenren, DateTime house_tianfatime, string house_figure)
        {
            try
            {
                strCon.Open();
                string strSql = "INSERT INTO house(house_word,house_number,house_owner,house_nature_id,house_idcardnumber,house_located_id,house_dihao,house_postscript,house_witness,house_proofreader,house_autograph,house_licensing_date,house_office,house_banzhenren,house_tianfatime,house_figure) VALUES(@house_word,@house_number,@house_owner,@house_nature_id,@house_idcardnumber,@house_located_id,@house_dihao,@house_postscript,@house_witness,@house_proofreader,@house_autograph,@house_licensing_date,@house_office,@house_banzhenren,@house_tianfatime,@house_figure)";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter houseword = new SqlParameter("@house_word", house_word);
                cmd.Parameters.Add(houseword);
                SqlParameter housenumber = new SqlParameter("@house_number", house_number);
                cmd.Parameters.Add(housenumber);
                SqlParameter houseowner = new SqlParameter("@house_owner", house_owner);
                cmd.Parameters.Add(houseowner);

                SqlParameter housenature_id = new SqlParameter("@house_nature_id", house_nature_id);
                cmd.Parameters.Add(housenature_id);

                SqlParameter houseidcardnumber = new SqlParameter("@house_idcardnumber", house_idcardnumber);
                cmd.Parameters.Add(houseidcardnumber);

                SqlParameter houselocated = new SqlParameter("@house_located_id", house_located_id);
                cmd.Parameters.Add(houselocated);
                SqlParameter housedihao = new SqlParameter("@house_dihao", house_dihao);
                cmd.Parameters.Add(housedihao);

                //,house_deed_tax_id,house_obligee_id,house_use_land_id,house_office,house_banzhenren,house_tianfatime,house_figure
                SqlParameter housepostscript = new SqlParameter("@house_postscript", house_postscript);
                cmd.Parameters.Add(housepostscript);
                SqlParameter housewitness = new SqlParameter("@house_witness", house_witness);
                cmd.Parameters.Add(housewitness);
                SqlParameter houseproofreader = new SqlParameter("@house_proofreader", house_proofreader);
                cmd.Parameters.Add(houseproofreader);
                SqlParameter houseautograph = new SqlParameter("@house_autograph", house_autograph);
                cmd.Parameters.Add(houseautograph);
                SqlParameter houselicensing_date = new SqlParameter("@house_licensing_date", house_licensing_date);
                cmd.Parameters.Add(houselicensing_date);

                SqlParameter houseoffice = new SqlParameter("@house_office", house_office);
                cmd.Parameters.Add(houseoffice);
                SqlParameter housebanzhenren = new SqlParameter("@house_banzhenren", house_banzhenren);
                cmd.Parameters.Add(housebanzhenren);
                SqlParameter housetianfatime = new SqlParameter("@house_tianfatime", house_tianfatime);
                cmd.Parameters.Add(housetianfatime);
                SqlParameter housefigure = new SqlParameter("@house_figure", house_figure);
                cmd.Parameters.Add(housefigure);
                //result接受受影响的行数，也就是说大于0的话表示添加成功
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询所有权证
        public DataSet Selecthouse()
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT house_id, house_word,house_number,house_owner,house_nature_id,house_idcardnumber,house_located_id,house_dihao,house_postscript,house_witness,house_proofreader,house_autograph,house_licensing_date,house_office,house_banzhenren,house_tianfatime,house_figure FROM house ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }




        //通过字号查询ID
        public int Selecthouseiddate(string word, string number)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT house_id FROM house where house_word='" + word + "' and house_number='" + number + "' ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询编辑所有权表
        public DataSet Selecthouseid(int id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT  house_word,house_number,house_owner,house_nature_id,house_idcardnumber,house_located_id,house_dihao,house_postscript,house_witness,house_proofreader,house_autograph,house_licensing_date,house_office,house_banzhenren,house_tianfatime,house_figure FROM house where house_id= '" + id + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //更新所有权表
        public int Updatahouse(string house_word, string house_number, string house_owner, int house_nature_id, string house_idcardnumber, int house_located_id, string house_dihao, string house_postscript, string house_witness, string house_proofreader, string house_autograph, DateTime house_licensing_date, string house_office, string house_banzhenren, DateTime house_tianfatime, string house_figure, int houseid)
        {
            try
            {
                strCon.Open();
                string strSql = "UPDATE  house  SET house_word=@house_word,house_number=@house_number,house_owner=@house_owner,house_nature_id=@house_nature_id,house_idcardnumber=@house_idcardnumber,house_located_id=@house_located_id,house_dihao=@house_dihao,house_postscript=@house_postscript,house_witness=@house_witness,house_proofreader=@house_proofreader,house_autograph=@house_autograph,house_licensing_date=@house_licensing_date,house_office=@house_office,house_banzhenren=@house_banzhenren,house_tianfatime=@house_tianfatime,house_figure=@house_figure WHERE house_id ='" + houseid + "'";
                SqlCommand cmd = new SqlCommand(strSql, strCon);
                SqlParameter houseword = new SqlParameter("@house_word", house_word);
                cmd.Parameters.Add(houseword);
                SqlParameter housenumber = new SqlParameter("@house_number", house_number);
                cmd.Parameters.Add(housenumber);
                SqlParameter houseowner = new SqlParameter("@house_owner", house_owner);
                cmd.Parameters.Add(houseowner);

                SqlParameter housenature_id = new SqlParameter("@house_nature_id", house_nature_id);
                cmd.Parameters.Add(housenature_id);

                SqlParameter houseidcardnumber = new SqlParameter("@house_idcardnumber", house_idcardnumber);
                cmd.Parameters.Add(houseidcardnumber);

                SqlParameter houselocated = new SqlParameter("@house_located_id", house_located_id);
                cmd.Parameters.Add(houselocated);
                SqlParameter housedihao = new SqlParameter("@house_dihao", house_dihao);
                cmd.Parameters.Add(housedihao);

                //,house_deed_tax_id,house_obligee_id,house_use_land_id,house_office,house_banzhenren,house_tianfatime,house_figure
                SqlParameter housepostscript = new SqlParameter("@house_postscript", house_postscript);
                cmd.Parameters.Add(housepostscript);
                SqlParameter housewitness = new SqlParameter("@house_witness", house_witness);
                cmd.Parameters.Add(housewitness);
                SqlParameter houseproofreader = new SqlParameter("@house_proofreader", house_proofreader);
                cmd.Parameters.Add(houseproofreader);
                SqlParameter houseautograph = new SqlParameter("@house_autograph", house_autograph);
                cmd.Parameters.Add(houseautograph);
                SqlParameter houselicensing_date = new SqlParameter("@house_licensing_date", house_licensing_date);
                cmd.Parameters.Add(houselicensing_date);

                SqlParameter houseoffice = new SqlParameter("@house_office", house_office);
                cmd.Parameters.Add(houseoffice);
                SqlParameter housebanzhenren = new SqlParameter("@house_banzhenren", house_banzhenren);
                cmd.Parameters.Add(housebanzhenren);
                SqlParameter housetianfatime = new SqlParameter("@house_tianfatime", house_tianfatime);
                cmd.Parameters.Add(housetianfatime);
                SqlParameter housefigure = new SqlParameter("@house_figure", house_figure);
                cmd.Parameters.Add(housefigure);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //删除所有权表记录
        public int Deletehouse(int strhouseid)
        {
            try
            {
                strCon.Open();
                string strSql = "DELETE FROM house WHERE house_id ='" + strhouseid + "' ";
                SqlCommand cmd = new SqlCommand(strSql, strCon);
                SqlParameter parn = new SqlParameter("@house_id", strhouseid);
                cmd.Parameters.Add(parn);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //插入房屋坐落
        public int Inserthouselocated(string located)
        {
            try
            {
                strCon.Open();
                string strSql = "INSERT INTO house_located(located) VALUES(@located)";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter houselocated = new SqlParameter("@located", located);
                cmd.Parameters.Add(houselocated);

                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询房屋坐落ID
        public int selectlocatedID(string strlocated)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT house_located_id FROM house_located where located= '" + strlocated + "'";
                DataSet ds = new DataSet();

                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                //DataTable dt =DataAdapterFill

                //List<T> aa = new List<T>();

                s.Fill(ds);

                // return ds;
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
                //int i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //根据id查询值,编辑使用
        public string selece_located_id(int strlocated)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT located FROM house_located where house_located_id= '" + strlocated + "'";
                DataSet ds = new DataSet();

                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                //DataTable dt =DataAdapterFill

                //List<T> aa = new List<T>();

                s.Fill(ds);

                // return ds;
                return ds.Tables[0].Rows[0][0].ToString();
                //int i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //根据id查询natureid,编辑使用
        public int selece_nature_id_fromhouse(int house_id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT house_nature_id FROM house where house_id= '" + house_id + "'";
                DataSet ds = new DataSet();

                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                //DataTable dt =DataAdapterFill

                //List<T> aa = new List<T>();

                s.Fill(ds);

                // return ds;
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
                //int i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }


        //根据id查询locatedid,编辑使用
        public int selece_located_id_fromhouse(int house_id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT house_located_id FROM house where house_id= '" + house_id + "'";
                DataSet ds = new DataSet();

                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                //DataTable dt =DataAdapterFill

                //List<T> aa = new List<T>();

                s.Fill(ds);

                // return ds;
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
                //int i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //查询房屋座落表
        public DataSet Selecthouselocated()
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT located FROM house_located ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }


        //增加房屋状况
        public int Insertcondition(string building_number, string room_number, string jianshu, string building_structure, string cengshu, string built_up_area, string remarks, int house_id)
        {
            try
            {
                strCon.Open();
                string strSql = "INSERT INTO house_condition(building_number,room_number,jianshu,building_structure,cengshu,built_up_area,remarks,house_id) VALUES(@building_number,@room_number,@jianshu,@building_structure,@cengshu,@built_up_area,@remarks,@house_id)";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter buildingnumber = new SqlParameter("@building_number", building_number);
                cmd.Parameters.Add(buildingnumber);
                SqlParameter roomnumber = new SqlParameter("@room_number", room_number);
                cmd.Parameters.Add(roomnumber);
                SqlParameter addjianshu = new SqlParameter("@jianshu", jianshu);
                cmd.Parameters.Add(addjianshu);

                SqlParameter buildingstructure = new SqlParameter("@building_structure", building_structure);
                cmd.Parameters.Add(buildingstructure);

                SqlParameter addcengshu = new SqlParameter("@cengshu", cengshu);
                cmd.Parameters.Add(addcengshu);

                SqlParameter builtuparea = new SqlParameter("@built_up_area", built_up_area);
                cmd.Parameters.Add(builtuparea);
                SqlParameter addremarks = new SqlParameter("@remarks", remarks);
                cmd.Parameters.Add(addremarks);

                SqlParameter houseid = new SqlParameter("@house_id", house_id);
                cmd.Parameters.Add(houseid);

                //result接受受影响的行数，也就是说大于0的话表示添加成功
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询房屋状况
        public DataSet Selectcondition(int houseid)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT house_condition_id, building_number,room_number,jianshu,building_structure,cengshu,built_up_area,remarks,house_id FROM house_condition where house_id='" + houseid + "' ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询编辑房屋状况
        public DataSet Selectconditionid(int id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT  building_number,room_number,jianshu,building_structure,cengshu,built_up_area,remarks,house_id FROM house_condition where house_condition_id= '" + id + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //更新房屋状况
        public int Updatacondition(string building_number, string room_number, string jianshu, string building_structure, string cengshu, string built_up_area, string remarks, int house_id, int conditionid)
        {
            try
            {
                strCon.Open();
                string strSql = "UPDATE  house_condition  SET building_number=@building_number,room_number=@room_number,jianshu=@jianshu,building_structure=@building_structure,cengshu=@cengshu,built_up_area=@built_up_area,remarks=@remarks WHERE   house_id ='" + house_id + "' and house_condition_id='" + conditionid + "' ";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter buildingnumber = new SqlParameter("@building_number", building_number);
                cmd.Parameters.Add(buildingnumber);
                SqlParameter roomnumber = new SqlParameter("@room_number", room_number);
                cmd.Parameters.Add(roomnumber);
                SqlParameter addjianshu = new SqlParameter("@jianshu", jianshu);
                cmd.Parameters.Add(addjianshu);

                SqlParameter buildingstructure = new SqlParameter("@building_structure", building_structure);
                cmd.Parameters.Add(buildingstructure);

                SqlParameter addcengshu = new SqlParameter("@cengshu", cengshu);
                cmd.Parameters.Add(addcengshu);

                SqlParameter builtuparea = new SqlParameter("@built_up_area", built_up_area);
                cmd.Parameters.Add(builtuparea);
                SqlParameter addremarks = new SqlParameter("@remarks", remarks);
                cmd.Parameters.Add(addremarks);

                SqlParameter houseid = new SqlParameter("@house_id", house_id);
                cmd.Parameters.Add(houseid);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //删除房屋状况
        public int Deletecondition(int strconditionid)
        {
            try
            {
                strCon.Open();
                string strSql = "DELETE FROM house_condition WHERE house_condition_id ='" + strconditionid + "' ";
                SqlCommand cmd = new SqlCommand(strSql, strCon);
                SqlParameter parn = new SqlParameter("@house_condition_id", strconditionid);
                cmd.Parameters.Add(parn);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //增加契税摘要
        public int Inserthousedeedtax(DateTime datetime, string price, string type, string tax_rate, string amount_money, string remarks, int house_id)
        {
            try
            {
                strCon.Open();
                string strSql = "INSERT INTO house_deed_tax(datetime,price,type,tax_rate,amount_money,remarks,house_id) VALUES(@datetime,@price,@type,@tax_rate,@amount_money,@remarks,@house_id)";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter adddatetime = new SqlParameter("@datetime", datetime);
                cmd.Parameters.Add(adddatetime);
                SqlParameter addprice = new SqlParameter("@price", price);
                cmd.Parameters.Add(addprice);
                SqlParameter addtype = new SqlParameter("@type", type);
                cmd.Parameters.Add(addtype);
                SqlParameter taxrate = new SqlParameter("@tax_rate", tax_rate);
                cmd.Parameters.Add(taxrate);
                SqlParameter amountmoney = new SqlParameter("@amount_money", amount_money);
                cmd.Parameters.Add(amountmoney);
                SqlParameter addremarks = new SqlParameter("@remarks", remarks);
                cmd.Parameters.Add(addremarks);
                SqlParameter houseid = new SqlParameter("@house_id", house_id);
                cmd.Parameters.Add(houseid);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //更新契税
        public int Updatehousedeedtax(DateTime datetime, string price, string type, string tax_rate, string amount_money, string remarks, int house_id)
        {
            try
            {
                strCon.Open();
                string strSql = "update house_deed_tax SET datetime=@datetime,price=@price,type=@type,tax_rate=@tax_rate,amount_money=@amount_money,remarks=@remarks WHERE house_id='" + house_id + "'";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter adddatetime = new SqlParameter("@datetime", datetime);
                cmd.Parameters.Add(adddatetime);
                SqlParameter addprice = new SqlParameter("@price", price);
                cmd.Parameters.Add(addprice);
                SqlParameter addtype = new SqlParameter("@type", type);
                cmd.Parameters.Add(addtype);
                SqlParameter taxrate = new SqlParameter("@tax_rate", tax_rate);
                cmd.Parameters.Add(taxrate);
                SqlParameter amountmoney = new SqlParameter("@amount_money", amount_money);
                cmd.Parameters.Add(amountmoney);
                SqlParameter addremarks = new SqlParameter("@remarks", remarks);
                cmd.Parameters.Add(addremarks);

                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询编辑契税摘要
        public DataSet Selecthousedeedtaxid(int id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT  datetime,price,type,tax_rate,amount_money,remarks,house_id FROM house_deed_tax where house_id= '" + id + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //增加设定他项权利摘要
        public int Inserthouseobligee(string obligee, string type, string room_number, string jianshu, string built_up_area, string right_value, string duration_right, DateTime logout_date, int house_id)
        {
            try
            {
                strCon.Open();
                string strSql = "INSERT INTO house_obligee(obligee,type,room_number,jianshu,built_up_area,right_value,duration_right,logout_date,house_id) VALUES(@obligee,@type,@room_number,@jianshu,@built_up_area,@right_value,@duration_right,@logout_date,@house_id)";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter addobligee = new SqlParameter("@obligee", obligee);
                cmd.Parameters.Add(addobligee);
                SqlParameter addtype = new SqlParameter("@type", type);
                cmd.Parameters.Add(addtype);
                SqlParameter roomnumber = new SqlParameter("@room_number", room_number);
                cmd.Parameters.Add(roomnumber);
                SqlParameter addjianshu = new SqlParameter("@jianshu", jianshu);
                cmd.Parameters.Add(addjianshu);
                SqlParameter builtuparea = new SqlParameter("@built_up_area", built_up_area);
                cmd.Parameters.Add(builtuparea);
                SqlParameter rightvalue = new SqlParameter("@right_value", right_value);
                cmd.Parameters.Add(rightvalue);
                SqlParameter durationright = new SqlParameter("@duration_right", duration_right);
                cmd.Parameters.Add(durationright);
                SqlParameter logoutdate = new SqlParameter("@logout_date", logout_date);
                cmd.Parameters.Add(logoutdate);
                SqlParameter houseid = new SqlParameter("@house_id", house_id);
                cmd.Parameters.Add(houseid);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询他项权利摘要
        public DataSet Selectobligee(int house_id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT house_obligee_id, obligee,type,room_number,jianshu,built_up_area,right_value,duration_right,logout_date,house_id FROM house_obligee where house_id='" + house_id + "' ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询编辑他项权利摘要
        public DataSet Selectobligeeid(int oblid)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT  obligee,type,room_number,jianshu,built_up_area,right_value,duration_right,logout_date FROM house_obligee where house_obligee_id= '" + oblid + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //更新他项权利摘要
        public int Updataobligee(string obligee, string type, string room_number, string jianshu, string built_up_area, string right_value, string duration_right, DateTime logout_date, int house_id, int oblid)
        {
            try
            {
                strCon.Open();
                string strSql = "UPDATE  house_obligee  SET obligee=@obligee,type=@type,room_number=@room_number,jianshu=@jianshu,built_up_area=@built_up_area,right_value=@right_value,duration_right=@duration_right,logout_date=@logout_date WHERE house_id ='" + house_id + "' and house_obligee_id= '" + oblid + "'";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter addobligee = new SqlParameter("@obligee", obligee);
                cmd.Parameters.Add(addobligee);
                SqlParameter addtype = new SqlParameter("@type", type);
                cmd.Parameters.Add(addtype);
                SqlParameter roomnumber = new SqlParameter("@room_number", room_number);
                cmd.Parameters.Add(roomnumber);
                SqlParameter addjianshu = new SqlParameter("@jianshu", jianshu);
                cmd.Parameters.Add(addjianshu);
                SqlParameter builtuparea = new SqlParameter("@built_up_area", built_up_area);
                cmd.Parameters.Add(builtuparea);
                SqlParameter rightvalue = new SqlParameter("@right_value", right_value);
                cmd.Parameters.Add(rightvalue);
                SqlParameter durationright = new SqlParameter("@duration_right", duration_right);
                cmd.Parameters.Add(durationright);
                SqlParameter logoutdate = new SqlParameter("@logout_date", logout_date);
                cmd.Parameters.Add(logoutdate);
                SqlParameter houseid = new SqlParameter("@house_id", house_id);
                cmd.Parameters.Add(houseid);

                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //删除设定他项权利摘要
        public int Deleteobligee(int strobligeeid)
        {
            try
            {
                strCon.Open();
                string strSql = "DELETE FROM house_obligee WHERE house_obligee_id ='" + strobligeeid + "' ";
                SqlCommand cmd = new SqlCommand(strSql, strCon);
                SqlParameter parn = new SqlParameter("@house_obligee_id", strobligeeid);
                cmd.Parameters.Add(parn);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }


        //增加使用土地摘要
        public int Inserthouseuseland(string use_area, string company, string card_number, string zidihao, int house_id)
        {
            try
            {
                strCon.Open();
                string strSql = "INSERT INTO house_use_land(use_area,company,card_number,zidihao,house_id) VALUES(@use_area,@company,@card_number,@zidihao,@house_id)";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter usearea = new SqlParameter("@use_area", use_area);
                cmd.Parameters.Add(usearea);
                SqlParameter addcompany = new SqlParameter("@company", company);
                cmd.Parameters.Add(addcompany);
                SqlParameter cardnumber = new SqlParameter("@card_number", card_number);
                cmd.Parameters.Add(cardnumber);
                SqlParameter addzidihao = new SqlParameter("@zidihao", zidihao);
                cmd.Parameters.Add(addzidihao);
                SqlParameter houseid = new SqlParameter("@house_id", house_id);
                cmd.Parameters.Add(houseid);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //更新使用土地
        public int Updataohouseuseland(string use_area, string company, string card_number, string zidihao, int house_id)
        {
            try
            {
                strCon.Open();
                string strSql = "UPDATE  house_use_land  SET use_area=@use_area,company=@company,card_number=@card_number,zidihao=@zidihao WHERE house_id ='" + house_id + "'";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter usearea = new SqlParameter("@use_area", use_area);
                cmd.Parameters.Add(usearea);
                SqlParameter addcompany = new SqlParameter("@company", company);
                cmd.Parameters.Add(addcompany);
                SqlParameter cardnumber = new SqlParameter("@card_number", card_number);
                cmd.Parameters.Add(cardnumber);
                SqlParameter addzidihao = new SqlParameter("@zidihao", zidihao);
                cmd.Parameters.Add(addzidihao);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询编辑使用土地摘要
        public DataSet Selecthouseuselandid(int id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT  use_area,company,card_number,zidihao,house_id FROM house_use_land where house_id= '" + id + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }


        //查询土地使用图片
        public string Selectlandpicture(int id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT  land_figure FROM land where land_id= '" + id + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }



        //查询房产图片
        public string Selecthousepicture(int id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT  house_figure FROM house where house_id= '" + id + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }


        #endregion

        #region 土地证
        //增加土地使用
        public int Insertland(string land_tile, string land_word, string land_number, string land_government, DateTime land_date, string land_user, string land_idcardnumber, string land_address, string land_map, string land_ground, string land_use, string land_use_period, string land_east, string land_south, string land_west, string land_north, string land_office, DateTime land_send_date, string land_managers, string land_licensing_people, string land_remarks, string land_change_items, string land_figure)
        {
            try
            {
                strCon.Open();
                string strSql = "INSERT INTO land(land_tile, land_word, land_number, land_government, land_date , land_user, land_idcardnumber, land_address, land_map, land_ground, land_use, land_use_period, land_east, land_south, land_west, land_north,land_office, land_send_date, land_managers, land_licensing_people, land_remarks, land_change_items, land_figure) VALUES(@land_tile,@land_word,@land_number,@land_government,@land_date ,@land_user,@land_idcardnumber,@land_address,@land_map,@land_ground,@land_use,@land_use_period,@land_east,@land_south,@land_west,@land_north,@land_office,@land_send_date,@land_managers,@land_licensing_people,@land_remarks,@land_change_items,@land_figure)";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter landtitle = new SqlParameter("@land_tile", land_tile);
                cmd.Parameters.Add(landtitle);

                SqlParameter landword = new SqlParameter("@land_word", land_word);
                cmd.Parameters.Add(landword);

                SqlParameter landnumber = new SqlParameter("@land_number", land_number);
                cmd.Parameters.Add(landnumber);
                SqlParameter landgoverment = new SqlParameter("@land_government", land_government);
                cmd.Parameters.Add(landgoverment);
                SqlParameter landdate = new SqlParameter("@land_date", land_date);
                cmd.Parameters.Add(landdate);
                SqlParameter landuser = new SqlParameter("@land_user", land_user);
                cmd.Parameters.Add(landuser);
                SqlParameter landidcardnumber = new SqlParameter("@land_idcardnumber", land_idcardnumber);
                cmd.Parameters.Add(landidcardnumber);

                SqlParameter landaddress = new SqlParameter("@land_address", land_address);
                cmd.Parameters.Add(landaddress);
                SqlParameter landmap = new SqlParameter("@land_map", land_map);
                cmd.Parameters.Add(landmap);
                SqlParameter landground = new SqlParameter("@land_ground", land_ground);
                cmd.Parameters.Add(landground);
                SqlParameter landuse = new SqlParameter("@land_use", land_use);
                cmd.Parameters.Add(landuse);
                SqlParameter land_useperiod = new SqlParameter("@land_use_period", land_use_period);
                cmd.Parameters.Add(land_useperiod);
                SqlParameter landeast = new SqlParameter("@land_east", land_east);
                cmd.Parameters.Add(landeast);
                SqlParameter landsouth = new SqlParameter("@land_south", land_south);
                //,string land_managors,string land_licensing,string land_remarks,string land_changeitems,string land_figure)
                cmd.Parameters.Add(landsouth);
                SqlParameter landwest = new SqlParameter("@land_west", land_west);
                cmd.Parameters.Add(landwest);
                SqlParameter landnorth = new SqlParameter("@land_north", land_north);
                cmd.Parameters.Add(landnorth);
                SqlParameter landoffice = new SqlParameter("@land_office", land_office);
                cmd.Parameters.Add(landoffice);
                SqlParameter land_senddate = new SqlParameter("@land_send_date", land_send_date);
                cmd.Parameters.Add(land_senddate);
                SqlParameter landmanagors = new SqlParameter("@land_managers", land_managers);
                cmd.Parameters.Add(landmanagors);
                SqlParameter landlicensing = new SqlParameter("@land_licensing_people", land_licensing_people);
                cmd.Parameters.Add(landlicensing);

                SqlParameter landremarks = new SqlParameter("@land_remarks", land_remarks);
                cmd.Parameters.Add(landremarks);
                SqlParameter landchangeitems = new SqlParameter("@land_change_items", land_change_items);
                cmd.Parameters.Add(landchangeitems);
                SqlParameter landfigure = new SqlParameter("@land_figure", land_figure);
                cmd.Parameters.Add(landfigure);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //更新land表
        //修改数据
        public int Updataland(string land_tile, string land_word, string land_number, string land_government, DateTime land_date, string land_user, string land_idcardnumber, string land_address, string land_map, string land_ground, string land_use, string land_use_period, string land_east, string land_south, string land_west, string land_north, string land_office, DateTime land_send_date, string land_managers, string land_licensing_people, string land_remarks, string land_change_items, string land_figure, int land_id)
        {
            try
            {
                strCon.Open();
                string strSql = "UPDATE  land  SET land_tile=@land_tile, land_word=@land_word, land_number=@land_number, land_government=@land_government, land_date=@land_date , land_user=@land_user, land_idcardnumber=@land_idcardnumber, land_address=@land_address, land_map=@land_map, land_ground=@land_ground, land_use=@land_use, land_use_period=@land_use_period, land_east=@land_east, land_south=@land_south, land_west=@land_west, land_north=@land_north,land_office=@land_office, land_send_date=@land_send_date, land_managers=@land_managers, land_licensing_people=@land_licensing_people, land_remarks=@land_remarks, land_change_items=@land_change_items, land_figure=@land_figure WHERE land_id ='" + land_id + "'";
                SqlCommand cmd = new SqlCommand(strSql, strCon);
                SqlParameter landtitle = new SqlParameter("@land_tile", land_tile);
                cmd.Parameters.Add(landtitle);

                SqlParameter landword = new SqlParameter("@land_word", land_word);
                cmd.Parameters.Add(landword);

                SqlParameter landnumber = new SqlParameter("@land_number", land_number);
                cmd.Parameters.Add(landnumber);
                SqlParameter landgoverment = new SqlParameter("@land_government", land_government);
                cmd.Parameters.Add(landgoverment);
                SqlParameter landdate = new SqlParameter("@land_date", land_date);
                cmd.Parameters.Add(landdate);
                SqlParameter landuser = new SqlParameter("@land_user", land_user);
                cmd.Parameters.Add(landuser);
                SqlParameter landidcardnumber = new SqlParameter("@land_idcardnumber", land_idcardnumber);
                cmd.Parameters.Add(landidcardnumber);

                SqlParameter landaddress = new SqlParameter("@land_address", land_address);
                cmd.Parameters.Add(landaddress);
                SqlParameter landmap = new SqlParameter("@land_map", land_map);
                cmd.Parameters.Add(landmap);
                SqlParameter landground = new SqlParameter("@land_ground", land_ground);
                cmd.Parameters.Add(landground);
                SqlParameter landuse = new SqlParameter("@land_use", land_use);
                cmd.Parameters.Add(landuse);
                SqlParameter land_useperiod = new SqlParameter("@land_use_period", land_use_period);
                cmd.Parameters.Add(land_useperiod);
                SqlParameter landeast = new SqlParameter("@land_east", land_east);
                cmd.Parameters.Add(landeast);
                SqlParameter landsouth = new SqlParameter("@land_south", land_south);
                //,string land_managors,string land_licensing,string land_remarks,string land_changeitems,string land_figure)
                cmd.Parameters.Add(landsouth);
                SqlParameter landwest = new SqlParameter("@land_west", land_west);
                cmd.Parameters.Add(landwest);
                SqlParameter landnorth = new SqlParameter("@land_north", land_north);
                cmd.Parameters.Add(landnorth);
                SqlParameter landoffice = new SqlParameter("@land_office", land_office);
                cmd.Parameters.Add(landoffice);
                SqlParameter land_senddate = new SqlParameter("@land_send_date", land_send_date);
                cmd.Parameters.Add(land_senddate);
                SqlParameter landmanagors = new SqlParameter("@land_managers", land_managers);
                cmd.Parameters.Add(landmanagors);
                SqlParameter landlicensing = new SqlParameter("@land_licensing_people", land_licensing_people);
                cmd.Parameters.Add(landlicensing);

                SqlParameter landremarks = new SqlParameter("@land_remarks", land_remarks);
                cmd.Parameters.Add(landremarks);
                SqlParameter landchangeitems = new SqlParameter("@land_change_items", land_change_items);
                cmd.Parameters.Add(landchangeitems);
                SqlParameter landfigure = new SqlParameter("@land_figure", land_figure);
                cmd.Parameters.Add(landfigure);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }




        //删除land数据
        public int Deleteland(int land_id)
        {
            try
            {
                strCon.Open();
                string strSql = "DELETE FROM land WHERE land_id = '" + land_id + "'";
                SqlCommand cmd = new SqlCommand(strSql, strCon);
                SqlParameter parn = new SqlParameter("@land_id", land_id);
                cmd.Parameters.Add(parn);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }


        //查询土地使用

        public DataSet Selectland()
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT land_id, land_tile, land_word, land_number, land_government, land_date , land_user, land_idcardnumber, land_address, land_map, land_ground, land_use, land_use_period, land_east, land_south, land_west, land_north,land_office, land_send_date, land_managers, land_licensing_people, land_remarks, land_change_items, land_figure FROM land ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //根据字号查询ID
        public int Selectlandid(string land_word, string land_number)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT land_id FROM land where land_word='" + land_word + "' and land_number='" + land_number + "' ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //根据id查询数据为编辑做准备
        public DataSet Selectlanddateto_edit(int land_id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT land_tile, land_word, land_number, land_government, land_date , land_user, land_idcardnumber, land_address, land_map, land_ground, land_use, land_use_period, land_east, land_south, land_west, land_north,land_office, land_send_date, land_managers, land_licensing_people, land_remarks, land_change_items, land_figure FROM land where land_id='" + land_id + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }


        //添加农村土地
        public int Insertcountryside(string land_area, string cultivated_land, string dry_land, string paddy_field, string orchard, string forest_land, string grassland, string inmate_mining, string construction_land, string homestead_land, string traffic_land, string water_land, string unused_land, int land_id)
        {
            try
            {
                strCon.Open();
                string strSql = "INSERT INTO land_countryside(land_area,cultivated_land,dry_land,paddy_field,orchard,forest_land,grassland,inmate_mining,construction_land,homestead_land,traffic_land,water_land,unused_land,land_id) VALUES(@land_area,@cultivated_land,@dry_land,@paddy_field,@orchard,@forest_land,@grassland,@inmate_mining,@construction_land,@homestead_land,@traffic_land,@water_land,@unused_land,@land_id)";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter landarea = new SqlParameter("@land_area", land_area);
                cmd.Parameters.Add(landarea);
                SqlParameter cultivatedland = new SqlParameter("@cultivated_land", cultivated_land);
                cmd.Parameters.Add(cultivatedland);
                SqlParameter dryland = new SqlParameter("@dry_land", dry_land);
                cmd.Parameters.Add(dryland);
                SqlParameter paddyfield = new SqlParameter("@paddy_field", paddy_field);
                cmd.Parameters.Add(paddyfield);
                SqlParameter landorchard = new SqlParameter("@orchard", orchard);
                cmd.Parameters.Add(landorchard);
                SqlParameter forestland = new SqlParameter("@forest_land", forest_land);
                cmd.Parameters.Add(forestland);
                SqlParameter countgrassland = new SqlParameter("@grassland", grassland);
                cmd.Parameters.Add(countgrassland);
                SqlParameter inmatemining = new SqlParameter("@inmate_mining", inmate_mining);
                cmd.Parameters.Add(inmatemining);
                SqlParameter constructionland = new SqlParameter("@construction_land", construction_land);
                cmd.Parameters.Add(constructionland);
                //homestead_land,traffic_land,water_land,unused_land,land_id
                SqlParameter homesteadland = new SqlParameter("@homestead_land", homestead_land);
                cmd.Parameters.Add(homesteadland);
                SqlParameter trafficland = new SqlParameter("@traffic_land", traffic_land);
                cmd.Parameters.Add(trafficland);
                SqlParameter waterland = new SqlParameter("@water_land", water_land);
                cmd.Parameters.Add(waterland);
                SqlParameter unusedland = new SqlParameter("@unused_land", unused_land);
                cmd.Parameters.Add(unusedland);
                SqlParameter landid = new SqlParameter("@land_id", land_id);
                cmd.Parameters.Add(landid);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //更新农村土地
        public int Updatacountryside(string land_area, string cultivated_land, string dry_land, string paddy_field, string orchard, string forest_land, string grassland, string inmate_mining, string construction_land, string homestead_land, string traffic_land, string water_land, string unused_land, int land_id)
        {
            try
            {
                strCon.Open();
                string strSql = "UPDATE  land_countryside  SET land_area=@land_area,cultivated_land=@cultivated_land,dry_land=@dry_land,paddy_field=@paddy_field,orchard=@orchard,forest_land=@forest_land,grassland=@grassland,inmate_mining=@inmate_mining,construction_land=@construction_land,homestead_land=@homestead_land,traffic_land=@traffic_land,water_land=@water_land,unused_land=@unused_land WHERE land_id ='" + land_id + "'";
                SqlCommand cmd = new SqlCommand(strSql, strCon);
                SqlParameter landarea = new SqlParameter("@land_area", land_area);
                cmd.Parameters.Add(landarea);
                SqlParameter cultivatedland = new SqlParameter("@cultivated_land", cultivated_land);
                cmd.Parameters.Add(cultivatedland);
                SqlParameter dryland = new SqlParameter("@dry_land", dry_land);
                cmd.Parameters.Add(dryland);
                SqlParameter paddyfield = new SqlParameter("@paddy_field", paddy_field);
                cmd.Parameters.Add(paddyfield);
                SqlParameter landorchard = new SqlParameter("@orchard", orchard);
                cmd.Parameters.Add(landorchard);
                SqlParameter forestland = new SqlParameter("@forest_land", forest_land);
                cmd.Parameters.Add(forestland);
                SqlParameter countgrassland = new SqlParameter("@grassland", grassland);
                cmd.Parameters.Add(countgrassland);
                SqlParameter inmatemining = new SqlParameter("@inmate_mining", inmate_mining);
                cmd.Parameters.Add(inmatemining);
                SqlParameter constructionland = new SqlParameter("@construction_land", construction_land);
                cmd.Parameters.Add(constructionland);
                //homestead_land,traffic_land,water_land,unused_land,land_id
                SqlParameter homesteadland = new SqlParameter("@homestead_land", homestead_land);
                cmd.Parameters.Add(homesteadland);
                SqlParameter trafficland = new SqlParameter("@traffic_land", traffic_land);
                cmd.Parameters.Add(trafficland);
                SqlParameter waterland = new SqlParameter("@water_land", water_land);
                cmd.Parameters.Add(waterland);
                SqlParameter unusedland = new SqlParameter("@unused_land", unused_land);
                cmd.Parameters.Add(unusedland);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //根据landid查找对应农村土地数据
        public DataSet Selectcountrysideiddate(int land_id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT land_area,cultivated_land,dry_land,paddy_field,orchard,forest_land,grassland,inmate_mining,construction_land,homestead_land,traffic_land,water_land,unused_land FROM land_countryside where land_id='" + land_id
                    + "' ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        //新增城镇土地
        public int Insertlandtown(string land_area, string built_up_area, string common_area, string sharing_area, string land_grade, int land_id)
        {
            try
            {
                strCon.Open();
                string strSql = "INSERT INTO land_town(land_area,built_up_area,common_area,sharing_area,land_grade,land_id) VALUES(@land_area,@built_up_area,@common_area,@sharing_area,@land_grade,@land_id)";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter landarea = new SqlParameter("@land_area", land_area);
                cmd.Parameters.Add(landarea);
                SqlParameter builtup_area = new SqlParameter("@built_up_area", built_up_area);
                cmd.Parameters.Add(builtup_area);
                SqlParameter commonarea = new SqlParameter("@common_area", common_area);
                cmd.Parameters.Add(commonarea);
                SqlParameter sharingarea = new SqlParameter("@sharing_area", sharing_area);
                cmd.Parameters.Add(sharingarea);
                SqlParameter landgrade = new SqlParameter("@land_grade", land_grade);
                cmd.Parameters.Add(landgrade);

                SqlParameter landid = new SqlParameter("@land_id", land_id);
                cmd.Parameters.Add(landid);

                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //更新城镇土地
        public int Updatalandtown(string land_area, string built_up_area, string common_area, string sharing_area, string land_grade, int land_id)
        {
            try
            {
                strCon.Open();
                string strSql = "UPDATE  land_town  SET land_area=@land_area,built_up_area=@built_up_area,common_area=@common_area,sharing_area=@sharing_area,land_grade=@land_grade WHERE land_id='" + land_id + "'";
                SqlCommand cmd = new SqlCommand(strSql, strCon);
                SqlParameter landarea = new SqlParameter("@land_area", land_area);
                cmd.Parameters.Add(landarea);
                SqlParameter builtup_area = new SqlParameter("@built_up_area", built_up_area);
                cmd.Parameters.Add(builtup_area);
                SqlParameter commonarea = new SqlParameter("@common_area", common_area);
                cmd.Parameters.Add(commonarea);
                SqlParameter sharingarea = new SqlParameter("@sharing_area", sharing_area);
                cmd.Parameters.Add(sharingarea);
                SqlParameter landgrade = new SqlParameter("@land_grade", land_grade);
                cmd.Parameters.Add(landgrade);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //根据landid查询城镇对应数据
        public DataSet Selectlandtowniddate(int land_id)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT land_area,built_up_area,common_area,sharing_area,land_grade FROM land_town where land_id='" + land_id + "'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

        #endregion



        //修改数据
        public int UpdataTest(string strName, string strSex, int intAge)
        {
            try
            {
                strCon.Open();
                string strSql = "UPDATE  Login  SET UPassWord=@strPwd WHERE UName =@strName";
                SqlCommand cmd = new SqlCommand(strSql, strCon);
                SqlParameter name = new SqlParameter("@name", strName);
                cmd.Parameters.Add(name);
                SqlParameter sex = new SqlParameter("@sex", strSex);
                cmd.Parameters.Add(sex);
                SqlParameter age = new SqlParameter("@sex", strSex);
                cmd.Parameters.Add(age);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //查询数据
        public DataSet SelectTest()
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT name,sex,age FROM testtable ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        //删除数据
        public int DeleteTest(string strName)
        {
            try
            {
                strCon.Open();
                string strSql = "DELETE FROM Login WHERE UName = @strName";
                SqlCommand cmd = new SqlCommand(strSql, strCon);
                SqlParameter parn = new SqlParameter("@strName", strName);
                cmd.Parameters.Add(parn);
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }



        //查询登录数据数据
        public int SelectLogin(string login_name, string psw)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT login_id FROM Login where login_name='" + login_name + "' and password='" + psw + "' ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return int.Parse(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }

         //查询登录数据数据
        public string SelectLoginname(string login_name)
        {
            try
            {
                strCon.Open();

                string strSql = "SELECT login_name FROM Login where login_name='" + login_name + "'  ";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);

                    return ds.Tables[0].Rows[0][0].ToString();

                
            }
            catch (Exception ex)
            {
                return "";
                throw ex;
            }
            //finally
            //{
                
            //}
        }



        //增加Login
        public int Insertlogin(string name, string login_name, string password, string user_right)
        {
            try
            {
                strCon.Open();
                string strSql = "INSERT INTO Login(name,login_name,password,user_right) VALUES(@name,@login_name,@password,@user_right)";
                SqlCommand cmd = new SqlCommand(strSql, strCon);


                SqlParameter usename = new SqlParameter("@name", name);
                cmd.Parameters.Add(usename);
                SqlParameter loginname = new SqlParameter("@login_name", login_name);
                cmd.Parameters.Add(loginname);
                SqlParameter loginpassword = new SqlParameter("@password", password);
                cmd.Parameters.Add(loginpassword);
                SqlParameter useright = new SqlParameter("@user_right", user_right);
                cmd.Parameters.Add(useright);
              
                int result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }



        //房屋条件查询
        public DataSet Selecthouseinquiry(string owner , string idcardnumber)
        {
            try
            {
                strCon.Open();

                string strSql = "select * from house where house_owner like '%" + owner + "%'  and house_idcardnumber like '%" + idcardnumber + "%'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }



        //土地条件查询
        public DataSet Selectlandinquiry(string user, string idcardnumber)
        {
            try
            {
                strCon.Open();

                string strSql = "select * from land where land_user like '%" + user + "%'  and land_idcardnumber like '%" + idcardnumber + "%'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(strSql, strCon);
                s.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                strCon.Close();
            }
        }
        

        
    }
}
