namespace BookManagement
{
    partial class FmBookBorrow
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
            this.dgvUserBorrow = new System.Windows.Forms.DataGridView();
            this.btnCheckUserBorrow = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBorrowUser = new System.Windows.Forms.ComboBox();
            this.cboBorrowBook = new System.Windows.Forms.ComboBox();
            this.btnDoBorrow = new System.Windows.Forms.Button();
            this.lblTip = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserBorrow)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUserBorrow
            // 
            this.dgvUserBorrow.AllowUserToAddRows = false;
            this.dgvUserBorrow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserBorrow.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvUserBorrow.Location = new System.Drawing.Point(0, 373);
            this.dgvUserBorrow.Name = "dgvUserBorrow";
            this.dgvUserBorrow.RowHeadersWidth = 62;
            this.dgvUserBorrow.RowTemplate.Height = 30;
            this.dgvUserBorrow.Size = new System.Drawing.Size(1429, 404);
            this.dgvUserBorrow.TabIndex = 0;
            this.dgvUserBorrow.Visible = false;
            // 
            // btnCheckUserBorrow
            // 
            this.btnCheckUserBorrow.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCheckUserBorrow.Location = new System.Drawing.Point(1066, 12);
            this.btnCheckUserBorrow.Name = "btnCheckUserBorrow";
            this.btnCheckUserBorrow.Size = new System.Drawing.Size(336, 131);
            this.btnCheckUserBorrow.TabIndex = 1;
            this.btnCheckUserBorrow.Text = "查看该用户未归还记录";
            this.btnCheckUserBorrow.UseVisualStyleBackColor = true;
            this.btnCheckUserBorrow.Click += new System.EventHandler(this.btnCheckUserBorrow_Click);
            // 
            // lblUser
            // 
            this.lblUser.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUser.Location = new System.Drawing.Point(25, 49);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(144, 57);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "选择用户";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(25, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 57);
            this.label1.TabIndex = 3;
            this.label1.Text = "选择图书";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboBorrowUser
            // 
            this.cboBorrowUser.FormattingEnabled = true;
            this.cboBorrowUser.Location = new System.Drawing.Point(226, 69);
            this.cboBorrowUser.Name = "cboBorrowUser";
            this.cboBorrowUser.Size = new System.Drawing.Size(295, 26);
            this.cboBorrowUser.TabIndex = 4;
            // 
            // cboBorrowBook
            // 
            this.cboBorrowBook.FormattingEnabled = true;
            this.cboBorrowBook.Location = new System.Drawing.Point(226, 293);
            this.cboBorrowBook.Name = "cboBorrowBook";
            this.cboBorrowBook.Size = new System.Drawing.Size(295, 26);
            this.cboBorrowBook.TabIndex = 5;
            // 
            // btnDoBorrow
            // 
            this.btnDoBorrow.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDoBorrow.Location = new System.Drawing.Point(1066, 236);
            this.btnDoBorrow.Name = "btnDoBorrow";
            this.btnDoBorrow.Size = new System.Drawing.Size(336, 131);
            this.btnDoBorrow.TabIndex = 6;
            this.btnDoBorrow.Text = "办理借书";
            this.btnDoBorrow.UseVisualStyleBackColor = true;
            this.btnDoBorrow.Click += new System.EventHandler(this.btnDoBorrow_Click);
            // 
            // lblTip
            // 
            this.lblTip.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTip.Location = new System.Drawing.Point(25, 106);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(348, 167);
            this.lblTip.TabIndex = 7;
            this.lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FmBookBorrow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1429, 777);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.btnDoBorrow);
            this.Controls.Add(this.cboBorrowBook);
            this.Controls.Add(this.cboBorrowUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.btnCheckUserBorrow);
            this.Controls.Add(this.dgvUserBorrow);
            this.Name = "FmBookBorrow";
            this.Text = "管理员 - 图书借书管理";
            this.Load += new System.EventHandler(this.FmBookBorrow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserBorrow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUserBorrow;
        private System.Windows.Forms.Button btnCheckUserBorrow;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBorrowUser;
        private System.Windows.Forms.ComboBox cboBorrowBook;
        private System.Windows.Forms.Button btnDoBorrow;
        private System.Windows.Forms.Label lblTip;
    }
}