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
    public partial class frm_back_invoice : Form
    {
        SqlCommand cmd;
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        Fill_Cbo fill = new Fill_Cbo();

        public frm_back_invoice()
        {
            InitializeComponent();
        }

        private void frm_back_invoice_Load(object sender, EventArgs e)
        {
            USER_NAME.Text = Main_Form.USER_NAME_STRING.Trim();
            fill.Fill_Back_Invoice(ID_ORDER_AUTO, ID_ORDER_HAND);
            txt_id.Text = Convert.ToString(max_id_TBL_PAY_RECEIPT());
        }

        private void chk_billing_num_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Invoice_num.Checked)
            {   
                ID_ORDER_HAND.Text = "";
                chk_Invoice.CheckState = CheckState.Unchecked;
                ID_ORDER_AUTO.ReadOnly = false;                                            
                chk_Invoice.Checked = false;
                ID_ORDER_HAND.ReadOnly = true;
            }
            else
            if (chk_Invoice_num.CheckState == CheckState.Unchecked)
            {
                ID_ORDER_AUTO.ReadOnly = true;
            }
        }
        private void chk_Deleget_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_Invoice.Checked)
                {
                    ID_ORDER_AUTO.Text = "";
                    chk_Invoice_num.CheckState = CheckState.Unchecked;
                    ID_ORDER_AUTO.ReadOnly = true;
                    chk_Invoice.Checked = true;
                    ID_ORDER_HAND.ReadOnly = false;
                }
                else
                if (chk_Invoice.CheckState == CheckState.Unchecked)
                {
                    ID_ORDER_HAND.ReadOnly = true;
                }
            }
            catch (Exception) { return; }
        }

        private void txt_billing_num_2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Show_Invoice_byID(txt_Invoice_num_2);
                fill.Show_Invoice_Details(txt_Invoice_num_2 , DGV_Invoice);
                if (DGV_Back_Invoice.HasChildren) { Clear_DGV_Back_Invoice(); }
            }
            catch (Exception){ }
        }

        public void Show_Invoice_byID(TextBox txt_Invoice_num_2)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                cmd = new SqlCommand("Select * from TBL_ORDER Where ID = N'"+txt_Invoice_num_2.Text.Trim()+"' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txt_amount_of_product.Text = dr["Amount_of_product"].ToString();
                    txt_Total_Brfore_Discount.Text = dr["TOTAL_BEFORE_REST"].ToString();
                    txt_total_Discount.Text = dr["Discount_value"].ToString();
                    Total_After_Discount_.Text = dr["TOTAL"].ToString();
                    txt_Total_Last.Text = dr["TOTAL"].ToString();
                    payment.Text = dr["PAYING"].ToString();
                    txt_The_rest.Text = dr["The_rest"].ToString();
                    ID_ORDER_AUTO.Text = dr["ID"].ToString();
                    //Invoice_Customer_NUM.Text = dr["ID_Order_Invoice"].ToString();
                    CUSTOMER_NAME.Text = dr["CUSTOMER_NAME"].ToString();
                    CUSTOMER_INVOICE_DATE.Text = dr["DATE"].ToString();
                }
                dr.Close();
                con.Close();
            } catch (Exception ) { }
        }

        private void txt_Invoice_number_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ID_ORDER_AUTO.Text.Trim() == "") { txt_par.Text = "";  txt_name.Text = ""; txt_unit.Text = ""; txt_tax.Text = ""; txt_Price.Text = "";  txt_total_Above.Text = ""; txt_quantity_store.Text = ""; txt_Machine.Text = ""; } if (ID_ORDER_AUTO.Text.Trim() == "0") { ID_ORDER_AUTO.SelectAll(); } txt_Invoice_num_2.Text = ID_ORDER_AUTO.Text.Trim();
            } catch (Exception) { }
        }

        private void Invoice_Customer_NUM_TextChanged(object sender, EventArgs e)
        {
            txt_Invoice_num_2.Text = ID_ORDER_HAND.Text.Trim();
        }

        private void DGV_Invoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DGV_Invoice.Rows[e.RowIndex].Selected = true;
                if (e.RowIndex >= 0)
                {
                    txt_par.Text = DGV_Invoice.SelectedRows[0].Cells["Parcode"].Value.ToString();
                    Fill_txt_parcode(txt_par);
                    //txt_parcode.Text = DGV_Billing.SelectedRows[0].Cells["Parcode"].Value.ToString();
                    //txt_name.Text = DGV_Billing.SelectedRows[0].Cells["Product_Name"].Value.ToString();
                    //txt_unit.Text = DGV_Billing.SelectedRows[0].Cells["UnitS"].Value.ToString();
                    //txt_first_price.Text = DGV_Billing.SelectedRows[0].Cells["FIRST_COST"].Value.ToString();
                    //txt_Price.Text = DGV_Billing.SelectedRows[0].Cells["Price"].Value.ToString();
                    //txt_quantity_store.Text = DGV_Billing.SelectedRows[0].Cells["LESS_PRICE"].Value.ToString();
                    //txt_tax.Text = DGV_Invoice.SelectedRows[0].Cells["Tax"].Value.ToString();
                    txt_qty.Text = DGV_Invoice.SelectedRows[0].Cells["Qty"].Value.ToString();
                    txt_total_Above.Text = DGV_Invoice.SelectedRows[0].Cells["Total"].Value.ToString();
                    btn_Delete_Invoice.Visible = false;
                    btn_Delete_Invoice.Enabled = false;
                    btn_Delete_Invoice.BackColor = Color.White;
                    btn_ADD_Invoice.Visible = true;
                    btn_ADD_Invoice.Enabled = true;
                }
            } catch (Exception) { }
        }

        public void Fill_txt_parcode(TextBox txt_par)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                cmd = new SqlCommand("SELECT Quantity FROM TBL_product WHERE PARCODE = N'" + txt_par.Text.Trim() + "' ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                    //txt_par.Text = dr["PARCODE"].ToString();
                    //txt_name.Text = dr["PRODUCT_NAME"].ToString();
                    //txt_unit.Text = dr["UNITS"].ToString();
                    //txt_first_price.Text = dr["INITIAL_COST"].ToString();
                    //txt_Price.Text = dr["PRICE"].ToString();
                    txt_quantity_store.Text = dt.Rows[0]["Quantity"].ToString();
                    //txt_lessprice.Text = dr["LESS_PRICE"].ToString();
                    //txt_tax.Text = dr["TAXING"].ToString();
                    txt_qty.Text = "1";
                    btn_Delete_Invoice.Enabled = false;
                    btn_Delete_Invoice.BackColor = Color.White;
                con.Close();
            } catch (Exception) { }
        }

        private void btn_ADD_Invoice_Click(object sender, EventArgs e)
        {
            try
            {
                decimal qty_store = Convert.ToDecimal(txt_quantity_store.Text.Trim());
                decimal qty_ord = Convert.ToDecimal(txt_qty.Text.Trim());
                decimal total = Convert.ToDecimal(txt_total_Above.Text.Trim());
                decimal txtfinish = Convert.ToDecimal(txt_Finish.Text.Trim());

                if (txt_par.Text.Trim() == "") { MessageBox.Show("من فضلك أختر الصنف المراد إضافته"); return; }
                bool found = false;
                if (DGV_Back_Invoice.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dtgr in DGV_Back_Invoice.Rows)
                    {
                        if (dtgr.Cells["PARCODE_back"].Value.ToString() == txt_par.Text.Trim())
                        {
                            dtgr.Cells["Qty_back"].Value = (qty_ord + Convert.ToDecimal(dtgr.Cells["Qty_back"].Value));
                            dtgr.Cells["Total_back"].Value = (total + Convert.ToDecimal(dtgr.Cells["Total_back"].Value));

                            decimal double_qty = qty_store + txtfinish;
                            string Finish = Convert.ToString(double_qty);
                            if (FK.Text.Trim() == "" && ID.Text.Trim() != "")
                            {
                                fill.UpdateP1_Parcode(Finish, txt_par);
                                fill.UpdateP2_Fk(Finish, ID);
                                ID.Text = "";
                                FK.Text = "";
                            }
                            else if (FK.Text.Trim() != "" && ID.Text.Trim() == "")
                            {
                                fill.UpdateP2_Parcode(Finish, txt_name);
                                fill.UpdateP1_Fk(Finish, FK);
                                ID.Text = "";
                                FK.Text = "";
                            }
                            //fill.UPDATE_QUANTITY(Finish , txt_par);
                            AMOUNT.Text = (Convert.ToDecimal(AMOUNT.Text.Trim()) + qty_ord).ToString();
                            TOTAL_BEFORE_DISCOUNT.Text = (Convert.ToDecimal(TOTAL_BEFORE_DISCOUNT.Text.Trim()) + total).ToString();
                            Clear();
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        insertintoDGV();
                        Clear();
                    }
                }
                else
                {
                  insertintoDGV();
                  Clear();
                }
            }
            catch (Exception){ }
        }
        public void Clear()
        {
            txt_par.Text = "";
            txt_name.Text = "";
            txt_unit.Text = "";
            txt_Price.Text = "";
            txt_quantity_store.Text = "";
            txt_parcode.Text = "";                      // هنا يتم تنفيذ اجراء text_changed
            txt_tax.Text = "";
            txt_qty.Text = "";
            txt_total_Above.Text = "";
            txt_quantity_store.Text = "";
            txt_Machine.Text = "";
        }

        public void insertintoDGV()
        {
            try
            {
                string parcode = txt_par.Text.Trim();
                string product_name = txt_name.Text.Trim();
                string unit = txt_unit.Text.Trim();
                decimal qty_ord = Convert.ToDecimal(txt_qty.Text.Trim());
                decimal tax = Convert.ToDecimal(txt_tax.Text.Trim());
                decimal price = Convert.ToDecimal(txt_Price.Text.Trim());
                decimal total = Convert.ToDecimal(txt_total_Above.Text.Trim());
                decimal txtfinish = Convert.ToDecimal(txt_Finish.Text.Trim());
                if (txt_quantity_store.Text.Trim() == "") { txt_quantity_store.Text = "0"; }
                decimal qty_store = Convert.ToDecimal(txt_quantity_store.Text.Trim());
                string id_machine = txt_Machine.Text.Trim();

                decimal double_qty = qty_store + txtfinish;
                string Finish = Convert.ToString(double_qty);
                if (FK.Text.Trim() == "" && ID.Text.Trim() != "")
                {
                    fill.UpdateP1_Parcode(Finish, txt_par);
                    fill.UpdateP2_Fk(Finish, ID);
                    ID.Text = "";
                    FK.Text = "";
                }
                else if (FK.Text.Trim() != "" && ID.Text.Trim() == "")
                {
                    fill.UpdateP2_Parcode(Finish, txt_name);
                    fill.UpdateP1_Fk(Finish, FK);
                    ID.Text = "";
                    FK.Text = "";
                }

                object[] row = { parcode, product_name, unit, qty_ord, tax, price, total };
                DGV_Back_Invoice.Rows.Add(row);

                AMOUNT.Text = (Convert.ToDecimal(AMOUNT.Text.Trim()) + qty_ord).ToString();
                TOTAL_BEFORE_DISCOUNT.Text = (Convert.ToDecimal(TOTAL_BEFORE_DISCOUNT.Text.Trim()) + total).ToString();
            }
            catch (Exception){ }
        }

        private void btn_Delete_Invoice_Click(object sender, EventArgs e)
        {
            Delete_Row();
        }

        private void DGV_Back_Invoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DGV_Back_Invoice.Rows[e.RowIndex].Selected = true;
                if (e.RowIndex >= 0)
                {
                    txt_par.Text = DGV_Back_Invoice.SelectedRows[0].Cells["Parcode_2"].Value.ToString();
                    Fill_txt_parcode(txt_par);
                    //txt_parcode.Text = DGV_Back_billing.SelectedRows[0].Cells[0].Value.ToString();
                    //txt_name.Text = DGV_Back_billing.SelectedRows[0].Cells["Product_Name2"].Value.ToString();
                    //txt_unit.Text = DGV_Back_billing.SelectedRows[0].Cells["Units2"].Value.ToString();
                    //txt_first_price.Text = DGV_Back_billing.SelectedRows[0].Cells["First_Price2"].Value.ToString();
                    //txt_Price.Text = DGV_Back_billing.SelectedRows[0].Cells["Price2"].Value.ToString();
                    //txt_quantity_store.Text = DGV_Back_billing.SelectedRows[0].Cells["LESS_PRICE2"].Value.ToString();
                    txt_tax.Text = DGV_Back_Invoice.SelectedRows[0].Cells["Tax_2"].Value.ToString();
                    txt_qty.Text = DGV_Back_Invoice.SelectedRows[0].Cells["Qty_2"].Value.ToString();
                    txt_total_Above.Text = DGV_Back_Invoice.SelectedRows[0].Cells["Total_2"].Value.ToString();
                    btn_Delete_Invoice.Visible = true;
                    btn_Delete_Invoice.Enabled = true;
                    btn_Delete_Invoice.BackColor = Color.Firebrick;
                    btn_ADD_Invoice.Visible = false;
                    btn_ADD_Invoice.Enabled = false;
                }
            }
            catch (Exception) { }
        }

        private void DGV_Invoice_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                txt_par.Text = DGV_Invoice.SelectedRows[0].Cells["pARCODEDataGridViewTextBoxColumn"].Value.ToString();
                txt_name.Text = DGV_Invoice.SelectedRows[0].Cells["pRODUCTNAMEDataGridViewTextBoxColumn"].Value.ToString();
                txt_unit.Text = DGV_Invoice.SelectedRows[0].Cells["uNITDataGridViewTextBoxColumn"].Value.ToString();
                txt_qty.Text = DGV_Invoice.SelectedRows[0].Cells["qTYDataGridViewTextBoxColumn"].Value.ToString();
                txt_tax.Text = DGV_Invoice.SelectedRows[0].Cells["Taxing"].Value.ToString();
                txt_Price.Text = DGV_Invoice.SelectedRows[0].Cells["pRICEDataGridViewTextBoxColumn"].Value.ToString();
                txt_total_Above.Text = DGV_Invoice.SelectedRows[0].Cells["tOTALDataGridViewTextBoxColumn"].Value.ToString();
                txt_Machine.Text = DGV_Invoice.SelectedRows[0].Cells["orderidDataGridViewTextBoxColumn"].Value.ToString();
                txt_parcode.Text = DGV_Invoice.SelectedRows[0].Cells["pARCODEDataGridViewTextBoxColumn"].Value.ToString();
                btn_ADD_Invoice.Enabled = true;
                btn_ADD_Invoice.Visible = true;
                btn_Delete_Invoice.Enabled = false;
                btn_Delete_Invoice.Visible = false;
                btn_Delete_Invoice.BackColor = Color.MediumVioletRed;
            } catch (Exception) {  }
            finally{ }
        }

        private void txt_parcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string sql1 = "Select ID,Quantity,Items_Number from TBL_PRODUCTS where parcode = N'" + txt_parcode.Text.Trim() + "' ";
                SqlCommand cmd_ = new SqlCommand(sql1, con);
                cmd_.ExecuteNonQuery();
                SqlDataAdapter da_ = new SqlDataAdapter(cmd_);
                DataTable dt_ = new DataTable();
                da_.Fill(dt_);
                if (dt_.Rows.Count > 0)
                {
                    ID.Text = dt_.Rows[0]["ID"].ToString();
                    txt_quantity_store.Text = dt_.Rows[0]["Quantity"].ToString();
                    Items_Number.Text = dt_.Rows[0]["Items_Number"].ToString();
                    FK.Text = "";
                    con.Close();
                    return;
                }
                else 
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();                                                                         // QUANTITY,FORIEGN_KEY_ID
                    string sql3 = "Select QUANTITY,FORIEGN_KEY_ID,Items_Number from NEW_PRODUCTS Where PARCODE_2 = N'" + txt_parcode.Text.Trim() + "' ";
                    SqlCommand cmd3 = new SqlCommand(sql3, con);
                    cmd3.ExecuteNonQuery();
                    SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                    DataTable dt3 = new DataTable();
                    da3.Fill(dt3);
                    if (dt3.Rows.Count > 0)
                    {
                        txt_quantity_store.Text = dt3.Rows[0]["QUANTITY"].ToString(); 
                        Items_Number.Text = dt3.Rows[0]["Items_Number"].ToString();
                        FK.Text = dt3.Rows[0]["FORIEGN_KEY_ID"].ToString();
                        ID.Text = "";
                        con.Close();
                        return;
                    }
                    else { con.Close(); }
                }
            } catch (Exception){  }  
            finally {  if (con.State == ConnectionState.Open) { con.Close(); } }
        }
        private void DGV_Back_Invoice_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DGV_Back_Invoice.Rows[e.RowIndex].Selected = true;
                if (e.RowIndex >= 0)
                {
                    txt_par.Text = DGV_Back_Invoice.SelectedRows[0].Cells["PARCODE_back"].Value.ToString();
                    txt_parcode.Text = DGV_Back_Invoice.SelectedRows[0].Cells["PARCODE_back"].Value.ToString(); // fill_quantituy
                    txt_name.Text = DGV_Back_Invoice.SelectedRows[0].Cells["PRODUCT_NAME_back"].Value.ToString();
                    txt_unit.Text = DGV_Back_Invoice.SelectedRows[0].Cells["UNIT_back"].Value.ToString();
                    txt_qty.Text = DGV_Back_Invoice.SelectedRows[0].Cells["QTY_back"].Value.ToString();
                    txt_tax.Text = DGV_Back_Invoice.SelectedRows[0].Cells["Taxing_back"].Value.ToString();
                    txt_Price.Text = DGV_Back_Invoice.SelectedRows[0].Cells["price_back"].Value.ToString();
                    txt_total_Above.Text = DGV_Back_Invoice.SelectedRows[0].Cells["Total_back"].Value.ToString();
                    //txt_Machine.Text = DGV_Invoice.SelectedRows[0].Cells["orderidDataGridViewTextBoxColumn"].Value.ToString();
                    btn_ADD_Invoice.Enabled = false;
                    btn_ADD_Invoice.Visible = false;
                    btn_Delete_Invoice.Enabled = true;
                    btn_Delete_Invoice.Visible = true;
                    btn_Delete_Invoice.BackColor = Color.Red;
                }
            } catch (Exception) { }
            finally { }
        }

        private void btn_save_pay_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                Save_DGV_Invoice_Back();
                while (DGV_Invoice.Rows.Count > 0)
                {
                    DGV_Invoice.Rows.RemoveAt(0);
                }
                while (DGV_Back_Invoice.Rows.Count > 0)
                {
                    DGV_Back_Invoice.Rows.RemoveAt(0);
                }
                AMOUNT.Text = "";
                TOTAL_BEFORE_DISCOUNT.Text = "";
                DISCOUNT.Text = "";
                TOTAL_AFTER_DISCOUNT.Text = ""; 
                Clear();
                MessageBox.Show("تم الحفظ");

            } catch (Exception){}
        }

        public void Save()
        {
            try
            {
                con.Open();
                string Sql = "Insert Into TBL_PAY_RECEIPT Values(N'" + ID_ORDER_AUTO.Text.Trim() + "',N'" + ID_ORDER_HAND.Text.Trim() + "'," +
                    "N'" + CUSTOMER_NAME.Text.Trim() + "',N'" + FROM_COUNT.Text.Trim() + "',N'" + CUSTOMER_INVOICE_DATE.Text.Trim() + "'," +
                    "N'" + ID_PAY_RECEIPT.Text.Trim() + "',N'" + USER_NAME.Text.Trim() + "',N'" + DATE_NOW.Text.Trim() + "'," +
                    "N'" + AMOUNT.Text.Trim() + "',N'" + TOTAL_BEFORE_DISCOUNT.Text.Trim() + "',N'" + DISCOUNT.Text.Trim() + "'," +
                    "N'" + TOTAL_AFTER_DISCOUNT.Text.Trim() + "',N'" + Cash.Text.Trim() + "',N'" + Cheek_Total.Text.Trim() + "'," +
                    "N'" + cheek_num.Text.Trim() + "',N'" + total_rest.Text.Trim() + "',N'" + currency.Text.Trim() + "'," +
                    "N'" + Date_Update.Text.Trim() + "',N'" + User_Update.Text.Trim() + "' ) ";
                SqlCommand cmd = new SqlCommand(Sql, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@ID_ORDER_AUTO", ID_ORDER_AUTO.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_ORDER_HAND", ID_ORDER_HAND.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_NAME", CUSTOMER_NAME.Text.Trim());
                cmd.Parameters.AddWithValue("@FROM_COUNT", FROM_COUNT.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_INVOICE_DATE", CUSTOMER_INVOICE_DATE.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_PAY_RECEIPT", ID_PAY_RECEIPT.Text.Trim());
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
                cmd.Parameters.AddWithValue("@Date_Update", ID_ORDER_AUTO.Text.Trim());
                cmd.Parameters.AddWithValue("@User_Update", ID_ORDER_AUTO.Text.Trim());

                txt_id.Text = Convert.ToString(max_id_TBL_PAY_RECEIPT());
                USER_NAME.Text = "0";
                txt_amount_of_product.Text = "0";
                txt_total_Discount.Text = "0";
                txt_Total_Last.Text = "0";
                payment.Text = "0";
                txt_The_rest.Text = "0";
                AMOUNT.Text = "0";
                TOTAL_BEFORE_DISCOUNT.Text = "0";
                DISCOUNT.Text = "0";
                TOTAL_AFTER_DISCOUNT.Text = "0";
                ID_ORDER_AUTO.Text = "";
                ID_ORDER_HAND.Text = "";
                CUSTOMER_INVOICE_DATE.Value = DATE_NOW.Value.Date;
                ID_PAY_RECEIPT.Text = "";
                Cash.Text = "";
                cheek_num.Text = "";
                total_rest.Text = "";
            } catch (Exception){ }
            finally{ con.Close(); }
        }

        public void Save_DGV_Invoice_Back()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string Query = "INSERT INTO TBL_Back_INVOICE_DETAILS (PARCODE_2,PRODUCT_NAME_2,UNITS_2,QTY_2,TAX_2,PRICE_2,TOTAL_2,ID_AUTO,ID_HAND) VALUES(@PARCODE_2,@PRODUCT_NAME_2,@UNITS_2,@QTY_2,@TAX_2,@PRICE_2,@TOTAL_2,@ID_AUTO,@ID_HAND)";
                con.Open();
                for (int i = 0; i < DGV_Back_Invoice.Rows.Count; i++)
                {
                    cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@PARCODE_2", DGV_Back_Invoice.Rows[i].Cells["PARCODE_back"].Value.ToString());
                    cmd.Parameters.AddWithValue("@PRODUCT_NAME_2", DGV_Back_Invoice.Rows[i].Cells["PRODUCT_NAME_back"].Value.ToString());
                    cmd.Parameters.AddWithValue("@UNITS_2", DGV_Back_Invoice.Rows[i].Cells["UNIT_back"].Value.ToString());
                    cmd.Parameters.AddWithValue("@QTY_2", DGV_Back_Invoice.Rows[i].Cells["QTY_back"].Value.ToString());
                    cmd.Parameters.AddWithValue("@TAX_2", DGV_Back_Invoice.Rows[i].Cells["Taxing_back"].Value.ToString());
                    cmd.Parameters.AddWithValue("@PRICE_2", DGV_Back_Invoice.Rows[i].Cells["price_back"].Value.ToString());
                    cmd.Parameters.AddWithValue("@TOTAL_2", DGV_Back_Invoice.Rows[i].Cells["Total_back"].Value.ToString());
                    cmd.Parameters.AddWithValue("@ID_AUTO", txt_id.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID_HAND", ID_ORDER_HAND.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception){ }
            finally
            {
                con.Close();
            }
        }

        public Int32 max_id_TBL_PAY_RECEIPT()
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            Int32 ID = 0;
            string Query = "Select max(ID)as max from TBL_PAY_RECEIPT";
            con.Open();
            cmd = new SqlCommand(Query,con);
            cmd.ExecuteNonQuery();

            DataSet dsS = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dsS);

            DataRow drS;
            drS = dsS.Tables[0].Rows[0];
            ID = Convert.ToInt32(drS["max"].ToString());
            ID++;
            con.Close();
            return ID;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("انتبة ربما تحذف الفاتورة", "تنبيه بالحذف", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    Clear_DGV_Back_Invoice();
                    this.Close();
                }
                else { return; }
            }
            catch (Exception)
            {

            }
            
        }

        public void Clear_DGV_Back_Invoice()
        {
            try
            {
                if (txt_id.Text == Convert.ToString(max_id_TBL_PAY_RECEIPT()))
                {
                    for (int i = 0; i < DGV_Back_Invoice.Rows.Count; i++)
                    {
                        while (DGV_Back_Invoice.Rows.Count > 0)
                        {
                            txt_par.Text = DGV_Back_Invoice.SelectedRows[0].Cells["PARCODE_back"].Value.ToString();
                            txt_parcode.Text = txt_par.Text.Trim();
                            txt_name.Text = DGV_Back_Invoice.SelectedRows[0].Cells["PRODUCT_NAME_back"].Value.ToString();
                            txt_unit.Text = DGV_Back_Invoice.SelectedRows[0].Cells["UNIT_back"].Value.ToString();
                            txt_qty.Text = DGV_Back_Invoice.SelectedRows[0].Cells["QTY_back"].Value.ToString();
                            txt_tax.Text = DGV_Back_Invoice.SelectedRows[0].Cells["Taxing_back"].Value.ToString();
                            txt_Price.Text = DGV_Back_Invoice.SelectedRows[0].Cells["price_back"].Value.ToString();
                            Delete_Row();
                        }
                    }
                }
                else {  }
            }
            catch (Exception) { }
            finally
            {
                //if (txt_amount_of_product.Text.Trim() == "0")
                //{
                
                //}
            }
        }

        public void Delete_Row()
        {
            try
            {
                if (txt_qty.Text.Trim() == "" || txt_quantity_store.Text.Trim() == "") { MessageBox.Show("لا توجد كمية متوفرة"); return; }
                decimal qty_store = Convert.ToDecimal(txt_quantity_store.Text.Trim());
                decimal txtfinish = Convert.ToDecimal(txt_Finish.Text.Trim());
                decimal doubleqty = qty_store - txtfinish;
                string Finish = Convert.ToString(doubleqty);
                if (FK.Text.Trim() == "" && ID.Text.Trim() != "")
                {
                    fill.UpdateP1_Parcode(Finish, txt_par);
                    fill.UpdateP2_Fk(Finish, ID);
                    ID.Text = "";
                    FK.Text = "";
                }
                else if (FK.Text.Trim() != "" && ID.Text.Trim() == "")
                {
                    fill.UpdateP2_Parcode(Finish, txt_name);
                    fill.UpdateP1_Fk(Finish, FK);
                    ID.Text = "";
                    FK.Text = "";
                }
                var rowindex = DGV_Back_Invoice.CurrentCell.RowIndex;
                DGV_Back_Invoice.Rows.RemoveAt(rowindex);

                decimal qty = Convert.ToDecimal(txt_qty.Text.Trim());
                decimal total = Convert.ToDecimal(txt_total_Above.Text.Trim());

                AMOUNT.Text = (Convert.ToDecimal(AMOUNT.Text.Trim()) - qty).ToString();
                TOTAL_BEFORE_DISCOUNT.Text = (Convert.ToDecimal(TOTAL_BEFORE_DISCOUNT.Text.Trim()) - total).ToString();

                txt_par.Text = "";
                txt_name.Text = "";
                txt_par.Text = "";
                txt_parcode.Text = "";
                txt_name.Text = "";
                txt_unit.Text = "";
                txt_Price.Text = "";
                txt_quantity_store.Text = "";
                txt_tax.Text = "";
                txt_qty.Text = "";
                txt_total_Above.Text = "";
                txt_Machine.Text = "";
                btn_Delete_Invoice.Enabled = false;
                btn_Delete_Invoice.BackColor = Color.White;
            } catch (Exception) { }
        }

        private void txt_lasttotal_back_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TOTAL_BEFORE_DISCOUNT.Text.Trim() == "") { TOTAL_AFTER_DISCOUNT.Text = ""; } else{decimal totalbefore = Convert.ToDecimal(TOTAL_BEFORE_DISCOUNT.Text.Trim());decimal discount = Convert.ToDecimal(DISCOUNT.Text.Trim());TOTAL_AFTER_DISCOUNT.Text = Convert.ToString(totalbefore - discount);}
            } catch (Exception) { }
        }

        private void txt_discount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (DISCOUNT.Text.Trim() == "" ) { TOTAL_AFTER_DISCOUNT.Text = TOTAL_BEFORE_DISCOUNT.Text.Trim(); }  if (TOTAL_BEFORE_DISCOUNT.Text.Trim() == "") { TOTAL_AFTER_DISCOUNT.Text = ""; } else { decimal totalbefore = Convert.ToDecimal(TOTAL_BEFORE_DISCOUNT.Text.Trim()); decimal discount = Convert.ToDecimal(DISCOUNT.Text.Trim()); TOTAL_AFTER_DISCOUNT.Text = Convert.ToString(totalbefore - discount); }
            } catch (Exception) { }
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_qty.Text.Trim() == "") { txt_qty.Text = "1"; txt_qty.SelectAll(); } if(txt_qty.Text.Trim() == "") { txt_qty.Text = "0"; }  decimal qty_above = Convert.ToDecimal(txt_qty.Text.Trim()); if (txt_Price.Text.Trim() == "") { txt_Price.Text = "0"; } decimal pric = Convert.ToDecimal(txt_Price.Text.Trim()); decimal total_2 = pric * qty_above; if (txt_total_Above.Text.Trim() == "") { txt_total_Above.Text = "0"; } txt_total_Above.Text = Convert.ToString(total_2); if (Items_Number.Text.Trim() == "") { Items_Number.Text = "0"; } decimal items_num = Convert.ToDecimal(Items_Number.Text.Trim());  txt_Finish.Text = Convert.ToString(qty_above * items_num);
            } catch (Exception) { }
        }

        private void Items_Number_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal items_num = Convert.ToDecimal(Items_Number.Text.Trim()); decimal qty_above = Convert.ToDecimal(txt_qty.Text.Trim()); txt_Finish.Text = Convert.ToString(qty_above * items_num);
            }
            catch (Exception) { }
        }

        private void frm_back_invoice_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select invoice_back_open,invoice_back_save,invoice_back_delete,invoice_back_new From USERS_PERMESSIONS Where USER_NAME = N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd22 = new SqlCommand(Query, con);
                SqlDataReader dr2 = cmd22.ExecuteReader();
                if (dr2.Read())
                {
                    if (dr2["invoice_back_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr2["invoice_back_save"].ToString() == "True") { btn_save_pay.Enabled = true; } else { btn_save_pay.Enabled = false; }
                    //if (dr2["invoice_back_delete"].ToString() == "True") { .Enabled = true; } else { .Enabled = false; }
                    //if (dr2["invoice_back_new"].ToString() == "True") { .Enabled = true; } else { .Enabled = false; }
                }
                dr2.Close();
            } catch (Exception) { }
            finally { con.Close(); }
        }
    }
}
