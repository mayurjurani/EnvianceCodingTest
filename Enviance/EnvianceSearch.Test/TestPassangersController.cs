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
        private const string FIRST_NAME = "Mayur";
        private const string FIRSTNAME_LASTNAME = "Mayur Jurani";
        private const string FIRSTNAME_MIDDLENAME_LASTNAME = "Mayur I Jurani";
        private const string LASTNAME_FIRSTNAME = "Jurani Mayur";
        private const string LONG_INVALID_NAME = "Mayur I Jurani Chandiramani";
        private const string VALID_NAME_WITH_MULTIPLE_SPACES = "Mayur               Jurani";
        private const string INVALID_STRING = "*_(&^%";
        private const string INVALID_EMPTY_STRING = "";
        private const string VALID_WITH_WILDCARD_CHARACTERS = "       Mayur ...(((**    && Jurani    ";
        private const string DATA_NOT_PRESENT_STRING = "Hello World";

        [TestMethod]
        public void TestGetPassangersByName()
        {
            var controller = new PassangersController();
            var result = controller.GetPassangersByName(FIRST_NAME) as OkNegotiatedContentResult<List<PassangerDTO>>;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count == 2);
            Assert.AreEqual("Mayur", result.Content[0].FirstName);
        }


        [TestMethod]
        public void TestGetPassangersByName_FirstnameAndLastName()
        {
            var controller = new PassangersController();
            var result = controller.GetPassangersByName(FIRSTNAME_LASTNAME) as OkNegotiatedContentResult<List<PassangerDTO>>;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count == 1);
            Assert.AreEqual("Mayur", result.Content[0].FirstName);
        }

        [TestMethod]
        public void TestGetPassangersByName_FirstMiddleLastName()
        {
            var controller = new PassangersController();
            var result = controller.GetPassangersByName(FIRSTNAME_MIDDLENAME_LASTNAME) as OkNegotiatedContentResult<List<PassangerDTO>>;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count == 1);
            Assert.AreEqual("Mayur", result.Content[0].FirstName);
        }

        [TestMethod]
        public void TestGetPassangersByName_LastNameAndFirstName()
        {
            var controller = new PassangersController();
            var result = controller.GetPassangersByName(LASTNAME_FIRSTNAME) as OkNegotiatedContentResult<List<PassangerDTO>>;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count == 1);
            Assert.AreEqual("Mayur", result.Content[0].FirstName);
        }

        [TestMethod]
        public void TestGetPassangersByName_LongInvalidName()
        {
            var controller = new PassangersController();
            var result = controller.GetPassangersByName(LONG_INVALID_NAME) as NotFoundResult;
            Assert.IsNotNull(result); 
        }

        [TestMethod]
        public void TestGetPassangersByName_ValidWithMultipleSpace()
        {
            var controller = new PassangersController();
            var result = controller.GetPassangersByName(VALID_NAME_WITH_MULTIPLE_SPACES) as OkNegotiatedContentResult<List<PassangerDTO>>;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count == 1);
            Assert.AreEqual("Mayur", result.Content[0].FirstName);
        }

        [TestMethod]
        public void TestGetPassangersByName_ValidWithWildcardCharacter()
        {
            var controller = new PassangersController();
            var result = controller.GetPassangersByName(VALID_WITH_WILDCARD_CHARACTERS) as OkNegotiatedContentResult<List<PassangerDTO>>;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count == 1);
            Assert.AreEqual("Mayur", result.Content[0].FirstName);
        }

        [TestMethod]
        public void TestGetPassangersByName_InvaliString()
        {
            var controller = new PassangersController();
            var result = controller.GetPassangersByName(INVALID_STRING) as NotFoundResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetPassangersByName_InvalidEmptyName()
        {
            var controller = new PassangersController();
            var result = controller.GetPassangersByName(INVALID_EMPTY_STRING) as NotFoundResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetPassangersByName_DataNotPresent()
        {
            var controller = new PassangersController();
            var result = controller.GetPassangersByName(DATA_NOT_PRESENT_STRING) as NotFoundResult;
            Assert.IsNotNull(result);
        }
    }
}
