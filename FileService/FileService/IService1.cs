using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Data;

namespace FileService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        #region 房屋所有权证


        //新增所有权性质
        [OperationContract]
        int Inserthousenature(string nature);
        //根据id查询natureid,编辑使用
        [OperationContract]
        int selece_nature_id_fromhouse(int house_id);
        //删除所有权性质
        [OperationContract]
        int Deletehousenature(string nature);
        //查询所有权性质
        [OperationContract]
        DataSet Selecthousenature();
        //查询所有权性质ID
        [OperationContract]
        int selecenayureID(string strnature);
        //检查所有权性质
        [OperationContract]
        int checkhousenature(string strnature);
        //根据id查询值,编辑使用
        [OperationContract]
        string selecenatureID(int strnature);

        //新增共有人
        [OperationContract]
        int Inserthousencommonor(string commonor, string share, string number, string comments, int house_id);
        //查询共有人
        [OperationContract]
        DataSet Selecthousecommonor(int Selecthousecommonor);
        //查询其中一个共有人
        [OperationContract]
        string Selecthousecommonortop1(int houseid);
        //查询编辑共有人
        [OperationContract]
        DataSet Selecthousecommonorid(int id);
        //更新共有人
        [OperationContract]
        int Updatacommonor(string commonor, string share, string number, string comments, int house_id, int commonorid);
        //删除共有人
        [OperationContract]
        int Deletecommonor(int strcommonorid);

        //新增房屋所有权数据
        [OperationContract]
        int Inserthouse(string house_word, string house_number, string house_owner, int house_nature_id, string house_idcardnumber, int house_located_id, string house_dihao, string house_postscript, string house_witness, string house_proofreader, string house_autograph, DateTime house_licensing_date, string house_office, string house_banzhenren, DateTime house_tianfatime, string house_figure);
        [OperationContract]
        int Updatahouse(string house_word, string house_number, string house_owner, int house_nature_id, string house_idcardnumber, int house_located_id, string house_dihao, string house_postscript, string house_witness, string house_proofreader, string house_autograph, DateTime house_licensing_date, string house_office, string house_banzhenren, DateTime house_tianfatime, string house_figure, int houseid);
        //删除房屋所有权表记录
        [OperationContract]
        int Deletehouse(int strhouseid);
        //查询房屋所有权表
        [OperationContract]
        DataSet Selecthouse();
        //查询编辑房屋所有权表
        [OperationContract]
        DataSet Selecthouseid(int id);
        //通过字号查询ID
        [OperationContract]
        int Selecthouseiddate(string word, string number);

        //新增房屋座落
        [OperationContract]
        int Inserthouselocated(string located);
        //查询房屋座落
        [OperationContract]
        DataSet Selecthouselocated();
        //根据id查询locatedid,编辑使用
        [OperationContract]
        int selece_located_id_fromhouse(int house_id);
        //查询房屋座落ID
        [OperationContract]
        int selectlocatedID(string strlocated);
        //根据id查询值,编辑使用
        [OperationContract]
        string selece_located_id(int strlocated);
        //检查房屋座落
        [OperationContract]
        int checkhouselocated(string strlocated);

        //新增房屋状况数据
        [OperationContract]
        int Insertcondition(string building_number, string room_number, string jianshu, string building_structure, string cengshu, string built_up_area, string remarks, int house_id);
        //查询房屋状况
        [OperationContract]
        DataSet Selectcondition(int houseid);
        //查询编辑房屋状况
        [OperationContract]
        DataSet Selectconditionid(int id);
        //更新房屋状况
        [OperationContract]
        int Updatacondition(string building_number, string room_number, string jianshu, string building_structure, string cengshu, string built_up_area, string remarks, int house_id, int conditionid);
        //删除房屋状况
        [OperationContract]
        int Deletecondition(int strconditionid);

        //增加契税摘要
        [OperationContract]
        int Inserthousedeedtax(DateTime datetime, string price, string type, string tax_rate, string amount_money, string remarks, int house_id);
        //更新契税
        [OperationContract]
        int Updatehousedeedtax(DateTime datetime, string price, string type, string tax_rate, string amount_money, string remarks, int house_id);
        //查询编辑契税摘要
        [OperationContract]
        DataSet Selecthousedeedtaxid(int id);

        //增加设定他项权利摘要
        [OperationContract]
        int Inserthouseobligee(string obligee, string type, string room_number, string jianshu, string built_up_area, string right_value, string duration_right, DateTime logout_date, int house_id);
        //查询他项权利摘要
        [OperationContract]
        DataSet Selectobligee(int house_id);
        //查询编辑他项权利摘要
        [OperationContract]
        DataSet Selectobligeeid(int oblid);
        //更新他项权利摘要
        [OperationContract]
        int Updataobligee(string obligee, string type, string room_number, string jianshu, string built_up_area, string right_value, string duration_right, DateTime logout_date, int house_id, int oblid);
        //删除设定他项权利摘要
        [OperationContract]
        int Deleteobligee(int strobligeeid);

        //增加使用土地摘要
        [OperationContract]
        int Inserthouseuseland(string use_area, string company, string card_number, string zidihao, int house_id);
        //更新使用土地
        [OperationContract]
        int Updataohouseuseland(string use_area, string company, string card_number, string zidihao, int house_id);
        //查询编辑使用土地摘要
        [OperationContract]
        DataSet Selecthouseuselandid(int id);
        //查询房产图片
        [OperationContract]
        string Selecthousepicture(int id);
        #endregion

        #region 土地证

        //新增土地使用
        [OperationContract]
        int Insertland(string land_tile, string land_word, string land_number, string land_government, DateTime land_date, string land_user, string land_idcardnumber, string land_address, string land_map, string land_ground, string land_use, string land_use_period, string land_east, string land_south, string land_west, string land_north, string land_office, DateTime land_send_date, string land_managers, string land_licensing_people, string land_remarks, string land_change_items, string land_figure);
        //删除land数据
        [OperationContract]
        int Deleteland(int land_id);
        //修改数据
        [OperationContract]
        int Updataland(string land_tile, string land_word, string land_number, string land_government, DateTime land_date, string land_user, string land_idcardnumber, string land_address, string land_map, string land_ground, string land_use, string land_use_period, string land_east, string land_south, string land_west, string land_north, string land_office, DateTime land_send_date, string land_managers, string land_licensing_people, string land_remarks, string land_change_items, string land_figure, int land_id);
        //查询土地使用
        [OperationContract]
        DataSet Selectland();
        //查询土地使用图片
        [OperationContract]
        string Selectlandpicture(int id);
        //根据字号查询ID
        [OperationContract]
        int Selectlandid(string land_word, string land_number);
        //根据id查询数据为编辑做准备
        [OperationContract]
        DataSet Selectlanddateto_edit(int land_id);

        //新增农村土地
        [OperationContract]
        int Insertcountryside(string land_area, string cultivated_land, string dry_land, string paddy_field, string orchard, string forest_land, string grassland, string inmate_mining, string construction_land, string homestead_land, string traffic_land, string water_land, string unused_land, int land_id);
        //根据landid查找对应农村土地数据
        [OperationContract]
        DataSet Selectcountrysideiddate(int land_id);
        //更新农村土地
        [OperationContract]
        int Updatacountryside(string land_area, string cultivated_land, string dry_land, string paddy_field, string orchard, string forest_land, string grassland, string inmate_mining, string construction_land, string homestead_land, string traffic_land, string water_land, string unused_land, int land_id);

        //新增城镇土地
        [OperationContract]
        int Insertlandtown(string land_area, string built_up_area, string common_area, string sharing_area, string land_grade, int land_id);
        //更新城镇土地
        [OperationContract]
        int Updatalandtown(string land_area, string built_up_area, string common_area, string sharing_area, string land_grade, int land_id);
        //根据landid查询城镇对应数据
        [OperationContract]
        DataSet Selectlandtowniddate(int land_id);

        #endregion

        #region 登录
        //查询登录数据数据
        [OperationContract]
        string SelectLoginname(string login_name);
        //增加Login
        [OperationContract]
        int Insertlogin(string name, string login_name, string password, string user_right);
        #endregion

        //房屋条件查询
        [OperationContract]
        DataSet Selecthouseinquiry(string owner, string idcardnumber);
        
        //土地条件查询
        [OperationContract]
        DataSet Selectlandinquiry(string user, string idcardnumber);

        //删除数据
        [OperationContract]
        int DeleteTest(string strName);
        //修改数据
        [OperationContract]
        int UpdataTest(string strName, string strSex, int intAge);
        //查询数据
        [OperationContract]
        DataSet SelectTest();


        //查询登录数据数据
        [OperationContract]
        int SelectLogin(string login_name, string psw);


        //上传文件
        [OperationContract]
        UpFileResult UpLoadFile(UpFile filestream);

        //下载文件
        [OperationContract]
        DownFileResult DownLoadFile(DownFile downfile);

        // TODO: 在此添加您的服务操作
    }

    // 使用下面示例中说明的数据协定将复合类型添加到服务操作
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [MessageContract]
    public class DownFile
    {
        [MessageHeader]
        public string FileName { get; set; }
    }

    [MessageContract]
    public class UpFileResult
    {
        [MessageHeader]
        public bool IsSuccess { get; set; }
        [MessageHeader]
        public string Message { get; set; }
    }

    [MessageContract]
    public class UpFile
    {
        [MessageHeader]
        public long FileSize { get; set; }
        [MessageHeader]
        public string FileName { get; set; }
        [MessageBodyMember]
        public Stream FileStream { get; set; }
    }

    [MessageContract]
    public class DownFileResult
    {
        [MessageHeader]
        public long FileSize { get; set; }
        [MessageHeader]
        public bool IsSuccess { get; set; }
        [MessageHeader]
        public string Message { get; set; }
        [MessageBodyMember]
        public Stream FileStream { get; set; }
    }
    
}
