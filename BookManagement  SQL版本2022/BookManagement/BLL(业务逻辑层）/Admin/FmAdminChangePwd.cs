using Lms.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookManagement
{
    public partial class FmAdminChangePwd : Form
    {
        private string _adminID;
        public FmAdminChangePwd()
        {
            InitializeComponent();
        }
        public FmAdminChangePwd(string AdminID)
        {
            InitializeComponent();
            _adminID = AdminID; // 将传入的ID赋值给全局变量
        }
        private void FmAdminChangePwd_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string oldPwd = txtOldPwd.Text.Trim();
            string newPwd = txtNewPwd.Text.Trim();

            // 非空校验
            if (string.IsNullOrEmpty(oldPwd) || string.IsNullOrEmpty(newPwd))
            {
                MessageBox.Show("原密码/新密码不能为空！");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open();
                    // 1. 验证原密码是否正确（操作T_Admin表）
                    string checkSql = "SELECT COUNT(*) FROM T_Admin WHERE AdminID=@AdminID AND Password=@OldPwd";
                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                    checkCmd.Parameters.Add(new SqlParameter("@AdminID", _adminID));
                    checkCmd.Parameters.Add(new SqlParameter("@OldPwd", oldPwd));
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count == 0)
                    {
                        MessageBox.Show("原密码错误！");
                        return;
                    }

                    // 2. 更新新密码
                    string updateSql = "UPDATE T_Admin SET Password=@NewPwd WHERE AdminID=@AdminID";
                    SqlCommand updateCmd = new SqlCommand(updateSql, conn);
                    updateCmd.Parameters.Add(new SqlParameter("@NewPwd", newPwd));
                    updateCmd.Parameters.Add(new SqlParameter("@AdminID", _adminID));
                    int rows = updateCmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("密码修改成功！");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("密码修改失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("修改密码出错：" + ex.Message);
            }
        
         }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FmChangePwd_Load(object sender, EventArgs e)
        {

        }
    }
}
