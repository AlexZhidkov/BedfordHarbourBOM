using System;
using System.Data;
using System.Data.SqlClient;

namespace Bom.Database.Tests
{
    public class RecalculateRecurseHelper : StoredProcedureHelperBase
    {
        public RecalculateRecurseHelper(string connectionString)
            : base(connectionString, @"..\..\..\" + m_storedProceduresPathFromRoot + "RecalculateRecurse.sql")
        {
            string sql = @"INSERT INTO Parts(Id, Type, IsOwnMake, Length, OwnCost, ComponentsCost, Count) VALUES
                               (501, 0, 0, 0,     10, 0, 1),
                               (502, 0, 0, 0,     20, 0, 2),
                               (503, 0, 0, 0,     30, 0, 1)";
            m_sqlInitTableData.Add(sql);

            sql = @"INSERT INTO Subassemblies(AssemblyId, SubassemblyId, InheritedCost, CostContribution) VALUES
                               (503, 501, 0, 1),
                               (503, 502, 0, 1)";
            m_sqlInitTableData.Add(sql);
        }

        public int Run(int partId, decimal productsDemand)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(m_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("RecalculateRecurse", connection))
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
                connection.Close();
            }
            return result;
        }
    } // RecalculateRecurseHelper
} // namespace Bom.Database.Tests