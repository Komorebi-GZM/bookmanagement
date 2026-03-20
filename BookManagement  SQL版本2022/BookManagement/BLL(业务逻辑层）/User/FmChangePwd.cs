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
    public partial class FmChangePwd : Form
    {
        // 接收当前用户ID（从FmUser传过来）
        private string _userID;
        public FmChangePwd(string userID)
        {
            InitializeComponent();
            _userID = userID;
        }

        // 确认修改
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string oldPwd = txtOldPwd.Text.Trim();
            string newPwd = txtNewPwd.Text.Trim();

            // 非空验证
            if (string.IsNullOrEmpty(oldPwd) || string.IsNullOrEmpty(newPwd))
            {
                MessageBox.Show("原密码/新密码不能为空！");
                return;
            }

            try
            {
                // 1. 验证原密码是否正确
                string checkSql = "SELECT COUNT(*) FROM T_user WHERE UserID=@UserID AND Password=@OldPwd";
                SqlParameter[] checkParas = new SqlParameter[]
                {
                new SqlParameter("@UserID", _userID),
                new SqlParameter("@OldPwd", oldPwd)
                };

                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    int count = Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, checkSql, checkParas));
                    if (count == 0)
                    {
                        MessageBox.Show("原密码错误！");
                        return;
                    }

                    // 2. 更新新密码
                    string updateSql = "UPDATE T_user SET Password=@NewPwd WHERE UserID=@UserID";
                    SqlParameter[] updateParas = new SqlParameter[]
                    {
                    new SqlParameter("@NewPwd", newPwd),
                    new SqlParameter("@UserID", _userID)
                    };

                    int rows = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, updateSql, updateParas);
                    if (rows > 0)
                    {
                        MessageBox.Show("密码修改成功！");
                        this.Close(); // 关闭弹窗
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

        // 取消修改
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FmChangePwd_Load(object sender, EventArgs e)
        {
            
        }
    }
}
