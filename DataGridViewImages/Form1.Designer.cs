namespace DataGridViewImages
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CategoryNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PictureColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.CurrentButton = new System.Windows.Forms.Button();
            this.DiscriptionLabel = new System.Windows.Forms.Label();
            this.AddCategoryButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CategoryNameColumn,
            this.PictureColumn});
            this.dataGridView1.Location = new System.Drawing.Point(18, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 100;
            this.dataGridView1.Size = new System.Drawing.Size(313, 383);
            this.dataGridView1.TabIndex = 0;
            // 
            // CategoryNameColumn
            // 
            this.CategoryNameColumn.DataPropertyName = "CategoryName";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.CategoryNameColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.CategoryNameColumn.HeaderText = "Name";
            this.CategoryNameColumn.Name = "CategoryNameColumn";
            // 
            // PictureColumn
            // 
            this.PictureColumn.DataPropertyName = "Picture";
            this.PictureColumn.HeaderText = "Picture";
            this.PictureColumn.Name = "PictureColumn";
            // 
            // CurrentButton
            // 
            this.CurrentButton.Location = new System.Drawing.Point(18, 469);
            this.CurrentButton.Name = "CurrentButton";
            this.CurrentButton.Size = new System.Drawing.Size(123, 23);
            this.CurrentButton.TabIndex = 1;
            this.CurrentButton.Text = "Get category";
            this.CurrentButton.UseVisualStyleBackColor = true;
            this.CurrentButton.Click += new System.EventHandler(this.CurrentButton_Click);
            // 
            // DiscriptionLabel
            // 
            this.DiscriptionLabel.AutoSize = true;
            this.DiscriptionLabel.Location = new System.Drawing.Point(18, 411);
            this.DiscriptionLabel.Name = "DiscriptionLabel";
            this.DiscriptionLabel.Size = new System.Drawing.Size(66, 15);
            this.DiscriptionLabel.TabIndex = 2;
            this.DiscriptionLabel.Text = "description";
            // 
            // AddCategoryButton
            // 
            this.AddCategoryButton.Location = new System.Drawing.Point(207, 469);
            this.AddCategoryButton.Name = "AddCategoryButton";
            this.AddCategoryButton.Size = new System.Drawing.Size(123, 23);
            this.AddCategoryButton.TabIndex = 3;
            this.AddCategoryButton.Text = "Add new";
            this.AddCategoryButton.UseVisualStyleBackColor = true;
            this.AddCategoryButton.Click += new System.EventHandler(this.AddCategoryButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 527);
            this.Controls.Add(this.AddCategoryButton);
            this.Controls.Add(this.DiscriptionLabel);
            this.Controls.Add(this.CurrentButton);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EF Core images";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn CategoryNameColumn;
        private DataGridViewImageColumn PictureColumn;
        private Button CurrentButton;
        private Label DiscriptionLabel;
        private Button AddCategoryButton;
    }
}