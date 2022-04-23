using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SALES
{
    public partial class frm_Branches : Form
    {
        //SQLServer SQL = new SQLServer(".\\SQLEXPRESS", " RealApplication","","", true);
        //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-G2CDA6M\SQLEXPRESS;Initial Catalog=RealApplication;Integrated Security=True");
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");

        public frm_Branches()
        {
            InitializeComponent();
            //txt_lvl_branch.Text = statu ;
        }

        private void Frm_Branches_Load(object sender, EventArgs e)
        {
            ClearAll();
            FillData(cbo_branches);
            txt_lvl_branch.Text = Main_Form.PASSWORD_STRING.Trim();
            txt_user_name_branch.Text = Main_Form.USER_NAME_STRING.Trim();
        }
        private void Btn_save_branch_Click(object sender, EventArgs e)
        {
            if (txt_id_branch.Text.Trim() == "" && txt_branch_name.Text.Trim() != "")
            {
                int id = 0;
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Select max(ID_BRANCH) as fds From TBL_BRANCH";
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

                con.Open();
                cmd = new SqlCommand("INSERT INTO TBL_BRANCH VALUES (N'" + id + "',N'" + txt_branch_name.Text.Trim() + "',"+
                    "N'" + txt_branch_adress.Text.Trim() + "',N'" + txt_branch_phone_1.Text.Trim() + "',N'" + txt_branch_mobile_2.Text.Trim() + "',"+
                    "N'" + txt_branch_manger.Text.Trim() + "',N'" + date_branch.Text.Trim() + "',N'" + txt_branch_last_Edits.Text.Trim() + "'," +
                    "N'" + txt_user_name_branch.Text.Trim() + "',N'" + txt_branch_Edite_Name.Text.Trim() + "',N'" + txt_details.Text.Trim() + "' ) ", con);
                cmd.ExecuteNonQuery();

                cmd.Parameters.AddWithValue("@ID_BRANCH", id);
                cmd.Parameters.AddWithValue("@BRANCH_NAME", txt_branch_name.Text.Trim());
                cmd.Parameters.AddWithValue("@BRANCH_ADRESS", txt_branch_adress.Text.Trim());
                cmd.Parameters.AddWithValue("@BRANCH_MOBILE_1", txt_branch_phone_1.Text.Trim());
                cmd.Parameters.AddWithValue("@BRANCH_MOBILE_2", txt_branch_mobile_2.Text.Trim());
                cmd.Parameters.AddWithValue("@BRANCH_MANGER", txt_branch_manger.Text.Trim());
                cmd.Parameters.AddWithValue("@BRANCH_DATE_INSERT", date_branch.Text.Trim());
              //cmd.Parameters.AddWithValue("@BRANCH_DATE_EDITE", txt_branch_manger.Text.Trim());
                cmd.Parameters.AddWithValue("@BRANCH_USER_NAME", txt_user_name_branch.Text.Trim());
                //cmd.Parameters.AddWithValue("@BRANCH_USER_NAME_EDITS", txt_user_name_branch.Text.Trim());
                cmd.Parameters.AddWithValue("@BRANCH_DETAILS", txt_details.Text.Trim());

                con.Close();
                ClearAll();
                txt_msg_branches.Text = "تم الحفظ بنجاح";
                FillData(cbo_branches);
            } 
            else if (txt_id_branch.Text.Trim() != "" )
            {
                EditeBranch();
            }
        }
        private void ClearAll()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).Text = String.Empty;
                    txt_id_branch.Text = " ";
                }
            }
        }

        private void Btn_new_branch_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void Btn_edite_branch_Click(object sender, EventArgs e)
        {
            EditeBranch();
        }

        public void EditeBranch()
        {
            con.Open();
            cmd = new SqlCommand("UPDATE TBL_BRANCH set BRANCH_NAME = N'" + txt_branch_name.Text.Trim() + "',BRANCH_ADRESS= N'" + txt_branch_adress.Text.Trim() + "'," +
                " BRANCH_MOBILE_1 = N'" + txt_branch_phone_1.Text.Trim() + "', BRANCH_MOBILE_2 = N'" + txt_branch_mobile_2.Text.Trim() + "', BRANCH_MANGER= N'" + txt_branch_manger.Text.Trim() + "'," +
                " BRANCH_DATE_INSERT = N'" + txt_branch_insert_date.Text.Trim() + "', BRANCH_DATE_EDITE= N'" + date_branch.Text.Trim() + "'," +
                " BRANCH_USER_NAME = N'" + txt_branch_insert_name.Text.Trim() + "'," +
                " BRANCH_USER_NAME_EDITS = N'" + txt_user_name_branch.Text.Trim() + "'," +
                " BRANCH_DETAILS = N'" + txt_details.Text.Trim() + "' where ID_BRANCH = N'" + txt_id_branch.Text.Trim() + "' ", con);

            cmd.ExecuteNonQuery();

            cmd.Parameters.AddWithValue("@BRANCH_NAME", txt_branch_name.Text.Trim());
            cmd.Parameters.AddWithValue("@BRANCH_ADRESS", txt_branch_adress.Text.Trim());
            cmd.Parameters.AddWithValue("@BRANCH_MOBILE_1", txt_branch_phone_1.Text.Trim());
            cmd.Parameters.AddWithValue("@BRANCH_MOBILE_2", txt_branch_mobile_2.Text.Trim());
            cmd.Parameters.AddWithValue("@BRANCH_MANGER", txt_branch_manger.Text.Trim());
            //cmd.Parameters.AddWithValue("@BRANCH_DATE_INSERT", txt_branch_insert_date.Text.Trim());
            cmd.Parameters.AddWithValue("@BRANCH_DATE_EDITE", date_branch.Text.Trim());
            cmd.Parameters.AddWithValue("@BRANCH_USER_NAME", txt_branch_insert_name.Text.Trim());
            cmd.Parameters.AddWithValue("@BRANCH_USER_NAME_EDITS", txt_user_name_branch.Text.Trim());
            cmd.Parameters.AddWithValue("@BRANCH_DETAILS", txt_details.Text.Trim());
            con.Close();
            ClearAll();
            txt_msg_branches.Text = "  تم التعديل بنجاح  " ;
            FillData(cbo_branches);
        }

        private void Btn_delete_branch_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("انتبة ربما يكون يكون هذا العنصر مرتبط بعناصر اخرى", "تنبيه بالحذف", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) {
            
                if (txt_id_branch.Text.Trim() != "" || txt_id_branch.Text.Trim() != null)
                {
                    con.Close();
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM TBL_BRANCH WHERE ID_BRANCH = N'" + txt_id_branch.Text.Trim() + "' ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    txt_msg_branches.Text = "تم الحذف بنجاح";
                    ClearAll();
                    FillData(cbo_branches);
                }
            }
            else if(txt_id_branch.Text.Trim() == "" || txt_id_branch.Text.Trim() == null)
            {
                txt_msg_branches.Text = "من فضلك قم باختيار الصنف المراد حذفه" ;
            }
        }

        public void FillData(ComboBox cbo)
        {
            cbo_branches.Items.Clear();
            con.Open();
            cmd = new SqlCommand( " Select * From TBL_BRANCH " , con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
            cbo.Items.Add(dr["BRANCH_NAME"].ToString());    ///   BRANCH_NAME  هنا نغير أسم العمود المراد ظهوره
            }
            dr.Close();
            con.Close();
        }

        private void Cbo_branches_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbo_branches.SelectedIndex > -1)
            {
                con.Open();
                cmd = new SqlCommand("Select * From TBL_BRANCH Where BRANCH_NAME like N'"+ cbo_branches.Text.Trim() +"' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                    while(dr.Read())
                    {
                        txt_id_branch.Text = dr["ID_BRANCH"].ToString();
                        txt_branch_name.Text = dr["BRANCH_Name"].ToString();
                        txt_branch_adress.Text = dr["BRANCH_ADRESS"].ToString();
                        txt_branch_phone_1.Text = dr["BRANCH_MOBILE_1"].ToString();
                        txt_branch_mobile_2.Text = dr["BRANCH_MOBILE_2"].ToString();
                        txt_branch_manger.Text = dr["BRANCH_MANGER"].ToString();
                        txt_branch_insert_date.Text = dr["BRANCH_DATE_INSERT"].ToString();
                        txt_branch_last_Edits.Text = dr["BRANCH_DATE_EDITE"].ToString();
                        txt_branch_insert_name.Text = dr["BRANCH_USER_NAME"].ToString();
                        txt_branch_Edite_Name.Text= dr["BRANCH_USER_NAME_EDITS"].ToString();
                        txt_details.Text = dr["BRANCH_DETAILS"].ToString();
                    }
                    dr.Close();
                con.Close();
            }
        }

        private void frm_Branches_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select branches_open,branches_save,branches_delete,branches_edite,branches_new From USERS_PERMESSIONS Where USER_NAME = N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd22 = new SqlCommand(Query, con);
                SqlDataReader dr2 = cmd22.ExecuteReader();
                if (dr2.Read())
                {
                    if (dr2["branches_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr2["branches_save"].ToString() == "True") { } else { btn_save_branch.Enabled = false; }
                    if (dr2["branches_delete"].ToString() == "True") { } else { btn_delete_branch.Enabled = false; }
                    if (dr2["branches_edite"].ToString() == "True") { } else { btn_edite_branch.Enabled = false; }
                    if (dr2["branches_new"].ToString() == "True") { } else { btn_new_branch.Enabled = false; }
                } dr2.Close();
            } catch (Exception) { }
            finally { con.Close(); }
        }
    }
}
