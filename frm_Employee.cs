using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Drawing2D;



namespace SALES
{
    public partial class frm_Employee : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd;
        OpenFileDialog of;
        string fileName;
        string folderpath = "C:\\Users\\Gomaa\\source\\repos\\SALES\\SALES\\Photos\\Employee";
        public string lastimagename;
        public string new_image_name;
        public frm_Employee()
        {
            InitializeComponent();
        }
        public void fill_employee(ComboBox cbo1)
        {

            con.Open();
            SqlCommand cmd2 = new SqlCommand("Select * From TBL_EMPLOYIES", con);
            SqlDataReader dr = cmd2.ExecuteReader();

            while (dr.Read())
            {       if (chk_name_emp.Checked)
                {
                    cbo1.Items.Add(dr["EMPLOYEE_NAME"].ToString());
                }
                else if (chk_id_emp.Checked)
                {
                    cbo1.Items.Add(dr["ID_EMPLOYEE"].ToString());
                }
                else if (chk_national_emp.Checked)
                {
                    cbo1.Items.Add(dr["EMPLOYEE_National_ID"].ToString());
                }
         
            }
            dr.Close();
            con.Close();
        }
        private void btn_emp_delete_Click(object sender, EventArgs e)
        {
            if (txt_id_employee.Text.Trim() != "" || txt_id_employee.Text.Trim() != null)
            {
                string FileName = pic_emp.Name;            //Convert.ToString(txt_id_employee.Text.Trim()) + ".jpg";
                string Path = folderpath + FileName;
                FileInfo file = new FileInfo(Path);
                if (file.Exists)
                {
                    file.Delete();
                }
                con.Open();
                cmd = new SqlCommand(" Delete from TBL_EMPLOYIES Where ID_EMPLOYEE = N'" + txt_id_employee.Text.Trim() + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                cbo_emp.Text = "";
                cbo_emp.Items.Clear();
                fill_employee(cbo_emp);
                txt_msg_emp.Text = " تم الحذف بنجاح";
                ClearAll();
            }
        }

        private void frm_Employee_Load(object sender, EventArgs e)
        {
            fill_employee(cbo_emp);
            txt_emp_user_name.Text = Main_Form.USER_NAME_STRING.Trim();
        }

        public void cbo_SelectedIndexChanged(ComboBox cbo, string fld)
        {
            try { 
            if (cbo.SelectedIndex > -1)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                cmd = new SqlCommand("Select * From Tbl_Employies Where N'" + fld + "' = N'" + cbo.Text.Trim() + "' ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    txt_id_employee.Text = dr["ID_EMPLOYEE"].ToString();
                    txt_employee_name.Text = dr["EMPLOYEE_NAME"].ToString();
                    employee_id_national.Text = dr["EMPLOYEE_National_ID"].ToString();
                    txt_emp_qualification.Text = dr["EMPLOYEE_qualification"].ToString();
                    txt_emp_salary.Text = dr["EMPLOYEE_SALARY"].ToString();
                    try
                    {
                        pic_emp.Image = Image.FromFile(folderpath + "\\" + txt_id_employee.Text.Trim() + 1 + ".jpg");
                    }
                    catch (Exception )
                    {

                    }
                    birth_date_emp.Value = Convert.ToDateTime(dr["EMPLOYEE_BIRTH_DATE"].ToString());
                    txt_date_insert.Text = dr["EMPLOYEE_DATE_INSERT"].ToString();
                    txt_edite_date.Text = dr["EMPLOYEE_DATE_EDITE"].ToString();
                    txt_EMPLOYEE_Social_status.Text = dr["EMPLOYEE_Social_status"].ToString();
                    txt_emp_num1.Text = dr["EMPLOYEE_MOBILE_1"].ToString();
                    txt_emp_num2.Text = dr["EMPLOYEE_MOBILE_2"].ToString();
                    txt_emp_num3.Text = dr["EMPLOYEE_MOBILE_3"].ToString();
                    txt_emp_email.Text = dr["EMPLOYEE_EMAIL"].ToString();
                    txt_emp_job.Text = dr["EMPLOYEE_JOBE"].ToString();
                    txt_emp_adress.Text = dr["EMPLOYEE_ADRESS"].ToString();
                    txt_emp_gender.Text = dr["EMPLOYEE_GENDER"].ToString();
                    cbo_on_job.Text = dr["EMPLOYEE_IN_JOBE"].ToString();
                    branch_name.Text = dr["BRANCH_NAME"].ToString();
                    store_name.Text = dr["STORE_NAME"].ToString();
                    branch_id.Text = dr["ID_BRANCH"].ToString();
                    store_id.Text = dr["ID_STORE"].ToString();
                    txt_emp_details.Text = dr["EMPLOYEE_DETAILS"].ToString();
                }
                dr.Close();
                con.Close();

                }
            }
            catch  { }
        }
 
        private void chk_name_emp_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_name_emp.Checked)
            {
                chk_id_emp.Checked = false;
                chk_national_emp.Checked = false;
                cbo_emp.Items.Clear();
                cbo_emp.Text = string.Empty;
                fill_employee(cbo_emp);
            }
        }

        private void chk_id_emp_CheckedChanged(object sender, EventArgs e)
        {
            chk_name_emp.Checked = false;
            chk_national_emp.Checked = false;
            cbo_emp.Items.Clear();
            cbo_emp.Text = string.Empty;
            fill_employee(cbo_emp);
        }

        private void chk_national_emp_CheckedChanged(object sender, EventArgs e)
        {
            chk_id_emp.Checked = false;
            chk_name_emp.Checked = false;
            cbo_emp.Items.Clear();
            cbo_emp.Text = string.Empty;
            fill_employee(cbo_emp);
        }

        public void ClearAll()
        {
            cbo_on_job.Text = "";
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    ctrl.Text = string.Empty;
                }
            }
            //pic_emp.Image = null ;
            txt_emp_user_name.Text = Main_Form.USER_NAME_STRING.Trim();
            branch_id.Text = "2";
            store_id.Text = "0";
        }

        private void btn_emp_new_Click(object sender, EventArgs e)
        {
            ClearAll();
            cbo_emp.Items.Clear();
            cbo_emp.Text = "";
            fill_employee(cbo_emp);
        }

        private void cbo_emp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chk_name_emp.Checked)
            {
                cbo_SelectedIndexChanged(cbo_emp, "'+EMPLOYEE_NAME+'");
            }
            else if (chk_id_emp.Checked)
            {
                cbo_SelectedIndexChanged(cbo_emp, "'+ID_EMPLOYEE+'");
            }
            else if (chk_national_emp.Checked)
            {
                cbo_SelectedIndexChanged(cbo_emp, "'+EMPLOYEE_National_ID+'");
            }
        }
        public void btn_add_emp_img_Click(object sender, EventArgs e)
        {
            lastimagename = pic_emp.ImageLocation ;
            of = new OpenFileDialog();
            of.Title = "أختر صورة من هنا";
            of.Filter = "Image Files|*.jpg;*.png;*.bmp;*.*;(*.*);";
            of.Multiselect = true;
            if (of.ShowDialog() == DialogResult.OK)
            {
                pic_emp.Image = Image.FromFile(of.FileName);
                fileName = of.FileName;
                new_image_name = of.FileName;
            }
        }

        private void btn_emp_save_Click(object sender, EventArgs e)
        {
            con.Close();
            int max = 0;
            con.Open();
            cmd = new SqlCommand("Select max(ID_EMPLOYEE) as fds From TBL_EMPLOYIES", con);
            cmd.ExecuteNonQuery();

            DataSet dsS = new DataSet();
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DA.Fill(dsS);

            DataRow drS;
            drS = dsS.Tables[0].Rows[0];
            max = Convert.ToInt32(drS["fds"].ToString());

            max++;
            con.Close();

            if (txt_id_employee.Text.Trim() == "")
            {
                con.Open();
                string querry = "insert into TBL_EMPLOYIES values (N'" + max + "',N'" + txt_employee_name.Text.Trim() + "'," +
                    "N'" + employee_id_national.Text.Trim() + "',N'" + txt_emp_qualification.Text.Trim() + "'," +
                    "N'" + txt_emp_salary.Text.Trim() + "',N'" + folderpath + "',N'" + birth_date_emp.Text.Trim() + "'," +
                    "N'" + date_emp_Now.Text.Trim() + "',N'" + txt_edite_date.Text.Trim() + "'," +
                    "N'" + txt_EMPLOYEE_Social_status.Text.Trim() + "'," +
                    "N'" + txt_emp_num1.Text.Trim() + "',N'" + txt_emp_num2.Text.Trim() + "',N'" + txt_emp_num3.Text.Trim() + "'," +
                    "N'" + txt_emp_email.Text.Trim() + "',N'" + txt_emp_job.Text.Trim() + "',N'" + txt_emp_adress.Text.Trim() + "'," +
                    "N'" + txt_emp_gender.Text.Trim() + "',N'" + cbo_on_job.Text.Trim() + "',N'" + branch_name.Text.Trim() + "'," +
                    "N'" + store_name.Text.Trim() + "',N'" + branch_id.Text.Trim() + "',N'" + store_id.Text.Trim() + "'," +
                    "N'" + txt_emp_details.Text.Trim() + "') ";
                cmd = new SqlCommand(querry, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@ID_EMPLOYEE", max);
                cmd.Parameters.AddWithValue("@EMPLOYEE_NAME", txt_employee_name.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_National_ID", employee_id_national.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_qualification", txt_emp_qualification.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_SALARY", txt_emp_salary.Text.Trim());
                //cmd.Parameters.AddWithValue("@EMPLOYEE_IMAGE", folderpath + Path.GetFileName(of.FileName));
                //File.Copy(fileName, Path.Combine(folderpath, Path.GetFileName(Convert.ToString(max) + 1 + ".jpg")), true);
                cmd.Parameters.AddWithValue("@EMPLOYEE_BIRTH_DATE", birth_date_emp.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_DATE_INSERT", date_emp_Now.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_Social_status", txt_EMPLOYEE_Social_status.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_MOBILE_1", txt_emp_num1.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_MOBILE_2", txt_emp_num2.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_MOBILE_3", txt_emp_num3.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_EMAIL", txt_emp_email.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_JOBE", txt_emp_job.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_ADRESS", txt_emp_adress.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_GENDER", txt_emp_gender.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_IN_JOBE", txt_emp_job.Text.Trim());
                cmd.Parameters.AddWithValue("@BRANCH_NAME", branch_name.Text.Trim());
                cmd.Parameters.AddWithValue("@STORE_NAME", store_name.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_BRANCH", branch_id.Text.Trim());  
                cmd.Parameters.AddWithValue("@ID_STORE", store_id.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_DETAILS", txt_emp_details.Text.Trim());
                con.Close();
                ClearAll();
                cbo_emp.Items.Clear();
                fill_employee(cbo_emp);
                txt_msg_emp.Text = " تم الحفظ بنجاح";
            }
            else if (txt_id_employee.Text.Trim() != "")
            {
                Edite_Emp();
            }
        }

        public void Edite_Emp()
        {
            //try { 
            if (txt_id_employee.Text.Trim() != "")
            {
                con.Open();
                cmd = new SqlCommand("UPDATE tbl_employies set EMPLOYEE_NAME = N'" + txt_employee_name.Text.Trim() + "'," +
                    "EMPLOYEE_National_ID=N'" + employee_id_national.Text.Trim() + "',EMPLOYEE_qualification=N'" + txt_emp_qualification.Text.Trim() + "'," +
                    "EMPLOYEE_SALARY=N'" + txt_emp_salary.Text.Trim() + "',EMPLOYEE_IMAGE =N'" + folderpath + "' ,EMPLOYEE_BIRTH_DATE=N'" + birth_date_emp.Text.Trim() + "'," +
                    "EMPLOYEE_DATE_INSERT= N'" + txt_date_insert.Text.Trim() + "',EMPLOYEE_DATE_EDITE = N'" + date_emp_Now.Text.Trim() + "', " +
                    "EMPLOYEE_Social_status=N'" + txt_EMPLOYEE_Social_status.Text.Trim() + "',EMPLOYEE_MOBILE_1=N'" + txt_emp_num1.Text.Trim() + "'," +
                    "EMPLOYEE_MOBILE_2=N'" + txt_emp_num2.Text.Trim() + "',EMPLOYEE_MOBILE_3 =N'" + txt_emp_num3.Text.Trim() + "',EMPLOYEE_EMAIL=N'" + txt_emp_email.Text.Trim() + "'," +
                    "EMPLOYEE_JOBE=N'" + txt_emp_job.Text.Trim() + "',EMPLOYEE_ADRESS=N'" + txt_emp_adress.Text.Trim() + "'," +
                    "EMPLOYEE_GENDER=N'" + txt_emp_gender.Text.Trim() + "',EMPLOYEE_IN_JOBE=N'" + cbo_on_job.Text.TrimEnd() + "'," +
                    "BRANCH_NAME=N'" + branch_name.Text.Trim() + "',STORE_NAME=N'" + store_name.Text.Trim() + "'," +
                    "ID_BRANCH=N'" + branch_id.Text.Trim() + "',ID_STORE=N'" + store_id.Text.Trim() + "'," +
                    "EMPLOYEE_DETAILS=N'" + txt_emp_details.Text.Trim() + "'  where ID_EMPLOYEE = N'" + txt_id_employee.Text.Trim() + "' ", con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@EMPLOYEE_NAME", txt_employee_name.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_National_ID", employee_id_national.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_qualification", txt_emp_qualification.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_SALARY", txt_emp_salary.Text.Trim());
                //pic_emp.Image=null;
                //String myPath = folderpath + "\\" + txt_id_employee.Text.Trim() + 1 + ".jpg";
                ////File.Delete(myPath);
                //cmd.Parameters.AddWithValue("@EMPLOYEE_IMAGE", folderpath + Path.GetFileName(of.FileName));
                //File.Copy(fileName, Path.Combine(folderpath, Path.GetFileName(Convert.ToString(txt_id_employee.Text.Trim()) + 1 +".jpg")), true);
                ////}
                cmd.Parameters.AddWithValue("@EMPLOYEE_BIRTH_DATE", birth_date_emp.Text.Trim());
                //cmd.Parameters.AddWithValue("@EMPLOYEE_DATE_INSERT", date_emp_Now.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_DATE_EDITE", date_emp_Now.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_Social_status", txt_EMPLOYEE_Social_status.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_MOBILE_1", txt_emp_num1.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_MOBILE_2", txt_emp_num2.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_MOBILE_3", txt_emp_num3.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_EMAIL", txt_emp_email.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_JOBE", txt_emp_job.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_ADRESS", txt_emp_adress.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_GENDER", txt_emp_gender.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_IN_JOBE", txt_emp_job.Text.Trim());
                cmd.Parameters.AddWithValue("@BRANCH_NAME", branch_name.Text.Trim());
                cmd.Parameters.AddWithValue("@STORE_NAME", store_name.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_BRANCH", branch_id.Text.Trim());
                cmd.Parameters.AddWithValue("@ID_STORE", store_id.Text.Trim());
                cmd.Parameters.AddWithValue("@EMPLOYEE_DETAILS", txt_emp_details.Text.Trim());
                con.Close();
                ClearAll();
                cbo_emp.Items.Clear();
                fill_employee(cbo_emp);
                txt_msg_emp.Text = "تم التعديل بنجاح";
                //}
                //catch (Exception ex)
                //{ txt_msg_emp.Text = ex.Message; }
            }
        }

        private void frm_Employee_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select Cutomers_open,Cutomers_save,Cutomers_delete,Cutomers_new From USERS_PERMESSIONS Where USER_NAME=N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlDataReader dr7 = cmd.ExecuteReader();
                if (dr7.Read())
                {
                    if (dr7["Cutomers_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr7["Cutomers_save"].ToString() == "True") { } else { btn_Cus_save.Enabled = false; }
                    if (dr7["Cutomers_delete"].ToString() == "True") { } else { btn_Cus_delete.Enabled = false; }
                    if (dr7["Cutomers_new"].ToString() == "True") { } else { btn_Cus_new.Enabled = false; }
                }
                dr7.Close();
            } catch (Exception) { }
            finally { con.Close(); }
        }

    }
}