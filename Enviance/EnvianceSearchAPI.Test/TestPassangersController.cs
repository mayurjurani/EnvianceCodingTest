using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTOs;
using System.Web.Http.Results;
using EnvianceSearchAPI.Controllers;
using System.Collections.Generic;

namespace EnvianceSearchAPI.Test
{
    [TestClass]
    public class TestPassangersController
    {
        [TestMethod]
        public void TestGetPassangersByName(string name)
        {
            var controller = new PassangersController();
            var result = controller.GetPassangersByName("Mayur") as OkNegotiatedContentResult<List<PassangerDTO>>;
            Assert.IsNotNull(result);
            Assert.AreEqual("Mayur", result.Content[0].FirstName);
        }


        [TestMethod]
        public void TestGetPassangersByName_FirstnameAndLastName(string name)
        {
            var controller = new PassangersController();
            var result = controller.GetPassangersByName("Mayur Jurani") as OkNegotiatedContentResult<List<PassangerDTO>>;
            Assert.IsNotNull(result);
            Assert.AreEqual("Mayur", result.Content[0].FirstName);
        }
    }
}
