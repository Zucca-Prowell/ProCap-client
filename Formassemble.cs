﻿using Npgsql;
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
        private async Task go(string message)
        {
            Prowell_slack_bot.slack.slack_init();
            await Prowell_slack_bot.slack.post_messageProCap(message);

        }
        private async Task gogo()
        {
            int temp;
            string message;
            int sum = 0;
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                DateTime currentTime = DateTime.Now.Date;
                conn.Open();
                string assemblemessage = "select assemble01,assemble02,assemble03,assemble04,assemble07 from assemble where c_date=@currentTime";
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
                            sum = (int)row["assemble01"] +(int)row["assemble02"] + (int)row["assemble03"] + (int)row["assemble04"] + (int)row["assemble07"];
                            message = "大家好！"+DateTime.Now.ToString("yyyy-MM-dd") + "成型課產量如下:" + "\n"+"成1組:   " +row["assemble01"]+"雙"+"\n"+ "成2組:   " + row["assemble02"] + "雙" + "\n" + "成3組:   " + row["assemble03"] + "雙" + "\n" + "成4組:   " + row["assemble04"] + "雙" + "\n" + "成7組:   " + row["assemble07"] + "雙"+"\n"+" 合計:    "+sum+"雙";
                            go(message);
                        }
                    }
                }
            }
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
                                {
                                    string checkExistingdate = "SELECT COUNT(*) FROM assemble WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT assemble01 FROM assemble WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE assemble SET assemble01 = @assemble01 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@assemble01", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    DataGridViewAssemble();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE assemble SET assemble01 = @assemble01 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@assemble01", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    DataGridViewAssemble();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO assemble (c_date, assemble01) VALUES (@currentTime, @assemble01)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@assemble01", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewAssemble();
                                            conn.Close();
                                            comboBox1.SelectedIndex++;
                                            textBox1.Text = "";
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
                                    string checkExistingdate = "SELECT COUNT(*) FROM assemble WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT assemble02 FROM assemble WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE assemble SET assemble02 = @assemble02 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@assemble02", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    DataGridViewAssemble();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE assemble SET assemble02 = @assemble02 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@assemble02", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    DataGridViewAssemble();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO assemble (c_date, assemble02) VALUES (@currentTime, @assemble02)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@assemble02", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewAssemble();
                                            conn.Close();
                                            comboBox1.SelectedIndex++;
                                            textBox1.Text = "";
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
                                {
                                    string checkExistingdate = "SELECT COUNT(*) FROM assemble WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT assemble03 FROM assemble WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE assemble SET assemble03 = @assemble03 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@assemble03", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    DataGridViewAssemble();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE assemble SET assemble03 = @assemble03 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@assemble03", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    DataGridViewAssemble();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO assemble (c_date, assemble03) VALUES (@currentTime, @assemble03)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@assemble03", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewAssemble();
                                            conn.Close();
                                            comboBox1.SelectedIndex++;
                                            textBox1.Text = "";
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
                                {
                                    string checkExistingdate = "SELECT COUNT(*) FROM assemble WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT assemble04 FROM assemble WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE assemble SET assemble04 = @assemble04 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@assemble04", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    DataGridViewAssemble();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE assemble SET assemble04 = @assemble04 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@assemble04", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    DataGridViewAssemble();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO assemble (c_date, assemble04) VALUES (@currentTime, @assemble04)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@assemble04", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewAssemble();
                                            conn.Close();
                                            comboBox1.SelectedIndex++;
                                            textBox1.Text = "";
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
                                {
                                    string checkExistingdate = "SELECT COUNT(*) FROM assemble WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT assemble07 FROM assemble WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE assemble SET assemble07 = @assemble07 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@assemble07", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    DataGridViewAssemble();
                                                    conn.Close();
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE assemble SET assemble07 = @assemble07 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@assemble07", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    DataGridViewAssemble();
                                                    conn.Close();
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO assemble (c_date, assemble07) VALUES (@currentTime, @assemble07)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@assemble07", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewAssemble();
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

        private void buttonmessage_Click(object sender, EventArgs e)
        {
            gogo();
            MessageBox.Show("成型今日產量發送成功！");
        }
    }
}
