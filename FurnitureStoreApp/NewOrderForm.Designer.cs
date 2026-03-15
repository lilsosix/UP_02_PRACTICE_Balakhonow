using System;
using System.Windows.Forms;
using System.Drawing;
namespace FurnitureStoreApp
{
    partial class NewOrderForm
    {
        private System.ComponentModel.IContainer components = null;

        // Элементы управления
        private System.Windows.Forms.Label lblOrderDateTitle;
        private System.Windows.Forms.Label lblOrderDate;
        private System.Windows.Forms.Label lblCompletionDate;
        private System.Windows.Forms.DateTimePicker dtpCompletionDate;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.Label lblSelectedClient;
        private System.Windows.Forms.Button btnSelectClient;

        private System.Windows.Forms.GroupBox gbProducts;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.DataGridView dataGridViewProducts;
        private System.Windows.Forms.Button btnAddToCart;

        private System.Windows.Forms.GroupBox gbCart;
        private System.Windows.Forms.DataGridView dataGridViewCart;
        private System.Windows.Forms.Button btnRemoveFromCart;
        private System.Windows.Forms.Label lblTotalAmount;

        private System.Windows.Forms.Button btnSaveOrder;
        private System.Windows.Forms.Button btnClients;
        private System.Windows.Forms.Button btnMenu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblOrderDateTitle = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblCompletionDate = new System.Windows.Forms.Label();
            this.dtpCompletionDate = new System.Windows.Forms.DateTimePicker();
            this.lblClient = new System.Windows.Forms.Label();
            this.lblSelectedClient = new System.Windows.Forms.Label();
            this.btnSelectClient = new System.Windows.Forms.Button();
            this.gbProducts = new System.Windows.Forms.GroupBox();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.dataGridViewProducts = new System.Windows.Forms.DataGridView();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.gbCart = new System.Windows.Forms.GroupBox();
            this.dataGridViewCart = new System.Windows.Forms.DataGridView();
            this.btnRemoveFromCart = new System.Windows.Forms.Button();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.btnSaveOrder = new System.Windows.Forms.Button();
            this.btnClients = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.cmbDelivery = new System.Windows.Forms.ComboBox();
            this.lblDelivery = new System.Windows.Forms.Label();
            this.txtPrepayment = new System.Windows.Forms.TextBox();
            this.lblPrepayment = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).BeginInit();
            this.gbCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOrderDateTitle
            // 
            this.lblOrderDateTitle.AutoSize = true;
            this.lblOrderDateTitle.Location = new System.Drawing.Point(13, 109);
            this.lblOrderDateTitle.Name = "lblOrderDateTitle";
            this.lblOrderDateTitle.Size = new System.Drawing.Size(87, 13);
            this.lblOrderDateTitle.TabIndex = 11;
            this.lblOrderDateTitle.Text = "Дата создания:";
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Location = new System.Drawing.Point(111, 109);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(61, 13);
            this.lblOrderDate.TabIndex = 10;
            this.lblOrderDate.Text = "01.01.2025";
            // 
            // lblCompletionDate
            // 
            this.lblCompletionDate.AutoSize = true;
            this.lblCompletionDate.Location = new System.Drawing.Point(13, 139);
            this.lblCompletionDate.Name = "lblCompletionDate";
            this.lblCompletionDate.Size = new System.Drawing.Size(101, 13);
            this.lblCompletionDate.TabIndex = 9;
            this.lblCompletionDate.Text = "Дата выполнения:";
            // 
            // dtpCompletionDate
            // 
            this.dtpCompletionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCompletionDate.Location = new System.Drawing.Point(111, 136);
            this.dtpCompletionDate.Name = "dtpCompletionDate";
            this.dtpCompletionDate.Size = new System.Drawing.Size(100, 20);
            this.dtpCompletionDate.TabIndex = 8;
            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Location = new System.Drawing.Point(13, 169);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(46, 13);
            this.lblClient.TabIndex = 7;
            this.lblClient.Text = "Клиент:";
            // 
            // lblSelectedClient
            // 
            this.lblSelectedClient.AutoSize = true;
            this.lblSelectedClient.Location = new System.Drawing.Point(181, 169);
            this.lblSelectedClient.Name = "lblSelectedClient";
            this.lblSelectedClient.Size = new System.Drawing.Size(60, 13);
            this.lblSelectedClient.TabIndex = 6;
            this.lblSelectedClient.Text = "не выбран";
            // 
            // btnSelectClient
            // 
            this.btnSelectClient.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSelectClient.ForeColor = System.Drawing.Color.White;
            this.btnSelectClient.Location = new System.Drawing.Point(65, 163);
            this.btnSelectClient.Name = "btnSelectClient";
            this.btnSelectClient.Size = new System.Drawing.Size(100, 25);
            this.btnSelectClient.TabIndex = 5;
            this.btnSelectClient.Text = "Выбрать клиента";
            this.btnSelectClient.UseVisualStyleBackColor = false;
            this.btnSelectClient.Click += new System.EventHandler(this.btnSelectClient_Click);
            // 
            // gbProducts
            // 
            this.gbProducts.Controls.Add(this.btnAddToCart);
            this.gbProducts.Controls.Add(this.dataGridViewProducts);
            this.gbProducts.Controls.Add(this.cmbCategory);
            this.gbProducts.Controls.Add(this.lblCategory);
            this.gbProducts.Controls.Add(this.txtSearch);
            this.gbProducts.Controls.Add(this.lblSearch);
            this.gbProducts.Location = new System.Drawing.Point(13, 204);
            this.gbProducts.Name = "gbProducts";
            this.gbProducts.Size = new System.Drawing.Size(560, 300);
            this.gbProducts.TabIndex = 4;
            this.gbProducts.TabStop = false;
            this.gbProducts.Text = "Товары";
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Location = new System.Drawing.Point(10, 260);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(120, 30);
            this.btnAddToCart.TabIndex = 0;
            this.btnAddToCart.Text = "Добавить в корзину";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // dataGridViewProducts
            // 
            this.dataGridViewProducts.AllowUserToAddRows = false;
            this.dataGridViewProducts.Location = new System.Drawing.Point(10, 50);
            this.dataGridViewProducts.Name = "dataGridViewProducts";
            this.dataGridViewProducts.ReadOnly = true;
            this.dataGridViewProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProducts.Size = new System.Drawing.Size(540, 200);
            this.dataGridViewProducts.TabIndex = 1;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Location = new System.Drawing.Point(290, 22);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(150, 21);
            this.cmbCategory.TabIndex = 2;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(220, 25);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(63, 13);
            this.lblCategory.TabIndex = 3;
            this.lblCategory.Text = "Категория:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(60, 22);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(150, 20);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(10, 25);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(42, 13);
            this.lblSearch.TabIndex = 5;
            this.lblSearch.Text = "Поиск:";
            // 
            // gbCart
            // 
            this.gbCart.Controls.Add(this.dataGridViewCart);
            this.gbCart.Controls.Add(this.btnRemoveFromCart);
            this.gbCart.Controls.Add(this.lblTotalAmount);
            this.gbCart.Location = new System.Drawing.Point(581, 204);
            this.gbCart.Name = "gbCart";
            this.gbCart.Size = new System.Drawing.Size(400, 300);
            this.gbCart.TabIndex = 3;
            this.gbCart.TabStop = false;
            this.gbCart.Text = "Корзина";
            // 
            // dataGridViewCart
            // 
            this.dataGridViewCart.AllowUserToAddRows = false;
            this.dataGridViewCart.Location = new System.Drawing.Point(10, 20);
            this.dataGridViewCart.Name = "dataGridViewCart";
            this.dataGridViewCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCart.Size = new System.Drawing.Size(380, 220);
            this.dataGridViewCart.TabIndex = 0;
            // 
            // btnRemoveFromCart
            // 
            this.btnRemoveFromCart.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnRemoveFromCart.ForeColor = System.Drawing.Color.White;
            this.btnRemoveFromCart.Location = new System.Drawing.Point(10, 250);
            this.btnRemoveFromCart.Name = "btnRemoveFromCart";
            this.btnRemoveFromCart.Size = new System.Drawing.Size(120, 30);
            this.btnRemoveFromCart.TabIndex = 1;
            this.btnRemoveFromCart.Text = "Удалить из корзины";
            this.btnRemoveFromCart.UseVisualStyleBackColor = false;
            this.btnRemoveFromCart.Click += new System.EventHandler(this.btnRemoveFromCart_Click);
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.Location = new System.Drawing.Point(150, 260);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(107, 17);
            this.lblTotalAmount.TabIndex = 2;
            this.lblTotalAmount.Text = "Итого: 0,00 ₽";
            // 
            // btnSaveOrder
            // 
            this.btnSaveOrder.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnSaveOrder.ForeColor = System.Drawing.Color.White;
            this.btnSaveOrder.Location = new System.Drawing.Point(842, 510);
            this.btnSaveOrder.Name = "btnSaveOrder";
            this.btnSaveOrder.Size = new System.Drawing.Size(129, 47);
            this.btnSaveOrder.TabIndex = 2;
            this.btnSaveOrder.Text = "Оформить заказ";
            this.btnSaveOrder.UseVisualStyleBackColor = false;
            this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
            // 
            // lblDelivery
            //
            this.lblDelivery.AutoSize = true;
            this.lblDelivery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblDelivery.Location = new System.Drawing.Point(400, 470);
            this.lblDelivery.Name = "lblDelivery";
            this.lblDelivery.Text = "Доставка:";
            //
            // cmbDelivery
            //
            this.cmbDelivery.Location = new System.Drawing.Point(470, 467);
            this.cmbDelivery.Name = "cmbDelivery";
            this.cmbDelivery.Size = new System.Drawing.Size(200, 21);
            this.cmbDelivery.TabIndex = 20;
            //
            // lblPrepayment
            //
            this.lblPrepayment.AutoSize = true;
            this.lblPrepayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblPrepayment.Location = new System.Drawing.Point(400, 500);
            this.lblPrepayment.Name = "lblPrepayment";
            this.lblPrepayment.Text = "Предоплата:";
            //
            // txtPrepayment
            //
            this.txtPrepayment.Location = new System.Drawing.Point(490, 498);
            this.txtPrepayment.Name = "txtPrepayment";
            this.txtPrepayment.Size = new System.Drawing.Size(120, 20);
            this.txtPrepayment.TabIndex = 21;
            this.txtPrepayment.Text = "0";

            // 
            // btnClients
            // 
            this.btnClients.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnClients.ForeColor = System.Drawing.Color.White;
            this.btnClients.Location = new System.Drawing.Point(141, 527);
            this.btnClients.Name = "btnClients";
            this.btnClients.Size = new System.Drawing.Size(100, 30);
            this.btnClients.TabIndex = 1;
            this.btnClients.Text = "Клиенты";
            this.btnClients.UseVisualStyleBackColor = false;
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Location = new System.Drawing.Point(12, 527);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(100, 30);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.Text = "← Меню";
            this.btnMenu.UseVisualStyleBackColor = false;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pictureBox2.Location = new System.Drawing.Point(87, -14);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(918, 105);
            this.pictureBox2.TabIndex = 13;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pictureBox1.BackgroundImage = global::FurnitureStoreApp.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-4, -4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(95, 95);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // NewOrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 569);
            this.Controls.Add(this.cmbDelivery);
            this.Controls.Add(this.lblDelivery);
            this.Controls.Add(this.txtPrepayment);
            this.Controls.Add(this.lblPrepayment);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.btnClients);
            this.Controls.Add(this.btnSaveOrder);
            this.Controls.Add(this.gbCart);
            this.Controls.Add(this.gbProducts);
            this.Controls.Add(this.btnSelectClient);
            this.Controls.Add(this.lblSelectedClient);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.dtpCompletionDate);
            this.Controls.Add(this.lblCompletionDate);
            this.Controls.Add(this.lblOrderDate);
            this.Controls.Add(this.lblOrderDateTitle);
            this.Name = "NewOrderForm";
            this.Text = "Новый заказ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewOrderForm_FormClosing);
            this.gbProducts.ResumeLayout(false);
            this.gbProducts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProducts)).EndInit();
            this.gbCart.ResumeLayout(false);
            this.gbCart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbDelivery;
        private System.Windows.Forms.Label lblDelivery;
        private System.Windows.Forms.TextBox txtPrepayment;
        private System.Windows.Forms.Label lblPrepayment;
    }
}