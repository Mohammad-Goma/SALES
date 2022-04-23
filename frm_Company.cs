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
    public partial class frm_Company : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd;
        public frm_Company()
        {
            InitializeComponent();
        }

        private void Btn_save_company_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Parse(this.date_company.Text);
            
            if (txt_ID.Text.Trim() =="" && txt_company_name.Text.Trim()!="" )
            {
                con.Open();
                cmd = new SqlCommand("Select max(ID_COMPANY) as fds From TBL_COMPANY", con);
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

                con.Open();
                cmd = new SqlCommand("INSERT INTO TBL_COMPANY values (N'" + id + "',N'" + txt_company_name.Text.Trim() + "'" +
                    ",N'" + txt_tax_company.Text.Trim() + "',N'" + txt_company_adress.Text.Trim() + "',N'" + dateTime + "'" +
                    ",N'" + txt_company_mobile_1.Text.Trim() + "',N'" + txt_company_mobile_2.Text.Trim() + "'" +
                    ",N'" + txt_company_Email.Text.Trim() + "',N'" + txt_mail_box.Text.Trim() + "',N'" + txt_company_fax.Text.Trim() + "'" +
                    ",N'" + txt_company_details.Text.Trim() + "') ", con);

                cmd.ExecuteNonQuery();

                cmd.Parameters.AddWithValue("@ID_COMPANY", id);
                cmd.Parameters.AddWithValue("@COMPANY_NAME", txt_company_name.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_TAX", txt_tax_company.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_ADRESS", txt_company_adress.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_DATE", dateTime);
                cmd.Parameters.AddWithValue("@COMPANY_MOBILE_1", txt_company_mobile_1.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_MOBILE_2", txt_company_mobile_2.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_EMAIL", txt_company_Email.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_MAIL_BOX", txt_mail_box.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_FAX", txt_company_fax.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_DETAILS", txt_company_details.Text.Trim());
                con.Close();
                txt_msg_company.Text = "تم الحفظ بنجاح";
            }
            else if (txt_ID.Text.Trim() != "")
            {
                con.Open();
                cmd = new SqlCommand("UPDATE TBL_COMPANY SET COMPANY_NAME = N'" + txt_company_name.Text.Trim() + "'," +
                    "COMPANY_TAX= N'" + txt_tax_company.Text.Trim()+ "'," +
                    "COMPANY_ADRESS= N'" + txt_company_adress.Text.Trim()+ "',COMPANY_DATE =N'"+dateTime+"'," +
                    "COMPANY_MOBILE_1= N'" + txt_company_mobile_1.Text.Trim() + "'," +
                    "COMPANY_MOBILE_2= N'" + txt_company_mobile_2.Text.Trim() + "'," +
                    "COMPANY_EMAIL= N'" + txt_company_Email.Text.Trim() + "'," +
                    "COMPANY_MAIL_BOX= N'" + txt_mail_box.Text.Trim() + "',COMPANY_FAX= N'" + txt_company_fax.Text.Trim() + "'," +
                    "COMPANY_DETAILS= N'" + txt_company_details.Text.Trim() + "' where ID_COMPANY = N'"+txt_ID.Text.Trim()+"' ", con );

                cmd.ExecuteNonQuery();
                
                cmd.Parameters.AddWithValue("@COMPANY_NAME", txt_company_name.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_TAX", txt_tax_company.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_ADRESS", txt_company_adress.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_DATE", dateTime);
                cmd.Parameters.AddWithValue("@COMPANY_MOBILE_1", txt_company_mobile_1.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_MOBILE_2", txt_company_mobile_2.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_EMAIL", txt_company_Email.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_MAIL_BOX", txt_mail_box.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_FAX", txt_company_fax.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANY_DETAILS", txt_company_details.Text.Trim());

                con.Close();
                txt_msg_company.Text = "تم التعديل بنجاح";
            }
            cbo_company.Items.Clear();
            fill_company(cbo_company);
        }

        private void cleareall()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = "";
                }
            }
        }
        private void Btn_new_company_Click(object sender, EventArgs e)
        {
            cleareall();
        }

        private void Btn_Delete_company_Click(object sender, EventArgs e)
        {
            if (txt_ID.Text.Trim() != "")
            {
                con.Open();
                cmd = new SqlCommand("Delete from TBL_COMPANY where ID_COMPANY = N'" + txt_ID.Text.Trim()+"' ",con);
                cmd.ExecuteNonQuery();
                con.Close();
                cleareall();
                txt_msg_company.Text = "تم الحذف";
            }
            else { txt_msg_company.Text = "من فضلك اختر الرقم المراد حذفه"; }
            fill_company(cbo_company);
        }

        private void Cbo_company_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbo_company.SelectedIndex > -1)
            {
                con.Open();
                cmd = new SqlCommand("SELECT * from TBL_COMPANY WHERE COMPANY_NAME = N'" + cbo_company.Text.Trim() + "' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txt_ID.Text = dr["ID_COMPANY"].ToString();
                    txt_company_name.Text = dr["COMPANY_NAME"].ToString();
                    txt_tax_company.Text = dr["COMPANY_TAX"].ToString();
                    txt_company_adress.Text = dr["COMPANY_ADRESS"].ToString();
                    date_company.Text = dr["COMPANY_DATE"].ToString();
                    txt_company_mobile_1.Text = dr["COMPANY_MOBILE_1"].ToString();
                    txt_company_mobile_2.Text = dr["COMPANY_MOBILE_2"].ToString();
                    txt_company_Email.Text = dr["COMPANY_EMAIL"].ToString();
                    txt_mail_box.Text = dr["COMPANY_MAIL_BOX"].ToString();
                    txt_company_fax.Text = dr["COMPANY_FAX"].ToString();
                    txt_company_details.Text = dr["COMPANY_DETAILS"].ToString();
                }
                txt_msg_company.Text = "تم الاختيار ";
                dr.Close();
                con.Close();
            }
        }

        private void Frm_Company_Load(object sender, EventArgs e)
        {
            fill_company(cbo_company);
            txt_user_Company.Text = Main_Form.USER_NAME_STRING.Trim();
        }

        public void fill_company(ComboBox cbo)
        {
            cbo.Items.Clear();
            con.Open();
            cmd = new SqlCommand("SELECT * from TBL_COMPANY ", con);

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            data_gv_company.DataSource = dt;

            SqlDataReader dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                cbo.Items.Add(dr["COMPANY_NAME"].ToString());
            }
            con.Close();
        }

        private void Txt_search_company_TextChanged(object sender, EventArgs e)
        {
            con.Close();
            con.Open();
            cmd = new SqlCommand("select * from tbl_company where company_name like N'%"+txt_search_company.Text.Trim()+"%' ",con);
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            data_gv_company.DataSource = dt;
            con.Close();
        }

        private void frm_Company_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select companies_open,companies_save,companies_delete,companies_new From USERS_PERMESSIONS Where USER_NAME=N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader dr7 = cmd.ExecuteReader();
                if (dr7.Read())
                {
                    if (dr7["companies_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr7["companies_save"].ToString() == "True") { } else { btn_save_company.Enabled = false; }
                    if (dr7["companies_delete"].ToString() == "True") { } else { btn_Delete_company.Enabled = false; }
                    if (dr7["companies_new"].ToString() == "True") { } else { btn_new_company.Enabled = false; }
                }
                dr7.Close();
            } catch (Exception) { }
            finally { con.Close(); }
        }
    }
}
