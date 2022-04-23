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
    public partial class frm_Currency : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        Tbl_Currency Currency = new Tbl_Currency();
        public frm_Currency()
        {
            InitializeComponent();
        }

        private void frm_Currency_Load(object sender, EventArgs e)
        {
            Currency.Fill_Currency(cbo_Currency);
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if(txt_Currency_Name.Text.Trim() != "" && txt_Currency_Number.Text.Trim() == "" || txt_Currency_Number.Text.Trim() == null)
            {
                Currency.ADD_Currency(txt_Currency_Name);
                cbo_Currency.Items.Clear();
                Currency.Fill_Currency(cbo_Currency);
                msg_Currency.Text = "تم الحفظ بنجاح";
            }
            else
            if(txt_Currency_Number.Text.Trim() != "" || txt_Currency_Number.Text.Trim() != null)
            {
                Currency.Update_Currency(txt_Currency_Name);
                cbo_Currency.Items.Clear();
                Currency.Fill_Currency(cbo_Currency);
                msg_Currency.Text = "تم التعديل بنجاح";
            }
            else { msg_Currency.Text = "من فضلك إملأ البيانات"; }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (cbo_Currency.Items.Count > 1)
            {
                if (txt_Currency_Number.Text.Trim() != "" || txt_Currency_Number.Text.Trim() != null)
                {
                    Currency.Delete_currency(txt_Currency_Number , msg_Currency);
                    cbo_Currency.Items.Clear();
                    Currency.Fill_Currency(cbo_Currency);
                }
            }
            else
            if (cbo_Currency.Items.Count == 1)
            {
                msg_Currency.Text = " انتبه ستحذف كل البيانات المتبيقة";
            }
            else { msg_Currency.Text = "من فضلك اختر العملة المراد حذفها"; }
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            txt_Currency_Number.Text = " ";
            txt_Currency_Name.Text = " ";
            msg_Currency.Text = " ";
        }

        private void cbo_Currency_SelectedIndexChanged(object sender, EventArgs e)
        {
            Currency.Currency_Index_changed(cbo_Currency,txt_Currency_Number,txt_Currency_Name);
        }

        private void frm_Currency_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select currencey_open,currencey_save,currencey_delete,currencey_new From USERS_PERMESSIONS Where USER_NAME = N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd22 = new SqlCommand(Query, con);
                SqlDataReader dr2 = cmd22.ExecuteReader();
                if (dr2.Read())
                {
                    if (dr2["currencey_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr2["currencey_save"].ToString() == "True") { btn_Save.Enabled = true; } else { btn_Save.Enabled = false; }
                    if (dr2["currencey_delete"].ToString() == "True") { btn_Delete.Enabled = true; } else { btn_Delete.Enabled = false; }
                    if (dr2["currencey_new"].ToString() == "True") { btn_New.Enabled = true; } else { btn_New.Enabled = false; }
                }
                dr2.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }
    }
}
