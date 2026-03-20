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
    public partial class FmBookReturn : Form
    {
        public FmBookReturn()
        {
            InitializeComponent();
        }

        private void FmBookReturn_Load(object sender, EventArgs e)
        {
            LoadReturnUserList(); // 加载所有用户
 
            lblTip.Text = "";
            // 表格默认隐藏列（只显示管理员关心的字段）
            dgvUnReturnedBooks.AllowUserToAddRows = false; // 关闭空行
        }
        // 步骤1：加载所有用户到下拉框（关联T_UserType，和借书窗口逻辑一致）
        private void LoadReturnUserList()
        {
            try
            {
                string sql = @"SELECT 
                                  u.UserID, 
                                  u.UserName, 
                                  ut.UserTypeName 
                              FROM T_user u
                              INNER JOIN T_UserType ut 
                              ON u.UserTypeID = ut.UserTypeID";
                DataTable dt = SqlHelper.ExecuteDataTable(sql, new SqlParameter[0]);

                if (dt.Rows.Count == 0)
                {
                    lblTip.Text = "暂无用户数据！";
                    lblTip.ForeColor = Color.Orange;
                    return;
                }

                // 拼接“用户名(用户类型)”显示
                dt.Columns.Add("UserName_UserType", typeof(string), "UserName + ' (' + UserTypeName + ')'");
                cboReturnUser.DataSource = dt;
                cboReturnUser.DisplayMember = "UserName_UserType";
                cboReturnUser.ValueMember = "UserID";
            }
            catch (Exception ex)
            {
                lblTip.Text = $"加载用户失败：{ex.Message}";
                lblTip.ForeColor = Color.Red;
            }
        }

        private void btnLoadUnReturned_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(cboReturnUser.SelectedValue);
                // 查询该用户所有未归还的借阅记录（关联图书信息）
                string sql = @"SELECT 
                                  br.BorrowID,
                                  u.UserName,
                                  b.BookName,
                                  br.BorrowDate,
                                  b.BookID,
                                  '未归还' AS 借阅状态
                              FROM T_Borrow br
                              JOIN T_user u ON br.UserID=u.UserID
                              JOIN T_Book b ON br.BookID=b.BookID
                              WHERE br.UserID=@UserID AND br.ReturnDate IS NULL";

                DataTable dt = SqlHelper.ExecuteDataTable(sql, new SqlParameter[] { new SqlParameter("@UserID", userId) });

                // 清空旧数据+绑定新数据
                dgvUnReturnedBooks.DataSource = null;
                dgvUnReturnedBooks.Columns.Clear();
                dgvUnReturnedBooks.DataSource = dt;

                // 优化列显示（管理员视角）
                if (dgvUnReturnedBooks.Columns.Contains("BorrowID")) dgvUnReturnedBooks.Columns["BorrowID"].Width = 80;
                if (dgvUnReturnedBooks.Columns.Contains("UserName")) dgvUnReturnedBooks.Columns["UserName"].Width = 100;
                if (dgvUnReturnedBooks.Columns["BookName"] != null) dgvUnReturnedBooks.Columns["BookName"].Width = 150;
                if (dgvUnReturnedBooks.Columns["BorrowDate"] != null) dgvUnReturnedBooks.Columns["BorrowDate"].Width = 180;
                // 隐藏冗余列（BookID仅用于逻辑，不显示）
                if (dgvUnReturnedBooks.Columns.Contains("BookID")) dgvUnReturnedBooks.Columns["BookID"].Visible = false;

                if (dt.Rows.Count == 0)
                {
                    lblTip.Text = $"【管理员提示】用户{cboReturnUser.Text}暂无未归还图书！";
                    lblTip.ForeColor = Color.Black;
                    btnDoReturn.Enabled = false; // 无记录时禁用还书按钮
                }
                else
                {
                    lblTip.Text = $"【管理员提示】共查询到{dt.Rows.Count}本未归还图书，请选择一行办理还书";
                    lblTip.ForeColor = Color.Black;
                    btnDoReturn.Enabled = true; // 有记录时启用按钮
                }
            }
            catch (Exception ex)
            {
                lblTip.Text = $"加载未归还记录失败：{ex.Message}";
                lblTip.ForeColor = Color.Red;
                btnDoReturn.Enabled = false;
            }
        }

        private void btnDoReturn_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. 校验：是否选中一行记录
                if (dgvUnReturnedBooks.SelectedRows.Count == 0)
                {
                    lblTip.Text = "【管理员提示】请先选中一行未归还记录！";
                    lblTip.ForeColor = Color.Red;
                    return;
                }

                // 2. 获取选中记录的关键信息
                int borrowId = Convert.ToInt32(dgvUnReturnedBooks.SelectedRows[0].Cells["BorrowID"].Value);
                int bookId = Convert.ToInt32(dgvUnReturnedBooks.SelectedRows[0].Cells["BookID"].Value);
                string bookName = dgvUnReturnedBooks.SelectedRows[0].Cells["BookName"].Value.ToString();
                string userName = dgvUnReturnedBooks.SelectedRows[0].Cells["UserName"].Value.ToString();

                // 3. 校验：该记录是否已归还（补充SqlConnection参数）
                string checkSql = "SELECT COUNT(*) FROM T_Borrow WHERE BorrowID=@BorrowID AND ReturnDate IS NULL";
                int unReturnedCount = 0;
                // 局部创建SqlConnection，按方法签名传参
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    unReturnedCount = Convert.ToInt32(
                        SqlHelper.ExecuteScalar(conn, CommandType.Text, checkSql, new SqlParameter[] { new SqlParameter("@BorrowID", borrowId) })
                    );
                }

                // 4. 执行还书操作：①更新归还日期 ②恢复图书库存（+1）
                // 4.1 更新T_Borrow的ReturnDate为当前时间（补充SqlConnection参数）
                string updateReturnSql = "UPDATE T_Borrow SET ReturnDate=GETDATE() WHERE BorrowID=@BorrowID";
                int updateResult = 0;
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    updateResult = SqlHelper.ExecuteNonQuery(
                        conn, CommandType.Text, updateReturnSql, new SqlParameter[] { new SqlParameter("@BorrowID", borrowId) }
                    );
                }

                // 4.2 更新T_Book的库存（Stock+1）（补充SqlConnection参数）
                string updateStockSql = "UPDATE T_Book SET Stock=Stock+1 WHERE BookID=@BookID";
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    SqlHelper.ExecuteNonQuery(
                        conn, CommandType.Text, updateStockSql, new SqlParameter[] { new SqlParameter("@BookID", bookId) }
                    );
                }

                // 5. 管理员专属提示（详细且友好）
                if (updateResult > 0)
                {
                    lblTip.Text = $"【管理员操作成功】已为用户{userName}办理《{bookName}》还书，库存已恢复！";
                    lblTip.ForeColor = Color.Green;
                    // 重新加载未归还记录（刷新表格）
                    btnLoadUnReturned_Click(null, null);
                }
                else
                {
                    lblTip.Text = "【管理员操作失败】还书记录更新失败，请重试！";
                    lblTip.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblTip.Text = $"【管理员操作异常】还书出错：{ex.Message}";
                lblTip.ForeColor = Color.Red;
            }
        }
    }
}
