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
    public partial class REPORT_PRODUCTS : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=" + Properties.Settings.Default.Server_Name + "; Initial Catalog=" + Properties.Settings.Default.DataBaseName + "; Integrated Security=false; User ID=" + Properties.Settings.Default.UserName + "; PASSWORD=" + Properties.Settings.Default.Password + "; ");

        public REPORT_PRODUCTS()
        {
            InitializeComponent();
        }

        public void Search()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                string sql = "Select * from TBL_PRODUCTS where SECTIONS = N'" + txt_search.Text.Trim() + "' ";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    DGV.DataSource = dt;
                }
            } catch (Exception){ }
            finally {  con.Close(); }
        }
        public void Fill()
        {
            try
            {
                if (con.State == ConnectionState.Open) { con.Close(); }
                con.Open();
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                string sql = "Select SECTIONS from TBL_PRODUCTS";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    col.Add(dr.GetValue(0).ToString());
                }
                txt_search.AutoCompleteCustomSource = col;
                dr.Close();

            } catch (Exception) { }
            finally { con.Close(); }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Search();
            Balance();
        }

        private void PRODUCTS_Load(object sender, EventArgs e)
        {
            Fill();
            txt_search.SelectAll();
        }

        public void Balance()
        {
            decimal B = 0;
            decimal C = 0;
            try
            {
                for (int i = 0; i < DGV.Rows.Count; i++)
                {
                    B += Convert.ToDecimal(DGV.Rows[i].Cells["PRICE"].Value.ToString());
                    C += Convert.ToDecimal(DGV.Rows[i].Cells["Quantity"].Value.ToString());
                }
            } catch (Exception) { }
            finally
            {
                Prices.Text = B.ToString().Replace("-", "");
                QTY.Text = C.ToString().Replace("-", "");
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_search.Text.Trim() == "") { QTY.Text = ""; Prices.Text = ""; while (DGV.Rows.Count > 0) { DGV.Rows.RemoveAt(0); } }
            }
            catch (Exception)
            {
            }
        }
    }
}
