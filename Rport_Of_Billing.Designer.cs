namespace SALES
{
    partial class Rport_Of_Billing
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
            this.billing_view = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // billing_view
            // 
            this.billing_view.ActiveViewIndex = -1;
            this.billing_view.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.billing_view.Cursor = System.Windows.Forms.Cursors.Default;
            this.billing_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.billing_view.Location = new System.Drawing.Point(0, 0);
            this.billing_view.Name = "billing_view";
            this.billing_view.Size = new System.Drawing.Size(1014, 493);
            this.billing_view.TabIndex = 0;
            this.billing_view.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // Rport_Of_Billing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 493);
            this.Controls.Add(this.billing_view);
            this.Name = "Rport_Of_Billing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rport_Of_Billing";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Rport_Of_Billing_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer billing_view;
    }
}