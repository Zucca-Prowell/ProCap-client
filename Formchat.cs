using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PROCAP_CLIENT
{
    public partial class Formchat : Form
    {
        public Formchat()
        {
            InitializeComponent();
           

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("yyyy-MM-dd" + "產量");
        }

        private void Formchat_Load(object sender, EventArgs e)
        {
            timer1.Start();
            DataGridViewChat();
        }
        private void DataGridViewChat()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string sqlchat = "select c_date,capacity,lean01,lean02,lean03 from cut";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlchat, conn);
                    NpgsqlDataAdapter adp = new NpgsqlDataAdapter(cmd);
                    DataTable dataTable_C = new DataTable();
                    adp.Fill(dataTable_C);
                    dataGridView1.DataSource = dataTable_C;
                    //int startIndex = Math.Max(0, dataTable_C.Rows.Count - 5);
                    //// 将 DataGridView 滚动到最近的5行数据
                    //if (dataGridView1.Rows.Count > 0)
                    //{
                    //    dataGridView1.FirstDisplayedScrollingRowIndex = startIndex;
                    //    dataGridView1.Rows[startIndex].Selected = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }
        }
        private async Task go()
        {
            Prowell_slack_bot.slack.slack_init();
            await Prowell_slack_bot.slack.post_messageProCap("這是一條測試消息");
        }

        private void buttonsubmit_Click(object sender, EventArgs e)
        {
            //go();

            //if (true)
            //{
            //    return;
            //}

            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("產量不能為空");
            }
            else
            {
                string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                    {

                        conn.Open();
                        DateTime currentTime = DateTime.Now.Date;
                        // 检查数据库中是否已存在相同时间戳的记录
                        string checkExistingSql = "SELECT COUNT(*) FROM cut WHERE c_date = @c_date";
                        using (NpgsqlCommand checkCommand = new NpgsqlCommand(checkExistingSql, conn))
                        {
                            checkCommand.Parameters.AddWithValue("c_date", currentTime);

                            // 检查是否存在相同时间戳的记录
                            int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                            if (count > 0)
                            {
                                // 如果存在相同时间戳的记录，执行更新操作
                                string updateSql = "UPDATE cut SET capacity = @capacity WHERE c_date = @c_date";

                                using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                {
                                    updateCommand.Parameters.AddWithValue("capacity", int.Parse(textBox1.Text.Trim()));
                                    updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                    updateCommand.ExecuteNonQuery();
                                }
                                MessageBox.Show("今日数据已更新");
                                conn.Close();
                                textBox1.Text = "";
                            }
                            else
                            {
                                // 如果不存在相同时间戳的记录，执行插入操作
                                string insertSql = "INSERT INTO cut (c_date, capacity) VALUES (@c_date, @capacity)";

                                using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                {
                                    insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                    insertCommand.Parameters.AddWithValue("capacity", int.Parse(textBox1.Text.Trim()));
                                    insertCommand.ExecuteNonQuery();
                                }
                                MessageBox.Show("數據提交成功");
                                conn.Close();
                                textBox1.Text = "";
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("數據庫連接失敗: " + ex.Message);
                    textBox1.Text = "";
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                buttonsubmit_Click(sender, e);
        }

        private void Formchat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
