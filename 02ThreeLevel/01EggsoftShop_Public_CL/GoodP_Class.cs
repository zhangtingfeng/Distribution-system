using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eggsoft_Public_CL
{
    public class GoodP_ShopClient_MultiFenXiaoLevel : EggsoftWX.Model.b019_tab_ShopClient_MultiFenXiaoLevel
    {
        public Int32 GoodID { get; set; }
        public Int32 UserID { get; set; }

        public Int32 intParentID { get; set; }
        public Int32 ManagerAgentParentID { get; set; }
        public Int32 ManagerGrandAgentParentID { get; set; }

        /// <summary>
        /// 按照商品的百分比 计算所得
        /// </summary>
        public Decimal AgentGet { get; set; }
        /// <summary>
        /// 按照商品的百分比 计算所得
        /// </summary>
        public Decimal ManagerAgentGet { get; set; }
        /// <summary>
        /// 按照商品的百分比 计算所得
        /// </summary>
        public Decimal ManagerGrandAgentGet { get; set; }

        public Decimal ChildGetbyGoodsPercent { get; set; }
        public Decimal GrandsonGetbyGoodsPercent { get; set; }
        public Decimal GreatsonGetbyGoodsPercent { get; set; }

        public System.Data.DataTable ChildGetList { get; set; }
        public System.Data.DataTable GrandsonGetList { get; set; }
        public System.Data.DataTable GreatsonGetList { get; set; }


        public Int32 intOperationID { get; set; }
        public Int32 OperationParentID { get; set; }
        public Int32 OperationGrandParentID { get; set; }

        public Decimal DecimalGoodPrice { get; set; }



        public String strGoodType { get; set; }
        public String strGoodTypeId { get; set; }
        public String strGoodTypeIdBuyInfo { get; set; }

        public Int32 FenXiaoLevelLength { get; set; }

    }
    public class GoodP_Class
    {
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="Int32UserID"></param>
        //public GoodP_Class(Int32 Int32UserID)
        //{

        //    Console.WriteLine("I am ProgramTest 默认构造函数,Int32UserID={0}", Int32UserID);
        //}


       
    }
}