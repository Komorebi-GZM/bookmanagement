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
    public partial class FmInLibraryBookQuery : Form
    {
        public FmInLibraryBookQuery()
        {
            InitializeComponent();
        }

        private void FmInLibraryBookQuery_Load(object sender, EventArgs e)
        {
            LoadInLibraryBooks();
        }
        private void LoadInLibraryBooks()
        {
            try
            {
                string sql = @"SELECT 
                                  b.BookID,
                                  b.BookName,
                                  bt.TypeName AS 图书类别,
                                  b.Author,
                                  b.Stock AS 剩余库存,
                                  (SELECT COUNT(*) FROM T_Borrow WHERE BookID=b.BookID AND ReturnDate IS NULL) AS 已借出数量,
                                  CASE WHEN b.Stock>0 THEN '在馆' ELSE '已借出' END AS 在馆状态
                              FROM T_Book b
                              JOIN T_BookType bt ON b.TypeID=bt.TypeID";

                // 调用SqlHelper获取数据（复用之前的SqlHelper逻辑）
                DataTable dt = SqlHelper.ExecuteDataTable(sql, new SqlParameter[0]);
                dgvInLibraryBooks.DataSource = dt;

                // 优化列宽（和之前一致）
                if (dgvInLibraryBooks.Columns.Contains("BookID"))
                    dgvInLibraryBooks.Columns["BookID"].Width = 80;
                if (dgvInLibraryBooks.Columns.Contains("BookName"))
                    dgvInLibraryBooks.Columns["BookName"].Width = 150;
                if (dgvInLibraryBooks.Columns.Contains("图书类别"))
                    dgvInLibraryBooks.Columns["图书类别"].Width = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载在馆图书失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInLibraryBooks();
        }
    }
}
