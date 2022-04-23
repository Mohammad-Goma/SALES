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
    public partial class frm_Back_billing : Form
    {
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");

        Fill_Cbo fill_Cbo = new Fill_Cbo();

        public frm_Back_billing()
        {
            InitializeComponent();
        }

        public Int64 max()
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            int id = 0;
            con.Open();
            cmd = new SqlCommand("Select max(ID) as fds From TBL_Catch_Receipt", con);
            cmd.ExecuteNonQuery();
            DataSet dsS = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dsS);
            DataRow drS;
            drS = dsS.Tables[0].Rows[0];
            id = Convert.ToInt32(drS["fds"].ToString());
            id++;
            con.Close();
            return id;
        }

        private void frm_Back_billing_Load(object sender, EventArgs e)
        {
            try
            {
                txt_id.Text = Convert.ToString(max_id_TBL_Catch_Receipt());
                fill_Cbo.Fill_Back_Billing(ID_ORDER_AUTO);
                USER_NAME.Text = Main_Form.USER_NAME_STRING.Trim();

            } catch (Exception){}
        }

        private void btn_search_billing_Click(object sender, EventArgs e)
        {
            try
            {
                if (ID_ORDER_AUTO.Text.Trim() != "")
                {
                    AMOUNT.Text = "0";
                    TOTAL_BEFORE_DISCOUNT.Text = "0";
                    DISCOUNT.Text = "0";
                    TOTAL_AFTER_DISCOUNT.Text = "0";
                    fill_Cbo.Show_Billing_Details(ID_ORDER_AUTO, DGV_Billing);
                    Show_Billing_byID(ID_ORDER_AUTO);
                    if (DGV_Back_billing.HasChildren) 
                    { DGV_Back_billing.Rows.Clear(); }
                }
                else
                if (BILLING_DELEGET_NUM.Text.Trim() != "")
                {
                    AMOUNT.Text = "0";
                    TOTAL_BEFORE_DISCOUNT.Text = "0";
                    DISCOUNT.Text = "0";
                    TOTAL_AFTER_DISCOUNT.Text = "0";
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    cmd = new SqlCommand(" Select ID from TBL_BILLING Where BILLING_DELEGET_NUM = N'" + BILLING_DELEGET_NUM.Text.Trim() + "' ", con);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    txt_billing_num_2.Text = dt.Rows[0]["ID"].ToString();
                    con.Close();
                    if (DGV_Back_billing.HasChildren) 
                    { DGV_Back_billing.Rows.Clear(); }
                }
            } catch (Exception){ }
        }

        public void Show_Billing_byID(TextBox txt_billing_number)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                SqlCommand cmd = new SqlCommand(" Select TOTAL_AMOUNT,TOTAL_BEFORE_DISCOUNT,TOTAL_DISCOUNT,TOTAL_AFTER_DISCOUNT," +
                                            " PAY_FIRST,THE_REST,BILLING_DELEGET_NUM,Exchange_bill_number,DELEGET_NAME," +
                                            " DELEGET_BILLING_DATE from TBL_BILLING Where ID = N'" + txt_billing_number.Text.Trim() + "' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txt_amount_of_product.Text = dr["TOTAL_AMOUNT"].ToString();
                    txt_Total_Brfore_Discount.Text = dr["TOTAL_BEFORE_DISCOUNT"].ToString();
                    txt_total_Discount.Text = dr["TOTAL_DISCOUNT"].ToString();
                    Total_After_Discount_.Text = dr["TOTAL_AFTER_DISCOUNT"].ToString();
                    txt_Total_Last.Text = dr["TOTAL_AFTER_DISCOUNT"].ToString();
                    payment.Text = dr["PAY_FIRST"].ToString();
                    txt_The_rest.Text = dr["THE_REST"].ToString();
                    BILLING_DELEGET_NUM.Text = dr["BILLING_DELEGET_NUM"].ToString(); //
                    ID_PAY_RECEIPT.Text = dr["Exchange_bill_number"].ToString();
                    CUSTOMER_NAME.Text = dr["DELEGET_NAME"].ToString();
                    CUSTOMER_INVOICE_DATE.Text = dr["DELEGET_BILLING_DATE"].ToString();
                }

                dr.Close();
            }
            catch (Exception){ }
            finally{ con.Close(); }
        }

        public void Show_Billing_by_billing_deleget()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                cmd = new SqlCommand("Select ID from TBL_BILLING Where BILLING_DELEGET_NUM = N'" + BILLING_DELEGET_NUM.Text.Trim() + "' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ID_ORDER_AUTO.Text = dr["ID"].ToString();
                }
                dr.Close();
                con.Close();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void DGV_Billing_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                    DGV_Billing.Rows[e.RowIndex].Selected = true;
                    if (e.RowIndex >= 0)
                    {
                    txt_par.Text = DGV_Billing.SelectedRows[0].Cells["Parcode"].Value.ToString();
                    //Fill_txt_parcode(txt_par);
                    txt_parcode.Text = DGV_Billing.SelectedRows[0].Cells["Parcode"].Value.ToString();
                    txt_name.Text = DGV_Billing.SelectedRows[0].Cells["Product_Name"].Value.ToString();
                    txt_unit.Text = DGV_Billing.SelectedRows[0].Cells["UnitS"].Value.ToString();
                    txt_first_price.Text = DGV_Billing.SelectedRows[0].Cells["FIRST_COST"].Value.ToString();
                    txt_Price.Text = DGV_Billing.SelectedRows[0].Cells["Price"].Value.ToString();
                    //txt_quantity_store.Text = DGV_Billing.SelectedRows[0].Cells["LESS_PRICE"].Value.ToString();
                    txt_tax.Text = DGV_Billing.SelectedRows[0].Cells["Tax"].Value.ToString();
                    txt_qty.Text = DGV_Billing.SelectedRows[0].Cells["Qty"].Value.ToString();
                    txt_total_Above.Text = DGV_Billing.SelectedRows[0].Cells["Total"].Value.ToString();
                    
                    btn_Delete_Billing.Visible = false;
                    btn_Delete_Billing.Enabled = false;
                    btn_Delete_Billing.BackColor = Color.White;
                    btn_ADD_Billing.Visible = true;
                    btn_ADD_Billing.Enabled = true;
                }
            }
            catch (Exception) { }
        }

        private void DGV_Billing_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //DGV_Billing.Rows[e.RowIndex].Selected = true ;
        }

        private void btn_Delete_Billing_Click(object sender, EventArgs e)
        {
            try
            {

                Delete_Row();
            }
            catch (Exception)
            {
            }
        }

        private void btn_ADD_Billing_Click(object sender, EventArgs e)
        {
            try
            {
                decimal qty_store = Convert.ToDecimal(txt_quantity_store.Text.Trim());
                decimal total = Convert.ToDecimal(txt_total_Above.Text.Trim());
                decimal qty_ord = Convert.ToDecimal(txt_qty.Text.Trim()); 
                decimal finish_ = Convert.ToDecimal(txt_Finish.Text.Trim());
                if (txt_par.Text.Trim() == "") { MessageBox.Show("من فضلك اختر الصنف المراد اضافته"); return; }
                bool found = false;
                if (DGV_Back_billing.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dtgr in DGV_Back_billing.Rows)
                    {
                        if (dtgr.Cells["PARCODE2"].Value.ToString() == txt_par.Text.Trim())
                        {
                            dtgr.Cells["Qty2"].Value = (qty_ord + Convert.ToDecimal(dtgr.Cells["Qty2"].Value));
                            dtgr.Cells["Total2"].Value = (total + Convert.ToDecimal(dtgr.Cells["Total2"].Value));

                            decimal double_qty = qty_store + finish_;
                            string Finish = Convert.ToString(double_qty);
                            //fill_Cbo.UPDATE_QUANTITY(Finish, txt_par);
                            if (FK.Text.Trim() == "" && ID.Text.Trim() != "")
                            {
                                fill_Cbo.UpdateP1_Parcode(Finish, txt_par);
                                fill_Cbo.UpdateP2_Fk(Finish, ID);
                                ID.Text = "";
                                FK.Text = "";
                            }
                            else if (FK.Text.Trim() != "" && ID.Text.Trim() == "")
                            {
                                fill_Cbo.UpdateP2_Parcode(Finish, txt_name);
                                fill_Cbo.UpdateP1_Fk(Finish, FK);
                                ID.Text = "";
                                FK.Text = "";
                            }
                            AMOUNT.Text = (Convert.ToDecimal(AMOUNT.Text.Trim()) + qty_ord).ToString();
                            TOTAL_BEFORE_DISCOUNT.Text = (Convert.ToDecimal(TOTAL_BEFORE_DISCOUNT.Text.Trim()) + total).ToString();
                            clear();
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        insertintoDGV();
                        clear();
                    }
                }
                else
                {
                    insertintoDGV();
                     clear();
                }
            }
            catch (Exception)
            {
            }
        }

        public void insertintoDGV()
        {
            try
            {
                decimal qty_store = Convert.ToDecimal(txt_quantity_store.Text.Trim());
                string parcode = txt_par.Text.Trim();
                string product_name = txt_name.Text.Trim();
                string unit = txt_unit.Text.Trim();
                decimal first_price = Convert.ToDecimal(txt_first_price.Text.Trim());
                decimal price = Convert.ToDecimal(txt_Price.Text.Trim());
                //decimal less_price = Convert.ToDecimal(txt_lessprice.Text.Trim());
                decimal tax = Convert.ToDecimal(txt_tax.Text.Trim());
                decimal qty = Convert.ToDecimal(txt_qty.Text.Trim());
                decimal total = Convert.ToDecimal(txt_total_Above.Text.Trim());
                decimal finish_ = Convert.ToDecimal(txt_Finish.Text.Trim());
                decimal doubleqty = qty_store - finish_;
                string Finish = Convert.ToString(doubleqty);
                //fill_Cbo.UPDATE_QUANTITY(Finish, txt_par);
                if (FK.Text.Trim() == "" && ID.Text.Trim() != "")
                {
                    fill_Cbo.UpdateP1_Parcode(Finish, txt_par);
                    fill_Cbo.UpdateP2_Fk(Finish, ID);
                    ID.Text = "";
                    FK.Text = "";
                }
                else if (FK.Text.Trim() != "" && ID.Text.Trim() == "")
                {
                    fill_Cbo.UpdateP2_Parcode(Finish, txt_name);
                    fill_Cbo.UpdateP1_Fk(Finish, FK);
                    ID.Text = "";
                    FK.Text = "";
                }
                object[] row = { parcode, product_name, unit, first_price, price, tax, qty, total };
                DGV_Back_billing.Rows.Add(row);

                AMOUNT.Text = (Convert.ToDecimal(AMOUNT.Text.Trim()) + qty).ToString();
                TOTAL_BEFORE_DISCOUNT.Text = (Convert.ToDecimal(TOTAL_BEFORE_DISCOUNT.Text.Trim()) + total).ToString();
            } catch (Exception){ }
        }
        public void clear()
        {
            txt_par.Text = "";
            txt_name.Text = "";
            txt_par.Text = "";
            txt_name.Text = "";
            txt_unit.Text = "";
            txt_first_price.Text = "";
            txt_Price.Text = "";
            txt_quantity_store.Text = "";
            txt_tax.Text = "";
            txt_qty.Text = "";
            txt_total_Above.Text = "";
        }

        private void DGV_Back_billing_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DGV_Back_billing.Rows[e.RowIndex].Selected = true;
                if (e.RowIndex >= 0)
                {
                    txt_par.Text = DGV_Back_billing.SelectedRows[0].Cells["Parcode2"].Value.ToString();
                    //Fill_txt_parcode(txt_par);
                    txt_parcode.Text = DGV_Back_billing.SelectedRows[0].Cells["Parcode2"].Value.ToString();
                    txt_name.Text = DGV_Back_billing.SelectedRows[0].Cells["Product_Name2"].Value.ToString();
                    txt_unit.Text = DGV_Back_billing.SelectedRows[0].Cells["Units2"].Value.ToString();
                    txt_first_price.Text = DGV_Back_billing.SelectedRows[0].Cells["First_Price2"].Value.ToString();
                    txt_Price.Text = DGV_Back_billing.SelectedRows[0].Cells["Price2"].Value.ToString();
                    //txt_quantity_store.Text = DGV_Back_billing.SelectedRows[0].Cells[" "].Value.ToString();
                    txt_tax.Text = DGV_Back_billing.SelectedRows[0].Cells["Tax2"].Value.ToString();
                    txt_qty.Text = DGV_Back_billing.SelectedRows[0].Cells["Qty2"].Value.ToString();
                    txt_total_Above.Text= DGV_Back_billing.SelectedRows[0].Cells["Total2"].Value.ToString();
                    btn_Delete_Billing.Visible = true;
                    btn_Delete_Billing.Enabled = true;
                    btn_Delete_Billing.BackColor = Color.Firebrick;
                    btn_ADD_Billing.Visible = false;
                    btn_ADD_Billing.Enabled = false;
                }
            }
            catch (Exception){ }
        }

        private void txt_parcode_TextChanged(object sender, EventArgs e)
        {
            Fill_txt_parcode(txt_parcode);
        }
        public void Fill_txt_parcode(TextBox txt_parcode)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID,Quantity,Items_Number FROM TBL_products WHERE PARCODE = N'" + txt_parcode.Text.Trim() + "' ", con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    //txt_name.Text = dt.Rows[0]["PRODUCT_NAME"].ToString();
                    //txt_unit.Text = dt.Rows[0]["UNITS"].ToString();
                    //txt_first_price.Text = dt.Rows[0]["INITIAL_COST"].ToString();
                    //txt_Price.Text = dt.Rows[0]["PRICE"].ToString();
                    txt_quantity_store.Text = dt.Rows[0]["Quantity"].ToString();
                    items_number.Text = dt.Rows[0]["Items_Number"].ToString();
                    ID.Text = dt.Rows[0]["ID"].ToString(); 
                    FK.Text = "";
                     //txt_lessprice.Text = dt.Rows[0]["LESS_PRICE"].ToString();
                     //txt_tax.Text = dt.Rows[0]["TAXING"].ToString();
                     //txt_qty.Text = "1";
                    btn_Delete_Billing.Enabled = false;
                    btn_Delete_Billing.BackColor = Color.White;
                    //}
                    //dr.Close();
                    con.Close();
                }
                else
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    SqlCommand cmd_ = new SqlCommand("SELECT Quantity,Items_Number,FORIEGN_KEY_ID FROM NEW_PRODUCTS WHERE PARCODE_2 = N'" + txt_parcode.Text.Trim() + "' ", con);
                    cmd_.ExecuteNonQuery();
                    SqlDataAdapter da_ = new SqlDataAdapter(cmd_);
                    DataTable dt_ = new DataTable();
                    da_.Fill(dt_);
                    if (dt_.Rows.Count > 0)
                    {
                        txt_quantity_store.Text = dt_.Rows[0]["Quantity"].ToString();
                        items_number.Text = dt_.Rows[0]["Items_Number"].ToString();
                        FK.Text = dt_.Rows[0]["FORIEGN_KEY_ID"].ToString();
                        ID.Text = "";
                        btn_Delete_Billing.Enabled = false;
                        btn_Delete_Billing.BackColor = Color.White;
                        con.Close();
                    }
                }
                //txt_par.Text = dt.Rows[0]["PARCODE"].ToString();
            } catch (Exception){  }
            finally { if (con.State == ConnectionState.Open) { con.Close(); } }
        }

        private void txt_billing_number_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ID_ORDER_AUTO.Text.Trim() == "") { ID_ORDER_AUTO.Text = "0"; txt_par.Text = ""; txt_name.Text = ""; txt_first_price.Text = ""; txt_Price.Text = ""; txt_quantity_store.Text = ""; txt_tax.Text = ""; txt_qty.Text = ""; txt_total_Above.Text = ""; } txt_billing_num_2.Text = ID_ORDER_AUTO.Text.Trim(); if (ID_ORDER_AUTO.Text.Trim() == "0") { ID_ORDER_AUTO.SelectAll(); }
            }
            catch (Exception) { }
         }

        private void txt_qty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) && e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
            catch (Exception)
            {

               
            }
          
        }

        private void txt_discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) && e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
            catch (Exception)
            {


            }
        }

        private void txt_billing_number_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) && e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
            catch (Exception){ }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) && e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }
            catch (Exception) { }
        }

        private void txt_discount_TextChanged(object sender, EventArgs e)
        {
            try{decimal total ; decimal discount ; if(DISCOUNT.Text.Trim() == "" || DISCOUNT.Text.Trim() == null || DISCOUNT.Text.Trim() == "0"){DISCOUNT.Text = "0"; DISCOUNT.SelectAll(); TOTAL_AFTER_DISCOUNT.Text = TOTAL_BEFORE_DISCOUNT.Text.Trim();}else if (DISCOUNT.Text.Trim() != "" || DISCOUNT.Text.Trim() != null || DISCOUNT.Text.Trim() != "0"){total = Convert.ToDecimal(TOTAL_BEFORE_DISCOUNT.Text.Trim()); discount = Convert.ToDecimal(DISCOUNT.Text.Trim()); TOTAL_AFTER_DISCOUNT.Text = Convert.ToString(total - discount);}}catch (Exception){ return; }
        }

        private void chk_billing_num_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_billing_num.Checked)
            {
                BILLING_DELEGET_NUM.Text = "";
                chk_Deleget.CheckState = CheckState.Unchecked;
                ID_ORDER_AUTO.ReadOnly = false;
                chk_Deleget.Checked = false;
                BILLING_DELEGET_NUM.ReadOnly = true;

            }else 
            if (chk_billing_num.CheckState == CheckState.Unchecked)
            {
                ID_ORDER_AUTO.ReadOnly = true;
            }
        }

        private void chk_Deleget_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_Deleget.Checked)
                {
                    ID_ORDER_AUTO.Text = "";
                    chk_billing_num.CheckState = CheckState.Unchecked;
                    ID_ORDER_AUTO.ReadOnly = true;
                    chk_Deleget.Checked = true;
                    BILLING_DELEGET_NUM.ReadOnly = false;
                }
                else
                if (chk_Deleget.CheckState == CheckState.Unchecked)
                { 
                    BILLING_DELEGET_NUM.ReadOnly = true;
                }
            } catch (Exception){ return; }
        }

        private void txt_billing_num_2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Show_Billing_byID(ID_ORDER_AUTO);
                fill_Cbo.Show_Billing_Details(ID_ORDER_AUTO, DGV_Billing);
                if (DGV_Back_billing.HasChildren) { Clear_DGV_Back_billing(); }
            } catch (Exception){ }
        }

        public void Save()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string sql = "Insert Into [dbo].[TBL_Catch_Receipt] Values (N'"+ ID_ORDER_AUTO.Text.Trim() + "',N'"+ ID_ORDER_HAND.Text.Trim() + "'," +
                    "N'"+ CUSTOMER_NAME.Text.Trim() + "',N'"+ FROM_COUNT.Text.Trim() + "',N'"+ CUSTOMER_INVOICE_DATE.Text.Trim() + "'," +
                    "N'"+ ID_PAY_RECEIPT.Text.Trim() + "',N'"+ USER_NAME.Text.Trim() + "',N'"+ DATE_NOW.Text.Trim() + "'," +
                    "N'"+ AMOUNT.Text.Trim() + "',N'"+ TOTAL_BEFORE_DISCOUNT.Text.Trim() + "',N'"+ DISCOUNT.Text.Trim() + "'," +
                    "N'"+ TOTAL_AFTER_DISCOUNT.Text.Trim() + "',N'"+ Cash.Text.Trim() + "',N'"+ Cheek_Total.Text.Trim() + "'," +
                    "N'"+ cheek_num.Text.Trim() + "',N'"+ total_rest.Text.Trim() + "',N'"+ currency.Text.Trim() + "'," +
                    "N'"+ Date_Update.Text.Trim() + "',N'"+ User_Update.Text.Trim() + "')";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@ID_ORDER_AUTO", ID_ORDER_AUTO.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_ORDER_HAND", ID_ORDER_HAND.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_PAY_RECEIPT", ID_PAY_RECEIPT.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_NAME", CUSTOMER_NAME.Text.Trim());
                cmd.Parameters.AddWithValue("@FROM_COUNT", FROM_COUNT.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_INVOICE_DATE", CUSTOMER_INVOICE_DATE.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_NAME", USER_NAME.Text.Trim());
                cmd.Parameters.AddWithValue("@DATE_NOW", DATE_NOW.Text.Trim());
                cmd.Parameters.AddWithValue("@AMOUNT", AMOUNT.Text.Trim());
                cmd.Parameters.AddWithValue("@TOTAL_BEFORE_DISCOUNT", TOTAL_BEFORE_DISCOUNT.Text.Trim());
                cmd.Parameters.AddWithValue("@DISCOUNT", DISCOUNT.Text.Trim());
                cmd.Parameters.AddWithValue("@TOTAL_AFTER_DISCOUNT", TOTAL_AFTER_DISCOUNT.Text.Trim());
                cmd.Parameters.AddWithValue("@Cash", Cash.Text.Trim());
                cmd.Parameters.AddWithValue("@Cheek_Total", Cheek_Total.Text.Trim());
                cmd.Parameters.AddWithValue("@cheek_num", cheek_num.Text.Trim());
                cmd.Parameters.AddWithValue("@total_rest", total_rest.Text.Trim());
                cmd.Parameters.AddWithValue("@currency", currency.Text.Trim());
                cmd.Parameters.AddWithValue("@Date_Update", Date_Update.Text.Trim());
                cmd.Parameters.AddWithValue("@User_Update", User_Update.Text.Trim());
            } catch (Exception) { }
            finally { con.Close(); }
        }

        public void SAVE2()
        {
            //try
            //{
            //    if (con.State == ConnectionState.Open) { con.Close(); }
            //    con.Open();
            //    string sql = "Insert into TBL_Catch_Receipt Values(N'" + txt_billing_num_2.Text.Trim() + "',N'" + BILLING_DELEGET_NUM.Text.Trim() + "'," +
            //        "N'" + CUSTOMER_NAME.Text.Trim() + "',N'" + DELEGET_BILLING_DATE.Text.Trim() + "',N'" + ID_PAY_RECEIPT.Text.Trim() + "'," +
            //        "N'" + txt_Catch_Hand.Text.Trim() + "',N'" + AMOUNT.Text.Trim() + "'," +
            //        "N'" + TOTAL_BEFORE_DISCOUNT.Text.Trim() + "',N'" + DISCOUNT.Text.Trim() + "',N'" + TOTAL_AFTER_DISCOUNT.Text.Trim() + "')";
            //    SqlCommand cmd = new SqlCommand(sql, con);
            //    cmd.ExecuteNonQuery();
            //    cmd.Parameters.AddWithValue("@ID_billing", txt_billing_num_2.Text.Trim());
            //    //  ID_ORDER_AUTO
            //    //  ID_ORDER_HAND
            //    //  CUSTOMER_NAME
            //    //  CUSTOMER_INVOICE_DATE
            //    //  ID_PAY_RECEIPT
            //    //  USER_NAME
            //    //  DATE_NOW
            //    cmd.Parameters.AddWithValue("@ID_ORDER_AUTO", BILLING_DELEGET_NUM.Text.Trim());
            //    cmd.Parameters.AddWithValue("@CUSTOMER_NAME", CUSTOMER_NAME.Text.Trim());
            //    //  FROM_COUNT
            //    cmd.Parameters.AddWithValue("@CUSTOMER_INVOICE_DATE", DELEGET_BILLING_DATE.Text.Trim());
            //    cmd.Parameters.AddWithValue("@ID_Exchange_num", ID_PAY_RECEIPT.Text.Trim());
            //    cmd.Parameters.AddWithValue("@ID_Catch_Receipt", txt_Catch_Hand.Text.Trim());
            //    cmd.Parameters.AddWithValue("@Amount", AMOUNT.Text.Trim());
            //    cmd.Parameters.AddWithValue("@Total_Before_Discount", TOTAL_BEFORE_DISCOUNT.Text.Trim());
            //    cmd.Parameters.AddWithValue("@Discount", DISCOUNT.Text.Trim());
            //    cmd.Parameters.AddWithValue("@Total_After_Discount", TOTAL_AFTER_DISCOUNT.Text.Trim());
            //    //  Cash
            //    //  Cheek_Total
            //    //  cheek_num
            //    //  total_rest
            //    //  currency
            //    //  Date_Update
            //    //  User_Update
            //}
            //catch (Exception){  }
            //finally { con.Close(); }
        }

        public void Insert_Back_billing_Details()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string Query = "INSERT INTO TBL_Back_BILLING_DETAILS (PARCODE_2,PRODUCT_NAME_2,UNITS_2,FIRST_COST_2,PRICE_2,TAX_2,QTY_2,TOTAL_2,ID_BILLING_2) VALUES(@PARCODE_2,@PRODUCT_NAME_2,@UNITS_2,@FIRST_COST_2,@PRICE_2,@TAX_2,@QTY_2,@TOTAL_2,@ID_BILLING_2)";
                con.Open();
                for (int i = 0; i < DGV_Billing.Rows.Count; i++)
                {
                    cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@PARCODE_2", DGV_Back_billing.Rows[i].Cells["PARCODE2"].Value.ToString());
                    cmd.Parameters.AddWithValue("@PRODUCT_NAME_2", DGV_Back_billing.Rows[i].Cells["Product_Name2"].Value.ToString());
                    cmd.Parameters.AddWithValue("@UNITS_2", DGV_Back_billing.Rows[i].Cells["UNITS2"].Value.ToString());
                    cmd.Parameters.AddWithValue("@FIRST_COST_2", DGV_Back_billing.Rows[i].Cells["First_Price2"].Value.ToString());
                    cmd.Parameters.AddWithValue("@PRICE_2", DGV_Back_billing.Rows[i].Cells["Price2"].Value.ToString());
                    cmd.Parameters.AddWithValue("@TAX_2", DGV_Back_billing.Rows[i].Cells["Tax2"].Value.ToString());
                    cmd.Parameters.AddWithValue("@QTY_2", DGV_Back_billing.Rows[i].Cells["Qty2"].Value.ToString());
                    cmd.Parameters.AddWithValue("@TOTAL_2", DGV_Back_billing.Rows[i].Cells["Total2"].Value.ToString());
                    cmd.Parameters.AddWithValue("@ID_BILLING_2", txt_id.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch (Exception ) { }
        }

        private void btn_save_Catch_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                Insert_Back_billing_Details();
                while (DGV_Billing.Rows.Count > 0)
                {
                    DGV_Billing.Rows.RemoveAt(0);
                }
                while (DGV_Back_billing.Rows.Count > 0)
                {
                    DGV_Back_billing.Rows.RemoveAt(0);
                }
                ID_ORDER_AUTO.Text = "";
                BILLING_DELEGET_NUM.Text = "";
                CUSTOMER_NAME.Text = "";
                CUSTOMER_INVOICE_DATE.Value = DATE_NOW.Value;
                ID_PAY_RECEIPT.Text = "";
                txt_Catch_Auto.Text = "";
                txt_Catch_Hand.Text = "";
                txt_amount_of_product.Text = "";
                txt_Total_Brfore_Discount.Text = "";
                txt_total_Discount.Text = "";
                Total_After_Discount_.Text = "";
                txt_Total_Last.Text = "";
                payment.Text = "";
                txt_The_rest.Text = "";
                txt_par.Text = "";
                txt_name.Text = "";
                txt_unit.Text = "";
                txt_first_price.Text = "";
                txt_Price.Text = "";
                txt_quantity_store.Text = "";
                txt_tax.Text = "";
                txt_qty.Text = "";
                txt_total_Above.Text = "";
                AMOUNT.Text = "0";
                TOTAL_BEFORE_DISCOUNT.Text = "0";
                DISCOUNT.Text = "0";
                TOTAL_AFTER_DISCOUNT.Text = "0";
                Cash.Text = "";
                Cheek_Total.Text = "";
                currency.Text = "";
                User_Update.Text = "";
                txt_id.Text = Convert.ToString(max());
                fill_Cbo.Fill_Back_Billing(ID_ORDER_AUTO);
                USER_NAME.Text = Main_Form.USER_NAME_STRING.Trim();
                MessageBox.Show("تم الحفظ");
            } catch (Exception) { }
        }

        private void txt_id_TextChanged(object sender, EventArgs e)
        {
            txt_Catch_Auto.Text = txt_id.Text.Trim();
        }

        private void BILLING_DELEGET_NUM_TextChanged(object sender, EventArgs e)
        {
            //txt_billing_num_2.Text = BILLING_DELEGET_NUM.Text.Trim();
        }

        public Int64 max_id_TBL_Catch_Receipt()
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            Int64 id = 0;
            con.Open();
            cmd = new SqlCommand("SELECT MAX(ID) AS fds FROM TBL_Catch_Receipt", con); // Select max(ID_Customer) as fds From TBL_CUSTOMER
            cmd.ExecuteNonQuery();

            DataSet ds = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(ds);

            DataRow dr;
            dr = ds.Tables[0].Rows[0];
            id = Convert.ToInt32(dr["fds"].ToString());

            id++;
            con.Close();
            return id;
        }

        public void Clear_DGV_Back_billing()
        {
            try
            {
                for (int i = 0; i < DGV_Back_billing.Rows.Count; i++)
                {
                    while (DGV_Back_billing.HasChildren)
                    {
                        //txt_parcode.Text = txt_par.Text.Trim();
                        txt_par.Text = DGV_Back_billing.SelectedRows[0].Cells["PARCODE2"].Value.ToString();
                        txt_name.Text = DGV_Back_billing.SelectedRows[0].Cells["Product_Name2"].Value.ToString();
                        txt_unit.Text = DGV_Back_billing.SelectedRows[0].Cells["UNITS2"].Value.ToString();
                        txt_first_price.Text = DGV_Back_billing.SelectedRows[0].Cells["First_Price2"].Value.ToString();
                        txt_Price.Text = DGV_Back_billing.SelectedRows[0].Cells["Price2"].Value.ToString();
                        txt_tax.Text = DGV_Back_billing.SelectedRows[0].Cells["Tax2"].Value.ToString();
                        txt_qty.Text = DGV_Back_billing.SelectedRows[0].Cells["Qty2"].Value.ToString();
                        txt_total_Above.Text = DGV_Back_billing.SelectedRows[0].Cells["Total2"].Value.ToString();
                        Delete_Row();
                    }
                }
            }
            catch (Exception)
            {

            }
           
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txt_id.Text == Convert.ToString(max_id_TBL_Catch_Receipt()))
                //{
                    if (MessageBox.Show("انتبة ربما تحذف الفاتورة","تنبيه بالحذف",MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        Clear_DGV_Back_billing();
                        this.Close();
                    }
                    else { return; }
                //}
                //else { this.Close(); }
            }
            catch (Exception ) {  }
        }

        public void Delete_Row()
        {
            try
            {
                decimal qty_ord = Convert.ToDecimal(txt_qty.Text.Trim());
                decimal qty_store = Convert.ToDecimal(txt_quantity_store.Text.Trim());
                decimal finish_ = Convert.ToDecimal(txt_Finish.Text.Trim());
                decimal doubleqty = qty_store + finish_;
                string Finish = Convert.ToString(doubleqty);
                //fill_Cbo.UPDATE_QUANTITY(Finish, txt_par);
                if (FK.Text.Trim() == "" && ID.Text.Trim() != "")
                {
                    fill_Cbo.UpdateP1_Parcode(Finish, txt_par);
                    fill_Cbo.UpdateP2_Fk(Finish, ID);
                    ID.Text = "";
                    FK.Text = "";
                }
                else if (FK.Text.Trim() != "" && ID.Text.Trim() == "")
                {
                    fill_Cbo.UpdateP2_Parcode(Finish, txt_name);
                    fill_Cbo.UpdateP1_Fk(Finish, FK);
                    ID.Text = "";
                    FK.Text = "";
                }
                var rowindex = DGV_Back_billing.CurrentCell.RowIndex;
                DGV_Back_billing.Rows.RemoveAt(rowindex);

                decimal qty = Convert.ToDecimal(txt_qty.Text.Trim());
                decimal total = Convert.ToDecimal(txt_total_Above.Text.Trim());
                AMOUNT.Text = (Convert.ToDecimal(AMOUNT.Text.Trim()) - qty).ToString();
                TOTAL_BEFORE_DISCOUNT.Text = (Convert.ToDecimal(TOTAL_BEFORE_DISCOUNT.Text.Trim()) - total).ToString();

                txt_par.Text = "";
                txt_name.Text = "";
                txt_par.Text = "";
                txt_name.Text = "";
                txt_unit.Text = "";
                txt_first_price.Text = "";
                txt_Price.Text = "";
                txt_quantity_store.Text = "";
                txt_tax.Text = "";
                txt_qty.Text = "";
                txt_total_Above.Text = "";
                btn_Delete_Billing.Enabled = false;
                btn_Delete_Billing.BackColor = Color.White;
            }
            catch (Exception) { }
        }

        //public void UPDATE_QUANTITY(string Finish)
        //{
        //        try
        //        {
        //            if (con.State == ConnectionState.Open) { con.Close(); }
        //            string sql = "UPDATE TBL_PRODUCT set Quantity = N'" + Finish + "' where PARCODE = N'" + txt_quantity_store.Text.Trim() + "' ";
        //            con.Open();
        //            cmd = new SqlCommand(sql, con);
        //            cmd.ExecuteNonQuery();
        //            cmd.Parameters.AddWithValue("@Quantity", Finish);
        //            con.Close();
        //        } catch (Exception){ }
        //}

        private void DGV_Back_billing_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void txt_par_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txt_parcode.Text = txt_par.Text.Trim();
            }
            catch (Exception)
            {

            } 
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_qty.Text.Trim() == " "){txt_qty.Text = "1"; txt_qty.SelectAll(); } decimal qty_; if (txt_qty.Text.Trim()=="") { qty_ = 0; } else { qty_ = Convert.ToDecimal(txt_qty.Text.Trim()); } decimal price_; if (txt_Price.Text.Trim() == "") {  price_ = 0; }  else { price_ = Convert.ToDecimal(txt_Price.Text.Trim()); }  decimal total_ = qty_ * price_ ; txt_total_Above.Text = total_.ToString(); decimal items_num; if (items_number.Text.Trim()=="") { items_num = 0; } else{items_num = Convert.ToDecimal(items_number.Text.Trim()); } txt_Finish.Text = Convert.ToString(qty_ * items_num);
            }
            catch (Exception) { }
        }

        private void items_number_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal qty_ = Convert.ToDecimal(txt_qty.Text.Trim()); decimal items_num = Convert.ToDecimal(items_number.Text.Trim()); txt_Finish.Text = Convert.ToString(qty_ * items_num);
            } catch (Exception) { }
        }

        private void frm_Back_billing_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select billing_back_open,billing_back_save,billing_back_delete,billing_back_new From USERS_PERMESSIONS Where USER_NAME = N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd22 = new SqlCommand(Query, con);
                SqlDataReader dr2 = cmd22.ExecuteReader();
                if (dr2.Read())
                {
                    if (dr2["billing_back_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr2["billing_back_save"].ToString() == "True") { btn_ADD_Billing.Enabled = true; } else { btn_ADD_Billing.Enabled = false; }
                    //if (dr2["invoice_back_delete"].ToString() == "True") { .Enabled = true; } else { .Enabled = false; }
                    //if (dr2["invoice_back_new"].ToString() == "True") { .Enabled = true; } else { .Enabled = false; }
                }
                dr2.Close();
            } catch (Exception) { }
            finally { con.Close(); }
        }

    }
}
