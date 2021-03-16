using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using EggsoftWX.IDAL;
namespace EggsoftWX.BLL
{
	/// <summary>
	/// 业务逻辑类tab_Shopping_Vouchers 的摘要说明。
	/// </summary>
	public class tab_Shopping_Vouchers_RunProcedure
	{
        Itab_Shopping_Vouchers_RunProcedure dal = EggsoftWX.DALFactory.tab_Shopping_Vouchers_RunProcedure.Create();
        public tab_Shopping_Vouchers_RunProcedure()
		{}
	
        #region 增加2000条数据 model  memo 002000
        public bool AddAllNum(string[] strSetstrValue)
        {
            return dal.AddAllNum(strSetstrValue);
        
        }


		#endregion  成员方法
	}
}
