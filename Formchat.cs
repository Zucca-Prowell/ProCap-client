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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }
        }
        private async Task go(string message)
        {
            Prowell_slack_bot.slack.slack_init();
            await Prowell_slack_bot.slack.post_messageProCap(message);

        }

        private void buttonsubmit_Click(object sender, EventArgs e)
        {
           
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
                        {
                            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
                            {
                                MessageBox.Show("產量不能為空");
                                textBox1.Focus();
                            }
                            else
                            {
                                string checkExistingdate = "SELECT COUNT(*) FROM cut WHERE c_date = @currentTime";
                                using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                                {
                                    checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                                    int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                                    if (count > 0)
                                    {
                                        string checkExistingdata = "SELECT capacity FROM cut WHERE c_date=@currentTime ";
                                        using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                                        {
                                            checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                            object result = checkCommanddata.ExecuteScalar();
                                            if ((count > 0) && (result != null && result != DBNull.Value))
                                            {
                                                string updateSql = "UPDATE cut SET capacity = @capacity WHERE c_date = @currentTime";
                                                using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                {
                                                    updateCommand.Parameters.AddWithValue("@capacity", int.Parse(textBox1.Text.Trim()));
                                                    updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                    updateCommand.ExecuteNonQuery();
                                                }
                                                MessageBox.Show("今日數據已更新");
                                                DataGridViewChat();
                                                conn.Close();
                                                textBox1.Text = "";
                                                textBox1.Focus();
                                            }
                                            else
                                            {
                                                string updateSql = "UPDATE cut SET capacity = @capacity WHERE c_date=@currentTime";
                                                using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                                {
                                                    updateCommand.Parameters.AddWithValue("@capacity", int.Parse(textBox1.Text.Trim()));
                                                    updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                                    updateCommand.ExecuteNonQuery();
                                                }
                                                MessageBox.Show("數據提交成功");
                                                DataGridViewChat();
                                                conn.Close();
                                                textBox1.Text = "";
                                                textBox1.Focus();
                                            }
                                        }
                                    }

                                    else
                                    {
                                        string insertSql = "INSERT INTO cut (c_date, capacity) VALUES (@currentTime, @capacity)";
                                        using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                        {
                                            insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                            insertCommand.Parameters.AddWithValue("@capacity", int.Parse(textBox1.Text.Trim()));
                                            insertCommand.ExecuteNonQuery();
                                        }
                                        MessageBox.Show("數據提交成功");
                                        DataGridViewChat();
                                        conn.Close();
                                        textBox1.Text = "";
                                        textBox1.Focus();
                                    }
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
                string chatmessage = "select capacity from cut where c_date=@currentTime";
                using (NpgsqlCommand cmd = new NpgsqlCommand(chatmessage, conn))
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
                            message = "大家好！"+DateTime.Now.ToString("yyyy-MM-dd")+"裁加配套產量如下:   "+  row["capacity"]+"雙";
                            go(message);
                            Thread.Sleep(1000);
                            Formlean formlean = new Formlean();
                            formlean.gogo1();
                            int num1 = Formlean.lean1chat;
                            //int num2 = Formlean.lean2chat;  //開線解除注釋
                            //int num3 = Formlean.lean3chat;  //開線解除注釋
                            int num4 = (int)row["capacity"];
                            sum=num1+/*num2+num3+*/num4;
                            message1 = "      合計:   " + sum + "雙";
                            go(message1);
                        }
                    }
                    
                }
            }
        }

        private void buttonmessage_Click(object sender, EventArgs e)
        {
            gogo();
            MessageBox.Show("裁加今日產量發送成功！");
        }
    }
}
