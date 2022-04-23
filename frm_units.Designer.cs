namespace SALES
{
    partial class frm_units
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
            this.cbo_units = new System.Windows.Forms.ComboBox();
            this.txt_id_units = new System.Windows.Forms.TextBox();
            this.txt_name_units = new System.Windows.Forms.TextBox();
            this.txt_details_units = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_save_unit = new System.Windows.Forms.Button();
            this.btn_delete_units = new System.Windows.Forms.Button();
            this.btn_edits_units = new System.Windows.Forms.Button();
            this.btn_new_units = new System.Windows.Forms.Button();
            this.lbl_msg_units = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbo_units
            // 
            this.cbo_units.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbo_units.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_units.FormattingEnabled = true;
            this.cbo_units.Location = new System.Drawing.Point(103, 63);
            this.cbo_units.Name = "cbo_units";
            this.cbo_units.Size = new System.Drawing.Size(380, 28);
            this.cbo_units.TabIndex = 0;
            this.cbo_units.SelectedIndexChanged += new System.EventHandler(this.cbo_units_SelectedIndexChanged);
            // 
            // txt_id_units
            // 
            this.txt_id_units.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_id_units.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_id_units.Location = new System.Drawing.Point(103, 114);
            this.txt_id_units.Name = "txt_id_units";
            this.txt_id_units.ReadOnly = true;
            this.txt_id_units.Size = new System.Drawing.Size(380, 26);
            this.txt_id_units.TabIndex = 1;
            // 
            // txt_name_units
            // 
            this.txt_name_units.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_name_units.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_name_units.Location = new System.Drawing.Point(103, 151);
            this.txt_name_units.Name = "txt_name_units";
            this.txt_name_units.Size = new System.Drawing.Size(380, 26);
            this.txt_name_units.TabIndex = 2;
            // 
            // txt_details_units
            // 
            this.txt_details_units.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_details_units.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_details_units.Location = new System.Drawing.Point(103, 196);
            this.txt_details_units.Multiline = true;
            this.txt_details_units.Name = "txt_details_units";
            this.txt_details_units.Size = new System.Drawing.Size(380, 138);
            this.txt_details_units.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "الرقم المرجعي";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "أسم الوحدة";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "التفاصيل";
            // 
            // btn_save_unit
            // 
            this.btn_save_unit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_save_unit.BackColor = System.Drawing.Color.DarkTurquoise;
            this.btn_save_unit.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save_unit.Location = new System.Drawing.Point(23, 354);
            this.btn_save_unit.Name = "btn_save_unit";
            this.btn_save_unit.Size = new System.Drawing.Size(99, 50);
            this.btn_save_unit.TabIndex = 7;
            this.btn_save_unit.Text = "حفظ";
            this.btn_save_unit.UseVisualStyleBackColor = false;
            this.btn_save_unit.Click += new System.EventHandler(this.btn_save_unit_Click);
            // 
            // btn_delete_units
            // 
            this.btn_delete_units.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delete_units.BackColor = System.Drawing.Color.Crimson;
            this.btn_delete_units.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete_units.Location = new System.Drawing.Point(149, 354);
            this.btn_delete_units.Name = "btn_delete_units";
            this.btn_delete_units.Size = new System.Drawing.Size(99, 50);
            this.btn_delete_units.TabIndex = 8;
            this.btn_delete_units.Text = "حذف";
            this.btn_delete_units.UseVisualStyleBackColor = false;
            this.btn_delete_units.Click += new System.EventHandler(this.btn_delete_units_Click);
            // 
            // btn_edits_units
            // 
            this.btn_edits_units.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_edits_units.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_edits_units.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_edits_units.Location = new System.Drawing.Point(269, 354);
            this.btn_edits_units.Name = "btn_edits_units";
            this.btn_edits_units.Size = new System.Drawing.Size(99, 50);
            this.btn_edits_units.TabIndex = 9;
            this.btn_edits_units.Text = "تعديل";
            this.btn_edits_units.UseVisualStyleBackColor = false;
            this.btn_edits_units.Click += new System.EventHandler(this.btn_edits_units_Click);
            // 
            // btn_new_units
            // 
            this.btn_new_units.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_new_units.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_new_units.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_new_units.Location = new System.Drawing.Point(386, 354);
            this.btn_new_units.Name = "btn_new_units";
            this.btn_new_units.Size = new System.Drawing.Size(99, 50);
            this.btn_new_units.TabIndex = 10;
            this.btn_new_units.Text = "جديد";
            this.btn_new_units.UseVisualStyleBackColor = false;
            this.btn_new_units.Click += new System.EventHandler(this.btn_new_units_Click);
            // 
            // lbl_msg_units
            // 
            this.lbl_msg_units.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_msg_units.AutoSize = true;
            this.lbl_msg_units.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_msg_units.ForeColor = System.Drawing.Color.Firebrick;
            this.lbl_msg_units.Location = new System.Drawing.Point(220, 428);
            this.lbl_msg_units.Name = "lbl_msg_units";
            this.lbl_msg_units.Size = new System.Drawing.Size(56, 20);
            this.lbl_msg_units.TabIndex = 11;
            this.lbl_msg_units.Text = "الرسائل";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(220, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 24);
            this.label4.TabIndex = 12;
            this.label4.Text = "الوحدات";
            // 
            // frm_units
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(497, 457);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_msg_units);
            this.Controls.Add(this.btn_new_units);
            this.Controls.Add(this.btn_edits_units);
            this.Controls.Add(this.btn_delete_units);
            this.Controls.Add(this.btn_save_unit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_details_units);
            this.Controls.Add(this.txt_name_units);
            this.Controls.Add(this.txt_id_units);
            this.Controls.Add(this.cbo_units);
            this.MaximizeBox = false;
            this.Name = "frm_units";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "الوحدات";
            this.Load += new System.EventHandler(this.frm_units_Load);
            this.Shown += new System.EventHandler(this.frm_units_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbo_units;
        private System.Windows.Forms.TextBox txt_id_units;
        private System.Windows.Forms.TextBox txt_name_units;
        private System.Windows.Forms.TextBox txt_details_units;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_save_unit;
        private System.Windows.Forms.Button btn_delete_units;
        private System.Windows.Forms.Button btn_edits_units;
        private System.Windows.Forms.Button btn_new_units;
        private System.Windows.Forms.Label lbl_msg_units;
        private System.Windows.Forms.Label label4;
    }
}