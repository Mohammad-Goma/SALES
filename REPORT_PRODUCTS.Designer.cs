namespace SALES
{
    partial class REPORT_PRODUCTS
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
            this.btn_Search = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.BRANCH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STORE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SECTIONS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PARCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCT_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UNITS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INITIAL_COST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LESS_PRICE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TAXING = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_search = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Prices = new System.Windows.Forms.TextBox();
            this.QTY = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Search
            // 
            this.btn_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Search.Location = new System.Drawing.Point(280, 32);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(127, 38);
            this.btn_Search.TabIndex = 1;
            this.btn_Search.Text = "بحث";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.DGV);
            this.panel1.Location = new System.Drawing.Point(3, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1365, 591);
            this.panel1.TabIndex = 2;
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.BackgroundColor = System.Drawing.Color.CornflowerBlue;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BRANCH,
            this.STORE,
            this.SECTIONS,
            this.PARCODE,
            this.PRODUCT_NAME,
            this.UNITS,
            this.INITIAL_COST,
            this.PRICE,
            this.LESS_PRICE,
            this.TAXING,
            this.Quantity});
            this.DGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV.Location = new System.Drawing.Point(0, 0);
            this.DGV.Name = "DGV";
            this.DGV.ReadOnly = true;
            this.DGV.Size = new System.Drawing.Size(1365, 591);
            this.DGV.TabIndex = 0;
            // 
            // BRANCH
            // 
            this.BRANCH.DataPropertyName = "BRANCH";
            this.BRANCH.HeaderText = "الفرع";
            this.BRANCH.Name = "BRANCH";
            this.BRANCH.ReadOnly = true;
            // 
            // STORE
            // 
            this.STORE.DataPropertyName = "STORE";
            this.STORE.HeaderText = "المخزن";
            this.STORE.Name = "STORE";
            this.STORE.ReadOnly = true;
            // 
            // SECTIONS
            // 
            this.SECTIONS.DataPropertyName = "SECTIONS";
            this.SECTIONS.HeaderText = "القسم";
            this.SECTIONS.Name = "SECTIONS";
            this.SECTIONS.ReadOnly = true;
            // 
            // PARCODE
            // 
            this.PARCODE.DataPropertyName = "PARCODE";
            this.PARCODE.HeaderText = "الباركود";
            this.PARCODE.Name = "PARCODE";
            this.PARCODE.ReadOnly = true;
            this.PARCODE.Width = 160;
            // 
            // PRODUCT_NAME
            // 
            this.PRODUCT_NAME.DataPropertyName = "PRODUCT_NAME";
            this.PRODUCT_NAME.HeaderText = "أسم المنتج";
            this.PRODUCT_NAME.Name = "PRODUCT_NAME";
            this.PRODUCT_NAME.ReadOnly = true;
            this.PRODUCT_NAME.Width = 160;
            // 
            // UNITS
            // 
            this.UNITS.DataPropertyName = "UNITS";
            this.UNITS.HeaderText = "الوحدة";
            this.UNITS.Name = "UNITS";
            this.UNITS.ReadOnly = true;
            // 
            // INITIAL_COST
            // 
            this.INITIAL_COST.DataPropertyName = "INITIAL_COST";
            this.INITIAL_COST.HeaderText = "التكلفة الأولية";
            this.INITIAL_COST.Name = "INITIAL_COST";
            this.INITIAL_COST.ReadOnly = true;
            this.INITIAL_COST.Width = 130;
            // 
            // PRICE
            // 
            this.PRICE.DataPropertyName = "PRICE";
            this.PRICE.HeaderText = "السعر";
            this.PRICE.Name = "PRICE";
            this.PRICE.ReadOnly = true;
            // 
            // LESS_PRICE
            // 
            this.LESS_PRICE.DataPropertyName = "LESS_PRICE";
            this.LESS_PRICE.HeaderText = "أقل سعر";
            this.LESS_PRICE.Name = "LESS_PRICE";
            this.LESS_PRICE.ReadOnly = true;
            // 
            // TAXING
            // 
            this.TAXING.DataPropertyName = "TAXING";
            this.TAXING.HeaderText = "الضريبة";
            this.TAXING.Name = "TAXING";
            this.TAXING.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "الكمية";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // txt_search
            // 
            this.txt_search.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txt_search.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txt_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_search.Location = new System.Drawing.Point(12, 32);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(245, 38);
            this.txt_search.TabIndex = 3;
            this.txt_search.TextChanged += new System.EventHandler(this.txt_search_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "اكتب اسم القسم هنا";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.Prices);
            this.panel2.Controls.Add(this.QTY);
            this.panel2.Location = new System.Drawing.Point(3, 677);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1365, 72);
            this.panel2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(861, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 29);
            this.label3.TabIndex = 3;
            this.label3.Text = "الكمية المتوفرة";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1204, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 29);
            this.label2.TabIndex = 2;
            this.label2.Text = "إجمالي السعر";
            // 
            // Prices
            // 
            this.Prices.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Prices.Location = new System.Drawing.Point(1011, 18);
            this.Prices.Name = "Prices";
            this.Prices.Size = new System.Drawing.Size(178, 33);
            this.Prices.TabIndex = 1;
            // 
            // QTY
            // 
            this.QTY.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QTY.Location = new System.Drawing.Point(658, 18);
            this.QTY.Name = "QTY";
            this.QTY.Size = new System.Drawing.Size(188, 33);
            this.QTY.TabIndex = 0;
            // 
            // REPORT_PRODUCTS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_search);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Search);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "REPORT_PRODUCTS";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "تقرير الأقسام";
            this.Load += new System.EventHandler(this.PRODUCTS_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn BRANCH;
        private System.Windows.Forms.DataGridViewTextBoxColumn STORE;
        private System.Windows.Forms.DataGridViewTextBoxColumn SECTIONS;
        private System.Windows.Forms.DataGridViewTextBoxColumn PARCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCT_NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn UNITS;
        private System.Windows.Forms.DataGridViewTextBoxColumn INITIAL_COST;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn LESS_PRICE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TAXING;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Prices;
        private System.Windows.Forms.TextBox QTY;
    }
}