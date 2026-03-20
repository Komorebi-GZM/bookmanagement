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
    public partial class FmUserManager : Form
    {
        public FmUserManager()
        {
            InitializeComponent();
        }

        private void FmReaderManager_Load(object sender, EventArgs e)
        {
            LoadReaderList();
     
        }
        /// <summary>
        /// 加载T_user表的读者数据
        /// </summary>
        private void LoadReaderList()
        {
            try
            {
                string sql = @"SELECT UserID AS 读者ID, 
                              UserName AS 读者姓名, 
                              Sex AS 性别, 
                              IDCard AS 身份证号, 
                              Tel AS 手机号,
                              Used AS 状态
                       FROM T_user";

                // 仅传递SQL字符串 + 空参数数组（匹配你的SqlHelper方法签名）
                DataTable dt = SqlHelper.ExecuteDataTable(
                    sql,                   // 参数1：SQL语句字符串
                    new SqlParameter[0]    // 参数2：空参数数组（替代null）
                );
                dgvReaders.DataSource = dt;

                // 优化列显示
                dgvReaders.Columns["状态"].HeaderText = "账号状态";
                dgvReaders.Columns["状态"].DefaultCellStyle.Format = "0=可用;1=禁用";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载读者列表失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string key = txtSearchKey.Text.Trim();
            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("请输入读者姓名或ID关键词！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadReaderList();
                return;
            }

            try
            {
                string sql = @"SELECT UserID AS 读者ID, 
                              UserName AS 读者姓名, 
                              Sex AS 性别, 
                              IDCard AS 身份证号, 
                              Tel AS 手机号,
                              Used AS 状态
                       FROM T_user
                       WHERE UserName LIKE @Key OR UserID LIKE @Key";

                // 直接传SQL + 参数数组（匹配你的SqlHelper签名）
                DataTable dt = SqlHelper.ExecuteDataTable(
                    sql,
                    new SqlParameter("@Key", "%" + key + "%")
                );
                dgvReaders.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("未找到匹配的读者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"搜索失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #region 辅助校验方法（解决“不存在名称”错误）
        private bool IsValidUserID(string userIDStr)
        {
            // 校验读者ID：9位纯数字
            return !string.IsNullOrEmpty(userIDStr.Trim()) && long.TryParse(userIDStr.Trim(), out _) && userIDStr.Trim().Length == 9;
        }

        private bool IsValidPassword(string password)
        {
            // 校验密码：6位纯数字
            return !string.IsNullOrEmpty(password.Trim()) && long.TryParse(password.Trim(), out _) && password.Trim().Length == 6;
        }

        private bool IsValidTel(string tel)
        {
            // 校验手机号：11位纯数字
            return !string.IsNullOrEmpty(tel.Trim()) && long.TryParse(tel.Trim(), out _) && tel.Trim().Length == 11;
        }
        #endregion
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. 读者ID（9位数字）
                string userIDStr = Interaction.InputBox(
                    "请输入9位数字格式的读者ID（示例：100000001）：",
                    "添加读者-ID",
                    "100000001"
                );
                if (userIDStr == null || !IsValidUserID(userIDStr))
                {
                    MessageBox.Show("读者ID格式错误！\n要求：9位纯数字", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                long userID = Convert.ToInt64(userIDStr.Trim());


                // 2. 读者姓名（2-10字符）
                string userName = Interaction.InputBox(
                    "请输入读者姓名（2-10个字符）：",
                    "添加读者-姓名"
                );
                if (userName == null || string.IsNullOrEmpty(userName.Trim()) || userName.Trim().Length < 2 || userName.Trim().Length > 10)
                {
                    MessageBox.Show("读者姓名格式错误！\n要求：2-10个字符", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                userName = userName.Trim();


                // 3. 密码（6位数字）
                string password = Interaction.InputBox(
                    "请输入6位数字格式的密码（示例：123456）：",
                    "添加读者-密码",
                    "123456"
                );
                if (password == null || !IsValidPassword(password))
                {
                    MessageBox.Show("密码格式错误！\n要求：6位纯数字", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // 4. 性别（男/女）
                string sex = Interaction.InputBox(
                    "请输入性别（男/女）：",
                    "添加读者-性别",
                    "男"
                );
                if (sex == null || (sex.Trim() != "男" && sex.Trim() != "女"))
                {
                    MessageBox.Show("性别格式错误！\n要求：只能输入“男”或“女”", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                sex = sex.Trim();


                // 5. 身份证号（18位）
                string idCard = Interaction.InputBox(
                    "请输入18位身份证号：",
                    "添加读者-身份证号"
                );
                if (idCard == null || idCard.Trim().Length != 18)
                {
                    MessageBox.Show("身份证号格式错误！\n要求：18位字符", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                idCard = idCard.Trim();


                // 6. 手机号（11位数字）
                string tel = Interaction.InputBox(
                    "请输入11位数字格式的手机号：",
                    "添加读者-手机号"
                );
                if (tel == null || !IsValidTel(tel))
                {
                    MessageBox.Show("手机号格式错误！\n要求：11位纯数字", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                tel = tel.Trim();

                // 7. 校验读者ID唯一性（匹配你的SqlHelper重载）
                // 手动创建数据库连接
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open(); // 打开连接

                    string checkSql = "SELECT COUNT(*) FROM T_user WHERE UserID=@UserID";
                    // 按重载传参：SqlConnection → CommandType → SQL语句 → 参数数组
                    object count = SqlHelper.ExecuteScalar(
                        conn,                  // 参数1：SqlConnection对象
                        CommandType.Text,      // 参数2：命令类型
                        checkSql,              // 参数3：SQL语句
                        new SqlParameter("@UserID", userID) // 参数4：参数数组
                    );

                    if (Convert.ToInt32(count) > 0)
                    {
                        MessageBox.Show($"读者ID {userID} 已存在！", "添加失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Close();
                        return;
                    }


                    // 8. 插入T_user表（匹配你的SqlHelper重载）
                    string insertSql = @"INSERT INTO T_user (UserID, UserName, Password, Sex, IDCard, Tel, Used)
                                VALUES (@UserID, @UserName, @Password, @Sex, @IDCard, @Tel, 1)";

                    // 按重载传参：SqlConnection → CommandType → SQL语句 → 参数数组
                    int rows = SqlHelper.ExecuteNonQuery(
                        conn,                  // 参数1：SqlConnection对象
                        CommandType.Text,      // 参数2：命令类型
                        insertSql,             // 参数3：SQL语句
                        new SqlParameter("@UserID", userID),
                        new SqlParameter("@UserName", userName),
                        new SqlParameter("@Password", password),
                        new SqlParameter("@Sex", sex),
                        new SqlParameter("@IDCard", idCard),
                        new SqlParameter("@Tel", tel)
                    );


                    if (rows > 0)
                    {
                        MessageBox.Show($"读者「{userName}」添加成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadReaderList();
                        txtSearchKey.Clear();
                    }
                    else
                    {
                        MessageBox.Show("读者添加失败！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    conn.Close(); // 关闭连接
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dgvReaders.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中要修改的读者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 获取原有数据
                long userID = Convert.ToInt64(dgvReaders.SelectedRows[0].Cells["读者ID"].Value);
                string oldUserName = dgvReaders.SelectedRows[0].Cells["读者姓名"].Value.ToString();
                string oldSex = dgvReaders.SelectedRows[0].Cells["性别"].Value.ToString();
                string oldIDCard = dgvReaders.SelectedRows[0].Cells["身份证号"].Value.ToString();
                string oldTel = dgvReaders.SelectedRows[0].Cells["手机号"].Value.ToString();


                // 1. 新姓名
                string newUserName = Interaction.InputBox(
                    $"请输入新姓名（原姓名：{oldUserName}）：",
                    "修改读者-姓名",
                    oldUserName
                );
                if (newUserName == null || string.IsNullOrEmpty(newUserName.Trim()) || newUserName.Trim().Length < 2 || newUserName.Trim().Length > 10)
                {
                    MessageBox.Show("姓名格式错误！\n要求：2-10个字符", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                newUserName = newUserName.Trim();


                // 2. 新性别
                string newSex = Interaction.InputBox(
                    $"请输入新性别（原性别：{oldSex}）：",
                    "修改读者-性别",
                    oldSex
                );
                if (newSex == null || (newSex.Trim() != "男" && newSex.Trim() != "女"))
                {
                    MessageBox.Show("性别格式错误！\n要求：只能输入“男”或“女”", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                newSex = newSex.Trim();


                // 3. 新身份证号
                string newIDCard = Interaction.InputBox(
                    $"请输入新身份证号（原号：{oldIDCard}）：",
                    "修改读者-身份证号",
                    oldIDCard
                );
                if (newIDCard == null || newIDCard.Trim().Length != 18)
                {
                    MessageBox.Show("身份证号格式错误！\n要求：18位字符", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                newIDCard = newIDCard.Trim();


                // 4. 新手机号
                string newTel = Interaction.InputBox(
                    $"请输入新手机号（原号：{oldTel}）：",
                    "修改读者-手机号",
                    oldTel
                );
                if (newTel == null || !IsValidTel(newTel))
                {
                    MessageBox.Show("手机号格式错误！\n要求：11位纯数字", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                newTel = newTel.Trim();


                // 5. 执行修改（匹配你的SqlHelper重载）
                string updateSql = @"UPDATE T_user
                            SET UserName=@UserName, Sex=@Sex, IDCard=@IDCard, Tel=@Tel
                            WHERE UserID=@UserID";

                // 手动创建数据库连接，按重载传参
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open(); // 打开连接

                    // 按重载传参：SqlConnection → CommandType → SQL语句 → 参数数组
                    int rows = SqlHelper.ExecuteNonQuery(
                        conn,                  // 参数1：SqlConnection对象
                        CommandType.Text,      // 参数2：命令类型
                        updateSql,             // 参数3：SQL语句
                        new SqlParameter("@UserName", newUserName),
                        new SqlParameter("@Sex", newSex),
                        new SqlParameter("@IDCard", newIDCard),
                        new SqlParameter("@Tel", newTel),
                        new SqlParameter("@UserID", userID)
                    );


                    if (rows > 0)
                    {
                        MessageBox.Show("读者信息修改成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadReaderList();
                        txtSearchKey.Clear();
                    }
                    else
                    {
                        MessageBox.Show("读者信息修改失败！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dgvReaders.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选中要删除的读者！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "删除读者会导致其借阅记录失效！是否确认删除？",
                "删除确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirm != DialogResult.Yes) return;

            try
            {
                long userID = Convert.ToInt64(dgvReaders.SelectedRows[0].Cells["读者ID"].Value);
                string userName = dgvReaders.SelectedRows[0].Cells["读者姓名"].Value.ToString();


                // 手动创建数据库连接，按重载传参
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open(); // 打开连接


                    // 校验是否有借阅记录（匹配你的SqlHelper.ExecuteScalar重载）
                    string checkBorrowSql = "SELECT COUNT(*) FROM T_Borrow WHERE UserID=@UserID";
                    object borrowCount = SqlHelper.ExecuteScalar(
                        conn,                  // 参数1：SqlConnection对象
                        CommandType.Text,      // 参数2：命令类型
                        checkBorrowSql,        // 参数3：SQL语句
                        new SqlParameter("@UserID", userID) // 参数4：参数数组
                    );

                    if (Convert.ToInt32(borrowCount) > 0)
                    {
                        MessageBox.Show($"读者「{userName}」存在未归还的借阅记录，无法删除！", "删除失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Close();
                        return;
                    }


                    // 执行删除（匹配你的SqlHelper.ExecuteNonQuery重载）
                    string deleteSql = "DELETE FROM T_user WHERE UserID=@UserID";
                    int rows = SqlHelper.ExecuteNonQuery(
                        conn,                  // 参数1：SqlConnection对象
                        CommandType.Text,      // 参数2：命令类型
                        deleteSql,             // 参数3：SQL语句
                        new SqlParameter("@UserID", userID) // 参数4：参数数组
                    );


                    if (rows > 0)
                    {
                        MessageBox.Show($"读者「{userName}」删除成功！", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadReaderList();
                        txtSearchKey.Clear();
                    }
                    else
                    {
                        MessageBox.Show("读者删除失败！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtSearchKey.Clear();
            LoadReaderList();
            MessageBox.Show("读者列表已刷新！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
