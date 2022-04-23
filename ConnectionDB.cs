using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace SALES
{
    public class SQLServer
    {
        SqlConnection connection;

        public SQLServer(String serverName, String dataBase, String userName, String password, Boolean IntegratedSecurity)
        {
            String connectionString = " ";

            if (userName == "" || password == "")
            { connectionString = "Data Source=" + serverName + ";Initial Catalog=" + dataBase + ";Integrated Security=" + IntegratedSecurity; }
            else
            {
                connectionString = "Data Source=" + serverName + ";Initial Catalog=" + dataBase +
                                   ";User Id=" + userName + ";Password=" + password + ";"; // +"Connect Timeout=300";
            }
            connection = new SqlConnection(connectionString);

            try
            { connection.Open(); }
            catch (SqlException e)
            {
                if (e.Number == 2) // حالة لايتوفر اتصال بالخدمة
                { MessageBox.Show("مزود خدمة قاعدة البيانات لا يعمل يجب الاتصال بمدير النظام", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
                else if (e.Number == 4060) // حالة توفر اتصال ولكن القاعدة غير متوفرة
                {
                    // استرداد قاعدة البيانات من نسخة احتياطية
                    int StatRB = 0;
                    //SqlConnection RestoreDB = new SqlConnection("Data Source=localhost\\sqlexpress;Initial Catalog=master;User Id=sa; Password=1782003; Integrated Security=False");
                    SqlConnection RestoreDB = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=master; Integrated Security=True");

                    string query = string.Format( @"restore database {1} from disk='{2}{0}'",
                                                    "RealApplication",//m_backUpFilePath,
                                                    "RealApplication",//m_dbName,
                                                    @"C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\Backup");//m_datFilePath);
                    SqlCommand cmd;
                    cmd = new SqlCommand(query, RestoreDB);

                    RestoreDB.Open();
                    try
                    {
                        StatRB = cmd.ExecuteNonQuery();
                        int milliseconds = 3000;
                        System.Threading.Thread.Sleep(milliseconds);
                        if (StatRB == -1)
                        { connection.Open(); }
                        else { MessageBox.Show("لم يتمكن البرنامج من استعادة النسخة الأصلية لقاعدة البيانات يرجى الاتصال بمدير النظام", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
                    }
                    catch
                    { MessageBox.Show("لم يتمكن البرنامج من استعادة النسخة الأصلية لقاعدة البيانات يرجى الاتصال بمدير النظام", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
                }
                else // حالة اخرى 
                { MessageBox.Show("يوجد خلل بالاتصال بقاعدة البيانات يرجى الاتصال بمدير النظام", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            }
        }

        public Boolean OpenConnection()
        {
            return true;
        }

        public Boolean CloseConection()
        {
            return false;
        }

        public Int32 Comandos(String sqlString)
        {
            connection.Close();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = sqlString;
            connection.Open();
            try
            { return cmd.ExecuteNonQuery(); }
            catch (InvalidCastException e)
            {
                throw (e);
                //connection.Close();
            }
            finally
            { connection.Close(); }
        }

        public Int32 Comandos(String sqlString, SqlParameter par)
        {
            connection.Close();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.Parameters.Add(par);
            cmd.CommandText = sqlString;
            connection.Open();
            try
            { return cmd.ExecuteNonQuery(); }
            catch (InvalidCastException e)
            {
                throw (e);
                //connection.Close();
            }
            finally
            { connection.Close(); }
        }

        public Int32 Comandos(String sqlString, SqlParameter par1, SqlParameter par2)
        {
            connection.Close();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.Parameters.Add(par1);
            cmd.Parameters.Add(par2);
            cmd.CommandText = sqlString;
            connection.Open();
            try
            { return cmd.ExecuteNonQuery(); }
            catch (InvalidCastException e)
            {
                throw (e);
                //connection.Close();
            }
            finally
            { connection.Close(); }
        }

        public Int32 Comandos(String sqlString, SqlParameter par1, SqlParameter par2, SqlParameter par3)
        {
            connection.Close();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.Parameters.Add(par1);
            cmd.Parameters.Add(par2);
            cmd.Parameters.Add(par3);
            cmd.CommandText = sqlString;
            connection.Open();
            try
            { return cmd.ExecuteNonQuery(); }
            catch (InvalidCastException e)
            {
                throw (e);
                //connection.Close();
            }
            finally
            { connection.Close(); }
        }

        public void ExecuteCommand(string str, SqlParameter[] param)
        {
            SqlCommand sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandText = str;
            sqlcmd.Connection = connection;
            if (param != null)
            {
                sqlcmd.Parameters.AddRange(param);
            }
            sqlcmd.ExecuteNonQuery();
        }

        public DataSet runSQLDataSet(String sqlString)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = sqlString;
                DataSet dts = new DataSet();
                SqlDataAdapter DataAdapter = new SqlDataAdapter(cmd);
                DataAdapter.Fill(dts);
                return dts;
            }
            catch (InvalidCastException e)
            {
                throw (e);
                //connection.Close();
            }
        }

        public DataTable ExecuteQuery(String sqlString)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = sqlString;
            IDataReader ad = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(ad);
            ad.Close();
            return dataTable;
        }

        public IDataReader ExecuteQueryDataReader(String sqlString)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = sqlString;
            IDataReader ad = cmd.ExecuteReader();
            return ad;
        }
    }
}
