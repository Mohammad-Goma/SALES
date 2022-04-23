namespace SALES
{
    partial class frm_product_sections
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.lblmsg = new System.Windows.Forms.Label();
            this.btncat_delete = new System.Windows.Forms.Button();
            this.btncat_edit = new System.Windows.Forms.Button();
            this.cbocat = new System.Windows.Forms.ComboBox();
            this.btncat_add = new System.Windows.Forms.Button();
            this.btncatnew = new System.Windows.Forms.Button();
            this.txtcatDetails = new System.Windows.Forms.TextBox();
            this.txtcatName = new System.Windows.Forms.TextBox();
            this.txtcatNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.branch_name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.store_name = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.branch_id = new System.Windows.Forms.TextBox();
            this.store_id = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Brown;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(9, 452);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 52);
            this.btnClose.TabIndex = 54;
            this.btnClose.Text = "Exit";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_refresh.BackColor = System.Drawing.Color.CadetBlue;
            this.btn_refresh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_refresh.Location = new System.Drawing.Point(42, 59);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(156, 40);
            this.btn_refresh.TabIndex = 53;
            this.btn_refresh.Text = "تنشيط";
            this.btn_refresh.UseVisualStyleBackColor = false;
            this.btn_refresh.Click += new System.EventHandler(this.Btn_refresh_Click);
            // 
            // lblmsg
            // 
            this.lblmsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblmsg.AutoSize = true;
            this.lblmsg.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.ForeColor = System.Drawing.Color.Red;
            this.lblmsg.Location = new System.Drawing.Point(589, 466);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(81, 23);
            this.lblmsg.TabIndex = 52;
            this.lblmsg.Text = "الرسائل";
            // 
            // btncat_delete
            // 
            this.btncat_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btncat_delete.BackColor = System.Drawing.Color.DeepPink;
            this.btncat_delete.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncat_delete.Location = new System.Drawing.Point(595, 391);
            this.btncat_delete.Name = "btncat_delete";
            this.btncat_delete.Size = new System.Drawing.Size(128, 53);
            this.btncat_delete.TabIndex = 47;
            this.btncat_delete.Text = "حذف التصنيف";
            this.btncat_delete.UseVisualStyleBackColor = false;
            this.btncat_delete.Click += new System.EventHandler(this.Btncat_delete_Click);
            // 
            // btncat_edit
            // 
            this.btncat_edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btncat_edit.BackColor = System.Drawing.Color.DarkOrchid;
            this.btncat_edit.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncat_edit.Location = new System.Drawing.Point(458, 391);
            this.btncat_edit.Name = "btncat_edit";
            this.btncat_edit.Size = new System.Drawing.Size(128, 53);
            this.btncat_edit.TabIndex = 45;
            this.btncat_edit.Text = "تعديل التصنيف";
            this.btncat_edit.UseVisualStyleBackColor = false;
            this.btncat_edit.Click += new System.EventHandler(this.Btncat_edit_Click);
            // 
            // cbocat
            // 
            this.cbocat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbocat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbocat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbocat.FormattingEnabled = true;
            this.cbocat.Location = new System.Drawing.Point(443, 65);
            this.cbocat.Name = "cbocat";
            this.cbocat.Size = new System.Drawing.Size(408, 21);
            this.cbocat.TabIndex = 50;
            this.cbocat.SelectedIndexChanged += new System.EventHandler(this.Cbocat_SelectedIndexChanged);
            // 
            // btncat_add
            // 
            this.btncat_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btncat_add.BackColor = System.Drawing.Color.DodgerBlue;
            this.btncat_add.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncat_add.Location = new System.Drawing.Point(732, 391);
            this.btncat_add.Name = "btncat_add";
            this.btncat_add.Size = new System.Drawing.Size(128, 53);
            this.btncat_add.TabIndex = 43;
            this.btncat_add.Text = "إضافة تصنيف";
            this.btncat_add.UseVisualStyleBackColor = false;
            this.btncat_add.Click += new System.EventHandler(this.Btncat_add_Click);
            // 
            // btncatnew
            // 
            this.btncatnew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btncatnew.BackColor = System.Drawing.Color.Lavender;
            this.btncatnew.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncatnew.Location = new System.Drawing.Point(321, 391);
            this.btncatnew.Name = "btncatnew";
            this.btncatnew.Size = new System.Drawing.Size(128, 53);
            this.btncatnew.TabIndex = 48;
            this.btncatnew.Text = "تصنيف جديد";
            this.btncatnew.UseVisualStyleBackColor = false;
            this.btncatnew.Click += new System.EventHandler(this.Btncatnew_Click);
            // 
            // txtcatDetails
            // 
            this.txtcatDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcatDetails.Location = new System.Drawing.Point(441, 216);
            this.txtcatDetails.Multiline = true;
            this.txtcatDetails.Name = "txtcatDetails";
            this.txtcatDetails.Size = new System.Drawing.Size(289, 148);
            this.txtcatDetails.TabIndex = 41;
            // 
            // txtcatName
            // 
            this.txtcatName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcatName.Location = new System.Drawing.Point(442, 172);
            this.txtcatName.Name = "txtcatName";
            this.txtcatName.Size = new System.Drawing.Size(288, 20);
            this.txtcatName.TabIndex = 39;
            // 
            // txtcatNumber
            // 
            this.txtcatNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtcatNumber.Location = new System.Drawing.Point(443, 131);
            this.txtcatNumber.Name = "txtcatNumber";
            this.txtcatNumber.ReadOnly = true;
            this.txtcatNumber.Size = new System.Drawing.Size(287, 20);
            this.txtcatNumber.TabIndex = 49;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(735, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 19);
            this.label4.TabIndex = 46;
            this.label4.Text = ": تفاصيل التصنيف  ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(735, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 19);
            this.label3.TabIndex = 44;
            this.label3.Text = ": اسم التصنيف    ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(737, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 19);
            this.label2.TabIndex = 42;
            this.label2.Text = ": رقم التصنيف        ";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(327, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 28);
            this.label1.TabIndex = 40;
            this.label1.Text = "شاشة تعريف التصنيفات";
            // 
            // branch_name
            // 
            this.branch_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.branch_name.Location = new System.Drawing.Point(49, 134);
            this.branch_name.Name = "branch_name";
            this.branch_name.Size = new System.Drawing.Size(251, 20);
            this.branch_name.TabIndex = 55;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(305, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 19);
            this.label5.TabIndex = 56;
            this.label5.Text = ": اسم الفرع    ";
            // 
            // store_name
            // 
            this.store_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.store_name.Location = new System.Drawing.Point(48, 175);
            this.store_name.Name = "store_name";
            this.store_name.Size = new System.Drawing.Size(251, 20);
            this.store_name.TabIndex = 57;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(304, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 19);
            this.label6.TabIndex = 58;
            this.label6.Text = ": اسم المخزن    ";
            // 
            // branch_id
            // 
            this.branch_id.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.branch_id.Location = new System.Drawing.Point(9, 134);
            this.branch_id.Name = "branch_id";
            this.branch_id.Size = new System.Drawing.Size(34, 20);
            this.branch_id.TabIndex = 59;
            this.branch_id.Visible = false;
            // 
            // store_id
            // 
            this.store_id.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.store_id.Location = new System.Drawing.Point(9, 175);
            this.store_id.Name = "store_id";
            this.store_id.Size = new System.Drawing.Size(34, 20);
            this.store_id.TabIndex = 60;
            this.store_id.Visible = false;
            // 
            // frm_product_sections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(902, 513);
            this.Controls.Add(this.store_id);
            this.Controls.Add(this.branch_id);
            this.Controls.Add(this.store_name);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.branch_name);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btn_refresh);
            this.Controls.Add(this.lblmsg);
            this.Controls.Add(this.btncat_delete);
            this.Controls.Add(this.btncat_edit);
            this.Controls.Add(this.cbocat);
            this.Controls.Add(this.btncat_add);
            this.Controls.Add(this.btncatnew);
            this.Controls.Add(this.txtcatDetails);
            this.Controls.Add(this.txtcatName);
            this.Controls.Add(this.txtcatNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frm_product_sections";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "التصنيفات";
            this.Load += new System.EventHandler(this.Product_sections_Load);
            this.Shown += new System.EventHandler(this.frm_product_sections_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.Button btncat_delete;
        private System.Windows.Forms.Button btncat_edit;
        private System.Windows.Forms.ComboBox cbocat;
        private System.Windows.Forms.Button btncat_add;
        private System.Windows.Forms.Button btncatnew;
        private System.Windows.Forms.TextBox txtcatDetails;
        private System.Windows.Forms.TextBox txtcatName;
        private System.Windows.Forms.TextBox txtcatNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox branch_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox store_name;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox branch_id;
        private System.Windows.Forms.TextBox store_id;
    }
}