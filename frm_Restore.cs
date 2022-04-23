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
    public partial class frm_Restore : Form
    {

        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");

        public frm_Restore()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = openFileDialog1.FileName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFileName.Text == "")
                {
                    MessageBox.Show("يجب اختيار المسار اولا", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                con.Open();
                string querey2 = "ALTER DATABASE RealApplication SET OFFLINE WITH ROLLBACK IMMEDIATE ;Restore Database RealApplication from Disk='" + txtFileName.Text + "' ";
                SqlCommand cmd = new SqlCommand(querey2 , con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show(" تمت عملية الاستعادة بنجاح "," تأكيد " , MessageBoxButtons.OK , MessageBoxIcon.Information);
            } catch (Exception ex){ MessageBox.Show(" " + ex , "تنبيه" , MessageBoxButtons.OK, MessageBoxIcon.Error) ; }
        }

        private void frm_Restore_Shown(object sender, EventArgs e)
        {
            lvl_permessions1();
        }
        public void lvl_permessions1()           
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Q = "Select back_copy_open,back_copy_choose,back_copy_save,back_copy_close From USERS_PERMESSIONS Where USER_NAME = N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd2 = new SqlCommand(Q, con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    if (dr2["back_copy_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr2["back_copy_choose"].ToString() == "True") { btnBrowse.Enabled = true; } else { btnBrowse.Enabled = false; }
                    if (dr2["back_copy_save"].ToString() == "True") { btnRestore.Enabled = true; } else { btnRestore.Enabled = false; }
                    if (dr2["back_copy_close"].ToString() == "True") { btnClose.Enabled = true; } else { btnClose.Enabled = false; }
                }
                dr2.Close();
            }
            catch (Exception) {  }
            finally { con.Close(); }
        }

        private void txtFileName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
