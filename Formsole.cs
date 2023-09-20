using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROCAP_CLIENT
{
    public partial class Formsole : Form
    {
        public Formsole()
        {
            InitializeComponent();
        }
        private async Task go(string message)
        {
            Prowell_slack_bot.slack.slack_init();
            await Prowell_slack_bot.slack.post_messageProCap(message);
        }
        private async Task gogo()
        {
            int temp;
            string message;

            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                DateTime currentTime = DateTime.Now.Date;
                conn.Open();
                string assemblemessage = "select sole1,sole2,sole3,sole4,sole5,sole6 from sole where c_date=@currentTime";
                using (NpgsqlCommand cmd = new NpgsqlCommand(assemblemessage, conn))
                {
                    cmd.Parameters.AddWithValue("@currentTime", currentTime);
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dictionary<string, object> row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object columnValue = reader[i];
                                row[columnName] = columnValue;
                            }
                            message = DateTime.Now.ToString("yyyy-MM-dd") + "組底產量:" + "\n" + "sole1:   " + row["sole1"] + "\n" + "sole2:   " + row["sole2"] + "\n" + "sole3:   " + row["sole3"] + "\n" + "sole4:   " + row["sole4"] + "\n" + "sole5:   " + row["sole5"] + "\n" + "sole6:   " + row["sole6"];
                            go(message);
                        }
                    }
                }
            }
        }
        private void buttonsubmit_Click(object sender, EventArgs e)
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            {
                                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                                {
                                    MessageBox.Show("產量不能為空");
                                    textBox1.Focus();
                                }

                                else
                                {   // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @c_date";
                                    string checkExistingdata = "SELECT sole1 FROM sole ";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("c_date", currentTime);
                                        // 检查是否存在相同时间戳的记录
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        object result = checkCommanddata.ExecuteScalar();
                                        if (count > 0)
                                        {
                                            // 如果存在相同时间戳的记录，执行更新操作
                                            if (result != null && result != DBNull.Value)
                                                MessageBox.Show("今日數據已更新");
                                            else
                                                MessageBox.Show("數據提交成功");
                                            string updateSql = "UPDATE sole SET sole1 = @sole1 WHERE c_date = @c_date";
                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("sole1", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }
                                            DataGridViewSole();
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql = "INSERT INTO sole (c_date, sole1) VALUES (@c_date, @sole1)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("sole1", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewSole();
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                    }
                                }
                            }
                            break;
                        case 1:
                            {
                                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                                {
                                    MessageBox.Show("產量不能為空");
                                    textBox1.Focus();
                                }

                                else
                                {   // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @c_date";
                                    string checkExistingdata = "SELECT sole2 FROM sole ";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("c_date", currentTime);
                                        // 检查是否存在相同时间戳的记录
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        object result = checkCommanddata.ExecuteScalar();
                                        if (count > 0)
                                        {
                                            // 如果存在相同时间戳的记录，执行更新操作
                                            if (result != null && result != DBNull.Value)
                                                MessageBox.Show("今日數據已更新");
                                            else
                                                MessageBox.Show("數據提交成功");
                                            string updateSql = "UPDATE sole SET sole2 = @sole2 WHERE c_date = @c_date";
                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("sole2", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }
                                            DataGridViewSole();
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql = "INSERT INTO sole (c_date, sole2) VALUES (@c_date, @sole2)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("sole2", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewSole();
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                    }
                                }
                            }
                            break;
                        case 2:
                            {
                                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                                {
                                    MessageBox.Show("產量不能為空");
                                    textBox1.Focus();
                                }

                                else
                                {    // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @c_date";
                                    string checkExistingdata = "SELECT sole3 FROM sole ";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("c_date", currentTime);
                                        // 检查是否存在相同时间戳的记录
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        object result = checkCommanddata.ExecuteScalar();
                                        if (count > 0)
                                        {
                                            // 如果存在相同时间戳的记录，执行更新操作
                                            if (result != null && result != DBNull.Value)
                                                MessageBox.Show("今日數據已更新");
                                            else
                                                MessageBox.Show("數據提交成功");
                                            string updateSql = "UPDATE sole SET sole3 = @sole3 WHERE c_date = @c_date";
                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("sole3", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }
                                            DataGridViewSole();
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql = "INSERT INTO sole3 (c_date, sole3) VALUES (@c_date, @sole3)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("sole3", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewSole();
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                    }
                                }
                            }
                            break;
                        case 3:
                            {
                                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                                {
                                    MessageBox.Show("產量不能為空");
                                    textBox1.Focus();
                                }

                                else
                                {    // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @c_date";
                                    string checkExistingdata = "SELECT sole4 FROM sole ";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("c_date", currentTime);
                                        // 检查是否存在相同时间戳的记录
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        object result = checkCommanddata.ExecuteScalar();
                                        if (count > 0)
                                        {
                                            // 如果存在相同时间戳的记录，执行更新操作
                                            if (result != null && result != DBNull.Value)
                                                MessageBox.Show("今日數據已更新");
                                            else
                                                MessageBox.Show("數據提交成功");
                                            string updateSql = "UPDATE sole SET sole4 = @sole4 WHERE c_date = @c_date";
                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("sole4", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }
                                            DataGridViewSole();
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql = "INSERT INTO sole (c_date, sole4) VALUES (@c_date, @sole4)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("sole4", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewSole();
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                    }
                                }
                            }
                            break;
                        case 4:
                            {
                                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                                {
                                    MessageBox.Show("產量不能為空");
                                    textBox1.Focus();
                                }

                                else
                                {    // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @c_date";
                                    string checkExistingdata = "SELECT sole5 FROM sole ";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("c_date", currentTime);
                                        // 检查是否存在相同时间戳的记录
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        object result = checkCommanddata.ExecuteScalar();
                                        if (count > 0)
                                        {
                                            // 如果存在相同时间戳的记录，执行更新操作
                                            if (result != null && result != DBNull.Value)
                                                MessageBox.Show("今日數據已更新");
                                            else
                                                MessageBox.Show("數據提交成功");
                                            string updateSql = "UPDATE sole SET sole5 = @sole5 WHERE c_date = @c_date";
                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("sole5", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }
                                            DataGridViewSole();
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql = "INSERT INTO sole (c_date, sole5) VALUES (@c_date, @sole5)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("sole5", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewSole();
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                    }
                                }
                            }
                            break;
                        case 5:
                            {
                                if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                                {
                                    MessageBox.Show("產量不能為空");
                                    textBox1.Focus();
                                }

                                else
                                {    // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @c_date";
                                    string checkExistingdata = "SELECT sole6 FROM sole ";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("c_date", currentTime);
                                        // 检查是否存在相同时间戳的记录
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        object result = checkCommanddata.ExecuteScalar();
                                        if (count > 0)
                                        {
                                            // 如果存在相同时间戳的记录，执行更新操作
                                            if (result != null && result != DBNull.Value)
                                                MessageBox.Show("今日數據已更新");
                                            else
                                                MessageBox.Show("數據提交成功");
                                            string updateSql = "UPDATE sole SET sole6 = @sole6 WHERE c_date = @c_date";
                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("sole6", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }
                                            DataGridViewSole();
                                            conn.Close();
                                            textBox1.Text = "";
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql = "INSERT INTO sole (c_date, sole6) VALUES (@c_date, @sole6)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("sole6", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewSole();
                                            conn.Close();
                                            textBox1.Text = "";
                                            textBox1.Focus();
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
                textBox1.Text = "";
            }
        }
        private void Formsole_Load(object sender, EventArgs e)
        {
            timer1.Start();
            comboBox1.SelectedIndex = 0;
            DataGridViewSole();
        }
        private void DataGridViewSole()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string sqlsole = "select c_date,sole1,sole2,sole3,sole4,sole5,sole6 from sole";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlsole, conn);
                    NpgsqlDataAdapter adp = new NpgsqlDataAdapter(cmd);
                    DataTable dataTable_So = new DataTable();
                    adp.Fill(dataTable_So);
                    dataGridView1.DataSource = dataTable_So;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("yyyy-MM-dd" + "產量");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void Formsole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonsubmit_Click(sender, e);
        }

        private void buttonmessage_Click(object sender, EventArgs e)
        {
            gogo();
            MessageBox.Show("組底今日產量發送成功！");
        }
    }
}
