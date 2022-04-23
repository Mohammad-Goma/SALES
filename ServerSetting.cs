using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SALES
{
    public partial class FrmServerSetting : Form
    {
        public FrmServerSetting()
        {
            InitializeComponent();
        }

        private void btn_Enter_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Server_Name = txt_server_name.Text;
            Properties.Settings.Default.DataBaseName= txt_Data_Base.Text;
            Properties.Settings.Default.UserName= txt_userName.Text;
            Properties.Settings.Default.Password= txt_password.Text;
            //if (checkBox1.Checked == false)
            //{
            //    Properties.Settings.Default.Checkboxserver = false;
            //}
            //else { Properties.Settings.Default.Checkboxserver = true; }

             Properties.Settings.Default.Save();
             lbl_msg.Text = "تم الحفظ بنجاح";
        }

        private void FrmServerSetting_Load(object sender, EventArgs e)
        {
            try
            {

            msg.Visible = false;
            txt_server_name.Text = Properties.Settings.Default.Server_Name;
            txt_Data_Base.Text = Properties.Settings.Default.DataBaseName;
            txt_userName.Text = Properties.Settings.Default.UserName;
            txt_password.Text = Properties.Settings.Default.Password;
            if (Properties.Settings.Default.Checkboxserver == false)
            {
                checkBox1.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
            }
            }
            catch (Exception)
            {

            }
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt.Text.Trim().Replace("'", "") == "28807211300376")
                {
                  My_Panel.Visible = false;
                }
                else
                {
                    msg.Visible = true;
                    msg.Text = "يرجى الاتصال بمزود الخدمة // 01554234558";
                }
            }
            catch
            {
            }
        }
    }
}
