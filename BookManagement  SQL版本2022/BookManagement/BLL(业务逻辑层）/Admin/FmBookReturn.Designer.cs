namespace BookManagement
{
    partial class FmBookReturn
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
            this.lblUser = new System.Windows.Forms.Label();
            this.cboReturnUser = new System.Windows.Forms.ComboBox();
            this.lblTip = new System.Windows.Forms.Label();
            this.dgvUnReturnedBooks = new System.Windows.Forms.DataGridView();
            this.btnLoadUnReturned = new System.Windows.Forms.Button();
            this.btnDoReturn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnReturnedBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUser.Location = new System.Drawing.Point(47, 44);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(161, 63);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "选择用户";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboReturnUser
            // 
            this.cboReturnUser.FormattingEnabled = true;
            this.cboReturnUser.Location = new System.Drawing.Point(250, 67);
            this.cboReturnUser.Name = "cboReturnUser";
            this.cboReturnUser.Size = new System.Drawing.Size(294, 26);
            this.cboReturnUser.TabIndex = 1;
            // 
            // lblTip
            // 
            this.lblTip.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTip.Location = new System.Drawing.Point(47, 230);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(497, 108);
            this.lblTip.TabIndex = 2;
            this.lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvUnReturnedBooks
            // 
            this.dgvUnReturnedBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnReturnedBooks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvUnReturnedBooks.Location = new System.Drawing.Point(0, 402);
            this.dgvUnReturnedBooks.Name = "dgvUnReturnedBooks";
            this.dgvUnReturnedBooks.RowHeadersWidth = 62;
            this.dgvUnReturnedBooks.RowTemplate.Height = 30;
            this.dgvUnReturnedBooks.Size = new System.Drawing.Size(1505, 343);
            this.dgvUnReturnedBooks.TabIndex = 3;
            // 
            // btnLoadUnReturned
            // 
            this.btnLoadUnReturned.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoadUnReturned.Location = new System.Drawing.Point(1075, 44);
            this.btnLoadUnReturned.Name = "btnLoadUnReturned";
            this.btnLoadUnReturned.Size = new System.Drawing.Size(339, 89);
            this.btnLoadUnReturned.TabIndex = 4;
            this.btnLoadUnReturned.Text = "加载该用户未归还记录";
            this.btnLoadUnReturned.UseVisualStyleBackColor = true;
            this.btnLoadUnReturned.Click += new System.EventHandler(this.btnLoadUnReturned_Click);
            // 
            // btnDoReturn
            // 
            this.btnDoReturn.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDoReturn.Location = new System.Drawing.Point(1075, 240);
            this.btnDoReturn.Name = "btnDoReturn";
            this.btnDoReturn.Size = new System.Drawing.Size(339, 89);
            this.btnDoReturn.TabIndex = 5;
            this.btnDoReturn.Text = "办理还书";
            this.btnDoReturn.UseVisualStyleBackColor = true;
            this.btnDoReturn.Click += new System.EventHandler(this.btnDoReturn_Click);
            // 
            // FmBookReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1505, 745);
            this.Controls.Add(this.btnDoReturn);
            this.Controls.Add(this.btnLoadUnReturned);
            this.Controls.Add(this.dgvUnReturnedBooks);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.cboReturnUser);
            this.Controls.Add(this.lblUser);
            this.Name = "FmBookReturn";
            this.Text = "管理员 - 图书还书管理";
            this.Load += new System.EventHandler(this.FmBookReturn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnReturnedBooks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cboReturnUser;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.DataGridView dgvUnReturnedBooks;
        private System.Windows.Forms.Button btnLoadUnReturned;
        private System.Windows.Forms.Button btnDoReturn;
    }
}