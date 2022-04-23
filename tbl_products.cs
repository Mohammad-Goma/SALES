using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;

namespace SALES
{
    class tbl_products
    {
        
        //SQLServer Sqlc;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-G2CDA6M\SQLEXPRESS;Initial Catalog=RealApplication;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();


        public tbl_products()
        {

        }

        public Int32 id
        {
            get;
            set;
        }
        public string PARCODE
        {
            get;
            set;
        }
        public string PRODUCT_NAME
        {
            get;
            set;
        }
        public string INITIAL_COST
        {
            get;
            set;
        }
        public string PRICE
        {
            get;
            set;
        }
        public string LESS_PRICE
        {
            get;
            set;
        }
        public string TAXING
        {
            get;
            set;
        }
        public string STOP
        {
            get;
            set;
        }
        public string Quantity
        { 
            get;
            set;
        }
        public string Suppliers
    {
            get;
            set;
        }
        public string Flashback
        {
            get;
            set;
        }
        public string Rate_of_sale
        {
            get;
            set;
        }
        public string Method_of_sale
        {
            get;
            set;
        }
        public string Item_reserved
        {
            get;
            set;
        }
        public System.Byte[] IMAG
        {
            get;
            set;
        }
        public System.DateTime[] DATE_OF_INSERT
        {
            get;
            set;
        }
        public string DATE_OF_Modification
        {
            get;
            set;
        }
        public string DETAILS
        {
            get;
            set;
        }



        public void Save()
        {
            //    cmd.Connection = con;
            //    cmd.CommandText = "Select max(ID) as fds From TBL_PRODUCTS";
            //    con.Open();
            //    cmd.ExecuteNonQuery();

            //    DataSet dsS = new DataSet();
            //    SqlDataAdapter DA = new SqlDataAdapter(cmd);
            //    DA.Fill(dsS);

            //    int id;
            //    DataRow drS;
            //    drS = dsS.Tables[0].Rows[0];
            //    id = Convert.ToInt32(drS["fds"].ToString());

            //    id++;
            //    con.Close();

            //    MemoryStream ms = new MemoryStream();
            //    frm.product_image.Image.Save(ms, product_image.Image.RawFormat);
            //    byte[] byteImage = ms.ToArray();


            //    DateTime dateTime = DateTime.Parse(this.date_product_now.Text);


            //    if (txt_id_product.Text.Trim() == "" && txt_parcode.Text.Trim() != "" && product_name.Text.Trim() != "")
            //    {
            //        con.Open();
            //        string query = "INSERT INTO TBL_PRODUCTS VALUES (N'" + id + "', N'" + txt_parcode.Text.Trim() + "', N'" + product_name.Text.Trim() + "',N'" + first_cost.Text.Trim() + "',N'" + txt_price.Text.Trim() + "',N'" + txt_less_price.Text.Trim() + "',N'" + txt_taxing.Text.Trim() + "'" +
            //            ",N'" + txt_stop.Text.Trim() + "',N'" + txt_quantity.Text.Trim() + "',N'" + txt_delegat.Text.Trim() + "',N'" + txt_accept_back.Text.Trim() + "'," +
            //            "N'" + txt_rate_buy.Text.Trim() + "',N'" + cbo_Method_of_sale.Text.Trim() + "',N'" + cbo_reserve.Text.Trim() + "','byteImage','" + dateTime + "', N'" + txt_last_edite_day.Text.Trim() + "' ,N'" + txt_details.Text.Trim() + "') ";

            //        cmd = new SqlCommand(query, con);
            //        cmd.ExecuteNonQuery();

            //        cmd.Parameters.AddWithValue("@Id", id);
            //        cmd.Parameters.AddWithValue("@PARCODE", txt_parcode.Text.Trim());
            //        cmd.Parameters.AddWithValue("@PRODUCT_NAME", product_name.Text.Trim());
            //        cmd.Parameters.AddWithValue("@INITIAL_COST", first_cost.Text.Trim());
            //        cmd.Parameters.AddWithValue("@PRICE", txt_price.Text.Trim());
            //        cmd.Parameters.AddWithValue("@LESS_PRICE", txt_less_price.Text.Trim());
            //        cmd.Parameters.AddWithValue("@TAXING", txt_taxing.Text.Trim());
            //        cmd.Parameters.AddWithValue("@STOP", txt_stop.Text.Trim());
            //        cmd.Parameters.AddWithValue("@Quantity", txt_quantity.Text.Trim());
            //        cmd.Parameters.AddWithValue("@Suppliers", txt_delegat.Text.Trim());
            //        cmd.Parameters.AddWithValue("@Flashback", txt_accept_back.Text.Trim());
            //        cmd.Parameters.AddWithValue("@Rate_of_sale", txt_rate_buy.Text.Trim());
            //        cmd.Parameters.AddWithValue("@Method_of_sale", cbo_Method_of_sale.Text.Trim());
            //        cmd.Parameters.AddWithValue("@Item_reserved", cbo_reserve.Text.Trim());
            //        cmd.Parameters.AddWithValue("@IMAG", byteImage);
            //        cmd.Parameters.AddWithValue("@DATE_OF_INSERT", dateTime);
            //        cmd.Parameters.AddWithValue("@DATE_OF_Modification", txt_last_edite_day.Text.Trim());
            //        cmd.Parameters.AddWithValue("@DETAILS", txt_details.Text.Trim());

            //        con.Close();

            //    }
            //    else if (txt_id_product.Text.Trim() != "")
            //    {
            //        con.Open();
            //        cmd = new SqlCommand("UPDATE tbl_products set PARCODE = N'" + txt_parcode.Text.Trim() + "', PRODUCT_NAME= N'" + product_name.Text.Trim() + "'" +
            //            ", INITIAL_COST = N'" + first_cost.Text.Trim() + "', PRICE = N'" + txt_price.Text.Trim() + "', LESS_PRICE= N'" + txt_less_price.Text.Trim() + "'" +
            //            ", TAXING= N'" + txt_taxing.Text.Trim() + "', STOP = N'" + txt_stop.Text.Trim() + "'" +
            //            ", Quantity = N'" + txt_quantity.Text.Trim() + "', Suppliers = N'" + txt_delegat.Text.Trim() + "', Flashback = N'" + txt_accept_back.Text.Trim() + "'" +
            //            ", Rate_of_sale= N'" + txt_rate_buy.Text.Trim() + "', Method_of_sale= N'" + cbo_Method_of_sale.Text.Trim() + "', Item_reserved= N'" + cbo_reserve.Text.Trim() + "'" +
            //            ", IMAG= 'byteImage', DATE_OF_INSERT= N'" + dateTime + "', DATE_OF_Modification= N'" + txt_last_edite_day.Text.Trim() + "'" +
            //            ", DETAILS= N'" + txt_details.Text.Trim() + "'  where ID =N'" + txt_id_product.Text.Trim() + "' ", con);
            //        cmd.ExecuteNonQuery();

            //        cmd.Parameters.AddWithValue("@PARCODE", txt_parcode.Text.Trim());
            //        cmd.Parameters.AddWithValue("@PRODUCT_NAME", product_name.Text.Trim());
            //        cmd.Parameters.AddWithValue("@INITIAL_COST", first_cost.Text.Trim());
            //        cmd.Parameters.AddWithValue("@PRICE", txt_price.Text.Trim());
            //        cmd.Parameters.AddWithValue("@LESS_PRICE", txt_less_price.Text.Trim());
            //        cmd.Parameters.AddWithValue("@TAXING", txt_taxing.Text.Trim());
            //        cmd.Parameters.AddWithValue("@STOP", txt_stop.Text.Trim());
            //        cmd.Parameters.AddWithValue("@Quantity", txt_quantity.Text.Trim());
            //        cmd.Parameters.AddWithValue("@Suppliers", txt_delegat.Text.Trim());
            //        cmd.Parameters.AddWithValue("@Flashback", txt_accept_back.Text.Trim());
            //        cmd.Parameters.AddWithValue("@Rate_of_sale", txt_rate_buy.Text.Trim());
            //        cmd.Parameters.AddWithValue("@Method_of_sale", cbo_Method_of_sale.Text.Trim());
            //        cmd.Parameters.AddWithValue("@Item_reserved", cbo_reserve.Text.Trim());
            //        cmd.Parameters.AddWithValue("@IMAG", byteImage);
            //        cmd.Parameters.AddWithValue("@DATE_OF_INSERT", dateTime);
            //        cmd.Parameters.AddWithValue("@DATE_OF_Modification", txt_last_edite_day.Text.Trim());
            //        cmd.Parameters.AddWithValue("@DETAILS", txt_details.Text.Trim());

            //        con.Close();
            //        txt_msg_product.Text = "تم التعديل بنجاح";

            //        cbo_name_search.Items.Clear();
            //        cbo_search_parcode.Items.Clear();
            //        fill();
            //    }
            //    else { txt_msg_product.Text = "يرجى ادخال البيانات"; }
        }
    }
}
