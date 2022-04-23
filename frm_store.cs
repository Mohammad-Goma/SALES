using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SALES
{
    public partial class frm_store : Form
    {
        ResizeTools rs = new ResizeTools();
        Boolean Test_Size = false;
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd ;
        //TBL_SOTRE tblstore;
        public frm_store()
        {
            InitializeComponent();
        }

        public void ClearAllTextBox()
        {
          foreach(Control ctrl in this.Controls)
            {
                if(ctrl is TextBox)
                {
                    ctrl.Text =string.Empty;
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void fill_stores_Data(ComboBox cbo)
        {
            con.Close();
            cbo.Items.Clear();
            con.Open();
            cmd = new SqlCommand("Select * From TBL_STORES", con);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            D_Grid_view_store.DataSource = dt;
            SqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                cbo.Items.Add(dtr["STORE_NAME"].ToString());
            }
            dtr.Close();
            con.Close();
        }

        private void Frm_store_Load(object sender, EventArgs e)
        {
           fill_stores_Data(cbo_store);
            rs.Container = grpbox_store;
            rs.FullRatioTable();
            AutoCompleteTextBox();
            //Search_Store_Name();
        }

        private void Frm_store_Resize(object sender, EventArgs e)
        {
            if (Test_Size)
            { rs.ResizeControls(); }
        }

        private void Frm_store_Shown(object sender, EventArgs e)
        {
            Test_Size = true;
            this.WindowState = FormWindowState.Normal; 
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select store_open,store_save,store_delete,store_edite,store_new From USERS_PERMESSIONS Where USER_NAME = N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd22 = new SqlCommand(Query, con);
                SqlDataReader dr2 = cmd22.ExecuteReader();
                if (dr2.Read())
                {
                    if (dr2["store_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr2["store_save"].ToString() == "True") { btnAdd.Enabled = true; } else { btnAdd.Enabled = false; }
                    if (dr2["store_delete"].ToString() == "True") { btnDelete.Enabled = true; } else { btnDelete.Enabled = false; }
                    if (dr2["store_new"].ToString() == "True") { btnNew.Enabled = true; } else { btnNew.Enabled = false; }
                    if (dr2["store_edite"].ToString() == "True") { btnedit_store.Enabled = true; } else { btnedit_store.Enabled = false; }
                }
                dr2.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        private void BtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public void AutoCompleteTextBox()
        {
            con.Open();
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            string Query = "Select * from TBL_STORES ";
            cmd = new SqlCommand(Query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                col.Add(dr.GetString(1));
            }
            txt_search_ingrid.AutoCompleteCustomSource = col;
            dr.Close();
            con.Close();
        }

        private void Txt_search_ingrid_TextChanged(object sender, EventArgs e)
        {
            Search_Store_Name();
        }

        public void Search_Store_Name()
        {
            try
            {
                con.Open();
                string Query = "Select * from TBL_STORES Where STORE_NAME like N'%" + txt_search_ingrid.Text.Trim() + "%' ";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                D_Grid_view_store.DataSource = dt;
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        private void Cbo_store_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_store.SelectedIndex > -1)
            {
                con.Open();
                cmd = new SqlCommand(" SELECT * FROM TBL_STORES WHERE STORE_NAME like N'"+cbo_store.Text.Trim()+"' ", con);
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                //SqlDataReader dr = cmd.ExecuteReader();
                //while (dr.Read())
                //{
                    txt_store_number.Text = dt.Rows[0]["ID_STORE"].ToString();
                    txt_store_name.Text = dt.Rows[0]["STORE_NAME"].ToString();
                    txt_store_adress.Text = dt.Rows[0]["STORE_ADRESS"].ToString();
                    txt_store_mobile_1.Text = dt.Rows[0]["STORE_MOILE_1"].ToString();
                    txt_store_mobile_2.Text = dt.Rows[0]["STORE_MOBILE_2"].ToString();
                    txt_store_manger.Text = dt.Rows[0]["STORE_MANGER"].ToString();
                    txt_branch_name.Text = dt.Rows[0]["BRANCH_Name"].ToString();
                    txt_barnch_id.Text = dt.Rows[0]["ID_BRANCH"].ToString();
                    txt_store_Details.Text = dt.Rows[0]["STORE_DETAILS"].ToString();
                //}
                con.Close();
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            //ClearAllTextBox();
            Clear();
        }

        private void Clear()
        {
            txt_store_number.Text = string.Empty;               //
            txt_store_name.Text = string.Empty;                 //
            txt_store_adress.Text = string.Empty;               //
            txt_store_mobile_1.Text = string.Empty;             //
            txt_store_mobile_2.Text = string.Empty;             //
            txt_store_manger.Text = string.Empty;               //
            txt_branch_name.Text = string.Empty;                     //
            txt_barnch_id.Text = string.Empty;                  //
            txt_store_Details.Text = string.Empty;              //
            cbo_store.Text = string.Empty;                      //
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try {
                if (txt_store_name.Text == "" && txt_store_adress.Text == "")
                {
                    MessageBox.Show(" من فضلك ادخل البيانات  ");
                }
                else if (txt_store_number.Text == "" && txt_store_name.Text != "")
                {
                    int id = 0;
                    cmd.Connection = con;
                    cmd.CommandText = "Select max(ID_store) as fds From TBL_STORES";
                    con.Open();
                    cmd.ExecuteNonQuery();
                    DataSet dsS = new DataSet();
                    SqlDataAdapter DA = new SqlDataAdapter(cmd);
                    DA.Fill(dsS);
                    DataRow drS;
                    drS = dsS.Tables[0].Rows[0];
                    id = Convert.ToInt32(drS["fds"].ToString());
                    id++;
                    cmd = new SqlCommand("INSERT INTO TBL_STORES VALUES (N'"+id+"',N'"+ txt_store_name.Text.Trim() + "'," +
                                            "N'" + txt_store_adress.Text.Trim() + "',N'" + txt_store_mobile_1.Text.Trim() + "'," +
                                            "N'" + txt_store_mobile_2.Text.Trim() + "',N'" + txt_store_manger.Text.Trim() + "'," +
                                            "N'" + txt_branch_name.Text.Trim() + "',N'" + txt_barnch_id.Text.Trim() + "'," +
                                            "N'" + txt_store_Details.Text.Trim() + "')");
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.AddWithValue("@Id_store", id);
                    cmd.Parameters.AddWithValue("@store_name", txt_store_name.Text.Trim());
                    cmd.Parameters.AddWithValue("@store_adress", txt_store_adress.Text.Trim());
                    cmd.Parameters.AddWithValue("@store_mobile_1", txt_store_mobile_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@store_mobile_2", txt_store_mobile_2.Text.Trim());
                    cmd.Parameters.AddWithValue("@store_manger", txt_store_manger.Text.Trim());  
                    cmd.Parameters.AddWithValue("@BRANCH_Name", txt_branch_name.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_BRANCH", txt_barnch_id.Text.Trim());
                    cmd.Parameters.AddWithValue("@store_details", txt_store_Details.Text.Trim());
                    lbl_store.Text = "تم الحفظ بنجاح ";
                    con.Close();
                    Clear();
                    fill_stores_Data(cbo_store);
                }
                else if (txt_store_number.Text != "")
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE TBL_STORES set store_name = N'" + txt_store_name.Text.Trim() + "'," +
                        "store_adress= N'" + txt_store_adress.Text.Trim() + "', STORE_MOILE_1 = N'" + txt_store_mobile_1.Text.Trim() + "'," +
                        " STORE_MOBILE_2 = N'" + txt_store_mobile_2.Text.Trim() + "',STORE_MANGER= N'" + txt_store_manger.Text.Trim() + "'," +
                        "BRANCH_Name= N'" + txt_branch_name.Text.Trim() + "',ID_BRANCH= N'" + txt_barnch_id.Text.Trim() + "',store_details= N'" + txt_store_Details.Text.Trim() + "' where ID_STORE =N'" + txt_store_number.Text.Trim() + "' ", con);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.AddWithValue("@store_name", txt_store_name.Text.Trim());
                    cmd.Parameters.AddWithValue("@store_adress", txt_store_adress.Text.Trim());
                    cmd.Parameters.AddWithValue("@store_mobile_1", txt_store_mobile_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@store_mobile_2", txt_store_mobile_2.Text.Trim());
                    cmd.Parameters.AddWithValue("@store_manger", txt_store_manger.Text.Trim());
                    cmd.Parameters.AddWithValue("@STORE_MANGER", txt_store_manger.Text.Trim());
                    cmd.Parameters.AddWithValue("@BRANCH_Name", txt_branch_name.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_BRANCH", txt_barnch_id.Text.Trim());
                    cmd.Parameters.AddWithValue("@store_details", txt_store_Details.Text.Trim());
                    lbl_store.Text = "تم التعديل بنجاح ";
                    con.Close();
                    Clear();
                    fill_stores_Data(cbo_store);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
            //cmd.Parameters.Add("@store_name", SqlDbType.NVarChar, 50).Value = txt_store_name.Text.Trim();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (txt_store_number.Text != "")
            {
                con.Open();
                cmd = new SqlCommand("DELETE FROM dr WHERE ID_STORE = '" + txt_store_number.Text.Trim() + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                lbl_store.Text = "تم الحذف بنجاح";
                Clear();
                fill_stores_Data(cbo_store);
            }
            else { MessageBox.Show("من فضلك أختر أسم المخزن المراد حذفه"); }

        }

        private void Btnedit_store_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("UPDATE dr set store_name = N'" + txt_store_name.Text.Trim() + "'," +
                "store_adress= N'" + txt_store_adress.Text.Trim() + "', STORE_MOILE_1 = N'" + txt_store_mobile_1.Text.Trim() + "'," +
                " STORE_MOBILE_2 = N'" + txt_store_mobile_2.Text.Trim() + "',STORE_MANGER= N'" + txt_store_manger.Text.Trim() + "'," +
                "BRANCH_Name= N'" + txt_branch_name.Text.Trim() + "',ID_BRANCH= N'" + txt_barnch_id.Text.Trim() + "',store_details= N'" + txt_store_Details.Text.Trim() + "' where ID_STORE =N'" + txt_store_number.Text.Trim() + "' ", con);
            cmd.ExecuteNonQuery();
            cmd.Parameters.AddWithValue("@store_name", txt_store_name.Text.Trim());
            cmd.Parameters.AddWithValue("@store_adress", txt_store_adress.Text.Trim());
            cmd.Parameters.AddWithValue("@store_mobile_1", txt_store_mobile_1.Text.Trim());
            cmd.Parameters.AddWithValue("@store_mobile_2", txt_store_mobile_2.Text.Trim());
            cmd.Parameters.AddWithValue("@store_manger", txt_store_manger.Text.Trim());
            cmd.Parameters.AddWithValue("@STORE_MANGER", txt_store_manger.Text.Trim());
            cmd.Parameters.AddWithValue("@BRANCH_Name", txt_branch_name.Text.Trim());
            cmd.Parameters.AddWithValue("@ID_BRANCH", txt_barnch_id.Text.Trim());
            cmd.Parameters.AddWithValue("@store_details", txt_store_Details.Text.Trim());
            lbl_store.Text = "تم التعديل بنجاح ";
            con.Close();
            Clear();
            fill_stores_Data(cbo_store);
        }

    }
}
