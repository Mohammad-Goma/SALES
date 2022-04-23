using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace SALES
{
    public partial class Rport_Of_Billing : Form
    {
        private string v1;
        private string v2;
        private string v3;
        private string v4;
        private string v5;
        private string v6;
        private string v7;
        private string v8;
        private string v9;
        private string v10;
        private string v11;
        private string v12;
        private string v13;
        private string v14;
        private string v15;
        private string v16;
        private string v17;
        private string v18;
        private string v19;
        private string v20;
        private string v21;
        private string v22;
        private string v23;
        private string v24;
        private string v25;

        public Rport_Of_Billing(string BRANCH_NAME, string STORE_NAME, string BOX_NAME,string CUSTOMER_RECIVE_BILLING, string BILLING_DELEGET_NUM, string CURENCY, string Exchange_bill_number, string METHOD_OF_TAX, string DELEGET_NAME, string TYPE_BILLING, string TOTAL_DELEGET_BILLING, string DELEGET_BILLING_DATE, string TOTAL_AMOUNT, string TOTAL_BEFORE_DISCOUNT, string DISCOUNT_PERCENT,string TOTAL_DISCOUNT, string DISCOUNT_MONEY, string TOTAL_AFTER_DISCOUNT, string txt_Total_Last,string PAY_FIRST, string THE_REST, string txt_Billing_insert_date, string txt_Billing_last_Edits_date, string txt_Billing_insert_name,string txt_Billing_Edite_Name)
        {
            this.v1 = BRANCH_NAME;
            this.v2 = STORE_NAME;
            this.v3 = BOX_NAME;
            this.v4 = CUSTOMER_RECIVE_BILLING;
            this.v5 = BILLING_DELEGET_NUM;
            this.v6 = CURENCY;
            this.v7 = Exchange_bill_number;
            this.v8 = METHOD_OF_TAX;
            this.v9 = DELEGET_NAME;
            this.v10 = TYPE_BILLING;
            this.v11 = TOTAL_DELEGET_BILLING;
            this.v12 = DELEGET_BILLING_DATE;
            this.v13 = TOTAL_AMOUNT;
            this.v14 = TOTAL_BEFORE_DISCOUNT;
            this.v15 = DISCOUNT_PERCENT;
            this.v16 = TOTAL_DISCOUNT;
            this.v17 = DISCOUNT_MONEY;
            this.v18 = TOTAL_AFTER_DISCOUNT;
            this.v19 = txt_Total_Last;
            this.v20 = PAY_FIRST;
            this.v21 = THE_REST;
            this.v22 = txt_Billing_insert_date;
            this.v23 = txt_Billing_last_Edits_date;
            this.v24 = txt_Billing_insert_name;
            this.v25 = txt_Billing_Edite_Name;
            InitializeComponent();
        }

        private void Rport_Of_Billing_Load(object sender, EventArgs e)
        {
            //PrintDialog printDialog = new PrintDialog();
            //if (printDialog.ShowDialog() == DialogResult.OK)
            //{
            //    ReportDocument reportDocument = new ReportDocument();
            //    reportDocument.Load(Application.StartupPath + "\\Billing_Crystal.rpt");
            //    reportDocument.PrintOptions.PrinterName = printDialog.PrinterSettings.PrinterName;
            //    reportDocument.PrintToPrinter(printDialog.PrinterSettings.Copies, printDialog.PrinterSettings.Collate, printDialog.PrinterSettings.FromPage, printDialog.PrinterSettings.ToPage);

            //    this.Close();

            //}
        }
    }
}
