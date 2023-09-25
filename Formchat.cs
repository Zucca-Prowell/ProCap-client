using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            this.Resize += new EventHandler(Formchat_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
        }
        protected internal void DataGridViewChat()
        {
            int temp;
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    string sqlsum1 = "select capacity,lean01,cutsum from cut where c_date=@currentTime ";
                    string sqlsum2 = "update cut set cutsum=@cutsum where c_date=@currentTime";
                    string sqlchat = "select c_date,cutsum,capacity,lean01,lean02,lean03,comment,leancomment from cut";
                    using (NpgsqlCommand cmd3 = new NpgsqlCommand(sqlchat, conn))
                    {
                        using (NpgsqlCommand cmd2 = new NpgsqlCommand(sqlsum2, conn))
                        {
                            using (NpgsqlCommand cmd1 = new NpgsqlCommand(sqlsum1, conn))
                            {
                                cmd1.Parameters.AddWithValue("@currentTime", currentTime);
                                using (NpgsqlDataReader reader = cmd1.ExecuteReader())
                                {
                                    reader.Read();
                                    {
                                        int value1;
                                        int value2;
                                        int value3;
                                        if (reader["capacity"] is int intValue1)
                                            value1 = intValue1;
                                        else
                                            value1 = 0;
                                        if (reader["lean01"] is int intValue2)
                                            value2 = intValue2;
                                        else
                                            value2 = 0;
                                        if (reader["capacity"] is int intValue3)
                                            value3 = intValue3;
                                        else
                                            value3 = 0;
                                        value3 = value1 + value2;
                                        temp = value3;
                                    }
                                }
                            }
                            cmd2.Parameters.AddWithValue("@cutsum", temp);
                            cmd2.Parameters.AddWithValue("@currentTime", currentTime);
                            cmd2.ExecuteNonQuery();
                        }
                        NpgsqlDataAdapter adp = new NpgsqlDataAdapter(cmd3);
                        DataTable dataTable_C = new DataTable();
                        adp.Fill(dataTable_C);
                        dataGridView1.DataSource = dataTable_C;
                    }
                    dataGridView1.Sort(dataGridView1.Columns["c_date"], ListSortDirection.Descending);
                    dataGridView1.Columns["c_date"].HeaderText = "日期";
                    dataGridView1.Columns["capacity"].HeaderText = "裁加(大線)";
                    dataGridView1.Columns["comment"].HeaderText = "補充說明";
                    dataGridView1.Columns["leancomment"].HeaderText = "lean線補充說明";
                    dataGridView1.Columns["lean01"].HeaderText = "lean1線";
                    dataGridView1.Columns["lean02"].HeaderText = "lean2線";
                    dataGridView1.Columns["lean03"].HeaderText = "lean3線";
                    dataGridView1.Columns["cutsum"].HeaderText = "裁加(總和)";

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
                textBox1.Focus();
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
            string message2;
            int sum = 0;
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                DateTime currentTime = DateTime.Now.Date;
                conn.Open();
                string chatmessage = "select capacity,comment from cut where c_date=@currentTime";
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
                            message = "大家好！" + DateTime.Now.ToString("yyyy-MM-dd") + "裁加配套產量如下:   " + row["capacity"] + "雙";
                            go(message);
                            Thread.Sleep(1000);
                            Formlean formlean = new Formlean();
                            formlean.gogo1();
                            int num1 = Formlean.lean1chat;
                            //int num2 = Formlean.lean2chat;  //開線解除注釋
                            //int num3 = Formlean.lean3chat;  //開線解除注釋
                            int num4;
                            if (row["capacity"] is int intValue)
                                num4 = intValue;
                            else
                                num4 = 0;
                            sum = num1 +/*num2+num3+*/num4;
                            message1 = "      合計:   " + sum + "雙";
                            go(message1);
                            if (row["comment"] is string stringValue)
                                message2 = stringValue;
                            else
                                message2 = "裁加今日無補充說明";
                            Thread.Sleep(1000);
                            go(message2);
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
                        string checkExistingdate = "SELECT COUNT(*) FROM cut WHERE c_date = @currentTime";
                        using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                        {
                            checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                            int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                            if (count > 0)
                            {
                                string checkExistingcomment = "SELECT comment FROM cut WHERE c_date=@currentTime ";
                                using (NpgsqlCommand checkCommandcomment = new NpgsqlCommand(checkExistingcomment, conn))
                                {
                                    checkCommandcomment.Parameters.AddWithValue("@currentTime", currentTime);
                                    object result = checkCommandcomment.ExecuteScalar();
                                    if ((count > 0) && (result != null && result != DBNull.Value))
                                    {
                                        string updateSql = "UPDATE cut SET comment = @comment WHERE c_date = @currentTime";
                                        using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                        {
                                            updateCommand.Parameters.AddWithValue("@comment", textBoxcomment.Text.Trim());
                                            updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                            updateCommand.ExecuteNonQuery();
                                        }
                                        MessageBox.Show("今日補充說明已更新");
                                        DataGridViewChat();
                                        conn.Close();
                                        textBoxcomment.Text = "";
                                        textBoxcomment.Focus();
                                    }
                                    else
                                    {
                                        string updateSql = "UPDATE cut SET comment = @comment WHERE c_date=@currentTime";
                                        using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                        {
                                            updateCommand.Parameters.AddWithValue("@comment", textBoxcomment.Text.Trim());
                                            updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                            updateCommand.ExecuteNonQuery();
                                        }
                                        MessageBox.Show("補充說明提交成功");
                                        DataGridViewChat();
                                        conn.Close();
                                        textBoxcomment.Text = "";
                                        textBoxcomment.Focus();
                                    }
                                }
                            }
                            else
                            {
                                string insertSql = "INSERT INTO cut (c_date, comment) VALUES (@currentTime, @comment)";
                                using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                {
                                    insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                    insertCommand.Parameters.AddWithValue("@comment", textBoxcomment.Text.Trim());
                                    insertCommand.ExecuteNonQuery();
                                }
                                MessageBox.Show("補充說明提交成功");
                                DataGridViewChat();
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

        private void Formchat_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
        }
    }
}
