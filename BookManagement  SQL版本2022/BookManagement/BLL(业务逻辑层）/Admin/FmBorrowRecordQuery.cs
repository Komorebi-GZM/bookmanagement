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
    public partial class FmBorrowRecordQuery : Form
    {
        public FmBorrowRecordQuery()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 获取选中的UserID（转换为int，0代表“全部用户”）
            int selectedUserId = Convert.ToInt32(cboQueryUser.SelectedValue);
            LoadBorrowRecords(selectedUserId);
        }
        private void btnQueryAll_Click(object sender, EventArgs e)
        {
            cboQueryUser.SelectedIndex = 0; // 切回“全部用户”
            LoadBorrowRecords(0); 
        }

        private void FmBorrowRecordQuery_Load(object sender, EventArgs e)
        {
            LoadQueryUserList(); // 加载用户列表到筛选下拉框
            LoadBorrowRecords(); // 默认加载所有借阅记录
        }
        // 步骤1：加载用户列表到筛选下拉框
        private void LoadQueryUserList()
        {
            try
            {
                string sql = "SELECT UserID, UserName FROM T_User WHERE Used=1";
                DataTable dt = SqlHelper.ExecuteDataTable(sql, new SqlParameter[0]);

                // 新增“全部用户”选项（UserID用0，避免Int32类型错误）
                DataRow dr = dt.NewRow();
                dr["UserID"] = 0; // 改为数值0，匹配Int32类型
                dr["UserName"] = "全部用户";
                dt.Rows.InsertAt(dr, 0);

                // 下拉框绑定（显示姓名，值存UserID）
                cboQueryUser.DataSource = dt;
                cboQueryUser.DisplayMember = "UserName";
                cboQueryUser.ValueMember = "UserID";
                cboQueryUser.SelectedIndex = 0; // 默认选中“全部用户”
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载用户列表失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 步骤2：加载借阅记录
        private void LoadBorrowRecords(int userId = 0) // 参数类型改为int
        {
            try
            {
                string sql;
                SqlParameter[] parameters;

                // 0=查所有；非0=按用户ID筛选（保留原逻辑）
                if (userId == 0)
                {
                    sql = @"SELECT 
                      br.BorrowID,
                      u.UserID,
                      u.UserName,
                      b.BookName,
                      br.BorrowDate,
                      br.ReturnDate,
                      CASE WHEN br.ReturnDate IS NULL THEN '未归还' ELSE '已归还' END AS 借阅状态
                  FROM T_Borrow br
                  JOIN T_User u ON br.UserID=u.UserID
                  JOIN T_Book b ON br.BookID=b.BookID";
                    parameters = new SqlParameter[0];
                }
                else
                {
                    sql = @"SELECT 
                      br.BorrowID,
                      u.UserID,
                      u.UserName,
                      b.BookName,
                      br.BorrowDate,
                      br.ReturnDate,
                      CASE WHEN br.ReturnDate IS NULL THEN '未归还' ELSE '已归还' END AS 借阅状态
                  FROM T_Borrow br
                  JOIN T_User u ON br.UserID=u.UserID
                  JOIN T_Book b ON br.BookID=b.BookID
                  WHERE u.UserID=@UserID";
                    parameters = new SqlParameter[] { new SqlParameter("@UserID", userId) };
                }

                DataTable dt = SqlHelper.ExecuteDataTable(sql, parameters);

                // 关键修正：绑定新数据前，强制清空DataGridView的旧数据
                dgvBorrowRecords.DataSource = null; // 解除旧数据源绑定
                if (dgvBorrowRecords.Rows.Count > 0)
                {
                    dgvBorrowRecords.Rows.Clear(); // 清空旧行
                }

                // 绑定新数据
                dgvBorrowRecords.DataSource = dt;

                // 列显示优化（保留原逻辑）
                if (dgvBorrowRecords.Columns.Contains("ReturnDate"))
                {
                    dgvBorrowRecords.Columns["ReturnDate"].DefaultCellStyle.NullValue = "未归还";
                    dgvBorrowRecords.Columns["ReturnDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                }
                if (dgvBorrowRecords.Columns.Contains("BorrowID")) dgvBorrowRecords.Columns["BorrowID"].Width = 80;
                if (dgvBorrowRecords.Columns.Contains("UserID")) dgvBorrowRecords.Columns["UserID"].Width = 100;
                if (dgvBorrowRecords.Columns.Contains("UserName")) dgvBorrowRecords.Columns["UserName"].Width = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询借阅记录失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
