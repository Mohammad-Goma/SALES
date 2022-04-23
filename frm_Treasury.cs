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
    public partial class frm_Treasury : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");
        SqlCommand cmd;
        public frm_Treasury()
        {
            InitializeComponent();
        }

        private void frm_Treasury_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'realApplicationDataSet3.TBL_Deposit' table. You can move, or remove it, as needed.
            //this.tBL_DepositTableAdapter.Fill(this.realApplicationDataSet3.TBL_Deposit);
            //// TODO: This line of code loads data into the 'realApplicationDataSet2.TBL_PAY_RECEIPT' table. You can move, or remove it, as needed.
            //this.tBL_PAY_RECEIPTTableAdapter.Fill(this.realApplicationDataSet2.TBL_PAY_RECEIPT);
            //// TODO: This line of code loads data into the 'realApplicationDataSet1.TBL_BILLING' table. You can move, or remove it, as needed.
            //this.tBL_BILLINGTableAdapter.Fill(this.realApplicationDataSet1.TBL_BILLING);
            //// TODO: This line of code loads data into the 'realApplicationDataSet.TBL_ORDER' table. You can move, or remove it, as needed.
            //this.tBL_ORDERTableAdapter.Fill(this.realApplicationDataSet.TBL_ORDER);

            txt_user_name.Text = Main_Form.USER_NAME_STRING.Trim();
            //BALANCE_1(); 
            //BALANCE_2(); 
            //BALANCE_3(); 
            //BALANCE_4();
        }

        private void btn_search_2_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); } con.Open();
                string dat1 = Convert.ToString(from_date_2.Text.Trim());
                string dat2 = Convert.ToString(to_date_2.Text.Trim());
                string sql = " Select * from TBL_ORDER where DATE BETWEEN '"+ dat1 + "' And '"+ dat2 + "' order By Date ";    //.ToString("MM/dd/yyyy") 
                cmd = new SqlCommand(sql , con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGV2.DataSource = dt; 
                BALANCE_2();
            }
            catch (Exception){}
            finally{con.Close();}
            //txt_amount_back_product.Text = (Convert.ToDecimal(txt_amount_back_product.Text.Trim()) + qty_ord).ToString();
            //txt_lasttotal_back.Text = (Convert.ToDecimal(txt_lasttotal_back.Text.Trim()) + total).ToString();
        }

        private void btn_search_3_Click(object sender, EventArgs e)
        {
            try
            {
                string dat1 = Convert.ToString(from_date_3.Text.Trim());
                string dat2 = Convert.ToString(to_date_3.Text.Trim());
                con.Open();
                string sql = " Select * From TBL_BILLING WHERE DATE_TIME_NOW BETWEEN '" + dat1 + "' And '" + dat2 + "' order By DATE_TIME_NOW ";
                cmd = new SqlCommand(sql,con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGV3.DataSource = dt;
                BALANCE_3();
            } catch (Exception){ }
            finally { con.Close(); }
            //txt_amount_back_product.Text = (Convert.ToDecimal(txt_amount_back_product.Text.Trim()) + qty_ord).ToString();
            //txt_lasttotal_back.Text = (Convert.ToDecimal(txt_lasttotal_back.Text.Trim()) + total).ToString();

        }

        private void btn_search_4_Click(object sender, EventArgs e)
        {
            try
            {
                string dat1 = Convert.ToString(from_date_4.Text.Trim());
                string dat2 = Convert.ToString(to_date_4.Text.Trim());
                con.Open();
                string sql = " Select * From TBL_PAY_RECEIPT WHERE DATE_NOW BETWEEN '" + dat1 + "' And '" + dat2 + "' order By DATE_NOW ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGV4.DataSource = dt;
                BALANCE_4();
            } catch (Exception){ }
            finally{ con.Close(); }
            //txt_amount_back_product.Text = (Convert.ToDecimal(txt_amount_back_product.Text.Trim()) + qty_ord).ToString();
            //txt_lasttotal_back.Text = (Convert.ToDecimal(txt_lasttotal_back.Text.Trim()) + total).ToString();

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                string dat1 = Convert.ToString(from_date.Text.Trim());
                string dat2 = Convert.ToString(to_date.Text.Trim());
                con.Open();
                string sql = " Select * From TBL_Deposit WHERE DATE_TIME_NOW BETWEEN '" + dat1 + "' And '" + dat2 + "' ";  // order By DATE_TIME_NOW 
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DGV.DataSource = dt;
                //Depositing.Text = dataGridView1.DataGridViewTextBoxColumn;
                BALANCE_1();
            }
            catch (Exception){}
            finally{ if (con.State == ConnectionState.Open) { con.Close(); } }
            //txt_amount_back_product.Text = (Convert.ToDecimal(txt_amount_back_product.Text.Trim()) + qty_ord).ToString();
            //txt_lasttotal_back.Text = (Convert.ToDecimal(txt_lasttotal_back.Text.Trim()) + total).ToString();

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    Int64 sum = 0;
            //    for (int i = 0; i < dataGridView1.Columns.Count; ++i)
            //    {
            //        sum += Convert.ToInt64(dataGridView1.Columns["mONEYDataGridViewTextBoxColumn"]);
            //    }
            //    Depositing.Text = sum.ToString();
            //} catch (Exception) { }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            //Int64 sum = 0;
            //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            //{
            //    sum += Convert.ToInt64(dataGridView1.Rows[i].Cells["mONEYDataGridViewTextBoxColumn"].Value);
            //}

            //Depositing.Text = sum.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Int64 sum = 0;
            //for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            //{
            //    sum += Convert.ToInt64(dataGridView1.Rows[i].Cells["mONEYDataGridViewTextBoxColumn"].Value);
            //}
            //Depositing.Text = sum.ToString();

        }

        public void BALANCE_1()
        {
            decimal A = 0;
            try
            {
                for (int i = 0; i < DGV.Rows.Count; i++)
                {
                    A += Convert.ToDecimal(DGV.Rows[i].Cells["mONEYDataGridViewTextBoxColumn"].Value.ToString());
                }
            }
            catch (Exception) { }
            finally
            {
                Depositing.Text = A.ToString().Replace("-", "");
            }
        }

        public void BALANCE_2()
        {
            decimal A = 0;
            try
            {
                for (int i = 0; i < DGV2.Rows.Count; i++)
                {
                    A += Convert.ToDecimal(DGV2.Rows[i].Cells["pAYINGDataGridViewTextBoxColumn"].Value.ToString());
                }
            }
            catch (Exception) { }
            finally
            {
                Revenues.Text = A.ToString().Replace("-", "");
            }
        }

        public void BALANCE_3()
        {
            decimal A = 0;
            try
            {
                for (int i = 0; i < DGV3.Rows.Count; i++)
                {
                    A += Convert.ToDecimal(DGV3.Rows[i].Cells["pAYFIRSTDataGridViewTextBoxColumn"].Value.ToString());
                }
            }
            catch (Exception) { }
            finally
            {
                Purchases.Text = A.ToString().Replace("-", "");
            }
        }

        public void BALANCE_4()
        {
            decimal A = 0;
            try
            {
                for (int i = 0; i < DGV4.Rows.Count; i++)
                {
                    A += Convert.ToDecimal(DGV4.Rows[i].Cells["cashDataGridViewTextBoxColumn"].Value.ToString());
                }
            } catch (Exception) { }
            finally
            {
                Expenses.Text = A.ToString().Replace("-", "");
            }
        }

        private void Depositing_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal a = 0; decimal b = 0; decimal c = 0; decimal d = 0; decimal a_b = 0; decimal c_d = 0; a = Convert.ToDecimal(Depositing.Text.Trim()); b = Convert.ToDecimal(Revenues.Text.Trim()); c = Convert.ToDecimal(Purchases.Text.Trim()); d = Convert.ToDecimal(Expenses.Text.Trim()); a_b = a + b; c_d = c + d; Net_profit.Text = (a_b - c_d).ToString();
            }
            catch (Exception) {  }
        }
    }
}
