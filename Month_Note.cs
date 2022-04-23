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
    public partial class Month_Note : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        public Month_Note()
        {
            InitializeComponent();
        }

        public void search_invoice()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                //string sql = "select * from TBL_ORDER  where DATE ='" + Date_N.Text.Trim()+ "' ";
                string sql = "Select ID,ID_Order_Invoice,Amount_of_product,TOTAL,PAYING,The_rest from TBL_ORDER where DATE Between '" + Date_N.Text.Trim() + "' And '" + Date_2.Text.Trim() + "' ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGV1.DataSource = dt;
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        public void search_billing()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                //string sql2 = "select * from TBL_BILLING  where DATE_TIME_NOW = '" + Date_N.Text.Trim() + "' ";

                string sql2 = "select ID,TOTAL_AMOUNT,TOTAL_AFTER_DISCOUNT,PAY_FIRST,THE_REST from TBL_BILLING where DATE_TIME_NOW Between '" + Date_N.Text.Trim() + "' And '" + Date_2.Text.Trim() + "' ";
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.ExecuteNonQuery();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                DGV2.DataSource = dt2;
            }
            catch (Exception)
            {

            }
            finally { con.Close(); }
        }

        public void search_pay()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                //string sql3 = "select * from TBL_PAY_RECEIPT where DATE_NOW = '" + Date_N.Text.Trim() + "' ";

                string sql3 = "select ID,ID_ORDER_AUTO,ID_ORDER_HAND,AMOUNT,TOTAL_AFTER_DISCOUNT,Cash,total_rest from TBL_PAY_RECEIPT where DATE_NOW Between '" + Date_N.Text.Trim() + "' And '" + Date_2.Text.Trim() + "' ";
                SqlCommand cmd3 = new SqlCommand(sql3, con);
                cmd3.ExecuteNonQuery();
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                DGV3.DataSource = dt3;
            }
            catch (Exception)
            {

            }
            finally { con.Close(); }
        }

        public void search_catch()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                //string sql4 = "select * from TBL_CATCH_RECEIPT where DATE_NOW = '" + Date_N.Text.Trim() + "' ";
                string sql4 = "select ID,ID_ORDER_AUTO,ID_ORDER_HAND,AMOUNT,TOTAL_AFTER_DISCOUNT,Cash,total_rest from TBL_CATCH_RECEIPT  where DATE_NOW Between '" + Date_N.Text.Trim() + "' And '" + Date_2.Text.Trim() + "' ";
                SqlCommand cmd4 = new SqlCommand(sql4, con);
                cmd4.ExecuteNonQuery();
                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                DGV4.DataSource = dt4;
            }
            catch (Exception) { }
            finally { con.Close(); }
        }

        public void BALANCE_1()
        {
            decimal A = 0;
            decimal B = 0;
            decimal C = 0;
            try
            {
                for (int i = 0; i < DGV1.Rows.Count; i++)
                {
                    A += Convert.ToDecimal(DGV1.Rows[i].Cells["TOTAL"].Value.ToString());
                    B += Convert.ToDecimal(DGV1.Rows[i].Cells["The_rest"].Value.ToString());
                    C += Convert.ToDecimal(DGV1.Rows[i].Cells["PAYING"].Value.ToString());
                }
            }
            catch (Exception) { }
            finally
            {
                total_1.Text = A.ToString().Replace("-", "");
                rest_1.Text = B.ToString().Replace("-", "");
                pay_1.Text = C.ToString().Replace("-", "");
            }
        }

        public void BALANCE_2()
        {
            decimal A = 0;
            decimal B = 0;
            decimal C = 0;
            try
            {
                for (int i = 0; i < DGV2.Rows.Count; i++)
                {
                    A += Convert.ToDecimal(DGV2.Rows[i].Cells["TOTAL_AFTER_DISCOUNT"].Value.ToString());
                    B += Convert.ToDecimal(DGV2.Rows[i].Cells["PAY_FIRST"].Value.ToString());
                    C += Convert.ToDecimal(DGV2.Rows[i].Cells["TH_REST"].Value.ToString());
                }
            }
            catch (Exception) { }
            finally
            {
                total_2.Text = A.ToString().Replace("-", "");
                rest_2.Text = B.ToString().Replace("-", "");
                pay_2.Text = C.ToString().Replace("-", "");
            }
        }

        public void BALANCE_3()
        {
            decimal A = 0;
            decimal B = 0;
            decimal C = 0;
            try
            {
                for (int i = 0; i < DGV3.Rows.Count; i++)
                {
                    A += Convert.ToDecimal(DGV3.Rows[i].Cells["TOTAL_AFTER_DISCOUNT3"].Value.ToString());
                    B += Convert.ToDecimal(DGV3.Rows[i].Cells["Cash"].Value.ToString());
                    C += Convert.ToDecimal(DGV3.Rows[i].Cells["total_rest"].Value.ToString());
                }
            }
            catch (Exception) { }
            finally
            {
                total_3.Text = A.ToString().Replace("-", "");
                rest_3.Text = B.ToString().Replace("-", "");
                pay_3.Text = C.ToString().Replace("-", "");
            }
        }

        public void BALANCE_4()
        {
            decimal A = 0;
            decimal B = 0;
            decimal C = 0;
            try
            {
                for (int i = 0; i < DGV4.Rows.Count; i++)
                {
                    A += Convert.ToDecimal(DGV4.Rows[i].Cells["TOTAL_AFTER_DISCOUNT_4"].Value.ToString());
                    B += Convert.ToDecimal(DGV4.Rows[i].Cells["total_rest_4"].Value.ToString());
                    C += Convert.ToDecimal(DGV4.Rows[i].Cells["Cash_4"].Value.ToString());
                }
            }
            catch (Exception) { }
            finally
            {
                total_4.Text = A.ToString().Replace("-", "");
                rest_4.Text = B.ToString().Replace("-", "");
                pay_4.Text = C.ToString().Replace("-", "");
            }
        }

        public void All_Sum()
        {
            try
            {
                decimal t1 = 0; decimal t2 = 0; decimal t3 = 0; decimal t4 = 0; decimal T_All = 0;
                t1 = Convert.ToDecimal(total_1.Text.Trim());
                t2 = Convert.ToDecimal(total_2.Text.Trim());
                t3 = Convert.ToDecimal(total_3.Text.Trim());
                t4 = Convert.ToDecimal(total_4.Text.Trim());

                T_All = Convert.ToDecimal(t1 + t2 + t3 + t4);
                Total_All.Text = Convert.ToString(T_All);

                decimal P1 = 0; decimal P2 = 0; decimal P3 = 0; decimal P4 = 0; decimal P_All = 0;

                P1 = Convert.ToDecimal(pay_1.Text.Trim());
                P2 = Convert.ToDecimal(pay_2.Text.Trim());
                P3 = Convert.ToDecimal(pay_3.Text.Trim());
                P4 = Convert.ToDecimal(pay_4.Text.Trim());

                P_All = Convert.ToDecimal(P1 + P2 + P3 + P4);
                PAY_ALL.Text = Convert.ToString(P_All);

                decimal D1 = 0; decimal D2 = 0; decimal D3 = 0; decimal D4 = 0; decimal D_All = 0;

                D1 = Convert.ToDecimal(rest_1.Text.Trim());
                D2 = Convert.ToDecimal(rest_2.Text.Trim());
                D3 = Convert.ToDecimal(rest_3.Text.Trim());
                D4 = Convert.ToDecimal(rest_4.Text.Trim());

                D_All = Convert.ToDecimal(D1 + D2 + D3 + D4);
                REST_ALL.Text = Convert.ToString(D_All);

            } catch (Exception){ }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            search_invoice();
            search_billing();
            search_pay();
            search_catch();
            BALANCE_1();
            BALANCE_2();
            BALANCE_3();
            BALANCE_4();
            All_Sum();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                string all = Total_All.Text.Trim();
                string pay = PAY_ALL.Text.Trim();
                string rest = REST_ALL.Text.Trim();
                string x = (" إجمالي المستحق" + all + "\n" + " إجمالي دائن " + pay + "\n" + " إجمالي مدين " + rest );

                e.Graphics.DrawString( x , new Font("Tahoma", 20), Brushes.Black, 10, 20); 
            }
            catch (Exception){ }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void btn_Preview_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void btn_Setup_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }
    }
}
