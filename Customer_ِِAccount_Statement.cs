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
    public partial class Customer_Account_Statement : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");

        //SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=RealApplication;Integrated Security=True");
        //Boolean ID_order = false;
        //Boolean Name_ = false;
        public Customer_Account_Statement()
        {
            InitializeComponent();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
           
                Search_ORDER();
                Search_pay_Recipt(); 
            try
            {
                decimal f = Convert.ToDecimal(Finish.Text.Trim());
                decimal G = Convert.ToDecimal(rest.Text.Trim());
                decimal C2 = f - G;
                Count.Text = Convert.ToString(C2).Replace("-","");
                if (f > G) { Account_Statu.Text = "مدين بمبلغ وقدره " + C2; }
                else if (f < G) { Account_Statu.Text = "دائن بمبلغ وقدره " + C2; }
            } catch (Exception) { }
        }

        public void Search_ORDER()
        {
            try
            {
                string sql = "Select ID,Date,ToTal,PAYING,The_rest From TBL_ORDER Where  Date Between '" + Date_1.Text.Trim() + "' And '" + Date_2.Text.Trim() + "' And CUSTOMER_NAME = N'" + custom_name.Text.Trim() + "'  ";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DGV.DataSource = dt;
                }
            } 
            catch (Exception){ }
            finally { con.Close(); BALANCE(); }
        }

        public void Search_pay_Recipt()                  //ID_ORDER_AUTO,DATE_NOW,TOTAL_AFTER_DISCOUNT,Cash,total_rest
        {
            try
            {
                string sql2 = " Select ID_ORDER_AUTO,DATE_NOW,TOTAL_AFTER_DISCOUNT,Cash,total_rest From TBL_PAY_RECEIPT Where  DATE_NOW Between '"+Date_1.Text.Trim()+"' And '"+Date_2.Text.Trim() + "' And CUSTOMER_NAME = N'" + custom_name.Text.Trim() + "'  ";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                con.Open();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    DGV_2.DataSource = dt2;
                }
            } catch (Exception){  } finally   {  con.Close();  BALANCE2(); }
        }

        public void Select_CUSTOMER_NAME()
        {
            try
            {
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                con.Open();
                string sql = "Select CUSTOMER_NAME From TBL_ORDER";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    col.Add(dr.GetValue(0).ToString());
                }
                dr.Close();
                custom_name.AutoCompleteCustomSource = col;
            } catch (Exception) { }
            finally { con.Close(); }
        } 

        private void Customer_Account_Statement_Load(object sender, EventArgs e)
        {
            Select_CUSTOMER_NAME();
        }

        public void BALANCE()
        {
            decimal A = 0;
            decimal B = 0;
            decimal C = 0;
            try
            {
                for (int i = 0; i < DGV.Rows.Count; i++)
                {
                    A += Convert.ToDecimal(DGV.Rows[i].Cells["TOTAL"].Value.ToString());
                    B += Convert.ToDecimal(DGV.Rows[i].Cells["PAYING"].Value.ToString());
                    C += Convert.ToDecimal(DGV.Rows[i].Cells["The_rest"].Value.ToString());
                }
            } catch (Exception) {  }
            finally
            {
                Creditor.Text = A.ToString();
                Debit.Text = B.ToString();
                Finish.Text = C.ToString();
            }
        }

        public void BALANCE2()
        {
            decimal D = 0;
            decimal E = 0;
            decimal F = 0;
            try
            {
                for (int i = 0; i < DGV_2.Rows.Count; i++)
                {
                    D += Convert.ToDecimal(DGV_2.Rows[i].Cells["TOTAL_AFTER_DISCOUNT"].Value.ToString());
                    E += Convert.ToDecimal(DGV_2.Rows[i].Cells["total_rest"].Value.ToString());
                    F += Convert.ToDecimal(DGV_2.Rows[i].Cells["Cash"].Value.ToString());
                }
            }
            catch (Exception) { }
            finally
            {
                total_2.Text = D.ToString();
                rest.Text = E.ToString();
                Finish_2.Text = F.ToString();
            }
        }

    }
}
