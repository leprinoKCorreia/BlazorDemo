using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using System.ComponentModel;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace BlazorBPAR.Services
{
    public class SQLQueryService
    {

        private readonly string _connectionString;

        public SQLQueryService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("currentDB");
        }

        public IList<Dictionary<string,object>> RunQuery(string query, string connection)
        {
            DataSet dsrpt = new DataSet();
            using (SqlConnection LITConnnect = new SqlConnection(connection))
            {
                LITConnnect.Open();
                SqlCommand sqlComm = new SqlCommand(query, LITConnnect);
                sqlComm.CommandType = CommandType.Text;
                sqlComm.CommandTimeout = 0;
                //sqlComm.ExecuteNonQuery();
                SqlDataAdapter daa = new SqlDataAdapter();
                daa.SelectCommand = sqlComm;
                daa.Fill(dsrpt, "DataSetFromCustomQuery");
                LITConnnect.Close();
                LITConnnect.Dispose();
            }

            return ConvertDataSetToList(dsrpt);

        }


        public IList<Dictionary<string,object>> ConvertDataSetToList(DataSet data)
        {
            IList<Dictionary<string,object>> list = new List<Dictionary<string,object>>(); 

            foreach(DataTable table in data.Tables)
            {
                foreach(DataRow row in table.Rows)
                {
                    var dict = new Dictionary<string, object>();
                    foreach(DataColumn column in table.Columns)
                    {
                        dict[column.ColumnName] = row[column];
                    }
                    list.Add(dict);
                }
            }
            return list;
        }
    }
}
