using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SALES
{
    class Tbl_Currency
    {
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");

        public int ID_Currency
        {
            get;
            set;
        }
        public string Currency_Name
        {
            get;
            set;
        }
        public void Fill_Currency(ComboBox cbo)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            cmd = new SqlCommand("Select * from Tbl_Currency", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cbo.Items.Add(dr["Currency_Name"].ToString());
            }
            dr.Close();
            con.Close();
        }
        public void Select_Currency(ComboBox cbo , TextBox txt_Currency_Number , TextBox txt_Currency_Name)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            cmd = new SqlCommand(" Select * from Tbl_Currency where Currency_Name = N'"+cbo.Text.Trim()+"' ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())

                txt_Currency_Number.Text = dr["ID_Currency"].ToString();
                txt_Currency_Name.Text = dr["Currency_Name"].ToString();

            dr.Close();
            con.Close();
        }
        public void Delete_currency(TextBox txt_Currency_Number , Label msg_Currency)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            cmd = new SqlCommand("Delete from Tbl_Currency where ID_Currency = N'"+txt_Currency_Number.Text.Trim()+"' " , con);
            cmd.ExecuteNonQuery();
            msg_Currency.Text = "تم الحذف بنجاح";
            con.Close();
        }
        public void ADD_Currency(TextBox txt_Currency_Name)
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            string Query = "Insert Into Tbl_Currency (Currency_Name) Values (@Currency_Name) ";
            cmd = new SqlCommand(Query,con);
            
            //cmd.Parameters.AddWithValue("@ID_Currency", txt_Currency_Number);
            cmd.Parameters.AddWithValue("@Currency_Name", txt_Currency_Name.Text.Trim());

            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void Update_Currency(TextBox txt_Currency_Name)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                string Query = "Update Tbl_Currency set Currency_Name = @Currency_Name where ID_Currency = @ID_Currency";
                cmd = new SqlCommand(Query, con);

                cmd.Parameters.AddWithValue("@Currency_Name", txt_Currency_Name.Text.Trim());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception ) { }
          
        }
        public void Currency_Index_changed(ComboBox cbo_Currency , TextBox txt_Currency_Number , TextBox txt_Currency_Name)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                if (cbo_Currency.SelectedIndex > -1)
                {
                    con.Open();
                    string Query = "select ID_Currency , Currency_Name from Tbl_Currency where Currency_Name = N'" + cbo_Currency.Text.Trim() + "' ";
                    cmd = new SqlCommand(Query, con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    txt_Currency_Number.Text = dt.Rows[0]["ID_Currency"].ToString();
                    txt_Currency_Name.Text = dt.Rows[0]["Currency_Name"].ToString();

                    con.Close();
                }
            }
            catch { }
        }
    }
}
