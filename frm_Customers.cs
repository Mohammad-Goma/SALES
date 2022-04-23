using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace SALES
{
    public partial class frm_Customers : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd;
        public frm_Customers()
        {
            InitializeComponent();
        }
        public void Fill_Customers()
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
                if (chk_Customer_name.Checked)
                {
                    cbo_Customer.Items.Add(dr["CUSTOMER_NAME"].ToString());
                }
                else if (chk_Customer_id.Checked)
                {
                    cbo_Customer.Items.Add(dr["ID_CUSTOMER"].ToString());
                }
                else if (chk_Customer_National_id.Checked)
                {
                    cbo_Customer.Items.Add(dr["CUSTOMER_NATIONAL_ID"].ToString());
                }
            }
        }

        private void frm_Customers_Load(object sender, EventArgs e)
        {
            Fill_Customers();
            Customer_user_name.Text=Main_Form.USER_NAME_STRING.Trim();
            lvl_customer.Text= Main_Form.PASSWORD_STRING.Trim();
        }

        private void chk_Customer_name_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Customer_name.Checked)
            {
                chk_Customer_id.Checked = false;
                chk_Customer_National_id.Checked = false;
                cbo_Customer.Items.Clear();
                cbo_Customer.Text = string.Empty;
                Fill_Customers();
            }
        }

        private void chk_Customer_National_id_CheckedChanged(object sender, EventArgs e)
        {
            chk_Customer_name.Checked = false;
            chk_Customer_id.Checked = false;
            cbo_Customer.Items.Clear();
            cbo_Customer.Text = string.Empty;
            Fill_Customers();
        }

        private void chk_Customer_id_CheckedChanged(object sender, EventArgs e)
        {
            chk_Customer_name.Checked = false;
            chk_Customer_National_id.Checked = false;
            cbo_Customer.Items.Clear();
            cbo_Customer.Text = string.Empty;
            Fill_Customers();
        }

        public void ClearAll()
        {
            foreach(Control ctrl in this.Controls)
            {
                if(ctrl is TextBox)
                {
                    ctrl.Text = string.Empty;
                }
            }
            Customer_user_name.Text = Main_Form.USER_NAME_STRING.Trim();
            lvl_customer.Text = Main_Form.PASSWORD_STRING.Trim();
        }

        private void btn_Delete_Customer_Click(object sender, EventArgs e)
        {
            if (Customer_id.Text.Trim() != "")
            {
                if (MessageBox.Show("انتبة ربما يكون هذا العنصر مرتبط بعناصر اخرى", "تنبيه بالحذف", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    string querry = " DELETE FROM TBL_CUSTOMER where ID_CUSTOMER = N'" + Customer_id.Text.Trim() + "' ";
                    cmd = new SqlCommand( querry , con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Fill_Customers();
                    msg_Customer.Text = " تم الحذف بنجاح ";
                }
            }
            else if(Customer_id.Text.Trim() == "") { msg_Customer.Text = "  من فضلك اختر العنصر المراد حذفه  "; }
        }

        public Image Show_image(object FLD_Picture)     ///  محمد رضا
        {
            byte[] data = (byte[])FLD_Picture;
            MemoryStream ms = new MemoryStream(data);
            Image img = Image.FromStream(ms);
            return img;
        }

        public void Fill_cbo_Customer_index_changed(ComboBox cbo , string fld)
        {
            try { 
            if(cbo.SelectedIndex > -1)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string querry = " select * from TBL_CUSTOMER where '" + fld +"' = N'"+cbo.Text.Trim()+"' ";
                cmd = new SqlCommand(querry , con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Customer_id.Text = dr["ID_Customer"].ToString();
                    Customer_name.Text = dr["Customer_NAME"].ToString();
                    Customer_phone_1.Text = dr["Customer_MOBILE_1"].ToString();
                    Customer_phone_2.Text = dr["Customer_MOBILE_2"].ToString();
                    Customer_phone_3.Text = dr["Customer_MOBILE_3"].ToString();
                    Customer_Adress.Text = dr["Customer_ADRESS"].ToString();
                    Customer_national_id.Text = dr["CUSTOMER_NATIONAL_ID"].ToString();
                    try
                    {
                        //pic_Customer.Image = Show_image(dr["CUSTOMER_IMAGE"]);
                    }
                    catch (Exception) { /*throw (Ex); */ }
                    Customer_friend.Text = dr["CUSTOMER_FRIENDS"].ToString();
                    cbo_customer_status.Text = dr["CUSTOMER_STATUS"].ToString();
                    cbo_customer_depit.Text = dr["CUSTOMER_STATUS_DEBT"].ToString();
                    Customer_date_of_Dealing.Text = Convert.ToDateTime(dr["CUSTOMER_Date_of_Dealing"]).ToString();
                    txt_Customer_insert_date.Text = dr["CUSTOMER_Date_of_Insert"].ToString();
                    txt_Customer_last_Edits.Text = dr["CUSTOMER_Date_of_Edits"].ToString();
                    txt_Customer_insert_name.Text = dr["USER_INSERT"].ToString();
                    txt_Customer_Edite_Name.Text = dr["USER_EDIT"].ToString();
                    Customer_Details.Text = dr["CUSTOMER_DETAILS"].ToString();
                }
                dr.Close();
                con.Close();
            }
            }
            catch  { }
        }

        private void cbo_Customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chk_Customer_name.Checked)
            {
                Fill_cbo_Customer_index_changed(cbo_Customer, "'+CUSTOMER_NAME+'");
            }
            else if (chk_Customer_id.Checked)
            {
                Fill_cbo_Customer_index_changed(cbo_Customer, "'+ID_CUSTOMER+'");
            }
            else if (chk_Customer_National_id.Checked)
            {
                Fill_cbo_Customer_index_changed(cbo_Customer, "'+CUSTOMER_NATIONAL_ID+'");
            }
        }

        private void btn_new_Customer_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        public Int64 max_id()
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            Int64 id = 0;
            con.Open();
            cmd = new SqlCommand("Select max(ID_Customer) as fds From TBL_CUSTOMER ", con);
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

        private void btn_save_Customer_Click(object sender, EventArgs e)
        {
            if(Customer_id.Text.Trim() == "" && Customer_name.Text.Trim() != "")
            {
                MemoryStream stream = new MemoryStream();
                pic_Customer.Image.Save(stream, ImageFormat.Jpeg);
                byte[] byteArray = stream.ToArray();

                string max = Convert.ToString( max_id() );

                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "INSERT INTO TBL_CUSTOMER VALUES (N'"+ max +"',N'"+ Customer_name.Text.Trim()+ "',N'" + Customer_Adress.Text.Trim() + "'," +
                    "N'" + Customer_national_id.Text.Trim() + "','"+ null +"'," +      //' byteArray '
                    "N'" + Customer_phone_1.Text.Trim() + "',N'" + Customer_phone_2.Text.Trim() + "',N'" + Customer_phone_3.Text.Trim() + "'," +
                    "N'" + Customer_friend.Text.Trim() + "',N'" + cbo_customer_status.Text.Trim() + "',N'" + cbo_customer_depit.Text.Trim() + "',N'" + Customer_Date_Time_Now.Text.Trim() + "'," +
                    "N'" + Customer_date_of_Dealing.Text.Trim() + "',N'" + txt_Customer_last_Edits.Text.Trim() + "'," +
                    "N'" + Customer_user_name.Text.Trim() + "',N'" + Customer_user_name.Text.Trim() + "',N'" + Customer_Details.Text.Trim() + "') ";
                cmd = new SqlCommand(Query ,con);
                cmd.ExecuteNonQuery();

                cmd.Parameters.AddWithValue("@ID_Customer", Customer_id.Text.Trim());
                cmd.Parameters.AddWithValue("@Customer_NAME", Customer_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Customer_ADRESS", Customer_Adress.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_NATIONAL_ID", Customer_national_id.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_IMAGE", null);                                 //byteArray
                cmd.Parameters.AddWithValue("@CUSTOMER_FRIENDS", Customer_friend.Text.Trim());
                cmd.Parameters.AddWithValue("@Customer_MOBILE_1", Customer_phone_1.Text.Trim());
                cmd.Parameters.AddWithValue("@Customer_MOBILE_2", Customer_phone_2.Text.Trim());
                cmd.Parameters.AddWithValue("@Customer_MOBILE_3", Customer_phone_3.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_FRIENDS", Customer_friend.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_STATUS", cbo_customer_status.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_STATUS_DEBT", cbo_customer_depit.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_Date_of_Insert", Customer_Date_Time_Now.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_Date_of_Dealing", Customer_date_of_Dealing.Text.Trim());
                //cmd.Parameters.AddWithValue("@CUSTOMER_Date_of_Edits", txt_Customer_last_Edits.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_INSERT", Customer_user_name.Text.Trim());
                //cmd.Parameters.AddWithValue("@USER_EDIT", Customer_user_name.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_DETAILS", Customer_Details.Text.Trim());
                con.Close();
                Fill_Customers();
                msg_Customer.Text = " تمت الإضافة بنجاح ";
            }
            else if (Customer_id.Text.Trim() != "")
            {
                Edite_Customer();
            }
        }

        public void Edite_Customer()
        {
                if (con.State == ConnectionState.Open) { con.Close(); }

                MemoryStream stream = new MemoryStream();
                pic_Customer.Image.Save(stream, ImageFormat.Jpeg);
                byte[] byteArray = stream.GetBuffer();

                string max = Convert.ToString(max_id());

                con.Open();
                string Query = "Update TBL_CUSTOMER set Customer_NAME = N'" + Customer_name.Text.Trim() + "'," +
                 "Customer_ADRESS = N'" + Customer_Adress.Text.Trim() + "'," +
                  "CUSTOMER_NATIONAL_ID = N'" + Customer_national_id.Text.Trim() + "', CUSTOMER_IMAGE=' byteArray '," + //' byteArray '
                  "Customer_MOBILE_1 = N'" + Customer_phone_1.Text.Trim() + "', Customer_MOBILE_2 = N'" + Customer_phone_2.Text.Trim() + "'," +
                  "Customer_MOBILE_3 = N'" + Customer_phone_3.Text.Trim() + "',CUSTOMER_FRIENDS = N'" + Customer_friend.Text.Trim() + "'," +
                  "CUSTOMER_STATUS = N'" + cbo_customer_status.Text.Trim() + "',CUSTOMER_STATUS_DEBT = N'" + cbo_customer_depit.Text.Trim() + "'," +
                  "CUSTOMER_Date_of_Insert = N'" + Customer_Date_Time_Now.Text.Trim() + "',CUSTOMER_Date_of_Dealing = N'" + Customer_date_of_Dealing.Text.Trim() + "'," +
                  "CUSTOMER_Date_of_Edits = N'" + Customer_Date_Time_Now.Text.Trim() + "'," +
                  "USER_INSERT = N'" + txt_Customer_insert_name.Text.Trim() + "', USER_EDIT = N'" + Customer_user_name.Text.Trim() + "'," +
                  "CUSTOMER_DETAILS = N'" + Customer_Details.Text.Trim() + "' where ID_Customer = N'"+ Customer_id.Text.Trim()+"' ";
                cmd = new SqlCommand(Query , con);
                cmd.ExecuteNonQuery();

                cmd.Parameters.AddWithValue("@Customer_NAME", Customer_name.Text.Trim());
                cmd.Parameters.AddWithValue("@Customer_ADRESS", Customer_Adress.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_NATIONAL_ID", Customer_national_id.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_IMAGE", null);  // byteArray
                cmd.Parameters.AddWithValue("@Customer_MOBILE_1", Customer_phone_1.Text.Trim());
                cmd.Parameters.AddWithValue("@Customer_MOBILE_2", Customer_phone_2.Text.Trim());
                cmd.Parameters.AddWithValue("@Customer_MOBILE_3", Customer_phone_3.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_FRIENDS", Customer_friend.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_STATUS", cbo_customer_status.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_Date_of_Insert", Customer_Date_Time_Now.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_Date_of_Dealing", Customer_date_of_Dealing.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_Date_of_Edits", txt_Customer_last_Edits.Text.Trim());
            //cmd.Parameters.AddWithValue("@USER_INSERT", txt_Customer_insert_name.Text.Trim());
            cmd.Parameters.AddWithValue("@USER_EDIT", Customer_user_name.Text.Trim());
                cmd.Parameters.AddWithValue("@CUSTOMER_DETAILS", Customer_Details.Text.Trim());

                con.Close();
                Fill_Customers();
                msg_Customer.Text = " تم التعديل بنجاح ";
        }

        private void btn_Add_img_Customer_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Image Files|*.jpg;*.png;*.bmp;*.*;(*.*);";
            if (of.ShowDialog() == DialogResult.OK)
            {
                pic_Customer.Image = new Bitmap(of.FileName);
            }
        }
    }
}
