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
    public partial class FmUser : Form
    {  
        private string currentUserID;

       
        public FmUser(string userId, string userName)
        {
            InitializeComponent();
            // 给全局变量赋值
            currentUserID = userId;
            // 同时给Label赋值显示
            lblUserID.Text = "用户ID：" + userId;
            lblUserName.Text = "用户姓名：" + userName;
        }

        private void FmUser_Load(object sender, EventArgs e)
        {
            // 初始隐藏两个区域
            gbBorrowBook.Visible = false;
            gbReturnBook.Visible = false;

            // 加载当前用户未归还的图书记录
            LoadUnreturnedBooks();
        }

        private void tsmiChangePwd_Click(object sender, EventArgs e)
        {
            // 打开修改密码弹窗，传入当前用户ID
            FmChangePwd frmPwd = new FmChangePwd(currentUserID);
            frmPwd.ShowDialog(); // 模态弹窗，必须关闭才能操作FmUser
        }

        private void 注销账号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 提示用户确认注销
            if (MessageBox.Show("确定要注销账号并返回登录页吗？（删除账号请使用管理员模式对帐号进行管理）", "注销确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // 关闭当前FmUser窗口
                this.Close();
                // 打开登录窗体FrmLog
                FmLog loginForm = new FmLog();
                loginForm.ShowDialog();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnQueryBorrowBook_Click(object sender, EventArgs e)
        {
            string key = txtBorrowBookKey.Text.Trim();
            // 若未输入关键词，查询所有库存>0的图书
            if (key == "请输入关键词") key = "";

            try
            {
                // SQL：模糊查询+过滤库存>0
                string sql = @"SELECT BookID AS 图书ID, BookName AS 书名, 
                      Author AS 作者, Stock AS 库存 
                      FROM T_Book 
                      WHERE (BookName LIKE @Key OR Author LIKE @Key) AND Stock > 0";

                SqlParameter[] paras = new SqlParameter[]
                {
            new SqlParameter("@Key", "%" + key + "%")
                };

                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                    sda.SelectCommand.Parameters.AddRange(paras);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    // 绑定到DataGridView
                    dgvBorrowableBooks.DataSource = dt;

                    // 空数据提示
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("暂无可借图书！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询可借图书失败：" + ex.Message);
            }
        }

        private void btnDoBorrow_Click(object sender, EventArgs e)
        {
            if (dgvBorrowableBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中要租借的图书！");
                return;
            }
            string bookID = dgvBorrowableBooks.SelectedRows[0].Cells["图书ID"].Value.ToString();
            int stock = Convert.ToInt32(dgvBorrowableBooks.SelectedRows[0].Cells["库存"].Value);
            if (stock <= 0)
            {
                MessageBox.Show("该图书暂无库存，无法租借！");
                return;
            }
            bool isBorrowed = CheckIsBorrowed(bookID);
            if (isBorrowed)
            {
                MessageBox.Show("你已租借该图书（未归还），无法重复租借！");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction(); // 开启事务

                    try
                    {
                        // 步骤1：插入借阅记录（改用SqlCommand+事务）
                        string insertBorrowSql = @"INSERT INTO T_Borrow (UserID, BookID, BorrowDate, ReturnDate) 
                                          VALUES (@UserID, @BookID, GETDATE(), NULL)";
                        using (SqlCommand cmd = new SqlCommand(insertBorrowSql, conn, tran))
                        {
                            cmd.Parameters.Add(new SqlParameter("@UserID", currentUserID));
                            cmd.Parameters.Add(new SqlParameter("@BookID", bookID));
                            cmd.ExecuteNonQuery();
                        }

                        // 步骤2：更新图书库存（改用SqlCommand+事务）
                        string updateStockSql = "UPDATE T_Book SET Stock=Stock-1 WHERE BookID=@BookID";
                        using (SqlCommand cmd = new SqlCommand(updateStockSql, conn, tran))
                        {
                            cmd.Parameters.Add(new SqlParameter("@BookID", bookID));
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit(); // 提交事务
                        MessageBox.Show("租借成功！");
                        btnQueryBorrowBook_Click(null, null); // 刷新可借列表
                        LoadUnreturnedBooks(); // 刷新未归还列表
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback(); // 回滚事务
                        MessageBox.Show("租借失败：" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("租借出错：" + ex.Message);
            }
        }
        private bool CheckIsBorrowed(string bookID)
        {
            string sql = @"SELECT COUNT(*) FROM T_Borrow 
                  WHERE UserID=@UserID AND BookID=@BookID AND ReturnDate IS NULL";
            using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@UserID", currentUserID));
                cmd.Parameters.Add(new SqlParameter("@BookID", bookID));
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        private void LoadUnreturnedBooks()
        {
            try
            {
                string sql = @"SELECT b.BorrowID AS 借阅ID, bo.BookID AS 图书ID, 
                      bo.BookName AS 书名, CONVERT(varchar, b.BorrowDate, 23) AS 借阅日期 
                      FROM T_Borrow b 
                      LEFT JOIN T_Book bo ON b.BookID=bo.BookID 
                      WHERE b.UserID=@UserID AND b.ReturnDate IS NULL";

                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@UserID", currentUserID));
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                 
                    dgvUnreturnedBooks.DataSource = dt;

                    dgvUnreturnedBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载未归还图书失败：" + ex.Message);
            }
        }

        private void btnDoReturn_Click(object sender, EventArgs e)
        {
            // 校验1：是否选中未归还记录
            if (dgvUnreturnedBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中要归还的图书！");
                return;
            }
           
            string borrowID = dgvUnreturnedBooks.SelectedRows[0].Cells["借阅ID"].Value.ToString();
            string bookID = dgvUnreturnedBooks.SelectedRows[0].Cells["图书ID"].Value.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open();
                    SqlTransaction tran = conn.BeginTransaction(); // 开启事务

                    try
                    {
                        // 步骤1：更新T_Borrow的ReturnDate
                        string updateReturnSql = @"UPDATE T_Borrow 
                                      SET ReturnDate=GETDATE() 
                                      WHERE BorrowID=@BorrowID AND UserID=@UserID";
                        using (SqlCommand cmd = new SqlCommand(updateReturnSql, conn, tran))
                        {
                            // 添加参数
                            cmd.Parameters.Add(new SqlParameter("@BorrowID", borrowID));
                            cmd.Parameters.Add(new SqlParameter("@UserID", currentUserID));
                            cmd.ExecuteNonQuery(); // 执行SQL
                        }

                        // 步骤2：更新T_Book的库存
                        string updateStockSql = "UPDATE T_Book SET Stock=Stock+1 WHERE BookID=@BookID";
                        using (SqlCommand cmd = new SqlCommand(updateStockSql, conn, tran))
                        {
                            cmd.Parameters.Add(new SqlParameter("@BookID", bookID));
                            cmd.ExecuteNonQuery(); // 执行SQL
                        }

                        tran.Commit(); // 提交事务
                        MessageBox.Show("归还成功！");
                        LoadUnreturnedBooks(); // 刷新未归还列表
                        btnQueryBorrowBook_Click(null, null); // 刷新可借图书列表
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback(); // 回滚事务
                        MessageBox.Show("归还失败：" + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("归还出错：" + ex.Message);
            }
        }

        private void tsmiBorrowBook_Click(object sender, EventArgs e)
        {
            // 显示租借区域，隐藏归还区域
            gbBorrowBook.Visible = true;
            gbReturnBook.Visible = false;

            // 设置租借区域占满父容器（pnlContent）
            gbBorrowBook.Dock = DockStyle.Fill;

            // 自动查询可借图书（同步数据）
            btnQueryBorrowBook_Click(null, null);
        }

        private void tsmiReturnBook_Click(object sender, EventArgs e)
        {
            // 显示归还区域，隐藏租借区域
            gbReturnBook.Visible = true;
            gbBorrowBook.Visible = false;

            // 设置归还区域占满父容器（pnlContent）
            gbReturnBook.Dock = DockStyle.Fill;

            // 自动刷新未归还图书（同步数据）
            LoadUnreturnedBooks();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            // 1. 防误操作：弹出确认对话框
            DialogResult confirmResult = MessageBox.Show(
                "确定要退出当前登录吗？",
                "退出登录",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            // 2. 确认退出则关闭当前窗体，打开登录页
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // 关闭FmUser窗体
                    this.Close();
                    // 打开全新的登录窗体（重置会话）
                    FmLog loginForm = new FmLog();
                    loginForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"退出登录失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FmUser_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}
