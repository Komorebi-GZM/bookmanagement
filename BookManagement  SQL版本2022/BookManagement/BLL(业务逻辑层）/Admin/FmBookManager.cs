using Lms.DataAccess;
using Microsoft.VisualBasic;
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
    public partial class FmBookManager : Form
    {
        private string connStr;
        public FmBookManager()
        {
            InitializeComponent();
        }

        private void FmBookManager_Load(object sender, EventArgs e)
        {
            LoadBookList(); // 自定义方法：加载图书列表
        }
        /// <summary>
        /// 加载T_Book表所有图书到DataGridView
        /// </summary>
        private void LoadBookList()
        {
            try
            {
                // 调用SqlHelper的方法获取图书列表
                string sql = "SELECT BookID AS 图书ID, BookName AS 书名, TypeID AS 类别ID, Author AS 作者, Stock AS 库存 FROM T_Book";
                DataTable dt = SqlHelper.ExecuteDataTable(sql);

                dgvBooks.DataSource = dt;
                dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载图书列表失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string key = txtSearchKey.Text.Trim();
            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("请输入书名/作者关键词！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBookList(); // 无关键词时刷新全量列表
                return;
            }

            try
            {
                // 1. 模糊查询SQL（用@Key作为参数）
                string sql = @"SELECT BookID AS 图书ID, BookName AS 书名, TypeID AS 类别ID, Author AS 作者, Stock AS 库存 
                      FROM T_Book 
                      WHERE BookName LIKE @Key OR Author LIKE @Key";

                // 2. 调用SqlHelper的方法（自动使用已初始化的连接字符串）
                DataTable dt = SqlHelper.ExecuteDataTable(
                    sql,
                    new SqlParameter("@Key", "%" + key + "%") // 传递模糊查询参数
                );

                // 3. 绑定到DataGridView
                dgvBooks.DataSource = dt;

                // 空结果提示
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到匹配的图书！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"搜索失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
     
            string bookName = Interaction.InputBox("请输入书名：", "添加图书");
            if (string.IsNullOrEmpty(bookName)) return;

            string typeIDStr = Interaction.InputBox("请输入类别ID：", "添加图书");
            if (!int.TryParse(typeIDStr, out int typeID))
            {
                MessageBox.Show("类别ID必须是数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string author = Interaction.InputBox("请输入作者：", "添加图书");
            if (string.IsNullOrEmpty(author)) return;

            string stockStr = Interaction.InputBox("请输入库存数（≥0）：", "添加图书");
            if (!int.TryParse(stockStr, out int stock) || stock < 0)
            {
                MessageBox.Show("库存必须是≥0的数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 关键修改：先创建SqlConnection对象，再调用正确的重载
            try
            {
                string sql = @"INSERT INTO T_Book (BookName, TypeID, Author, Stock) 
                      VALUES (@BookName, @TypeID, @Author, @Stock)";

                // 1. 用SqlHelper的连接字符串创建SqlConnection
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    // 2. 调用带SqlConnection+CommandType+SQL+参数数组的重载（匹配第98行方法）
                    int rows = SqlHelper.ExecuteNonQuery(
                        conn,                  // 参数1：SqlConnection对象
                        CommandType.Text,      // 参数2：命令类型
                        sql,                   // 参数3：SQL语句
                                               // 参数4：SqlParameter数组（你的SqlHelper支持可变参数）
                        new SqlParameter("@BookName", bookName),
                        new SqlParameter("@TypeID", typeID),
                        new SqlParameter("@Author", author),
                        new SqlParameter("@Stock", stock)
                    );

                    if (rows > 0)
                    {
                        MessageBox.Show("添加图书成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBookList();
                        txtSearchKey.Clear();
                    }
                    else
                    {
                        MessageBox.Show("添加图书失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            // 第一步：校验是否选中要修改的图书行
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中要修改的图书！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 第二步：获取选中行的原有数据（匹配DataGridView列名）
                int bookID = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["图书ID"].Value);
                string oldBookName = dgvBooks.SelectedRows[0].Cells["书名"].Value.ToString();
                int oldTypeID = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["类别ID"].Value);
                string oldAuthor = dgvBooks.SelectedRows[0].Cells["作者"].Value.ToString();
                int oldStock = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["库存"].Value);

                // 第三步：弹窗输入新数据（默认填充原有值，降低输入成本）
                string newBookName = Microsoft.VisualBasic.Interaction.InputBox(
                    $"请输入新书名（原书名：{oldBookName}）：",
                    "修改图书-书名",
                    oldBookName
                );
                // 取消输入/空值校验
                if (string.IsNullOrEmpty(newBookName))
                {
                    MessageBox.Show("书名不能为空！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string newTypeIDStr = Microsoft.VisualBasic.Interaction.InputBox(
                    $"请输入新类别ID（原类别ID：{oldTypeID}）：",
                    "修改图书-类别ID",
                    oldTypeID.ToString()
                );
                // 类别ID数字校验
                if (!int.TryParse(newTypeIDStr, out int newTypeID))
                {
                    MessageBox.Show("类别ID必须是整数！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string newAuthor = Microsoft.VisualBasic.Interaction.InputBox(
                    $"请输入新作者（原作者：{oldAuthor}）：",
                    "修改图书-作者",
                    oldAuthor
                );
                // 作者空值校验
                if (string.IsNullOrEmpty(newAuthor))
                {
                    MessageBox.Show("作者不能为空！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string newStockStr = Microsoft.VisualBasic.Interaction.InputBox(
                    $"请输入新库存（原库存：{oldStock}）：",
                    "修改图书-库存",
                    oldStock.ToString()
                );
                // 库存数字+非负校验
                if (!int.TryParse(newStockStr, out int newStock) || newStock < 0)
                {
                    MessageBox.Show("库存必须是≥0的整数！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 第四步：构建修改SQL语句
                string sql = @"UPDATE T_Book 
                      SET BookName = @NewBookName, 
                          TypeID = @NewTypeID, 
                          Author = @NewAuthor, 
                          Stock = @NewStock 
                      WHERE BookID = @BookID";

                // 第五步：创建SqlConnection（复用SqlHelper的连接字符串）
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    // 调用SqlHelper正确重载：SqlConnection → CommandType → SQL → 参数数组
                    int affectedRows = SqlHelper.ExecuteNonQuery(
                        conn,                  // 参数1：SqlConnection对象
                        CommandType.Text,      // 参数2：命令类型（普通SQL）
                        sql,                   // 参数3：修改SQL语句
                                               // 参数4：SqlParameter数组（按SQL参数顺序传递）
                        new SqlParameter("@NewBookName", newBookName),
                        new SqlParameter("@NewTypeID", newTypeID),
                        new SqlParameter("@NewAuthor", newAuthor),
                        new SqlParameter("@NewStock", newStock),
                        new SqlParameter("@BookID", bookID)
                    );

                    // 第六步：处理修改结果
                    if (affectedRows > 0)
                    {
                        MessageBox.Show("图书信息修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBookList();       // 刷新列表，展示最新数据
                        txtSearchKey.Clear(); // 清空搜索框，避免残留关键词干扰
                    }
                    else
                    {
                        MessageBox.Show("修改失败：未找到对应图书或数据无变更！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("数据格式错误：图书ID/类别ID/库存必须是数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"数据库操作失败：{sqlEx.Message}", "SQL错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改图书异常：{ex.Message}", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // 第一步：校验是否选中图书行
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中要下架的图书！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 第二步：二次确认（防止误操作）
            DialogResult confirmResult = MessageBox.Show(
                "警告：下架图书后库存将设为0，是否确认执行？",
                "下架确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirmResult != DialogResult.Yes)
            {
                return; // 点击“否”直接退出
            }

            try
            {
                // 第三步：获取选中图书的ID（匹配DataGridView列名“图书ID”）
                int bookID = Convert.ToInt32(dgvBooks.SelectedRows[0].Cells["图书ID"].Value);

                // 第四步：构建下架SQL（库存设为0）
                string sql = "UPDATE T_Book SET Stock=0 WHERE BookID=@BookID";

                // 第五步：创建SqlConnection（复用SqlHelper的连接字符串）
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    // 调用SqlHelper正确的重载：SqlConnection + CommandType + SQL + 参数数组
                    int affectedRows = SqlHelper.ExecuteNonQuery(
                        conn,                  // 参数1：SqlConnection对象
                        CommandType.Text,      // 参数2：命令类型（普通SQL语句）
                        sql,                   // 参数3：下架SQL语句
                        new SqlParameter("@BookID", bookID) // 参数4：图书ID参数
                    );

                    // 第六步：判断执行结果
                    if (affectedRows > 0)
                    {
                        MessageBox.Show("图书下架成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBookList(); // 下架后自动刷新列表
                        txtSearchKey.Clear(); // 清空搜索框，显示全量数据
                    }
                    else
                    {
                        MessageBox.Show("下架失败：未找到对应图书或无数据变更！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("数据格式错误：图书ID必须是数字！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"数据库操作失败：{sqlEx.Message}", "SQL错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"下架操作异常：{ex.Message}", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // 第一步：清空搜索框（避免残留关键词影响刷新结果）
                txtSearchKey.Clear();

                // 第二步：重新加载全量图书列表
                LoadBookList();

                // 第三步：提示刷新成功
                MessageBox.Show("图书列表已刷新完成！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"刷新列表失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
