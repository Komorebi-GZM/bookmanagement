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
    public partial class FmUserTypeManage : Form
    {
        public FmUserTypeManage()
        {
            InitializeComponent();
        }

        private void FmUserTypeManage_Load(object sender, EventArgs e)
        {
            LoadUserTypeList();
        }
        private void LoadUserTypeList()
        {
            try
            {
                string sql = "SELECT UserTypeID AS 类别ID, UserTypeName AS 类别名称 FROM T_UserType";
                // 直接调用SqlHelper的ExecuteDataTable，仅传SQL和空参数数组
                DataTable dt = SqlHelper.ExecuteDataTable(
                    sql,
                    new SqlParameter[0] // 无参数时传空数组
                );
                dgvUserTypes.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载读者类别失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string key = txtSearchKey.Text.Trim();
            try
            {
                // 1. 用参数化查询替代字符串拼接，避免SQL注入
                string sql = "SELECT UserTypeID AS 类别ID, UserTypeName AS 类别名称 FROM T_UserType WHERE UserTypeName LIKE @Key";

                // 2. 构造参数（模糊查询需拼接%）
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@Key", $"%{key}%")
                };

                // 3. 调用SqlHelper.ExecuteDataTable，仅传SQL和参数数组
                DataTable dt = SqlHelper.ExecuteDataTable(
                    sql,
                    parameters
                );
                dgvUserTypes.DataSource = dt;

                // 无搜索结果提示
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show($"未找到包含「{key}」的读者类别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"搜索失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string typeName = Interaction.InputBox("请输入读者类别名称（如“学生”）：", "增加读者类别");
            typeName = typeName.Trim();

            // 基础校验
            if (string.IsNullOrEmpty(typeName))
            {
                MessageBox.Show("类别名称不能为空！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (typeName.Length > 20)
            {
                MessageBox.Show("类别名称不能超过20个字符！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 1. 创建数据库连接（匹配SqlHelper的参数要求）
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open(); // 打开连接

                    // 步骤1：唯一性校验（传全SqlHelper所需参数）
                    string checkSql = "SELECT COUNT(*) FROM T_UserType WHERE UserTypeName=@TypeName";
                    SqlParameter[] checkParams = new SqlParameter[] { new SqlParameter("@TypeName", typeName) };
                    object count = SqlHelper.ExecuteScalar(
                        conn,               // 参数1：SqlConnection
                        CommandType.Text,   // 参数2：CommandType（文本SQL）
                        checkSql,           // 参数3：命令文本
                        checkParams         // 参数4：参数数组
                    );

                    if (Convert.ToInt32(count) > 0)
                    {
                        MessageBox.Show($"类别「{typeName}」已存在！", "增加失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Close();
                        return;
                    }

                    // 步骤2：插入新类别（传全SqlHelper所需参数）
                    string insertSql = "INSERT INTO T_UserType (UserTypeName) VALUES (@TypeName)";
                    SqlParameter[] insertParams = new SqlParameter[] { new SqlParameter("@TypeName", typeName) };
                    int rows = SqlHelper.ExecuteNonQuery(
                        conn,               // 参数1：SqlConnection
                        CommandType.Text,   // 参数2：CommandType
                        insertSql,          // 参数3：命令文本
                        insertParams        // 参数4：参数数组
                    );

                    if (rows > 0)
                    {
                        MessageBox.Show($"读者类别「{typeName}」增加成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUserTypeList();
                        txtSearchKey.Clear();
                    }

                    conn.Close(); // 关闭连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"增加失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            // 校验是否选中行
            if (dgvUserTypes.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中要修改的读者类别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 获取选中行数据
            int typeID = Convert.ToInt32(dgvUserTypes.SelectedRows[0].Cells["类别ID"].Value);
            string oldName = dgvUserTypes.SelectedRows[0].Cells["类别名称"].Value.ToString();

            // 弹出输入框获取新名称
            string newName = Interaction.InputBox($"请输入新的类别名称（原名称：{oldName}）：", "修改读者类别", oldName);
            newName = newName.Trim();

            // 基础校验
            if (string.IsNullOrEmpty(newName) || newName.Length > 20)
            {
                MessageBox.Show("类别名称格式错误（1-20个字符）！", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (newName == oldName)
            {
                MessageBox.Show("类别名称未修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 1. 创建并打开数据库连接（匹配SqlHelper的参数要求）
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open();

                    // 步骤1：唯一性校验（补全SqlHelper所需的所有参数）
                    string checkSql = "SELECT COUNT(*) FROM T_UserType WHERE UserTypeName=@NewName AND UserTypeID!=@TypeID";
                    SqlParameter[] checkParams = new SqlParameter[]
                    {
                new SqlParameter("@NewName", newName),
                new SqlParameter("@TypeID", typeID)
                    };
                    object count = SqlHelper.ExecuteScalar(
                        conn,               // 参数1：SqlConnection
                        CommandType.Text,   // 参数2：CommandType（文本SQL）
                        checkSql,           // 参数3：命令文本
                        checkParams         // 参数4：参数数组
                    );

                    if (Convert.ToInt32(count) > 0)
                    {
                        MessageBox.Show($"类别「{newName}」已存在！", "修改失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Close();
                        return;
                    }

                    // 步骤2：更新类别名称（补全SqlHelper所需的所有参数）
                    string updateSql = "UPDATE T_UserType SET UserTypeName=@NewName WHERE UserTypeID=@TypeID";
                    SqlParameter[] updateParams = new SqlParameter[]
                    {
                new SqlParameter("@NewName", newName),
                new SqlParameter("@TypeID", typeID)
                    };
                    int rows = SqlHelper.ExecuteNonQuery(
                        conn,               // 参数1：SqlConnection
                        CommandType.Text,   // 参数2：CommandType
                        updateSql,          // 参数3：命令文本
                        updateParams        // 参数4：参数数组
                    );

                    if (rows > 0)
                    {
                        MessageBox.Show("读者类别修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUserTypeList(); // 刷新列表
                    }

                    conn.Close(); // 关闭连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 校验是否选中行
            if (dgvUserTypes.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中要删除的读者类别！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 获取选中行数据
            int typeID = Convert.ToInt32(dgvUserTypes.SelectedRows[0].Cells["类别ID"].Value);
            string typeName = dgvUserTypes.SelectedRows[0].Cells["类别名称"].Value.ToString();

            // 二次确认删除
            DialogResult confirm = MessageBox.Show(
                $"确定要删除读者类别「{typeName}」吗？\n删除后关联的读者将无法正常分类！",
                "删除确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirm != DialogResult.Yes) return;

            try
            {
                // 1. 创建并打开数据库连接（匹配SqlHelper参数要求）
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open();

                    // 步骤1：校验是否关联读者（补全SqlHelper所需参数）
                    string checkSql = "SELECT COUNT(*) FROM T_user WHERE UserTypeID=@TypeID";
                    SqlParameter[] checkParams = new SqlParameter[] { new SqlParameter("@TypeID", typeID) };
                    object count = SqlHelper.ExecuteScalar(
                        conn,               // 参数1：SqlConnection
                        CommandType.Text,   // 参数2：CommandType（文本SQL）
                        checkSql,           // 参数3：命令文本
                        checkParams         // 参数4：参数数组
                    );

                    if (Convert.ToInt32(count) > 0)
                    {
                        MessageBox.Show($"类别「{typeName}」已关联读者，无法删除！", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Close();
                        return;
                    }

                    // 步骤2：执行删除（补全SqlHelper所需参数）
                    string deleteSql = "DELETE FROM T_UserType WHERE UserTypeID=@TypeID";
                    SqlParameter[] deleteParams = new SqlParameter[] { new SqlParameter("@TypeID", typeID) };
                    int rows = SqlHelper.ExecuteNonQuery(
                        conn,               // 参数1：SqlConnection
                        CommandType.Text,   // 参数2：CommandType
                        deleteSql,          // 参数3：命令文本
                        deleteParams        // 参数4：参数数组
                    );

                    if (rows > 0)
                    {
                        MessageBox.Show($"读者类别「{typeName}」删除成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUserTypeList(); // 刷新列表
                    }

                    conn.Close(); // 关闭连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUserTypeList();
            txtSearchKey.Clear();
            MessageBox.Show("读者类别列表已刷新！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
