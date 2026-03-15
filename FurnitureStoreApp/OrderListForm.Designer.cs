using System.Windows.Forms;
using System.Drawing;
namespace FurnitureStoreApp
{
    partial class OrdersListForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewOrders;
        private System.Windows.Forms.TextBox txtSearchOrderNumber;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkDateRange;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.Button btnViewOrder;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnMenu;
        // Pagination controls
        private System.Windows.Forms.Label       lblPageInfo;
        private System.Windows.Forms.Label       lblLegend;
        private System.Windows.Forms.Button      btnPrevPage;
        private System.Windows.Forms.Button      btnNextPage;
        private System.Windows.Forms.FlowLayoutPanel pnlPages;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.txtSearchOrderNumber = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.chkDateRange = new System.Windows.Forms.CheckBox();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.lblSort = new System.Windows.Forms.Label();
            this.btnViewOrder = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.lblLegend = new System.Windows.Forms.Label();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.pnlPages = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.AllowUserToAddRows = false;
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Location = new System.Drawing.Point(12, 193);
            this.dataGridViewOrders.MultiSelect = false;
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.ReadOnly = true;
            this.dataGridViewOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOrders.Size = new System.Drawing.Size(1136, 360);
            this.dataGridViewOrders.TabIndex = 0;
            // 
            // txtSearchOrderNumber
            // 
            this.txtSearchOrderNumber.Location = new System.Drawing.Point(94, 112);
            this.txtSearchOrderNumber.Name = "txtSearchOrderNumber";
            this.txtSearchOrderNumber.Size = new System.Drawing.Size(150, 20);
            this.txtSearchOrderNumber.TabIndex = 13;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(6, 115);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(83, 13);
            this.lblSearch.TabIndex = 14;
            this.lblSearch.Text = "Номер заказа:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Location = new System.Drawing.Point(294, 112);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(150, 21);
            this.cmbStatus.TabIndex = 11;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(244, 115);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 13);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Статус:";
            // 
            // chkDateRange
            // 
            this.chkDateRange.AutoSize = true;
            this.chkDateRange.Location = new System.Drawing.Point(459, 116);
            this.chkDateRange.Name = "chkDateRange";
            this.chkDateRange.Size = new System.Drawing.Size(97, 17);
            this.chkDateRange.TabIndex = 10;
            this.chkDateRange.Text = "Диапазон дат";
            // 
            // dtpStart
            // 
            this.dtpStart.Enabled = false;
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(578, 99);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(200, 20);
            this.dtpStart.TabIndex = 7;
            this.dtpStart.Value = new System.DateTime(2026, 1, 24, 8, 54, 55, 294);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Enabled = false;
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(578, 125);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(200, 20);
            this.dtpEnd.TabIndex = 6;
            this.dtpEnd.Value = new System.DateTime(2026, 2, 24, 8, 54, 55, 295);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Location = new System.Drawing.Point(556, 99);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(16, 13);
            this.lblStart.TabIndex = 9;
            this.lblStart.Text = "с:";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(550, 125);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(22, 13);
            this.lblEnd.TabIndex = 8;
            this.lblEnd.Text = "по:";
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(834, 113);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 5;
            this.btnReset.Text = "Сброс";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // cmbSort
            // 
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.Items.AddRange(new object[] {
            "По возрастанию стоимости",
            "По убыванию стоимости"});
            this.cmbSort.Location = new System.Drawing.Point(1001, 115);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(150, 21);
            this.cmbSort.TabIndex = 3;
            this.cmbSort.SelectedIndexChanged += new System.EventHandler(this.cmbSort_SelectedIndexChanged);
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.Location = new System.Drawing.Point(925, 115);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(70, 13);
            this.lblSort.TabIndex = 4;
            this.lblSort.Text = "Сортировка:";
            // 
            // btnViewOrder
            // 
            this.btnViewOrder.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnViewOrder.ForeColor = System.Drawing.Color.White;
            this.btnViewOrder.Location = new System.Drawing.Point(992, 625);
            this.btnViewOrder.Name = "btnViewOrder";
            this.btnViewOrder.Size = new System.Drawing.Size(156, 46);
            this.btnViewOrder.TabIndex = 2;
            this.btnViewOrder.Text = "Просмотр заказа";
            this.btnViewOrder.UseVisualStyleBackColor = false;
            this.btnViewOrder.Click += new System.EventHandler(this.btnViewOrder_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Location = new System.Drawing.Point(992, 573);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(156, 46);
            this.btnExportExcel.TabIndex = 1;
            this.btnExportExcel.Text = "Отчёт за период";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnMenu.ForeColor = System.Drawing.Color.White;
            this.btnMenu.Location = new System.Drawing.Point(9, 656);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(100, 30);
            this.btnMenu.TabIndex = 0;
            this.btnMenu.Text = "← Меню";
            this.btnMenu.UseVisualStyleBackColor = false;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPageInfo.ForeColor = System.Drawing.Color.DimGray;
            this.lblPageInfo.Location = new System.Drawing.Point(12, 560);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(131, 16);
            this.lblPageInfo.TabIndex = 22;
            this.lblPageInfo.Text = "Показано: 0 из 0";
            // 
            // lblLegend
            // 
            this.lblLegend.AutoSize = true;
            this.lblLegend.Cursor = System.Windows.Forms.Cursors.Help;
            this.lblLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.lblLegend.Location = new System.Drawing.Point(12, 625);
            this.lblLegend.Name = "lblLegend";
            this.lblLegend.Size = new System.Drawing.Size(436, 15);
            this.lblLegend.TabIndex = 17;
            this.lblLegend.Text = "Цвета:      Принят       В пути              Доставлен           Завершён      От" +
    "менен";
            this.lblLegend.Click += new System.EventHandler(this.lblLegend_Click);
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrevPage.Location = new System.Drawing.Point(12, 582);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(36, 28);
            this.btnPrevPage.TabIndex = 20;
            this.btnPrevPage.Text = "◄";
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnNextPage.Location = new System.Drawing.Point(54, 582);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(36, 28);
            this.btnNextPage.TabIndex = 21;
            this.btnNextPage.Text = "►";
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // pnlPages
            // 
            this.pnlPages.Location = new System.Drawing.Point(96, 582);
            this.pnlPages.Name = "pnlPages";
            this.pnlPages.Size = new System.Drawing.Size(800, 32);
            this.pnlPages.TabIndex = 18;
            this.pnlPages.WrapContents = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pictureBox2.Location = new System.Drawing.Point(82, -7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1088, 100);
            this.pictureBox2.TabIndex = 16;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pictureBox1.BackgroundImage = global::FurnitureStoreApp.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-4, -7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(95, 100);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // OrdersListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 698);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.btnViewOrder);
            this.Controls.Add(this.cmbSort);
            this.Controls.Add(this.lblSort);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.chkDateRange);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtSearchOrderNumber);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.lblLegend);
            this.Controls.Add(this.pnlPages);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnPrevPage);
            this.Controls.Add(this.lblPageInfo);
            this.Controls.Add(this.dataGridViewOrders);
            this.Name = "OrdersListForm";
            this.Text = "Учёт заказов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OrdersListForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
    }
}