using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SALES
{
    public partial class frm_Deposit : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd;
        public frm_Deposit()
        {
            InitializeComponent();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if( ID_HAND.Text.Trim() == "" && MONEY.Text.Trim() == "")
            {
                msg.Text = "من فضلك امل البيانات";
            }
            else if (ID.Text == ""){ Save();}
            else if (ID.Text != ""){ Edite();}
        }

        public void Save()
        {
            try
            {
                con.Open();
                string sql = "Insert Into TBL_Deposit Values(N'" + ID_HAND.Text.Trim() + "',N'" + MONEY.Text.Trim() + "'," +
                    "N'" + NAME.Text.Trim() + "',N'" + DATE_TIME_NOW.Text.Trim() + "',N'" + USER_NAME.Text.Trim() + "'," +
                    "N'" + DATE_EDITE.Text.Trim() + "',N'" + USER_EDITE.Text.Trim() + "',N'" + DETAILS.Text.Trim() + "')";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@ID_HAND", ID_HAND.Text.Trim());
                cmd.Parameters.AddWithValue("@MONEY", MONEY.Text.Trim());
                cmd.Parameters.AddWithValue("@NAME", NAME.Text.Trim());
                cmd.Parameters.AddWithValue("@DATE_TIME_NOW", DATE_TIME_NOW.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_NAME", USER_NAME.Text.Trim());
                //cmd.Parameters.AddWithValue("@DATE_EDITE", DATE_EDITE.Text.Trim());
                //cmd.Parameters.AddWithValue("@USER_EDITE", USER_EDITE.Text.Trim());
                cmd.Parameters.AddWithValue("@DETAILS", DETAILS.Text.Trim());
                Clear();
                msg.Text = "تم الحفظ بنجاح";
            } catch (Exception) { }
            finally { con.Close(); }
        }

        public void Edite()
        {
            try
            {
                con.Open();
                string sql = "Update TBL_Deposit SET ID_HAND = N'" + ID_HAND.Text.Trim() + "',MONEY = N'" + MONEY.Text.Trim() + "'," +
                    "NAME=N'" + NAME.Text.Trim() + "',DATE_TIME_NOW = N'" + date_insert.Text.Trim() + "'," +
                    "USER_NAME = N'" + user_name_insert.Text.Trim() + "',DATE_EDITE = N'" + DATE_TIME_NOW.Text.Trim() + "'," +
                    "USER_EDITE=N'"+ USER_NAME.Text.Trim() + "'," +
                    "DETAILS = N'" + DETAILS.Text.Trim() + "' Where ID = N'"+ID.Text.Trim()+"' ";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Parameters.AddWithValue("@ID_HAND", ID_HAND.Text.Trim());
                cmd.Parameters.AddWithValue("@MONEY", MONEY.Text.Trim());
                cmd.Parameters.AddWithValue("@NAME", NAME.Text.Trim());
                //cmd.Parameters.AddWithValue("@DATE_TIME_NOW", DATE_TIME_NOW.Text.Trim());
                //cmd.Parameters.AddWithValue("@USER_NAME", USER_NAME.Text.Trim());
                cmd.Parameters.AddWithValue("@DATE_EDITE", DATE_TIME_NOW.Text.Trim());
                cmd.Parameters.AddWithValue("@USER_EDITE", USER_NAME.Text.Trim());
                cmd.Parameters.AddWithValue("@DETAILS", DETAILS.Text.Trim());
                Clear();
                msg.Text = "تم التعديل بنجاح";
            }
            catch (Exception){ }
            finally { con.Close(); }
        }

        private void frm_Deposit_Load(object sender, EventArgs e)
        {
            try
            {

                fillData();
                USER_NAME.Text = Main_Form.USER_NAME_STRING.Trim();
                //ID.Text = Convert.ToString(max());
            }
            catch (Exception)
            {
            }
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            try
            {
                ID.Text = "";
                ID_HAND.Text = "";
                MONEY.Text = "";
                NAME.Text = "";
                DATE_TIME_NOW.Text = "";
                //USER_NAME.Text = "";
                DATE_EDITE.Text = "";
                USER_EDITE.Text = "";
                DETAILS.Text = "";
                user_name_insert.Text = "";
                date_insert.Text = "";
                txt_Search.Text = "";
                fillData();
            }
            catch (Exception) { }
        }

        public Int64 max()
        {
            //try
            //{
                if (con.State == ConnectionState.Open) { con.Close(); }
                int id = 0;
                con.Open();
                cmd = new SqlCommand("Select max(ID) as fds From TBL_Deposit",con);
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
            //} catch (Exception){ }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ID.Text.Trim() == "") { msg.Text = "من فضلك اختر الصنف المراد حذفه"; return; }
                con.Open();
                cmd = new SqlCommand("DELETE FROM TBL_Deposit WHERE ID = '" + ID.Text.Trim() + "' ", con);
                cmd.ExecuteNonQuery();
                Clear();
                msg.Text = "تم الحذف بنجاح";
            }
            catch (Exception){ }
            finally { con.Close(); }
        }

        public void fillData()
        {
            try
            {
                con.Open();
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                cmd = new SqlCommand("Select * FROM TBL_Deposit " , con);
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                SqlDataReader dr = cmd.ExecuteReader();
                DGV_DEPOSITE.DataSource = dt;                                        
                while (dr.Read())
                {
                    col.Add(dr.GetString(0));
                }
                txt_Search.AutoCompleteCustomSource = col;
                dr.Close();
                msg.Text = "تم الحذف بنجاح";
            } catch (Exception) { }
            finally{ con.Close(); }
        }

        private void txt_Search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sql = "Select * From TBL_Deposit Where ID = N'"+ txt_Search.Text.Trim()+"' ";
                cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                    ID.Text = dt.Rows[0]["ID"].ToString();
                    ID_HAND.Text = dt.Rows[0]["ID_HAND"].ToString();
                    MONEY.Text = dt.Rows[0]["MONEY"].ToString();
                    NAME.Text = dt.Rows[0]["NAME"].ToString();
                    date_insert.Text = dt.Rows[0]["DATE_TIME_NOW"].ToString();
                    user_name_insert.Text = dt.Rows[0]["USER_NAME"].ToString();
                    DATE_EDITE.Text = dt.Rows[0]["DATE_EDITE"].ToString();
                    USER_EDITE.Text = dt.Rows[0]["USER_EDITE"].ToString();
                    DETAILS.Text = dt.Rows[0]["DETAILS"].ToString();
                msg.Text = "";
            } catch (Exception){ }
            finally { con.Close(); }
        }

        private void frm_Deposit_Shown(object sender, EventArgs e)
        {
            lvl_permessions();
        }
        public void lvl_permessions()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string Query = "Select insert_money_open,insert_money_save,insert_money_delete,insert_money_new From USERS_PERMESSIONS Where USER_NAME = N'" + Main_Form.USER_NAME_STRING + "' ";
                SqlCommand cmd22 = new SqlCommand(Query, con);
                SqlDataReader dr2 = cmd22.ExecuteReader();
                if (dr2.Read())
                {
                    if (dr2["insert_money_open"].ToString() == "True") { } else { this.Close(); return; }
                    if (dr2["insert_money_save"].ToString() == "True") { btn_Add.Enabled = true; } else { btn_Add.Enabled = false; }
                    if (dr2["insert_money_delete"].ToString() == "True") { btn_Delete.Enabled = true; } else { btn_Delete.Enabled = false; }
                    if (dr2["insert_money_new"].ToString() == "True") { btn_New.Enabled = true; } else { btn_New.Enabled = false; }
                } dr2.Close();
            } catch (Exception) { }
            finally { con.Close(); }
        }
    }
}
