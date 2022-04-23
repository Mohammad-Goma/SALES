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
    public partial class frm_Back : Form
    {
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");

        public frm_Back()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //try
            //{
                //string database = @"Data Source=DESKTOP-G2CDA6M\SQLEXPRESS;";
                if (txtFileName.Text == "")
                {
                    MessageBox.Show("يجب أختيار المسار أولا","تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string filename = @txtFileName.Text + "\\RealApplication" + DateTime.Now.ToShortDateString().Replace('/', '-')+ " - " + DateTime.Now.ToLongTimeString().Replace(':','-');
                if (con.State == ConnectionState.Open){ con.Close(); }
                con.Open();
                string querey = " backup database RealApplication to Disk ='" +filename+ ".bak' ";
                cmd = new SqlCommand( querey , con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("تمت عملية النسخ بنجاح","تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //} catch (Exception ex) { throw; }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Back_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select take_back_open,take_back_choose,take_back_save,take_back_close From USERS_PERMESSIONS Where USER_NAME = N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd22 = new SqlCommand(Query, con);
                SqlDataReader dr2 = cmd22.ExecuteReader();
                if (dr2.Read())
                {
                    if (dr2["take_back_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr2["take_back_choose"].ToString() == "True") { btnBrowse.Enabled = true; } else { btnBrowse.Enabled = false; }
                    if (dr2["take_back_save"].ToString() == "True") { btnCreate.Enabled = true; } else { btnCreate.Enabled = false; }
                    if (dr2["take_back_close"].ToString() == "True") { btnClose.Enabled = true; } else { btnClose.Enabled = false; }
                }
                dr2.Close();
            }
            catch (Exception) {  }
            finally { con.Close(); }
        }
    }
}
