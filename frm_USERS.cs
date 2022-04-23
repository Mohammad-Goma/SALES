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
    public partial class frm_USERS : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd;
        //string folderpath = "C:\\Users\\Gomaa\\source\\repos\\SALES\\SALES\\Photos\\Employee";
        public frm_USERS()
        {
            InitializeComponent();
        }

        public void Fill_Users()
        {
            if (con.State == ConnectionState.Open){ con.Close(); }
            
            cbo_USER.Items.Clear();
            ClearAll();
            con.Open();
            string querry = "SELECT * FROM TBL_USERS ";
            cmd = new SqlCommand(querry , con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (chk_USER_name.Checked)
                {
                    cbo_USER.Items.Add(dr["USER_NAME"].ToString());
                }
                else if (chk_USER_id.Checked)
                {
                    cbo_USER.Items.Add(dr["ID_USER"].ToString());
                }
                else if (chk_USER_NATIONAL_ID.Checked)
                {
                    cbo_USER.Items.Add(dr["USER_National_ID"].ToString());
                }
            }dr.Close();
            con.Close();
        }

        public void Fill_USERS_index_change_withstored()
        {

            if (cbo_user_2.SelectedIndex > -1)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Fill_user";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    USER_id.Text = dr["ID_USER"].ToString();
                    USER_name.Text = dr["USER_NAME"].ToString();
                    USER_PASSWORD.Text = dr["PASSWORD"].ToString();
                    USER_phone_1.Text = dr["USER_LEVEL"].ToString();
                    USER_phone_1.Text = dr["USER_MOBILE_1"].ToString();
                    USER_phone_2.Text = dr["USER_MOBILE_2"].ToString();
                    USER_phone_3.Text = dr["USER_MOBILE_3"].ToString();
                    USER_EMAIL.Text = dr["USER_EMAIL"].ToString();
                    USER_Adress.Text = dr["USER_ADRESS"].ToString();
                    //pic_USER.Image = Show_image(dr["USER_IMAGE"]);                              /// جلب الصورة
                    cbo_USER_IN_WORK.Text = dr["IN_WORK"].ToString();
                    USER_friend.Text = dr["USER_Friend"].ToString();
                    USER_JOB.Text = dr["USER_job"].ToString();
                    txt_USER_insert_date.Text = dr["USER_Date_Now"].ToString();
                    txt_USER_last_Edits.Text = dr["USER_Date_Edits"].ToString();
                    USER_date_of_Dealing.Text = dr["USER_Date_Dealing"].ToString();
                    USER_Details.Text = dr["USER_DETAILS"].ToString();
                }
                dr.Close();
                con.Close();

                if (USER_name.Text.Trim() == "ADMINISTRATOR" || USER_PASSWORD.Text.Trim() == "28807211300376")
                {
                    USER_PASSWORD.Text = "";
                    USER_name.Enabled = false;
                    btn_Delete_USER.Enabled = false;
                    btn_save_USER.Enabled = false;
                    btn_Add_img_USER.Enabled = false;
                }
                else
                {
                    USER_name.Enabled = true;
                    btn_Delete_USER.Enabled = true;
                    btn_save_USER.Enabled = true;
                    btn_Add_img_USER.Enabled = true;
                }
            }
        }

        //void conv_photo()    //converting photo to binary data
        //{
        //    
        //    if (pic_USER.Image != null)
        //    {
        //        MemoryStream ms = new MemoryStream();
        //        pic_USER.Image.Save(ms, ImageFormat.Jpeg);
        //        byte[] photo_aray = new byte[ms.Length];
        //        ms.Position = 0;
        //        ms.Read(photo_aray, 0, photo_aray.Length);
        //        cmd.Parameters.AddWithValue("@image", photo_aray);
        //    }
        //}
        public void Fill_USERS_index_change(ComboBox cbo, string fld)
        {
            if (cbo.SelectedIndex > -1)
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string querry = "SELECT * FROM TBL_USERS where '"+fld+"' = N'"+cbo.Text.Trim() + "' ";
                SqlCommand cmd = new SqlCommand(querry , con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    USER_id.Text = dr["ID_USER"].ToString();
                    USER_name.Text = dr["USER_NAME"].ToString();
                    //decimal s =Convert.ToDecimal( dr["PASSWORD"].ToString()); //s = s / 99; //USER_PASSWORD.Text = s.ToString();
                    USER_PASSWORD.Text = dr["PASSWORD"].ToString();
                    USER_phone_1.Text = dr["USER_LEVEL"].ToString();
                    USER_phone_1.Text = dr["USER_MOBILE_1"].ToString();
                    USER_phone_2.Text = dr["USER_MOBILE_2"].ToString();
                    USER_phone_3.Text = dr["USER_MOBILE_3"].ToString();
                    USER_EMAIL.Text = dr["USER_EMAIL"].ToString();
                    USER_Adress.Text = dr["USER_ADRESS"].ToString();
                    //pic_USER.Image = Show_image(dr["USER_IMAGE"]);  
                    cbo_USER_IN_WORK.Text = dr["IN_WORK"].ToString();
                    USER_friend.Text = dr["USER_Friend"].ToString();
                    USER_JOB.Text = dr["USER_job"].ToString();
                    txt_USER_insert_date.Text = dr["USER_Date_Now"].ToString();
                    txt_USER_last_Edits.Text = dr["USER_Date_Edits"].ToString();
                    USER_date_of_Dealing.Text = dr["USER_Date_Dealing"].ToString();
                    USER_Details.Text = dr["USER_DETAILS"].ToString();
                }
                dr.Close();
                con.Close();
                if (USER_name.Text.Trim() == "ADMIN" || USER_PASSWORD.Text.Trim() == "99" )
                {
                    USER_PASSWORD.Text = "";
                    USER_name.Enabled = false;
                    btn_Delete_USER.Enabled = false;
                    btn_save_USER.Enabled = false;
                    btn_Add_img_USER.Enabled = false;
                }
                else 
                {
                    USER_name.Enabled = true;
                    btn_Delete_USER.Enabled = true;
                    btn_save_USER.Enabled = true;
                    btn_Add_img_USER.Enabled = true;
                }
            }
        }     // Fill TextBoxs From ComboBox

        private void btn_new_USER_Click(object sender, EventArgs e)
        {
            Fill_Users();
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
        }

        private void chk_USER_name_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_USER_name.Checked)
            {
                chk_USER_id.Checked = false;
                chk_USER_NATIONAL_ID.Checked = false;
                cbo_USER.Items.Clear();
                cbo_USER.Text = string.Empty;
                Fill_Users();
            }
        }

        private void chk_USER_National_id_CheckedChanged(object sender, EventArgs e)
        {
            chk_USER_name.Checked = false;
            chk_USER_id.Checked = false;
            cbo_USER.Items.Clear();
            cbo_USER.Text = string.Empty;
            Fill_Users();
        }

        private void chk_USER_id_CheckedChanged(object sender, EventArgs e)
        {
            chk_USER_name.Checked = false;
            chk_USER_NATIONAL_ID.Checked = false;
            cbo_USER.Items.Clear();
            cbo_USER.Text = string.Empty;
            Fill_Users();
        }

        private void frm_USERS_Load(object sender, EventArgs e)
        {
            Fill_Users();
            fill_employes(cbo_Employee);

            USER_user_name.Text = Main_Form.USER_NAME_STRING.Trim();
        }

        public void fill_employes(ComboBox cbo1)
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            con.Open();
            cmd = new SqlCommand("Select * From TBL_EMPLOYIES", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                cbo1.Items.Add(dr["EMPLOYEE_NAME"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void cbo_Employee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbo_Employee.SelectedIndex > -1)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    cmd = new SqlCommand("Select * From Tbl_Employies Where EMPLOYEE_NAME = N'" + cbo_Employee.Text.Trim() + "' ", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        USER_id.Text = dr["ID_EMPLOYEE"].ToString();
                        USER_name.Text = dr["EMPLOYEE_NAME"].ToString();
                        USER_national_id.Text = dr["EMPLOYEE_National_ID"].ToString();
                        USER_phone_1.Text = dr["EMPLOYEE_MOBILE_1"].ToString();
                        USER_phone_2.Text = dr["EMPLOYEE_MOBILE_2"].ToString();
                        USER_phone_3.Text = dr["EMPLOYEE_MOBILE_3"].ToString();
                        USER_EMAIL.Text = dr["EMPLOYEE_EMAIL"].ToString();
                        cbo_USER_IN_WORK.Text = dr["EMPLOYEE_JOBE"].ToString();
                        USER_Adress.Text = dr["EMPLOYEE_ADRESS"].ToString();
                        USER_JOB.Text = dr["EMPLOYEE_JOBE"].ToString();
                        txt_USER_insert_date.Text = dr["EMPLOYEE_DATE_INSERT"].ToString();
                        USER_date_of_Dealing.Text =Convert.ToString( dr["EMPLOYEE_DATE_INSERT"]).ToString();
                        txt_USER_last_Edits.Text = dr["EMPLOYEE_DATE_EDITE"].ToString();
                    }
                    //try
                    //{
                    //    pic_USER.Image = Image.FromFile(folderpath + "\\" + USER_id.Text.Trim() + 1 + ".jpg");
                    //}
                    //catch (Exception )
                    //{

                    //}
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ) { }
        }        // الموظفين 
      
        private void cbo_USER_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chk_USER_name.Checked)
            {
                Fill_USERS_index_change(cbo_USER, "'+ USER_NAME +'");
            }
            else if (chk_USER_id.Checked)
            {
                Fill_USERS_index_change(cbo_USER, "'+ ID_USER +'");
            }
            else if (chk_USER_NATIONAL_ID.Checked)
            {
                Fill_USERS_index_change(cbo_USER, "'+ USER_National_ID +'");
            }
        }            // المستخدمين

        private void btn_Delete_USER_Click(object sender, EventArgs e)
        {
            if (USER_name.Text.Trim() == "ADMIN" || USER_PASSWORD.Text.Trim() == "28807211300")
            { MessageBox.Show("لا يمكن التلاعب بالمسئول "); }
            else if (USER_id.Text.Trim() != "")
            {
                if (MessageBox.Show("انتبة ربما يكون هذا العنصر مرتبط بعناصر اخرى", "تنبيه بالحذف", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    string querry = " DELETE FROM TBL_USERS where ID_USER = N'" + USER_id.Text.Trim() + "' ";
                    cmd = new SqlCommand(querry, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Fill_Users();
                    msg_USER.Text = " تم الحذف بنجاح ";
                }
            }
            else if (USER_id.Text.Trim() == "") { msg_USER.Text = "  من فضلك اختر العنصر المراد حذفه  " ; }
        }

        public Int64 max_id()
        {
            if (con.State == ConnectionState.Open) { con.Close(); }
            Int64 id = 0;
            con.Open();
            cmd = new SqlCommand("Select max(ID_USER) as fds From TBL_USERS ", con);
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

        public byte[] byteArray(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, img.RawFormat);
            byte[] byteImage = ms.ToArray();
            return byteImage;
        }

        private void btn_save_USER_Click(object sender, EventArgs e)
        {
            //if (USER_name.Text.Trim() == "ADMIN")
            
            if (USER_name.Text.Trim() == "ADMIN" || USER_PASSWORD.Text.Trim() == "28807211300")
            { MessageBox.Show("لا يمكن التلاعب بالمسئول"); }
            else if (USER_id.Text.Trim() == "" && USER_name.Text.Trim() != "" )
            {
                //decimal pass = Convert.ToDecimal( USER_PASSWORD.Text.Trim());
                //pass = pass * 99;
                //string pas_ = pass.ToString();
                string id = Convert.ToString(max_id());
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Querry = " Insert Into TBL_USERS VALUES ('"+ id + "',N'"+ USER_name.Text.Trim()+ "',N'" + USER_PASSWORD.Text.Trim() + "'," +
                    "N'" + cbo_USER_LEVEL.Text.Trim() + "',N'" + USER_phone_1.Text.Trim() + "',N'" + USER_phone_2.Text.Trim() + "'," +
                    "N'" + USER_phone_3.Text.Trim() + "',N'" + USER_EMAIL.Text.Trim() + "',N'" + USER_Adress.Text.Trim() + "'," +
                    "N'" + USER_national_id.Text.Trim() + "','"+byteArray(pic_USER.Image)+"' ,N'" + cbo_USER_IN_WORK.Text.Trim() + "'," +
                    "N'" + USER_friend.Text.Trim() + "',N'" + USER_JOB.Text.Trim() + "',N'" + USER_Date_Time_Now.Text.Trim() + "'," +
                    "N'" + txt_USER_last_Edits.Text.Trim() + "',N'" + USER_date_of_Dealing.Text.Trim() + "',N'" + USER_Details.Text.Trim() + "') ";

                cmd = new SqlCommand(Querry , con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@USER_id", id);
                cmd.Parameters.AddWithValue("@USER_NAME", USER_name.Text.Trim());
                cmd.Parameters.AddWithValue("@PASSWORD", USER_PASSWORD.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_LEVEL", cbo_USER_LEVEL.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_MOBILE_1", USER_phone_1.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_MOBILE_2", USER_phone_2.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_MOBILE_3", USER_phone_3.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_EMAIL", USER_EMAIL.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_ADRESS", USER_Adress.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_National_ID", USER_national_id.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_IMAGE", byteArray(pic_USER.Image));
                cmd.Parameters.AddWithValue("@IN_WORK", cbo_USER_IN_WORK.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_Friend", USER_friend.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_job", USER_JOB.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_Date_Now", USER_Date_Time_Now.Text.Trim());
                //cmd.Parameters.AddWithValue("@USER_Date_Edits", .Text.Trim());
                cmd.Parameters.AddWithValue("@USER_Date_Dealing", USER_date_of_Dealing.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_DETAILS", USER_Details.Text.Trim());
                con.Close();
                Fill_Users();
                msg_USER.Text = "تم الحفظ بنجاح  " ;
            }
            else if (USER_id.Text.Trim() != "")
            {
                Edit_User();
            }
            else { msg_USER.Text = " من فضلك أدخل البيانات " ; }
        }

        public void Edit_User()
        {
            try
            {

                if (USER_id.Text.Trim() != "")
                {
                    string id = Convert.ToString(max_id());
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Open();
                    string Querry = " update TBL_USERS set USER_NAME = N'" + USER_name.Text.Trim() + "',PASSWORD = N'" + USER_PASSWORD.Text.Trim() + "'," +
                        "USER_LEVEL=N'" + cbo_USER_LEVEL.Text.Trim() + "',USER_MOBILE_1=N'" + USER_phone_1.Text.Trim() + "',USER_MOBILE_2=N'" + USER_phone_2.Text.Trim() + "'," +
                        "USER_MOBILE_3 = N'" + USER_phone_3.Text.Trim() + "',USER_EMAIL=N'" + USER_EMAIL.Text.Trim() + "',USER_ADRESS=N'" + USER_Adress.Text.Trim() + "'," +
                        "USER_National_ID=N'" + USER_national_id.Text.Trim() + "',USER_IMAGE='" + byteArray(pic_USER.Image) + "' ,IN_WORK=N'" + cbo_USER_IN_WORK.Text.Trim() + "'," +
                        "USER_Friend=N'" + USER_friend.Text.Trim() + "',USER_job=N'" + USER_JOB.Text.Trim() + "',USER_Date_Now = N'" + USER_Date_Time_Now.Text.Trim() + "'," +
                        "USER_Date_Edits=N'" + txt_USER_last_Edits.Text.Trim() + "',USER_Date_Dealing=N'" + USER_date_of_Dealing.Text.Trim() + "',USER_DETAILS=N'" + USER_Details.Text.Trim() + "' where ID_USER = N'" + USER_id.Text.Trim() + "' ";

                    cmd = new SqlCommand(Querry, con);
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.AddWithValue("@USER_NAME", USER_name.Text.Trim());
                    cmd.Parameters.AddWithValue("@PASSWORD", USER_PASSWORD.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_LEVEL", cbo_USER_LEVEL.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_MOBILE_1", USER_phone_1.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_MOBILE_2", USER_phone_2.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_MOBILE_3", USER_phone_3.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_EMAIL", USER_EMAIL.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_ADRESS", USER_Adress.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_National_ID", USER_national_id.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_IMAGE", byteArray(pic_USER.Image));
                    cmd.Parameters.AddWithValue("@IN_WORK", cbo_USER_IN_WORK.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_Friend", USER_friend.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_job", USER_JOB.Text.Trim());
                    //cmd.Parameters.AddWithValue("@USER_Date_Now", USER_Date_Time_Now.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_Date_Edits", USER_Date_Time_Now.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_Date_Dealing", USER_date_of_Dealing.Text.Trim());
                    cmd.Parameters.AddWithValue("@USER_DETAILS", USER_Details.Text.Trim());

                    Fill_Users();
                    msg_USER.Text = "تم التعديل بنجاح  ";
                }
            }
            catch (Exception)
            {

            }
            finally { con.Close(); }
       
        }

        private void btn_Add_img_USER_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "أختر صورة من هنا";
            of.Filter = "Image Files|*.jpg;*.png;*.bmp;*.*;(*.*);";
            of.Multiselect = true;
            if (of.ShowDialog() == DialogResult.OK)
            {
                pic_USER.Image = Image.FromFile(of.FileName);
            }
        }

        public Image Show_image(object FLD_Picture)     ///  محمد رضا
        {
            byte[] data = (byte[])FLD_Picture;
            MemoryStream ms = new MemoryStream(data);
            ms.Write(data, 0, data.Length);
            Image img = Image.FromStream(ms);
            return img;
        }

        private void USER_PASSWORD_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void frm_USERS_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select users_open,users_save,users_delete,users_new From USERS_PERMESSIONS Where USER_NAME=N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader dr7 = cmd.ExecuteReader();
                if (dr7.Read())
                {
                    if (dr7["users_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr7["users_save"].ToString() == "True") { } else { btn_save_USER.Enabled = false; }
                    if (dr7["users_delete"].ToString() == "True") { } else { btn_Delete_USER.Enabled = false; }
                    if (dr7["users_new"].ToString() == "True") { } else { btn_new_USER.Enabled = false; }
                }
                dr7.Close();
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

    }
}
