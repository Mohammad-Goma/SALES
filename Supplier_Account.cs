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
    public partial class Supplier_Account : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        public Supplier_Account()
        {
            InitializeComponent();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Search_TBL_BILLING();
            Search_TBL_CATCH_RECEIPT();
            try
            {
                decimal f = Convert.ToDecimal(Debit.Text.Trim());
                decimal G = Convert.ToDecimal(Finish_2.Text.Trim());
                decimal C2 = f - G;
                Count.Text = Convert.ToString(C2).Replace("-", "");
                if (f > G) { Account_Statu.Text = "مدين بمبلغ وقدره " + C2; }
                else if (f < G) { Account_Statu.Text = "دائن بمبلغ وقدره " + C2; }
            } catch (Exception) { }
        }

        public void Search_TBL_BILLING()
        {
            try
            {
                string sql = "Select ID,DATE_TIME_NOW,TOTAL_AFTER_DISCOUNT,PAY_FIRST,THE_REST From TBL_BILLING Where DELEGET_NAME = N'" + CUSTOMER_NAME.Text.Trim() + "' And DATE_TIME_NOW Between '" + Date_1.Value.Date + "' And '" + Date_2.Value.Date + "' ";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DGV.DataSource = dt;
                }
            } catch (Exception) { }
            finally { con.Close(); BALANCE(); }
        }

        public void Search_TBL_CATCH_RECEIPT()    //ID_ORDER_AUTO,DATE_NOW,TOTAL_AFTER_DISCOUNT,Cash,total_rest
        {
            try
            {
                string sql2 = " Select ID_ORDER_AUTO,DATE_NOW,TOTAL_AFTER_DISCOUNT,Cash,total_rest From TBL_CATCH_RECEIPT Where CUSTOMER_NAME = N'" + CUSTOMER_NAME.Text.Trim() + "' And DATE_NOW Between '" + Date_1.Value.Date + "' And '" + Date_2.Value.Date + "' ";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                con.Open();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    DGV_2.DataSource = dt2;
                }
            } catch (Exception) { }
            finally { con.Close();  BALANCE2(); }
        }

        public void Select_CUSTOMER_NAME()
        {
            try
            {
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                con.Open();
                string sql = "Select DELEGET_NAME From TBL_BILLING";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    col.Add(dr.GetValue(0).ToString());
                }
                dr.Close();
                CUSTOMER_NAME.AutoCompleteCustomSource = col;
            }
            catch (Exception) { }
            finally { con.Close(); }
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
                    A += Convert.ToDecimal(DGV.Rows[i].Cells["TOTAL_AFTER_DISCOUNT"].Value.ToString());
                    B += Convert.ToDecimal(DGV.Rows[i].Cells["PAY_FIRST"].Value.ToString());
                    C += Convert.ToDecimal(DGV.Rows[i].Cells["The_rest"].Value.ToString());
                }
            }
            catch (Exception){ }
            finally
            {
                Creditor.Text = A.ToString().Replace("-","");
                Debit.Text = B.ToString().Replace("-", "");
                Finish.Text = C.ToString().Replace("-", ""); 
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
                    D += Convert.ToDecimal(DGV_2.Rows[i].Cells["TOTAL_AFTER_"].Value.ToString());
                    E += Convert.ToDecimal(DGV_2.Rows[i].Cells["total_rest"].Value.ToString());
                    F += Convert.ToDecimal(DGV_2.Rows[i].Cells["Cash"].Value.ToString());
                }
            } catch (Exception) { }
            finally 
            { 
                total_2.Text = D.ToString().Replace("-", ""); 
                rest.Text = E.ToString().Replace("-", ""); 
                Finish_2.Text = F.ToString().Replace("-", ""); 
            }
        }

        private void Supplier_Account_Load(object sender, EventArgs e)
        {
            Select_CUSTOMER_NAME();
        }

    }
}
