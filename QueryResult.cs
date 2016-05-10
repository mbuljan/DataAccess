using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class QueryResult
    {
        internal DataTable _resultTable = new DataTable();
        internal string _errorMessage = string.Empty;

        public DataTable ResultTable
        {
            get
            {
                return _resultTable;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
        }

        public QueryResult(DataTable resultTable, string errorMessage)
        {
            _resultTable = resultTable;
            _errorMessage = errorMessage;
        }

        public QueryResult() { }

        public bool HasError() { return _errorMessage != string.Empty; }
        public bool HasResult() { return _resultTable.Rows.Count > 0; }

        public object GetResult(int rowNumber, string columnName)
        {
            return ResultTable.Rows[rowNumber][columnName];
        }
    }
}
