using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SALES
{
    public class Fill_Cbo
    {
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");

        public void ClearAll()
        {
            //foreach (Control ctrl in this.Controls)
            //{
            //    if (ctrl is TextBox) 
            //    { ctrl.Text = string.Empty; }
            //}
        }

        public void UPDATE_QUANTITY(decimal Finish2, string prod_name)  //   frm_invoice.Close_Invoice();  خاص بدالة 
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string sql = "UPDATE TBL_products set Quantity = N'"+Finish2+ "' where PRODUCT_NAME = N'" + prod_name+ "' ";
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@Quantity", Finish2);
            }
            catch (Exception) { }
            finally { con.Close(); }
        }
        public void UPDATE_QUANTITY(string Finish,TextBox txt_par)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string sql = "UPDATE TBL_products set Quantity = N'" + Finish + "' where PARCODE = N'" + txt_par.Text.Trim() + "' ";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@Quantity", Finish);
            }
            catch (Exception)
            {
            }
            finally { con.Close(); }
        }
        public void fill_2product_tblByID(TextBox txt_parcode)
        {
            try
            {
                string sql = "SELECT PARCODE AS [ID] FROM TBL_PRODUCTS UNION ALL SELECT PARCODE_2 AS [ID] FROM NEW_PRODUCTS";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                AutoCompleteStringCollection col1 = new AutoCompleteStringCollection();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    col1.Add(dr.GetValue(0).ToString());
                }
                dr.Close(); 
                txt_parcode.AutoCompleteCustomSource = col1;
            } catch (Exception) { }
            finally { con.Close(); }
        }

        public void fill_2product_tblByName(TextBox txt_product_name)
        {
            try
            {
                string sql = "SELECT PRODUCT_NAME AS [ID] FROM TBL_PRODUCTS UNION ALL SELECT PRODUCT_NAME AS [ID] FROM NEW_PRODUCTS";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                AutoCompleteStringCollection col1 = new AutoCompleteStringCollection();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    col1.Add(dr.GetValue(0).ToString());
                }
                dr.Close();
                txt_product_name.AutoCompleteCustomSource = col1;
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        public void Fill_Product(ComboBox cbo_search_parcode, ComboBox cbo_name_search)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                //ClearAll();
                cbo_search_parcode.Items.Clear();
                cbo_name_search.Items.Clear();
                con.Open();
                SqlCommand Cmd = new SqlCommand("SELECT * FROM TBL_products", con);
                SqlDataReader Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    cbo_search_parcode.Items.Add(Reader["PARCODE"].ToString());    ///   BRANCH_NAME   هنا نغير اسم العمود المراد ظهوره
                    cbo_name_search.Items.Add(Reader["PRODUCT_NAME"].ToString());
                }
                Reader.Close();
            }
            catch (Exception){ }
            finally {  con.Close(); }
        }

        //public void Fill_Billing(TextBox txt_parcode, TextBox txt_product_name)
        //{
        //    if (con.State == ConnectionState.Open) { con.Close(); }

        //    con.Open();
        //    AutoCompleteStringCollection col_1 = new AutoCompleteStringCollection();
        //    AutoCompleteStringCollection col_2 = new AutoCompleteStringCollection();

        //    string Query = "SELECT PARCODE as[id],PRODUCT_NAME as[name] FROM TBL_products UNION ALL PARCODE_2 as[id],PRODUCT_NAME as[name] FROM NEW_PRODUCTS";
        //    cmd = new SqlCommand(Query, con);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        col_1.Add(dr.GetValue(0).ToString());
        //        col_2.Add(dr.GetValue(1).ToString());
        //    }
        //    txt_parcode.AutoCompleteCustomSource = col_1;
        //    txt_product_name.AutoCompleteCustomSource = col_2;

        //    dr.Close();
        //    con.Close();
        //}

        public void Fill_cbo_PRODUCT_NAME(ComboBox cbo_product_name_invoice)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string query = "SELECT * FROM TBL_products ";
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbo_product_name_invoice.Items.Add(dr["PRODUCT_NAME"].ToString());
                }
                dr.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
           
        }

        public void Fill_cbo_parcode(ComboBox cbo_parcode_invoice)
        {
            try
            {
                cbo_parcode_invoice.Items.Clear();
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string query = "SELECT * FROM TBL_products ";
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbo_parcode_invoice.Items.Add(dr["PARCODE"].ToString());
                }
                dr.Close();
            }
            catch (Exception)
            {

            }

            finally { con.Close(); }
        }

        public void fill_Categories(ComboBox cbo)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("SELECT * FROM TBL_CATEGORIES ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbo.Items.Add(dr["CATEGORY_NAME"].ToString());
                }
                dr.Close();
            }
            finally { con.Close(); }
            
        }

        public void fill_stores_Data(ComboBox cbo)
        {
            try
            {
                con.Close();
                cbo.Items.Clear();
                con.Open();
                cmd = new SqlCommand("Select * From TBL_STORES", con);
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                SqlDataReader dtr = cmd.ExecuteReader();
                while (dtr.Read())
                {
                    cbo.Items.Add(dtr["STORE_NAME"].ToString());
                }
                dtr.Close();
            }
            finally
            {
                con.Close();
            }
        }

        public void Fill_Branches(ComboBox cbo_branch_product)
        {
            try
            {
                cbo_branch_product.Items.Clear();
                con.Open();
                cmd = new SqlCommand(" Select * From TBL_BRANCH ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbo_branch_product.Items.Add(dr["BRANCH_NAME"].ToString());    ///   BRANCH_NAME  هنا نغير أسم العمود المراد ظهوره
                }
                dr.Close();
            }
            finally
            {
                con.Close();
            }   
         
        }

        public void Fill_Units(ComboBox cbo)
        {
            try
            {
                cbo.Items.Clear();
                ClearAll();
                con.Open();
                cmd = new SqlCommand("select * from tbl_unit", con);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cbo.Items.Add(dr["UNIT_NAME"].ToString());
                }
                dr.Close();
            }
            finally
            {
                con.Close();
            }
        }

        public void fill_company(ComboBox cbo)
        {
            try
            {
                cbo.Items.Clear();
                con.Open();
                cmd = new SqlCommand("SELECT * from TBL_COMPANY ", con);

                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cbo.Items.Add(dr["COMPANY_NAME"].ToString());
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void Fill_Users(ComboBox cbo_USER)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                cbo_USER.Items.Clear();
                ClearAll();
                con.Open();
                string querry = "SELECT * FROM TBL_USERS ";
                cmd = new SqlCommand(querry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbo_USER.Items.Add(dr["USER_NAME"].ToString());
                }
                dr.Close();
            }
            finally
            {
                con.Close();
            }
        }

        public void Fill_Customers(ComboBox cbo_Customer)
        {
            try
            {
                cbo_Customer.Items.Clear();
                ClearAll();
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Querry = "SELECT * FROM TBL_CUSTOMER";
                cmd = new SqlCommand(Querry, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbo_Customer.Items.Add(dr["CUSTOMER_NAME"].ToString());
                }
                dr.Close();
            }
            finally
            {
                con.Close();
            }
          
        }

        public void Fill_cbo_Customer_index_changed(ComboBox cbo_customer, TextBox txt_depit_invoice)
        {
            try
            {
                if (cbo_customer.SelectedIndex > -1)
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    string query = " Select CUSTOMER_STATUS_DEBT From TBL_CUSTOMER WHERE Customer_NAME = N'" + cbo_customer.Text.Trim() + "' ";
                    cmd = new SqlCommand(query, con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())

                        txt_depit_invoice.Text = dr["CUSTOMER_STATUS_DEBT"].ToString();

                    dr.Close();
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void Fill_Deleget(ComboBox cbo_Deleget_Name)
        {
            try
            {
                cbo_Deleget_Name.Items.Clear();
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                cmd = new SqlCommand("SELECT * FROM TBL_DELEGATES", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbo_Deleget_Name.Items.Add(dr["DELEGATE_NAME"].ToString());
                }
                dr.Close();
            }
            finally { con.Close(); }
        }

        public void Fill_Currency(ComboBox cbo)
        {
            try
            {
                if (con.State == ConnectionState.Open){con.Close();} con.Open();
                cmd = new SqlCommand("Select * from Tbl_Currency", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbo.Items.Add(dr["Currency_Name"].ToString());
                }
                dr.Close();
            } 
            finally { con.Close(); }
        }

        public void Fill_Billing(TextBox txt_parcode)
        {
            try
            {
                con.Open();
                AutoCompleteStringCollection col_1 = new AutoCompleteStringCollection();

                cmd = new SqlCommand("SELECT * FROM TBL_products", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    col_1.Add(dr.GetString(4));
                }
                txt_parcode.AutoCompleteCustomSource = col_1;
                dr.Close();
            }
            finally { con.Close(); }
        }

        public void Fill_Back_Billing(TextBox txt_billing_number)
        {
            try
            {
                con.Open();
                AutoCompleteStringCollection col_1 = new AutoCompleteStringCollection();

                cmd = new SqlCommand("SELECT * FROM TBL_BILLING_DETAILS", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    col_1.Add(dr.GetValue(10).ToString());              /// 10
                }
                txt_billing_number.AutoCompleteCustomSource = col_1;
                dr.Close();
            }
            finally { con.Close(); }
        }

        public void Fill_Back_Invoice(TextBox txt_Invoice_number , TextBox Invoice_Customer_NUM)
        {
            try
            {
                con.Open();
                AutoCompleteStringCollection col_1 = new AutoCompleteStringCollection();
                AutoCompleteStringCollection col_2 = new AutoCompleteStringCollection();
                cmd = new SqlCommand("SELECT * FROM TBL_ORDER", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    col_1.Add(dr.GetValue(0).ToString());  //ID_Order_Invoice
                    col_2.Add(dr.GetValue(1).ToString());
                }
                txt_Invoice_number.AutoCompleteCustomSource = col_1;
                Invoice_Customer_NUM.AutoCompleteCustomSource = col_2;
                dr.Close();
            }
            finally { con.Close(); }
        }

        public void Show_Billing_Details(TextBox txt_billing_number , DataGridView DGV_Billing)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                cmd = new SqlCommand("Select * from TBL_BILLING_DETAILS Where ID_BILLING = N'"+txt_billing_number.Text.Trim()+"' ", con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGV_Billing.DataSource = dt;
                //con.Close();
            } 
            finally { con.Close(); }
        }

        public void Show_Invoice_Details(TextBox txt_Invoice_num_2 , DataGridView DGV_Invoice)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                cmd = new SqlCommand("Select * from TBL_ORDER_DETAILS Where Order_id = N'" + txt_Invoice_num_2.Text.Trim() + "' ", con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGV_Invoice.DataSource = dt;
            } finally { con.Close(); }
        }

        public void Fill_txt_search_order(ComboBox cbo, string fld)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd = new SqlCommand("SELECT * FROM TBL_ORDER WHERE '" + fld + "' = N'" + cbo.Text.Trim() + "' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cbo.Text = dr[fld].ToString();
                }
                dr.Close();
            }

            finally { con.Close(); }
        }

        public void Select_TBL_PAY_RECEIPT(TextBox txt_search)                     // سند الصرف
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                string sql = "select * from TBL_PAY_RECEIPT ";
                cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    col.Add(dr.GetValue(0).ToString());
                }
                dr.Close();
                txt_search.AutoCompleteCustomSource = col;
               
            }
            finally { con.Close(); }
        }

        public void Select_TBL_CATCH_RECEIPT(TextBox txt_search)                     // سند القبض
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                string sql = "select * from TBL_CATCH_RECEIPT ";
                cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    col.Add(dr.GetValue(0).ToString());
                }
                dr.Close();
                txt_search.AutoCompleteCustomSource = col;
            }
            finally { con.Close(); }
        }

        public void UpdateP1_Parcode(string Finish, TextBox txt_product_parcode)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string sql = "UPDATE TBL_PRODUCTS set Quantity = N'" + Finish + "' where PARCODE = N'" + txt_product_parcode.Text.Trim() + "' ";
                con.Open();
                SqlCommand cmd_p1 = new SqlCommand(sql, con);
                cmd_p1.ExecuteNonQuery();
                cmd_p1.Parameters.AddWithValue("@Quantity", Finish);
            } 
            finally { con.Close(); }
        }

        public void UpdateP1_Fk(string Finish, TextBox id_fk)
        {
            try 
            { 
                if (con.State == ConnectionState.Open) { con.Close(); }
                string sql = "UPDATE TBL_PRODUCTS Set Quantity = N'" + Finish + "' Where ID = N'" + id_fk.Text.Trim() + "' ";
                con.Open();
                SqlCommand cmd_1 = new SqlCommand(sql, con);
                cmd_1.ExecuteNonQuery();
                cmd_1.Parameters.AddWithValue("@Quantity", Finish);
            } 
            finally { con.Close(); }
        }
        public void UpdateP1_Fk(object Qty, object id_fk)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string sql = "UPDATE TBL_PRODUCTS Set Quantity = N'" + Qty + "' Where ID = N'" + id_fk + "' ";
                con.Open();
                SqlCommand cmd_1 = new SqlCommand(sql, con);
                cmd_1.ExecuteNonQuery();
                cmd_1.Parameters.AddWithValue("@Quantity", Qty);
            }
            finally { con.Close(); }
        }
        public void UpdateP2_Parcode(decimal Finish, string prod_name)  
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string sql = " update  NEW_PRODUCTS set  QUANTITY = N'" + Finish + "' where PRODUCT_NAME = N'" + prod_name + "'  ";
                con.Open();
                SqlCommand cmd_tbl2 = new SqlCommand(sql, con);
                cmd_tbl2.Parameters.AddWithValue("@Quantity", Finish);
                cmd_tbl2.ExecuteNonQuery();
                //con.Close();

                //con.Open();
                //string sq2 = "UPDATE TBL_PRODUCTS SET Quantity =N'" + Finish + "' Where PARCODE = N'" + par_ + "'";
                //SqlCommand cmd_tbl1 = new SqlCommand(sq2, con);
                //cmd_tbl1.Parameters.AddWithValue("@Quantity", Finish);
                //cmd_tbl1.ExecuteNonQuery();
            }
            finally { con.Close(); }
        }
        public void UpdateP2_Parcode(string Finish, TextBox txt_product_name_invoice)  //  تحديث كل اسم بنفس الأسم
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string sql = "UPDATE NEW_PRODUCTS Set Quantity = N'" + Finish + "' Where PRODUCT_NAME = N'" + txt_product_name_invoice.Text.Trim() + "' ";
                con.Open();
                SqlCommand cmd_p2 = new SqlCommand(sql, con);
                cmd_p2.ExecuteNonQuery();
                cmd_p2.Parameters.AddWithValue("@Quantity", Finish);
            }
            finally { con.Close(); }
        }
        public void UpdateP2_Parcode(object Qty, object txt_product_name_invoice)  //  تحديث كل اسم بنفس الأسم
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string sql = "UPDATE NEW_PRODUCTS Set Quantity = N'" + Qty + "' Where PRODUCT_NAME = N'" + txt_product_name_invoice + "' ";
                con.Open();
                SqlCommand cmd_p2 = new SqlCommand(sql, con);
                cmd_p2.ExecuteNonQuery();
                cmd_p2.Parameters.AddWithValue("@Quantity", Qty);
            }
            finally { con.Close(); }
        }

        public void UpdateP2_Fk(string Finish, TextBox id_product)  // FORIEGN_KEY_ID تحديث جدول الاصناف الاضافية بناءا على رقم 
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string sql = "UPDATE NEW_PRODUCTS Set Quantity = N'" + Finish + "' Where FORIEGN_KEY_ID = N'" + id_product.Text.Trim() + "' ";
                con.Open();
                SqlCommand cmd_p = new SqlCommand(sql, con);
                cmd_p.ExecuteNonQuery();
                cmd_p.Parameters.AddWithValue("@Quantity", Finish);
            }
            finally { con.Close(); }
        }
    }
}
