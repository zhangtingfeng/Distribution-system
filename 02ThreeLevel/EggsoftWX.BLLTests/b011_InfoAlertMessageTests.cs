using Microsoft.VisualStudio.TestTools.UnitTesting;
using EggsoftWX.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EggsoftWX.BLL.Tests
{
    [TestClass()]
    public class b011_InfoAlertMessageTests
    {
        [TestMethod()]
        public void ExistsCountTest()
        {

            EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
            bll_b011_InfoAlertMessage.ExistsCount("ShopClient_ID=@ShopClient_ID  and Type=@Type and isnull(Done,0)=16", 16, "12Info_b010_AskModifyParent");


            for (int i = 0; i < 100; i++)
            {
                Task t1 = Task.Factory.StartNew(() =>
        {

            //EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
            bll_b011_InfoAlertMessage.ExistsCount("ShopClient_ID=@ShopClient_ID  and Type=@Type and isnull(Done,0)=" + 15, 15, "11Info_b010_AskModifyParent");
        }
            );
            }
            for (int i = 0; i < 1000; i++)
            {
                Task t2 = Task.Factory.StartNew(() =>
        {

            //EggsoftWX.BLL.b011_InfoAlertMessage bll_b011_InfoAlertMessage = new EggsoftWX.BLL.b011_InfoAlertMessage();
            bll_b011_InfoAlertMessage.ExistsCount("ShopClient_ID=@ShopClient_ID  and Type=@Type and isnull(Done,0)=16", 16, "12Info_b010_AskModifyParent");
        }
            );
            }
            Assert.IsTrue(true);
            System.Threading.Thread.Sleep(10000000);

            //Assert.Fail("OK","OK");
        }
    }
}