using Microsoft.VisualStudio.TestTools.UnitTesting;
using EggsoftWX.SQLServerDAL.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EggsoftWX.SQLServerDAL.Util.Tests
{
    [TestClass()]
    public class ChangeParamsObjectToSqlParameterTests
    {
        [TestMethod()]
        public void ChangeParamsObjectToSqlParameterActionTest()
        {
            ChangeParamsObjectToSqlParameter.ChangeParamsObjectToSqlParameterAction("select count(*) from [b011_InfoAlertMessage] where 1>0 and shopclient_id=@shopclient_id and userid=@userid and type=@type and readed=0", 6666, 666);

            Assert.Fail();
        }
    }
}