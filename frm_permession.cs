using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace SALES
{
    public partial class frm_permession : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        //Boolean invoice_open_ = false;
        public frm_permession()
        {
            InitializeComponent();
        }

        private void frm_login_Load(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string query = " SELECT * FROM TBL_USERS ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dt = cmd.ExecuteReader();
                while (dt.Read())
                {
                    cbo_login.Items.Add(dt["USER_NAME"]).ToString();
                }
                dt.Close();
            } catch (Exception) { }
            finally { con.Close(); }
            //btn_save.Enabled = false;
            //btn_Delete.Enabled = false;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            //USER_NAME.Text = "";
            //PASSWORD.Text = "";
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbo_login_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ClearAll();
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string query = "SELECT * FROM USERS_PERMESSIONS WHERE USER_NAME = N'" + cbo_login.Text.Trim() + "' ";
                SqlCommand cmd2 = new SqlCommand(query, con);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    USER_NAME.Text = dr["USER_NAME"].ToString();
                    PASSWORD.Text = dr["PASSWORD"].ToString();
                    USER_LEVEL.Text = dr["USER_LEVEL"].ToString();
                    invoice_open.Checked = Convert.ToBoolean(dr["invoice_open"].ToString());
                    invoice_save.Checked = Convert.ToBoolean(dr["invoice_save"].ToString());
                    invoice_delete.Checked = Convert.ToBoolean(dr["invoice_delete"].ToString());
                    invoice_edite.Checked = Convert.ToBoolean(dr["invoice_edite"].ToString());
                    invoice_new.Checked = Convert.ToBoolean(dr["invoice_new"].ToString());
                    Billing_open_screen.Checked = Convert.ToBoolean(dr["Billing_open_screen"].ToString());
                    Billing_save.Checked = Convert.ToBoolean(dr["Billing_save"].ToString());
                    Billing_delete.Checked = Convert.ToBoolean(dr["Billing_delete"].ToString());
                    Billing_edite.Checked = Convert.ToBoolean(dr["Billing_edite"].ToString());
                    Billing_new.Checked = Convert.ToBoolean(dr["Billing_new"].ToString());
                    products_open.Checked = Convert.ToBoolean(dr["products_open"].ToString());
                    products_save.Checked = Convert.ToBoolean(dr["products_save"].ToString());
                    products_delete.Checked = Convert.ToBoolean(dr["products_delete"].ToString());
                    products_edite.Checked = Convert.ToBoolean(dr["products_edite"].ToString());
                    products_new.Checked = Convert.ToBoolean(dr["products_new"].ToString());
                    open_sections.Checked = Convert.ToBoolean(dr["open_sections"].ToString());
                    sections_save.Checked = Convert.ToBoolean(dr["sections_save"].ToString());
                    sections_delete.Checked = Convert.ToBoolean(dr["sections_delete"].ToString());
                    sections_edite.Checked = Convert.ToBoolean(dr["sections_edite"].ToString());
                    sections_new.Checked = Convert.ToBoolean(dr["sections_new"].ToString());
                    Employee_open.Checked = Convert.ToBoolean(dr["Employee_open"].ToString());
                    Employee_save.Checked = Convert.ToBoolean(dr["Employee_save"].ToString());
                    Employee_delete.Checked = Convert.ToBoolean(dr["Employee_delete"].ToString());
                    Employee_edite.Checked = Convert.ToBoolean(dr["Employee_edite"].ToString());
                    Employee_new.Checked = Convert.ToBoolean(dr["Employee_new"].ToString());
                    companies_open.Checked = Convert.ToBoolean(dr["companies_open"].ToString());
                    companies_save.Checked = Convert.ToBoolean(dr["companies_save"].ToString());
                    companies_delete.Checked = Convert.ToBoolean(dr["companies_delete"].ToString());
                    companies_edite.Checked = Convert.ToBoolean(dr["companies_edite"].ToString());
                    companies_new.Checked = Convert.ToBoolean(dr["companies_new"].ToString());
                    Deleget_open.Checked = Convert.ToBoolean(dr["Deleget_open"].ToString());
                    Deleget_save.Checked = Convert.ToBoolean(dr["Deleget_save"].ToString());
                    Deleget_delete.Checked = Convert.ToBoolean(dr["Deleget_delete"].ToString());
                    Deleget_edite.Checked = Convert.ToBoolean(dr["Deleget_edite"].ToString());
                    Deleget_new.Checked = Convert.ToBoolean(dr["Deleget_new"].ToString());
                    users_open.Checked = Convert.ToBoolean(dr["users_open"].ToString());
                    users_save.Checked = Convert.ToBoolean(dr["users_save"].ToString());
                    users_delete.Checked = Convert.ToBoolean(dr["users_delete"].ToString());
                    users_edite.Checked = Convert.ToBoolean(dr["users_edite"].ToString());
                    users_new.Checked = Convert.ToBoolean(dr["users_new"].ToString());
                    Reports_open.Checked = Convert.ToBoolean(dr["Reports_open"].ToString());
                    Reports_save.Checked = Convert.ToBoolean(dr["Reports_save"].ToString());
                    Reports_delete.Checked = Convert.ToBoolean(dr["Reports_delete"].ToString());
                    Reports_edite.Checked = Convert.ToBoolean(dr["Reports_edite"].ToString());
                    Reports_new.Checked = Convert.ToBoolean(dr["Reports_new"].ToString());
                    Cutomers_open.Checked = Convert.ToBoolean(dr["Cutomers_open"].ToString());
                    Cutomers_save.Checked = Convert.ToBoolean(dr["Cutomers_save"].ToString());
                    Cutomers_delete.Checked = Convert.ToBoolean(dr["Cutomers_delete"].ToString());
                    Cutomers_edite.Checked = Convert.ToBoolean(dr["Cutomers_edite"].ToString());
                    Cutomers_new.Checked = Convert.ToBoolean(dr["Cutomers_new"].ToString());
                    store_open.Checked = Convert.ToBoolean(dr["store_open"].ToString());
                    store_save.Checked = Convert.ToBoolean(dr["store_save"].ToString());
                    store_delete.Checked = Convert.ToBoolean(dr["store_delete"].ToString());
                    store_edite.Checked = Convert.ToBoolean(dr["store_edite"].ToString());
                    store_new.Checked = Convert.ToBoolean(dr["store_new"].ToString());
                    branches_open.Checked = Convert.ToBoolean(dr["branches_open"].ToString());
                    branches_save.Checked = Convert.ToBoolean(dr["branches_save"].ToString());
                    branches_delete.Checked = Convert.ToBoolean(dr["branches_delete"].ToString());
                    branches_edite.Checked = Convert.ToBoolean(dr["branches_edite"].ToString());
                    branches_new.Checked = Convert.ToBoolean(dr["branches_new"].ToString());
                    pay_open.Checked = Convert.ToBoolean(dr["pay_open"].ToString());
                    pay_save.Checked = Convert.ToBoolean(dr["pay_save"].ToString());
                    pay_delete.Checked = Convert.ToBoolean(dr["pay_delete"].ToString());
                    pay_edite.Checked = Convert.ToBoolean(dr["pay_edite"].ToString());
                    pay_new.Checked = Convert.ToBoolean(dr["pay_new"].ToString());
                    catch_open.Checked = Convert.ToBoolean(dr["catch_open"].ToString());
                    catch_save.Checked = Convert.ToBoolean(dr["catch_save"].ToString());
                    catch_delete.Checked = Convert.ToBoolean(dr["catch_delete"].ToString());
                    catch_edite.Checked = Convert.ToBoolean(dr["catch_edite"].ToString());
                    catch_new.Checked = Convert.ToBoolean(dr["catch_new"].ToString());
                    currencey_open.Checked = Convert.ToBoolean(dr["currencey_open"].ToString());
                    currencey_save.Checked = Convert.ToBoolean(dr["currencey_save"].ToString());
                    currencey_delete.Checked = Convert.ToBoolean(dr["currencey_delete"].ToString());
                    currencey_edite.Checked = Convert.ToBoolean(dr["currencey_edite"].ToString());
                    currencey_new.Checked = Convert.ToBoolean(dr["currencey_new"].ToString());
                    insert_money_open.Checked = Convert.ToBoolean(dr["insert_money_open"].ToString());
                    insert_money_save.Checked = Convert.ToBoolean(dr["insert_money_save"].ToString());
                    insert_money_delete.Checked = Convert.ToBoolean(dr["insert_money_delete"].ToString());
                    insert_money_edite.Checked = Convert.ToBoolean(dr["insert_money_edite"].ToString());
                    insert_money_new.Checked = Convert.ToBoolean(dr["insert_money_new"].ToString());
                    invoice_back_open.Checked = Convert.ToBoolean(dr["invoice_back_open"].ToString());
                    invoice_back_save.Checked = Convert.ToBoolean(dr["invoice_back_save"].ToString());
                    invoice_back_delete.Checked = Convert.ToBoolean(dr["invoice_back_delete"].ToString());
                    invoice_back_edite.Checked = Convert.ToBoolean(dr["invoice_back_edite"].ToString());
                    invoice_back_new.Checked = Convert.ToBoolean(dr["invoice_back_new"].ToString());
                    billing_back_open.Checked = Convert.ToBoolean(dr["billing_back_open"].ToString());
                    billing_back_save.Checked = Convert.ToBoolean(dr["billing_back_save"].ToString());
                    billing_back_delete.Checked = Convert.ToBoolean(dr["billing_back_delete"].ToString());
                    billing_back_edite.Checked = Convert.ToBoolean(dr["billing_back_edite"].ToString());
                    billing_back_new.Checked = Convert.ToBoolean(dr["billing_back_new"].ToString());
                    tresurey_open.Checked = Convert.ToBoolean(dr["tresurey_open"].ToString());
                    tresurey_save.Checked = Convert.ToBoolean(dr["tresurey_save"].ToString());
                    tresurey_delete.Checked = Convert.ToBoolean(dr["tresurey_delete"].ToString());
                    tresurey_edite.Checked = Convert.ToBoolean(dr["tresurey_edite"].ToString());
                    tresurey_new.Checked = Convert.ToBoolean(dr["tresurey_new"].ToString());
                    Units_open.Checked = Convert.ToBoolean(dr["Units_open"].ToString());
                    Units_save.Checked = Convert.ToBoolean(dr["Units_save"].ToString());
                    Units_delete.Checked = Convert.ToBoolean(dr["Units_delete"].ToString());
                    Units_edite.Checked = Convert.ToBoolean(dr["Units_edite"].ToString());
                    Units_new.Checked = Convert.ToBoolean(dr["Units_new"].ToString());
                    permessions_open.Checked = Convert.ToBoolean(dr["permessions_open"].ToString());
                    permessions_save.Checked = Convert.ToBoolean(dr["permessions_save"].ToString());
                    permessions_delete.Checked = Convert.ToBoolean(dr["permessions_delete"].ToString());
                    permessions_edite.Checked = Convert.ToBoolean(dr["permessions_edite"].ToString());
                    permessions_new.Checked = Convert.ToBoolean(dr["permessions_new"].ToString());

                    back_copy_open.Checked = Convert.ToBoolean(dr["back_copy_open"].ToString());
                    back_copy_choose.Checked = Convert.ToBoolean(dr["back_copy_choose"].ToString());
                    back_copy_save.Checked = Convert.ToBoolean(dr["back_copy_save"].ToString());
                    back_copy_close.Checked = Convert.ToBoolean(dr["back_copy_close"].ToString());
                    take_back_open.Checked = Convert.ToBoolean(dr["take_back_open"].ToString());
                    take_back_choose.Checked = Convert.ToBoolean(dr["take_back_choose"].ToString());
                    take_back_save.Checked = Convert.ToBoolean(dr["take_back_save"].ToString());
                    take_back_close.Checked = Convert.ToBoolean(dr["take_back_close"].ToString());
                    DETAILS.Text = dr["DETAILS"].ToString();
                    
                }
                else
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    string query3 = "SELECT * FROM TBL_USERS WHERE USER_NAME = N'" + cbo_login.Text.Trim() + "' ";
                    SqlCommand cmd3 = new SqlCommand(query3, con);
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);
                    if (dt3.Rows.Count > 0)
                    {
                        USER_NAME.Text = dt3.Rows[0]["USER_NAME"].ToString();
                        PASSWORD.Text = dt3.Rows[0]["PASSWORD"].ToString();
                        USER_LEVEL.Text = dt3.Rows[0]["USER_LEVEL"].ToString();
                    }
                    if (con.State == ConnectionState.Open) { con.Close(); }
                }
            } catch (Exception) { }
            finally 
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                if (USER_NAME.Text == "ADMIN") { PASSWORD.Text = ""; }
                btn_save.Enabled = true;
                btn_Delete.Enabled = true;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Select_Customer_Name();
            btn_save.Enabled = false;
            btn_Delete.Enabled = false;
        }

        public void Save()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Sql = "Insert Into USERS_PERMESSIONS Values(N'"+ USER_NAME.Text.Trim() + "',N'"+ PASSWORD.Text.Trim() + "'," +
                    "N'" + USER_LEVEL.Text.Trim() + "',N'" + invoice_open.Checked.ToString().ToString() + "',N'" + invoice_save.Checked.ToString() + "'," +
                    "N'" + invoice_delete.Checked.ToString() + "',N'" + invoice_edite.Checked.ToString() + "',N'" + invoice_new.Checked.ToString() + "'," +
                    "N'" + Billing_open_screen.Checked.ToString() + "',N'" + Billing_save.Checked.ToString() + "',N'" + Billing_delete.Checked.ToString() + "'," +
                    "N'" + Billing_edite.Checked.ToString() + "',N'" + Billing_new.Checked.ToString() + "',N'" + products_open.Checked.ToString() + "'," +
                    "N'" + products_save.Checked.ToString() + "',N'" + products_delete.Checked.ToString() + "',N'" + products_edite.Checked.ToString() + "'," +
                    "N'" + products_new.Checked.ToString() + "',N'" + open_sections.Checked.ToString() + "',N'" + sections_save.Checked.ToString() + "'," +
                    "N'" + sections_delete.Checked.ToString() + "',N'" + sections_edite.Checked.ToString() + "',N'" + sections_new.Checked.ToString() + "'," +
                    "N'" + Employee_open.Checked.ToString() + "',N'" + Employee_save.Checked.ToString() + "',N'" + Employee_delete.Checked.ToString() + "'," +
                    "N'" + Employee_edite.Checked.ToString() + "',N'" + Employee_new.Checked.ToString() + "',N'" + companies_open.Checked.ToString() + "'," +
                    "N'" + companies_save.Checked.ToString() + "',N'" + companies_delete.Checked.ToString() + "',N'" + companies_edite.Checked.ToString() + "'," +
                    "N'" + companies_new.Checked.ToString() + "',N'" + Deleget_open.Checked.ToString() + "',N'" + Deleget_save.Checked.ToString() + "'," +
                    "N'" + Deleget_delete.Checked.ToString() + "',N'" + Deleget_edite.Checked.ToString() + "',N'" + Deleget_new.Checked.ToString() + "'," +
                    "N'" + users_open.Checked.ToString() + "',N'" + users_save.Checked.ToString() + "',N'" + users_delete.Checked.ToString() + "'," +
                    "N'" + users_edite.Checked.ToString() + "',N'" + users_new.Checked.ToString() + "',N'" + Reports_open.Checked.ToString() + "'," +
                    "N'" + Reports_save.Checked.ToString() + "',N'" + Reports_delete.Checked.ToString() + "',N'" + Reports_edite.Checked.ToString() + "'," +
                    "N'" + Reports_new.Checked.ToString() + "',N'" + Cutomers_open.Checked.ToString() + "',N'" + Cutomers_save.Checked.ToString() + "'," +
                    "N'" + Cutomers_delete.Checked.ToString() + "',N'" + Cutomers_edite.Checked.ToString() + "',N'" + Cutomers_new.Checked.ToString() + "'," +
                    "N'" + store_open.Checked.ToString() + "',N'" + store_save.Checked.ToString() + "',N'" + store_delete.Checked.ToString() + "'," +
                    "N'" + store_edite.Checked.ToString() + "',N'" + store_new.Checked.ToString() + "',N'" + branches_open.Checked.ToString() + "'," +
                    "N'" + branches_save.Checked.ToString() + "',N'" + branches_delete.Checked.ToString() + "',N'" + branches_edite.Checked.ToString() + "'," +
                    "N'" + branches_new.Checked.ToString() + "',N'" + pay_open.Checked.ToString() + "',N'" + pay_save.Checked.ToString() + "'," +
                    "N'" + pay_delete.Checked.ToString() + "',N'" + pay_edite.Checked.ToString() + "',N'" + pay_new.Checked.ToString() + "'," +
                    "N'" + catch_open.Checked.ToString() + "',N'" + catch_save.Checked.ToString() + "',N'" + catch_delete.Checked.ToString() + "'," +
                    "N'" + catch_edite.Checked.ToString() + "',N'" + catch_new.Checked.ToString() + "',N'" + currencey_open.Checked.ToString() + "'," +
                    "N'" + currencey_save.Checked.ToString() + "',N'" + currencey_delete.Checked.ToString() + "',N'" + currencey_edite.Checked.ToString() + "'," +
                    "N'" + currencey_new.Checked.ToString() + "',N'" + insert_money_open.Checked.ToString() + "',N'" + insert_money_save.Checked.ToString() + "'," +
                    "N'" + insert_money_delete.Checked.ToString() + "',N'" + insert_money_edite.Checked.ToString() + "',N'" + insert_money_new.Checked.ToString() + "'," +
                    "N'" + invoice_back_open.Checked.ToString() + "',N'" + invoice_back_save.Checked.ToString() + "',N'" + invoice_back_delete.Checked.ToString() + "'," +
                    "N'" + invoice_back_edite.Checked.ToString() + "',N'" + invoice_back_new.Checked.ToString() + "',N'" + billing_back_open.Checked.ToString() + "'," +
                    "N'" + billing_back_save.Checked.ToString() + "',N'" + billing_back_delete.Checked.ToString() + "',N'" + billing_back_edite.Checked.ToString() + "'," +
                    "N'" + billing_back_new.Checked.ToString() + "',N'" + tresurey_open.Checked.ToString() + "',N'" + tresurey_save.Checked.ToString() + "'," +
                    "N'" + tresurey_delete.Checked.ToString() + "',N'" + tresurey_edite.Checked.ToString() + "',N'" + tresurey_new.Checked.ToString() + "'," +
                    "N'" + Units_open.Checked.ToString() + "',N'" + Units_save.Checked.ToString() + "',N'" + Units_delete.Checked.ToString() + "'," +
                    "N'" + Units_edite.Checked.ToString() + "',N'" + Units_new.Checked.ToString() + "',N'" + permessions_open.Checked.ToString() + "'," +
                    "N'" + permessions_save.Checked.ToString() + "',N'" + permessions_delete.Checked.ToString() + "',N'" + permessions_edite.Checked.ToString() + "'," +
                    "N'" + permessions_new.Checked.ToString() + "',N'" + back_copy_open.Checked.ToString() + "',N'" + back_copy_choose.Checked.ToString() + "'," +
                    "N'" + back_copy_save.Checked.ToString() + "',N'" + back_copy_close.Checked.ToString() + "',N'" + take_back_open.Checked.ToString() + "'," +
                    "N'" + take_back_choose.Checked.ToString() + "',N'" + take_back_save.Checked.ToString() + "',N'" + take_back_close.Checked.ToString() + "'," +
                    "N'" + DETAILS.Text.Trim() + "') ";

                SqlCommand cmd = new SqlCommand(Sql, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue(@"USER_NAME", USER_NAME.Text.Trim());
                cmd.Parameters.AddWithValue(@"PASSWORD", PASSWORD.Text.Trim());
                cmd.Parameters.AddWithValue(@"USER_LEVEL", USER_LEVEL.Text.Trim());
                cmd.Parameters.AddWithValue(@"invoice_open", invoice_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_save", invoice_save.Checked.ToString()); 
                cmd.Parameters.AddWithValue(@"invoice_delete", invoice_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_edite", invoice_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_new", invoice_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Billing_open_screen", Billing_open_screen.Checked.ToString()); 
                cmd.Parameters.AddWithValue(@"Billing_save", Billing_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Billing_delete", Billing_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Billing_edite", Billing_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Billing_new", Billing_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"products_open", products_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"products_save", products_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"products_delete", products_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"products_edite", products_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"products_new", products_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"open_sections", open_sections.Checked.ToString());
                cmd.Parameters.AddWithValue(@"sections_save", sections_save.Checked.ToString());                  
                cmd.Parameters.AddWithValue(@"sections_delete", sections_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"sections_edite", sections_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"sections_new", sections_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Employee_open", Employee_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Employee_save", Employee_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Employee_delete", Employee_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Employee_edite", Employee_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Employee_new", Employee_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"companies_open", companies_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"companies_save", companies_save.Checked.ToString());                
                cmd.Parameters.AddWithValue(@"companies_delete", companies_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"companies_edite", companies_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"companies_new", companies_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Deleget_open", Deleget_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Deleget_save", Deleget_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Deleget_delete", Deleget_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Deleget_edite", Deleget_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Deleget_new", Deleget_new.Checked.ToString());                           
                cmd.Parameters.AddWithValue(@"users_open", users_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"users_save", users_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"users_delete", users_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"users_edite", users_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"users_new", users_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Reports_open", Reports_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Reports_save", Reports_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Reports_delete", Reports_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Reports_edite", Reports_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Reports_new", Reports_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Cutomers_open", Cutomers_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Cutomers_save", Cutomers_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Cutomers_delete", Cutomers_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Cutomers_edite", Cutomers_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Cutomers_new", Cutomers_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"store_open", store_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"store_save", store_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"store_delete", store_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"store_edite", store_edite.Checked.ToString()); 
                cmd.Parameters.AddWithValue(@"store_new", store_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"branches_open", branches_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"branches_save", branches_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"branches_delete", branches_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"branches_edite", branches_edite.Checked.ToString()); 
                cmd.Parameters.AddWithValue(@"branches_new", branches_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"pay_open", pay_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"pay_save", pay_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"pay_delete", pay_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"pay_edite", pay_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"pay_new", pay_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"catch_open", catch_open.Checked.ToString());                                  
                cmd.Parameters.AddWithValue(@"catch_save", catch_save.Checked.ToString()); 
                cmd.Parameters.AddWithValue(@"catch_delete", catch_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"catch_edite", catch_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"catch_new", catch_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"currencey_open", currencey_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"currencey_save", currencey_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"currencey_delete", currencey_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"currencey_edite", currencey_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"currencey_new", currencey_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"insert_money_open", insert_money_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"insert_money_save", insert_money_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"insert_money_delete", insert_money_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"insert_money_edite", insert_money_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"insert_money_new", insert_money_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_back_open", invoice_back_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_back_save", invoice_back_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_back_delete", invoice_back_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_back_edite", invoice_back_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_back_new", invoice_back_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"billing_back_open", billing_back_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"billing_back_save", billing_back_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"billing_back_delete", billing_back_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"billing_back_edite", billing_back_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"billing_back_new", billing_back_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"tresurey_open", tresurey_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"tresurey_save", tresurey_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"tresurey_delete", tresurey_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"tresurey_edite", tresurey_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"tresurey_new", tresurey_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Units_open", Units_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Units_save", Units_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Units_delete", Units_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Units_edite", Units_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Units_new", Units_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"permessions_open", permessions_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"permessions_save", permessions_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"permessions_delete", permessions_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"permessions_edite", permessions_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"permessions_new", permessions_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"back_copy_open", back_copy_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"back_copy_choose", back_copy_choose.Checked.ToString());
                cmd.Parameters.AddWithValue(@"back_copy_save", back_copy_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"back_copy_close", back_copy_close.Checked.ToString());
                cmd.Parameters.AddWithValue(@"take_back_open", take_back_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"take_back_choose", take_back_choose.Checked.ToString());
                cmd.Parameters.AddWithValue(@"take_back_save", take_back_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"take_back_close", take_back_close.Checked.ToString());
                cmd.Parameters.AddWithValue(@"DETAILS", DETAILS.Text.Trim());
                MessageBox.Show("تم الحفظ بنجاح");
            } catch (Exception) {  }
            finally { con.Close(); ClearAll(); }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Sql = "Delete From USERS_PERMESSIONS Where USER_NAME = N'" + cbo_login.Text.Trim() + "' ";
                SqlCommand cmd = new SqlCommand(Sql, con);
                cmd.ExecuteNonQuery();
            } catch (Exception) { }
            finally 
            { 
                con.Close(); 
                ClearAll();
                btn_save.Enabled = false;
                btn_Delete.Enabled = false;
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        public void ClearAll()
        {
            try
            {
                cbo_login.Text = "";
                USER_NAME.Text = "";
                PASSWORD.Text  = "";
                USER_LEVEL.Text= "";
                invoice_open.CheckState = CheckState.Unchecked;
                invoice_save.CheckState = CheckState.Unchecked;
                invoice_delete.CheckState = CheckState.Unchecked;
                invoice_edite.CheckState = CheckState.Unchecked;
                invoice_new.CheckState = CheckState.Unchecked;
                Billing_open_screen.CheckState = CheckState.Unchecked;
                Billing_save.CheckState = CheckState.Unchecked;
                Billing_delete.CheckState = CheckState.Unchecked;
                Billing_edite.CheckState = CheckState.Unchecked;
                Billing_new.CheckState = CheckState.Unchecked;
                products_open.CheckState = CheckState.Unchecked;
                products_save.CheckState = CheckState.Unchecked;
                products_delete.CheckState = CheckState.Unchecked;
                products_edite.CheckState = CheckState.Unchecked;
                products_new.CheckState = CheckState.Unchecked;
                open_sections.CheckState = CheckState.Unchecked;
                sections_save.CheckState = CheckState.Unchecked;
                sections_delete.CheckState = CheckState.Unchecked;
                sections_edite.CheckState = CheckState.Unchecked;
                sections_new.CheckState = CheckState.Unchecked;
                Employee_open.CheckState = CheckState.Unchecked;
                Employee_save.CheckState = CheckState.Unchecked;
                Employee_delete.CheckState = CheckState.Unchecked;
                Employee_edite.CheckState = CheckState.Unchecked;
                Employee_new.CheckState = CheckState.Unchecked;
                companies_open.CheckState = CheckState.Unchecked;
                companies_save.CheckState = CheckState.Unchecked;
                companies_delete.CheckState = CheckState.Unchecked;
                companies_edite.CheckState = CheckState.Unchecked;
                companies_new.CheckState = CheckState.Unchecked;
                Deleget_open.CheckState = CheckState.Unchecked;
                Deleget_save.CheckState = CheckState.Unchecked;
                Deleget_delete.CheckState = CheckState.Unchecked;
                Deleget_edite.CheckState = CheckState.Unchecked;
                Deleget_new.CheckState = CheckState.Unchecked;
                users_open.CheckState = CheckState.Unchecked;
                users_save.CheckState = CheckState.Unchecked;
                users_delete.CheckState = CheckState.Unchecked;
                users_edite.CheckState = CheckState.Unchecked;
                users_new.CheckState = CheckState.Unchecked;
                Reports_open.CheckState = CheckState.Unchecked;
                Reports_save.CheckState = CheckState.Unchecked;
                Reports_delete.CheckState = CheckState.Unchecked;
                Reports_edite.CheckState = CheckState.Unchecked;
                Reports_new.CheckState = CheckState.Unchecked;
                Cutomers_open.CheckState = CheckState.Unchecked;
                Cutomers_save.CheckState = CheckState.Unchecked;
                Cutomers_delete.CheckState = CheckState.Unchecked;
                Cutomers_edite.CheckState = CheckState.Unchecked;
                Cutomers_new.CheckState = CheckState.Unchecked;
                store_open.CheckState = CheckState.Unchecked;
                store_save.CheckState = CheckState.Unchecked;
                store_delete.CheckState = CheckState.Unchecked;
                store_edite.CheckState = CheckState.Unchecked;
                store_new.CheckState = CheckState.Unchecked;
                branches_open.CheckState = CheckState.Unchecked;
                branches_save.CheckState = CheckState.Unchecked;
                branches_delete.CheckState = CheckState.Unchecked;
                branches_edite.CheckState = CheckState.Unchecked;
                branches_new.CheckState = CheckState.Unchecked;
                pay_open.CheckState = CheckState.Unchecked;
                pay_save.CheckState = CheckState.Unchecked;
                pay_delete.CheckState = CheckState.Unchecked;
                pay_edite.CheckState = CheckState.Unchecked;
                pay_new.CheckState = CheckState.Unchecked;
                catch_open.CheckState = CheckState.Unchecked;
                catch_save.CheckState = CheckState.Unchecked;
                catch_delete.CheckState = CheckState.Unchecked;
                catch_edite.CheckState = CheckState.Unchecked;
                catch_new.CheckState = CheckState.Unchecked;
                currencey_open.CheckState = CheckState.Unchecked;
                currencey_save.CheckState = CheckState.Unchecked;
                currencey_delete.CheckState = CheckState.Unchecked;
                currencey_edite.CheckState = CheckState.Unchecked;
                currencey_new.CheckState = CheckState.Unchecked;
                insert_money_open.CheckState = CheckState.Unchecked;
                insert_money_save.CheckState = CheckState.Unchecked;
                insert_money_delete.CheckState = CheckState.Unchecked;
                insert_money_edite.CheckState = CheckState.Unchecked;
                insert_money_new.CheckState = CheckState.Unchecked;
                invoice_back_open.CheckState = CheckState.Unchecked;
                invoice_back_save.CheckState = CheckState.Unchecked;
                invoice_back_delete.CheckState = CheckState.Unchecked;
                invoice_back_edite.CheckState = CheckState.Unchecked;
                invoice_back_new.CheckState = CheckState.Unchecked;
                billing_back_open.CheckState = CheckState.Unchecked;
                billing_back_save.CheckState = CheckState.Unchecked;
                billing_back_delete.CheckState = CheckState.Unchecked;
                billing_back_edite.CheckState = CheckState.Unchecked;
                billing_back_new.CheckState = CheckState.Unchecked;
                tresurey_open.CheckState = CheckState.Unchecked;
                tresurey_save.CheckState = CheckState.Unchecked;
                tresurey_delete.CheckState = CheckState.Unchecked;
                tresurey_edite.CheckState = CheckState.Unchecked;
                tresurey_new.CheckState = CheckState.Unchecked;
                Units_open.CheckState = CheckState.Unchecked;
                Units_save.CheckState = CheckState.Unchecked;
                Units_delete.CheckState = CheckState.Unchecked;
                Units_edite.CheckState = CheckState.Unchecked;
                Units_new.CheckState = CheckState.Unchecked;
                permessions_open.CheckState = CheckState.Unchecked;
                permessions_save.CheckState = CheckState.Unchecked;
                permessions_delete.CheckState = CheckState.Unchecked;
                permessions_edite.CheckState = CheckState.Unchecked;
                permessions_new.CheckState = CheckState.Unchecked;
                back_copy_open.CheckState = CheckState.Unchecked;
                back_copy_choose.CheckState = CheckState.Unchecked;
                back_copy_save.CheckState = CheckState.Unchecked;
                back_copy_close.CheckState = CheckState.Unchecked;
                take_back_open.CheckState = CheckState.Unchecked;
                take_back_choose.CheckState = CheckState.Unchecked;
                take_back_save.CheckState = CheckState.Unchecked;
                take_back_close.CheckState = CheckState.Unchecked;
                DETAILS.Text = "";
            } catch (Exception) { }
        }

        private void invoice_open_CheckedChanged(object sender, EventArgs e)
        {
            // .......... //
        }

        public void Select_Customer_Name()
        {
            //try
            //{
                con.Open();
                string Sql = "Select USER_NAME from USERS_PERMESSIONS Where USER_NAME = N'"+ USER_NAME.Text.Trim() + "' ";
                SqlCommand cmd = new SqlCommand(Sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable DT3 = new DataTable();
                da.Fill(DT3);
                if (DT3.Rows.Count > 0)
                {
                    if ( USER_NAME.Text.Trim() == DT3.Rows[0]["USER_NAME"].ToString()) 
                    { Edite(); } 
                }
                else { Save(); }
            if (con.State == ConnectionState.Open) { con.Close(); }
            //} catch ( Exception ){  } 
            //finally { con.Close();  }
        }

        public void Edite()
        {
            try 
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string Sql_2 = "Update  USERS_PERMESSIONS Set USER_NAME =N'" + USER_NAME.Text.Trim() + "',PASSWORD =N'" + PASSWORD.Text.Trim() + "'," +
                    "USER_LEVEL =N'" + USER_LEVEL.Text.Trim() + "',invoice_open =N'" + invoice_open.Checked.ToString() + "',invoice_save =N'" + invoice_save.Checked.ToString() + "'," +
                    "invoice_delete =N'" + invoice_delete.Checked.ToString() + "',invoice_edite =N'" + invoice_edite.Checked.ToString() + "',invoice_new =N'" + invoice_new.Checked.ToString() + "'," +
                    "Billing_open_screen =N'" + Billing_open_screen.Checked.ToString() + "',Billing_save =N'" + Billing_save.Checked.ToString() + "',Billing_delete =N'" + Billing_delete.Checked.ToString() + "'," +
                    "Billing_edite =N'" + Billing_edite.Checked.ToString() + "',Billing_new =N'" + Billing_new.Checked.ToString() + "',products_open =N'" + products_open.Checked.ToString() + "'," +
                    "products_save =N'" + products_save.Checked.ToString() + "',products_delete =N'" + products_delete.Checked.ToString() + "',products_edite =N'" + products_edite.Checked.ToString() + "'," +
                    "products_new =N'" + products_new.Checked.ToString() + "',open_sections =N'" + open_sections.Checked.ToString() + "',sections_save =N'" + sections_save.Checked.ToString() + "'," +
                    "sections_delete =N'" + sections_delete.Checked.ToString() + "',sections_edite =N'" + sections_edite.Checked.ToString() + "',sections_new =N'" + sections_new.Checked.ToString() + "'," +
                    "Employee_open =N'" + Employee_open.Checked.ToString() + "',Employee_save =N'" + Employee_save.Checked.ToString() + "',Employee_delete =N'" + Employee_delete.Checked.ToString() + "'," +
                    "Employee_edite =N'" + Employee_edite.Checked.ToString() + "',Employee_new =N'" + Employee_new.Checked.ToString() + "',companies_open =N'" + companies_open.Checked.ToString() + "'," +
                    "companies_save =N'" + companies_save.Checked.ToString() + "',companies_delete =N'" + companies_delete.Checked.ToString() + "',companies_edite =N'" + companies_edite.Checked.ToString() + "'," +
                    "companies_new =N'" + companies_new.Checked.ToString() + "',Deleget_open =N'" + Deleget_open.Checked.ToString() + "',Deleget_save =N'" + Deleget_save.Checked.ToString() + "'," +
                    "Deleget_delete =N'" + Deleget_delete.Checked.ToString() + "',Deleget_edite =N'" + Deleget_edite.Checked.ToString() + "',Deleget_new =N'" + Deleget_new.Checked.ToString() + "'," +
                    "users_open =N'" + users_open.Checked.ToString() + "',users_save =N'" + users_save.Checked.ToString() + "',users_delete =N'" + users_delete.Checked.ToString() + "'," +
                    "users_edite =N'" + users_edite.Checked.ToString() + "',users_new =N'" + users_new.Checked.ToString() + "',Reports_open =N'" + Reports_open.Checked.ToString() + "'," +
                    "Reports_save =N'" + Reports_save.Checked.ToString() + "',Reports_delete =N'" + Reports_delete.Checked.ToString() + "',Reports_edite =N'" + Reports_edite.Checked.ToString() + "'," +
                    "Reports_new =N'" + Reports_new.Checked.ToString() + "',Cutomers_open =N'" + Cutomers_open.Checked.ToString() + "',Cutomers_save =N'" + Cutomers_save.Checked.ToString() + "'," +
                    "Cutomers_delete =N'" + Cutomers_delete.Checked.ToString() + "',Cutomers_edite =N'" + Cutomers_edite.Checked.ToString() + "',Cutomers_new =N'" + Cutomers_new.Checked.ToString() + "'," +
                    "store_open =N'" + store_open.Checked.ToString() + "',store_save =N'" + store_save.Checked.ToString() + "',store_delete =N'" + store_delete.Checked.ToString() + "'," +
                    "store_edite =N'" + store_edite.Checked.ToString() + "',store_new =N'" + store_new.Checked.ToString() + "',branches_open =N'" + branches_open.Checked.ToString() + "'," +
                    "branches_save =N'" + branches_save.Checked.ToString() + "',branches_delete =N'" + branches_delete.Checked.ToString() + "',branches_edite =N'" + branches_edite.Checked.ToString() + "'," +
                    "branches_new =N'" + branches_new.Checked.ToString() + "',pay_open =N'" + pay_open.Checked.ToString() + "',pay_save =N'" + pay_save.Checked.ToString() + "'," +
                    "pay_delete =N'" + pay_delete.Checked.ToString() + "',pay_edite =N'" + pay_edite.Checked.ToString() + "',pay_new =N'" + pay_new.Checked.ToString() + "'," +
                    "catch_open =N'" + catch_open.Checked.ToString() + "',catch_save =N'" + catch_save.Checked.ToString() + "',catch_delete =N'" + catch_delete.Checked.ToString() + "'," +
                    "catch_edite =N'" + catch_edite.Checked.ToString() + "',catch_new =N'" + catch_new.Checked.ToString() + "',currencey_open =N'" + currencey_open.Checked.ToString() + "'," +
                    "currencey_save =N'" + currencey_save.Checked.ToString() + "',currencey_delete =N'" + currencey_delete.Checked.ToString() + "',currencey_edite =N'" + currencey_edite.Checked.ToString() + "'," +
                    "currencey_new =N'" + currencey_new.Checked.ToString() + "',insert_money_open =N'" + insert_money_open.Checked.ToString() + "',insert_money_save =N'" + insert_money_save.Checked.ToString() + "'," +
                    "insert_money_delete =N'" + insert_money_delete.Checked.ToString() + "',insert_money_edite =N'" + insert_money_edite.Checked.ToString() + "',insert_money_new =N'" + insert_money_new.Checked.ToString() + "'," +
                    "invoice_back_open =N'" + invoice_back_open.Checked.ToString() + "',invoice_back_save =N'" + invoice_back_save.Checked.ToString() + "',invoice_back_delete =N'" + invoice_back_delete.Checked.ToString() + "'," +
                    "invoice_back_edite =N'" + invoice_back_edite.Checked.ToString() + "',invoice_back_new =N'" + invoice_back_new.Checked.ToString() + "',billing_back_open =N'" + billing_back_open.Checked.ToString() + "'," +
                    "billing_back_save =N'" + billing_back_save.Checked.ToString() + "',billing_back_delete =N'" + billing_back_delete.Checked.ToString() + "',billing_back_edite =N'" + billing_back_edite.Checked.ToString() + "'," +
                    "billing_back_new =N'" + billing_back_new.Checked.ToString() + "',tresurey_open =N'" + tresurey_open.Checked.ToString() + "',tresurey_save =N'" + tresurey_save.Checked.ToString() + "'," +
                    "tresurey_delete =N'" + tresurey_delete.Checked.ToString() + "',tresurey_edite =N'" + tresurey_edite.Checked.ToString() + "',tresurey_new =N'" + tresurey_new.Checked.ToString() + "'," +
                    "Units_open =N'" + Units_open.Checked.ToString() + "',Units_save =N'" + Units_save.Checked.ToString() + "',Units_delete =N'" + Units_delete.Checked.ToString() + "'," +
                    "Units_edite =N'" + Units_edite.Checked.ToString() + "',Units_new =N'" + Units_new.Checked.ToString() + "',permessions_open =N'" + permessions_open.Checked.ToString() + "'," +
                    "permessions_save =N'" + permessions_save.Checked.ToString() + "',permessions_delete =N'" + permessions_delete.Checked.ToString() + "',permessions_edite =N'" + permessions_edite.Checked.ToString() + "'," +
                    "permessions_new =N'" + permessions_new.Checked.ToString() + "',back_copy_open =N'" + back_copy_open.Checked.ToString() + "',back_copy_choose =N'" + back_copy_choose.Checked.ToString() + "'," +
                    "back_copy_save =N'" + back_copy_save.Checked.ToString() + "',back_copy_close =N'" + back_copy_close.Checked.ToString() + "',take_back_open =N'" + take_back_open.Checked.ToString() + "'," +
                    "take_back_choose =N'" + take_back_choose.Checked.ToString() + "',take_back_save =N'" + take_back_save.Checked.ToString() + "',take_back_close =N'" + take_back_close.Checked.ToString() + "'," +
                    "DETAILS =N'" + DETAILS.Text.Trim() + "' where USER_NAME=N'"+ USER_NAME.Text.Trim() + "' ";
                con.Open();
                SqlCommand cmd = new SqlCommand(Sql_2, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue(@"USER_NAME", USER_NAME.Text.Trim());
                cmd.Parameters.AddWithValue(@"PASSWORD", PASSWORD.Text.Trim());
                cmd.Parameters.AddWithValue(@"USER_LEVEL", USER_LEVEL.Text.Trim());
                cmd.Parameters.AddWithValue(@"invoice_open", invoice_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_save", invoice_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_delete", invoice_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_edite", invoice_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_new", invoice_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Billing_open_screen", Billing_open_screen.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Billing_save", Billing_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Billing_delete", Billing_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Billing_edite", Billing_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Billing_new", Billing_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"products_open", products_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"products_save", products_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"products_delete", products_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"products_edite", products_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"products_new", products_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"open_sections", open_sections.Checked.ToString());
                cmd.Parameters.AddWithValue(@"sections_save", sections_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"sections_delete", sections_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"sections_edite", sections_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"sections_new", sections_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Employee_open", Employee_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Employee_save", Employee_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Employee_delete", Employee_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Employee_edite", Employee_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Employee_new", Employee_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"companies_open", companies_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"companies_save", companies_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"companies_delete", companies_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"companies_edite", companies_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"companies_new", companies_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Deleget_open", Deleget_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Deleget_save", Deleget_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Deleget_delete", Deleget_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Deleget_edite", Deleget_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Deleget_new", Deleget_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"users_open", users_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"users_save", users_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"users_delete", users_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"users_edite", users_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"users_new", users_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Reports_open", Reports_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Reports_save", Reports_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Reports_delete", Reports_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Reports_edite", Reports_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Reports_new", Reports_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Cutomers_open", Cutomers_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Cutomers_save", Cutomers_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Cutomers_delete", Cutomers_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Cutomers_edite", Cutomers_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Cutomers_new", Cutomers_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"store_open", store_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"store_save", store_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"store_delete", store_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"store_edite", store_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"store_new", store_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"branches_open", branches_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"branches_save", branches_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"branches_delete", branches_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"branches_edite", branches_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"branches_new", branches_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"pay_open", pay_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"pay_save", pay_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"pay_delete", pay_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"pay_edite", pay_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"pay_new", pay_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"catch_open", catch_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"catch_save", catch_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"catch_delete", catch_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"catch_edite", catch_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"catch_new", catch_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"currencey_open", currencey_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"currencey_save", currencey_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"currencey_delete", currencey_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"currencey_edite", currencey_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"currencey_new", currencey_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"insert_money_open", insert_money_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"insert_money_save", insert_money_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"insert_money_delete", insert_money_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"insert_money_edite", insert_money_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"insert_money_new", insert_money_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_back_open", invoice_back_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_back_save", invoice_back_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_back_delete", invoice_back_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_back_edite", invoice_back_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"invoice_back_new", invoice_back_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"billing_back_open", billing_back_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"billing_back_save", billing_back_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"billing_back_delete", billing_back_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"billing_back_edite", billing_back_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"billing_back_new", billing_back_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"tresurey_open", tresurey_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"tresurey_save", tresurey_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"tresurey_delete", tresurey_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"tresurey_edite", tresurey_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"tresurey_new", tresurey_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Units_open", Units_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Units_save", Units_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Units_delete", Units_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Units_edite", Units_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"Units_new", Units_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"permessions_open", permessions_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"permessions_save", permessions_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"permessions_delete", permessions_delete.Checked.ToString());
                cmd.Parameters.AddWithValue(@"permessions_edite", permessions_edite.Checked.ToString());
                cmd.Parameters.AddWithValue(@"permessions_new", permessions_new.Checked.ToString());
                cmd.Parameters.AddWithValue(@"back_copy_open", back_copy_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"back_copy_choose", back_copy_choose.Checked.ToString());
                cmd.Parameters.AddWithValue(@"back_copy_save", back_copy_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"back_copy_close", back_copy_close.Checked.ToString());
                cmd.Parameters.AddWithValue(@"take_back_open", take_back_open.Checked.ToString());
                cmd.Parameters.AddWithValue(@"take_back_choose", take_back_choose.Checked.ToString());
                cmd.Parameters.AddWithValue(@"take_back_save", take_back_save.Checked.ToString());
                cmd.Parameters.AddWithValue(@"take_back_close", take_back_close.Checked.ToString());
                cmd.Parameters.AddWithValue(@"DETAILS", DETAILS.Text.Trim());
            } catch (Exception) {  } 
            finally { 
                con.Close(); 
                ClearAll(); 
                MessageBox.Show("تم التعديل بنجاح"); 
            }
        }

        private void frm_permession_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select permessions_open,permessions_save,permessions_delete,permessions_new From USERS_PERMESSIONS Where USER_NAME = N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd22 = new SqlCommand(Query, con);
                SqlDataReader dr2 = cmd22.ExecuteReader();
                if (dr2.Read())
                {
                    if (dr2["permessions_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr2["permessions_save"].ToString() == "True") { btn_save.Enabled = true; } else { btn_save.Enabled = false; }
                    if (dr2["permessions_delete"].ToString() == "True") { btn_Delete.Enabled = true; } else { btn_Delete.Enabled = false; }
                    if (dr2["permessions_new"].ToString() == "True") { btn_new.Enabled = true; } else { btn_new.Enabled = false; }
                }
                dr2.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }
    }
}
