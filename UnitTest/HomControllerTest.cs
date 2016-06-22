using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mvcSample.Controllers;

namespace UnitTest
{
    [TestClass]
    public class HomControllerTest
    {
        [TestInitialize]
        public void init()
        {
          
        }

        [TestMethod]
        public void Test1()
        {
            var testCtl = new HomeController();
            var result = testCtl.Index();
            var viewResult = (result as ViewResult);
            var model = viewResult.Model;



        }
    }
}
