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
    public partial class FmBookBorrow : Form
    {
        public FmBookBorrow()
        {
            InitializeComponent();
        }

        private void FmBookBorrow_Load(object sender, EventArgs e)
        {
            LoadBorrowUserList(); // 加载所有用户（管理员可看全部）
            LoadBorrowBookList(); // 加载可借图书
            dgvUserBorrow.Visible = false; // 默认隐藏用户借阅记录表格
            lblTip.Text = "";
        }
        // 步骤1：加载所有用户（管理员视角：显示用户类型，无Used=1限制）
        private void LoadBorrowUserList()
        {
            try
            {
                // 关联T_user和T_UserType，获取用户+对应的用户类型名称
                string sql = @"SELECT 
                          u.UserID, 
                          u.UserName, 
                          ut.UserTypeName  -- 从T_UserType获取类型名称
                      FROM T_user u
                      INNER JOIN T_UserType ut 
                      ON u.UserTypeID = ut.UserTypeID"; // 通过UserTypeID关联

                DataTable dt = SqlHelper.ExecuteDataTable(sql, new SqlParameter[0]);

                // 调试：确认查询结果
                MessageBox.Show($"查询到用户数：{dt.Rows.Count}行", "调试");

                if (dt.Rows.Count == 0)
                {
                    lblTip.Text = "T_user表暂无用户，或未配置用户类型关联";
                    lblTip.ForeColor = Color.Orange;
                    return;
                }

                // 清空旧数据+绑定
                cboBorrowUser.DataSource = null;
                cboBorrowUser.Items.Clear();

                // 拼接“用户名(用户类型)”显示列
                dt.Columns.Add("UserName_UserType", typeof(string), "UserName + ' (' + UserTypeName + ')'");

                // 绑定下拉框
                cboBorrowUser.DataSource = dt;
                cboBorrowUser.DisplayMember = "UserName_UserType";
                cboBorrowUser.ValueMember = "UserID";

                lblTip.Text = $"成功加载{dt.Rows.Count}个用户（关联用户类型）";
                lblTip.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                lblTip.Text = $"加载失败：{ex.Message}";
                lblTip.ForeColor = Color.Red;
            }
        }
        // 步骤2：加载可借图书（管理员视角：显示库存数）
        private void LoadBorrowBookList()
        {
            try
            {
                // 管理员可看图书库存数，方便判断
                string sql = "SELECT BookID, BookName, Stock FROM T_Book WHERE Stock>0";
                DataTable dt = SqlHelper.ExecuteDataTable(sql, new SqlParameter[0]);

                // 下拉框显示：图书名（库存数），值存BookID
                cboBorrowBook.DisplayMember = "BookName_Stock";
                cboBorrowBook.ValueMember = "BookID";
                // 新增计算列，拼接图书名和库存
                dt.Columns.Add("BookName_Stock", typeof(string), "BookName + ' (库存：' + Stock + ')'");

                cboBorrowBook.DataSource = dt;
            }
            catch (Exception ex)
            {
                lblTip.Text = $"加载图书失败：{ex.Message}";
                lblTip.ForeColor = Color.Red;
            }
        }

        private void btnCheckUserBorrow_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(cboBorrowUser.SelectedValue);
                // 查询该用户所有未归还的借阅记录
                string sql = @"SELECT 
                                  br.BorrowID,
                                  b.BookName,
                                  br.BorrowDate,
                                  '未归还' AS 状态
                              FROM T_Borrow br
                              JOIN T_Book b ON br.BookID=b.BookID
                              WHERE br.UserID=@UserID AND br.ReturnDate IS NULL";
                DataTable dt = SqlHelper.ExecuteDataTable(sql, new SqlParameter("@UserID", userId));

                // 显示表格，展示未归还记录
                dgvUserBorrow.Visible = true;
                dgvUserBorrow.DataSource = dt;
                // 优化列宽
                dgvUserBorrow.Columns["BorrowID"].Width = 80;
                dgvUserBorrow.Columns["BookName"].Width = 150;
                dgvUserBorrow.Columns["BorrowDate"].Width = 180;

                if (dt.Rows.Count == 0)
                {
                    lblTip.Text = "该用户暂无未归还的图书！";
                    lblTip.ForeColor = Color.Black;
                }
                else
                {
                    lblTip.Text = $"该用户有{dt.Rows.Count}本未归还图书（如上）";
                    lblTip.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                lblTip.Text = $"查询用户借阅记录失败：{ex.Message}";
                lblTip.ForeColor = Color.Red;
            }
        }

        private void btnDoBorrow_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(cboBorrowUser.SelectedValue);
                int bookId = Convert.ToInt32(cboBorrowBook.SelectedValue);

                // 1. 重复借阅校验（仅当前窗口适配参数，不影响其他引用）
                string checkSql = @"SELECT COUNT(*) FROM T_Borrow 
                            WHERE UserID=@UserID AND BookID=@BookID AND ReturnDate IS NULL";
                SqlParameter[] checkParams = new SqlParameter[]
                {
            new SqlParameter("@UserID", userId),
            new SqlParameter("@BookID", bookId)
                };
                int borrowCount = 0;
                // 局部创建连接，仅当前窗口用，不影响其他代码
                using (SqlConnection conn = GetSqlConnection())
                {
                    borrowCount = Convert.ToInt32(
                        SqlHelper.ExecuteScalar(conn, CommandType.Text, checkSql, checkParams)
                    );
                }
                if (borrowCount > 0)
                {
                    lblTip.Text = $"【管理员提示】用户{cboBorrowUser.Text}已借阅《{cboBorrowBook.Text}》且未归还，禁止重复借阅！";
                    lblTip.ForeColor = Color.Red;
                    return;
                }

                // 2. 库存校验（同理：局部连接，不影响其他引用）
                string stockSql = "SELECT Stock FROM T_Book WHERE BookID=@BookID";
                int stock = 0;
                using (SqlConnection conn = GetSqlConnection())
                {
                    stock = Convert.ToInt32(
                        SqlHelper.ExecuteScalar(conn, CommandType.Text, stockSql, new SqlParameter("@BookID", bookId))
                    );
                }
                if (stock <= 0)
                {
                    lblTip.Text = $"【管理员提示】《{cboBorrowBook.Text}》当前库存为0，无法借阅！";
                    lblTip.ForeColor = Color.Red;
                    return;
                }

                // 3. 执行借书：插入借阅记录（局部连接）
                string insertBorrowSql = @"INSERT INTO T_Borrow (UserID, BookID, BorrowDate)
                                   VALUES (@UserID, @BookID, GETDATE())";
                SqlParameter[] insertParams = new SqlParameter[]
                {
            new SqlParameter("@UserID", userId),
            new SqlParameter("@BookID", bookId)
                };
                int insertResult = 0;
                using (SqlConnection conn = GetSqlConnection())
                {
                    insertResult = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, insertBorrowSql, insertParams);
                }

                // 4. 更新图书库存（局部连接）
                string updateStockSql = "UPDATE T_Book SET Stock=Stock-1 WHERE BookID=@BookID";
                using (SqlConnection conn = GetSqlConnection())
                {
                    SqlHelper.ExecuteNonQuery(conn, CommandType.Text, updateStockSql, new SqlParameter("@BookID", bookId));
                }

                // 5. 操作提示（逻辑不变）
                if (insertResult > 0)
                {
                    lblTip.Text = $"【管理员操作成功】已为用户{cboBorrowUser.Text}办理《{cboBorrowBook.Text}》借书，当前库存剩余：{stock - 1}";
                    lblTip.ForeColor = Color.Green;
                    LoadBorrowBookList();
                    dgvUserBorrow.Visible = false;
                }
                else
                {
                    lblTip.Text = "【管理员操作失败】借书记录插入失败，请检查数据库！";
                    lblTip.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblTip.Text = $"【管理员操作异常】借书出错：{ex.Message}";
                lblTip.ForeColor = Color.Red;
            }
        }
        private SqlConnection GetSqlConnection()
        {
            return new SqlConnection(SqlHelper.connectionString);
        }
    }
}
