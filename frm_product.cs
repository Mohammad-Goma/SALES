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

    public partial class frm_product : Form
    {
        public Fill_Cbo fill_cbo = new Fill_Cbo();
        ResizeTools resiz = new ResizeTools();
        Boolean Test_Size = false;

        Boolean col1 = false;
        Boolean col2 = false;

        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd;

        private delegate void EnableDelegate(bool enable);

        public frm_product()
        {
            InitializeComponent();
        }
        //public void fill()
        //{
        //    ClearAll();
        //    cbo_search_par.Items.Clear();
        //    cbo_name_sear.Items.Clear();
        //    con.Open();
        //    SqlCommand Cmd = new SqlCommand("SELECT * FROM TBL_products", con);
        //    SqlDataReader Reader = Cmd.ExecuteReader();
        //    while (Reader.Read())
        //    {
        //        cbo_search_par.Items.Add(Reader["PARCODE"].ToString());    ///   BRANCH_NAME   هنا نغير اسم العمود المراد ظهوره
        //        cbo_name_sear.Items.Add(Reader["PRODUCT_NAME"].ToString());
        //    }
        //    Reader.Close();
        //    con.Close();
        //}   

        private void Frm_product_Load(object sender, EventArgs e)
        {
            resiz.Container = grop_product;
            resiz.FullRatioTable();
            fill_cbo.fill_2product_tblByID(txt_search_parcode);
            fill_cbo.fill_2product_tblByName(txt_name_search);
            fill_cbo.fill_Categories(cbo_categories);
            fill_cbo.fill_stores_Data(cbo_stores);
            fill_cbo.Fill_Branches(cbo_branch_product);
            fill_cbo.Fill_Units(cbo_units);
            fill_cbo.Fill_Units(cbo_unit_2);
            fill_cbo.fill_company(cbo_company_product);
            fill_cbo.Fill_Billing(txt_parcode);
            fill_cbo.Fill_Deleget(cbo_Delegete);
            //fill_new_product();
            txt_lvl_product.Text = Main_Form.PASSWORD_STRING.Trim();
            txt_user.Text = Main_Form.USER_NAME_STRING.Trim(); 
            free_id_product();
        }

        private void Frm_product_Shown(object sender, EventArgs e)
        {
            Test_Size = true; lvl_permessions();
        }
        public void lvl_permessions( )
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select products_open,products_save,products_delete,products_new From USERS_PERMESSIONS Where USER_NAME=N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader dr7 = cmd.ExecuteReader();
                if (dr7.Read())
                {
                    if (dr7["products_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr7["products_save"].ToString() == "True") { } else { btn_save.Enabled = false; }
                    if (dr7["products_delete"].ToString() == "True") { } else { btn_delete.Enabled = false; }
                    if (dr7["products_new"].ToString() == "True") { } else { btn_new.Enabled = false; }
                }
                dr7.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }
        private void Frm_product_Resize(object sender, EventArgs e)
        {
            if (Test_Size)
            { resiz.ResizeControls(); }
        }

        private void Btn_add_image_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog of = new OpenFileDialog();
                of.Title = "أختر صورة من هنا";
                of.Filter = "Image Files|*.jpg;*.png;*.bmp;*.*;(*.*);";
                of.Multiselect = true;
                if (of.ShowDialog() == DialogResult.OK)
                {
                    product_image.Image = new Bitmap(of.FileName);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        public void Save()
        {
            try
            {
                select_parcode_product(txt_parcode);
                select_parcode_product2(txt_parcode);

                if (col1 == true || col2 == true)
                { MessageBox.Show("رقم الباركود هذا موجود مسبقاً"); col1 = false; col2 = false; return; }

                con.Open();
                cmd = new SqlCommand("select max(ID) as fld_max from TBL_products ", con);
                cmd.ExecuteNonQuery();

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                int max;
                DataRow dtr;
                dtr = ds.Tables[0].Rows[0];
                max = Convert.ToInt32(dtr["fld_max"].ToString());
                max++;
                con.Close();

                MemoryStream ms = new MemoryStream();
                product_image.Image.Save(ms, product_image.Image.RawFormat);
                byte[] byteImage = ms.ToArray();

                DateTime dateTime = DateTime.Parse(this.date_product_now.Text);

                con.Open();
                string query = " INSERT INTO TBL_products VALUES(N'" + max + "', N'" + cbo_branch_product.Text.Trim() + "'," +
                    "N'" + cbo_stores.Text.Trim() + "',N'" + cbo_categories.Text.Trim() + "',N'" + txt_parcode.Text.Trim() + "'," +
                    "N'" + txt_product_name.Text.Trim() + "',N'" + cbo_units.Text.Trim() + "',N'" + txt_first_cost.Text.Trim() + "'," +
                    "N'" + txt_price.Text.Trim() + "',N'" + txt_less_price.Text.Trim() + "',N'" + txt_taxing.Text.Trim() + "'," +
                    "N'" + txt_quantity.Text.Trim() + "',N'" + txt_stop.Text.Trim() + "',N'" + cbo_Delegete.Text.Trim() + "'," +
                    "N'" + txt_accept_back.Text.Trim() + "',N'" + cbo_Security_year.Text.Trim() + "',N'" + cbo_Security_Month.Text.Trim() + "'," +
                    "N'" + cbo_Repairs.Text.Trim() + "',N'" + cbo_Method_of_sale.Text.Trim() + "',N'" + cbo_reserve.Text.Trim() + "' ," +
                    "N'" + cbo_company_product.Text.Trim() + "','byteImage',N'" + txt_user.Text.Trim() + "',N'" + txt_edite_user.Text.Trim() + "'," +
                    "N'" + date_product_now.Text.Trim() + "',N'" + txt_last_edite_day.Text.Trim() + "',N'" + Items_Number.Text.Trim() + "'," +
                    "N'" + branch_id.Text.Trim() + "',N'" + store_id.Text.Trim() + "'," +
                    "N'" + categorey_id.Text.Trim() + "',N'" + unite_id.Text.Trim() + "',N'" + deleget_id.Text.Trim() + "'," +
                    "N'" + txt_details.Text.Trim() + "' ) ";

                cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();

                cmd.Parameters.AddWithValue("@Id", max);
                cmd.Parameters.AddWithValue("@BRANCH", cbo_branch_product.Text.Trim());
                cmd.Parameters.AddWithValue("@STORE", cbo_stores.Text.Trim());
                cmd.Parameters.AddWithValue("@SECTIONS", cbo_categories.Text.Trim());
                cmd.Parameters.AddWithValue("@PARCODE", txt_parcode.Text.Trim());
                cmd.Parameters.AddWithValue("@PRODUCT_NAME", txt_product_name.Text.Trim());
                cmd.Parameters.AddWithValue("@UNITS", cbo_units.Text.Trim());
                cmd.Parameters.AddWithValue("@INITIAL_COST", txt_first_cost.Text.Trim());
                cmd.Parameters.AddWithValue("@PRICE", txt_price.Text.Trim());
                cmd.Parameters.AddWithValue("@LESS_PRICE", txt_less_price.Text.Trim());
                cmd.Parameters.AddWithValue("@TAXING", txt_taxing.Text.Trim());
                cmd.Parameters.AddWithValue("@Quantity", txt_quantity.Text.Trim());
                cmd.Parameters.AddWithValue("@STOP", txt_stop.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGET", cbo_Delegete.Text.Trim());
                cmd.Parameters.AddWithValue("@Flashback", txt_accept_back.Text.Trim());
                cmd.Parameters.AddWithValue("@Item_WARRANTY_YEAR", cbo_Security_year.Text.Trim());
                cmd.Parameters.AddWithValue("@Item_WARRANTY_MONTH", cbo_Security_Month.Text.Trim());
                cmd.Parameters.AddWithValue("@MAINTENANCE", cbo_Repairs.Text.Trim());
                cmd.Parameters.AddWithValue("@Method_of_sale", cbo_Method_of_sale.Text.Trim());
                cmd.Parameters.AddWithValue("@Item_reserved", cbo_reserve.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANIES", cbo_company_product.Text.Trim());
                cmd.Parameters.AddWithValue("@IMAG", byteImage);
                cmd.Parameters.AddWithValue("@USER_NAME_INSERT", txt_user.Text.Trim());
                //cmd.Parameters.AddWithValue("@USER_NAME_EDITS", txt_edite_user.Text.Trim());
                cmd.Parameters.AddWithValue("@DATE_OF_INSERT", date_product_now.Text.Trim());
                //cmd.Parameters.AddWithValue("@DATE_OF_Edits", txt_last_edite_day.Text.Trim());
                cmd.Parameters.AddWithValue("@Items_Number", Items_Number.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_BRANCH", branch_id.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_STORE", store_id.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_CATEGORY", categorey_id.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_UNIT", unite_id.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_DELEGET", deleget_id.Text.Trim());
                cmd.Parameters.AddWithValue("@DETAILS", txt_details.Text.Trim());

                txt_search_parcode.Text = "";
                txt_name_search.Text = "";
                fill_cbo.fill_2product_tblByID(txt_search_parcode);
                fill_cbo.fill_2product_tblByName(txt_name_search);
                txt_msg_product.Text = "تم الحفظ بنجاح";
            }
            catch (Exception) { }
            finally
            {
                con.Close();
            }
        }

        private void Btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_parcode.Text.Trim() == "" || txt_product_name.Text.Trim() == "" || cbo_units.Text.Trim() == "" || txt_first_cost.Text.Trim() == "" || txt_price.Text.Trim() == "" || txt_less_price.Text.Trim() == "" || txt_quantity.Text.Trim() == "")
                { txt_msg_product.Text = "من فضلك املأ البيانات الملونة "; }
                else
                {
                    if (txt_id_product.Text.Trim() == "" && txt_parcode.Text.Trim() != "" && txt_product_name.Text.Trim() != "")
                    { Save(); }
                    else if (txt_id_product.Text.Trim() != "")
                    { Update_quantity(); Edite_product();  }
                    else { txt_msg_product.Text = "يرجى ادخال البيانات"; }
                }
            } catch (Exception) { }
            finally { col1 = false; col2 = false; if (con.State == ConnectionState.Open) { con.Close(); } }
        }

        public void ClearAll()
        {
            txt_id_product.Text = "";
            txt_date_insert.Text = "";
            txt_accept_back.Text = "";
            cbo_Delegete.Text = "";
            txt_details.Text = "";
            txt_last_edite_day.Text = "";
            txt_less_price.Text = "";
            txt_msg_product.Text = "";
            txt_parcode.Text = "";
            cbo_units.Text = "";
            txt_price.Text = "";
            txt_quantity.Text = "";
            txt_stop.Text = "";
            txt_taxing.Text = "0";
            txt_user_insert.Text = "";
            txt_user_insert.Text = "";
            txt_product_name.Text = "";
            txt_first_cost.Text = "";
            Items_Number.Text = "1";
            tax_percent.Text = "0";
            cbo_company_product.Text = "";
            //txt_search_parcode.Text = "";
            //txt_name_search.Text = "";
            Clearnew();
            Selectdeleget(txt_id_product);
            deleget_names.Text = "";
            txt_id_deleget.Text = "";
            Select_NEW_PRODUCTS();
            fill_cbo.fill_2product_tblByID(txt_search_parcode);
            fill_cbo.fill_2product_tblByName(txt_name_search);
        }

        public void Edite_product()
        {
            MemoryStream ms = new MemoryStream();
            product_image.Image.Save(ms, product_image.Image.RawFormat);
            byte[] byteImage = ms.ToArray();

            txt_box_new_parcode.BackColor = Color.White;
            select_parcode_product(txt_box_new_parcode);
            select_parcode_product2(txt_box_new_parcode);
            if (col1 == true || col2 == true)
            { MessageBox.Show("رقم الباركود هذا موجود مسبقاً"); col1 = false; col2 = false; return; }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE TBL_products set BRANCH = N'" + cbo_branch_product.Text.Trim() + "'," +
                        "STORE = N'" + cbo_stores.Text.Trim() + "', SECTIONS = N'" + cbo_categories.Text.Trim() + "', PARCODE=N'" + txt_parcode.Text.Trim() + "'," +
                        "PRODUCT_NAME = N'" + txt_product_name.Text.Trim() + "', UNITS = N'" + cbo_units.Text.Trim() + "', INITIAL_COST=N'" + txt_first_cost.Text.Trim() + "'," +
                        "PRICE = N'" + txt_price.Text.Trim() + "', LESS_PRICE = N'" + txt_less_price.Text.Trim() + "', TAXING=N'" + txt_taxing.Text.Trim() + "'," +
                        "Quantity = N'" + txt_quantity.Text.Trim() + "', STOP = N'" + txt_stop.Text.Trim() + "', DELEGET=N'" + cbo_Delegete.Text.Trim() + "'," +
                        "Flashback = N'" + txt_accept_back.Text.Trim() + "', Item_WARRANTY_YEAR = N'" + cbo_Security_year.Text.Trim() + "'," +
                        "Item_WARRANTY_MONTH=N'" + cbo_Security_Month.Text.Trim() + "', MAINTENANCE = N'" + cbo_Repairs.Text.Trim() + "', " +
                        "Method_of_sale = N'" + cbo_Method_of_sale.Text.Trim() + "' , Item_reserved=N'" + cbo_reserve.Text.Trim() + "'," +
                        "COMPANIES = N'" + cbo_company_product.Text.Trim() + "', IMAG = 'byteImage', USER_NAME_INSERT=N'" + txt_user_insert.Text.Trim() + "'," +
                        "USER_NAME_EDITS=N'" + txt_user.Text.Trim() + "', DATE_OF_INSERT = N'" + txt_date_insert.Text.Trim() + "', " +
                        "DATE_OF_Edits = N'" + date_product_now.Text.Trim() + "',Items_Number = N'" + Items_Number.Text.Trim() + "',ID_BRANCH = N'" + branch_id.Text.Trim() + "'," +
                        "ID_STORE = N'" + store_id.Text.Trim() + "',ID_CATEGORY = N'" + categorey_id.Text.Trim() + "'," +
                        "ID_UNIT = N'" + unite_id.Text.Trim() + "',ID_DELEGET = N'" + deleget_id.Text.Trim() + "'," +
                        "DETAILS = N'" + txt_details.Text.Trim() + "'  where ID = N'" + txt_id_product.Text.Trim() + "' ", con);

                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@BRANCH", cbo_branch_product.Text.Trim());
                cmd.Parameters.AddWithValue("@STORE", cbo_stores.Text.Trim());
                cmd.Parameters.AddWithValue("@SECTIONS", cbo_categories.Text.Trim());
                cmd.Parameters.AddWithValue("@PARCODE", txt_parcode.Text.Trim());
                cmd.Parameters.AddWithValue("@PRODUCT_NAME", txt_product_name.Text.Trim());
                cmd.Parameters.AddWithValue("@UNITS", cbo_units.Text.Trim());
                cmd.Parameters.AddWithValue("@INITIAL_COST", txt_first_cost.Text.Trim());
                cmd.Parameters.AddWithValue("@PRICE", txt_price.Text.Trim());
                cmd.Parameters.AddWithValue("@LESS_PRICE", txt_less_price.Text.Trim());
                cmd.Parameters.AddWithValue("@TAXING", txt_taxing.Text.Trim());
                cmd.Parameters.AddWithValue("@Quantity", txt_quantity.Text.Trim());
                cmd.Parameters.AddWithValue("@STOP", txt_stop.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGET", cbo_Delegete.Text.Trim());
                cmd.Parameters.AddWithValue("@Flashback", txt_accept_back.Text.Trim());
                cmd.Parameters.AddWithValue("@Item_WARRANTY_YEAR", cbo_Security_year.Text.Trim());
                cmd.Parameters.AddWithValue("@Item_WARRANTY_MONTH", cbo_Security_Month.Text.Trim());
                cmd.Parameters.AddWithValue("@MAINTENANCE", cbo_Repairs.Text.Trim());
                cmd.Parameters.AddWithValue("@Method_of_sale", cbo_Method_of_sale.Text.Trim());
                cmd.Parameters.AddWithValue("@Item_reserved", cbo_reserve.Text.Trim());
                cmd.Parameters.AddWithValue("@COMPANIES", cbo_company_product.Text.Trim());
                cmd.Parameters.AddWithValue("@IMAG", byteImage);
                //cmd.Parameters.AddWithValue("@USER_NAME_INSERT", txt_user.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_NAME_EDITS", txt_edite_user.Text.Trim());
                //cmd.Parameters.AddWithValue("@DATE_OF_INSERT", date_product_now.Text.Trim());
                cmd.Parameters.AddWithValue("@DATE_OF_Edits", date_product_now.Text.Trim());
                cmd.Parameters.AddWithValue("@Items_Number", Items_Number.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_BRANCH", branch_id.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_STORE", store_id.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_CATEGORY", categorey_id.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_UNIT", unite_id.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_DELEGET", deleget_id.Text.Trim());
                cmd.Parameters.AddWithValue("@DETAILS", txt_details.Text.Trim());
                con.Close();
                //txt_search_parcode.Text = "";
                //txt_name_search.Text = "";
                fill_cbo.fill_2product_tblByID(txt_search_parcode);
                fill_cbo.fill_2product_tblByName(txt_name_search);
                fill_new_product();
                Select_NEW_PRODUCTS();
                txt_msg_product.Text = "تم التعديل بنجاح";
            }
        }

        public void Update_quantity()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string sqlq = "update NEW_PRODUCTS set QUANTITY = N'" + txt_quantity.Text.Trim() + "' where FORIEGN_KEY_ID =N'"+ txt_id_product.Text.Trim() + "' ";
                SqlCommand cmdq = new SqlCommand(sqlq, con);
                cmdq.ExecuteNonQuery();
                cmdq.Parameters.AddWithValue("@QUANTITY", txt_quantity.Text.Trim());
            }
            catch (Exception)
            {
            }
            finally { con.Close(); }
        }
        private void Btn_new_Click(object sender, EventArgs e)
        {
            ClearAll();
            txt_search_parcode.Text = "";
            txt_name_search.Text = "";
            //Clearnew();
            //ClearNewDeleget();
            //Selectdeleget(txt_id_product);
            //fill_new_product();
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_id_product.Text.Trim() != "")
            {
                con.Open();
                cmd = new SqlCommand("DELETE FROM TBL_products WHERE ID = N'" + txt_id_product.Text.Trim() + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                txt_msg_product.Text = "تم الحذف بنجاح";
                txt_search_parcode.Text = "";
                txt_name_search.Text = "";
                fill_cbo.fill_2product_tblByID(txt_search_parcode);
                fill_cbo.fill_2product_tblByName(txt_name_search);
                ClearAll();
            }
            else
            {
                txt_msg_product.Text = "يرجى تحديد الصنف المراد حذفه";
            }
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public Image Show_image(object FLD_Picture)
        {
                byte[] data = (byte[])FLD_Picture;
                MemoryStream PIC = new MemoryStream(data);
                PIC.Write(data, 0, data.Length);
                Image returnImage = Image.FromStream(PIC);
                return returnImage;
        }

       
        public void Fill_txt_product(string fld, TextBox txt)
        {
            try
            {
                con.Open();
                string query = "SELECT ID,BRANCH,STORE,SECTIONS,PARCODE,PRODUCT_NAME,UNITS,INITIAL_COST,PRICE,LESS_PRICE,TAXING,Quantity,DELEGET," +
                    "COMPANIES,IMAG,USER_NAME_INSERT,USER_NAME_EDITS,DATE_OF_INSERT,DATE_OF_Edits,DETAILS FROM TBL_products WHERE '" + fld + "' = N'"+txt.Text.Trim()+"' ";
                SqlCommand cmd5 = new SqlCommand(query, con);
                SqlDataAdapter da5 = new SqlDataAdapter(cmd5);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                if(dt5.Rows.Count > 0)
                {
                    txt_id_product.Text = dt5.Rows[0]["ID"].ToString();
                    cbo_branch_product.Text = dt5.Rows[0]["BRANCH"].ToString();
                    cbo_stores.Text = dt5.Rows[0]["STORE"].ToString();
                    cbo_categories.Text = dt5.Rows[0]["SECTIONS"].ToString();
                    txt_parcode.Text = dt5.Rows[0]["PARCODE"].ToString();
                    txt_product_name.Text = dt5.Rows[0]["PRODUCT_NAME"].ToString();
                    cbo_units.Text = dt5.Rows[0]["UNITS"].ToString();
                    txt_first_cost.Text = dt5.Rows[0]["INITIAL_COST"].ToString();
                    txt_price.Text = dt5.Rows[0]["PRICE"].ToString();
                    txt_less_price.Text = dt5.Rows[0]["LESS_PRICE"].ToString();
                    txt_taxing.Text = dt5.Rows[0]["TAXING"].ToString();
                    txt_quantity.Text = dt5.Rows[0]["Quantity"].ToString();
                    //txt_stop.Text = dr["STOP"].ToString();
                    cbo_Delegete.Text = dt5.Rows[0]["DELEGET"].ToString();
                    //cbo_Security_year.Text = dr["Item_WARRANTY_YEAR"].ToString();
                    //cbo_Security_Month.Text = dr["Item_WARRANTY_MONTH"].ToString();
                    //cbo_Repairs.Text = dr["MAINTENANCE"].ToString();
                    //cbo_Method_of_sale.Text = dr["METHOD_OF_SALE"].ToString();
                    //cbo_reserve.Text = dr["Item_reserved"].ToString();
                    cbo_company_product.Text = dt5.Rows[0]["COMPANIES"].ToString();
                    //try
                    //{
                    //    product_image.Image = Show_image(dt5.Rows[0]["IMAG"]);
                    //}
                    //catch (Exception)
                    //{
                    //    //product_image.Image = null;
                    //}
                    txt_user_insert.Text = dt5.Rows[0]["USER_NAME_INSERT"].ToString();
                    txt_edite_user.Text = dt5.Rows[0]["USER_NAME_EDITS"].ToString();
                    txt_date_insert.Text = dt5.Rows[0]["DATE_OF_INSERT"].ToString();
                    txt_last_edite_day.Text = dt5.Rows[0]["DATE_OF_Edits"].ToString();
                    //DGV_PRODUCT.Text = dr["OTHER_PRODUCTS"].ToString();
                    txt_details.Text = dt5.Rows[0]["DETAILS"].ToString();
                }
            }
            catch (Exception)
            {
                
            }
            finally { con.Close(); }
        }

        public void Fill_txt_name()
        {
            try
            {
                con.Open();
                string query = "SELECT * FROM TBL_products WHERE PRODUCT_NAME = N'" + txt_name_search.Text.Trim() + "' ";
                cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txt_id_product.Text = dr["ID"].ToString();
                    cbo_branch_product.Text = dr["BRANCH"].ToString();
                    cbo_stores.Text = dr["STORE"].ToString();
                    cbo_categories.Text = dr["SECTIONS"].ToString();
                    txt_parcode.Text = dr["PARCODE"].ToString();
                    txt_product_name.Text = dr["PRODUCT_NAME"].ToString();
                    cbo_units.Text = dr["UNITS"].ToString();
                    txt_first_cost.Text = dr["INITIAL_COST"].ToString();
                    txt_price.Text = dr["PRICE"].ToString();
                    txt_less_price.Text = dr["LESS_PRICE"].ToString();
                    txt_taxing.Text = dr["TAXING"].ToString();
                    txt_quantity.Text = dr["Quantity"].ToString();
                    txt_stop.Text = dr["STOP"].ToString();
                    cbo_Delegete.Text = dr["DELEGET"].ToString();
                    cbo_Security_year.Text = dr["Item_WARRANTY_YEAR"].ToString();
                    cbo_Security_Month.Text = dr["Item_WARRANTY_MONTH"].ToString();
                    cbo_Repairs.Text = dr["MAINTENANCE"].ToString();
                    cbo_Method_of_sale.Text = dr["METHOD_OF_SALE"].ToString();
                    cbo_reserve.Text = dr["Item_reserved"].ToString();
                    cbo_company_product.Text = dr["COMPANIES"].ToString();
                    //try
                    //{
                    //    product_image.Image = Show_image(dr["IMAG"]);
                    //}
                    //catch (Exception)
                    //{
                    //    //product_image.Image = null;
                    //}
                    txt_user_insert.Text = dr["USER_NAME_INSERT"].ToString();
                    txt_edite_user.Text = dr["USER_NAME_EDITS"].ToString();
                    txt_date_insert.Text = dr["DATE_OF_INSERT"].ToString();
                    txt_last_edite_day.Text = dr["DATE_OF_Edits"].ToString();
                    //DGV_PRODUCT.Text = dr["OTHER_PRODUCTS"].ToString();
                    txt_details.Text = dr["DETAILS"].ToString();
                }
                dr.Close();
            }
            catch (Exception)
            {
            }
            finally { con.Close(); }
        }

        private void tax_percent_TextChanged(object sender, EventArgs e)
        {
            try { if (tax_percent.Text.Trim() == "") { tax_percent.Text = "0"; tax_percent.SelectAll(); } else if (txt_price.Text.Trim() == "") { txt_price.Text = "0"; txt_price.SelectAll(); } decimal price = Convert.ToDecimal(txt_price.Text.Trim()); decimal taxpercent = Convert.ToDecimal(tax_percent.Text.Trim()); txt_taxing.Text = Convert.ToString((taxpercent * price) / 100);
            } catch (Exception) { }
        }

        private void txt_price_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (tax_percent.Text.Trim() == "") { tax_percent.Text = "0"; tax_percent.SelectAll(); } else if (txt_price.Text.Trim() == "") { txt_price.Text = "0"; txt_price.SelectAll(); } decimal price = Convert.ToDecimal(txt_price.Text.Trim()); decimal taxpercent = Convert.ToDecimal(tax_percent.Text.Trim()); txt_taxing.Text = Convert.ToString((taxpercent * price) / 100);
            }
            catch (Exception) { }
        }

        public void Add_delegets()
        {
            try
            {
                // if (txt_id_product.Text.Trim() == "" ) { MessageBox.Show("يرجى اختيار الصنف المراد اضافة موردين له"); return; }
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string sql = "Insert Into DELEGETS_ITEMS_EXPORT Values(N'" + deleget_names.Text.Trim() + "',N'" + txt_parcode.Text.Trim() + "',N'" + txt_id_product.Text.Trim() + "')";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@DELGET_NAME", deleget_names.Text.Trim());
                cmd.Parameters.AddWithValue("@PARCODE", txt_parcode.Text.Trim());
                cmd.Parameters.AddWithValue("@FORIEN_KEY", txt_id_product.Text.Trim());
                txt_msg_product.Text = "تمت الاضافة بنجاح";
                deleget_names.Text = "";
            } catch (Exception) { }
            finally
            {
                con.Close();
                DGV_deleget.Refresh();
                Selectdeleget(txt_id_product);
                deleget_names.Text = "";
                txt_id_deleget.Text = "";
            }
        }

        private void btn_add_deleget_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_id_product.Text.Trim() != "" && txt_id_deleget.Text.Trim() == "")
                {
                    if (deleget_names.Text.Trim() == "")
                    {
                        deleget_names.BackColor = Color.Red;
                    }
                    else
                    {
                        deleget_names.BackColor = Color.White;
                        Add_delegets();
                    }
                }
                else if (txt_id_product.Text.Trim() != "" && txt_id_deleget.Text.Trim() != "")
                {
                    // UpDateDeleget();
                }
            }
            catch (Exception) { }
            finally
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
            }
        }

        public void Selectdeleget(TextBox txt_id_product)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string sql = "SELECT DELEGATE_NAME, ID_DELEGATE FROM TBL_DELEGATES Where ID_product = N'" + txt_id_product.Text.Trim() + "' ";
                SqlCommand cmdDEleg = new SqlCommand(sql, con);
                SqlDataAdapter daDEleg = new SqlDataAdapter(cmdDEleg);
                DataTable dtDEleg = new DataTable();
                daDEleg.Fill(dtDEleg);
                DGV_deleget.DataSource = dtDEleg;
            }
            catch (Exception) { }
            finally { if (con.State == ConnectionState.Open) { con.Close(); } }
        }

        private void cbo_branch_product_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sql = "select ID_BRANCH from TBL_BRANCH where BRANCH_NAME=N'" + cbo_branch_product.Text.Trim() + "' ";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                branch_id.Text = dt.Rows[0]["ID_BRANCH"].ToString();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        private void cbo_stores_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string sql = "select ID_STORE from TBL_STORES where STORE_NAME=N'" + cbo_stores.Text.Trim() + "' ";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                store_id.Text = dt.Rows[0]["ID_STORE"].ToString();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        private void cbo_categories_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string sql = "select ID_CATEGORY from TBL_CATEGORIES where CATEGORY_NAME=N'" + cbo_categories.Text.Trim() + "' ";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                categorey_id.Text = dt.Rows[0]["ID_CATEGORY"].ToString();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        private void cbo_units_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string sql = "select ID_UNIT from TBL_UNIT where UNIT_NAME=N'" + cbo_units.Text.Trim() + "' ";
                SqlCommand cmd2 = new SqlCommand(sql, con);
                SqlDataReader dr = cmd2.ExecuteReader();
                //cmd.ExecuteNonQuery();
                //SqlDataAdapter da = new SqlDataAdapter(cmd2);
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                while(dr.Read())
                {
                    unite_id.Text = dr["ID_UNIT"].ToString();
                }dr.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        private void cbo_Delegete_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string sql = "select ID_DELEGATE from TBL_DELEGATES where DELEGATE_NAME=N'" + cbo_Delegete.Text.Trim() + "' ";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                //deleget_id.Text = dt.Rows[0]["ID_DELEGATE"].ToString();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        public void Clearnew()
        {
            txt_box_new_parcode.Text = "";
            cbo_unit_2.Text = "";
            txt_unit_items.Text = "";
            txt_first_coast_2.Text = "";
            txt_price_2.Text = "";
            txt_less_price_2.Text = "";
            tax_count.Text = "";
            tax_total2.Text = "";
            ID_product_2.Text = "";
            fill_new_product();
        }

        private void btn_new_2_Click(object sender, EventArgs e)
        {
            try
            {
                txt_box_new_parcode.BackColor = Color.White;
                Clearnew();
            } catch (Exception) { }
        }

        private void btn_delete_2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_id_product.Text.Trim() != "" && txt_box_new_parcode.Text != "" && cbo_unit_2.Text != "" && txt_unit_items.Text != "" && txt_first_coast_2.Text != "" && txt_price_2.Text != "" && txt_less_price_2.Text != "")
                {
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM NEW_PRODUCTS WHERE PARCODE_2 = N'" + txt_box_new_parcode.Text.Trim() + "' ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    txt_msg_product.Text = "تم الحذف بنجاح";

                    Clearnew();
                    fill_new_product();
                    DGV_new_product.Refresh();
                }
                else { txt_msg_product.Text = "يرجى تحديد الصنف المراد حذفه"; }
            }
            catch (Exception)
            {
            }
            finally { con.Close(); }
        }

        private void btn_save_2_Click(object sender, EventArgs e)
        {
            if (txt_id_product.Text.Trim() != "" && ID_product_2.Text.Trim() == "")
            {
                Save_2();
            }
            else
            if (ID_product_2.Text.Trim() != "") { Update2(); Edite_product(); }
        }

        public void fill_new_product()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string sqlpro = "SELECT * FROM NEW_PRODUCTS Where FORIEGN_KEY_ID =N'"+txt_id_product.Text.Trim()+"' ";
                SqlCommand cmdnp = new SqlCommand(sqlpro, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter danp = new SqlDataAdapter(cmdnp);
                DataTable dtnp = new DataTable();
                danp.Fill(dtnp);
                DGV_new_product.DataSource = dtnp;
            } 
            catch (Exception) { }
            finally { con.Close(); }
        }

        public void Save_2()
        {
            try
            {
                if (txt_box_new_parcode.Text.Trim() != "" && txt_product_name.Text.Trim() != "" && cbo_unit_2.Text.Trim() != "" && txt_unit_items.Text.Trim() != "" && txt_first_coast_2.Text.Trim() != "" && txt_price_2.Text.Trim() != "" && txt_less_price_2.Text.Trim() != "" && txt_quantity.Text.Trim() != "" && txt_id_product.Text.Trim() != "")
                {
                    txt_box_new_parcode.BackColor = Color.White;
                    select_parcode_product(txt_box_new_parcode);
                    select_parcode_product2(txt_box_new_parcode);
                    if (col1 == true || col2 == true)
                    { MessageBox.Show("رقم الباركود هذا موجود مسبقاً"); col1 = false; col2 = false; return; }
                    else
                    {
                        string sql = "Insert Into NEW_PRODUCTS Values(N'" + txt_box_new_parcode.Text.Trim() + "',N'" + txt_product_name.Text.Trim() + "'," +
                        "N'" + cbo_unit_2.Text.Trim() + "',N'" + txt_unit_items.Text.Trim() + "',N'" + txt_first_coast_2.Text.Trim() + "'," +
                        "N'" + txt_price_2.Text.Trim() + "',N'" + txt_less_price_2.Text.Trim() + "',N'" + txt_quantity.Text.Trim() + "'," +
                        "N'" + tax_total2.Text.Trim() + "'," +
                        "N'" + txt_id_product.Text.Trim() + "')";
                        SqlCommand cmd_save = new SqlCommand(sql, con);
                        con.Open();
                        cmd_save.ExecuteNonQuery();
                        cmd_save.Parameters.AddWithValue("@PARCODE_2", txt_box_new_parcode.Text.Trim());
                        cmd_save.Parameters.AddWithValue("@PRODUCT_NAME", txt_product_name.Text.Trim());
                        cmd_save.Parameters.AddWithValue("@UNITS", cbo_unit_2.Text.Trim());
                        cmd_save.Parameters.AddWithValue("@Items_Number", txt_unit_items.Text.Trim());
                        cmd_save.Parameters.AddWithValue("@FIRST_COAST", txt_first_coast_2.Text.Trim());
                        cmd_save.Parameters.AddWithValue("@PRICE", txt_price_2.Text.Trim());
                        cmd_save.Parameters.AddWithValue("@LESS_PRICE", txt_less_price_2.Text.Trim());
                        cmd_save.Parameters.AddWithValue("@QUANTITY", txt_quantity.Text.Trim());
                        cmd_save.Parameters.AddWithValue("@TAXING", tax_total2.Text.Trim());
                        cmd_save.Parameters.AddWithValue("@FORIEGN_KEY_ID", txt_id_product.Text.Trim());
                        //fill();
                        //ClearAll();
                        Clearnew();
                    }
                }
                else { txt_box_new_parcode.BackColor = Color.Red; }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                col1 = false;
                col2 = false;
                fill_new_product();
                DGV_new_product.Refresh();
            }
        }

        public void Update2()
        {
            try
            {
                txt_box_new_parcode.BackColor = Color.White;
                string sql = "Update [dbo].[NEW_PRODUCTS] Set PARCODE_2=N'" + txt_box_new_parcode.Text.Trim() + "'," +
                    "PRODUCT_NAME=N'" + txt_product_name.Text.Trim() + "',UNITS=N'" + cbo_unit_2.Text.Trim() + "'," +
                    "Items_Number=N'" + txt_unit_items.Text.Trim() + "',FIRST_COAST=N'" + txt_first_coast_2.Text.Trim() + "'," +
                    "PRICE=N'" + txt_price_2.Text.Trim() + "',LESS_PRICE=N'" + txt_less_price_2.Text.Trim() + "'," +
                    "QUANTITY=N'" + txt_quantity.Text.Trim() + "',TAXING=N'" + tax_total2.Text.Trim() + "'," +
                    "FORIEGN_KEY_ID=N'" + txt_id_product.Text.Trim() + "' Where ID = N'" + ID_product_2.Text.Trim() + "' ";
                con.Open();
                SqlCommand cmdupdate = new SqlCommand(sql, con);
                cmdupdate.ExecuteNonQuery();
                cmdupdate.Parameters.AddWithValue("@PARCODE_2", txt_box_new_parcode.Text.Trim());
                cmdupdate.Parameters.AddWithValue("@PRODUCT_NAME", txt_product_name.Text.Trim());
                cmdupdate.Parameters.AddWithValue("@UNITS", cbo_unit_2.Text.Trim());
                cmdupdate.Parameters.AddWithValue("@Items_Number", txt_unit_items.Text.Trim());
                cmdupdate.Parameters.AddWithValue("@FIRST_COAST", txt_first_coast_2.Text.Trim());
                cmdupdate.Parameters.AddWithValue("@PRICE", txt_price_2.Text.Trim());
                cmdupdate.Parameters.AddWithValue("@LESS_PRICE", txt_less_price_2.Text.Trim());
                cmdupdate.Parameters.AddWithValue("@QUANTITY", txt_quantity.Text.Trim());
                cmdupdate.Parameters.AddWithValue("@TAXING", tax_total2.Text.Trim());
                cmdupdate.Parameters.AddWithValue("@FORIEGN_KEY_ID", txt_id_product.Text.Trim());
                Clearnew();
            }
            catch (Exception)
            {
            }
            finally
            {
                con.Close();
                fill_new_product();
                DGV_new_product.Refresh();
            }
        }

        private void DGV_new_product_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DGV_new_product.Rows[e.RowIndex].Selected = true;
                if (e.RowIndex >= 0)
                {
                    txt_box_new_parcode.Text = DGV_new_product.SelectedRows[0].Cells["PARCODE_2"].Value.ToString();
                    //txt_product_name.Text= DGV_new_product.SelectedRows[0].Cells["PRODUCT_NAME"].Value.ToString();
                    cbo_unit_2.Text = DGV_new_product.SelectedRows[0].Cells["UNITS"].Value.ToString();
                    txt_unit_items.Text = DGV_new_product.SelectedRows[0].Cells["ITEMS"].Value.ToString();
                    txt_first_coast_2.Text = DGV_new_product.SelectedRows[0].Cells["FIRST_COAST"].Value.ToString();
                    txt_price_2.Text = DGV_new_product.SelectedRows[0].Cells["PRICE"].Value.ToString();
                    txt_less_price_2.Text = DGV_new_product.SelectedRows[0].Cells["LESS_PRICE"].Value.ToString();
                    //txt_quantity.Text = DGV_new_product.SelectedRows[0].Cells["QUANTITY"].Value.ToString();
                    ID_product_2.Text = DGV_new_product.SelectedRows[0].Cells["ID"].Value.ToString();
                }
            }
            catch (Exception) { }
        }

        public void select_parcode_product(TextBox txt_parcode)
        {
            try
            {
                con.Open();
                string sql = "SELECT PARCODE FROM TBL_products ";
                SqlCommand cmd7 = new SqlCommand(sql, con);
                SqlDataReader dr = cmd7.ExecuteReader();
                while (dr.Read())
                {
                    if (txt_parcode.Text.Trim() == dr["PARCODE"].ToString())
                    { col1 = true; break; }
                    //else { col1 = false; }
                }
                dr.Close();
                con.Close();
            } catch (Exception) {  }
        }

        public void select_parcode_product2(TextBox txt_parcode)
        {
            con.Open();
            string sql = "SELECT PARCODE_2 FROM NEW_PRODUCTS ";
            SqlCommand cmd5 = new SqlCommand(sql, con);
            SqlDataReader dr = cmd5.ExecuteReader();
            while (dr.Read())
            {
                if (txt_parcode.Text.Trim() == dr["PARCODE_2"].ToString())
                { col2 = true; break; }
            }
            dr.Close();
            con.Close();
            //return dt5;
        }

        private void DGV_deleget_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DGV_deleget.Rows[e.RowIndex].Selected = true;
                if (e.RowIndex >= 0)
                {
                    deleget_names.Text = DGV_deleget.SelectedRows[0].Cells["DELGET_NAME"].Value.ToString();
                    txt_id_deleget.Text = DGV_deleget.SelectedRows[0].Cells["ID_Deleget"].Value.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        public void ClearNewDeleget()
        {
            deleget_names.Text = "";
            txt_id_deleget.Text = "";
            Selectdeleget(txt_id_product);
        }

        private void btn_delete_deleget_Click(object sender, EventArgs e)
        {
            if (deleget_names.Text.Trim() != "" && txt_id_deleget.Text != "")
            {
                con.Open();
                cmd = new SqlCommand("DELETE FROM DELEGETS_ITEMS_EXPORT WHERE ID = N'" + txt_id_deleget.Text.Trim() + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                txt_msg_product.Text = "تم الحذف بنجاح";

                Selectdeleget(txt_id_product);
                deleget_names.Text = "";
                txt_id_deleget.Text = "";
            }
            else { MessageBox.Show("يرجى أختيار المندوب المراد حذفه"); }
        }

        private void btn_new_Deleget_Click(object sender, EventArgs e)
        {
            Selectdeleget(txt_id_product);
            deleget_names.Text = "";
            txt_id_deleget.Text = "";
            //Selectdeleget(txt_id_deleget);
        }

        private void tax_count_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_price_2.Text.Trim() == "") { tax_total2.Text = "0"; txt_price_2.SelectAll(); } if (tax_count.Text.Trim() == "") { tax_total2.Text = "0"; tax_total2.SelectAll(); } if (txt_price_2.Text.Trim() == "") { txt_price_2.Text = "0"; } decimal pirce2 = Convert.ToDecimal(txt_price_2.Text.Trim());if (tax_count.Text.Trim() == "") { tax_count.Text = "0"; } decimal tax1 = Convert.ToDecimal(tax_count.Text.Trim()); decimal total = (tax1 * pirce2) / 100; tax_total2.Text = Convert.ToString(total);
            }
            catch (Exception)
            {

            }
        }

        private void txt_search_parcode_TextChanged(object sender, EventArgs e)
        {
            try
            {
            if (txt_search_parcode.Text.Trim()=="") { ClearAll(); } 
                else
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    ClearAll(); con.Open(); 
                    string sql2 = "Select PARCODE From TBL_PRODUCTS Where PARCODE = N'" + txt_search_parcode.Text.Trim() + "' "; 
                    SqlCommand cmd1 = new SqlCommand(sql2, con);
                    SqlDataAdapter dapro = new SqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    dapro.Fill(dt);
                    if(dt.Rows.Count > 0)
                    {
                        con.Close(); 
                        Fill_txt_product("'+PARCODE+'", txt_search_parcode);
                        Selectdeleget(txt_id_product);
                        deleget_names.Text = "";
                        txt_id_deleget.Text = "";
                        fill_new_product();
                        return;
                    }
                    if (con.State == ConnectionState.Open) { con.Close(); } 
                    Select_NEW_PRODUCTS(); 
                    Fill_txt_product("'+ID+'", ID_product_2);
                    Selectdeleget(txt_id_product);
                    deleget_names.Text = "";
                    txt_id_deleget.Text = "";
                    fill_new_product();
                }
            } catch (Exception) { }
            finally{ if(con.State == ConnectionState.Open) { con.Close(); } }
        }

        public void Select_NEW_PRODUCTS()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Sql2 = "Select * from NEW_PRODUCTS where PARCODE_2 = N'"+txt_search_parcode.Text.Trim()+"' ";
                SqlCommand cmd2 = new SqlCommand(Sql2, con);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    txt_box_new_parcode.Text = dr2["PARCODE_2"].ToString();
                    cbo_unit_2.Text = dr2["PRODUCT_NAME"].ToString();
                    txt_unit_items.Text = dr2["UNITS"].ToString();
                    txt_first_coast_2.Text = dr2["Items_Number"].ToString();
                    txt_price_2.Text = dr2["PRICE"].ToString();
                    txt_less_price_2.Text = dr2["LESS_PRICE"].ToString();
                    //tax_count.Text = dr2[" "].ToString();                             // حساب الضريبة تحتاج معادلة خاصة
                    tax_total2.Text = dr2["TAXING"].ToString();
                    ID_product_2.Text = dr2["FORIEGN_KEY_ID"].ToString();
                }
                dr2.Close();
            }
            catch (Exception)
            {
            }
            finally { con.Close(); }
        }

        private void txt_name_search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_name_search.Text.Trim() == "") { ClearAll(); }else{ Fill_txt_product("'+PRODUCT_NAME+'", txt_name_search); Selectdeleget(txt_id_product); deleget_names.Text = ""; txt_id_deleget.Text = ""; fill_new_product();  }
            } catch (Exception) { }
        }

        public void select_par()
        {
            con.Open();
            string query = "SELECT parcode from TBL_products WHERE PRODUCT_NAME = N'" + txt_name_search.Text.Trim() + "' ";
            SqlCommand cmd12 = new SqlCommand(query, con);
            SqlDataReader dr = cmd12.ExecuteReader();
            while (dr.Read())
            {
                txt_id_product.Text = dr["parcode"].ToString();
            }
            dr.Close();
            con.Close();
        }

        private void txt_id_product_TextChanged(object sender, EventArgs e)
        {
            free_id_product();
        }

        public void free_id_product()
        {
            if (txt_id_product.Text.Trim() != "") { txt_box_new_parcode.ReadOnly = false; cbo_unit_2.Enabled = true; txt_unit_items.ReadOnly = false; txt_first_coast_2.ReadOnly = false; txt_price_2.ReadOnly = false; txt_less_price_2.ReadOnly = false; tax_count.ReadOnly = false; } else { txt_box_new_parcode.ReadOnly = true; cbo_unit_2.Enabled = false; txt_unit_items.ReadOnly = true; txt_first_coast_2.ReadOnly = true; txt_price_2.ReadOnly = true; txt_less_price_2.ReadOnly = true; tax_count.ReadOnly = true; }
        }
        //if (con.State == ConnectionState.Open) { con.Close(); }
        //con.Open(); 
        //string sql2 = "Select parcode from TBL_PRODUCTS Where PRODUCT_NAME = N'" + txt_name_search.Text.Trim() + "' "; 
        //SqlCommand cmd1 = new SqlCommand(sql2, con); 
        //SqlDataReader dr1 = cmd1.ExecuteReader(); 
        //while (dr1.Read()) 
        //{ con.Close(); 
        //    dr1.Close(); 
        //    Fill_txt_product("'+PRODUCT_NAME+'", txt_name_search); 
        //    Selectdeleget(txt_id_product); 
        //    fill_new_product(); 
        //    return; 
        //}
        //if (con.State == ConnectionState.Open) { con.Close(); }
        //dr1.Close(); 
        //Select_NEW_PRODUCTS(); 
        //Fill_txt_product("'+PRODUCT_NAME+'", ID_product_2); 
        //Selectdeleget(txt_id_product); 
        //fill_new_product();

    }
}
