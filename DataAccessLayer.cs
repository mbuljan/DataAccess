using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataAccessLayer
    {
        private string _connectionString = string.Empty;
        private SqlConnection _sqlConnection = null;

        public DataAccessLayer(string connectionString)
        {
            _connectionString = connectionString;
        }

        public QueryResult ExecuteProcedure(string procedureName, List<SqlParameter> parameters)
        {
            QueryResult queryResult = new QueryResult();

            try
            {
                if (_sqlConnection == null)
                     _sqlConnection = new SqlConnection(_connectionString);

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    dataAdapter.SelectCommand = new SqlCommand(procedureName, _sqlConnection);
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    // adding all parameters for the procedure call
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        dataAdapter.SelectCommand.Parameters.Add(parameters[i]);
                    }

                    // procedure call and saving results
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "Result");
                    queryResult._resultTable = dataSet.Tables["Result"];
                }
            }
            catch (SqlException ex)
            {
                queryResult._errorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                queryResult._errorMessage = ex.Message;
            }
            return queryResult;
        }
    }
}
