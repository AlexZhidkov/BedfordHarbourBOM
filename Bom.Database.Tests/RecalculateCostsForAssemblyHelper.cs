using System;
using System.Data;
using System.Data.SqlClient;

namespace Bom.Database.Tests
{
    public class RecalculateCostsForAssemblyHelper : StoredProcedureHelperBase
    {
        public RecalculateCostsForAssemblyHelper(string connectionString) :
            base(connectionString, @"..\..\..\" + m_storedProceduresPathFromRoot + "RecalculateCostsForAssembly.sql")
        {
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
            m_sqlInitTableData.Add(sql);

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
            m_sqlInitTableData.Add(sql);
        }

        public int Run(int partId)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(m_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("RecalculateCostsForAssembly", connection))
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
                connection.Close();
            }
            return result;
        }
    } // RecalculateCostsForAssemblyHelper
} // namespace Bom.Database.Tests