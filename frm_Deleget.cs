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
    public partial class frm_Deleget : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd;
        public frm_Deleget()
        {
            InitializeComponent();
        }

        private void btn_Add_img_deleget_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Image Files|*.jpg;*.png;*.bmp;*.*;(*.*);";
            if (of.ShowDialog() == DialogResult.OK)
            {
                pic_delegate.Image = new Bitmap(of.FileName);
            }
        }

        public void Fill_Delegate()
        {
            ClearAll();
            cbo_delegate.Items.Clear();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            cmd = new SqlCommand("SELECT * FROM TBL_DELEGATES", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                       if (chk_delgate_name.Checked)
                {
                    cbo_delegate.Items.Add(dr["DELEGATE_NAME"].ToString());
                }
                else if (chk_delegate_id.Checked)
                {
                    cbo_delegate.Items.Add(dr["ID_DELEGATE"].ToString());
                }
                else if (chk_delgate_National_id.Checked)
                {
                    cbo_delegate.Items.Add(dr["DELEGATE_NATIONAL_ID"].ToString());
                }
            }
            dr.Close();
            con.Close();
        }

        private void frm_Deleget_Load(object sender, EventArgs e)
        {
            Fill_Delegate();
            txt_user_Deleget.Text = Main_Form.USER_NAME_STRING.Trim();
        }

        public Image Show_image(object FLD_Picture)     ///  محمد رضا
        {
            byte[] data = (byte[])FLD_Picture;
            MemoryStream ms = new MemoryStream(data);
            ms.Write(data, 0, data.Length);
            Image img = Image.FromStream(ms,true);
            return img;
        }

        public void Fill_cbo_Delegate_index_changed(ComboBox cbo ,string fld )
        {
            try
            {
                if (cbo.SelectedIndex > -1)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    cmd = new SqlCommand("SELECT * FROM TBL_DELEGATES WHERE '" + fld + "' = N'" + cbo.Text.Trim() + "' ", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        delegate_id.Text = dr["ID_DELEGATE"].ToString();
                        delegate_name.Text = dr["DELEGATE_NAME"].ToString();
                        delegate_phone_1.Text = dr["DELEGATE_MOBILE_1"].ToString();
                        delegate_phone_2.Text = dr["DELEGATE_MOBILE_2"].ToString();
                        delegate_phone_3.Text = dr["DELEGATE_MOBILE_3"].ToString();
                        delegate_Adress.Text = dr["DELEGATE_ADRESS"].ToString();
                        try
                        {
                            //pic_delegate.Image = Show_image(dr["DELEGATE_IMAGE"]);
                            //pic_delegate.Image = ByteToImage((byte[])dr["DELEGATE_IMAGE"]);
                        }catch (Exception ) { }
                        delegate_national_id.Text = dr["DELEGATE_NATIONAL_ID"].ToString();
                        delegate_id_comp.Text = dr["DELEGATE_ID_IN_COMPANY"].ToString();
                        delegate_comp_name.Text = dr["DELEGATE_COMPANY_NAME"].ToString();
                        delegate_manger.Text = dr["DELEGATE_MANAGER_NAME"].ToString();
                        delegate_manger_phone_1.Text = dr["DELEGATE_MANAGER_MOBILE_1"].ToString();
                        delegate_manger_phone_2.Text = dr["DELEGATE_MANAGER_MOBILE_2"].ToString();
                        delegate_manger_phone_3.Text = dr["DELEGATE_MANAGER_MOBILE_3"].ToString();
                        txt_insert_name.Text= dr["USER_Insert"].ToString();
                        txt_Edite_Name.Text = dr["USER_EDITE"].ToString();
                        delegate_status.Text = dr["DELEGATE_STATU"].ToString();
                        txt_delegate_insert_date.Text = dr["DELEGATE_Date_of_Insert"].ToString();
                        delegate_date_of_Dealing.Text = Convert.ToDateTime(dr["DELEGATE_Date_of_Dealing"]).ToString();
                        txt_delegate_last_Edits.Text = dr["DELEGATE_Date_of_Edite"].ToString();
                        Company_id.Text = dr["ID_COMPANY"].ToString();
                        product_id.Text = dr["ID_product"].ToString();
                        delegate_Details.Text = dr["DELEGATE_DETAILS"].ToString();
                    }
                    dr.Close();
                    con.Close();
                }
            } catch (Exception Exp){ msg_delegate.Text = Exp.Message; }
        }

        public static Bitmap ByteToImage(byte[] blob)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                mStream.Write(blob, 0, blob.Length);
                mStream.Seek(0, SeekOrigin.Begin);

                Bitmap bm = new Bitmap(mStream);
                return bm;
            }
        }

        private void cbo_delegate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chk_delgate_name.Checked)
            {
                Fill_cbo_Delegate_index_changed(cbo_delegate, "'+DELEGATE_NAME+'");
            }
            else if (chk_delegate_id.Checked)
            {
                Fill_cbo_Delegate_index_changed(cbo_delegate, "'+ID_DELEGATE+'");
            }
            else if (chk_delgate_National_id.Checked)
            {
                Fill_cbo_Delegate_index_changed(cbo_delegate, "'+DELEGATE_NATIONAL_ID+'");
            }
        }

        private void chk_delgate_name_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_delgate_name.Checked)
            {
                chk_delegate_id.Checked = false;
                chk_delgate_National_id.Checked = false;
                cbo_delegate.Items.Clear();
                cbo_delegate.Text = string.Empty;
                Fill_Delegate();
            }
        }

        private void chk_delgate_National_id_CheckedChanged(object sender, EventArgs e)
        {
            chk_delgate_name.Checked = false;
            chk_delegate_id.Checked = false;
            cbo_delegate.Items.Clear();
            cbo_delegate.Text = string.Empty;
            Fill_Delegate();
        }

        private void chk_delegate_id_CheckedChanged(object sender, EventArgs e)
        {
            chk_delgate_name.Checked = false;
            chk_delgate_National_id.Checked = false;
            cbo_delegate.Items.Clear();
            cbo_delegate.Text = string.Empty;
            Fill_Delegate();
        }

        private void btn_new_delegete_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        public void ClearAll()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = string.Empty;
                }
            }
            Company_id.Text = "0";
            product_id.Text = "0";
            txt_user_Deleget.Text = Main_Form.USER_NAME_STRING.Trim();
        }

        private void btn_Delete_Delegete_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Attention", "هل أنت متاكد من الحذف", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd = new SqlCommand("DELETE FROM TBL_DELEGATES WHERE ID_DELEGATE = N'" + delegate_id.Text.Trim() + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                ClearAll();
                Fill_Delegate();
                msg_delegate.Text = "تم الحذف بنجاح ";
            }
           
        }

        private void btn_save_Delegete_Click(object sender, EventArgs e)
        {
            try
            {
            if (con.State == ConnectionState.Open){con.Close();}
            int id = 0;
            con.Open();
            cmd = new SqlCommand("Select max(ID_DELEGATE) as fds From TBL_DELEGATES", con);
            cmd.ExecuteNonQuery();
            DataSet dsS = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dsS);
            DataRow drS;
            drS = dsS.Tables[0].Rows[0];
            id = Convert.ToInt32(drS["fds"].ToString());
            id++;
            con.Close();

            MemoryStream ms = new MemoryStream();
            pic_delegate.Image.Save(ms,ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();

            if (delegate_id.Text.Trim() == "" && delegate_name.Text.Trim() != "")
            {
                if (con.State == ConnectionState.Open){con.Close(); }
                con.Open();
                string querry = "INSERT INTO TBL_DELEGATES VALUES ('" + id + "',N'" + delegate_name.Text.Trim() + "',N'" + delegate_phone_1.Text.Trim() + "'," +
                    "N'" + delegate_phone_2.Text.Trim() + "',N'" + delegate_phone_3.Text.Trim() + "',N'" + delegate_Adress.Text.Trim() + "', ' byteImage ' ," +
                    " N'" + delegate_national_id.Text.Trim() + "',N'" + delegate_id_comp.Text.Trim() + "',N'" + delegate_comp_name.Text.Trim() + "'," +
                    "N'" + delegate_manger.Text.Trim() + "',N'" + delegate_manger_phone_1.Text.Trim() + "', N'" + delegate_manger_phone_2.Text.Trim() + "'," +
                    "N'" + delegate_manger_phone_3.Text.Trim() + "',N'" + Delegete_Date_Time_Now.Text.Trim() + "',N'" + delegate_date_of_Dealing.Text.Trim() + "'," +
                    "N'" + txt_delegate_last_Edits.Text.Trim()+ "',N'" + txt_user_Deleget.Text.Trim() + "',N'" + txt_user_Deleget.Text.Trim() + "'," +
                    "N'" + delegate_status.Text.Trim() + "',N'" + Company_id.Text.Trim() + "'," +
                    "N'" + product_id.Text.Trim() + "',N'" + delegate_Details.Text.Trim() + "') ";

                cmd = new SqlCommand(querry , con);      // USER_Insert   USER_EDITE
                cmd.ExecuteNonQuery();

                cmd.Parameters.AddWithValue("@ID_DELEGATE", id);
                cmd.Parameters.AddWithValue("@DELEGATE_NAME", delegate_name.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MOBILE_1", delegate_phone_1.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MOBILE_2", delegate_phone_2.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MOBILE_3", delegate_phone_3.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_ADRESS", delegate_Adress.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_IMAGE", byteImage);
                cmd.Parameters.AddWithValue("@DELEGATE_NATIONAL_ID", delegate_national_id.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_ID_IN_COMPANY", delegate_id_comp.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_COMPANY_NAME", delegate_comp_name.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MANAGER_NAME", delegate_manger.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MANAGER_MOBILE_1", delegate_manger_phone_1.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MANAGER_MOBILE_2", delegate_manger_phone_2.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MANAGER_MOBILE_3", delegate_manger_phone_3.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_Date_of_Insert", Delegete_Date_Time_Now.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_Date_of_Dealing", delegate_date_of_Dealing.Text.Trim());
                //cmd.Parameters.AddWithValue("@DELEGATE_Date_of_Edite",txt_delegate_last_Edits.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_Insert", txt_user_Deleget.Text.Trim());
                //cmd.Parameters.AddWithValue("@USER_EDITE", txt_user_Deleget.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_STATU", delegate_status.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_COMPANY", Company_id.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_product", product_id.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_DETAILS", delegate_Details.Text.Trim());
                con.Close();
                Fill_Delegate();
                msg_delegate.Text = "تم الحفظ بنجاح";
            }

            else if (delegate_id.Text.Trim() != "" )
            {
                string q = "UPDATE TBL_DELEGATES set DELEGATE_NAME=N'"+delegate_name.Text.Trim()+ "',DELEGATE_MOBILE_1=N'"+ delegate_phone_1.Text.Trim() + "'," +
                    " DELEGATE_MOBILE_2 = N'"+ delegate_phone_2.Text.Trim() + "',DELEGATE_MOBILE_3= N'"+ delegate_phone_3.Text.Trim() + "'," +
                    " DELEGATE_ADRESS = N'"+ delegate_Adress.Text.Trim() + "',DELEGATE_IMAGE = ' byteImage ', " +
                    " DELEGATE_NATIONAL_ID=N'"+ delegate_national_id.Text.Trim() + "' ,DELEGATE_ID_IN_COMPANY=N'"+ delegate_id_comp.Text.Trim() + "' ," +
                    " DELEGATE_COMPANY_NAME=N'"+ delegate_manger.Text.Trim() + "',DELEGATE_MANAGER_NAME=N'"+ delegate_manger.Text.Trim() + "' ," +
                    " DELEGATE_MANAGER_MOBILE_1=N'"+ delegate_manger_phone_1.Text.Trim() + "' ,DELEGATE_MANAGER_MOBILE_2= N'"+ delegate_manger_phone_2.Text.Trim() + "' ," +
                    " DELEGATE_MANAGER_MOBILE_3=N'"+ delegate_manger_phone_3.Text.Trim() + "' ,DELEGATE_Date_of_Insert=N'"+ txt_delegate_insert_date.Text.Trim() + "'," +
                    " DELEGATE_Date_of_Dealing=N'"+ delegate_date_of_Dealing.Text.Trim() + "',DELEGATE_Date_of_Edite=N'"+ Delegete_Date_Time_Now.Text.Trim() + "'," +
                    " USER_Insert=N'" + txt_user_Deleget.Text.Trim() + "',USER_EDITE=N'" + txt_user_Deleget.Text.Trim() + "'," +
                    " DELEGATE_STATU=N'"+ delegate_status.Text.Trim() + "',ID_COMPANY = N'" + Company_id.Text.Trim() + "'," +
                    " ID_product = N'" + product_id.Text.Trim() + "',DELEGATE_DETAILS = N'" + delegate_Details.Text.Trim() + "'  where ID_DELEGATE = N'" + delegate_id.Text.Trim() + "' ";
                con.Open();
                cmd = new SqlCommand( q ,con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@DELEGATE_NAME", delegate_name.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MOBILE_1", delegate_phone_1.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MOBILE_2", delegate_phone_2.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MOBILE_3", delegate_phone_3.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_ADRESS", delegate_Adress.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_IMAGE", byteImage);
                cmd.Parameters.AddWithValue("@DELEGATE_NATIONAL_ID", delegate_national_id.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_ID_IN_COMPANY", delegate_id_comp.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_COMPANY_NAME", delegate_comp_name.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MANAGER_NAME", delegate_manger.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MANAGER_MOBILE_1", delegate_manger_phone_1.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MANAGER_MOBILE_2", delegate_manger_phone_2.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_MANAGER_MOBILE_3", delegate_manger_phone_3.Text.Trim());
                //cmd.Parameters.AddWithValue("@DELEGATE_Date_of_Insert", txt_delegate_insert_date.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_Date_of_Dealing", delegate_date_of_Dealing.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_Date_of_Edite" , Delegete_Date_Time_Now.Text.Trim());
                //cmd.Parameters.AddWithValue("@USER_Insert", txt_user_Deleget.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_EDITE", txt_user_Deleget.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_STATU", delegate_status.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_COMPANY", Company_id.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_product", product_id.Text.Trim());
                cmd.Parameters.AddWithValue("@DELEGATE_DETAILS", delegate_Details.Text.Trim());
                con.Close();
                Fill_Delegate();
                msg_delegate.Text = "تم التعديل بنجاح";
            }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void frm_Deleget_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select Deleget_open,Deleget_save,Deleget_delete,Deleget_new From USERS_PERMESSIONS Where USER_NAME=N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader dr7 = cmd.ExecuteReader();
                if (dr7.Read())
                {
                    if (dr7["Deleget_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr7["Deleget_save"].ToString() == "True") { } else { btn_save_Delegete.Enabled = false; }
                    if (dr7["Deleget_delete"].ToString() == "True") { } else { btn_Delete_Delegete.Enabled = false; }
                    if (dr7["Deleget_new"].ToString() == "True") { } else { btn_new_delegete.Enabled = false; }
                }
                dr7.Close();
            } catch (Exception) { }
            finally { con.Close(); }
        }
    }
}
