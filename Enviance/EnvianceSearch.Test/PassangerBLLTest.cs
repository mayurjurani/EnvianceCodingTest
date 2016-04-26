using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLLFactory;
using IType;
using DTOs;
using System.Collections.Generic;
using Moq;
using System.Reflection;

namespace EnvianceSearch.Test
{
    [TestClass]
    public class PassangerBLLTest
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

        IPassangerBLL objPassangerBLL;

        [TestInitialize]
        public void Initialize()
        {
            objPassangerBLL = PassangerBLLFactory.getPassangerBLLObject();
        }

        [TestMethod]
        public void TestSearchPassangerByName_HappyCase()
        {
            List<PassangerDTO> result =  objPassangerBLL.SearchPassangerByName(FIRSTNAME_MIDDLENAME_LASTNAME);
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(result[0].FirstName, "Mayur");
            Assert.AreEqual(result[0].MiddleName, "I");
            Assert.AreEqual(result[0].LastName, "Jurani");
        }

        [TestMethod]
        public void TestSearchPassangerByName_UsingFirstName()
        {
            List<PassangerDTO> result = objPassangerBLL.SearchPassangerByName(FIRST_NAME);
            Assert.IsTrue(result.Count > 1);            
        }

        [TestMethod]
        public void TestSearchPassangerByName_UsingFirstAndLastName()
        {
            List<PassangerDTO> result = objPassangerBLL.SearchPassangerByName(FIRSTNAME_LASTNAME);
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(result[0].FirstName, "Mayur");
            Assert.AreEqual(result[0].MiddleName, "I");
            Assert.AreEqual(result[0].LastName, "Jurani");
        }

        [TestMethod]
        public void TestSearchPassangerByName_UsingLastAndFirstName()
        {
            List<PassangerDTO> result = objPassangerBLL.SearchPassangerByName(LASTNAME_FIRSTNAME);
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(result[0].FirstName, "Mayur");
            Assert.AreEqual(result[0].MiddleName, "I");
            Assert.AreEqual(result[0].LastName, "Jurani");
        }

        [TestMethod]
        public void TestSearchPassangerByName_LongInvalidParameter()
        {
            List<PassangerDTO> result = objPassangerBLL.SearchPassangerByName(LONG_INVALID_NAME);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void TestSearchPassangerByName_ValidNameWithMultipleSpacs()
        {
            List<PassangerDTO> result = objPassangerBLL.SearchPassangerByName(VALID_NAME_WITH_MULTIPLE_SPACES);
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(result[0].FirstName, "Mayur");
            Assert.AreEqual(result[0].MiddleName, "I");
            Assert.AreEqual(result[0].LastName, "Jurani");
        }

        [TestMethod]
        public void TestSearchPassangerByName_InvalidString()
        {
            List<PassangerDTO> result = objPassangerBLL.SearchPassangerByName(INVALID_STRING);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void TestSearchPassangerByName_InvalidEmptyString()
        {
            List<PassangerDTO> result = objPassangerBLL.SearchPassangerByName(INVALID_EMPTY_STRING);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void TestSearchPassangerByName_ValidStringWithWildcardCharacters()
        {
            List<PassangerDTO> result = objPassangerBLL.SearchPassangerByName(VALID_WITH_WILDCARD_CHARACTERS);
            Assert.IsTrue(result.Count == 1);
            Assert.AreEqual(result[0].FirstName, "Mayur");
            Assert.AreEqual(result[0].MiddleName, "I");
            Assert.AreEqual(result[0].LastName, "Jurani");
        }

        [TestMethod]
        public void TestSearchPassangerByName_DataNotPresent()
        {
            List<PassangerDTO> result = objPassangerBLL.SearchPassangerByName(DATA_NOT_PRESENT_STRING);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void VerifyDALMethodCalled()
        {
            Mock<IPassangerDAL> mockPassangerDAL = new Mock<IPassangerDAL>();
            IPassangerDAL objDAL = mockPassangerDAL.Object;
            objPassangerBLL.GetType().GetField("objPassangerDAL", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objPassangerBLL, objDAL);
            objPassangerBLL.SearchPassangerByName(FIRST_NAME);
            mockPassangerDAL.Verify(t => t.SearchPassangerByName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));
        }

        [TestMethod]
        public void VerifyDALMethodNotCalled()
        {
            Mock<IPassangerDAL> mockPassangerDAL = new Mock<IPassangerDAL>();
            IPassangerDAL objDAL = mockPassangerDAL.Object;
            objPassangerBLL.GetType().GetField("objPassangerDAL", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(objPassangerBLL, objDAL);
            objPassangerBLL.SearchPassangerByName(null);
            mockPassangerDAL.Verify(t => t.SearchPassangerByName(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),Times.Never());
        }
    }
}
