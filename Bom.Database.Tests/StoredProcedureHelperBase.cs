using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Bom.Database.Tests
{
    // Creates stored procedure in the database, initialises data tables
    public class StoredProcedureHelperBase : IStoredProcedureTest
    {
        public StoredProcedureHelperBase(string connectionString, string storedProcedurePath)
        {
            this.m_connectionString = connectionString;
            this.m_storedProcedurePath = storedProcedurePath;
            this.m_sqlInitTableData = new List<string>();

            CreateStoredProcedureInDatabaseFromPredefinedPlaceOnTheRepo();
        }

        public void InitTableData()
        {
            using (SqlConnection connection = new SqlConnection(m_connectionString))
            {
                // SET IDENTITY_INSERT acts only for current open connection, 
                // so execute all inserts with one open connection
                connection.Open();
                StoredProceduresTests.ExecuteSqlWithConnection("SET IDENTITY_INSERT Parts ON", connection);
                foreach (string sql in m_sqlInitTableData)
                {
                    StoredProceduresTests.ExecuteSqlWithConnection(sql, connection);
                }
                connection.Close();
            }
        }

        private void CreateStoredProcedureInDatabaseFromPredefinedPlaceOnTheRepo()
        {
            string storedProcedureCreateSql = System.IO.File.ReadAllText(m_storedProcedurePath);
            StoredProceduresTests.ExecuteSql(storedProcedureCreateSql, m_connectionString);
        }

        protected const string m_storedProceduresPathFromRoot = @"Bom.Data\SQL\StoredProcedures\";
        protected string m_connectionString;
        protected List<string> m_sqlInitTableData;
        private string m_storedProcedurePath;

    } // StoredProcedureHelperBase
} // namespace Bom.Database.Tests