namespace BookManagement
{
    partial class FmInLibraryBookQuery
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
            this.dgvInLibraryBooks = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInLibraryBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvInLibraryBooks
            // 
            this.dgvInLibraryBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInLibraryBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInLibraryBooks.Location = new System.Drawing.Point(0, 0);
            this.dgvInLibraryBooks.Name = "dgvInLibraryBooks";
            this.dgvInLibraryBooks.RowHeadersWidth = 62;
            this.dgvInLibraryBooks.RowTemplate.Height = 30;
            this.dgvInLibraryBooks.Size = new System.Drawing.Size(1494, 804);
            this.dgvInLibraryBooks.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.Location = new System.Drawing.Point(1291, 31);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(177, 80);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // FmInLibraryBookQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1494, 804);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvInLibraryBooks);
            this.Name = "FmInLibraryBookQuery";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "在馆图书查询";
            this.Load += new System.EventHandler(this.FmInLibraryBookQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInLibraryBooks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvInLibraryBooks;
        private System.Windows.Forms.Button btnRefresh;
    }
}