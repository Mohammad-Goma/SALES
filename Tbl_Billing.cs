using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SALES
{
    class Tbl_Billing
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd;

     
        public int ID
        {
            get;
            set;
        } 
        public string USER_NAME
        {
            get;
            set;
        }
        public string BRANCH_NAME
        {
            get;
            set;
        }
        public string STORE_NAME
        {
            get;
            set;
        }
        public string BOX_NAME
        {
            get;
            set;
        }
        public string BILLING_DELEGET_NUM
        {
            get;
            set;
        }
        public string CURENCY
        {
            get;
            set;
        }
        public string METHOD_OF_TAX
        {
            get;
            set;
        }
        public string DELEGET_NAME
        {
            get;
            set;
        }
        public string TYPE_BILLING
        {
            get;
            set;
        }
        public string TOTAL_DELEGET_BILLING
        {
            get;
            set;
        }
        public string Exchange_bill_number
        {
            get;
            set;
        }
        public string DELEGET_BILLING_DATE
        {
            get;
            set;
        }
        public string TOTAL_AMOUNT
        {
            get;
            set;
        }
        public string DISCOUNT_PERCENT
        {
            get;
            set;
        }
        public string DISCOUNT_MONEY
        {
            get;
            set;
        }
        public string TOTAL_BEFORE_DISCOUNT
        {
            get;
            set;
        }
        public string TOTAL_DISCOUNT
        {
            get;
            set;
        }
        public string TOTAL_AFTER_DISCOUNT
        {
            get;
            set;
        }
        public string PAY_FIRST
        {
            get;
            set;
        }
        public string THE_REST
        {
            get;
            set;
        }
        public string DATE_TIME_NOW
        {
            get;
            set;
        }
        public string DATE_EDITS
        {
            get;
            set;
        }
        public string USER_EDITS
        {
            get;
            set;
        }
        public string DETAILS
        {
            get;
            set;
        }

        public void Insert_billing( TextBox txt_id, TextBox txt_user_name, ComboBox cbo_branches,
            ComboBox cbo_store, ComboBox cbo_box, ComboBox cbo_name_Recive_Invoice, TextBox txt_Delget_number, ComboBox cbo_Currencey,
            TextBox txt_Exchange_bill_number, ComboBox cbo_Method_of_calculating_tax, ComboBox cbo_Deleget_Name, ComboBox cbo_Invoice_type,
            TextBox txt_Total_invoice_resource, DateTime Date_Of_Invoice_Deleget, TextBox txt_amount_of_product,
            TextBox txt_Discount_percent, TextBox txt_Discount_Money, TextBox txt_Total_Brfore_Discount,
            TextBox txt_total_Discount, TextBox Total_After_Discount, TextBox txt_Down_payment,
            TextBox txt_The_rest, DateTime Date_Time_Now, TextBox txt_Billing_last_Edits_date,
            TextBox txt_Billing_insert_name, TextBox txt_Billing_Edite_Name, TextBox txt_msg_Billing)
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            string date = Convert.ToString(Date_Of_Invoice_Deleget);

            string query = string.Format(" Insert Into Tbl_Billing Values(N'" + txt_id.Text.Trim() + "',N'" + txt_user_name.Text.Trim() + "'," +
                "N'" + cbo_branches.Text.Trim() + "',N'" + cbo_store.Text.Trim() + "',N'" + cbo_box.Text.Trim() + "'," +
                "N'" + cbo_name_Recive_Invoice.Text.Trim() + "',N'" + txt_Delget_number.Text.Trim() + "',N'" + cbo_Currencey.Text.Trim() + "'," +
                "N'" + txt_Exchange_bill_number.Text.Trim() + "',N'" + cbo_Method_of_calculating_tax.Text.Trim() + "'," +
                "N'" + cbo_Deleget_Name.Text.Trim() + "',N'" + cbo_Invoice_type.Text.Trim() + "',N'" + txt_Total_invoice_resource.Text.Trim() + "'," +
                "N'" + Date_Of_Invoice_Deleget.ToLongTimeString() + "',N'" + txt_amount_of_product.Text.Trim() + "',N'" + txt_Discount_percent.Text.Trim() + "',N'" + txt_Discount_Money.Text.Trim() + "'," +
                "N'" + txt_Total_Brfore_Discount.Text.Trim() + "',N'" + txt_total_Discount.Text.Trim() + "', N'" + Total_After_Discount.Text.Trim() + "'," +
                "N'" + txt_Down_payment.Text.Trim() + "',N'" + txt_The_rest.Text.Trim() + "'," +
                "N'" + Date_Time_Now.ToLongTimeString() + "',N'" + txt_Billing_last_Edits_date.Text.Trim() + "',N'" + txt_Billing_insert_name.Text.Trim() + "'," +
                "N'" + txt_Billing_Edite_Name.Text.Trim() + "') ");

            con.Open();
            cmd = new SqlCommand(query, con); 
            cmd.ExecuteNonQuery();
            cmd.Parameters.AddWithValue("@ID", txt_id);
            cmd.Parameters.AddWithValue("@USER_NAME", txt_user_name);
            cmd.Parameters.AddWithValue("@BRANCH_NAME", cbo_branches);
            cmd.Parameters.AddWithValue("@STORE_NAME", cbo_store);
            cmd.Parameters.AddWithValue("@BOX_NAME", cbo_box);
            cmd.Parameters.AddWithValue("@CUSTOMER_RECIVE_BILLING", cbo_name_Recive_Invoice);
            cmd.Parameters.AddWithValue("@BILLING_DELEGET_NUM", txt_Delget_number);
            cmd.Parameters.AddWithValue("@CURENCY", cbo_Currencey);
            cmd.Parameters.AddWithValue("@Exchange_bill_number", txt_Exchange_bill_number);
            cmd.Parameters.AddWithValue("@METHOD_OF_TAX", cbo_Method_of_calculating_tax);
            cmd.Parameters.AddWithValue("@DELEGET_NAME", cbo_Deleget_Name);
            cmd.Parameters.AddWithValue("@TYPE_BILLING", cbo_Invoice_type);
            cmd.Parameters.AddWithValue("@TOTAL_DELEGET_BILLING", txt_Total_invoice_resource);
            cmd.Parameters.AddWithValue("@DELEGET_BILLING_DATE", date);
            cmd.Parameters.AddWithValue("@TOTAL_AMOUNT", txt_amount_of_product);
            cmd.Parameters.AddWithValue("@DISCOUNT_PERCENT", txt_Discount_percent);
            cmd.Parameters.AddWithValue("@DISCOUNT_MONEY", txt_Discount_Money);
            cmd.Parameters.AddWithValue("@TOTAL_BEFORE_DISCOUNT", txt_Total_Brfore_Discount);
            cmd.Parameters.AddWithValue("@TOTAL_DISCOUNT", txt_total_Discount);
            cmd.Parameters.AddWithValue("@TOTAL_AFTER_DISCOUNT", Total_After_Discount);
            cmd.Parameters.AddWithValue("@THE_REST", txt_Down_payment);
            cmd.Parameters.AddWithValue("@DATE_TIME_NOW", Date_Time_Now);
            cmd.Parameters.AddWithValue("@DATE_EDITS", txt_Billing_last_Edits_date);
            cmd.Parameters.AddWithValue("@Billing_insert_name", txt_Billing_insert_name);
            cmd.Parameters.AddWithValue("@Billing_Edite_name", txt_Billing_Edite_Name);

            //Comandos(query);
            if (con.State == ConnectionState.Open) { con.Close(); }
            txt_msg_Billing.Text = "تم الحفظ بنجاح";
        }
        public Int32 Comandos(String sqlString)
        {
            con.Close();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sqlString;
            con.Open();
            try
            { return cmd.ExecuteNonQuery(); }
            catch (InvalidCastException e)
            {
                throw (e);
                //connection.Close();
            }
            finally
            { con.Close(); }
        }

        public void Update_billing()
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            
            string Query = "Update Tbl_billing set INT=@INT,USER_NAME=@USER_NAME,BRANCH_NAME=@BRANCH_NAME,STORE_NAME=@STORE_NAME," +
                "BOX_NAME=@BOX_NAME,BILLING_DELEGET_NUM=@BILLING_DELEGET_NUM,CURENCY=@CURENCY,Exchange_bill_number=@Exchange_bill_number," +
                "DELEGET_BILLING_DATE=@DELEGET_BILLING_DATE,TOTAL_AMOUNT=@TOTAL_AMOUNT," +
                "DISCOUNT_PERCENT=@DISCOUNT_PERCENT,DISCOUNT_MONEY=@DISCOUNT_MONEY,TOTAL_BEFORE_DISCOUNT=@TOTAL_BEFORE_DISCOUNT," +
                "TOTAL_DISCOUNT=@TOTAL_DISCOUNT,TOTAL_AFTER_DISCOUNT=@TOTAL_AFTER_DISCOUNT,PAY_FIRST=@PAY_FIRST," +
                "THE_REST=@THE_REST,DATE_TIME_NOW=@DATE_TIME_NOW,DATE_EDITS=@DATE_EDITS,USER_EDITS=@USER_EDITS,DETAILS=@DETAILS where ID = @ID ";
            con.Open();
            Comandos(Query);
            if (con.State == ConnectionState.Open) { con.Close(); }
        }

        public void Fill_Billing(TextBox txt_parcode, TextBox txt_product_name)
        {
            con.Open();
            AutoCompleteStringCollection col_1 = new AutoCompleteStringCollection();
            AutoCompleteStringCollection col_2 = new AutoCompleteStringCollection();

            string Query = "SELECT * FROM TBL_product ";
            cmd = new SqlCommand(Query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                col_1.Add(dr.GetValue(4).ToString());
                col_2.Add(dr.GetValue(5).ToString());
            }
            txt_parcode.AutoCompleteCustomSource = col_1;
            txt_product_name.AutoCompleteCustomSource = col_2;

            dr.Close();
            con.Close();
        }

        public void Fill_Billing(TextBox txt_parcode)
        {
            con.Open();
            AutoCompleteStringCollection col_1 = new AutoCompleteStringCollection();

            cmd = new SqlCommand("SELECT * FROM TBL_product ", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                col_1.Add(dr.GetString(4));
            }
            txt_parcode.AutoCompleteCustomSource = col_1;
            dr.Close();
            con.Close();
        }

        public Int32 maxid()
        {
            
            con.Close();
            
            con.Open();
            cmd = new SqlCommand("Select max(ID) as fds From TBL_BILLING", con);
            cmd.ExecuteNonQuery();

            DataSet dsS = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dsS);

            int id;
            DataRow drS;
            drS = dsS.Tables[0].Rows[0];
            id = Convert.ToInt32(drS["fds"].ToString());

            id++;
            return id;
        }

    }
}
