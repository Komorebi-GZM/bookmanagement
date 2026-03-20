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
    public partial class FmAdmin : Form
    {
        private string currentAdminID; // 存储当前登录管理员ID
        public FmAdmin()
        {
            InitializeComponent();
        }

        // 带参数构造：接收AdminID（管理员ID）、AdminName（管理员姓名）
        public FmAdmin(string AdminID, string AdminName)
        {
            InitializeComponent();
            currentAdminID = AdminID; // 接收并存储管理员ID
            // 赋值给Label，显示对应字段
            lblAdminID.Text = "管理员ID：" + AdminID;
            lblAdminName.Text = "管理员姓名：" + AdminName;
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tsmiAdminChangePwd_Click(object sender, EventArgs e)
        {
            // 打开管理员修改密码子窗体，并传递当前管理员ID
            FmAdminChangePwd changePwdForm = new FmAdminChangePwd(currentAdminID);
            changePwdForm.ShowDialog(); 
        }

        private void tsmiAdminCancelAccount_Click(object sender, EventArgs e)
        {//此处 “注销账号” 实际为管理员退出登录（删除管理员账号属于高危操作，不建议普通场景开放，仅保留退出逻辑）
            // 1. 弹出确认提示框（适配管理员场景的文案）
            DialogResult result = MessageBox.Show(
                "确定要注销管理员账号并返回登录页吗？(删除管理员账号属于高危操作,暂无权限）",  // 提示文本
                "管理员注销确认",                      // 标题
                MessageBoxButtons.YesNo,               
                MessageBoxIcon.Question                // 图标：
            );

            // 2. 点击“是”则执行注销（退出登录）
            if (result == DialogResult.Yes)
            {
                try
                {
                    // 关闭当前管理员窗体
                    this.Close();
                    // 打开登录窗体
                    FmLog loginForm = new FmLog();
                    loginForm.Show();
                }
                catch (Exception ex)
                {
                    // 异常提示（仅用MessageBox）
                    MessageBox.Show(
                        $"注销失败：{ex.Message}",
                        "错误",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void tsmiAdminBookManager_Click(object sender, EventArgs e)
        {
            // 实例化并打开图书管理窗体（模态显示，需处理完再返回FmAdmin）
            FmBookManager bookForm = new FmBookManager();
            bookForm.ShowDialog();
        }

        private void tsmiAdminBookType_Click(object sender, EventArgs e)
        {
            FmBookTypeManager typeForm = new FmBookTypeManager();
            typeForm.ShowDialog();
        }

        private void tsmiAddAdmin_Click(object sender, EventArgs e)
        {

            try
            {
                // ===================== 1. 输入并校验管理员ID（9位数字） =====================
                string adminIDStr = Interaction.InputBox(
                    "请输入9位数字格式的管理员ID（示例：100000001）：",
                    "添加管理员-ID",
                    "100000001"
                );
                if (adminIDStr == null || !IsValidAdminID(adminIDStr))
                {
                    MessageBox.Show("管理员ID格式错误！\n要求：9位纯数字（如100000001）", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                long adminID = Convert.ToInt64(adminIDStr.Trim());


                // ===================== 2. 输入并校验密码（6位数字） =====================
                string password = Interaction.InputBox(
                    "请输入6位数字格式的管理员密码（示例：123456）：",
                    "添加管理员-密码",
                    "123456"
                );
                if (password == null || !IsValidPassword(password))
                {
                    MessageBox.Show("密码格式错误！\n要求：6位纯数字（如123456）", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // ===================== 3. 输入并校验管理员姓名 =====================
                string adminName = Interaction.InputBox(
                    "请输入管理员姓名（2-10个字符）：",
                    "添加管理员-姓名"
                );
                if (adminName == null || string.IsNullOrEmpty(adminName.Trim()) || adminName.Trim().Length < 2 || adminName.Trim().Length > 10)
                {
                    MessageBox.Show("管理员姓名格式错误！\n要求：2-10个字符（不能为空/过长）", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                adminName = adminName.Trim();


                // ===================== 4. 校验AdminID唯一性 =====================
                // 核心：先创建SqlConnection，按你的重载传参
                using (SqlConnection conn = new SqlConnection(SqlHelper.connectionString))
                {
                    conn.Open(); // 手动打开连接（匹配你的SqlHelper逻辑）

                    // 调用ExecuteScalar：SqlConnection → CommandType → SQL → 参数数组
                    string checkSql = "SELECT COUNT(*) FROM T_Admin WHERE AdminID=@AdminID";
                    object count = SqlHelper.ExecuteScalar(
                        conn,                  // 参数1：SqlConnection对象
                        CommandType.Text,      // 参数2：命令类型
                        checkSql,              // 参数3：SQL语句
                        new SqlParameter("@AdminID", adminID) // 参数4：参数数组
                    );


                    // ===================== 5. 校验ID重复 =====================
                    if (Convert.ToInt32(count) > 0)
                    {
                        MessageBox.Show($"管理员ID {adminID} 已存在！请更换ID后重试", "添加失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        conn.Close();
                        return;
                    }


                    // ===================== 6. 插入数据库 =====================
                    string insertSql = @"INSERT INTO T_Admin (AdminID, Password, AdminName) 
                                VALUES (@AdminID, @Password, @AdminName)";

                    // 调用ExecuteNonQuery：SqlConnection → CommandType → SQL → 参数数组
                    int rows = SqlHelper.ExecuteNonQuery(
                        conn,                  // 参数1：SqlConnection对象
                        CommandType.Text,      // 参数2：命令类型
                        insertSql,             // 参数3：SQL语句
                        new SqlParameter("@AdminID", adminID),
                        new SqlParameter("@Password", password),
                        new SqlParameter("@AdminName", adminName)
                    );


                    // ===================== 7. 结果提示 =====================
                    if (rows > 0)
                    {
                        MessageBox.Show($"✅ 管理员添加成功！\nID：{adminID}\n姓名：{adminName}", "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("❌ 管理员添加失败！\n数据库无数据写入", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    conn.Close(); // 手动关闭连接
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"❌ 数据库错误：{sqlEx.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ 添加异常：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region 辅助校验方法（规范复用）
        /// <summary>
        /// 校验管理员ID：9位纯数字
        /// </summary>
        /// <param name="adminIDStr">输入的ID字符串</param>
        /// <returns>是否符合格式</returns>
        private bool IsValidAdminID(string adminIDStr)
        {
            if (string.IsNullOrEmpty(adminIDStr.Trim())) return false;
            // 纯数字 + 长度9位
            return long.TryParse(adminIDStr.Trim(), out _) && adminIDStr.Trim().Length == 9;
        }

        /// <summary>
        /// 校验管理员密码：6位纯数字
        /// </summary>
        /// <param name="password">输入的密码字符串</param>
        /// <returns>是否符合格式</returns>
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password.Trim())) return false;
            // 纯数字 + 长度6位
            return long.TryParse(password.Trim(), out _) && password.Trim().Length == 6;
        }
        #endregion

        private void tsmiReaderManager_Click(object sender, EventArgs e)
        {
            FmUserManager readerForm = new FmUserManager();
            readerForm.ShowDialog();
        }

        private void 退出登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. 关闭当前管理员主界面
                this.Close();

                // 2. 实例化并显示登录窗体（FmLog）
                FmLog loginForm = new FmLog();
                loginForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"退出登录失败：{ex.Message}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tsmiUserTypeManager_Click(object sender, EventArgs e)
        {
            // 打开读者类别管理窗口（模态显示，禁止操作管理员窗口）
            FmUserTypeManage userTypeForm = new FmUserTypeManage();
            userTypeForm.ShowDialog();
        }

        private void btnAdminBorrowQuery_Click(object sender, EventArgs e)
        {
            
        }

        private void tsmiInLibraryQuery_Click(object sender, EventArgs e)
        {
            FmInLibraryBookQuery inLibraryQueryForm = new FmInLibraryBookQuery();
            inLibraryQueryForm.Show();
        }

        private void tsmiBorrowRecordQuery_Click(object sender, EventArgs e)
        {
            // 打开借阅情况查询新窗口
            FmBorrowRecordQuery borrowRecordForm = new FmBorrowRecordQuery();
            borrowRecordForm.Show();
        }

        private void tsmiBookBorrow_Click(object sender, EventArgs e)
        {
            FmBookBorrow bookBorrowForm = new FmBookBorrow();
            bookBorrowForm.Show();
        }

        private void 办理还书ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FmBookReturn bookReturnForm = new FmBookReturn();
            bookReturnForm.Show();
        }

        private void FmAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void FmAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
