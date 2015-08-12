using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Bom.Database.Tests
{
    [TestClass]
    public class StoredProcedures
    {
        public StoredProcedures()
        {
            makeConnectionStrings();
        }

        [TestInitialize]
        public void Initialize()
        {
            // connect to existing master DB and create a unit test database
            CreateUnitTestDB();

            // initialise created unit test db
            // create required tables and stored procedure
            CreateUnitTestDBStructure();
        }

        // use only one [TestMethod] for each SP in order to not to create/drop database for each test
        [TestMethod]
        public void SPRecalculateCostsForAssembly()
        {
            ISPTest spHelper = makeSPHelper("RecalculateCostsForAssembly", m_connectionString);
            spHelper.InitData();

            // 1 SPRecalculateCostSimple
            int res = spHelper.Run(501); // just own cost, no compound
            Assert.AreEqual(10, res);

            // 2 SPRecalculateCostCompoundOne
            res = spHelper.Run(503); // 503 own cost + 501 own cost
            Assert.AreEqual(40, res);

            // 3 SPRecalculateCostCompoundOneQuant2
            res = spHelper.Run(504); // 504 + 2*502
            Assert.AreEqual(80, res);

            // 4 SPRecalculateCostCompoundTwo
            res = spHelper.Run(505); // 505 + (501 + 502)
            Assert.AreEqual(80, res);

            // 5 SPRecalculateCostCompoundTwoQuant2
            res = spHelper.Run(506); // 506 + (2*501 + 2*502)
            Assert.AreEqual(120, res);

            // 6 SPRecalculateCostCompoundLev2
            res = spHelper.Run(507); // 507 + (2*504 + 2*505)
            Assert.AreEqual(390, res);

            // 7    SPRecalculateCostCompoundComplex
            // 80 (own) + 4*501 + 3*502 + 2*503 + 2*504 + 3*505 + 2*506 + 507 = 
            // 80       + 40    + 60    + 2*40  + 2*80  + 3*80  + 2*120 + 390 = 
            // 180                      + 80    + 160   + 240   + 240   + 390 =
            // 1290
            res = spHelper.Run(508);
            Assert.AreEqual(1290, res);

            // 8 SPRecalculateCostNoOwnCost()
            res = spHelper.Run(500); // 0 + 5*502 + 507
            Assert.AreEqual(490, res);

            // 9 SPRecalculateCostZeroPartInFirstLevel()
            // Tests the fetch loop on the subparts of the next sublevel,
            // specifically the case when one of the sublevel parts returns 0 cost
            res = spHelper.Run(511); // 1000 + 12 + 0 = 1012
            Assert.AreEqual(1012, res);
        }

        [TestMethod]
        public void SPRecalculateRecurse()
        {
            ISPTest spHelper = makeSPHelper("RecalculateRecurse", m_connectionString);
            spHelper.InitData();

            int res = spHelper.Run(503, 2);
            Assert.AreEqual(2, res);
        }

        [TestCleanup]
        public void CleanUp()
        {
            DropUnitTestDB();
        }

        // service methods: initialise and clean up helpers
        public static void ExecuteSqlWithConnection(string sql, SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
        }

        public static void ExecuteSql(string sql, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                ExecuteSqlWithConnection(sql, conn);
                conn.Close();
            }
        }

        private void CreateUnitTestDB()
        {
            // create a database similar to the SQL Server Model database in the default location,
            // using default connection to master db
            ExecuteSql("CREATE DATABASE " + m_dbName, m_connectionStringGlobal);
        }

        private void DropUnitTestDB()
        {
            // required, as we opened a connection to the unit_test db at least once
            SqlConnection.ClearAllPools();
            // drop the unit test db
            ExecuteSql("DROP DATABASE " + m_dbName, m_connectionStringGlobal);
        }

        private void CreateUnitTestDBStructure()
        {
            // 1. Create required tables - sql taken from MSMS, Script Table as > CREATE To
            // You can use sql_quotes.py python script to add quotes to insert into C# code

            // 1.1 Parts table
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

            // 1.2 Subassemblies table
            sql = "CREATE TABLE [dbo].[Subassemblies](" +
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

        // Factory Method design pattern
        private static ISPTest makeSPHelper(string spName, string connectionString)
        {
            switch (spName)
            {
                case "RecalculateCostsForAssembly":
                    return new RecalculateCostsForAssemblyHelper(connectionString);
                case "RecalculateRecurse":
                    return new RecalculateRecurseHelper(connectionString);
                default:
                    throw new ArgumentException("Unknown type: " + spName);
            }
        }

        private void makeConnectionStrings()
        {
            // for SQLServer 2014, need to create the db first with cmd>sqllocaldb create v12.0  
            // https://connect.microsoft.com/SQLServer/feedback/details/845278/sql-server-2014-express-localdb-does-not-create-automatic-instance-v12-0
            // 
            // to debug, disable dropping of the unit test database and connect in MS Management Studio to (localdb)\V12.0.
            string dataSource = @"Data Source=(localdb)\v12.0;";
            m_connectionStringGlobal = dataSource + "Integrated Security=true;";
            m_connectionString = dataSource + "Database=" + m_dbName + ";Integrated Security=true;";
        }

        // Data
        private const string m_dbName = "BOM_unit_test";
        private string m_connectionStringGlobal;
        private string m_connectionString;

    } // StoredProcedures

    // SP test method helpers. 
    //
    // Each SP is supported by a class with ISPTest interface with methods InitData(), Run().
    // InitData() should initialise the test data in the tables, 
    // Run() method invokes the stored procedure.
    // Base class SPHelper creates the SP in the database.
    // Design principles: separation of concerns, polymorphism

    interface ISPTest
    {
        void InitData();
        // allows to write in test Run(501); which is clearer than use some interface IParameters
        int Run(int param1, decimal param2 = 0);
    }

    public class SPHelper
    {
        public SPHelper(string connectionString, string spPath) 
        {
            this.m_connectionString = connectionString;
            this.m_spPath = spPath;

            // create stored procedure in the database
            CreateStoredProcedure();
        }

        private void CreateStoredProcedure()
        {
            // create required stored procedure from predefined place on the repo
            string spCreateCode = System.IO.File.ReadAllText(m_spPath);
            StoredProcedures.ExecuteSql(spCreateCode, m_connectionString);
        }

        protected string m_connectionString;
        private string m_spPath;
    }

    public class RecalculateCostsForAssemblyHelper : SPHelper, ISPTest
    {
        public RecalculateCostsForAssemblyHelper(string connectionString) : 
            base(connectionString, @"..\..\..\Bom.Data\SQL\StoredProcedures\RecalculateCostsForAssembly.sql")
        {
        }

        // SP RecalculateCostsForAssembly helpers
        public void InitData()
        {
            // fill in test data
            using (SqlConnection conn = new SqlConnection(m_connectionString))
            {
                // allow to set Id manually
                // should work only for current open connection, so execute all inserts for one open connection
                conn.Open();
                StoredProcedures.ExecuteSqlWithConnection("SET IDENTITY_INSERT Parts ON", conn);

                string sql = @"INSERT INTO Parts(Id, Type, IsOwnMake, Length, OwnCost, ComponentsCost) VALUES                          (500, 0, 0, 0,    0, 0),
                               (501, 0, 0, 0,     10, 0),
                               (502, 0, 0, 0,     20, 0),
                               (503, 0, 0, 0,     30, 0),
                               (504, 0, 0, 0,     40, 0),
                               (505, 0, 0, 0,     50, 0),
                               (506, 0, 0, 0,     60, 0),
                               (507, 0, 0, 0,     70, 0),
                               (508, 0, 0, 0,     80, 0),   

                               (511, 0, 0, 0,   1000, 0),
                               (512, 0, 0, 0,     12, 0),
                               (513, 0, 0, 0,      0, 0)";
                StoredProcedures.ExecuteSqlWithConnection(sql, conn);

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
                               (511, 513, 0, 1)";

                StoredProcedures.ExecuteSqlWithConnection(sql, conn);
                conn.Close();
            }
        }

        public int Run(int partId, decimal param2 = 0)
        {
            int result = -1;
            using (SqlConnection conn = new SqlConnection(m_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("RecalculateCostsForAssembly", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@partId", partId));
                    decimal tc = 0;
                    cmd.Parameters.Add(new SqlParameter("@TotalCost", tc));
                    SqlParameter retval = cmd.Parameters.Add("@TotalCost", SqlDbType.Decimal);
                    retval.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(retval.Value);
                }
                conn.Close();
            }
            return result;
        }
    } // RecalculateCostsForAssemblyHelper

    public class RecalculateRecurseHelper : SPHelper, ISPTest
    {
        public RecalculateRecurseHelper(string connectionString)
            : base(connectionString, @"..\..\..\Bom.Data\SQL\StoredProcedures\RecalculateRecurse.sql")
        {
        }

        public void InitData()
        {
            // fill in test data
            using (SqlConnection conn = new SqlConnection(m_connectionString))
            {
                // allow to set Id manually
                // should work only for current open connection, so execute all inserts for one open connection
                conn.Open();
                StoredProcedures.ExecuteSqlWithConnection("SET IDENTITY_INSERT Parts ON", conn);

                string sql = @"INSERT INTO Parts(Id, Type, IsOwnMake, Length, OwnCost, ComponentsCost, Count) VALUES
                               (501, 0, 0, 0,     10, 0, 1),
                               (502, 0, 0, 0,     20, 0, 2),
                               (503, 0, 0, 0,     30, 0, 1)";

                StoredProcedures.ExecuteSqlWithConnection(sql, conn);

                sql = @"INSERT INTO Subassemblies(AssemblyId, SubassemblyId, InheritedCost, CostContribution) VALUES
                               (503, 501, 0, 1),
                               (503, 502, 0, 1)";

                StoredProcedures.ExecuteSqlWithConnection(sql, conn);
                conn.Close();
            }
        }

        public int Run(int partId, decimal productsDemand)
        {
            int result = -1;
            using (SqlConnection conn = new SqlConnection(m_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("RecalculateRecurse", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@partId", partId));
                    cmd.Parameters.Add(new SqlParameter("@ProductsDemand", productsDemand));
                    int productsCount = 0;
                    cmd.Parameters.Add(new SqlParameter("@ProductsCount", productsCount));
                    SqlParameter retval = cmd.Parameters.Add("@ProductsCount", SqlDbType.Int);
                    retval.Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    result = Convert.ToInt32(retval.Value);
                }
                conn.Close();
            }
            return result;
        }

    } // RecalculateRecurseHelper

} // namespace Bom.Database.Tests
