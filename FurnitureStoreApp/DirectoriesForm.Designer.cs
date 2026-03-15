using System.Windows.Forms;
using System.Drawing;
namespace FurnitureStoreApp
{
    partial class DirectoriesForm
    {
        private System.ComponentModel.IContainer components = null;

        // Шапка и кнопка возврата
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnMenu;

        // Общие элементы
        private TabControl tabControl;
        private TabPage tabRoles;
        private TabPage tabCategories;
        private TabPage tabMaterials;
        private TabPage tabStatuses;
        private TabPage tabDeliveryMethods;

        // Для вкладки "Роли"
        private DataGridView dgvRoles;
        private Button btnAddRole;
        private Button btnEditRole;
        private Button btnDeleteRole;

        // Для вкладки "Категории"
        private DataGridView dgvCategories;
        private Button btnAddCategory;
        private Button btnEditCategory;
        private Button btnDeleteCategory;

        // Для вкладки "Материалы"
        private DataGridView dgvMaterials;
        private Button btnAddMaterial;
        private Button btnEditMaterial;
        private Button btnDeleteMaterial;

        // Для вкладки "Статусы"
        private DataGridView dgvStatuses;
        private Button btnAddStatus;
        private Button btnEditStatus;
        private Button btnDeleteStatus;

        // Для вкладки "Способы доставки"
        private DataGridView dgvDeliveryMethods;
        private Button btnAddDelivery;
        private Button btnEditDelivery;
        private Button btnDeleteDelivery;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader    = new System.Windows.Forms.Panel();
            this.pictureBox1  = new System.Windows.Forms.PictureBox();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.lblTitle     = new System.Windows.Forms.Label();
            this.btnMenu      = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabRoles = new System.Windows.Forms.TabPage();
            this.dgvRoles = new System.Windows.Forms.DataGridView();
            this.btnAddRole = new System.Windows.Forms.Button();
            this.btnEditRole = new System.Windows.Forms.Button();
            this.btnDeleteRole = new System.Windows.Forms.Button();
            this.tabCategories = new System.Windows.Forms.TabPage();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.btnEditCategory = new System.Windows.Forms.Button();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.tabMaterials = new System.Windows.Forms.TabPage();
            this.dgvMaterials = new System.Windows.Forms.DataGridView();
            this.btnAddMaterial = new System.Windows.Forms.Button();
            this.btnEditMaterial = new System.Windows.Forms.Button();
            this.btnDeleteMaterial = new System.Windows.Forms.Button();
            this.tabStatuses = new System.Windows.Forms.TabPage();
            this.dgvStatuses = new System.Windows.Forms.DataGridView();
            this.btnAddStatus = new System.Windows.Forms.Button();
            this.btnEditStatus = new System.Windows.Forms.Button();
            this.btnDeleteStatus = new System.Windows.Forms.Button();
            this.tabDeliveryMethods = new System.Windows.Forms.TabPage();
            this.dgvDeliveryMethods = new System.Windows.Forms.DataGridView();
            this.btnAddDelivery = new System.Windows.Forms.Button();
            this.btnEditDelivery = new System.Windows.Forms.Button();
            this.btnDeleteDelivery = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabRoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).BeginInit();
            this.tabCategories.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.tabMaterials.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).BeginInit();
            this.tabStatuses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatuses)).BeginInit();
            this.tabDeliveryMethods.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryMethods)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl.Controls.Add(this.tabRoles);
            this.tabControl.Controls.Add(this.tabCategories);
            this.tabControl.Controls.Add(this.tabMaterials);
            this.tabControl.Controls.Add(this.tabStatuses);
            this.tabControl.Controls.Add(this.tabDeliveryMethods);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.None;
            this.tabControl.Location = new System.Drawing.Point(0, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(686, 390);
            this.tabControl.TabIndex = 0;
            // 
            // tabRoles
            // 
            this.tabRoles.Controls.Add(this.dgvRoles);
            this.tabRoles.Controls.Add(this.btnAddRole);
            this.tabRoles.Controls.Add(this.btnEditRole);
            this.tabRoles.Controls.Add(this.btnDeleteRole);
            this.tabRoles.Location = new System.Drawing.Point(4, 25);
            this.tabRoles.Name = "tabRoles";
            this.tabRoles.Size = new System.Drawing.Size(678, 361);
            this.tabRoles.TabIndex = 0;
            this.tabRoles.Text = "Роли";
            // 
            // dgvRoles
            // 
            this.dgvRoles.AllowUserToAddRows = false;
            this.dgvRoles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoles.Location = new System.Drawing.Point(9, 9);
            this.dgvRoles.Name = "dgvRoles";
            this.dgvRoles.ReadOnly = true;
            this.dgvRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoles.Size = new System.Drawing.Size(643, 277);
            this.dgvRoles.TabIndex = 0;
            // 
            // btnAddRole
            // 
            this.btnAddRole.Location = new System.Drawing.Point(9, 295);
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Size = new System.Drawing.Size(86, 26);
            this.btnAddRole.TabIndex = 1;
            this.btnAddRole.Text = "Добавить";
            // 
            // btnEditRole
            // 
            this.btnEditRole.Location = new System.Drawing.Point(103, 295);
            this.btnEditRole.Name = "btnEditRole";
            this.btnEditRole.Size = new System.Drawing.Size(86, 26);
            this.btnEditRole.TabIndex = 2;
            this.btnEditRole.Text = "Изменить";
            // 
            // btnDeleteRole
            // 
            this.btnDeleteRole.Location = new System.Drawing.Point(197, 295);
            this.btnDeleteRole.Name = "btnDeleteRole";
            this.btnDeleteRole.Size = new System.Drawing.Size(86, 26);
            this.btnDeleteRole.TabIndex = 3;
            this.btnDeleteRole.Text = "Удалить";
            // 
            // tabCategories
            // 
            this.tabCategories.Controls.Add(this.dgvCategories);
            this.tabCategories.Controls.Add(this.btnAddCategory);
            this.tabCategories.Controls.Add(this.btnEditCategory);
            this.tabCategories.Controls.Add(this.btnDeleteCategory);
            this.tabCategories.Location = new System.Drawing.Point(4, 25);
            this.tabCategories.Name = "tabCategories";
            this.tabCategories.Size = new System.Drawing.Size(678, 361);
            this.tabCategories.TabIndex = 1;
            this.tabCategories.Text = "Категории";
            // 
            // dgvCategories
            // 
            this.dgvCategories.AllowUserToAddRows = false;
            this.dgvCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategories.Location = new System.Drawing.Point(9, 9);
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.Size = new System.Drawing.Size(643, 277);
            this.dgvCategories.TabIndex = 0;
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Location = new System.Drawing.Point(9, 295);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(86, 26);
            this.btnAddCategory.TabIndex = 1;
            this.btnAddCategory.Text = "Добавить";
            // 
            // btnEditCategory
            // 
            this.btnEditCategory.Location = new System.Drawing.Point(103, 295);
            this.btnEditCategory.Name = "btnEditCategory";
            this.btnEditCategory.Size = new System.Drawing.Size(86, 26);
            this.btnEditCategory.TabIndex = 2;
            this.btnEditCategory.Text = "Изменить";
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.Location = new System.Drawing.Point(197, 295);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(86, 26);
            this.btnDeleteCategory.TabIndex = 3;
            this.btnDeleteCategory.Text = "Удалить";
            // 
            // tabMaterials
            // 
            this.tabMaterials.Controls.Add(this.dgvMaterials);
            this.tabMaterials.Controls.Add(this.btnAddMaterial);
            this.tabMaterials.Controls.Add(this.btnEditMaterial);
            this.tabMaterials.Controls.Add(this.btnDeleteMaterial);
            this.tabMaterials.Location = new System.Drawing.Point(4, 25);
            this.tabMaterials.Name = "tabMaterials";
            this.tabMaterials.Size = new System.Drawing.Size(678, 361);
            this.tabMaterials.TabIndex = 2;
            this.tabMaterials.Text = "Материалы";
            // 
            // dgvMaterials
            // 
            this.dgvMaterials.AllowUserToAddRows = false;
            this.dgvMaterials.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaterials.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterials.Location = new System.Drawing.Point(9, 9);
            this.dgvMaterials.Name = "dgvMaterials";
            this.dgvMaterials.ReadOnly = true;
            this.dgvMaterials.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaterials.Size = new System.Drawing.Size(643, 277);
            this.dgvMaterials.TabIndex = 0;
            // 
            // btnAddMaterial
            // 
            this.btnAddMaterial.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnAddMaterial.ForeColor = System.Drawing.Color.White;
            this.btnAddMaterial.Location = new System.Drawing.Point(9, 295);
            this.btnAddMaterial.Name = "btnAddMaterial";
            this.btnAddMaterial.Size = new System.Drawing.Size(86, 26);
            this.btnAddMaterial.TabIndex = 1;
            this.btnAddMaterial.Text = "Добавить";
            this.btnAddMaterial.UseVisualStyleBackColor = false;
            // 
            // btnEditMaterial
            // 
            this.btnEditMaterial.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnEditMaterial.ForeColor = System.Drawing.Color.White;
            this.btnEditMaterial.Location = new System.Drawing.Point(103, 295);
            this.btnEditMaterial.Name = "btnEditMaterial";
            this.btnEditMaterial.Size = new System.Drawing.Size(86, 26);
            this.btnEditMaterial.TabIndex = 2;
            this.btnEditMaterial.Text = "Изменить";
            this.btnEditMaterial.UseVisualStyleBackColor = false;
            // 
            // btnDeleteMaterial
            // 
            this.btnDeleteMaterial.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnDeleteMaterial.ForeColor = System.Drawing.Color.White;
            this.btnDeleteMaterial.Location = new System.Drawing.Point(197, 295);
            this.btnDeleteMaterial.Name = "btnDeleteMaterial";
            this.btnDeleteMaterial.Size = new System.Drawing.Size(86, 26);
            this.btnDeleteMaterial.TabIndex = 3;
            this.btnDeleteMaterial.Text = "Удалить";
            this.btnDeleteMaterial.UseVisualStyleBackColor = false;
            // 
            // tabStatuses
            // 
            this.tabStatuses.Controls.Add(this.dgvStatuses);
            this.tabStatuses.Controls.Add(this.btnAddStatus);
            this.tabStatuses.Controls.Add(this.btnEditStatus);
            this.tabStatuses.Controls.Add(this.btnDeleteStatus);
            this.tabStatuses.Location = new System.Drawing.Point(4, 25);
            this.tabStatuses.Name = "tabStatuses";
            this.tabStatuses.Size = new System.Drawing.Size(678, 361);
            this.tabStatuses.TabIndex = 3;
            this.tabStatuses.Text = "Статусы";
            // 
            // dgvStatuses
            // 
            this.dgvStatuses.AllowUserToAddRows = false;
            this.dgvStatuses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStatuses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatuses.Location = new System.Drawing.Point(9, 9);
            this.dgvStatuses.Name = "dgvStatuses";
            this.dgvStatuses.ReadOnly = true;
            this.dgvStatuses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStatuses.Size = new System.Drawing.Size(643, 277);
            this.dgvStatuses.TabIndex = 0;
            // 
            // btnAddStatus
            // 
            this.btnAddStatus.Location = new System.Drawing.Point(9, 295);
            this.btnAddStatus.Name = "btnAddStatus";
            this.btnAddStatus.Size = new System.Drawing.Size(86, 26);
            this.btnAddStatus.TabIndex = 1;
            this.btnAddStatus.Text = "Добавить";
            // 
            // btnEditStatus
            // 
            this.btnEditStatus.Location = new System.Drawing.Point(103, 295);
            this.btnEditStatus.Name = "btnEditStatus";
            this.btnEditStatus.Size = new System.Drawing.Size(86, 26);
            this.btnEditStatus.TabIndex = 2;
            this.btnEditStatus.Text = "Изменить";
            // 
            // btnDeleteStatus
            // 
            this.btnDeleteStatus.Location = new System.Drawing.Point(197, 295);
            this.btnDeleteStatus.Name = "btnDeleteStatus";
            this.btnDeleteStatus.Size = new System.Drawing.Size(86, 26);
            this.btnDeleteStatus.TabIndex = 3;
            this.btnDeleteStatus.Text = "Удалить";
            // 
            // tabDeliveryMethods
            // 
            this.tabDeliveryMethods.Controls.Add(this.dgvDeliveryMethods);
            this.tabDeliveryMethods.Controls.Add(this.btnAddDelivery);
            this.tabDeliveryMethods.Controls.Add(this.btnEditDelivery);
            this.tabDeliveryMethods.Controls.Add(this.btnDeleteDelivery);
            this.tabDeliveryMethods.Location = new System.Drawing.Point(4, 25);
            this.tabDeliveryMethods.Name = "tabDeliveryMethods";
            this.tabDeliveryMethods.Size = new System.Drawing.Size(678, 361);
            this.tabDeliveryMethods.TabIndex = 4;
            this.tabDeliveryMethods.Text = "Способы доставки";
            // 
            // dgvDeliveryMethods
            // 
            this.dgvDeliveryMethods.AllowUserToAddRows = false;
            this.dgvDeliveryMethods.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDeliveryMethods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDeliveryMethods.Location = new System.Drawing.Point(9, 9);
            this.dgvDeliveryMethods.Name = "dgvDeliveryMethods";
            this.dgvDeliveryMethods.ReadOnly = true;
            this.dgvDeliveryMethods.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeliveryMethods.Size = new System.Drawing.Size(643, 277);
            this.dgvDeliveryMethods.TabIndex = 0;
            // 
            // btnAddDelivery
            // 
            this.btnAddDelivery.Location = new System.Drawing.Point(9, 295);
            this.btnAddDelivery.Name = "btnAddDelivery";
            this.btnAddDelivery.Size = new System.Drawing.Size(86, 26);
            this.btnAddDelivery.TabIndex = 1;
            this.btnAddDelivery.Text = "Добавить";
            // 
            // btnEditDelivery
            // 
            this.btnEditDelivery.Location = new System.Drawing.Point(103, 295);
            this.btnEditDelivery.Name = "btnEditDelivery";
            this.btnEditDelivery.Size = new System.Drawing.Size(86, 26);
            this.btnEditDelivery.TabIndex = 2;
            this.btnEditDelivery.Text = "Изменить";
            // 
            // btnDeleteDelivery
            // 
            this.btnDeleteDelivery.Location = new System.Drawing.Point(197, 295);
            this.btnDeleteDelivery.Name = "btnDeleteDelivery";
            this.btnDeleteDelivery.Size = new System.Drawing.Size(86, 26);
            this.btnDeleteDelivery.TabIndex = 3;
            this.btnDeleteDelivery.Text = "Удалить";
            // 
            // DirectoriesForm
            // 
            // pnlHeader — оранжевая шапка
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(686, 58);
            this.pnlHeader.TabStop = false;

            // pictureBox1 — логотип
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(255, 192, 128);
            this.pictureBox1.BackgroundImage = global::FurnitureStoreApp.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(-3, -2);
            this.pictureBox1.Size = new System.Drawing.Size(79, 79);
            this.pictureBox1.TabStop = false;

            // lblTitle
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(85, 8);
            this.lblTitle.Size = new System.Drawing.Size(500, 42);
            this.lblTitle.Text = "Управление справочниками";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.pnlHeader.Controls.Add(this.pictureBox1);
            this.pnlHeader.Controls.Add(this.lblTitle);

            // btnMenu
            this.btnMenu.Location = new System.Drawing.Point(12, 458);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(110, 30);
            this.btnMenu.TabIndex = 99;
            this.btnMenu.Text = "← Меню";
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 498);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.btnMenu);
            this.Controls.Add(this.pnlHeader);
            this.Name = "DirectoriesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Управление справочниками";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabRoles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).EndInit();
            this.tabCategories.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.tabMaterials.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterials)).EndInit();
            this.tabStatuses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatuses)).EndInit();
            this.tabDeliveryMethods.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeliveryMethods)).EndInit();
            this.ResumeLayout(false);

        }
    }
}