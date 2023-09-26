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

            string message;
            string message1;
            int sum = 0;
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                DateTime currentTime = DateTime.Now.Date;
                conn.Open();
                string assemblemessage = "select stitch1,stitch2,stitch3,stitch4,stitch5,comment from stitch where c_date=@currentTime";
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
                            int num1 = (int)row["stitch1"];
                            int num2 = (int)row["stitch2"];
                            int num3 = (int)row["stitch3"];
                            int num4 = (int)row["stitch4"];
                            int num5 = (int)row["stitch5"];
                            sum = num1 + num2 + num3 + num4 + num5;
                            if (row["comment"] is string stringValue)
                                message1 = stringValue;
                            else
                                message1 = "針車今日無補充說明";
                            message = "大家好！" + DateTime.Now.ToString("yyyy-MM-dd") + "針車產量如下:" + "\n" + "針一課:   " + row["stitch1"] + "雙" + "\n" + "針二課:   " + row["stitch2"] + "雙" + "\n" + "針三課:   " + row["stitch3"] + "雙" + "\n" + "針四課:   " + row["stitch4"] + "雙" + "\n" + "針五課:   " + row["stitch5"] + "雙" + "\n" + " 合計:     " + sum + "雙" + "\n" + message1;
                            go(message);
                        }
                    }
                }
            }
        }
        private void stitchsum()
        {
            int temp;
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    string sqlstitchsum1 = "select stitch1,stitch2,stitch3,stitch4,stitch5,lean1線,stitchsum from stitch where c_date=@currentTime";
                    string sqlstitchsum2 = "update stitch set stitchsum=@stitchsum where c_date=@currentTime";
                    using (NpgsqlCommand cmd2 = new NpgsqlCommand(sqlstitchsum2, conn))
                    {
                        using (NpgsqlCommand cmd1 = new NpgsqlCommand(sqlstitchsum1, conn))
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
                                    if (reader["stitch1"] is int intValue1)
                                        value1 = intValue1;
                                    else
                                        value1 = 0;
                                    if (reader["stitch2"] is int intValue2)
                                        value2 = intValue2;
                                    else
                                        value2 = 0;
                                    if (reader["stitch3"] is int intValue3)
                                        value3 = intValue3;
                                    else
                                        value3 = 0;
                                    if (reader["stitch4"] is int intValue4)
                                        value4 = intValue4;
                                    else
                                        value4 = 0;
                                    if (reader["stitch5"] is int intValue5)
                                        value5 = intValue5;
                                    else
                                        value5 = 0;
                                    if (reader["stitchsum"] is int intValue6)
                                        value6 = intValue6;
                                    else
                                        value6 = 0;
                                    value6 = value1 + value2 + value3 + value4 + value5;
                                    temp = value6;
                                }
                            }
                        }
                        cmd2.Parameters.AddWithValue("@currentTime", currentTime);
                        cmd2.Parameters.AddWithValue("stitchsum", temp);
                        cmd2.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }
        }
            private void DataGridViewStitch()
            {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    string sqlstitch = "select c_date,stitchsum,stitch1,stitch2,stitch3,stitch4,stitch5,lean1線,lean2線,lean3線,comment from stitch";
                    using (NpgsqlCommand cmd3 = new NpgsqlCommand(sqlstitch, conn))
                    {
                        
                        NpgsqlDataAdapter adp = new NpgsqlDataAdapter(cmd3);
                        DataTable dataTable_St = new DataTable();
                        adp.Fill(dataTable_St);
                        dataGridView1.DataSource = dataTable_St;
                        dataGridView1.Sort(dataGridView1.Columns["c_date"], ListSortDirection.Descending);
                        dataGridView1.Columns["c_date"].HeaderText = "日期";
                        dataGridView1.Columns["comment"].HeaderText = "補充說明";
                        dataGridView1.Columns["stitch1"].HeaderText = "針1課";
                        dataGridView1.Columns["stitch2"].HeaderText = "針2課";
                        dataGridView1.Columns["stitch3"].HeaderText = "針3課";
                        dataGridView1.Columns["stitch4"].HeaderText = "針4課";
                        dataGridView1.Columns["stitch5"].HeaderText = "針5課";
                        dataGridView1.Columns["stitchsum"].HeaderText = "針車合計";
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

        private void Formstitch_Load(object sender, EventArgs e)
        {
            timer1.Start();
            comboBox1.SelectedIndex = 0;
            DataGridViewStitch();
            this.Resize += new EventHandler(Formstitch_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
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
                                    string checkExistingdate = "SELECT COUNT(*) FROM stitch WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT stitch1 FROM stitch WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE stitch SET stitch1 = @stitch1 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@stitch1", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    stitchsum();
                                                    DataGridViewStitch();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE stitch SET stitch1 = @stitch1 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@stitch1", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    stitchsum();
                                                    DataGridViewStitch();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO stitch (c_date, stitch1) VALUES (@currentTime, @stitch1)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@stitch1", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            stitchsum();
                                            DataGridViewStitch();
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
                                    string checkExistingdate = "SELECT COUNT(*) FROM stitch WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT stitch2 FROM stitch WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE stitch SET stitch2 = @stitch2 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@stitch2", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    stitchsum();
                                                    DataGridViewStitch();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE stitch SET stitch2 = @stitch2 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@stitch2", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    stitchsum();
                                                    DataGridViewStitch();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO stitch (c_date, stitch2) VALUES (@currentTime, @stitch2)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@stitch2", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            stitchsum();
                                            DataGridViewStitch();
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
                                    string checkExistingdate = "SELECT COUNT(*) FROM stitch WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT stitch3 FROM stitch WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE stitch SET stitch3 = @stitch3 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@stitch3", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    stitchsum();
                                                    DataGridViewStitch();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE stitch SET stitch3 = @stitch3 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@stitch3", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    stitchsum();
                                                    DataGridViewStitch();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO stitch (c_date, stitch3) VALUES (@currentTime, @stitch3)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@stitch3", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            stitchsum();
                                            DataGridViewStitch();
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
                                    string checkExistingdate = "SELECT COUNT(*) FROM stitch WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT stitch4 FROM stitch WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE stitch SET stitch4 = @stitch4 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@stitch4", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    stitchsum();
                                                    DataGridViewStitch();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE stitch SET stitch4 = @stitch4 WHERE c_date=@currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@stitch4", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    stitchsum();
                                                    DataGridViewStitch();
                                                    conn.Close();
                                                    comboBox1.SelectedIndex++;
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }

                                        else
                                        {
                                            string insertSql = "INSERT INTO stitch (c_date, stitch4) VALUES (@currentTime, @stitch4)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@stitch4", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            stitchsum();
                                            DataGridViewStitch();
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
                                    string checkExistingdate = "SELECT COUNT(*) FROM stitch WHERE c_date = @currentTime";
                                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                    {
                                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                        if (count > 0)
                                        {
                                            string checkExistingdata = "SELECT stitch5 FROM stitch WHERE c_date=@currentTime ";
                                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                            {
                                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                                object result = checkCommanddata.ExecuteScalar();
                                                if ((count > 0) && (result != null && result != DBNull.Value))
                                                {
                                                    string updateSql = "UPDATE stitch SET stitch5 = @stitch5 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@stitch5", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("今日數據已更新");
                                                    stitchsum();
                                                    DataGridViewStitch();
                                                    conn.Close();
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                                else
                                                {
                                                    string updateSql = "UPDATE stitch SET stitch5 = @stitch5 WHERE c_date = @currentTime";
                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("@stitch5", int.Parse(textBox1.Text.Trim()));
                                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                        updateCommand.ExecuteNonQuery();
                                                    }
                                                    MessageBox.Show("數據提交成功");
                                                    stitchsum();
                                                    DataGridViewStitch();
                                                    conn.Close();
                                                    textBox1.Text = "";
                                                    textBox1.Focus();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string insertSql = "INSERT INTO stitch (c_date, stitch5) VALUES (@currentTime, @stitch5)";
                                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                            {
                                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                insertCommand.Parameters.AddWithValue("@stitch5", int.Parse(textBox1.Text.Trim()));
                                                insertCommand.ExecuteNonQuery();
                                            }
                                            MessageBox.Show("數據提交成功");
                                            stitchsum();
                                            DataGridViewStitch();
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
            if (e.KeyCode == Keys.Enter)
                buttonsubmit_Click(sender, e);
        }

        private void buttonmessage_Click(object sender, EventArgs e)
        {
            gogo();
            MessageBox.Show("針車今日產量發送成功！");
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
                        string checkExistingdate = "SELECT COUNT(*) FROM stitch WHERE c_date = @currentTime";
                        using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                        {
                            checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                            int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                            if (count > 0)
                            {
                                string checkExistingcomment = "SELECT comment FROM stitch WHERE c_date=@currentTime ";
                                using (NpgsqlCommand checkCommandcomment = new NpgsqlCommand(checkExistingcomment, conn))
                                {
                                    checkCommandcomment.Parameters.AddWithValue("@currentTime", currentTime);
                                    object result = checkCommandcomment.ExecuteScalar();
                                    if ((count > 0) && (result != null && result != DBNull.Value))
                                    {
                                        string updateSql = "UPDATE stitch SET comment = @comment WHERE c_date = @currentTime";
                                        using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                        {
                                            updateCommand.Parameters.AddWithValue("@comment", textBoxcomment.Text.Trim());
                                            updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                            updateCommand.ExecuteNonQuery();
                                        }
                                        MessageBox.Show("今日補充說明已更新");
                                        DataGridViewStitch();
                                        conn.Close();
                                        textBoxcomment.Text = "";
                                        textBoxcomment.Focus();
                                    }
                                    else
                                    {
                                        string updateSql = "UPDATE stitch SET comment = @comment WHERE c_date=@currentTime";
                                        using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                        {
                                            updateCommand.Parameters.AddWithValue("@comment", textBoxcomment.Text.Trim());
                                            updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                            updateCommand.ExecuteNonQuery();
                                        }
                                        MessageBox.Show("補充說明提交成功");
                                        DataGridViewStitch();
                                        conn.Close();
                                        textBoxcomment.Text = "";
                                        textBoxcomment.Focus();
                                    }
                                }
                            }
                            else
                            {
                                string insertSql = "INSERT INTO stitch (c_date, comment) VALUES (@currentTime, @comment)";
                                using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                {
                                    insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                    insertCommand.Parameters.AddWithValue("@comment", textBoxcomment.Text.Trim());
                                    insertCommand.ExecuteNonQuery();
                                }
                                MessageBox.Show("補充說明提交成功");
                                DataGridViewStitch();
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

        private void Formstitch_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
        }
    }
}
