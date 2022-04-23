using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SALES
{
    public partial class frm_new_invoice_crystal : Form
    {
        private string cbo_user_invoice;
        private string cbo_customer;
        private string date_invoice;
        private string txt_id;
        private string txt_casheer;
        private string txt_amount_of_product;
        private string cbo_pay_way;
        private string Total_;
        private string txt_pay;
        private string txt_The_rest;
        private string Total_2;

        public frm_new_invoice_crystal( string cbo_user_invoice, string cbo_customer, string date_invoice, string txt_id,
                                        string txt_casheer, string txt_amount_of_product, string cbo_pay_way,
                                        string Total_,string txt_pay, string txt_The_rest, string Total_2)
        {
            this.cbo_user_invoice = cbo_user_invoice;
            this.cbo_customer = cbo_customer;
            this.date_invoice = date_invoice;
            this.txt_id = txt_id;
            this.txt_casheer = txt_casheer;
            this.txt_amount_of_product = txt_amount_of_product;
            this.cbo_pay_way = cbo_pay_way;
            this.Total_ = Total_;
            this.txt_pay = txt_pay;
            this.txt_The_rest = txt_The_rest;
            this.Total_2 = Total_2;
            InitializeComponent();
        }
    }
}
