using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SALES
{
    public partial class frm_product_sections : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd ;
        //Global_functions globfun;
        public frm_product_sections()
        {
            InitializeComponent();
        }

        public void fill_Categories(ComboBox cbo)
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM TBL_CATEGORIES ",con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cbo.Items.Add(dr["CATEGORY_NAME"].ToString());
            }
            dr.Close();
            con.Close();
        }
        private void Cbocat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbocat.SelectedIndex > -1)
            {
                con.Open();
                cmd = new SqlCommand("SELECT * FROM TBL_CATEGORIES where CATEGORY_NAME = N'"+cbocat.Text.Trim()+"' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txtcatNumber.Text = dr[0].ToString();
                    txtcatName.Text = dr["CATEGORY_NAME"].ToString();
                    branch_name.Text= dr["BRANCH_NAME"].ToString(); 
                    store_name.Text = dr["STORE_NAME"].ToString();
                    branch_id.Text= dr["ID_BRANCH"].ToString();
                    store_id.Text = dr["ID_STORE"].ToString();
                    txtcatDetails.Text = dr["CATEGORY_DETAILS"].ToString();
                }
                dr.Close();
                con.Close();
            }
        }

        private void Product_sections_Load(object sender, EventArgs e)
        {
            fill_Categories(cbocat);
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Btn_refresh_Click(object sender, EventArgs e)
        {
            cleare();
        }
        private void Btncat_delete_Click(object sender, EventArgs e)
        {
            if (txtcatNumber.Text == "")
            {
                lblmsg.Text = "من فضلك اختر التصنيف المراد حذفه";
            }
            else if (txtcatNumber.Text != "" || txtcatNumber.Text != null)
            {
                con.Open();
                cmd = new SqlCommand("Delete FROM TBL_CATEGORIES where ID_CATEGORY = N'" + txtcatNumber.Text.Trim() + "' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("تم الحذف بنجاح");
                con.Close();
                cleare();
            }
        }
        private void Btncatnew_Click(object sender, EventArgs e)
        {
            cleare();
        }
        public void cleare()
        {
            cbocat.Items.Clear();
            cbocat.Text = "";
            txtcatName.Text = "";
            txtcatNumber.Text = "";
            branch_name.Text = "";
            store_name.Text = "";
            branch_id.Text = "";
            store_id.Text = "";
            txtcatDetails.Text = "";
            fill_Categories(cbocat);
        }
        private void Btncat_add_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                con.Close();
                cmd.Connection = con;
                cmd.CommandText = "Select max(ID_CATEGORY) as fds From TBL_CATEGORIES";
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
                if (txtcatNumber.Text == "" && txtcatName.Text != "")
                {
                    con.Open();       
                    cmd = new SqlCommand("insert into TBL_CATEGORIES values(N'" + id + "',N'" + txtcatName.Text.Trim() + "'," +
                        "N'" + branch_name.Text.Trim() + "',N'" + store_name.Text.Trim() + "'," +
                        "N'" + branch_id.Text.Trim() + "',N'" + store_id.Text.Trim() + "'," +
                        "N'" + txtcatDetails.Text.Trim() + "')", con);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.AddWithValue("@ID_CATEGORY", id).ToString();
                    cmd.Parameters.AddWithValue("@CATEGORY_NAME", txtcatName.Text.Trim()).ToString();
                    cmd.Parameters.AddWithValue("@BRANCH_NAME", branch_name.Text.Trim()).ToString();
                    cmd.Parameters.AddWithValue("@STORE_NAME", store_name.Text.Trim()).ToString();
                    cmd.Parameters.AddWithValue("@ID_BRANCH", branch_id.Text.Trim()).ToString();
                    cmd.Parameters.AddWithValue("@ID_STORE", store_id.Text.Trim()).ToString();
                    cmd.Parameters.AddWithValue("@CATEGORY_DETAILS", txtcatDetails.Text.Trim()).ToString();
                    con.Close();
                    lblmsg.Text = "تم الحفظ بنجاح";
                    cleare();
                }
                else if (txtcatNumber.Text != "")
                {
                    con.Open();
                    cmd = new SqlCommand("Update TBL_CATEGORIES set ID_CATEGORY=N'" + txtcatNumber.Text.Trim() + "'," +
                        " CATEGORY_NAME = N'" + txtcatName.Text.Trim() + "',BRANCH_NAME = N'" + branch_name.Text.Trim() + "'," +
                        " STORE_NAME = N'" + store_name.Text.Trim() + "',ID_BRANCH = N'" + branch_id.Text.Trim() + "'," +
                        "ID_STORE = N'" + store_id.Text.Trim() + "',CATEGORY_DETAILS = N'" + txtcatDetails.Text.Trim() + "'   where ID_CATEGORY = N'" + txtcatNumber.Text.Trim() + "' ", con);
                    cmd.ExecuteNonQuery();
                    //cmd.Parameters.AddWithValue("@ID_CATEGORY", txtcatNumber.Text.Trim()).ToString();
                    cmd.Parameters.AddWithValue("@CATEGORY_NAME", txtcatName.Text.Trim()).ToString();
                    cmd.Parameters.AddWithValue("@BRANCH_NAME", branch_name.Text.Trim()).ToString();
                    cmd.Parameters.AddWithValue("@STORE_NAME", store_name.Text.Trim()).ToString();
                    cmd.Parameters.AddWithValue("@ID_BRANCH", branch_id.Text.Trim()).ToString();
                    cmd.Parameters.AddWithValue("@ID_STORE", store_id.Text.Trim()).ToString();
                    cmd.Parameters.AddWithValue("@CATEGORY_DETAILS", txtcatDetails.Text.Trim()).ToString();
                    con.Close();
                    lblmsg.Text = "تم التعديل بنجاح";
                    cleare();
                }
            } catch (Exception){ throw;}
        }
        private void Btncat_edit_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("Update TBL_CATEGORIES set ID_CATEGORY=N'" + txtcatNumber.Text.Trim() + "'," +
                        " CATEGORY_NAME = N'" + txtcatName.Text.Trim() + "',BRANCH_NAME = N'" + branch_name.Text.Trim() + "'," +
                        " STORE_NAME = N'" + store_name.Text.Trim() + "',ID_BRANCH = N'" + branch_id.Text.Trim() + "'," +
                        "ID_STORE = N'" + store_id.Text.Trim() + "',CATEGORY_DETAILS = N'" + txtcatDetails.Text.Trim() + "'   where ID_CATEGORY = N'" + txtcatNumber.Text.Trim() + "' ", con);
            cmd.ExecuteNonQuery();
            //cmd.Parameters.AddWithValue("@ID_CATEGORY", txtcatNumber.Text.Trim()).ToString();
            cmd.Parameters.AddWithValue("@CATEGORY_NAME", txtcatName.Text.Trim()).ToString();
            cmd.Parameters.AddWithValue("@BRANCH_NAME", branch_name.Text.Trim()).ToString();
            cmd.Parameters.AddWithValue("@STORE_NAME", store_name.Text.Trim()).ToString();
            cmd.Parameters.AddWithValue("@ID_BRANCH", branch_id.Text.Trim()).ToString();
            cmd.Parameters.AddWithValue("@ID_STORE", store_id.Text.Trim()).ToString();
            cmd.Parameters.AddWithValue("@CATEGORY_DETAILS", txtcatDetails.Text.Trim()).ToString();
            con.Close();

            lblmsg.Text = "تم التعديل بنجاح";
            cleare();
        }

        private void frm_product_sections_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select open_sections,sections_save,sections_delete,sections_edite,sections_new From USERS_PERMESSIONS Where USER_NAME=N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader dr7 = cmd.ExecuteReader();
                if (dr7.Read())
                {
                    if (dr7["open_sections"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr7["sections_save"].ToString() == "True") { } else { btncat_add.Enabled = false; }
                    if (dr7["sections_delete"].ToString() == "True") { } else { btncat_delete.Enabled = false; }
                    if (dr7["sections_edite"].ToString() == "True") { } else { btncat_edit.Enabled = false; }
                    if (dr7["sections_new"].ToString() == "True") { } else { btncatnew.Enabled = false; }
                }
                dr7.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

    }
}
