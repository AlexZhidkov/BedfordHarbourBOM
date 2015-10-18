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

        public int Run(int partId, decimal productsDemand, out int productscount, out decimal totalcost)
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
                    //two output parameters - products count and total cost
                    SqlParameter sp_productscount = cmd.Parameters.Add(new SqlParameter("@ProductsCount", SqlDbType.Int));
                    cmd.Parameters["@ProductsCount"].Direction = ParameterDirection.Output;
                    SqlParameter sp_totalcost = cmd.Parameters.Add(new SqlParameter("@TotalCost", SqlDbType.Decimal));
                    cmd.Parameters["@TotalCost"].Direction = ParameterDirection.Output;
                    result = cmd.ExecuteNonQuery();
                    productscount = Convert.ToInt32(sp_productscount.Value);
                    totalcost = Convert.ToDecimal(sp_totalcost.Value);
                }
                connection.Close();
            }
            return result;
        }
    } // RecalculateRecurseHelper
} // namespace Bom.Database.Tests