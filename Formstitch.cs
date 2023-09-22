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
                            message = "大家好！" + DateTime.Now.ToString("yyyy-MM-dd") + "針車產量如下:" + "\n" + "針一課:   " + row["stitch1"] + "雙" + "\n" + "針二課:   " + row["stitch2"] + "雙" + "\n" + "針三課:   " + row["stitch3"] + "雙" + "\n" + "針四課:   " + row["stitch4"] + "雙" + "\n" + "針五課:   " + row["stitch5"] + "雙" + "\n" + " 合計:     " + sum + "雙"+"\n"+message1;
                            go(message);
                        }
                    }
                }
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
                    string sqlstitch = "select c_date,stitch1,stitch2,stitch3,stitch4,stitch5,lean1線,lean2線,lean3線,comment from stitch";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlstitch, conn);
                    NpgsqlDataAdapter adp = new NpgsqlDataAdapter(cmd);
                    DataTable dataTable_St = new DataTable();
                    adp.Fill(dataTable_St);
                    dataGridView1.DataSource = dataTable_St;

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
    }
}
