using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Management;
using System.Text;
using System.Windows.Forms;


namespace SALES
{
    public partial class Main_Form : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source="+Properties.Settings.Default.Server_Name+"; Initial Catalog="+Properties.Settings.Default.DataBaseName+"; Integrated Security=false; User ID="+Properties.Settings.Default.UserName+"; PASSWORD="+Properties.Settings.Default.Password+"; ");
        // SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS,1433;Initial Catalog=RealApplication;Integrated Security=false; User ID="+Properties.Settings.Default.UserName+"; PASSWORD="+ Properties.Settings.Default.Password + "; ");
       
        SqlCommand cmd;
        ResizeTools resiz = new ResizeTools();
        //Boolean Test_Size = false;
        FrmServerSetting serverSetting;
        public frm_Billing billing;
        public frm_USERS frm_us;
        public frm_permession log;
        public frm_Reports _Reports;
        public frm_Restore _Restore;
        public frm_Back back;
        public frm_store frm_store;
        public frm_product_sections prodsection;
        public frm_Branches branches;
        public frm_product frm_Prod;
        public frm_Company frm_comp;
        public frm_Employee frm_emp;
        public frm_units unit;
        public frm_Deleget deleget;
        public frm_Customers _Customers;
        public frm_Invoice invoice;
        public static string USER_NAME_STRING;
        public static string PASSWORD_STRING;
        public frm_Currency _Currency;
        public frm_Treasury treasury;
        public frm_Back_billing back_Billing;
        public frm_back_invoice back_Invoice;
        public frm_Catch_Money Catch;
        public frm_Pay_Money receipt;
        public frm_Deposit Deposit;
        //private string domain = "01554234558";
        //Boolean found = false;

        bool hdid = false;
        bool biosNum = false;
        public static Form Instance { get; private set; }

        
        public Main_Form( )
        {
           
            InitializeComponent();
        }

       


        public void permission(int x , Button btn)
        {
            int levelPermission = 1 ;
            levelPermission = x ;
            if (levelPermission == 2) { btn.Visible = false; } 
            else if (levelPermission == 3) { btn.Visible = false; }
        }

        public void openform(Form form)
        {
            Form frm;
            frm = form;
            if (frm == null)
            {
                frm = new Form();
                frm.Show();
            }
            else { frm.BringToFront(); }
        }

        public void load_shown_resize(Form form)
        {
            Form frm = new Form();
            frm = form;
            if (frm.Created)
            {
                resiz.FullRatioTable();
            }
            else if (frm.ShowDialog() == DialogResult.OK)
            {
                //Test_Size = true;
                this.WindowState = FormWindowState.Normal;
            }
            //else if (frm.ResizeBegin==ResizeBegin.)
            //{
            //}
        }
        public void GetAllDiskDrivesID()
        {
            var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_DiskDrive");
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                HardDrive hd = new HardDrive();
                hd.SerialNo = wmi_HD.GetPropertyValue("SerialNumber").ToString(); //get the serailNumber of diskdrive
                if (hd.SerialNo == "S3YBNB0M302486M")  //    تغيير رقم الهارد   S3YBNB0M302486M   WD-WX61AA5N2LRD
                {
                    hdid = true;
                }
            }
            var search = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BIOS");
            foreach (ManagementObject wmi_HD in search.Get())
            {
                bios_v bis = new bios_v();
                bis.SerialNo = wmi_HD.GetPropertyValue("SerialNumber").ToString(); //get the serailNumber of diskdrive
                if (bis.SerialNo == "R90JE780R9N0B5C1800U")   //    تغيير رقم البورد FGVJJ72   R90JE780R9N0B5C1800U
                {
                    biosNum = true;
                }
            }
        }

        public class HardDrive
        {
            public string SerialNo { get; set; }
        }

        public class bios_v
        {
            public string SerialNo { get; set; }
        }

        private void Main_Form_Load(object sender, EventArgs e)
        {
            try
            {
                GetAllDiskDrivesID();
                //string path = "C:\\Windows\\Evu.sys";
                //if (File.ReadAllText(path).Contains("ممنوع حذف هذا الملف"))
                //{
                //    found = true;
                //}
                if (hdid || biosNum)      //hdid && biosNum           //found
                {
                    ToolStrip_main.Visible = false;
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    string query = " SELECT * FROM TBL_USERS ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cbo_login.Items.Add(dr["USER_NAME"]).ToString();
                    }
                    dr.Close();
                }
                else { MessageBox.Show("رقم الهارد أو رقم البوردة خطأ يرجى الإتصال بمزود الخدمة ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning); this.Close(); }
            }
            catch (Exception) {  }
            finally {  con.Close(); }
        }

        private void Main_Form_Shown(object sender, EventArgs e)
        {
            //    Test_Size = true;
            //    this.WindowState = FormWindowState.Maximized;
        }

        private void Main_Form_Resize(object sender, EventArgs e)
        {
            //if (Test_Size)
            //{ resiz.ResizeControls(); }
        }

        private void Btn_product_Sections_Click_2(object sender, EventArgs e)
        {
            if (prodsection == null)
            {
                prodsection = new frm_product_sections();
                prodsection.Show();
                
            }else if (prodsection.Visible == true)
            {
                prodsection.BringToFront();
                //prosection.Focus();
            }else
            {
                prodsection.Close();
                prodsection = new frm_product_sections();
                prodsection.Show();
            }
            prodsection.WindowState = FormWindowState.Normal;
        }
       
        private void Btn_branches_Click(object sender, EventArgs e)
        {
            if (branches == null)
            {
                branches = new frm_Branches();
                branches.Show();
            }
            else if (branches.Visible == true)
            {
                branches.BringToFront();
            }
            else { branches.Close(); branches = new frm_Branches(); branches.Show(); }
            branches.WindowState = FormWindowState.Normal;
        }

        private void Btn_store_Click(object sender, EventArgs e)
        {
            if (frm_store == null)
            {
                frm_store = new frm_store();
                frm_store.Show();
            }
            else if (frm_store.Visible == true)
            {
                frm_store.BringToFront();
                frm_store.Focus();
            }
            else { frm_store.Close(); frm_store = new frm_store(); frm_store.Show(); }

            frm_store.WindowState = FormWindowState.Normal;
        }

        private void Btn_product_Click(object sender, EventArgs e)
        {
            try
            {
                if (frm_Prod == null)
                {
                    frm_Prod = new frm_product();
                    frm_Prod.Show();
                }
                else if (frm_Prod.Visible == true)
                {
                    frm_Prod.BringToFront();
                }
                else { frm_Prod.Close(); frm_Prod = new frm_product(); frm_Prod.Show(); }
                frm_Prod.WindowState = FormWindowState.Maximized;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        private void Btn_Company_Click(object sender, EventArgs e)
        {
            if (frm_comp == null)
            {
                frm_comp = new frm_Company();
                frm_comp.Show();
            }
            else if (frm_comp.Visible == true)
            { frm_comp.BringToFront();
                frm_comp.Focus();
            }
            else { frm_comp.Close(); frm_comp = new frm_Company(); frm_comp.Show(); }
            frm_comp.WindowState = FormWindowState.Maximized;
        }

        private void Btn_Employee_Click(object sender, EventArgs e)
        {
            if(frm_emp == null)
            {
                frm_emp =  new frm_Employee();
                frm_emp.Show();
            }
            else if (frm_emp.Visible == true)
            {
                frm_emp.BringToFront();
                frm_emp.Focus();
            }
            else { frm_emp.Close(); frm_emp = new frm_Employee(); frm_emp.Show(); }
            frm_emp.WindowState = FormWindowState.Maximized;
        }

        private void btn_frm_unit_Click(object sender, EventArgs e)
        {
            if (unit == null)
            {
                unit = new frm_units();
                unit.Show();
            }
            else if (unit.Visible == true)
            {
                unit.BringToFront();
                unit.Focus();
            }
            else { unit.Close(); unit = new frm_units(); unit.Show(); }
            unit.WindowState = FormWindowState.Normal;
        }

        private void btn_Delegate_Click(object sender, EventArgs e)
        {
            if (deleget == null)
            {
                deleget = new frm_Deleget();
                deleget.Show();
            }
            else if (deleget.Visible == true)
            {
                deleget.BringToFront();
                deleget.Focus();
            }
            else { deleget.Close(); deleget = new frm_Deleget(); deleget.Show(); }
            deleget.WindowState = FormWindowState.Maximized;
        }

        private void btn_Customer_Click(object sender, EventArgs e)
        {
            if (_Customers == null)
            {
                _Customers = new frm_Customers();
                _Customers.Show();
            }
            else if (_Customers.Visible == true)
            {
                _Customers.BringToFront();
                _Customers.Focus();
            }
            else { _Customers.Close(); _Customers = new frm_Customers(); _Customers.Show(); }
            _Customers.WindowState = FormWindowState.Maximized;
        }

        private void btn_users_Click(object sender, EventArgs e)
        {
            if (frm_us == null)
            {
                frm_us = new frm_USERS();
                frm_us.Show();
            }
            else if (frm_us.Visible == true)
            {
                frm_us.BringToFront();
                frm_us.Focus();
            }
            else { frm_us.Close(); frm_us = new frm_USERS(); frm_us.Show(); }
            frm_us.WindowState = FormWindowState.Maximized;
        }

        private void btn_Enter_Click(object sender, EventArgs e)
        {
            SqlConnection con2 = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
            try
            {
                //if (con2.State == ConnectionState.Open) { con2.Close(); }
                //con2.Open();
                //string query = "SELECT USER_NAME,PASSWORD FROM TBL_USERS";
                //SqlCommand cmd = new SqlCommand(query, con2);
                //SqlDataReader dr = cmd.ExecuteReader();
                //while (dr.Read())
                //{
                //    if (txt_user_name.Text.Trim().Replace("'", "") == dr["USER_NAME"].ToString() && txt_user_pass.Text.Trim().Replace("'", "") == dr["PASSWORD"].ToString())
                //    {
                        TXT_USER_MAIN_FORM.Text = txt_user_name.Text.Trim();
                        panel_login.Visible = false;
                        this.WindowState = FormWindowState.Maximized;
                        ToolStrip_main.Visible = true;
                        toolStrip2.Visible = true;
                        toolStrip3.Visible = true;
                        this.BackColor = Color.LightSkyBlue;
                        pictureBox1.Visible = true;
                        pictureBox1.Dock = DockStyle.Fill;
                        USER_NAME_STRING = TXT_USER_MAIN_FORM.Text.Trim();
                        PASSWORD_STRING = txt_lvl_login_frm_main.Text.Trim();
                //    }
                //    else { msg_login.Text = "من فضلك تأكد من أسم المستخدم و كلمة السر"; }
                //}
                //dr.Close(); 
            }
            catch (Exception) { msg_login.Text = "من فضلك تأكد من الإتصال بالسرفر"; }
            finally
            {
                //con2.Close();
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            txt_user_name.Text = "";
            txt_user_pass.Text = "";
        }

        private void cbo_login_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            con.Open();
            string query = " SELECT * FROM TBL_USERS WHERE USER_NAME = N'" + cbo_login.Text.Trim() + "' ";
            cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txt_user_name.Text = dr["USER_NAME"].ToString();
                txt_lvl_login_frm_main.Text = dr["USER_LEVEL"].ToString();
            }
            dr.Close();
            con.Close();
        }

        private void btn_login_frm_Click(object sender, EventArgs e)
        {
            if (log == null)
            {
                log = new frm_permession();
                log.Show();
            }
            else if (log.Visible == true)
            {
                log.BringToFront();
                log.Focus();
            }
            else { log.Close(); log = new frm_permession(); log.Show(); }
            log.WindowState = FormWindowState.Maximized;
        }
       
        private void btn_sale_invoice_Click(object sender, EventArgs e)
        {
            if (invoice == null)
            {
                invoice = new frm_Invoice();
                invoice.Show();
            }
            else if (invoice.Visible == true)
            {
                invoice.BringToFront();
                invoice.Focus();
            }
            else { invoice.Close(); invoice = new frm_Invoice(); invoice.Show(); }
            invoice.WindowState = FormWindowState.Maximized;
        }

        private void btn_reports_Click(object sender, EventArgs e)
        {
            if (_Reports == null)
            {
                _Reports = new frm_Reports();
                _Reports.Show();
            }
            else if (_Reports.Visible == true)
            {
                _Reports.BringToFront();
                _Reports.Focus();
            }
            else { _Reports.Close(); _Reports = new frm_Reports(); _Reports.Show(); }
            _Reports.WindowState = FormWindowState.Normal;
        }

        private void btn_restore_Data_Click(object sender, EventArgs e)
        {
            if (_Restore == null)
            {
                _Restore = new frm_Restore();
                _Restore.Show();
            }
            else if (_Restore.Visible == true)
            {
                _Restore.BringToFront();
                _Restore.Focus();
            }
            else { _Restore.Close(); _Restore = new frm_Restore(); _Restore.Show(); }
            _Restore.WindowState = FormWindowState.Normal;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_frm_back_Click(object sender, EventArgs e)
        {
            if (back == null)
            {
                back = new frm_Back();
                back.Show();
            }
            else if (back.Visible == true)
            {
                back.BringToFront();
                back.Focus();
            }
            else { back.Close(); back = new frm_Back(); back.Show(); }
            back.WindowState = FormWindowState.Normal;
        }

        private void btn_buy_invoice_Click(object sender, EventArgs e)           //billing
        {
            if (billing == null)
            {
                billing = new frm_Billing();
                billing.Show();
            }
            else if (billing.Visible == true)
            {
                billing.BringToFront();
                billing.Focus();
            }
            else { billing.Close(); billing = new frm_Billing(); billing.Show(); }
            billing.WindowState = FormWindowState.Maximized;
        }

        private void txt_user_name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter){SendKeys.Send("{TAB}");}
        }

        private void txt_user_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter){SendKeys.Send("{TAB}");}
        }

        private void btn_currency_Click(object sender, EventArgs e)
        {
            if (_Currency == null)
            {
                _Currency = new frm_Currency();
                _Currency.Show();
            }
            else if (_Currency.Visible == true)
            {
                _Currency.BringToFront();
                _Currency.Focus();
            }
            else { _Currency.Close(); _Currency = new frm_Currency(); _Currency.Show(); }
            _Currency.WindowState = FormWindowState.Normal;
        }

        private void btn_Tresure_Click(object sender, EventArgs e)
        {
            if (treasury == null)
            {
                treasury = new frm_Treasury();
                treasury.Show();
            }
            else if (treasury.Visible == true)
            {
                treasury.BringToFront();
                treasury.Focus();
            }
            else { treasury.Close(); treasury = new frm_Treasury(); treasury.Show(); }
            treasury.WindowState = FormWindowState.Maximized;
        }

        private void btn_back_billing_Click(object sender, EventArgs e)
        {
            if (back_Billing == null)
            {
                back_Billing = new frm_Back_billing();
                back_Billing.Show();
            }
            else if (back_Billing.Visible == true)
            {
                back_Billing.BringToFront();
                back_Billing.Focus();
            }
            else { back_Billing.Close(); back_Billing = new frm_Back_billing(); back_Billing.Show(); }
            back_Billing.WindowState = FormWindowState.Maximized;
        }

        private void btn_back_Invoice_Click(object sender, EventArgs e)
        {
            if (back_Invoice == null)
            {
                back_Invoice = new frm_back_invoice();
                back_Invoice.Show();
            }
            else if (back_Invoice.Visible == true)
            {
                back_Invoice.BringToFront();
                back_Invoice.Focus();
            }
            else { back_Invoice.Close(); back_Invoice = new frm_back_invoice(); back_Invoice.Show(); }
            back_Invoice.WindowState = FormWindowState.Maximized;
        }

        private void btn_receipt_Click(object sender, EventArgs e)
        {
            if (receipt == null)
            {
                receipt = new frm_Pay_Money();
                receipt.Show();
            }
            else if (receipt.Visible == true)
            {
                receipt.BringToFront();
                receipt.Focus();
            }
            else { receipt.Close(); receipt = new frm_Pay_Money(); receipt.Show(); }
            receipt.WindowState = FormWindowState.Normal;
        }

        private void btn_Catch_Receipt_Click(object sender, EventArgs e)
        {
            if (Catch == null)
            {
                Catch = new frm_Catch_Money();
                Catch.Show();
            }
            else if (Catch.Visible == true)
            {
                Catch.BringToFront();
                Catch.Focus();
            }
            else { Catch.Close(); Catch = new frm_Catch_Money(); Catch.Show(); }
            Catch.WindowState = FormWindowState.Normal;
        }

        private void btn_Deposit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Deposit == null)
                {
                    Deposit = new frm_Deposit();
                    Deposit.Show();
                }
                else if (Deposit.Visible == true)
                {
                    Deposit.BringToFront();
                    Deposit.Focus();
                }
                else { Deposit.Close(); Deposit = new frm_Deposit(); Deposit.Show(); }
                Deposit.WindowState = FormWindowState.Normal;
            }
            catch (Exception){ }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
            if(txt_user_pass.UseSystemPasswordChar == true) { txt_user_pass.UseSystemPasswordChar = false; }
            else { txt_user_pass.UseSystemPasswordChar = true; }
        }

        private void txt_user_pass_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        public void lvl_permessions(Button btn1, Button btn2, Button btn3, Button btn4, Button btn5)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select * from USERS_PERMESSIONS Where USER_NAME = N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader dr1 = cmd.ExecuteReader();
                if (dr1.Read())
                {
                    if (dr1["invoice_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr1["invoice_save"].ToString() == "True") { } else { btn2.Enabled = false; }
                    if (dr1["invoice_new"].ToString() == "True") { } else { btn3.Enabled = false; }
                    if (dr1["invoice_new"].ToString() == "True") { } else { btn4.Enabled = false; }
                    if (dr1["invoice_new"].ToString() == "True") { } else { btn5.Enabled = false; }
                }
                dr1.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        public void lvl_permessions(Button btn1, Button btn2, Button btn3)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select * From USERS_PERMESSIONS Where USER_NAME = N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader dr2 = cmd.ExecuteReader();
                if (dr2.Read())
                {
                    if (dr2["invoice_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr2["invoice_save"].ToString() == "True") { } else { btn1.Enabled = false; }
                    if (dr2["invoice_new"].ToString() == "True") { } else { btn2.Enabled = false; }
                    if (dr2["invoice_new"].ToString() == "True") { } else { btn3.Enabled = false; }
                }
                dr2.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        public void lvl_permessions(Button btn1, Button btn2)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select * From USERS_PERMESSIONS Where USER_NAME=N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader dr7 = cmd.ExecuteReader();
                if (dr7.Read())
                {
                    if (dr7["invoice_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr7["invoice_save"].ToString() == "True") { } else { btn1.Enabled = false; }
                    if (dr7["invoice_new"].ToString() == "True") { } else { btn2.Enabled = false; }
                }
                dr7.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            if (serverSetting == null)
            {
                serverSetting = new FrmServerSetting();
                serverSetting.Show();
            }
            else if (serverSetting.Visible == true)
            {
                serverSetting.BringToFront();
                serverSetting.Focus();
            }
            else { serverSetting.Close(); serverSetting = new FrmServerSetting(); serverSetting.Show(); }
            serverSetting.WindowState = FormWindowState.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // string hdserial = "";
            // string biosserial = "";
        }
    }
}
