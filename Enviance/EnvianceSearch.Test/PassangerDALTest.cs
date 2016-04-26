using Microsoft.VisualStudio.TestTools.UnitTesting;
using DALFactory;
using IType;
using System.Data;
using System.Data.SqlClient;

namespace EnvianceSearch.Test
{
    [TestClass]
    public class PassangerDALTest
    {
        private const string NAME1 = "Mayur*";
        private const string NAME2 = "Jurani*";
        private const string NAME3 = "I*";
        IPassangerDAL objPassangerDAL;

        [TestInitialize]
        public void Initialize()
        {
            objPassangerDAL = PassangerDALFactory.getPassangerDALObject();
        }

        [TestMethod]
        public void TestSearchPassangerByName_HappyCase()
        {            
            DataTable dt = objPassangerDAL.SearchPassangerByName(NAME1, NAME2, NAME3);
            Assert.IsTrue(dt.Rows.Count == 1);
            Assert.AreEqual(dt.Rows[0]["FirstName"].ToString(), "Mayur");
        }

        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void TestSearchPassangerByName_EmptyparamterThrowsException()
        {            
            DataTable dt = objPassangerDAL.SearchPassangerByName("", "", "");            
        }
        [TestMethod]
        public void TestSearchPassangerByName_NotPresentRecordparamter()
        {            
            DataTable dt = objPassangerDAL.SearchPassangerByName("abc", "pqr", "xyz");
            Assert.IsTrue(dt.Rows.Count == 0);
        }

        [TestMethod]
        public void TestSearchPassangerByName_RandomOrderParamter()
        {            
            DataTable dt = objPassangerDAL.SearchPassangerByName(NAME2, NAME3, NAME1);
            Assert.IsTrue(dt.Rows.Count == 1);
            Assert.AreEqual(dt.Rows[0]["FirstName"].ToString(), "Mayur");
        }

        [TestMethod]
        public void TestSearchPassangerByName_SingleParameter()
        {            
            DataTable dt = objPassangerDAL.SearchPassangerByName(NAME1, "", "");
            Assert.IsTrue(dt.Rows.Count == 2);
            Assert.AreEqual(dt.Rows[0]["FirstName"].ToString(), "Mayur");
        }
    }
}
