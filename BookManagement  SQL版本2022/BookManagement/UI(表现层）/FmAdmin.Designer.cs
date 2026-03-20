namespace BookManagement
{
    partial class FmAdmin
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
            this.tsmiAdminPersonal = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdminChangePwd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdminCancelAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdminBookManager = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdminBookType = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReaderManager = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUserTypeManager = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdminBorrowQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInLibraryQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBorrowRecordQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBookBorrow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDoReturn = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAdminID = new System.Windows.Forms.Label();
            this.lblAdminName = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdminPersonal,
            this.tsmiAdminBookManager,
            this.tsmiAdminBookType,
            this.tsmiReaderManager,
            this.tsmiUserTypeManager,
            this.tsmiAdminBorrowQuery,
            this.tsmiExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1521, 38);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiAdminPersonal
            // 
            this.tsmiAdminPersonal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdminChangePwd,
            this.tsmiAdminCancelAccount,
            this.tsmiAddAdmin});
            this.tsmiAdminPersonal.Name = "tsmiAdminPersonal";
            this.tsmiAdminPersonal.Size = new System.Drawing.Size(215, 34);
            this.tsmiAdminPersonal.Text = "个人信息管理";
            // 
            // tsmiAdminChangePwd
            // 
            this.tsmiAdminChangePwd.Name = "tsmiAdminChangePwd";
            this.tsmiAdminChangePwd.Size = new System.Drawing.Size(271, 38);
            this.tsmiAdminChangePwd.Text = "修改密码";
            this.tsmiAdminChangePwd.Click += new System.EventHandler(this.tsmiAdminChangePwd_Click);
            // 
            // tsmiAdminCancelAccount
            // 
            this.tsmiAdminCancelAccount.Name = "tsmiAdminCancelAccount";
            this.tsmiAdminCancelAccount.Size = new System.Drawing.Size(271, 38);
            this.tsmiAdminCancelAccount.Text = "注销账号";
            this.tsmiAdminCancelAccount.Click += new System.EventHandler(this.tsmiAdminCancelAccount_Click);
            // 
            // tsmiAddAdmin
            // 
            this.tsmiAddAdmin.Name = "tsmiAddAdmin";
            this.tsmiAddAdmin.Size = new System.Drawing.Size(271, 38);
            this.tsmiAddAdmin.Text = "添加管理员";
            this.tsmiAddAdmin.Click += new System.EventHandler(this.tsmiAddAdmin_Click);
            // 
            // tsmiAdminBookManager
            // 
            this.tsmiAdminBookManager.Name = "tsmiAdminBookManager";
            this.tsmiAdminBookManager.Size = new System.Drawing.Size(153, 34);
            this.tsmiAdminBookManager.Text = "图书管理";
            this.tsmiAdminBookManager.Click += new System.EventHandler(this.tsmiAdminBookManager_Click);
            // 
            // tsmiAdminBookType
            // 
            this.tsmiAdminBookType.Name = "tsmiAdminBookType";
            this.tsmiAdminBookType.Size = new System.Drawing.Size(215, 34);
            this.tsmiAdminBookType.Text = "图书类别管理";
            this.tsmiAdminBookType.Click += new System.EventHandler(this.tsmiAdminBookType_Click);
            // 
            // tsmiReaderManager
            // 
            this.tsmiReaderManager.Name = "tsmiReaderManager";
            this.tsmiReaderManager.Size = new System.Drawing.Size(153, 34);
            this.tsmiReaderManager.Text = "读者管理";
            this.tsmiReaderManager.Click += new System.EventHandler(this.tsmiReaderManager_Click);
            // 
            // tsmiUserTypeManager
            // 
            this.tsmiUserTypeManager.Name = "tsmiUserTypeManager";
            this.tsmiUserTypeManager.Size = new System.Drawing.Size(215, 34);
            this.tsmiUserTypeManager.Text = "读者类别管理";
            this.tsmiUserTypeManager.Click += new System.EventHandler(this.tsmiUserTypeManager_Click);
            // 
            // tsmiAdminBorrowQuery
            // 
            this.tsmiAdminBorrowQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiInLibraryQuery,
            this.tsmiBorrowRecordQuery,
            this.tsmiBookBorrow,
            this.tsmiDoReturn});
            this.tsmiAdminBorrowQuery.Name = "tsmiAdminBorrowQuery";
            this.tsmiAdminBorrowQuery.Size = new System.Drawing.Size(277, 34);
            this.tsmiAdminBorrowQuery.Text = "图书借阅综合管理";
            this.tsmiAdminBorrowQuery.Click += new System.EventHandler(this.btnAdminBorrowQuery_Click);
            // 
            // tsmiInLibraryQuery
            // 
            this.tsmiInLibraryQuery.Name = "tsmiInLibraryQuery";
            this.tsmiInLibraryQuery.Size = new System.Drawing.Size(302, 38);
            this.tsmiInLibraryQuery.Text = "在馆图书查询";
            this.tsmiInLibraryQuery.Click += new System.EventHandler(this.tsmiInLibraryQuery_Click);
            // 
            // tsmiBorrowRecordQuery
            // 
            this.tsmiBorrowRecordQuery.Name = "tsmiBorrowRecordQuery";
            this.tsmiBorrowRecordQuery.Size = new System.Drawing.Size(302, 38);
            this.tsmiBorrowRecordQuery.Text = "借阅情况查询";
            this.tsmiBorrowRecordQuery.Click += new System.EventHandler(this.tsmiBorrowRecordQuery_Click);
            // 
            // tsmiBookBorrow
            // 
            this.tsmiBookBorrow.Name = "tsmiBookBorrow";
            this.tsmiBookBorrow.Size = new System.Drawing.Size(302, 38);
            this.tsmiBookBorrow.Text = "办理借书";
            this.tsmiBookBorrow.Click += new System.EventHandler(this.tsmiBookBorrow_Click);
            // 
            // tsmiDoReturn
            // 
            this.tsmiDoReturn.Name = "tsmiDoReturn";
            this.tsmiDoReturn.Size = new System.Drawing.Size(302, 38);
            this.tsmiDoReturn.Text = "办理还书";
            this.tsmiDoReturn.Click += new System.EventHandler(this.办理还书ToolStripMenuItem_Click);
            // 
            // tsmiExit
            // 
            this.tsmiExit.ForeColor = System.Drawing.Color.Red;
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(153, 34);
            this.tsmiExit.Text = "退出登录";
            this.tsmiExit.Click += new System.EventHandler(this.退出登录ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("华文行楷", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(12, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 40);
            this.label1.TabIndex = 2;
            this.label1.Text = "管理员";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAdminID
            // 
            this.lblAdminID.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblAdminID.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAdminID.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblAdminID.Location = new System.Drawing.Point(12, 138);
            this.lblAdminID.Name = "lblAdminID";
            this.lblAdminID.Size = new System.Drawing.Size(522, 40);
            this.lblAdminID.TabIndex = 4;
            this.lblAdminID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAdminID.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblAdminName
            // 
            this.lblAdminName.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAdminName.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lblAdminName.Location = new System.Drawing.Point(12, 202);
            this.lblAdminName.Name = "lblAdminName";
            this.lblAdminName.Size = new System.Drawing.Size(522, 40);
            this.lblAdminName.TabIndex = 5;
            this.lblAdminName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(1521, 1020);
            this.Controls.Add(this.lblAdminName);
            this.Controls.Add(this.lblAdminID);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FmAdmin";
            this.Text = "管理员";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmAdmin_FormClosing);
            this.Load += new System.EventHandler(this.FmAdmin_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdminPersonal;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdminChangePwd;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdminCancelAccount;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdminBookManager;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.Label lblAdminID;
        private System.Windows.Forms.Label lblAdminName;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdminBookType;
        private System.Windows.Forms.ToolStripMenuItem tsmiUserTypeManager;
        private System.Windows.Forms.ToolStripMenuItem tsmiReaderManager;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdminBorrowQuery;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddAdmin;
        private System.Windows.Forms.ToolStripMenuItem tsmiInLibraryQuery;
        private System.Windows.Forms.ToolStripMenuItem tsmiBorrowRecordQuery;
        private System.Windows.Forms.ToolStripMenuItem tsmiBookBorrow;
        private System.Windows.Forms.ToolStripMenuItem tsmiDoReturn;
    }
}