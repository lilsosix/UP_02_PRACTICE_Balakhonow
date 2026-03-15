
namespace FurnitureStoreApp
{
    partial class UserCatalogForm
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
            this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbPriceSort = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnViewCart = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cmbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.btnClearFilters = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewProducts
            // 
            this.dataGridViewProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProducts.Location = new System.Drawing.Point(6, 163);
            this.dataGridViewProducts.Name = "dataGridViewProducts";
            this.dataGridViewProducts.Size = new System.Drawing.Size(776, 369);
            this.dataGridViewProducts.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(6, 126);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(130, 123);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "button1";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // cmbPriceSort
            // 
            this.cmbPriceSort.FormattingEnabled = true;
            this.cmbPriceSort.Location = new System.Drawing.Point(555, 123);
            this.cmbPriceSort.Name = "cmbPriceSort";
            this.cmbPriceSort.Size = new System.Drawing.Size(121, 21);
            this.cmbPriceSort.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(682, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "ASC";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(734, 121);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "DESC";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.Location = new System.Drawing.Point(268, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(221, 31);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Каталог товаров";
            // 
            // btnViewCart
            // 
            this.btnViewCart.Location = new System.Drawing.Point(672, 538);
            this.btnViewCart.Name = "btnViewCart";
            this.btnViewCart.Size = new System.Drawing.Size(116, 29);
            this.btnViewCart.TabIndex = 7;
            this.btnViewCart.Text = "корзина";
            this.btnViewCart.UseVisualStyleBackColor = true;
            this.btnViewCart.Click += new System.EventHandler(this.btnCart_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 541);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "назад";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // cmbCategoryFilter
            // 
            this.cmbCategoryFilter.FormattingEnabled = true;
            this.cmbCategoryFilter.Location = new System.Drawing.Point(322, 99);
            this.cmbCategoryFilter.Name = "cmbCategoryFilter";
            this.cmbCategoryFilter.Size = new System.Drawing.Size(121, 21);
            this.cmbCategoryFilter.TabIndex = 9;
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Location = new System.Drawing.Point(611, 36);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(75, 64);
            this.btnClearFilters.TabIndex = 10;
            this.btnClearFilters.Text = "сбросить фильтры";
            this.btnClearFilters.UseVisualStyleBackColor = true;
            // 
            // UserCatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 576);
            this.Controls.Add(this.btnClearFilters);
            this.Controls.Add(this.cmbCategoryFilter);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnViewCart);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbPriceSort);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dataGridViewProducts);
            this.Name = "UserCatalogForm";
            this.Text = "Form5";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbPriceSort;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnViewCart;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cmbCategoryFilter;
        private System.Windows.Forms.Button btnClearFilters;
    }
}