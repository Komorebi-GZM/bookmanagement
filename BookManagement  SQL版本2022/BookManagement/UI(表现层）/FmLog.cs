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
using System.Data;
using System.Data.SqlClient;
using Lms.DataAccess;
using BookManagement;

namespace BookManagement
{
    public partial class FmLog : Form
    {
        public FmLog()
        {
            InitializeComponent();
            /*try
            {
                // 1. 获取SqlHelper中的连接字符串
                string connStr = Lms.DataAccess.SqlHelper.connectionString;
                // 2. 创建SqlConnection对象
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // 3. 调用ExecuteScalar，传入SqlConnection、CommandType、SQL语句
                    object adminCount = Lms.DataAccess.SqlHelper.ExecuteScalar(conn, CommandType.Text, "SELECT COUNT(*) FROM T_Admin");
                    MessageBox.Show("✅ 数据库连接成功！T_Admin表有：" + adminCount.ToString() + " 条数据");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ 连接失败：" + ex.Message);
            }*/
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {

        }

        private void FmLog_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 1. 获取控件输入值（匹配你的控件名：txtid=账号框，txtPassword=密码框，rbtnAdmin=管理员单选框）
            string account = txtid.Text.Trim();
            string pwd = txtPassword.Text.Trim();
            bool isAdmin = rbtnAdmin.Checked;

            // 2. 非空验证
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("账号或密码不能为空！");
                return;
            }

            try
            {
                string sql = "";
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter("@Account", account),
                    new SqlParameter("@Pwd", pwd)
                };

                // 3. 区分管理员/用户，编写查询SQL（查ID+姓名，而非仅COUNT）
                if (isAdmin)
                {
                    // 管理员：查T_Admin表的AdminID（ID）和Name（姓名），匹配你的数据库字段
                    sql = "SELECT AdminID, AdminName FROM T_Admin WHERE AdminID=@Account AND Password=@Pwd";
                }
                else
                {
                    // 普通用户：查T_user表的UserID（ID）和Name（姓名），需匹配你的T_user表字段
                    sql = "SELECT UserID, UserName FROM T_user WHERE UserID=@Account AND Password=@Pwd";
                }

                // 4. 打开数据库连接，读取ID+姓名
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddRange(paras); // 添加参数（防SQL注入）
                    conn.Open(); // 打开连接
                    SqlDataReader reader = cmd.ExecuteReader(); // 读取数据

                    // 5. 判断是否查询到数据（登录成功）
                    if (reader.Read())
                    {
                        // 读取第一列（ID）、第二列（姓名）
                        string userId = reader[0].ToString();
                        string userName = reader[1].ToString();

                        MessageBox.Show("登录成功！");

                        // 6. 传参给目标窗体并打开
                        if (isAdmin)
                        {
                            // 管理员：传AdminID和Name给FormAdmin
                            FmAdmin adminForm = new FmAdmin(userId, userName);
                            adminForm.Show();
                        }
                        else
                        {
                            // 普通用户：传UserID和UserName给FmUser（已解开注释，删除多余提示）
                            FmUser userForm = new FmUser(userId, userName);
                            userForm.Show();
                        }

                        this.Hide(); // 隐藏登录窗体
                    }
                    else
                    {
                        // 未查询到数据=账号/密码错误
                        MessageBox.Show("账号或密码错误！");
                    }

                    reader.Close(); // 关闭DataReader
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("登录失败：" + ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // 关闭整个应用程序，避免后台残留
            Application.Exit();
        }

        private void FmLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
