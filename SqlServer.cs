using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SimpleADO
{
    public class SqlServer
    {
        /// <summary>
        /// This method will return Datatable for select commands or queries
        /// </summary>
        /// <param name="conStringName">Database connection string</param>
        /// <param name="query">Sql query text</param>
        /// <param name="queryparams">Query parameters</param>
        /// <returns>DataTable for the specified SQL select query</returns>
        public async Task<DataTable> SelectQueryAsync(string conStringName, string query, string[] queryparams)
        {
            string conStr = ConfigurationManager.ConnectionStrings[conStringName].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = 0;
            foreach (string param in queryparams)
            {
                cmd.Parameters.AddWithValue("@param_" + i, queryparams[i]);
                i++;
            }
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            await Task.Run(()=>da.Fill(dt));
            con.Close();
            return dt;
        }
        /// <summary>
        /// This method will return void for delete,update and insert sql commands or queries
        /// </summary>
        /// <param name="conStringName">Database connection string</param>
        /// <param name="query">Sql query text</param>
        /// <param name="queryparams">Query parameters</param>
        public void CUDQuery(string conStringName, string query, string[] queryparams)
        {

            string conStr = ConfigurationManager.ConnectionStrings[conStringName].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = 0;
            foreach (string param in queryparams)
            {
                cmd.Parameters.AddWithValue("@param_" + i, queryparams[i]);
                i++;
            }
            cmd.ExecuteNonQuery();
            con.Close();
        }
        /// <summary>
        /// ReaderQuery method if for reading data from database and put it to SqlDataReader.
        /// This method will return an opened reader, during implementation remember to close the reader and the connection
        /// </summary>
        /// <param name="conStringName">Database connection string</param>
        /// <param name="query">Sql query text</param>
        /// <param name="queryparams">Query parameters</param>
        /// <returns>SqlConnection Connection and SqlDataReader Reader (both opened)</returns>
        public ReadearReturn ReaderQuery(string conStringName, string query, string[] queryparams)
        {
            string conStr = ConfigurationManager.ConnectionStrings[conStringName].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = 0;
            foreach (string param in queryparams)
            {
                cmd.Parameters.AddWithValue("@param_" + i, queryparams[i]);
                i++;
            }
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            var readerReturn = new ReadearReturn()
            {
                Connection = con,
                Reader = rdr
            };
            return readerReturn;
        }
        /// <summary>
        /// A class for return properties of <c>ReaderQuery</c> method.
        /// </summary>
        public class ReadearReturn
        {
            public SqlConnection Connection { get; set; }
            public SqlDataReader Reader { get; set; }
        }
        /// <summary>
        /// Count entries to the database for an SQL query string with parameters
        /// </summary>
        /// <param name="conStringName">Database connection string</param>
        /// <param name="query">Sql query text</param>
        /// <param name="queryparams">Query parameters</param>
        /// <returns><c>int</c> number of data entries</returns>
        public int CountQuery(string conStringName, string query, string[] queryparams)
        {
            string conStr = ConfigurationManager.ConnectionStrings[conStringName].ConnectionString;
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = 0;
            foreach (string param in queryparams)
            {
                cmd.Parameters.AddWithValue("@param_" + i, queryparams[i]);
                i++;
            }
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            var count = Convert.ToInt32(dt.Rows.Count.ToString());
            con.Close();
            return count;
        }
    }
}
