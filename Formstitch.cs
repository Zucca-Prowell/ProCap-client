using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROCAP_CLIENT
{
    public partial class Formstitch : Form
    {
        public Formstitch()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("yyyy-MM-dd" + "產量");
        }

        private void Formstitch_Load(object sender, EventArgs e)
        {
            timer1.Start();
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
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
                                {   
                                    // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingSql1 = "SELECT COUNT(*) FROM stitch WHERE c_date = @c_date";
                                    using (NpgsqlCommand checkCommand = new NpgsqlCommand(checkExistingSql1, conn))
                                    {
                                        checkCommand.Parameters.AddWithValue("c_date", currentTime);

                                        // 检查是否存在相同时间戳的记录
                                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                                        if (count > 0)
                                        {
                                            // 如果存在相同时间戳的记录，执行更新操作
                                            string updateSql1 = "UPDATE stitch SET stitch1 = @stitch1 WHERE c_date = @c_date";

                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql1, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("stitch1", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("今日数据已更新");
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql1 = "INSERT INTO stitch (c_date, stitch1) VALUES (@c_date, @stitch1)";

                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql1, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("stitch1", int.Parse(textBox1.Text.Trim()));
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
                                {
                                    // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingSql2 = "SELECT COUNT(*) FROM stitch WHERE c_date = @c_date";
                                    using (NpgsqlCommand checkCommand = new NpgsqlCommand(checkExistingSql2, conn))
                                    {
                                        checkCommand.Parameters.AddWithValue("c_date", currentTime);

                                        // 检查是否存在相同时间戳的记录
                                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            // 如果存在相同时间戳的记录，执行更新操作
                                            string updateSql2 = "UPDATE stitch SET stitch2 = @stitch2 WHERE c_date = @c_date";

                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql2, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("stitch2", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("今日数据已更新");
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql2 = "INSERT INTO stitch (c_date, stitch2) VALUES (@c_date, @stitch2)";

                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql2, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("stitch2", int.Parse(textBox1.Text.Trim()));
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
                                {    // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingSql3 = "SELECT COUNT(*) FROM stitch WHERE c_date = @c_date";
                                    using (NpgsqlCommand checkCommand = new NpgsqlCommand(checkExistingSql3, conn))
                                    {
                                        checkCommand.Parameters.AddWithValue("c_date", currentTime);

                                        // 检查是否存在相同时间戳的记录
                                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                                        if (count > 0)
                                        {
                                            // 如果存在相同时间戳的记录，执行更新操作
                                            string updateSql3 = "UPDATE stitch SET stitch3 = @stitch3 WHERE c_date = @c_date";

                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql3, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("stitch3", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("今日数据已更新");
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql3 = "INSERT INTO stitch (c_date, stitch3) VALUES (@c_date, @stitch3)";

                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql3, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("stitch3", int.Parse(textBox1.Text.Trim()));
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
                                {    // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingSql4 = "SELECT COUNT(*) FROM stitch WHERE c_date = @c_date";
                                    using (NpgsqlCommand checkCommand = new NpgsqlCommand(checkExistingSql4, conn))
                                    {
                                        checkCommand.Parameters.AddWithValue("c_date", currentTime);

                                        // 检查是否存在相同时间戳的记录
                                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                                        if (count > 0)
                                        {
                                            // 如果存在相同时间戳的记录，执行更新操作
                                            string updateSql4 = "UPDATE stitch SET stitch4 = @stitch4 WHERE c_date = @c_date";

                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql4, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("stitch4", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("今日数据已更新");
                                            conn.Close();
                                            textBox1.Text = "";
                                            comboBox1.SelectedIndex++;
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql4 = "INSERT INTO stitch (c_date, stitch4) VALUES (@c_date, @stitch4)";

                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql4, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("stitch4", int.Parse(textBox1.Text.Trim()));
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
                                {    // 检查数据库中是否已存在相同时间戳的记录
                                    string checkExistingSql5 = "SELECT COUNT(*) FROM stitch WHERE c_date = @c_date";
                                    using (NpgsqlCommand checkCommand = new NpgsqlCommand(checkExistingSql5, conn))
                                    {
                                        checkCommand.Parameters.AddWithValue("c_date", currentTime);

                                        // 检查是否存在相同时间戳的记录
                                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                                        if (count > 0)
                                        {
                                            // 如果存在相同时间戳的记录，执行更新操作
                                            string updateSql5 = "UPDATE stitch SET stitch5 = @stitch5 WHERE c_date = @c_date";

                                            using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql5, conn))
                                            {
                                                updateCommand.Parameters.AddWithValue("stitch5", int.Parse(textBox1.Text.Trim()));
                                                updateCommand.Parameters.AddWithValue("c_date", currentTime);
                                                updateCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("今日数据已更新");
                                            conn.Close();
                                            textBox1.Text = "";
                                            textBox1.Focus();
                                        }
                                        else
                                        {
                                            // 如果不存在相同时间戳的记录，执行插入操作
                                            string insertSql5 = "INSERT INTO stitch (c_date, stitch5) VALUES (@c_date, @stitch5)";

                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql5, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("c_date", currentTime);
                                                insertCommand.Parameters.AddWithValue("stitch5", int.Parse(textBox1.Text.Trim()));
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

        private void Formstitch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
                buttonsubmit_Click(sender, e);  
        }
    }
}
