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
    public partial class frm_units : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd;

        public frm_units()
        {
            InitializeComponent();
        }
        public void Fill_Units(ComboBox cbo)
        {
            try
            {
                cbo_units.Items.Clear();
                ClearAll();
                con.Open();
                cmd = new SqlCommand("select * from tbl_unit", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cbo.Items.Add(dr["UNIT_NAME"].ToString());
                }
                dr.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            
        }
        public void ClearAll()
        {
            txt_id_units.Text = "";
            txt_name_units.Text = "";
            txt_details_units.Text = "";
        }

        private void btn_new_units_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btn_delete_units_Click(object sender, EventArgs e)
        {
            if(txt_id_units.Text.Trim()!="" && txt_name_units.Text.Trim()!="")
            {
                con.Open();
                cmd = new SqlCommand("Delete from tbl_unit where ID_UNIT = N'"+txt_id_units.Text.Trim()+"' ",con);
                cmd.ExecuteNonQuery();

                lbl_msg_units.Text = " تم الحذف بنجاح " ;
                con.Close();
                Fill_Units(cbo_units);
            }
            else if (txt_id_units.Text.Trim() == "")
            {
                lbl_msg_units.Text = " يرجى اختيار الصنف المراد حذفه ";
            }
        }

        private void frm_units_Load(object sender, EventArgs e)
        {
            Fill_Units(cbo_units);
        }

        private void cbo_units_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbo_units.SelectedIndex > -1)
            {
                con.Open();
                cmd = new SqlCommand("Select * from TBL_UNIT where UNIT_NAME = N'"+cbo_units.Text.Trim()+"' " , con);
                
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txt_id_units.Text = dr["ID_UNIT"].ToString();
                    txt_name_units.Text = dr["UNIT_NAME"].ToString();
                    txt_details_units.Text = dr["UNIT_DETAILS"].ToString();
                    lbl_msg_units.Text = "تم الاختيار بنجاح";
                }
                dr.Close();
                con.Close();
            }
        }

        public void Edite_unites()
        {
            try
            {

            
            con.Open();
            cmd = new SqlCommand("Select max(ID_UNIT) as fds From Tbl_Unit", con);
            cmd.ExecuteNonQuery();

            DataSet dsS = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dsS);

            int id;
            DataRow drS;
            drS = dsS.Tables[0].Rows[0];
            id = Convert.ToInt32(drS["fds"].ToString());

            id++;
            con.Close();

            if (txt_id_units.Text.Trim() != "" || txt_id_units.Text.Trim() != null)
            {
                con.Open();
                SqlCommand cmd_ED = new SqlCommand("UPDATE TBL_UNIT SET ID_UNIT = N'" + id + "',UNIT_NAME = N'" + txt_name_units.Text.Trim() + "',UNIT_DETAILS = N'" + txt_details_units.Text.Trim() + "' where ID_UNIT = N'"+txt_id_units.Text.Trim()+"'  ", con);
                cmd_ED.ExecuteNonQuery();

                cmd_ED.Parameters.AddWithValue("@ID_UNIT", id);
                cmd_ED.Parameters.AddWithValue("@UNIT_NAME", txt_name_units.Text.Trim());
                cmd_ED.Parameters.AddWithValue("@UNIT_DETAILS", txt_details_units.Text.Trim());

                lbl_msg_units.Text = "تم التعديل بنجاح ";
                con.Close();
                Fill_Units(cbo_units);
                }

            }
            catch (Exception)
            {

            }
        }
        private void btn_save_unit_Click(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("Select max(ID_UNIT) as fds From Tbl_Unit", con);
            cmd.ExecuteNonQuery();

            DataSet dsS = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dsS);

            int id;
            DataRow drS;
            drS = dsS.Tables[0].Rows[0];
            id = Convert.ToInt32(drS["fds"].ToString());

            id++;
            con.Close();

            if (txt_id_units.Text.Trim() == "" || txt_id_units.Text.Trim() == null && txt_name_units.Text.Trim() != "")
            {
                con.Open();
                cmd = new SqlCommand("Insert Into Tbl_Unit (ID_UNIT,UNIT_NAME,UNIT_DETAILS) Values (N'"+id+"',N'"+ txt_name_units.Text.Trim() + "',N'" + txt_details_units.Text.Trim() + "') ", con);
                cmd.ExecuteNonQuery();

                cmd.Parameters.AddWithValue("@ID_UNIT", id);
                cmd.Parameters.AddWithValue("@UNIT_NAME", txt_name_units.Text.Trim());
                cmd.Parameters.AddWithValue("@UNIT_DETAILS", txt_details_units.Text.Trim());

                lbl_msg_units.Text = "تم الحفظ بنجاح ";
                con.Close();
                Fill_Units(cbo_units);
            }
            else if (txt_id_units.Text.Trim() != "" || txt_id_units.Text.Trim() != null)
            {
                Edite_unites();
            }
            else { lbl_msg_units.Text = "من فضلك املأ البيانات"; }
        }

        private void btn_edits_units_Click(object sender, EventArgs e)
        {
            Edite_unites();
        }

        private void frm_units_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select Units_open,Units_save,Units_delete,Units_edite,Units_new From USERS_PERMESSIONS Where USER_NAME = N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd22 = new SqlCommand(Query, con);
                SqlDataReader dr2 = cmd22.ExecuteReader();
                if (dr2.Read())
                {
                    if (dr2["Units_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr2["Units_save"].ToString() == "True") { btn_save_unit.Enabled = true; } else { btn_save_unit.Enabled = false; }
                    if (dr2["Units_delete"].ToString() == "True") { btn_delete_units.Enabled = true; } else { btn_delete_units.Enabled = false; }
                    if (dr2["Units_edite"].ToString() == "True") { btn_edits_units.Enabled = true; } else { btn_edits_units.Enabled = false; }
                    if (dr2["Units_new"].ToString() == "True") { btn_new_units.Enabled = true; } else { btn_new_units.Enabled = false; }
                }
                dr2.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }
    }
}
