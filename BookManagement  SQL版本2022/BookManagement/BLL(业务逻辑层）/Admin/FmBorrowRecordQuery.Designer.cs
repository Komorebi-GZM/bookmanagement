namespace BookManagement
{
    partial class FmBorrowRecordQuery
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnQueryAll = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cboQueryUser = new System.Windows.Forms.ComboBox();
            this.dgvBorrowRecords = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.btnQueryAll);
            this.panel1.Controls.Add(this.btnQuery);
            this.panel1.Controls.Add(this.cboQueryUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1474, 100);
            this.panel1.TabIndex = 0;
            // 
            // btnQueryAll
            // 
            this.btnQueryAll.AutoSize = true;
            this.btnQueryAll.Location = new System.Drawing.Point(1000, 19);
            this.btnQueryAll.Name = "btnQueryAll";
            this.btnQueryAll.Size = new System.Drawing.Size(273, 53);
            this.btnQueryAll.TabIndex = 2;
            this.btnQueryAll.Text = "查看全部记录";
            this.btnQueryAll.UseVisualStyleBackColor = true;
            this.btnQueryAll.Click += new System.EventHandler(this.btnQueryAll_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(641, 19);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(197, 53);
            this.btnQuery.TabIndex = 1;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.button1_Click);
            // 
            // cboQueryUser
            // 
            this.cboQueryUser.FormattingEnabled = true;
            this.cboQueryUser.Location = new System.Drawing.Point(125, 28);
            this.cboQueryUser.Name = "cboQueryUser";
            this.cboQueryUser.Size = new System.Drawing.Size(357, 36);
            this.cboQueryUser.TabIndex = 0;
            // 
            // dgvBorrowRecords
            // 
            this.dgvBorrowRecords.AllowUserToAddRows = false;
            this.dgvBorrowRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBorrowRecords.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvBorrowRecords.Location = new System.Drawing.Point(0, 309);
            this.dgvBorrowRecords.Name = "dgvBorrowRecords";
            this.dgvBorrowRecords.RowHeadersWidth = 62;
            this.dgvBorrowRecords.RowTemplate.Height = 30;
            this.dgvBorrowRecords.Size = new System.Drawing.Size(1474, 483);
            this.dgvBorrowRecords.TabIndex = 1;
            // 
            // FmBorrowRecordQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 792);
            this.Controls.Add(this.dgvBorrowRecords);
            this.Controls.Add(this.panel1);
            this.Name = "FmBorrowRecordQuery";
            this.Text = "借阅情况查询";
            this.Load += new System.EventHandler(this.FmBorrowRecordQuery_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowRecords)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboQueryUser;
        private System.Windows.Forms.Button btnQueryAll;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridView dgvBorrowRecords;
    }
}