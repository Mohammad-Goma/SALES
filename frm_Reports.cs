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
    public partial class frm_Reports : Form
    {
        Customer_Account_Statement cus_state;
        Supplier_Account supplier_Account;
        Day_Note day_Note;
        Month_Note M_note;
        REPORT_PRODUCTS rpt_prod;
        REPORT_STORES _STORES;
        Customer_Account_Statement customer;
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");

        public frm_Reports()
        {
            InitializeComponent();
        }

        private void btn_report_emp_Click(object sender, EventArgs e)
        {
            //if (emp == null)
            //{
            //    emp = new REPORT_EMPLOYEIS();
            //    emp.Show();
            //}
            //else if (emp.Visible == true)
            //{
            //    emp.BringToFront();
            //    emp.Focus();
            //}
            //else { emp.Close(); emp = new REPORT_EMPLOYEIS(); emp.Show(); }
            //emp.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //    if (inv == null)
            //    {
            //        inv = new REPORT_INVOICE();
            //        inv.Show();
            //    }
            //    else if (inv.Visible == true)
            //    {
            //        inv.BringToFront();
            //        inv.Focus();
            //    }
            //    else { inv.Close(); inv = new REPORT_INVOICE(); inv.Show(); }
            //    inv.WindowState = FormWindowState.Maximized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (bill == null)
            //{
            //    bill = new REPORT_BILLING();
            //    bill.Show();
            //}
            //else if (bill.Visible == true)
            //{
            //    bill.BringToFront();
            //    bill.Focus();
            //}
            //else { bill.Close(); bill = new REPORT_BILLING(); bill.Show(); }
            //bill.WindowState = FormWindowState.Maximized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (rpt_prod == null)
            {
                rpt_prod = new REPORT_PRODUCTS();
                rpt_prod.Show();
            }
            else if (rpt_prod.Visible == true)
            {
                rpt_prod.BringToFront();
                rpt_prod.Focus();
            }
            else { rpt_prod.Close(); rpt_prod = new REPORT_PRODUCTS(); rpt_prod.Show(); }
            rpt_prod.WindowState = FormWindowState.Maximized;
        }



        private void button5_Click(object sender, EventArgs e)
        {
            //if (prod == null)
            //{
            //    prod = new REPORT_PRODUCTS();
            //    prod.Show();
            //}
            //else if (prod.Visible == true)
            //{
            //    prod.BringToFront();
            //    prod.Focus();
            //}
            //else { prod.Close(); prod = new REPORT_PRODUCTS(); prod.Show(); }
            //prod.WindowState = FormWindowState.Maximized;
        }

        private void btn_Employies_Click(object sender, EventArgs e)
        {
            
            if (cus_state == null)
            {
                cus_state = new Customer_Account_Statement();
                cus_state.Show();
            }
            else if (cus_state.Visible == true)
            {
                cus_state.BringToFront();
                cus_state.Focus();
            }
            else { cus_state.Close(); cus_state = new Customer_Account_Statement(); cus_state.Show(); }
            cus_state.WindowState = FormWindowState.Maximized;
        }

        private void btn_Deleget_Click(object sender, EventArgs e)
        {
            if (supplier_Account == null)
            {
                supplier_Account = new Supplier_Account();
                supplier_Account.Show();
            }
            else if (supplier_Account.Visible == true)
            {
                supplier_Account.BringToFront();
                supplier_Account.Focus();
            }
            else { supplier_Account.Close(); supplier_Account = new Supplier_Account(); supplier_Account.Show(); }
            supplier_Account.WindowState = FormWindowState.Maximized;

        }

        private void frm_Reports_Load(object sender, EventArgs e)  
        {
            
        }

        private void btn_day_note_Click(object sender, EventArgs e)
        {
            if (day_Note == null)
            {
                day_Note = new Day_Note();
                day_Note.Show();
            }
            else if (day_Note.Visible == true)
            {
                day_Note.BringToFront();
                day_Note.Focus();
            }
            else { day_Note.Close(); day_Note = new Day_Note(); day_Note.Show(); }
            day_Note.WindowState = FormWindowState.Maximized;
        }

        private void btn_Note_note_Click(object sender, EventArgs e)
        {
            if (M_note == null)
            {
                M_note = new Month_Note();
                M_note.Show();
            }
            else if (M_note.Visible == true)
            {
                M_note.BringToFront();
                M_note.Focus();
            }
            else { M_note.Close(); M_note = new Month_Note(); M_note.Show(); }
            M_note.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click_1(object sender, EventArgs e) 
        {
            if (_STORES == null)
            {
                _STORES = new REPORT_STORES();
                _STORES.Show();
            }
            else if (_STORES.Visible == true)
            {
                _STORES.BringToFront();
                _STORES.Focus();
            }
            else { _STORES.Close(); _STORES = new REPORT_STORES(); _STORES.Show(); }
            _STORES.WindowState = FormWindowState.Maximized;
        }

        private void frm_Reports_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select Reports_open,Reports_save,Reports_delete,Reports_new From USERS_PERMESSIONS Where USER_NAME=N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader dr7 = cmd.ExecuteReader();
                if (dr7.Read())
                {
                    if (dr7["Reports_open"].ToString() == "True") { } else { this.Close(); return; }
                    //if (dr7["sections_save"].ToString() == "True") { } else { btncat_add.Enabled = false; }
                    //if (dr7["sections_delete"].ToString() == "True") { } else { btncat_delete.Enabled = false; }
                    //if (dr7["sections_edite"].ToString() == "True") { } else { btncat_edit.Enabled = false; }
                    //if (dr7["sections_new"].ToString() == "True") { } else { btncatnew.Enabled = false; }
                }
                dr7.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        private void btn_Customer_Click(object sender, EventArgs e)
        {
            try
            {
                if (customer == null)
                {
                    customer = new Customer_Account_Statement();
                    customer.Show();
                }
                else if (rpt_prod.Visible == true)
                {
                    customer.BringToFront();
                    customer.Focus();
                }
                else { customer.Close(); customer = new Customer_Account_Statement(); customer.Show(); }
                customer.WindowState = FormWindowState.Maximized;
            }
            catch (Exception){ }
        }
    }
}
