namespace SALES
{
    partial class FshowRpt
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
            this.Dept_View = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // Dept_View
            // 
            this.Dept_View.ActiveViewIndex = -1;
            this.Dept_View.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Dept_View.Cursor = System.Windows.Forms.Cursors.Default;
            this.Dept_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dept_View.Location = new System.Drawing.Point(0, 0);
            this.Dept_View.Name = "Dept_View";
            this.Dept_View.ShowRefreshButton = false;
            this.Dept_View.Size = new System.Drawing.Size(1217, 504);
            this.Dept_View.TabIndex = 0;
            this.Dept_View.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // FshowRpt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1217, 504);
            this.Controls.Add(this.Dept_View);
            this.Name = "FshowRpt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "شاشة عرض تقارير المبيعات";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FshowRpt_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer Dept_View;
    }
}