using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
        float X, Y;
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                float a;
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)(a);
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                if (newx == 1)
                {
                    Single currentSize = Convert.ToSingle(mytag[4]) * (float)Math.Sqrt(newy);
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                }
                if (newy == 1)
                {
                    Single currentSize = Convert.ToSingle(mytag[4]) * (float)Math.Sqrt(newx);
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                }
                else
                {
                    Single currentSize = Convert.ToSingle(mytag[4]) * (float)Math.Sqrt(newx * newy);
                    con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                }
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }
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
            string message1;
            int sum = 0;
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                DateTime currentTime = DateTime.Now.Date;
                conn.Open();
                string assemblemessage = "select sole1,sole2,sole3,sole4,sole5,sole6,comment from sole where c_date=@currentTime";
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
                            if (row["comment"] is string stringValue)
                                message1 = stringValue;
                            else
                                message1 = "組底今日無補充說明";
                            sum = (int)row["sole1"] + (int)row["sole2"] + (int)row["sole3"] + (int)row["sole4"] + (int)row["sole5"] + (int)row["sole6"];
                            message = "大家好！" + DateTime.Now.ToString("yyyy-MM-dd") + "組底產量如下:" + "\n" + "流水線1組:   " + row["sole1"] + "雙" + "\n" + "流水線2組:   " + row["sole2"] + "雙" + "\n" + "流水線3組:   " + row["sole3"] + "雙" + "\n" + "流水線4組:   " + row["sole4"] + "雙" + "\n" + "流水線5組:   " + row["sole5"] + "雙" + "\n" + "流水線6組:   " + row["sole6"] + "雙" + "\n" + "\t" + "合計:       " + sum + "雙" + "\n" + message1;
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
                                {
                                    string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT sole1 FROM sole WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE sole SET sole1 = @sole1 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@sole1", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    DataGridViewSole();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE sole SET sole1 = @sole1 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@sole1", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    DataGridViewSole();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO sole (c_date, sole1) VALUES (@currentTime, @sole1)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@sole1", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewSole();
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
                                    string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT sole2 FROM sole WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE sole SET sole2 = @sole2 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@sole2", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    DataGridViewSole();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE sole SET sole2 = @sole2 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@sole2", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    DataGridViewSole();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO sole (c_date, sole2) VALUES (@currentTime, @sole2)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@sole2", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewSole();
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
                                    string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT sole3 FROM sole WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE sole SET sole3 = @sole3 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@sole3", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    DataGridViewSole();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE sole SET sole3 = @sole3 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@sole3", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    DataGridViewSole();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO sole (c_date, sole3) VALUES (@currentTime, @sole3)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@sole3", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewSole();
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
                                    string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT sole4 FROM sole WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE sole SET sole4 = @sole4 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@sole4", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    DataGridViewSole();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE sole SET sole4 = @sole4 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@sole4", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    DataGridViewSole();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO sole (c_date, sole4) VALUES (@currentTime, @sole4)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@sole4", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewSole();
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
                                    string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT sole5 FROM sole WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE sole SET sole5 = @sole5 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@sole5", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    DataGridViewSole();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE sole SET sole5 = @sole5 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@sole5", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    DataGridViewSole();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO sole (c_date, sole5) VALUES (@currentTime, @sole5)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@sole5", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            DataGridViewSole();
                                            conn.Close();
                                            comboBox1.SelectedIndex++;
                                            textBox1.Text = "";
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
                                {
                                    string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT sole6 FROM sole WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE sole SET sole6 = @sole6 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@sole6", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    DataGridViewSole();
                                                    conn.Close();
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE sole SET sole6 = @sole6 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@sole6", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    DataGridViewSole();
                                                    conn.Close();
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO sole (c_date, sole6) VALUES (@currentTime, @sole6)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@sole6", int.Parse(textBox1.Text.Trim()));
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
            this.Resize += new EventHandler(Formsole_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
        }
        private void DataGridViewSole()
        {
            int temp;
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    string sqlsole = "select c_date,solesum,sole1,sole2,sole3,sole4,sole5,sole6,comment from sole";
                    string sqlsolesum1 = "select sole1,sole2,sole3,sole4,sole5,sole6,solesum from sole where c_date=@currentTime";
                    string sqlsolesum2 = "update sole set solesum=@solesum where c_date=@currentTime";
                    using (NpgsqlCommand cmd3 = new NpgsqlCommand(sqlsole, conn))
                    {
                        using (NpgsqlCommand cmd2 = new NpgsqlCommand(sqlsolesum2, conn))
                        {
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand(sqlsolesum1, conn))
                            {
                                cmd1.Parameters.AddWithValue("@currentTime", currentTime);
                                using (NpgsqlDataReader reader = cmd1.ExecuteReader())
                                {
                                    reader.Read();
                                    {
                                        int value1;
                                        int value2;
                                        int value3;
                                        int value4;
                                        int value5;
                                        int value6;
                                        int value7;
                                        if (reader["sole1"] is int intValue1)
                                            value1 = intValue1;
                                        else
                                            value1 = 0;
                                        if (reader["sole2"] is int intValue2)
                                            value2 = intValue2;
                                        else
                                            value2 = 0;
                                        if (reader["sole3"] is int intValue3)
                                            value3 = intValue3;
                                        else
                                            value3 = 0;
                                        if (reader["sole4"] is int intValue4)
                                            value4 = intValue4;
                                        else
                                            value4 = 0;
                                        if (reader["sole5"] is int intValue5)
                                            value5 = intValue5;
                                        else
                                            value5 = 0;
                                        if (reader["sole6"] is int intValue6)
                                            value6 = intValue6;
                                        else
                                            value6 = 0;
                                        if (reader["solesum"] is int intValue7)
                                            value7 = intValue7;
                                        else
                                            value7 = 0;
                                        value7 = value1 + value2 + value3 + value4 + value5 + value6;
                                        temp = value7;
                                    }
                                }
                            }
                            cmd2.Parameters.AddWithValue("@solesum", temp);
                            cmd2.Parameters.AddWithValue("@currentTime", currentTime);
                            cmd2.ExecuteNonQuery();
                        }
                          NpgsqlDataAdapter adp = new NpgsqlDataAdapter(cmd3);
                        DataTable dataTable_So = new DataTable();
                        adp.Fill(dataTable_So);
                        dataGridView1.DataSource = dataTable_So;
                        dataGridView1.Sort(dataGridView1.Columns["c_date"], ListSortDirection.Descending);
                        dataGridView1.Columns["c_date"].HeaderText = "日期";
                        dataGridView1.Columns["comment"].HeaderText = "補充說明";
                        dataGridView1.Columns["sole1"].HeaderText = "流水線1";
                        dataGridView1.Columns["sole2"].HeaderText = "流水線2";
                        dataGridView1.Columns["sole3"].HeaderText = "流水線3";
                        dataGridView1.Columns["sole4"].HeaderText = "流水線4";
                        dataGridView1.Columns["sole5"].HeaderText = "流水線5";
                        dataGridView1.Columns["sole6"].HeaderText = "流水線6";
                        dataGridView1.Columns["solesum"].HeaderText = "組底合計";
                    }
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

        private void buttoncomment_Click(object sender, EventArgs e)
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {

                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    {
                        string checkExistingdate = "SELECT COUNT(*) FROM sole WHERE c_date = @currentTime";
                        using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                        {
                            checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                            int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                            if (count > 0)
                            {
                                string checkExistingcomment = "SELECT comment FROM sole WHERE c_date=@currentTime ";
                                using (NpgsqlCommand checkCommandcomment = new NpgsqlCommand(checkExistingcomment, conn))
                                {
                                    checkCommandcomment.Parameters.AddWithValue("@currentTime", currentTime);
                                    object result = checkCommandcomment.ExecuteScalar();
                                    if ((count > 0) && (result != null && result != DBNull.Value))
                                    {
                                        string updateSql = "UPDATE sole SET comment = @comment WHERE c_date = @currentTime";
                                        using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                        {
                                            updateCommand.Parameters.AddWithValue("@comment", textBoxcomment.Text.Trim());
                                            updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                            updateCommand.ExecuteNonQuery();
                                        }
                                        MessageBox.Show("今日補充說明已更新");
                                        DataGridViewSole();
                                        conn.Close();
                                        textBoxcomment.Text = "";
                                        textBoxcomment.Focus();
                                    }
                                    else
                                    {
                                        string updateSql = "UPDATE sole SET comment = @comment WHERE c_date=@currentTime";
                                        using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                        {
                                            updateCommand.Parameters.AddWithValue("@comment", textBoxcomment.Text.Trim());
                                            updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                            updateCommand.ExecuteNonQuery();
                                        }
                                        MessageBox.Show("補充說明提交成功");
                                        DataGridViewSole();
                                        conn.Close();
                                        textBoxcomment.Text = "";
                                        textBoxcomment.Focus();
                                    }
                                }
                            }
                            else
                            {
                                string insertSql = "INSERT INTO sole (c_date, comment) VALUES (@currentTime, @comment)";
                                using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                {
                                    insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                    insertCommand.Parameters.AddWithValue("@comment", textBoxcomment.Text.Trim());
                                    insertCommand.ExecuteNonQuery();
                                }
                                MessageBox.Show("補充說明提交成功");
                                DataGridViewSole();
                                conn.Close();
                                textBoxcomment.Text = "";
                                textBoxcomment.Focus();
                            }
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

        private void textBoxcomment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttoncomment_Click(sender, e);
        }

        private void Formsole_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
        }
    }
}
