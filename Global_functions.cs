using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace SALES
{
    class Global_functions
    {

        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        //SQLServer SQL;
        SqlCommand cmd;
        
        public Global_functions()
        {
            
        }

        public Int32 maxid (DataTable tbl , object fld_id)
        {
            int id = 0;
            con.Close();
            cmd=new SqlCommand("Select max('"+fld_id+"') as fds From 'tbl' " ,con);
            con.Open();
            cmd.ExecuteNonQuery();

            DataSet dsS = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dsS);

            DataRow drS;
            drS = dsS.Tables[0].Rows[0];
            id = Convert.ToInt32(drS["fds"].ToString());

            id++;
            con.Close();
            return id;
        }

        public string Get_CellData(DataSet Table_Function, String Fld_Name)
        {
            DataRow dT_Row;
            try
            {
                dT_Row = Table_Function.Tables[0].Rows[0];
                if (dT_Row[Fld_Name].ToString() == "")
                {
                    return Convert.ToString(0);
                }
                else
                {
                    return dT_Row[Fld_Name].ToString();
                }
            }
            catch { return Convert.ToString(0); }
        }

        public void ADD_ORDER_DETALES( string PARCODE, string PRODUCT_NAME, string UNIT,string QTY, string PRICE, string TOTAL,string ID_PRODUCT)
        {
            con.Open();
            SqlParameter[] par = new SqlParameter[7];

            //par[0] = new SqlParameter("@ID" , SqlDbType.Int);
            //par[0].Value = ID;

            par[1] = new SqlParameter("@PARCODE" , SqlDbType.VarChar, 50);
            par[1].Value = PARCODE;

            par[2] = new SqlParameter("@PRODUCT_NAME" , SqlDbType.VarChar, 50);
            par[2].Value = PRODUCT_NAME;

            par[3] = new SqlParameter("@UNIT" , SqlDbType.VarChar, 50);
            par[3].Value = UNIT;

            par[4] = new SqlParameter("@QTY" , SqlDbType.VarChar, 50);
            par[4].Value = QTY;

            par[5] = new SqlParameter("@PRICE", SqlDbType.VarChar, 50);
            par[5].Value = PRICE;

            par[6] = new SqlParameter("@TOTAL", SqlDbType.VarChar, 50);
            par[6].Value = TOTAL;

            par[7] = new SqlParameter("@ID_PRODUCT", SqlDbType.VarChar, 50);
            par[7].Value = ID_PRODUCT;

            //SQL.ExecuteCommand( "ADD_ORDER_DETALES" , par );
            con.Close();
        }

        public Int32 Comandos(String sqlString)
        {
            con.Close();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sqlString;
            con.Open();
            try
            { return cmd.ExecuteNonQuery(); }
            catch (InvalidCastException e)
            {
                throw (e);
                //connection.Close();
            }
            finally
            { con.Close(); }
        }

    }
}
