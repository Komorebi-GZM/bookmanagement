namespace BookManagement
{
    partial class FmChangePwd
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
            this.txtOldPwd = new System.Windows.Forms.TextBox();
            this.OldPwd = new System.Windows.Forms.Label();
            this.NewPwd = new System.Windows.Forms.Label();
            this.txtNewPwd = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtOldPwd
            // 
            this.txtOldPwd.Location = new System.Drawing.Point(508, 293);
            this.txtOldPwd.Name = "txtOldPwd";
            this.txtOldPwd.Size = new System.Drawing.Size(300, 28);
            this.txtOldPwd.TabIndex = 0;
            // 
            // OldPwd
            // 
            this.OldPwd.AutoSize = true;
            this.OldPwd.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OldPwd.Location = new System.Drawing.Point(391, 288);
            this.OldPwd.Name = "OldPwd";
            this.OldPwd.Size = new System.Drawing.Size(111, 33);
            this.OldPwd.TabIndex = 1;
            this.OldPwd.Text = "原密码";
            // 
            // NewPwd
            // 
            this.NewPwd.AutoSize = true;
            this.NewPwd.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NewPwd.Location = new System.Drawing.Point(391, 368);
            this.NewPwd.Name = "NewPwd";
            this.NewPwd.Size = new System.Drawing.Size(111, 33);
            this.NewPwd.TabIndex = 3;
            this.NewPwd.Text = "新密码";
            // 
            // txtNewPwd
            // 
            this.txtNewPwd.Location = new System.Drawing.Point(508, 373);
            this.txtNewPwd.Name = "txtNewPwd";
            this.txtNewPwd.Size = new System.Drawing.Size(300, 28);
            this.txtNewPwd.TabIndex = 2;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("楷体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(400, 500);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(110, 50);
            this.btnConfirm.TabIndex = 4;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("楷体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(700, 500);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 50);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FmChangePwd
            // 
            this.ClientSize = new System.Drawing.Size(1252, 884);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.NewPwd);
            this.Controls.Add(this.txtNewPwd);
            this.Controls.Add(this.OldPwd);
            this.Controls.Add(this.txtOldPwd);
            this.Name = "FmChangePwd";
            this.Text = "修改密码";
            this.Load += new System.EventHandler(this.FmChangePwd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOldPwd;
        private System.Windows.Forms.Label OldPwd;
        private System.Windows.Forms.Label NewPwd;
        private System.Windows.Forms.TextBox txtNewPwd;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}