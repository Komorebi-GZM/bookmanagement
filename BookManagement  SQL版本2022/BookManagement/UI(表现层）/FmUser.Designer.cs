namespace BookManagement
{
    partial class FmUser
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.个人信息管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChangePwd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.租界管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBorrowBook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReturnBook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.gbBorrowBook = new System.Windows.Forms.GroupBox();
            this.btnDoBorrow = new System.Windows.Forms.Button();
            this.dgvBorrowableBooks = new System.Windows.Forms.DataGridView();
            this.btnQueryBorrowBook = new System.Windows.Forms.Button();
            this.txtBorrowBookKey = new System.Windows.Forms.TextBox();
            this.lblBorrowTip = new System.Windows.Forms.Label();
            this.gbReturnBook = new System.Windows.Forms.GroupBox();
            this.btnDoReturn = new System.Windows.Forms.Button();
            this.dgvUnreturnedBooks = new System.Windows.Forms.DataGridView();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.gbBorrowBook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowableBooks)).BeginInit();
            this.gbReturnBook.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnreturnedBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.个人信息管理ToolStripMenuItem,
            this.租界管理ToolStripMenuItem,
            this.tsmiExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1533, 38);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 个人信息管理ToolStripMenuItem
            // 
            this.个人信息管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiChangePwd,
            this.tsmiLogout});
            this.个人信息管理ToolStripMenuItem.Name = "个人信息管理ToolStripMenuItem";
            this.个人信息管理ToolStripMenuItem.Size = new System.Drawing.Size(216, 36);
            this.个人信息管理ToolStripMenuItem.Text = "个人信息管理";
            // 
            // tsmiChangePwd
            // 
            this.tsmiChangePwd.Name = "tsmiChangePwd";
            this.tsmiChangePwd.Size = new System.Drawing.Size(242, 40);
            this.tsmiChangePwd.Text = "修改密码";
            this.tsmiChangePwd.Click += new System.EventHandler(this.tsmiChangePwd_Click);
            // 
            // tsmiLogout
            // 
            this.tsmiLogout.Name = "tsmiLogout";
            this.tsmiLogout.Size = new System.Drawing.Size(242, 40);
            this.tsmiLogout.Text = "注销账号";
            this.tsmiLogout.Click += new System.EventHandler(this.注销账号ToolStripMenuItem_Click);
            // 
            // 租界管理ToolStripMenuItem
            // 
            this.租界管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBorrowBook,
            this.tsmiReturnBook});
            this.租界管理ToolStripMenuItem.Name = "租界管理ToolStripMenuItem";
            this.租界管理ToolStripMenuItem.Size = new System.Drawing.Size(154, 36);
            this.租界管理ToolStripMenuItem.Text = "租借管理";
            // 
            // tsmiBorrowBook
            // 
            this.tsmiBorrowBook.Name = "tsmiBorrowBook";
            this.tsmiBorrowBook.Size = new System.Drawing.Size(242, 40);
            this.tsmiBorrowBook.Text = "租借图书";
            this.tsmiBorrowBook.Click += new System.EventHandler(this.tsmiBorrowBook_Click);
            // 
            // tsmiReturnBook
            // 
            this.tsmiReturnBook.Name = "tsmiReturnBook";
            this.tsmiReturnBook.Size = new System.Drawing.Size(242, 40);
            this.tsmiReturnBook.Text = "归还图书";
            this.tsmiReturnBook.Click += new System.EventHandler(this.tsmiReturnBook_Click);
            // 
            // tsmiExit
            // 
            this.tsmiExit.ForeColor = System.Drawing.Color.Red;
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(154, 36);
            this.tsmiExit.Text = "退出登录";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label1.Font = new System.Drawing.Font("华文行楷", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(34, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "用户";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUserName
            // 
            this.lblUserName.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblUserName.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblUserName.Location = new System.Drawing.Point(34, 192);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(371, 40);
            this.lblUserName.TabIndex = 4;
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUserID
            // 
            this.lblUserID.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblUserID.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUserID.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblUserID.Location = new System.Drawing.Point(34, 128);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(371, 40);
            this.lblUserID.TabIndex = 5;
            this.lblUserID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbBorrowBook
            // 
            this.gbBorrowBook.Controls.Add(this.btnDoBorrow);
            this.gbBorrowBook.Controls.Add(this.dgvBorrowableBooks);
            this.gbBorrowBook.Controls.Add(this.btnQueryBorrowBook);
            this.gbBorrowBook.Controls.Add(this.txtBorrowBookKey);
            this.gbBorrowBook.Controls.Add(this.lblBorrowTip);
            this.gbBorrowBook.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbBorrowBook.Location = new System.Drawing.Point(12, 259);
            this.gbBorrowBook.Name = "gbBorrowBook";
            this.gbBorrowBook.Size = new System.Drawing.Size(688, 612);
            this.gbBorrowBook.TabIndex = 7;
            this.gbBorrowBook.TabStop = false;
            this.gbBorrowBook.Text = "租借图书";
            // 
            // btnDoBorrow
            // 
            this.btnDoBorrow.AutoSize = true;
            this.btnDoBorrow.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDoBorrow.Location = new System.Drawing.Point(148, 278);
            this.btnDoBorrow.Name = "btnDoBorrow";
            this.btnDoBorrow.Size = new System.Drawing.Size(323, 56);
            this.btnDoBorrow.TabIndex = 4;
            this.btnDoBorrow.Text = "确认租借选中图书";
            this.btnDoBorrow.UseVisualStyleBackColor = true;
            this.btnDoBorrow.Click += new System.EventHandler(this.btnDoBorrow_Click);
            // 
            // dgvBorrowableBooks
            // 
            this.dgvBorrowableBooks.AllowUserToAddRows = false;
            this.dgvBorrowableBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvBorrowableBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBorrowableBooks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvBorrowableBooks.Location = new System.Drawing.Point(3, 362);
            this.dgvBorrowableBooks.Name = "dgvBorrowableBooks";
            this.dgvBorrowableBooks.ReadOnly = true;
            this.dgvBorrowableBooks.RowHeadersWidth = 62;
            this.dgvBorrowableBooks.RowTemplate.Height = 30;
            this.dgvBorrowableBooks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBorrowableBooks.Size = new System.Drawing.Size(682, 247);
            this.dgvBorrowableBooks.TabIndex = 3;
            // 
            // btnQueryBorrowBook
            // 
            this.btnQueryBorrowBook.AutoSize = true;
            this.btnQueryBorrowBook.Location = new System.Drawing.Point(204, 168);
            this.btnQueryBorrowBook.Name = "btnQueryBorrowBook";
            this.btnQueryBorrowBook.Size = new System.Drawing.Size(208, 63);
            this.btnQueryBorrowBook.TabIndex = 2;
            this.btnQueryBorrowBook.Text = "查询可借图书";
            this.btnQueryBorrowBook.UseVisualStyleBackColor = true;
            this.btnQueryBorrowBook.Click += new System.EventHandler(this.btnQueryBorrowBook_Click);
            // 
            // txtBorrowBookKey
            // 
            this.txtBorrowBookKey.Location = new System.Drawing.Point(204, 95);
            this.txtBorrowBookKey.Name = "txtBorrowBookKey";
            this.txtBorrowBookKey.Size = new System.Drawing.Size(250, 39);
            this.txtBorrowBookKey.TabIndex = 1;
            // 
            // lblBorrowTip
            // 
            this.lblBorrowTip.AutoSize = true;
            this.lblBorrowTip.Location = new System.Drawing.Point(72, 98);
            this.lblBorrowTip.Name = "lblBorrowTip";
            this.lblBorrowTip.Size = new System.Drawing.Size(70, 28);
            this.lblBorrowTip.TabIndex = 0;
            this.lblBorrowTip.Text = "书名";
            // 
            // gbReturnBook
            // 
            this.gbReturnBook.Controls.Add(this.btnDoReturn);
            this.gbReturnBook.Controls.Add(this.dgvUnreturnedBooks);
            this.gbReturnBook.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbReturnBook.Location = new System.Drawing.Point(798, 259);
            this.gbReturnBook.Name = "gbReturnBook";
            this.gbReturnBook.Size = new System.Drawing.Size(688, 612);
            this.gbReturnBook.TabIndex = 8;
            this.gbReturnBook.TabStop = false;
            this.gbReturnBook.Text = "归还图书";
            // 
            // btnDoReturn
            // 
            this.btnDoReturn.AutoSize = true;
            this.btnDoReturn.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDoReturn.Location = new System.Drawing.Point(213, 253);
            this.btnDoReturn.Name = "btnDoReturn";
            this.btnDoReturn.Size = new System.Drawing.Size(271, 55);
            this.btnDoReturn.TabIndex = 1;
            this.btnDoReturn.Text = "确认归还选中图书";
            this.btnDoReturn.UseVisualStyleBackColor = true;
            this.btnDoReturn.Click += new System.EventHandler(this.btnDoReturn_Click);
            // 
            // dgvUnreturnedBooks
            // 
            this.dgvUnreturnedBooks.AllowUserToAddRows = false;
            this.dgvUnreturnedBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnreturnedBooks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvUnreturnedBooks.Location = new System.Drawing.Point(3, 362);
            this.dgvUnreturnedBooks.Name = "dgvUnreturnedBooks";
            this.dgvUnreturnedBooks.ReadOnly = true;
            this.dgvUnreturnedBooks.RowHeadersWidth = 62;
            this.dgvUnreturnedBooks.RowTemplate.Height = 30;
            this.dgvUnreturnedBooks.Size = new System.Drawing.Size(682, 247);
            this.dgvUnreturnedBooks.TabIndex = 0;
            this.dgvUnreturnedBooks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // pnlContent
            // 
            this.pnlContent.Location = new System.Drawing.Point(798, 259);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(735, 692);
            this.pnlContent.TabIndex = 2;
            // 
            // FmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1533, 957);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.gbReturnBook);
            this.Controls.Add(this.gbBorrowBook);
            this.Controls.Add(this.lblUserID);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FmUser";
            this.Text = "用户";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmUser_FormClosing);
            this.Load += new System.EventHandler(this.FmUser_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbBorrowBook.ResumeLayout(false);
            this.gbBorrowBook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowableBooks)).EndInit();
            this.gbReturnBook.ResumeLayout(false);
            this.gbReturnBook.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnreturnedBooks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 个人信息管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiChangePwd;
        private System.Windows.Forms.ToolStripMenuItem tsmiLogout;
        private System.Windows.Forms.ToolStripMenuItem 租界管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiBorrowBook;
        private System.Windows.Forms.ToolStripMenuItem tsmiReturnBook;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.GroupBox gbBorrowBook;
        private System.Windows.Forms.GroupBox gbReturnBook;
        private System.Windows.Forms.DataGridView dgvBorrowableBooks;
        private System.Windows.Forms.Button btnQueryBorrowBook;
        private System.Windows.Forms.TextBox txtBorrowBookKey;
        private System.Windows.Forms.Label lblBorrowTip;
        private System.Windows.Forms.Button btnDoBorrow;
        private System.Windows.Forms.DataGridView dgvUnreturnedBooks;
        private System.Windows.Forms.Button btnDoReturn;
        private System.Windows.Forms.Panel pnlContent;
    }
}