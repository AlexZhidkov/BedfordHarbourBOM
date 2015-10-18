using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Bom.Database.Tests
{
    [TestClass]
    public class StoredProceduresTests
    {
        public enum StoredProcedures { RecalculateCostsForAssembly = 1, RecalculateRecurse };

        public StoredProceduresTests()
        {
            makeConnectionStrings();
        }

        [TestInitialize]
        public void Initialize()
        {
            CreateUnitTestDbUsingMasterDbConnection();
            CreatePartsTable();
            CreateSubassembliesTable();
        }

        // use only one [TestMethod] for each stored procedure in order to 
        // not to create/drop database for each test, which is time consuming
        [TestMethod]
        public void StoredProcedureRecalculateCostsForAssembly()
        {
            RecalculateCostsForAssemblyHelper helper = (RecalculateCostsForAssemblyHelper)makeStoredProcedureHelperBase(StoredProcedures.RecalculateCostsForAssembly, m_connectionString);
            helper.InitTableData();

            // 1 StoredProcedureRecalculateCostSimple
            int res = helper.Run(501); // just own cost, no compound
            Assert.AreEqual(10, res);

            // 2 StoredProcedureRecalculateCostCompoundOne
            res = helper.Run(503); // 503 own cost + 501 own cost
            Assert.AreEqual(40, res);

            // 3 StoredProcedureRecalculateCostCompoundOneQuant2
            res = helper.Run(504); // 504 + 2*502
            Assert.AreEqual(80, res);

            // 4 StoredProcedureRecalculateCostCompoundTwo
            res = helper.Run(505); // 505 + (501 + 502)
            Assert.AreEqual(80, res);

            // 5 StoredProcedureRecalculateCostCompoundTwoQuant2
            res = helper.Run(506); // 506 + (2*501 + 2*502)
            Assert.AreEqual(120, res);

            // 6 StoredProcedureRecalculateCostCompoundLev2
            res = helper.Run(507); // 507 + (2*504 + 2*505)
            Assert.AreEqual(390, res);

            // 7    StoredProcedureRecalculateCostCompoundComplex
            // 80 (own) + 4*501 + 3*502 + 2*503 + 2*504 + 3*505 + 2*506 + 507 = 
            // 80       + 40    + 60    + 2*40  + 2*80  + 3*80  + 2*120 + 390 = 
            // 180                      + 80    + 160   + 240   + 240   + 390 =
            // 1290
            res = helper.Run(508);
            Assert.AreEqual(1290, res);

            // 8 StoredProcedureRecalculateCostNoOwnCost()
            res = helper.Run(500); // 0 + 5*502 + 507
            Assert.AreEqual(490, res);

            // 9 StoredProcedureRecalculateCostZeroPartInFirstLevel()
            // Tests the fetch loop on the subparts of the next sublevel,
            // specifically the case when one of the sublevel parts returns 0 cost
            res = helper.Run(511); // 1000 + 12 + 0 = 1012
            Assert.AreEqual(1012, res);
        }

        [TestMethod]
        public void StoredProcedureRecalculateRecurse()
        {
            RecalculateRecurseHelper helper = (RecalculateRecurseHelper)makeStoredProcedureHelperBase(StoredProcedures.RecalculateRecurse, m_connectionString);
            helper.InitTableData();

            int productscount = 0;
            decimal totalcost = 0;
            int res = helper.Run(503, 2, out productscount, out totalcost);
            Assert.AreEqual(2, productscount);
            Assert.AreEqual(60, totalcost);
        }

        [TestCleanup]
        public void CleanUp()
        {
            DropUnitTestDb();
        }

        public static void ExecuteSqlWithConnection(string sql, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
            }
        }

        public static void ExecuteSql(string sql, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                ExecuteSqlWithConnection(sql, connection);
                connection.Close();
            }
        }

        private void CreateUnitTestDbUsingMasterDbConnection()
        {
            ExecuteSql("CREATE DATABASE " + m_dbName, m_connectionStringGlobal);
        }

        private void DropUnitTestDb()
        {
            ClearConnectionPoolsAsConnectionWasOpened();
            ExecuteSql("DROP DATABASE " + m_dbName, m_connectionStringGlobal);
        }

        private void ClearConnectionPoolsAsConnectionWasOpened()
        {
            SqlConnection.ClearAllPools();
        }
 
       // To get sql code for creation, do in MSMS stored procedure file context menu: 
       // Script Table as > CREATE To.
       // You can use sql_quotes.py python script to add quotes for strings to insert into C# code
       private void CreatePartsTable()
       {
            string sql = "CREATE TABLE [dbo].[Parts](" +
                "	[Id] [int] IDENTITY(1,1) NOT NULL," +
                "	[Type] [int] NOT NULL," +
                "	[Number] [nvarchar](max) NULL," +
                "	[Description] [nvarchar](max) NULL," +
                "	[IsOwnMake] [bit] NOT NULL," +
                "	[Length] [int] NOT NULL," +
                "	[OwnCost] [decimal](25, 13) NOT NULL," +
                "	[ComponentsCost] [decimal](25, 13) NOT NULL," +
                "	[Notes] [nvarchar](max) NULL," +
                "	[Count] [int] NOT NULL DEFAULT ((0))," +
                "	[CountDate] [datetime] NULL DEFAULT ('1900-01-01T00:00:00.000')," +
                "	[OnOrder] [int] NOT NULL DEFAULT ((0))," +
                "	[Capability] [int] NOT NULL DEFAULT ((0))," +
                "	[Demand] [int] NOT NULL DEFAULT ((0))," +
                " CONSTRAINT [PK_dbo.Parts] PRIMARY KEY CLUSTERED " +
                "(" +
                "	[Id] ASC" +
                ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                ") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
            ExecuteSql(sql, m_connectionString);
       }

       private void CreateSubassembliesTable()
       {
            string sql = "CREATE TABLE [dbo].[Subassemblies](" +
                "	[Id] [int] IDENTITY(1,1) NOT NULL," +
                "	[AssemblyId] [int] NOT NULL," +
                "	[SubassemblyId] [int] NOT NULL," +
                "	[InheritedCost] [decimal](25, 13) NOT NULL," +
                "	[CostContribution] [decimal](25, 13) NOT NULL," +
                "	[Notes] [nvarchar](max) NULL," +
                "	[Capability] [int] NOT NULL DEFAULT ((0))," +
                "	[Demand] [decimal](25, 13) NOT NULL DEFAULT ((0))," +
                " CONSTRAINT [PK_dbo.Subassemblies] PRIMARY KEY CLUSTERED " +
                "(" +
                "	[Id] ASC" +
                ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" +
                ") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";

            ExecuteSql(sql, m_connectionString);
        }

        // Factory Method
        private static IStoredProcedureTest makeStoredProcedureHelperBase(StoredProcedures storedProcedure, string connectionString)
        {
            switch (storedProcedure)
            {
                case StoredProcedures.RecalculateCostsForAssembly:
                    return new RecalculateCostsForAssemblyHelper(connectionString);
                case StoredProcedures.RecalculateRecurse:
                    return new RecalculateRecurseHelper(connectionString);
                default:
                    throw new ArgumentException("Not implemented: " + storedProcedure);
            }
        }

        private void makeConnectionStrings()
        {
            // for SQLServer 2014, need to create the db first with cmd>sqllocaldb create v12.0  
            // https://connect.microsoft.com/SQLServer/feedback/details/845278/sql-server-2014-express-localdb-does-not-create-automatic-instance-v12-0
            // 
            // to debug, disable dropping of the unit test database and connect in MS Management Studio to (localdb)\V12.0.
            string dataSource = @"Data Source=(LocalDB)\mssqllocaldb;";
            m_connectionStringGlobal = dataSource + "Integrated Security=true;";
            m_connectionString = dataSource + "Database=" + m_dbName + ";Integrated Security=true;";
        }

        private const string m_dbName = "BOM_unit_test";
        private string m_connectionStringGlobal;
        private string m_connectionString;

    } // StoredProcedures
} // namespace Bom.Database.Tests
