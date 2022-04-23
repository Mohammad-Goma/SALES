using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Web;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace SALES
{
    public partial class frm_Invoice : Form
    {
        public decimal QTY_From_TBl_1 = 0;
        public decimal QTY_From_TBl_2 = 0;
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd;
        Fill_Cbo Fill_ = new Fill_Cbo();
        frm_Invoice invoice;
        //Boolean BACK_FROM_Dgv = false;
        
        public frm_Invoice()
        {
            InitializeComponent();
        }
        
        public void Close_Invoice()
        {
            try
            {
                decimal Finish2 = 0;
                if (txt_id.Text == Convert.ToString(max_id_TBL_ORDER()))
                {
                    if (MessageBox.Show("انتبة ربما تحذف الفاتورة", "تنبيه بالحذف", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        string prod_name = "";
                        string Qty_ = "";
                        
                        for (int i = 0; i < DGV_INVOCE.Rows.Count; i++)
                        {
                            DGV_INVOCE.ClearSelection();
                            DGV_INVOCE.Rows[0].Selected = true;
                            while (DGV_INVOCE.Rows.Count > 0)
                            {
                                Finish2 = 0;

                                prod_name = Convert.ToString(DGV_INVOCE.Rows[i].Cells["pRODUCTNAMEDataGridViewTextBoxColumn"].Value);
                                Qty_ = Convert.ToString(DGV_INVOCE.Rows[i].Cells["qTYDataGridViewTextBoxColumn"].Value);

                                decimal DECQty = Convert.ToDecimal(Qty_);
                                QTY_From_TBl_1 = 0;                                  // تصفير الكميات قبل اعطائها قيمة
                                QTY_From_TBl_2 = 0;                                  // تصفير الكميات قبل اعطائها قيمة 
                                Select_QTY_From_Two_Table(prod_name);
                                
                                    Finish2 = QTY_From_TBl_1 + DECQty;
                                    /// Now Update Tbl
                                    Fill_.UPDATE_QUANTITY(Finish2, prod_name);
                                    Fill_.UpdateP2_Parcode(Finish2, prod_name);
                                
                                //else
                                //if(QTY_From_TBl_1 < 0) 
                                //{
                                //    _ = Math.Abs(QTY_From_TBl_1);  // Math.Abs(i)   Convert From Negative To positive
                                //    Finish2 = QTY_From_TBl_1 + intQty;
                                //    /// Now Update Tbl1
                                //    Fill_.UPDATE_QUANTITY(Finish2, par_);
                                //}
                                //else
                                //if (QTY_From_TBl_2 < 0)
                                //{
                                //    _ = Math.Abs(QTY_From_TBl_2);  // Math.Abs(i)   Convert From Negative To positive
                                //    Finish2 = QTY_From_TBl_2 + intQty;
                                //    /// Now Update Tbl2
                                //    Fill_.UpdateP2_Parcode(Finish2, par_);
                                //}
                                int row_ = DGV_INVOCE.CurrentCell.RowIndex;
                                DGV_INVOCE.Rows.RemoveAt(row_);
                            }
                        }
                        this.Close();
                        
                    }
                    else { return; }
                }
                else
                {
                    this.Close();
                }
            }
            catch  { this.Close(); } // 
        }
        
        public void Select_QTY_From_Two_Table(string PRODUCT_NAM)
        {
            try
            {
                QTY_From_TBl_1 = 0;
                QTY_From_TBl_2 = 0;

                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string query = "SELECT Quantity FROM TBL_PRODUCTS WHERE PRODUCT_NAME = N'" + PRODUCT_NAM + "' ";
                SqlCommand cmdTbl = new SqlCommand(query, con);
                SqlDataReader dr = cmdTbl.ExecuteReader();
                if (dr.Read())
                {
                    QTY_From_TBl_1 = Convert.ToDecimal( dr["Quantity"].ToString());
                }
                else 
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    string query2 = "SELECT QUANTITY FROM NEW_PRODUCTS WHERE PRODUCT_NAME = N'" + PRODUCT_NAM + "' ";
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.ExecuteNonQuery();
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    DataTable dt2 = new DataTable();
                    da2.Fill(dt2);
                    if (dt2.Rows.Count > 0)
                    {
                        QTY_From_TBl_2 = Convert.ToDecimal(dt2.Rows[0]["QUANTITY"].ToString());
                    }
                    con.Close();
                } 
            }
            catch  { }
        }    
        private void btn_close_invoice_Click(object sender, EventArgs e)
        {
            Close_Invoice();
        }

        public void lvl_permessions(Button btn1, Button btn2, Button btn3, Button btn4)
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
                    if (dr1["invoice_save"].ToString() == "True") { } else { btn1.Enabled = false; }
                    if (dr1["invoice_new"].ToString() == "True") { } else { btn2.Enabled = false; }
                    if (dr1["invoice_new"].ToString() == "True") { } else { btn3.Enabled = false; }
                    if (dr1["invoice_new"].ToString() == "True") { } else { btn4.Enabled = false; }
                }
                dr1.Close();
            }
            catch  { }
            finally { con.Close(); }
        }


        public void lvl_permessions(Button btn1, Button btn2)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select * From USERS_PERMESSIONS Where USER_NAME=N'" + cbo_user_invoice.Text.Trim() + "' ";
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
            catch  { }
            finally { con.Close(); }
        }

        private void frm_Invoice_Load(object sender, EventArgs e)
        {
            try
            {
                //frm_Order_Report frm_Report = new frm_Order_Report();
                // TODO: This line of code loads data into the 'realApplicationDataSet.TBL_ORDER_DETAILS' table. You can move, or remove it, as needed.
                //this.tBL_ORDER_DETAILSTableAdapter.Fill(this.realApplicationDataSet.TBL_ORDER_DETAILS);
                DGV_INVOCE.DataSource = null;
                Fill_.fill_2product_tblByID(txt_parcode);
                Fill_.fill_2product_tblByName(txt_product_name);
                //fill_2product_tblByID();
                //fill_2product_tblByName();
                txt_id.Text = Convert.ToString(max_id_TBL_ORDER());
                Fill_.Fill_Units(cbo_units);
                Fill_.Fill_Users(cbo_user_invoice);
                Fill_.Fill_Customers(cbo_customer);
                Fill_.fill_stores_Data(cbo_store);
                Fill_.Fill_Branches(cbo_branches);
                Fill_.Fill_Billing(txt_parcode_invoice);
                cbo_user_invoice.Text = Main_Form.USER_NAME_STRING.Trim();
                //lvl_permessions(invoice, btn_Save_Without_Save, btn_SaveAndPrint, btn_new_invoice);
            }
            catch  { }
        }

        public void Clear_Invoice()
        {
            txt_id.Text = "";
            txt_casheer.Text = "";
            txt_parcode_invoice.Text = "";
            txt_product_name_invoice.Text = "";
            txt_true_price.Text = "";
            cbo_units.Text = "";
            qty.Text = "1";
            txt_price.Text = "";
            txt_Total_Above.Text = "";
            txt_quantity.Text = "";
            txt_ID_ORDER_INVOICE.Text = "";
            txt_product_parcode.Text = "";
            DGV_INVOCE.Rows.Clear();
            txt_amount_of_product.Text = "0";
            Discount_percent.Text = "";
            txt_Discount_Money.Text = "";
            txt_Qunt.Text = "0";
            txt_Discount_value.Text = "0";
            Total_Discount.Text = "0";
            Total_.Text = "0";
            txt_pay.Text = "0";
            txt_The_rest.Text = "0";
            //msg_invoice.Text = "";

            txt_id.Text = Convert.ToString(max_id_TBL_ORDER());
        }

        //private void cbo_parcode_invoice_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try 
        //    { 
        //        if (con.State == ConnectionState.Open) { con.Close(); }
        //        if (a.SelectedIndex > -1)
        //        {
        //            con.Open();
        //            string query = "SELECT PARCODE,PRODUCT_NAME,UNITS,PRICE,Quantity,Items_Number,Taxing FROM TBL_PRODUCTS WHERE PARCODE = N'" + a.Text.Trim() + "' ";
        //            cmd = new SqlCommand(query, con);
        //            cmd.ExecuteNonQuery();
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            DataTable dt = new DataTable();
        //            da.Fill(dt);
        //                txt_parcode_invoice.Text= dt.Rows[0]["PARCODE"].ToString();
        //                txt_product_name_invoice.Text = dt.Rows[0]["PRODUCT_NAME"].ToString();
        //                cbo_units.Text = dt.Rows[0]["UNITS"].ToString();
        //                txt_true_price.Text = dt.Rows[0]["PRICE"].ToString();        //  السعر الثابت الذي لا يتغير
        //                txt_price.Text = dt.Rows[0]["PRICE"].ToString();
        //                txt_quantity.Text = dt.Rows[0]["Quantity"].ToString();
        //                qty.Text= dt.Rows[0]["Items_Number"].ToString();
        //                Tax.Text = dt.Rows[0]["Taxing"].ToString();
        //                txt_product_parcode.Text = dt.Rows[0]["PARCODE"].ToString();
        //            btn_delete_row.Enabled = false;
        //            btn_delete_row.BackColor = Color.White;
        //            con.Close();
        //            //btn_add_product_Click(sender, e);
        //        }
        //    }
        //    catch  { };
        //}

        //public void fill_2product_tblByID()
        //{
        //    try
        //    {
        //        a.Items.Clear();
        //        if (con.State == ConnectionState.Open) { con.Close(); }
        //        con.Open();
        //        string query = "SELECT * FROM TBL_PRODUCTS ";
        //        cmd = new SqlCommand(query, con);
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        while (dr.Read())
        //        {
        //            a.Items.Add(dr["PARCODE"].ToString());
        //        }
        //        dr.Close();
        //        con.Close();
        //    } catch { }
        //}

        //public void fill_2product_tblByName()
        //{
        //    if (con.State == ConnectionState.Open) { con.Close(); }
        //    con.Open();
        //    string query = "SELECT * FROM TBL_PRODUCTS ";
        //    cmd = new SqlCommand(query, con);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        b.Items.Add(dr["PRODUCT_NAME"].ToString());
        //    }
        //    dr.Close();
        //    con.Close();
        //}

        //private void cbo_product_name_invoice_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try 
        //    {
        //        if (con.State == ConnectionState.Open) { con.Close(); }
        //        con.Open();
        //        string query = "SELECT * FROM TBL_PRODUCTS WHERE PRODUCT_NAME like N'" + b.Text.Trim() + "' ";
        //        cmd = new SqlCommand(query, con);
        //        cmd.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        txt_product_parcode.Text= dt.Rows[0]["ID"].ToString();
        //        txt_parcode_invoice.Text = dt.Rows[0]["PARCODE"].ToString();
        //        txt_product_name_invoice.Text = dt.Rows[0]["PRODUCT_NAME"].ToString();
        //        cbo_units.Text = dt.Rows[0]["UNITS"].ToString();
        //        txt_true_price.Text = dt.Rows[0]["PRICE"].ToString();     //  السعر الثابت الذي لا يتغير
        //        txt_price.Text = dt.Rows[0]["PRICE"].ToString();
        //        //qty.Text = "1";
        //        txt_quantity.Text = dt.Rows[0]["Quantity"].ToString();
        //        qty.Text = dt.Rows[0]["Items_Number"].ToString();
        //        Tax.Text = dt.Rows[0]["Taxing"].ToString();
        //        txt_product_parcode.Text= dt.Rows[0]["PARCODE"].ToString();
        //        btn_delete_row.Enabled = false;
        //        btn_delete_row.BackColor = Color.White;

        //    }
        //    catch(Exception ) { }
        //    finally { con.Close(); }
        //}

        public void btn_add_product_Click(object sender, EventArgs e)
        {
            ADDBtn();
        }
        public void ADDBtn()
        {
            try
            {
                decimal total = 0;
                //if (txt_parcode_invoice.Text == "" && txt_product_name_invoice.Text == "") { MessageBox.Show("من فضلك أختر الصنف المراد إضافته الى فاتورة المبيعات"); }
                decimal true_price = Convert.ToDecimal(txt_true_price.Text.Trim());
                decimal txt_insert = Convert.ToDecimal(txt_price.Text.Trim());
                decimal Qty_ord = Convert.ToDecimal(qty.Text.Trim());    //
                if (txt_Total_Above.Text.Trim() == "") { txt_Total_Above.Text = "0"; }
                total = Convert.ToDecimal(txt_Total_Above.Text.Trim());
                if (txt_quantity.Text.Trim() == "") { txt_quantity.Text = "0"; }
                decimal txt_qua = Convert.ToDecimal(txt_quantity.Text.Trim());
                if (Tax.Text.Trim() == "") { Tax.Text = "0"; }
                decimal tax = Convert.ToDecimal(Tax.Text.Trim());
                //if (txt_price.Text.Trim() == "") { MessageBox.Show("من فضلك املأ  خانة السعر "); }
                //else
                if (txt_parcode.AutoCompleteCustomSource.Count <= -1 || txt_product_name.AutoCompleteCustomSource.Count <= -1) return;    //if (cbo_parcode_invoice.SelectedIndex <= -1 || cbo_product_name_invoice.SelectedIndex <= -1) return;
                else
                if (txt_qua <= 0 || txt_quantity.Text.Trim() == null)
                {
                    msg_invoice.Text = "لا توجد كميات من هذا الصنف في المخزن";
                }
                else { msg_invoice.Text = " "; }
                bool found = false;
                if (DGV_INVOCE.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dtgr in DGV_INVOCE.Rows)
                    {
                        if (dtgr.Cells[0].Value.ToString() == txt_parcode_invoice.Text.Trim())
                        {
                            dtgr.Cells["qTYDataGridViewTextBoxColumn"].Value = (Qty_ord + Convert.ToDecimal(dtgr.Cells["qTYDataGridViewTextBoxColumn"].Value));
                            dtgr.Cells["tOTALDataGridViewTextBoxColumn"].Value = (total + Convert.ToDecimal(dtgr.Cells["tOTALDataGridViewTextBoxColumn"].Value));
                            dtgr.Cells["Taxing"].Value = (tax + Convert.ToDecimal(dtgr.Cells["Taxing"].Value));

                            decimal Finish_QTY = txt_qua - Qty_ord;
                            string Finish = Convert.ToString(Finish_QTY);
                            if (id_fk.Text.Trim() == "" && id_product.Text.Trim() != "")
                            {
                                Fill_.UpdateP1_Parcode(Finish, txt_product_parcode);
                                Fill_.UpdateP2_Fk(Finish, id_product);
                                id_product.Text = "";
                                id_fk.Text = "";
                            }
                            else if (id_fk.Text.Trim() != "" && id_product.Text.Trim() == "")
                            {
                                Fill_.UpdateP2_Parcode(Finish, txt_product_name_invoice);
                                Fill_.UpdateP1_Fk(Finish, id_fk);
                                id_product.Text = "";
                                id_fk.Text = "";
                            }
                            txt_amount_of_product.Text = (Convert.ToDecimal(txt_amount_of_product.Text) + Qty_ord).ToString();
                            txt_Qunt.Text = (Convert.ToDecimal(txt_Qunt.Text) + total).ToString();
                            Total_Tax.Text = (Convert.ToDecimal(Total_Tax.Text) + tax).ToString();        //
                            //decimal C = 0;
                            //    for (int i = 0; i < DGV_INVOCE.Rows.Count; i++)
                            //    {
                            //        C += Convert.ToDecimal(DGV_INVOCE.Rows[i].Cells["Taxing"].Value.ToString());
                            //    }
                            //    Total_Tax.Text = C.ToString().Replace("-","");
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
            catch  { }
            finally { }
        }

        public void Clear()
        {
            try
            {
                //txt_parcode.Text = "";
                //txt_product_name.Text = "";
                txt_parcode_invoice.Text = "";
                txt_product_name_invoice.Text = "";
                cbo_units.Text = "";
                qty.Text = "";
                Tax.Text = "";
                LESS_PRICE.Text = "";
                txt_price.Text = "";
                txt_Total_Above.Text = "";
                txt_quantity.Text = "";
                //txt_ID_ORDER_INVOICE.Text = "";
                Fill_.fill_2product_tblByID(txt_parcode);
                Fill_.fill_2product_tblByID(Search);
                Fill_.fill_2product_tblByName(txt_product_name);
            }
            catch  { }
        }

        public void insertintoDGV()
        {
            try
            {
                decimal Qty_ord = Convert.ToDecimal(qty.Text.Trim());
                decimal txt_qua = Convert.ToDecimal(txt_quantity.Text.Trim());
                decimal Finish_QTY = txt_qua - Qty_ord;
                string Finish = Convert.ToString(Finish_QTY);

                if (id_fk.Text.Trim() == "" && id_product.Text.Trim() != "")
                {
                    Fill_.UpdateP1_Parcode(Finish, txt_product_parcode);
                    Fill_.UpdateP2_Fk(Finish, id_product);
                    id_product.Text = "";
                    id_fk.Text = "";
                }
                else if (id_fk.Text.Trim() != "" && id_product.Text.Trim() == "")
                {
                    Fill_.UpdateP2_Parcode(Finish, txt_product_name_invoice);
                    Fill_.UpdateP1_Fk(Finish, id_fk);
                    id_product.Text = "";
                    id_fk.Text = "";
                }
                string parc = Convert.ToString(txt_parcode_invoice.Text.Trim());
                string name = txt_product_name_invoice.Text.Trim();
                decimal Qty = Convert.ToDecimal(qty.Text.Trim());
                decimal tax = Convert.ToDecimal(Tax.Text.Trim());
                string unit = cbo_units.Text.Trim();
                decimal price = Convert.ToDecimal(txt_price.Text.Trim());
                decimal subtotal = Convert.ToDecimal(txt_Total_Above.Text.Trim());
                int ID = Convert.ToInt32(txt_id.Text.Trim());
                var id_hand = txt_ID_ORDER_INVOICE.Text.Trim();
                object[] row = { parc, name, unit, Qty, tax, price, subtotal, ID, id_hand };
                DGV_INVOCE.Rows.Add(row);
                if (txt_amount_of_product.Text.Trim() == "") { txt_amount_of_product.Text = "0"; }
                else if (txt_Qunt.Text.Trim() == "") { txt_Qunt.Text = "0"; }

                txt_amount_of_product.Text = (Convert.ToDecimal(txt_amount_of_product.Text) + Qty).ToString();
                txt_Qunt.Text = (Convert.ToDecimal(txt_Qunt.Text) + subtotal).ToString();
                Total_Tax.Text = (Convert.ToDecimal(Total_Tax.Text) + tax).ToString();    //
                //decimal C = 0;
                //for (int i = 0; i < DGV_INVOCE.Rows.Count; i++)
                //{
                //    C += Convert.ToDecimal(DGV_INVOCE.Rows[i].Cells["Taxing"].Value.ToString());
                //}
                //Total_Tax.Text = C.ToString().Replace("-", "");
                //txt_Qty_Ord.Text = "1"; 
            }
            catch  { }
            finally { btn_delete_row.Enabled = false; }
        }

        private void txt_pay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal lasttotal = Convert.ToDecimal(Total_.Text.Trim()); if (txt_pay.Text.Trim() == "") { txt_pay.Text = "0";  txt_pay.SelectAll(); txt_The_rest.Text = Convert.ToString(lasttotal * -1); txt_The_rest.BackColor = Color.Yellow; } decimal pay = Convert.ToDecimal(txt_pay.Text.Trim()); decimal minus = lasttotal * -1; txt_The_rest.Text = Convert.ToString(pay - lasttotal).Replace("-", ""); if (lasttotal > pay) { txt_The_rest.BackColor = Color.Yellow; } else if (pay == lasttotal) { txt_The_rest.BackColor = Color.White; } else if (pay > lasttotal) { txt_The_rest.BackColor = Color.Green; }
            }
            catch  { }
        }

        public void Print_key()
        {
            try
            {
                FshowRpt frm = new FshowRpt(cbo_user_invoice.Text.Trim(), cbo_customer.Text.Trim(), date_invoice.Text.Trim(), txt_id.Text.Trim(), txt_casheer.Text.Trim(), txt_amount_of_product.Text.Trim(), cbo_pay_way.Text.Trim(), Total_.Text.Trim(), txt_pay.Text.Trim(), txt_The_rest.Text.Trim(), Total_.Text.Trim());
                Dept_rpt rp = new Dept_rpt();
                Ds myds = new Ds();

                foreach (DataGridViewRow dgv in DGV_INVOCE.Rows)
                {
                    myds.Debt.Rows.Add( dgv.Cells["pARCODEDataGridViewTextBoxColumn"].Value, dgv.Cells["pRODUCTNAMEDataGridViewTextBoxColumn"].Value,
                                        dgv.Cells["uNITDataGridViewTextBoxColumn"].Value, dgv.Cells["qTYDataGridViewTextBoxColumn"].Value, dgv.Cells["Taxing"].Value, 
                                        dgv.Cells["pRICEDataGridViewTextBoxColumn"].Value, dgv.Cells["tOTALDataGridViewTextBoxColumn"].Value, 
                                        dgv.Cells["orderidDataGridViewTextBoxColumn"].Value, dgv.Cells["ID_Order_Invoice"].Value);
                }
                DataView dv = new DataView(myds.Tables["Debt"]);
                rp.SetDataSource(dv); 
                rp.SetParameterValue("cbo_user_invoice", cbo_user_invoice.Text.Trim());
                rp.SetParameterValue("cbo_customer", cbo_customer.Text.Trim());
                rp.SetParameterValue("date_invoice", date_invoice.Text.Trim());
                rp.SetParameterValue("txt_id", txt_id.Text.Trim());
                rp.SetParameterValue("txt_casheer", txt_casheer.Text.Trim());
                rp.SetParameterValue("txt_amount_of_product", txt_amount_of_product.Text.Trim());
                rp.SetParameterValue("cbo_pay_way", cbo_pay_way.Text.Trim());
                rp.SetParameterValue("Total_", Total_.Text.Trim());
                rp.SetParameterValue("txt_pay", txt_pay.Text.Trim());
                rp.SetParameterValue("txt_The_rest", txt_The_rest.Text.Trim());
                rp.SetParameterValue("Total_2", Total_.Text.Trim());
                frm.Dept_View.ReportSource = rp;
                frm.Dept_View.Refresh();
                //rp.PrintToPrinter(1, false, 0, 0);
                frm.Show();
                //labeltrue();
            }
            catch  { }
        }
        public void Print_key_BigformF7()
        {
            try
            {
                frm_new_invoice_crystal frm_new_invoice = new frm_new_invoice_crystal(cbo_user_invoice.Text.Trim(),
                    cbo_customer.Text.Trim(), date_invoice.Text.Trim(), txt_id.Text.Trim(),
                    txt_casheer.Text.Trim(), txt_amount_of_product.Text.Trim(), cbo_pay_way.Text.Trim(),
                    Total_.Text.Trim(), txt_pay.Text.Trim(), txt_The_rest.Text.Trim(), Total_.Text.Trim());
                big_invoice rp = new big_invoice();
                DataSetnew_invoice myds2 = new DataSetnew_invoice();

                foreach (DataGridViewRow dgv in DGV_INVOCE.Rows)
                {
                    myds2.Debt2.Rows.Add(dgv.Cells["pARCODEDataGridViewTextBoxColumn"].Value, dgv.Cells["pRODUCTNAMEDataGridViewTextBoxColumn"].Value,
                                          dgv.Cells["uNITDataGridViewTextBoxColumn"].Value, dgv.Cells["qTYDataGridViewTextBoxColumn"].Value,
                                          dgv.Cells["Taxing"].Value, dgv.Cells["pRICEDataGridViewTextBoxColumn"].Value,
                                          dgv.Cells["tOTALDataGridViewTextBoxColumn"].Value, dgv.Cells["orderidDataGridViewTextBoxColumn"].Value,
                                          dgv.Cells["ID_Order_Invoice"].Value);
                }
                DataView dv2 = new DataView(myds2.Tables["Debt2"]);
                rp.SetDataSource(dv2);
                // أسم الموظف / اسم العميل / تاريخ الفاتورة /  رقم الطلب /  رقم الكاشير /  اسم الصنف / 
                // الكمية / السعر / عدد الاصناف / طريقة الدفع / الاجمالي / المدفوع / الباقي / اجمالي الفاتورة 
                rp.SetParameterValue("cbo_user_invoice", cbo_user_invoice.Text.Trim());
                rp.SetParameterValue("cbo_customer", cbo_customer.Text.Trim());
                rp.SetParameterValue("date_invoice", date_invoice.Text.Trim());
                rp.SetParameterValue("txt_id", txt_id.Text.Trim());
                //rp.SetParameterValue("txt_casheer", txt_casheer.Text.Trim());
                rp.SetParameterValue("txt_amount_of_product", txt_amount_of_product.Text.Trim());
                //rp.SetParameterValue("cbo_pay_way", cbo_pay_way.Text.Trim());
                rp.SetParameterValue("Total_", Total_.Text.Trim());
                //rp.SetParameterValue("txt_pay", txt_pay.Text.Trim());
                //rp.SetParameterValue("txt_The_rest", txt_The_rest.Text.Trim());
                rp.SetParameterValue("Total_2", Total_.Text.Trim());
                rp.SetParameterValue("Taxing", Total_Tax.Text.Trim());

                frm_new_invoice.BigInvoice_view.ReportSource = rp;
                frm_new_invoice.BigInvoice_view.Refresh();
                //rp.PrintToPrinter(1, false, 0, 0);
                frm_new_invoice.Show();
                //PrintAuto();
                labeltrue();
            }
            catch  { }
        }
        public void Print_key_Bigform()
        {
            try
            {
                frm_new_invoice_crystal frm_new_invoice = new frm_new_invoice_crystal(cbo_user_invoice.Text.Trim(),
                    cbo_customer.Text.Trim(), date_invoice.Text.Trim(), txt_id.Text.Trim(),
                    txt_casheer.Text.Trim(), txt_amount_of_product.Text.Trim(), cbo_pay_way.Text.Trim(),
                    Total_.Text.Trim(), txt_pay.Text.Trim(), txt_The_rest.Text.Trim(), Total_.Text.Trim());
                big_invoice rp = new big_invoice();
                DataSetnew_invoice myds2 = new DataSetnew_invoice();

                foreach (DataGridViewRow dgv in DGV_INVOCE.Rows)
                {
                    myds2.Debt2.Rows.Add(dgv.Cells["pARCODEDataGridViewTextBoxColumn"].Value, dgv.Cells["pRODUCTNAMEDataGridViewTextBoxColumn"].Value,
                                          dgv.Cells["uNITDataGridViewTextBoxColumn"].Value, dgv.Cells["qTYDataGridViewTextBoxColumn"].Value,
                                          dgv.Cells["Taxing"].Value, dgv.Cells["pRICEDataGridViewTextBoxColumn"].Value,
                                          dgv.Cells["tOTALDataGridViewTextBoxColumn"].Value, dgv.Cells["orderidDataGridViewTextBoxColumn"].Value,
                                          dgv.Cells["ID_Order_Invoice"].Value);
                }
                DataView dv2 = new DataView(myds2.Tables["Debt2"]);
                rp.SetDataSource(dv2);
                // أسم الموظف / اسم العميل / تاريخ الفاتورة /  رقم الطلب /  رقم الكاشير /  اسم الصنف / 
                // الكمية / السعر / عدد الاصناف / طريقة الدفع / الاجمالي / المدفوع / الباقي / اجمالي الفاتورة 
                rp.SetParameterValue("cbo_user_invoice", cbo_user_invoice.Text.Trim());
                rp.SetParameterValue("cbo_customer", cbo_customer.Text.Trim());
                rp.SetParameterValue("date_invoice", date_invoice.Text.Trim());
                rp.SetParameterValue("txt_id", txt_id.Text.Trim());
                //rp.SetParameterValue("txt_casheer", txt_casheer.Text.Trim());
                rp.SetParameterValue("txt_amount_of_product", txt_amount_of_product.Text.Trim());
                //rp.SetParameterValue("cbo_pay_way", cbo_pay_way.Text.Trim());
                rp.SetParameterValue("Total_", Total_.Text.Trim());
                //rp.SetParameterValue("txt_pay", txt_pay.Text.Trim());
                //rp.SetParameterValue("txt_The_rest", txt_The_rest.Text.Trim());
                rp.SetParameterValue("Total_2", Total_.Text.Trim());
                rp.SetParameterValue("Taxing", Total_Tax.Text.Trim());

                frm_new_invoice.BigInvoice_view.ReportSource = rp;
                frm_new_invoice.BigInvoice_view.Refresh();
                rp.PrintToPrinter(1, false, 0, 0);
                //frm_new_invoice.Show();
                //PrintAuto();
                labeltrue();
            }
            catch  {    }
        }
        public void PrintAuto()
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                ReportDocument reportDocument = new ReportDocument();
                reportDocument.Load(Application.StartupPath + "\\big_invoice.rpt");
                reportDocument.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName;
                reportDocument.PrintToPrinter(printDialog.PrinterSettings.Copies, printDialog.PrinterSettings.Collate, printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage);
                //this.Close();
            }
        }
        private void btn_print_Click(object sender, EventArgs e)
        {
            Print_key();
            //Print_key_Bigform();
        }

        public void Qty_Qty2()
        {
            try
            {
                string par3 = parcode_2.Text.Trim();
                string product3 = product_name_2.Text.Trim();
                string units_3 = units_2.Text.Trim();
                string qty4 = qty3.Text.Trim();
                string tax_3 = tax3.Text.Trim();
                string price_3 = price3.Text.Trim();
                string total_3 = total3.Text.Trim();
                string id = txt_id.Text.Trim();
                string ID_ORDER = txt_ID_ORDER_INVOICE.Text.Trim();
                object[] row2 = { par3, product3, units_3, qty4, tax_3, price_3, total_3, id, ID_ORDER };
                DGV_INVOCE.Rows.Add(row2);
            }
            catch  { }
        }


        //decimal Qty_Ord = Convert.ToDecimal(this.qty.Text.Trim());
        //decimal qty2 = Convert.ToDecimal(back_qty.Text.Trim());

        //string parcode_1 = txt_parcode_invoice.Text.Trim();
        //string product_name = txt_product_name_invoice.Text.Trim();
        //string units_1 = cbo_units.Text.Trim();
        //decimal Tax_1 = Convert.ToDecimal(Tax.Text.Trim());
        //decimal qty_ord_1 = Convert.ToDecimal(qty.Text.Trim());
        //decimal price_1 = Convert.ToDecimal(txt_price.Text.Trim());
        //decimal total_1 = Convert.ToDecimal(txt_Total_Above.Text.Trim());
        //decimal tax_2 = Convert.ToDecimal(back_tax.Text.Trim());
        //decimal qty_ord_2 = Convert.ToDecimal(qty.Text.Trim());
        //decimal price_2 = Convert.ToDecimal(back_price.Text.Trim());
        //decimal total_2 = Convert.ToDecimal(back_total.Text.Trim());

        //decimal tax_1_2 = tax_2 - Tax_1;
        //decimal qty_1_2 = qty_ord_2 - qty_ord_1;
        //decimal price1_2 = price_2 - price_1;
        //decimal total_1_2 = total_2 - total_1;

        //qty3.Text = Convert.ToString(qty_1_2);
        //tax3.Text = Convert.ToString(tax_1_2);
        //price3.Text = Convert.ToString(price1_2);
        //total3.Text = Convert.ToString(total_1_2);

        public void Delete_Row()
        {
            int rowindex = DGV_INVOCE.CurrentCell.RowIndex;
            try
            {
                //if (txt_parcode_invoice.Text == "" && txt_product_name_invoice.Text == "") { MessageBox.Show("من فضلك أختر الصنف المراد حذفه من  فاتورة المبيعات"); }
                //else
                //{
                decimal Qty_Ord = Convert.ToDecimal(qty.Text.Trim());
                //decimal qty2 = Convert.ToDecimal(back_qty.Text.Trim());
                //decimal qt_3 = Convert.ToDecimal(qty3.Text.Trim());
                decimal qty_ = Convert.ToDecimal(qty.Text.Trim());
                                    //   قراءة الصف الحالي      
                decimal txt_qunt = Convert.ToDecimal(txt_Qunt.Text.Trim());         //   تحويل قيمة خلية المجموع الاولي الي ديسميل   
                decimal dec_item = Convert.ToDecimal(txt_Total_Above.Text.Trim());  //   تحويل خلية المجموع الى ديسيميل
                txt_Qunt.Text = Convert.ToString(txt_qunt - dec_item);              //   اعطاء قيمة جديدة لخلية المجموع

                int amount = Convert.ToInt32(txt_amount_of_product.Text.Trim());    //   حذف الكمية المحذوفة من بوكس مجموع الكميات
                txt_amount_of_product.Text = Convert.ToString(amount - qty_);       //   حذف الكمية المحذوفة من بوكس مجموع الكميات

                decimal txt_qua = Convert.ToDecimal(txt_quantity.Text.Trim());
                qty_ord_test.Text = Convert.ToString(Qty_Ord);
                decimal qua_Qty = txt_qua + Qty_Ord;                                //   زيادة قيمة الكمية المتوفرة  

                txt_Finish.Text = Convert.ToString(qua_Qty);
                string Finish = txt_Finish.Text.Trim();
                if (id_fk.Text.Trim() == "" && id_product.Text.Trim() != "")
                {
                    Fill_.UpdateP1_Parcode(Finish, txt_product_parcode);
                    Fill_.UpdateP2_Fk(Finish, id_product);
                    id_product.Text = "";
                    id_fk.Text = "";
                }
                else if (id_fk.Text.Trim() != "" && id_product.Text.Trim() == "")
                {
                    Fill_.UpdateP2_Parcode(Finish, txt_product_name_invoice);
                    Fill_.UpdateP1_Fk(Finish, id_fk);
                    id_product.Text = "";
                    id_fk.Text = "";
                }
                DGV_INVOCE.Rows.RemoveAt(rowindex);                      // حذف الصف الحالي
                                                                         //if (qty2 <= qt_3) { } else { Qty_Qty2(); fill_2product_tblByID(); fill_2product_tblByName(); }
                ClearAbove();

                Fill_.fill_2product_tblByID(txt_parcode);
                Fill_.fill_2product_tblByID(Search);
                Fill_.fill_2product_tblByName(txt_product_name);
                //Clear();
                //}
            }
            catch  { DGV_INVOCE.Rows.RemoveAt(rowindex); }
            finally { btn_delete_row.Enabled = false; }
        }

        public void btn_delete_row_Click(object sender, EventArgs e)
        {
            Delete_Row();
        }

        private void Discount_percent_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal txt_qunt = Convert.ToDecimal(txt_Qunt.Text.Trim()); if (Discount_percent.Text.Trim() == "") { Total_Discount.Text = "0"; txt_Discount_value.Text = "0"; decimal Discount_Money = Convert.ToDecimal(txt_Discount_Money.Text.Trim()); decimal txt_qunt_Discount_Money = txt_qunt - Discount_Money; Total_Discount.Text = Convert.ToString(txt_qunt_Discount_Money); txt_Discount_value.Text = txt_Discount_Money.Text.Trim(); } decimal percent = Convert.ToDecimal(Discount_percent.Text.Trim()); decimal Discount_value = Convert.ToDecimal(txt_Discount_value.Text.Trim()); decimal percent_txt_qunt = (percent * txt_qunt) / 100; txt_Discount_value.Text = Convert.ToString(percent_txt_qunt);
            }
            catch  { }
        }

        private void txt_Discount_Money_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_Discount_Money.Text.Trim() == "") { Total_Discount.Text = "0"; txt_Discount_value.Text = "0"; } decimal txt_qunt = Convert.ToDecimal(txt_Qunt.Text.Trim());  decimal Discount_Money = Convert.ToDecimal(txt_Discount_Money.Text.Trim()); decimal txt_qunt_Discount_Money = txt_qunt - Discount_Money; Total_Discount.Text = Convert.ToString(txt_qunt_Discount_Money);  txt_Discount_value.Text = txt_Discount_Money.Text.Trim();
            }
            catch  { }

        }

        private void txt_Discount_value_TextChanged(object sender, EventArgs e)
        {
            decimal txt_qunt = Convert.ToDecimal(txt_Qunt.Text.Trim()); decimal Discount_value = Convert.ToDecimal(txt_Discount_value.Text.Trim()); decimal txt_qunt_Discount_value = txt_qunt - Discount_value; Total_Discount.Text = Convert.ToString(txt_qunt_Discount_value);   //      txt_Qunt - txt_Discount_value
        }

        private void chk_percent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_percent.Checked) { chk_money.Checked = false; chk_percent.Checked = true; } txt_Discount_Money.ReadOnly = true; txt_Discount_Money.Text = ""; Discount_percent.ReadOnly = false; //Total_Discount.Text = "0";

            }
            catch 
            {

               
            }
         }

        private void chk_money_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_money.Checked) { chk_percent.Checked = false; chk_money.Checked = true; }  Discount_percent.Text = ""; txt_Discount_Money.ReadOnly = false; Discount_percent.ReadOnly = true;

            }
            catch 
            {

            }
        }

        private void Total_Discount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_Discount_value.Text.Trim() == txt_Qunt.Text.Trim()) { Total_Discount.Text = "0"; Total_.Text = "0"; } else if (Total_Discount.Text.Trim() == "0" && txt_Discount_value.Text.Trim() != "0") { decimal txt_qunt = Convert.ToDecimal(txt_Qunt.Text.Trim()); decimal Total_discount = Convert.ToDecimal(Total_Discount.Text.Trim()); Total_.Text = Convert.ToString(txt_qunt - Total_discount); } else { Total_.Text = Total_Discount.Text.Trim(); }

            }
            catch 
            {

            }
         }

        private void txt_Qunt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_Qunt.Text.Trim() == "") { txt_Qunt.Text = "0"; } decimal txt_qunt = Convert.ToDecimal(txt_Qunt.Text.Trim()); decimal Total_discount = Convert.ToDecimal(Total_Discount.Text.Trim()); Total_.Text = Convert.ToString(txt_qunt - Total_discount); if (chk_percent.Checked) { Discount_percent_TextChanged(sender, e); } else if (chk_money.Checked) { txt_Discount_Money_TextChanged(sender, e); }
            }
            catch 
            {
            }
          }

        public void UPDATE_QUANTITY_PRODUCT_1_PRODUCT_1()
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            string sql = "UPDATE TBL_PRODUCTS set Quantity = N'" + txt_quantity.Text.Trim() + "' where PARCODE = N'" + txt_product_parcode.Text.Trim() + "' ";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            cmd.Parameters.AddWithValue("@Quantity", txt_quantity.Text.Trim());
            con.Close();
            txt_parcode.Text = "";
            txt_product_name.Text = "";
            Fill_.fill_2product_tblByID(txt_parcode);
            Fill_.fill_2product_tblByName(txt_product_name);
        }

        public void UPDATE_QUANTITY_PRODUCT_1(string Finish)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string sql = "UPDATE TBL_PRODUCTS set Quantity = N'" + Finish + "' where PARCODE = N'" + txt_product_parcode.Text.Trim() + "' ";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@Quantity", Finish);
                txt_parcode.Text = "";
                txt_product_name.Text = "";
                Fill_.fill_2product_tblByID(txt_parcode);
                Fill_.fill_2product_tblByName(txt_product_name);
            }
            catch  { }
            finally { con.Close(); }
        }

        public void UPDATE_QUANTITY_PRODUCT_2(string Finish)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                string sql = "UPDATE NEW_PRODUCTS Set QUANTITY = N'" + Finish + "' where FORIEGN_KEY_ID = N'" + id_product.Text.Trim() + "' ";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@QUANTITY", Finish);
            }
            catch  { }
            finally { con.Close(); }
        }

        private void txt_quantity_TextChanged(object sender, EventArgs e)
        {

        }

        public void Select_Quantity(TextBox txt_parcode_invoice)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string query = "SELECT Quantity FROM TBL_PRODUCTS WHERE PARCODE = N'" + txt_parcode_invoice.Text.Trim() + "' ";
                SqlCommand cmd2 = new SqlCommand(query, con);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    txt_quantity.Text = dr["Quantity"].ToString();
                }
                con.Close();
            }
            catch 
            {

            }
        }
        public void DGV_INVOCE_CellClick(object sender, DataGridViewCellEventArgs e)            //   (Textbox) نقل البيانات من(جريد فيو ) الى الخانات 
        {
            try
            {
                DGV_INVOCE.Rows[e.RowIndex].Selected = true;
                if (e.RowIndex >= 0)
                {
                    //Select_Rows_From_DGVToBoxesAbove();
                    chk_fill.Checked = false;
                    txt_parcode.Text = DGV_INVOCE.SelectedRows[0].Cells["pARCODEDataGridViewTextBoxColumn"].Value.ToString();
                    //txt_product_name_invoice.Text = DGV_INVOCE.SelectedRows[0].Cells["pRODUCTNAMEDataGridViewTextBoxColumn"].Value.ToString();
                    //cbo_units.Text = DGV_INVOCE.SelectedRows[0].Cells["uNITDataGridViewTextBoxColumn"].Value.ToString();
                    qty.Text = DGV_INVOCE.SelectedRows[0].Cells["qTYDataGridViewTextBoxColumn"].Value.ToString();
                    //Tax.Text = DGV_INVOCE.SelectedRows[0].Cells["Taxing"].Value.ToString();
                    //txt_price.Text = DGV_INVOCE.SelectedRows[0].Cells["pRICEDataGridViewTextBoxColumn"].Value.ToString();
                    //txt_Total_Above.Text = DGV_INVOCE.SelectedRows[0].Cells["tOTALDataGridViewTextBoxColumn"].Value.ToString();
                    //Select_Quantity(txt_parcode_invoice);
                    btn_delete_row.Enabled = true;
                    btn_delete_row.BackColor = Color.MediumVioletRed; 
                }
            }
            catch  {  }                        //  Attention
        }

        public void DGV_INVOCE_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                DGV_INVOCE.Rows[e.RowIndex].Selected = true;
            }
            catch  { }
        }

        public Int64 max_id_TBL_ORDER()
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            Int64 id = 0;
            con.Open();
            cmd = new SqlCommand("SELECT MAX(ID) AS fds FROM TBL_ORDER", con); // Select max(ID_Customer) as fds From TBL_CUSTOMER
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

        public Int64 max_id_order_Details()
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            Int64 id = 0;
            con.Open();
            cmd = new SqlCommand("SELECT MAX(Id_Order_Details) AS fds FROM order_details", con); // Select max(ID_Customer) as fds From TBL_CUSTOMER
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

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGV_INVOCE.Rows.Count <= 0)
                {
                    MessageBox.Show("يرجى ملئ الفاتورة بالبيانات");
                }
                else
                {
                    Save_Order("1");
                    Save_Order_Details();
                    msg_invoice.Text = " تم حفظ الفاتورة بنجاح ";
                }
                Clear_Invoice();
                labeltrue();
            }
            catch 
            {

            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                chk_fill.Checked = true;
            }
        }

        public void Save_Order(string Save_print)
        {
            try
            {
                string id = Convert.ToString(max_id_TBL_ORDER());
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string sqlcon = " insert into [dbo].[TBL_ORDER] Values('" + id + "',N'" + txt_ID_ORDER_INVOICE.Text.Trim() + "',N'" + txt_casheer.Text.Trim() + "',N'" + cbo_user_invoice.Text.Trim() + "',N'" + cbo_store.Text.Trim() + "',N'" + cbo_customer.Text.Trim() + "',N'" + cbo_pay_way.Text.Trim() + "',N'" + date_invoice.Text.Trim() + "',N'" + txt_amount_of_product.Text.Trim() + "',N'" + txt_Qunt.Text.Trim() + "',N'" + txt_Discount_value.Text.Trim() + "',N'" + Total_.Text.Trim() + "',N'" + txt_pay.Text.Trim() + "',N'" + txt_The_rest.Text.Trim() + "',N'" + Save_print + "' ) ";

                cmd = new SqlCommand(sqlcon, con);
                cmd.ExecuteNonQuery();

                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@ID_Order_Invoice", txt_ID_ORDER_INVOICE.Text.Trim());
                cmd.Parameters.AddWithValue("@Casheer_number", txt_casheer.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_NAME", cbo_user_invoice.Text.Trim());
                cmd.Parameters.AddWithValue("@STORE_NAME", cbo_store.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_NAME", cbo_customer.Text.Trim());
                cmd.Parameters.AddWithValue("@PAY_WAY", cbo_pay_way.Text.Trim());
                cmd.Parameters.AddWithValue("@DATE", date_invoice.Text.Trim());
                cmd.Parameters.AddWithValue("@Amount_of_product", txt_amount_of_product.Text.Trim());
                cmd.Parameters.AddWithValue("@TOTAL_BEFORE_REST", txt_Qunt.Text.Trim());
                cmd.Parameters.AddWithValue("@Discount_value", txt_Discount_value.Text.Trim());
                cmd.Parameters.AddWithValue("@TOTAL", Total_.Text.Trim());
                cmd.Parameters.AddWithValue("@PAYING", txt_pay.Text.Trim());
                cmd.Parameters.AddWithValue("@The_rest", txt_The_rest.Text.Trim());
                cmd.Parameters.AddWithValue("@Save_print", Save_print);
                con.Close();
            }
            catch  { }
        }

        public void Save_Order_Details()
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            con.Open();
            for (int i = 0; i < DGV_INVOCE.Rows.Count; i++)
            {
                string sqlquerry = "Insert into [dbo].[TBL_ORDER_DETAILS] (PARCODE,PRODUCT_NAME,UNIT,QTY,Taxing,PRICE,TOTAL,Order_id,ID_Order_Invoice) Values (@PARCODE,@PRODUCT_NAME,@UNIT,@QTY,@Taxing,@PRICE,@TOTAL,@Order_id,@ID_Order_Invoice) ";

                cmd = new SqlCommand(sqlquerry, con);
                cmd.Parameters.AddWithValue("@PARCODE", DGV_INVOCE.Rows[i].Cells["pARCODEDataGridViewTextBoxColumn"].Value.ToString());
                cmd.Parameters.AddWithValue("@PRODUCT_NAME", DGV_INVOCE.Rows[i].Cells["pRODUCTNAMEDataGridViewTextBoxColumn"].Value.ToString());
                cmd.Parameters.AddWithValue("@UNIT", DGV_INVOCE.Rows[i].Cells["uNITDataGridViewTextBoxColumn"].Value.ToString());
                cmd.Parameters.AddWithValue("@QTY", DGV_INVOCE.Rows[i].Cells["qTYDataGridViewTextBoxColumn"].Value.ToString());
                cmd.Parameters.AddWithValue("@Taxing", DGV_INVOCE.Rows[i].Cells["Taxing"].Value.ToString());
                cmd.Parameters.AddWithValue("@PRICE", DGV_INVOCE.Rows[i].Cells["pRICEDataGridViewTextBoxColumn"].Value.ToString());
                cmd.Parameters.AddWithValue("@TOTAL", DGV_INVOCE.Rows[i].Cells["tOTALDataGridViewTextBoxColumn"].Value.ToString());
                cmd.Parameters.AddWithValue("@Order_id", txt_id.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_Order_Invoice", txt_ID_ORDER_INVOICE.Text.Trim());
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }

        public void fill_product_minus()
        {
            try
            {
                decimal Tax_1 = Convert.ToDecimal(Tax.Text.Trim()); decimal qty_ord_1 = Convert.ToDecimal(this.qty.Text.Trim()); decimal price_1 = Convert.ToDecimal(txt_price.Text.Trim()); decimal total_1 = Convert.ToDecimal(txt_Total_Above.Text.Trim()); decimal tax_2 = Convert.ToDecimal(back_tax.Text.Trim()); decimal qty_ord_2 = Convert.ToDecimal(back_qty.Text.Trim()); decimal price_2 = Convert.ToDecimal(back_price.Text.Trim()); decimal total_2 = Convert.ToDecimal(back_total.Text.Trim()); qty3.Text = Convert.ToString(qty_ord_2 - qty_ord_1); tax3.Text = back_tax.Text.Trim(); price3.Text = back_price.Text.Trim(); total3.Text = Convert.ToString(total_2 - total_1); back_total.Text = txt_Total_Above.Text.Trim();
            }
            catch  { }
        }

        private void txt_Qty_Ord_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (qty.Text.Trim() == "") { qty.Text = "1"; qty.SelectAll(); } decimal qty_ = Convert.ToDecimal(qty.Text.Trim()); decimal price = Convert.ToDecimal(txt_price.Text.Trim()); decimal total = qty_ * price; txt_Total_Above.Text = Convert.ToString(total);
                //decimal qty_back = Convert.ToDecimal(back_qty.Text.Trim());
                //if (btn_delete_row.Enabled == true && qty > qty_back)  {this.qty.Text = back_qty.Text.Trim();  MessageBox.Show("الكمية المطلوب حذفها أكبر من الموجودة في الفاتورة"); } if (qty < qty_back) { fill_product_minus(); } 
            }
            catch  { }
        }

        public void Select_Rows_From_DGVToBoxesAbove()
        {
            try
            {
                //txt_parcode_invoice.Text = DGV_INVOCE.SelectedRows[0].Cells["pARCODEDataGridViewTextBoxColumn"].Value.ToString();
                //txt_parcode.Text = DGV_INVOCE.SelectedRows[0].Cells["pARCODEDataGridViewTextBoxColumn"].Value.ToString();
                txt_parcode_invoice.Text = DGV_INVOCE.SelectedRows[0].Cells["pARCODEDataGridViewTextBoxColumn"].Value.ToString();
                txt_product_name_invoice.Text = DGV_INVOCE.SelectedRows[0].Cells["pRODUCTNAMEDataGridViewTextBoxColumn"].Value.ToString();
                cbo_units.Text = DGV_INVOCE.SelectedRows[0].Cells["uNITDataGridViewTextBoxColumn"].Value.ToString();
                qty.Text = DGV_INVOCE.SelectedRows[0].Cells["qTYDataGridViewTextBoxColumn"].Value.ToString();
                Tax.Text = DGV_INVOCE.SelectedRows[0].Cells["Taxing"].Value.ToString();
                txt_price.Text = DGV_INVOCE.SelectedRows[0].Cells["pRICEDataGridViewTextBoxColumn"].Value.ToString();
                txt_Total_Above.Text = DGV_INVOCE.SelectedRows[0].Cells["tOTALDataGridViewTextBoxColumn"].Value.ToString();
                Select_Quantity(txt_parcode_invoice);
                //txt_quantity.Text = DGV_INVOCE.SelectedRows[0].Cells["qTYDataGridViewTextBoxColumn"].Value.ToString();
                //txt_ID_ORDER_INVOICE.Text = DGV_INVOCE.SelectedRows[0].Cells[6].Value.ToString();
                parcode_2.Text = DGV_INVOCE.SelectedRows[0].Cells["pARCODEDataGridViewTextBoxColumn"].Value.ToString();
                product_name_2.Text = DGV_INVOCE.SelectedRows[0].Cells["pRODUCTNAMEDataGridViewTextBoxColumn"].Value.ToString();
                back_qty.Text = DGV_INVOCE.SelectedRows[0].Cells["qTYDataGridViewTextBoxColumn"].Value.ToString();          // 
                back_tax.Text = DGV_INVOCE.SelectedRows[0].Cells["Taxing"].Value.ToString();
                back_price.Text = DGV_INVOCE.SelectedRows[0].Cells["pRICEDataGridViewTextBoxColumn"].Value.ToString();      //
                units_2.Text = DGV_INVOCE.SelectedRows[0].Cells["uNITDataGridViewTextBoxColumn"].Value.ToString();
                back_total.Text = DGV_INVOCE.SelectedRows[0].Cells["tOTALDataGridViewTextBoxColumn"].Value.ToString();     //
            }
            catch  { }
        }

        public void btn_new_invoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_id.Text == Convert.ToString(max_id_TBL_ORDER()))
                {
                    if (MessageBox.Show("انتبة ربما تحذف الفاتورة", "تنبيه بالحذف", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        for (int i = 0; i < DGV_INVOCE.Rows.Count; i++)
                        {
                            while (DGV_INVOCE.Rows.Count > 0)
                            {
                                Search.Text = Convert.ToString(DGV_INVOCE.Rows[i].Cells["pARCODEDataGridViewTextBoxColumn"].Value);   // 0
                                qty.Text = Convert.ToString(DGV_INVOCE.Rows[i].Cells["qTYDataGridViewTextBoxColumn"].Value);                                        // 0           
                                Delete_Row();
                            }
                        }
                        chk_fill.Checked = true;
                        this.Close();
                        if (invoice == null)
                        {
                            invoice = new frm_Invoice();
                            invoice.Show();
                        }
                        else
                        { 
                            this.Close(); invoice = new frm_Invoice();
                            invoice.Show(); invoice.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else { return; }
                }
                else
                {
                    this.Close(); invoice = new frm_Invoice();
                    invoice.Show(); invoice.WindowState = FormWindowState.Maximized;
                }
            }
            catch  {  }
            finally
            {
                btn_Save_Without_Print.Visible = true;
            }
        }
        private void cbo_parcode_invoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void cbo_product_name_invoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void cbo_units_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_Qty_Ord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txt_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txt_price_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_price.Text.Trim() ==""|| txt_price.Text.Trim() == null) { txt_price.Text = "0"; txt_price.SelectAll(); }
                decimal txt_insert = 0; 
                if (txt_price.Text.Trim() != "")
                {
                    txt_insert = Convert.ToDecimal(txt_price.Text.Trim());
                }
                //else { txt_price.Text = "0"; }
                decimal qty_ = Convert.ToDecimal(qty.Text.Trim());
                decimal total = qty_ * txt_insert;
                txt_Total_Above.Text = Convert.ToString(total);
                decimal lessprice = 0;
                if (LESS_PRICE.Text.Trim() != "") { lessprice = Convert.ToDecimal(LESS_PRICE.Text.Trim()); }
                decimal true_price = 0;
                if (txt_true_price.Text.Trim() != "")
                {
                    true_price = Convert.ToDecimal(txt_true_price.Text.Trim());
                }
                if ( txt_insert >= lessprice)
                {
                    txt_price.BackColor = Color.White;
                    msg_invoice.Text = " ";
                }
                else if(txt_insert < lessprice)
                {
                    txt_price.BackColor = Color.Yellow;
                    msg_invoice.Text = "انتبه السعر المدخل اقل من سعر البيع";
                }
            } catch  {  }
        }

        private void cbo_customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fill_.Fill_cbo_Customer_index_changed(cbo_customer, txt_depit_Customer);
        }

        public void SaveWithOutPrint()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string selectID = "select ID from TBL_ORDER ";
                cmd = new SqlCommand(selectID, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (txt_id.Text.Trim() == dr["ID"].ToString())
                    {
                        MessageBox.Show("يرجى اختيار فاتورة جديدة", "تنبيه الفاتورة مدخلة من قبل");
                        dr.Close();
                        con.Close();
                        return;
                    }
                }
                dr.Close();
                con.Close();

                    if (DGV_INVOCE.Rows.Count <= 0)
                    {
                        MessageBox.Show("يرجى ملئ الفاتورة بالبيانات");
                    }
                    else
                    {
                        Save_Order("0");
                        Save_Order_Details();
                        msg_invoice.Text = " تم حفظ الفاتورة بنجاح ";
                    }
                Clear_Invoice();
                labeltrue();
                msg_invoice.Text = " تم حفظ الفاتورة بنجاح ";
            }
            catch 
            {


            }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                chk_fill.Checked = true;
            }
        }

        private void btn_Save_Without_Save_Click(object sender, EventArgs e)
        {
            SaveWithOutPrint();
        }

        public void labelfalse()
        {
            //label18.Visible = false;
            //label20.Visible = false;
            txt_parcode.Enabled = false;
            txt_product_name.Enabled = false;
            btn_add_product.Enabled = false;
            btn_delete_row.Enabled = false;
            btn_Save_Without_Print.Enabled = false;
            btn_SaveAndPrint.Enabled = false;
            Discount_percent.Enabled = false;
            txt_Discount_Money.Enabled = false;
            txt_pay.ReadOnly = true;
        }

        public void labelfalse_2()
        {
            txt_parcode.Enabled = false;
            txt_product_name.Enabled = false;
            btn_add_product.Enabled = false;
            btn_delete_row.Visible = false;
            btn_Save_Without_Print.Enabled = false;
            btn_SaveAndPrint.Enabled = false;
        }

        public void labeltrue()
        {
            //label18.Visible = false;
            //label20.Visible = false;
            txt_parcode.Enabled = true;
            txt_product_name.Enabled = true;
            btn_add_product.Enabled = true;
            btn_delete_row.Enabled = true;
            btn_Save_Without_Print.Enabled = true;
            btn_SaveAndPrint.Enabled = true;
            Discount_percent.Enabled = true;
            txt_Discount_Money.Enabled = true;
            txt_pay.ReadOnly = false;
        }

        private void btn_show_invoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_search.Text.Trim() == "" && txt_search.Text.Trim() == null && txt_ID_ORDER_INVOICE.Text.Trim() == "" && txt_ID_ORDER_INVOICE.Text.Trim() == null)
                {
                    MessageBox.Show("من فضلك إختر رقم الفاتورة المراد البحث عنه");
                }
                else
                if (txt_search.Text.Trim() != "" && chk_ID_auto.Checked)                                             /*  */
                {
                    //labelfalse();
                    txt_parcode.Text = "";
                    labelfalse_2();
                    Show_invoice("'+ID+'", txt_search);
                    Show_invoice_Details("'+Order_id+'", txt_search);
                }
                else
                if (txt_ID_ORDER_INVOICE.Text.Trim() != "" && chk_ID_hand.Checked)                                  //)
                {
                    //labelfalse();
                    txt_parcode.Text = "";
                    labelfalse_2();
                    Show_invoice("'+ID_Order_Invoice+'", txt_ID_ORDER_INVOICE);
                    Show_invoice_Details("'+ID_Order_Invoice+'", txt_ID_ORDER_INVOICE);
                }
            }
            catch (Exception EXC) { MessageBox.Show(EXC.Message); }
            finally
            {
                btn_Save_Without_Print.Visible = false;
            }
        }

        public void Show_invoice(string fld, TextBox txt)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                cmd = new SqlCommand("Select * from TBL_ORDER Where '" + fld + "' = N'" + txt.Text.Trim() + "' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txt_id.Text = dr["ID"].ToString();
                    txt_ID_ORDER_INVOICE.Text = dr["ID_Order_Invoice"].ToString();
                    txt_casheer.Text = dr["Casheer_number"].ToString();
                    cbo_user_invoice.Text = dr["USER_NAME"].ToString();
                    cbo_store.Text = dr["STORE_NAME"].ToString();
                    cbo_customer.Text = dr["CUSTOMER_NAME"].ToString();
                    cbo_pay_way.Text = dr["PAY_WAY"].ToString();
                    string Datestring = dr["DATE"].ToString();
                    date_invoice.Value = Convert.ToDateTime(Datestring);
                    txt_amount_of_product.Text = dr["Amount_of_product"].ToString();
                    txt_Qunt.Text = dr["TOTAL_BEFORE_REST"].ToString();
                    txt_Discount_value.Text = dr["Discount_value"].ToString();
                    Total_Discount.Text = dr["TOTAL"].ToString();
                    Total_.Text = dr["TOTAL"].ToString();
                    txt_pay.Text = dr["PAYING"].ToString();
                    //txt_The_rest.Text = dr["The_rest"].ToString();
                    Save_print.Text = dr["Save_print"].ToString();
                    txt_ID_ORDER_INVOICE.Text = dr["ID_Order_Invoice"].ToString();
                }
                dr.Close();
                con.Close();
            }
            catch  { }
        }

        public void Show_invoice_Details(string fld, TextBox txt)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                cmd = new SqlCommand("Select * from TBL_ORDER_DETAILS Where '" + fld + "' = N'" + txt.Text.Trim() + "' ", con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGV_INVOCE.DataSource = dt;
            }
            catch  { }
            finally
            {
                con.Close();
            }
        }


        private void btn_delete_invoice_Click(object sender, EventArgs e)
        {
            try
            {

                con.Open();
                string Query_1 = " DELETE FROM TBL_ORDER where ID = N'" + txt_id.Text.Trim() + "' ";
                cmd = new SqlCommand(Query_1, con);
                cmd.ExecuteNonQuery();
                con.Close();

                con.Open();
                string Query_2 = " DELETE FROM TBL_ORDER_DETAILS where Order_id = N'" + txt_id.Text.Trim() + "' ";
                cmd = new SqlCommand(Query_2, con);
                cmd.ExecuteNonQuery();

                msg_invoice.Text = "تم الحذف بنجاح";
            }
            finally { con.Close(); }
        }

        private void btn_Edite_Invoice_Click(object sender, EventArgs e)
        {
            Edite_invoice();
            //Edite_invoice_Details();
            MessageBox.Show("تم التعديل بنجاح");
        }

        public void Edite_invoice()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Update TBL_ORDER set ID_Order_Invoice=N'"+txt_ID_ORDER_INVOICE.Text.Trim()+"'," +
                    "Casheer_number=N'"+ txt_casheer.Text.Trim() + "'," +
                    "USER_NAME=N'"+ cbo_user_invoice.Text.Trim() + "'," +
                    "STORE_NAME=N'"+ cbo_store.Text.Trim() + "'," +
                    "CUSTOMER_NAME=N'" + cbo_customer.Text.Trim() + "'," +
                    "PAY_WAY=N'" + cbo_pay_way.Text.Trim() + "'," +
                    "DATE=N'" + date_invoice.Text.Trim() + "'," +
                    "Amount_of_product=N'" + txt_amount_of_product.Text.Trim() + "'," +
                    "TOTAL_BEFORE_REST=N'" + txt_Qunt.Text.Trim() + "'," +
                    "Discount_value=N'" + txt_Discount_value.Text.Trim() + "'," +
                    "TOTAL=N'" + Total_Discount.Text.Trim() + "'," +
                    "PAYING=N'" + txt_pay.Text.Trim() + "'," +
                    "The_rest=N'"+ txt_The_rest + "' Where ID = N'" + txt_id.Text.Trim() + "' ";
                cmd = new SqlCommand(Query, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@ID_Order_Invoice", txt_ID_ORDER_INVOICE.Text.Trim());
                cmd.Parameters.AddWithValue("@Casheer_number", txt_casheer.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_NAME ", cbo_user_invoice.Text.Trim());
                cmd.Parameters.AddWithValue("@STORE_NAME ", cbo_store.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_NAME ", cbo_customer.Text.Trim());
                cmd.Parameters.AddWithValue("@PAY_WAY ", cbo_pay_way.Text.Trim());
                cmd.Parameters.AddWithValue("@DATE ", date_invoice.Text.Trim());
                cmd.Parameters.AddWithValue("@Amount_of_product ", txt_amount_of_product.Text.Trim());
                cmd.Parameters.AddWithValue("@TOTAL_BEFORE_REST ", txt_Qunt.Text.Trim());
                cmd.Parameters.AddWithValue("@Discount_value ", txt_Discount_value.Text.Trim());
                cmd.Parameters.AddWithValue("@TOTAL ", Total_Discount.Text.Trim());
                cmd.Parameters.AddWithValue("@PAYING ", txt_pay.Text.Trim());
                cmd.Parameters.AddWithValue("@The_rest ", txt_The_rest.Text.Trim());
                con.Close();
            }
            catch  {  }
        }

        public void Edite_invoice_Details()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Update TBL_ORDER_DETAILS SET (PARCODE=@PARCODE,PRODUCT_NAME=@PRODUCT_NAME,UNIT=@UNIT,QTY=@QTY,PRICE=@PRICE,TOTAL=@TOTAL,Order_id=@Order_id) Where Order_id = N'" + txt_id.Text.Trim() + "' ";
                for (int i = 0; i < DGV_INVOCE.Rows.Count; i++)
                {
                    cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@PARCODE", DGV_INVOCE.Rows[i].Cells["pARCODEDataGridViewTextBoxColumn"].Value.ToString());
                    cmd.Parameters.AddWithValue("@PRODUCT_NAME", DGV_INVOCE.Rows[i].Cells["pRODUCTNAMEDataGridViewTextBoxColumn"].Value.ToString());
                    cmd.Parameters.AddWithValue("@UNIT", DGV_INVOCE.Rows[i].Cells["uNITDataGridViewTextBoxColumn"].Value.ToString());
                    cmd.Parameters.AddWithValue("@QTY", DGV_INVOCE.Rows[i].Cells["qTYDataGridViewTextBoxColumn"].Value.ToString());
                    cmd.Parameters.AddWithValue("@PRICE", DGV_INVOCE.Rows[i].Cells["pRICEDataGridViewTextBoxColumn"].Value.ToString());
                    cmd.Parameters.AddWithValue("@TOTAL", DGV_INVOCE.Rows[i].Cells["tOTALDataGridViewTextBoxColumn"].Value.ToString());
                    cmd.Parameters.AddWithValue("@Order_id", txt_id.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
            catch  { }
        }

        private void chk_ID_hand_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ID_hand.Checked)
            {
                txt_search.Text = "";
                chk_ID_auto.Checked = false;
                txt_ID_ORDER_INVOICE.ReadOnly = false;
                txt_search.ReadOnly = true;
            }
            else if (chk_ID_hand.CheckState == CheckState.Unchecked)
            {
                chk_ID_hand.Checked = false;
                txt_ID_ORDER_INVOICE.ReadOnly = true;
                txt_search.ReadOnly = false;
            }
        }

        private void chk_ID_auto_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ID_auto.Checked)
            {
                txt_ID_ORDER_INVOICE.Text = "";
                chk_ID_hand.Checked = false;
                txt_ID_ORDER_INVOICE.ReadOnly = true;
                txt_search.ReadOnly = false;
            }
            else if (chk_ID_auto.CheckState == CheckState.Unchecked)
            {
                chk_ID_auto.Checked = false;
                txt_ID_ORDER_INVOICE.ReadOnly = false;
                txt_search.ReadOnly = true;
            }
        }

        public void PlayBack()
        {
            for (int i = 0; i < DGV_INVOCE.Rows.Count; i++)
            {

                cmd.Parameters.AddWithValue("@PARCODE", DGV_INVOCE.Rows[i].Cells["pARCODEDataGridViewTextBoxColumn"].Value.ToString());
                cmd.Parameters.AddWithValue("@QTY", "QTY" + DGV_INVOCE.Rows[i].Cells["qTYDataGridViewTextBoxColumn"].Value.ToString());
            }
            con.Close();
        }

        private void txt_pay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) && e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            else if (txt_pay.Text.Trim() == "0") { txt_pay.SelectAll(); }
        }

        private void Discount_percent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) && e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_Discount_Money_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && (!char.IsDigit(e.KeyChar) && e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txt_ID_ORDER_INVOICE_TextChanged(object sender, EventArgs e)
        {

        }
        public void ClearAbove()
        {
            txt_parcode_invoice.Text = "";
            txt_product_name_invoice.Text = "";
            cbo_units.Text = "";
            qty.Text = "";
            Tax.Text = "";
            txt_price.Text = "";
            txt_Total_Above.Text = "";
            txt_quantity.Text = "";
        }

        
        private void txt_parcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                insert_into_txt_parcode(txt_parcode);
                btn_Save_Without_Print.Visible = true;
                if (chk_fill.Checked && txt_parcode_invoice.Text.Trim() !="" && txt_product_name_invoice.Text.Trim() != "")
                {
                    ADDBtn();  
                }
                //txt_parcode.SelectAll();
            }
            catch  {  }
        }

        private void txt_product_name_TextChanged(object sender, EventArgs e)
        {
            if (txt_product_name.Text.Trim() == "") { ClearAbove(); return; }
            Insert_Name(txt_product_name);
            if (chk_fill.Checked && txt_parcode_invoice.Text.Trim() != "" && txt_product_name_invoice.Text.Trim() != "")
            {
                ADDBtn(); // return;
            }
            //txt_product_name.SelectAll();
        }

        private void txt_Total_Above_TextChanged(object sender, EventArgs e)
        {
            //fill_product_minus();
        }

        public void insert_into_txt_parcode(TextBox txt_parcode)
        {
            try
            {
                    if (txt_parcode.Text.Trim() == "") { LESS_PRICE.Text = "0"; ClearAbove(); return; }
                    
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    string query = "SELECT ID,PARCODE,PRODUCT_NAME,UNITS,PRICE,LESS_PRICE,Quantity,Items_Number,Taxing FROM TBL_PRODUCTS WHERE PARCODE = N'" + txt_parcode.Text.Trim() + "' ";
                    cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        id_product.Text = dt.Rows[0]["ID"].ToString();
                        id_fk.Text = "";
                        txt_parcode_invoice.Text = dt.Rows[0]["PARCODE"].ToString();
                        txt_product_name_invoice.Text = dt.Rows[0]["PRODUCT_NAME"].ToString();
                        cbo_units.Text = dt.Rows[0]["UNITS"].ToString();
                        txt_true_price.Text = dt.Rows[0]["PRICE"].ToString();                 //  السعر الثابت الذي لا يتغير
                        txt_price.Text = dt.Rows[0]["PRICE"].ToString();
                        //txt_Total_Above.Text = dt.Rows[0]["PRICE"].ToString();
                        LESS_PRICE.Text= dt.Rows[0]["LESS_PRICE"].ToString();
                        txt_quantity.Text = dt.Rows[0]["Quantity"].ToString();      //
                        qty.Text = dt.Rows[0]["Items_Number"].ToString();
                        Tax.Text = dt.Rows[0]["Taxing"].ToString();
                        txt_product_parcode.Text = dt.Rows[0]["PARCODE"].ToString();
                        id_fk.Text = "";
                        btn_delete_row.Enabled = false;
                        btn_delete_row.BackColor = Color.White;
                    }
                    else
                    {
                        // الدخول الي جدول الاصناف الثاني لجلب البيانات
                        if (con.State == ConnectionState.Open) { con.Close(); }
                        con.Open();
                        string query2 = "SELECT PARCODE_2,PRODUCT_NAME,UNITS,PRICE,LESS_PRICE,QUANTITY,Items_Number,Taxing,FORIEGN_KEY_ID,ID FROM NEW_PRODUCTS WHERE PARCODE_2 = N'" + txt_parcode.Text.Trim() + "' ";
                        SqlCommand cmd2 = new SqlCommand(query2, con);
                        cmd2.ExecuteNonQuery();
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        DataTable dt2 = new DataTable();
                        da2.Fill(dt2);
                        if (dt2.Rows.Count > 0)
                        {
                            id_product.Text = "";
                            id_fk.Text = dt2.Rows[0]["FORIEGN_KEY_ID"].ToString();
                            txt_parcode_invoice.Text = dt2.Rows[0]["PARCODE_2"].ToString();
                            txt_product_name_invoice.Text = dt2.Rows[0]["PRODUCT_NAME"].ToString();
                            cbo_units.Text = dt2.Rows[0]["UNITS"].ToString();
                            txt_true_price.Text = dt2.Rows[0]["PRICE"].ToString();                 //  السعر الثابت الذي لا يتغير
                            txt_price.Text = dt2.Rows[0]["PRICE"].ToString();
                            //txt_Total_Above.Text = dt2.Rows[0]["PRICE"].ToString();

                            LESS_PRICE.Text = dt.Rows[0]["LESS_PRICE"].ToString();
                            txt_quantity.Text = dt2.Rows[0]["QUANTITY"].ToString();
                            qty.Text = dt2.Rows[0]["Items_Number"].ToString();
                            Tax.Text = dt2.Rows[0]["Taxing"].ToString();
                            txt_product_parcode.Text = dt2.Rows[0]["PARCODE_2"].ToString();
                            btn_delete_row.Enabled = false;
                            btn_delete_row.BackColor = Color.White;
                        }
                    }
                
            }
            catch  {  }
            finally { con.Close(); }
        }

        public void Insert_Name(TextBox txtproduct_name)
        {
            try
            {
                
                //else
                //{
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string query = "SELECT ID,PARCODE,PRODUCT_NAME,UNITS,PRICE,LESS_PRICE,QUANTITY,Items_Number,Taxing,PARCODE FROM TBL_PRODUCTS WHERE PRODUCT_NAME = N'" + txtproduct_name.Text.Trim() + "' ";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    id_product.Text = dt.Rows[0]["ID"].ToString();
                    id_fk.Text = "";
                    txt_product_parcode.Text = dt.Rows[0]["ID"].ToString();
                    txt_parcode_invoice.Text = dt.Rows[0]["PARCODE"].ToString();
                    txt_product_name_invoice.Text = dt.Rows[0]["PRODUCT_NAME"].ToString();
                    cbo_units.Text = dt.Rows[0]["UNITS"].ToString();
                    txt_true_price.Text = dt.Rows[0]["PRICE"].ToString();                     //  السعر الثابت الذي لا يتغير
                    txt_price.Text = dt.Rows[0]["PRICE"].ToString();
                    //txt_Total_Above.Text = dt.Rows[0]["PRICE"].ToString();
                    LESS_PRICE.Text = dt.Rows[0]["LESS_PRICE"].ToString();
                    txt_quantity.Text = dt.Rows[0]["Quantity"].ToString();
                    qty.Text = dt.Rows[0]["Items_Number"].ToString();
                    Tax.Text = dt.Rows[0]["Taxing"].ToString();
                    txt_product_parcode.Text = dt.Rows[0]["PARCODE"].ToString();
                    btn_delete_row.Enabled = false;
                    btn_delete_row.BackColor = Color.White;
                }
                else
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    string query2 = "SELECT PARCODE_2,PRODUCT_NAME,UNITS,PRICE,LESS_PRICE,QUANTITY,Items_Number,Taxing,FORIEGN_KEY_ID,ID FROM NEW_PRODUCTS WHERE PRODUCT_NAME = N'" + txtproduct_name.Text.Trim() + "' ";
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();

                    while (dr2.Read())
                    {
                        id_product.Text = "";
                        id_fk.Text = dr2["FORIEGN_KEY_ID"].ToString();
                        txt_product_parcode.Text = dr2["ID"].ToString();
                        txt_parcode_invoice.Text = dr2["PARCODE_2"].ToString();
                        txt_product_name_invoice.Text = dr2["PRODUCT_NAME"].ToString();
                        cbo_units.Text = dr2["UNITS"].ToString();
                        txt_true_price.Text = dr2["PRICE"].ToString();                       //  السعر الثابت الذي لا يتغير
                        txt_price.Text = dr2["PRICE"].ToString();
                        //txt_Total_Above.Text = dt.Rows[0]["PRICE"].ToString();
                        LESS_PRICE.Text = dt.Rows[0]["LESS_PRICE"].ToString();
                        txt_quantity.Text = dr2["QUANTITY"].ToString();
                        qty.Text = dr2["Items_Number"].ToString();
                        Tax.Text = dr2["Taxing"].ToString();
                        txt_product_parcode.Text = dr2["PARCODE_2"].ToString();
                    }
                    btn_delete_row.Enabled = false;
                    btn_delete_row.BackColor = Color.White;
                }
                //}
            }
            catch  { }
            finally { con.Close(); }
        }

        private void Search_TextChanged(object sender, EventArgs e)
        {
            insert_into_txt_parcode(Search);
            btn_delete_invoice.Enabled = false;
        }
        public void Fill_search()   // للاختبار فقط
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string query = "SELECT ID,PARCODE,PRODUCT_NAME,Quantity,Items_Number FROM TBL_PRODUCTS WHERE PARCODE = N'" + Search.Text.Trim() + "' ";
                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    id_product.Text = "ID";
                    txt_parcode_invoice.Text = dt.Rows[0]["PARCODE"].ToString();
                    txt_product_name_invoice.Text = dt.Rows[0]["PRODUCT_NAME"].ToString();
                    txt_quantity.Text = dt.Rows[0]["Quantity"].ToString();
                    qty.Text = dt.Rows[0]["Items_Number"].ToString();
                    id_fk.Text = "";
                    con.Close();
                }
                else
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    string q = "SELECT PARCODE_2,PRODUCT_NAME,Quantity,Items_Number FROM NEW_PRODUCTS WHERE PARCODE_2 = N'" + Search.Text.Trim() + "' ";
                    cmd = new SqlCommand(q, con);
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    if (dt1.Rows.Count > 0)
                    {
                        txt_parcode_invoice.Text = dt1.Rows[0]["PARCODE_2"].ToString();
                        txt_product_name_invoice.Text = dt.Rows[0]["PRODUCT_NAME"].ToString();
                        txt_quantity.Text = dt1.Rows[0]["Quantity"].ToString();
                        qty.Text = dt1.Rows[0]["Items_Number"].ToString();
                        id_product.Text = "";
                        id_fk.Text = dt1.Rows[0]["FORIEGN_KEY_ID"].ToString();
                        con.Close();
                    }
                }
            }
            catch  { }
            finally { if (con.State == ConnectionState.Open) { con.Close(); } }
        }

        private void frm_Invoice_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select invoice_open,invoice_save,invoice_new From USERS_PERMESSIONS Where USER_NAME = N'" + cbo_user_invoice.Text.Trim() + "' ";
                SqlCommand cmd22 = new SqlCommand(Query, con);
                SqlDataReader dr2 = cmd22.ExecuteReader();
                if (dr2.Read())
                {
                    if (dr2["invoice_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr2["invoice_save"].ToString() == "True") { btn_Save_Without_Print.Enabled = true; } else { btn_Save_Without_Print.Enabled = false; }
                    if (dr2["invoice_new"].ToString() == "True") { btn_new_invoice.Enabled = true; } else { btn_new_invoice.Enabled = false; }
                }
                dr2.Close();
            }
            catch  { }
            finally { con.Close(); }
        }

        private void frm_Invoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11 && DGV_INVOCE.Rows.Count > 0)
            {
                Print_key();                    // طباعة الفاتورة الصغيرة
            } 
            else if (e.KeyCode == Keys.F10 && DGV_INVOCE.Rows.Count > 0)
            {
                Print_key_Bigform();
            }
            else if (e.KeyCode == Keys.F9 && DGV_INVOCE.Rows.Count > 0)
            {
                PrintAuto();                    //الطباعة مع ظهور الاعدادات
            }
            else 
            if (e.KeyCode == Keys.F1 && DGV_INVOCE.Rows.Count > 0)
            {
                SaveWithOutPrint();             //حفظ 
            }
            else
            if (e.KeyCode == Keys.Escape)
            {
                Close_Invoice();
            }
            else
            if (e.KeyCode == Keys.F7 && DGV_INVOCE.Rows.Count > 0)
            {
                Print_key_BigformF7();
            }
        }

        private void Tax_TextChanged(object sender, EventArgs e)
        {
            if (Tax.Text.Trim() == "") { Tax.Text = "0"; }
        }
    }
}
