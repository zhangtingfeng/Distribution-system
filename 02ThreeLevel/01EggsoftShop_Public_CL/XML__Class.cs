using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;


namespace Eggsoft_Public_CL
{
    /// <summary>
    /// 运费模版 便于检索使用
    /// </summary>
    public class XML_Sheng_ID_Name
    {// 取静态化字符串 有利于前台调用 增强性能
        public const string strXML_Sheng_ID_NamePub = "<?xml version=\"1.0\" encoding=\"gb2312\"?><ArrayOfAnyType xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>1</ShengID><ShengName>北京市</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>38</ShengID><ShengName>天津市</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>96</ShengID><ShengName>河北省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>268</ShengID><ShengName>山西省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>387</ShengID><ShengName>内蒙古自治区</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>1261</ShengID><ShengName>山东省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>19</ShengID><ShengName>上海市</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>776</ShengID><ShengName>江苏省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>882</ShengID><ShengName>浙江省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>972</ShengID><ShengName>安徽省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>1162</ShengID><ShengName>江西省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>56</ShengID><ShengName>重庆市</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2040</ShengID><ShengName>四川省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2221</ShengID><ShengName>贵州省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2309</ShengID><ShengName>云南省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2438</ShengID><ShengName>西藏自治区</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>488</ShengID><ShengName>辽宁省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>588</ShengID><ShengName>吉林省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>648</ShengID><ShengName>黑龙江省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>1077</ShengID><ShengName>福建省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>1784</ShengID><ShengName>广东省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>1907</ShengID><ShengName>广西壮族自治区</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2016</ShengID><ShengName>海南省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>1401</ShengID><ShengName>河南省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>1560</ShengID><ShengName>湖北省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>1662</ShengID><ShengName>湖南省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2511</ShengID><ShengName>陕西省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2618</ShengID><ShengName>甘肃省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2705</ShengID><ShengName>青海省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2748</ShengID><ShengName>宁夏回族自治区</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2769</ShengID><ShengName>新疆维吾尔自治区</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2868</ShengID><ShengName>台湾省</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2893</ShengID><ShengName>香港特别行政区</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>2911</ShengID><ShengName>澳门特别行政区</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>3401</ShengID><ShengName>北美洲</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>3402</ShengID><ShengName>南美洲</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>3403</ShengID><ShengName>亚洲</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>3404</ShengID><ShengName>非洲</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>3405</ShengID><ShengName>欧洲</ShengName></anyType><anyType xsi:type=\"XML_Sheng_ID_Name\"><ShengID>3406</ShengID><ShengName>大洋洲</ShengName></anyType></ArrayOfAnyType>";


        private int _ShengID;
        private string _Name;

        /// <summary>
        /// 
        /// </summary>
        public int ShengID
        {
            set { _ShengID = value; }
            get { return _ShengID; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShengName
        {
            set { _Name = value; }
            get { return _Name; }
        }
    }


    /// <summary>
    /// 运费模版 便于检索使用
    /// </summary>
    public class XML_YunFei_GoodPrice_GoodCount_GoodsMoney_AllGoodsJianShu_AllGoodsMoney
    {//      
        private int _GoodID;//商品的ID
        private Decimal _GoodPrice;//商品的单价
        private int _GoodCount;//商品的个数
        private Decimal _GoodsMoney;//商品的总值
        private int _FreightTemplate_ID;//商品的模版ID
        private int _AllGoodsJianShuNoFright;//商品的个数包邮条件
        private Decimal _AllGoodsMoneyNoFright;//商品的金额包邮条件

        /// <summary>
        /// 1
        /// </summary>
        public int GoodID
        {
            set { _GoodID = value; }
            get { return _GoodID; }
        }
        /// <summary>
        /// 2
        /// </summary>
        public Decimal GoodPrice
        {
            set { _GoodPrice = value; }
            get { return _GoodPrice; }
        }
        /// <summary>
        /// 3
        /// </summary>
        public int GoodCount
        {
            set { _GoodCount = value; }
            get { return _GoodCount; }
        }

        /// <summary>
        /// 4
        /// </summary>
        public Decimal GoodsMoney
        {
            set { _GoodsMoney = value; }
            get { return _GoodsMoney; }
        }

        /// <summary>
        /// 5
        /// </summary>
        public int FreightTemplate_ID
        {
            set { _FreightTemplate_ID = value; }
            get { return _FreightTemplate_ID; }
        }
        /// <summary>
        /// 5
        /// </summary>
        public int AllGoodsJianShuNoFright
        {
            set { _AllGoodsJianShuNoFright = value; }
            get { return _AllGoodsJianShuNoFright; }
        }

        /// <summary>
        /// 6
        /// </summary>
        public Decimal AllGoodsMoneyNoFright
        {
            set { _AllGoodsMoneyNoFright = value; }
            get { return _AllGoodsMoneyNoFright; }
        }
    }


    public class XML__Class_FahuoDan
    {

        #region Model FahuoDan
        //ID,ClassName,ClassIcon,Updatetime,ClassInfo,Sort,IsShow,IsLock,

        private string _FaHuoGongSi;
        private string _FaHuoDanHao;
        private string _ShouHuoRenXinMing;
        private string _ShouHuoRenDianHua;
        private string _ShouHuoRenDiZhi;
        private string _FaHuoRenXingMing;
        private string _FaHuoRenXDianHua;
        private string _FaHuoRenDiZhi;


        /// <summary>
        /// 
        /// </summary>
        public string FaHuoGongSi
        {
            set { _FaHuoGongSi = value; }
            get { return _FaHuoGongSi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FaHuoDanHao
        {
            set { _FaHuoDanHao = value; }
            get { return _FaHuoDanHao; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShouHuoRenXinMing
        {
            set { _ShouHuoRenXinMing = value; }
            get { return _ShouHuoRenXinMing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShouHuoRenDianHua
        {
            set { _ShouHuoRenDianHua = value; }
            get { return _ShouHuoRenDianHua; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ShouHuoRenDiZhi
        {
            set { _ShouHuoRenDiZhi = value; }
            get { return _ShouHuoRenDiZhi; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FaHuoRenXingMing
        {
            set { _FaHuoRenXingMing = value; }
            get { return _FaHuoRenXingMing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FaHuoRenXDianHua
        {
            set { _FaHuoRenXDianHua = value; }
            get { return _FaHuoRenXDianHua; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FaHuoRenDiZhi
        {
            set { _FaHuoRenDiZhi = value; }
            get { return _FaHuoRenDiZhi; }
        }

        #endregion Model


    }


    public class XML__Class_Shop_Client
    {



        #region Model Shop_Client
        //ID,ClassName,ClassIcon,Updatetime,ClassInfo,Sort,IsShow,IsLock,
        private bool _Bool_AddWatermater_Logo_;

        private bool _CheckEmail;
        private String _Email;
        private String _Back_Color;
        private String _FontColor;
        private String _MenuBarColor;
        private String _WeiXinErWeiMaIMGMakeTuiGuang;
        private String _WeiXinGongZhongPingTaiErWeiMaIMG;
        private String _WeiXinErWeiMaIMG;
        private String _ShopLogoImage;
        private int _IntGoodClassShowType;
        private String _WeiXinRalationUserIDList;
        private string _stringPowerList;
        private string _AcceptMsgList;

        /// <summary>
        /// 
        /// </summary>
        public bool Bool_AddWatermater_Logo_
        {
            set { _Bool_AddWatermater_Logo_ = value; }
            get { return _Bool_AddWatermater_Logo_; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool CheckEmail
        {
            set { _CheckEmail = value; }
            get { return _CheckEmail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String Email
        {
            set { _Email = value; }
            get { return _Email; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String Back_Color
        {
            set { _Back_Color = value; }
            get { return _Back_Color; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String FontColor
        {
            set { _FontColor = value; }
            get { return _FontColor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String MenuBarColor
        {
            set { _MenuBarColor = value; }
            get { return _MenuBarColor; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String ShopLogoImage
        {
            set { _ShopLogoImage = value; }
            get { return _ShopLogoImage; }
        }


        /// <summary>
        /// 
        /// </summary>
        public String WeiXinGongZhongPingTaiErWeiMaIMG
        {
            set { _WeiXinGongZhongPingTaiErWeiMaIMG = value; }
            get { return _WeiXinGongZhongPingTaiErWeiMaIMG; }
        }


        /// <summary>
        /// 
        /// </summary>
        public String WeiXinErWeiMaIMG
        {
            set { _WeiXinErWeiMaIMG = value; }
            get { return _WeiXinErWeiMaIMG; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String WeiXinErWeiMaIMGMakeTuiGuang
        {
            set { _WeiXinErWeiMaIMGMakeTuiGuang = value; }
            get { return _WeiXinErWeiMaIMGMakeTuiGuang; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IntGoodClassShowType
        {
            set { _IntGoodClassShowType = value; }
            get { return _IntGoodClassShowType; }
        }

        // <summary>
        /// 
        /// </summary>
        public string stringPowerList
        {
            set { _stringPowerList = value; }
            get { return _stringPowerList; }
        }


        /// <summary>
        /// 
        /// </summary>
        public String WeiXinRalationUserIDList
        {
            set { _WeiXinRalationUserIDList = value; }
            get { return _WeiXinRalationUserIDList; }
        }


        /// <summary>
        /// 
        /// </summary>
        public String AcceptMsgList
        {
            set { _AcceptMsgList = value; }
            get { return _AcceptMsgList; }
        }

        #endregion Model
    }



    public class XML__Class_Shop_O2o
    {



        #region Model Shop_Client
        //ID,ClassName,ClassIcon,Updatetime,ClassInfo,Sort,IsShow,IsLock,

        private bool _CheckEmail;
        private String _Email;
        private String _ShopLogoo2oImage;
        private String _WeiXinRalationUserIDList;


        /// <summary>
        /// 
        /// </summary>
        public bool CheckEmail
        {
            set { _CheckEmail = value; }
            get { return _CheckEmail; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String Email
        {
            set { _Email = value; }
            get { return _Email; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String ShopLogoo2oImage
        {
            set { _ShopLogoo2oImage = value; }
            get { return _ShopLogoo2oImage; }
        }


        /// <summary>
        /// 
        /// </summary>
        public String WeiXinRalationUserIDList
        {
            set { _WeiXinRalationUserIDList = value; }
            get { return _WeiXinRalationUserIDList; }
        }



        #endregion Model
    }

    public class XML__Class_Shop_Goods
    {



        #region Model XML__Class_Shop_Goods
        //ID,ClassName,ClassIcon,Updatetime,ClassInfo,Sort,IsShow,IsLock,


        private String _Mp3path;


        /// <summary>
        /// 
        /// </summary>
        public String Mp3path
        {
            set { _Mp3path = value; }
            get { return _Mp3path; }
        }

        #endregion Model
    }
}