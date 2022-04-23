namespace SALES
{
    partial class frm_Currency
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.msg_Currency = new System.Windows.Forms.Label();
            this.cbo_Currency = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Currency_Name = new System.Windows.Forms.TextBox();
            this.txt_Currency_Number = new System.Windows.Forms.TextBox();
            this.btn_New = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.msg_Currency);
            this.groupBox1.Controls.Add(this.cbo_Currency);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_Currency_Name);
            this.groupBox1.Controls.Add(this.txt_Currency_Number);
            this.groupBox1.Controls.Add(this.btn_New);
            this.groupBox1.Controls.Add(this.btn_Delete);
            this.groupBox1.Controls.Add(this.btn_Save);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(438, 329);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // msg_Currency
            // 
            this.msg_Currency.AutoSize = true;
            this.msg_Currency.Location = new System.Drawing.Point(207, 300);
            this.msg_Currency.Name = "msg_Currency";
            this.msg_Currency.Size = new System.Drawing.Size(0, 20);
            this.msg_Currency.TabIndex = 8;
            // 
            // cbo_Currency
            // 
            this.cbo_Currency.FormattingEnabled = true;
            this.cbo_Currency.Location = new System.Drawing.Point(40, 46);
            this.cbo_Currency.Name = "cbo_Currency";
            this.cbo_Currency.Size = new System.Drawing.Size(360, 28);
            this.cbo_Currency.TabIndex = 7;
            this.cbo_Currency.SelectedIndexChanged += new System.EventHandler(this.cbo_Currency_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(341, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "أسم العملة";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(342, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "رقم العملة";
            // 
            // txt_Currency_Name
            // 
            this.txt_Currency_Name.Location = new System.Drawing.Point(40, 155);
            this.txt_Currency_Name.Name = "txt_Currency_Name";
            this.txt_Currency_Name.Size = new System.Drawing.Size(261, 26);
            this.txt_Currency_Name.TabIndex = 4;
            // 
            // txt_Currency_Number
            // 
            this.txt_Currency_Number.Location = new System.Drawing.Point(40, 104);
            this.txt_Currency_Number.Name = "txt_Currency_Number";
            this.txt_Currency_Number.ReadOnly = true;
            this.txt_Currency_Number.Size = new System.Drawing.Size(261, 26);
            this.txt_Currency_Number.TabIndex = 3;
            // 
            // btn_New
            // 
            this.btn_New.BackColor = System.Drawing.SystemColors.Window;
            this.btn_New.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_New.Location = new System.Drawing.Point(42, 244);
            this.btn_New.Name = "btn_New";
            this.btn_New.Size = new System.Drawing.Size(93, 47);
            this.btn_New.TabIndex = 2;
            this.btn_New.Text = "جديد";
            this.btn_New.UseVisualStyleBackColor = false;
            this.btn_New.Click += new System.EventHandler(this.btn_New_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.Color.HotPink;
            this.btn_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Delete.Location = new System.Drawing.Point(170, 244);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(93, 47);
            this.btn_Delete.TabIndex = 1;
            this.btn_Delete.Text = "حذف";
            this.btn_Delete.UseVisualStyleBackColor = false;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Save.Location = new System.Drawing.Point(297, 244);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(93, 47);
            this.btn_Save.TabIndex = 0;
            this.btn_Save.Text = "حفظ";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // frm_Currency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(438, 329);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "frm_Currency";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "أختيار العملات";
            this.Load += new System.EventHandler(this.frm_Currency_Load);
            this.Shown += new System.EventHandler(this.frm_Currency_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Currency_Name;
        private System.Windows.Forms.TextBox txt_Currency_Number;
        private System.Windows.Forms.Button btn_New;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.ComboBox cbo_Currency;
        private System.Windows.Forms.Label msg_Currency;
    }
}