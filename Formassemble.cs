using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROCAP_CLIENT
{
    public partial class Formassemble : Form
    {
        public Formassemble()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("yyyy-MM-dd" + "產量");
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
                                {  // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingdate = "SELECT COUNT(*) FROM assemble WHERE c_date = @c_date";
                                    string checkExistingdata = "SELECT assemble01 FROM assemble ";
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
                                            string updateSql = "UPDATE assemble SET assemble01 = @assemble01 WHERE c_date = @c_date";
                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("assemble01", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }

                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql = "INSERT INTO assemble (c_date, assemble01) VALUES (@c_date, @assemble01)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("assemble01", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
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
                                    string checkExistingdate = "SELECT COUNT(*) FROM assemble WHERE c_date = @c_date";
                                    string checkExistingdata = "SELECT assemble02 FROM assemble ";
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
                                            string updateSql = "UPDATE assemble SET assemble02 = @assemble02 WHERE c_date = @c_date";
                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("assemble02", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }

                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql = "INSERT INTO assemble (c_date, assemble02) VALUES (@c_date, @assemble02)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("assemble02", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
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
                                {  // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingdate = "SELECT COUNT(*) FROM assemble WHERE c_date = @c_date";
                                    string checkExistingdata = "SELECT assemble03 FROM assemble ";
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
                                            string updateSql = "UPDATE assemble SET assemble03 = @assemble03 WHERE c_date = @c_date";
                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("assemble03", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }

                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql = "INSERT INTO assemble (c_date, assemble03) VALUES (@c_date, @assemble03)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("assemble03", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
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
                                {  // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingdate = "SELECT COUNT(*) FROM assemble WHERE c_date = @c_date";
                                    string checkExistingdata = "SELECT assemble04 FROM assemble ";
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
                                            string updateSql = "UPDATE assemble SET assemble04 = @assemble04 WHERE c_date = @c_date";
                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("assemble04", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }

                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql = "INSERT INTO assemble (c_date, assemble04) VALUES (@c_date, @assemble04)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("assemble04", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
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
                                {   // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingdate = "SELECT COUNT(*) FROM assemble WHERE c_date = @c_date";
                                    string checkExistingdata = "SELECT assemble07 FROM assemble ";
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
                                            string updateSql = "UPDATE assemble SET assemble07 = @assemble07 WHERE c_date = @c_date";
                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("assemble07", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }

                                            conn.Close();
                                            textBox1.Text = "";
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql = "INSERT INTO assemble (c_date, assemble07) VALUES (@c_date, @assemble07)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("assemble07", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
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
        private void DataGridViewAssemble()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string sqlassemble = "select c_date,assemble01,assemble02,assemble03,assemble04,assemble07,lean01,lean02,lean03 from assemble";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlassemble, conn);
                    NpgsqlDataAdapter adp = new NpgsqlDataAdapter(cmd);
                    DataTable dataTable_A = new DataTable();
                    adp.Fill(dataTable_A);
                    dataGridView1.DataSource = dataTable_A;
                    int startIndex = Math.Max(0, dataTable_A.Rows.Count - 5);

                    // 将 DataGridView 滚动到最近的5行数据
                    if (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.FirstDisplayedScrollingRowIndex = startIndex;
                        dataGridView1.Rows[startIndex].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }
        }
        private void Formassemble_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            timer1.Start();
            DataGridViewAssemble();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void Formassemble_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonsubmit_Click(sender, e);
        }
    }
}
