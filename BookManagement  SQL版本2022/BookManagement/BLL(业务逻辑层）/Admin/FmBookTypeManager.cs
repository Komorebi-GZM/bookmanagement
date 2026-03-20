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
    public partial class FmBookTypeManager : Form
    {
        private string connStr = SqlHelper.connectionString;
        public FmBookTypeManager()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 校验是否选中类别行
            if (dgvBookTypes.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中要删除的类别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 二次确认
            DialogResult confirm = MessageBox.Show(
                "删除类别会导致关联该类别的图书失去分类！是否确认删除？",
                "删除确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirm != DialogResult.Yes) return;

            // 获取类别ID
            int typeID = Convert.ToInt32(dgvBookTypes.SelectedRows[0].Cells["类别ID"].Value);

            try
            {
                // 第一步：校验是否有图书关联该类别（调用ExecuteScalar也需匹配重载）
                string checkSql = "SELECT COUNT(*) FROM T_Book WHERE TypeID=@TypeID";
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open();
                    // 调用ExecuteScalar的对应重载
                    object count = SqlHelper.ExecuteScalar(
                        conn,
                        CommandType.Text,
                        checkSql,
                        new SqlParameter("@TypeID", typeID)
                    );

                    if (Convert.ToInt32(count) > 0)
                    {
                        MessageBox.Show("该类别下存在图书，无法删除！", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // 第二步：删除类别（调用ExecuteNonQuery）
                    string deleteSql = "DELETE FROM T_BookType WHERE TypeID=@TypeID";
                    int rows = SqlHelper.ExecuteNonQuery(
                        conn,
                        CommandType.Text,
                        deleteSql,
                        new SqlParameter("@TypeID", typeID)
                    );

                    if (rows > 0)
                    {
                        MessageBox.Show("类别删除成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBookTypeList();
                        txtSearchKey.Clear();
                    }
                    else
                    {
                        MessageBox.Show("类别删除失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 校验是否选中类别行
            if (dgvBookTypes.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中要修改的类别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 获取原有数据
            int typeID = Convert.ToInt32(dgvBookTypes.SelectedRows[0].Cells["类别ID"].Value);
            string oldTypeName = dgvBookTypes.SelectedRows[0].Cells["类别名称"].Value.ToString();

            // 输入新类别名称
            string newTypeName = Interaction.InputBox(
                $"请输入新类别名称（原名称：{oldTypeName}）：",
                "修改图书类别",
                oldTypeName
            );
            if (string.IsNullOrEmpty(newTypeName.Trim()))
            {
                MessageBox.Show("类别名称不能为空！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string sql = "UPDATE T_BookType SET TypeName=@TypeName WHERE TypeID=@TypeID";

                // 核心修正：先创建SqlConnection，按重载顺序传参
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open();
                    int rows = SqlHelper.ExecuteNonQuery(
                        conn,                  // 参数1：SqlConnection对象
                        CommandType.Text,      // 参数2：命令类型
                        sql,                   // 参数3：SQL语句
                        new SqlParameter("@TypeName", newTypeName.Trim()), // 参数4：参数数组
                        new SqlParameter("@TypeID", typeID)
                    );

                    if (rows > 0)
                    {
                        MessageBox.Show("类别修改成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBookTypeList();
                        txtSearchKey.Clear();
                    }
                    else
                    {
                        MessageBox.Show("类别修改失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FmBookTypeManager_Load(object sender, EventArgs e)
        {
            LoadBookTypeList();
        }
        private void LoadBookTypeList()
        {
            try
            {
                string sql = "SELECT TypeID AS 类别ID, TypeName AS 类别名称 FROM T_BookType";

                // 核心修正：仅传SQL语句 + 空参数数组（匹配SqlHelper第25行签名）
                DataTable dt = SqlHelper.ExecuteDataTable(
                    sql,                   // 参数1：SQL字符串
                    new SqlParameter[0]    // 参数2：空参数数组（替代null）
                );
                dgvBookTypes.DataSource = dt;
                dgvBookTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载类别列表失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string typeName = Interaction.InputBox("请输入新类别名称：", "添加图书类别");
            if (string.IsNullOrEmpty(typeName.Trim()))
            {
                MessageBox.Show("类别名称不能为空！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string sql = "INSERT INTO T_BookType (TypeName) VALUES (@TypeName)";

                // 关键：先创建SqlConnection，再调用匹配的重载（行98）
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open();
                    // 参数顺序：SqlConnection → CommandType → SQL语句 → 参数数组
                    int rows = SqlHelper.ExecuteNonQuery(
                        conn,                  // 参数1：SqlConnection对象
                        CommandType.Text,      // 参数2：命令类型（普通SQL）
                        sql,                   // 参数3：SQL语句
                        new SqlParameter("@TypeName", typeName.Trim()) // 参数4：参数数组
                    );

                    if (rows > 0)
                    {
                        MessageBox.Show("类别添加成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBookTypeList();
                        txtSearchKey.Clear();
                    }
                    else
                    {
                        MessageBox.Show("类别添加失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"数据库错误：{sqlEx.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchKey.Clear();
            LoadBookTypeList();
            MessageBox.Show("类别列表已刷新！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string key = txtSearchKey.Text.Trim();
            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("请输入类别名称关键词！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBookTypeList();
                return;
            }

            try
            {
                string sql = @"SELECT TypeID AS 类别ID, TypeName AS 类别名称 
                              FROM T_BookType 
                              WHERE TypeName LIKE @Key";

                // 调用：传SQL + 参数数组
                DataTable dt = SqlHelper.ExecuteDataTable(
                    sql,
                    new SqlParameter("@Key", "%" + key + "%")
                );
                dgvBookTypes.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到匹配的类别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"搜索失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
