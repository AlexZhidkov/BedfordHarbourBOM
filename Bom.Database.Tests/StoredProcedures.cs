using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;

namespace Bom.Database.Tests
{
    [TestClass]
    public class StoredProcedures
    {
        private SqlConnection m_conn;
        private string m_connetionString = "Data Source=localhost\\SQLEXPR2014;Initial Catalog=BOMDB_test;Trusted_Connection=Yes;";

        private void ExecuteSql(string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, m_conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        private int RunRecalculateCost(int partId)
        {
            SqlCommand cmd = new SqlCommand("RecalculateCostsForAssembly", m_conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@partId", partId));
            decimal tc = 0;
            cmd.Parameters.Add(new SqlParameter("@TotalCost", tc));
            SqlParameter retval = cmd.Parameters.Add("@TotalCost", SqlDbType.Decimal);
            retval.Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            int result = Convert.ToInt32(retval.Value);
            return result;
        }

        [TestInitialize]
        public void Initialize()
        {
            m_conn = new SqlConnection(m_connetionString);
            m_conn.Open();

            // clean up from previous run
            ExecuteSql("DELETE FROM Parts WHERE Id >= 500");
            ExecuteSql("DELETE FROM Subassemblies WHERE AssemblyId >= 500");
            ExecuteSql("SET IDENTITY_INSERT Parts ON"); // allow to set Id manually

            string sql = @"INSERT INTO Parts(Id, Type, IsOwnMake, Length, OwnCost, ComponentsCost) VALUES                          (500, 0, 0, 0,    0, 0),
                           (501, 0, 0, 0,     10, 0),
                           (502, 0, 0, 0,     20, 0),
                           (503, 0, 0, 0,     30, 0),
                           (504, 0, 0, 0,     40, 0),
                           (505, 0, 0, 0,     50, 0),
                           (506, 0, 0, 0,     60, 0),
                           (507, 0, 0, 0,     70, 0),
                           (508, 0, 0, 0,     80, 0),

                           (511, 0, 0, 0,      0, 0),
                           (512, 0, 0, 0,      0, 0),
                           (513, 0, 0, 0,   1000, 0),
                           (514, 0, 0, 0,      0, 0),
                           (554, 0, 0, 0, 163.32, 0)";
            ExecuteSql(sql);

            sql = @"INSERT INTO Subassemblies(AssemblyId, SubassemblyId, InheritedCost, CostContribution) VALUES
                           (503, 501, 0, 1),

                           (504, 502, 0, 2),

                           (505, 501, 0, 1),
                           (505, 502, 0, 1),

                           (506, 501, 0, 2),
                           (506, 502, 0, 2),

                           (507, 504, 0, 2),
                           (507, 505, 0, 2),

                           (508, 501, 0, 4),
                           (508, 502, 0, 3),
                           (508, 503, 0, 2),
                           (508, 504, 0, 2),
                           (508, 505, 0, 3),
                           (508, 506, 0, 2),
                           (508, 507, 0, 1),

                           (500, 502, 0, 5),
                           (500, 507, 0, 1),

                           (511, 512, 0, 1),
                           (512, 513, 0, 1),
                           (513, 514, 0, 1),
                           (514, 554, 0, 2)";
            ExecuteSql(sql);

        }
        
        [TestMethod] // 1
        public void SPRecalculateCostSimple()
        {
            int res = RunRecalculateCost(501); // just own cost, no compound
            Assert.AreEqual(10, res);
        }

        [TestMethod] // 2
        public void SPRecalculateCostCompoundOne()
        {
            int res = RunRecalculateCost(503); // 503 own cost + 501 own cost
            Assert.AreEqual(40, res);
        }

        [TestMethod] // 3
        public void SPRecalculateCostCompoundOneQuant2()
        {
            int res = RunRecalculateCost(504); // 504 + 2*502
            Assert.AreEqual(80, res);
        }

        [TestMethod] // 4
        public void SPRecalculateCostCompoundTwo()
        {
            int res = RunRecalculateCost(505); // 505 + (501 + 502)
            Assert.AreEqual(80, res);
        }

        [TestMethod] // 5
        public void SPRecalculateCostCompoundTwoQuant2()
        {
            int res = RunRecalculateCost(506); // 506 + (2*501 + 2*502)
            Assert.AreEqual(120, res);
        }

        [TestMethod] // 6
        public void SPRecalculateCostCompoundLev2()
        {
            int res = RunRecalculateCost(507); // 507 + (2*504 + 2*505)
            Assert.AreEqual(390, res);
        }

        [TestMethod] // 7
        public void SPRecalculateCostCompoundComplex()
        {
            // 80 (own) + 4*501 + 3*502 + 2*503 + 2*504 + 3*505 + 2*506 + 507 = 
            // 80       + 40    + 60    + 2*40  + 2*80  + 3*80  + 2*120 + 390 = 
            // 180                      + 80    + 160   + 240   + 240   + 390 =
            // 1290
            int res = RunRecalculateCost(508); 
            Assert.AreEqual(1290, res);
        }

        [TestMethod] // 8
        public void SPRecalculateCostNoOwnCost()
        {
            int res = RunRecalculateCost(500); // 0 + 5*502 + 507
            Assert.AreEqual(490, res);
        }

        [TestMethod] // 9
        public void SPRecalculateCostBin()
        {
            int res = RunRecalculateCost(511); // 0 + 0 + 1000 + 0 + 2*163.32
            Assert.AreEqual(1326, res);
        }

        [TestCleanup]
        public void CleanUp()
        {
            // leave tables with test data to allow debuging the SP with Management Studio
            m_conn.Close();
        }
    }
}
