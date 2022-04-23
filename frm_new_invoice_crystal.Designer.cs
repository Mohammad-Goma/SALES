namespace SALES
{
    partial class frm_new_invoice_crystal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BigInvoice_view = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // BigInvoice_view
            // 
            this.BigInvoice_view.ActiveViewIndex = -1;
            this.BigInvoice_view.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BigInvoice_view.Cursor = System.Windows.Forms.Cursors.Default;
            this.BigInvoice_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BigInvoice_view.Location = new System.Drawing.Point(0, 0);
            this.BigInvoice_view.Name = "BigInvoice_view";
            this.BigInvoice_view.Size = new System.Drawing.Size(1206, 506);
            this.BigInvoice_view.TabIndex = 0;
            this.BigInvoice_view.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // frm_new_invoice_crystal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1206, 506);
            this.Controls.Add(this.BigInvoice_view);
            this.Name = "frm_new_invoice_crystal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frm_new_invoice_crystal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion
        public CrystalDecisions.Windows.Forms.CrystalReportViewer BigInvoice_view;
    }
}