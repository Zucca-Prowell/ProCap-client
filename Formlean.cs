using System.Drawing.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection.Metadata;
using Npgsql;
using System.Data;



namespace PROCAP_CLIENT
{
    public partial class Formlean : Form
    {
        public static int lean1chat;
        public static int lean2chat;
        public static int lean3chat;
        TextBox[] textcheck = new TextBox[5];
        bool[] boolarray = new bool[8];
        bool judgement = false;
        int k = 0;
        bool flag = false;
        bool result = false;
        int counted;

        public Formlean()
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
        protected internal async Task go(string message)
        {
            Prowell_slack_bot.slack.slack_init();
            await Prowell_slack_bot.slack.post_messageProCap(message);

        }
        protected internal async Task gogo1()
        {
            string message1;
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                DateTime currentTime = DateTime.Now.Date;
                conn.Open();
                string leanchat = "select lean01,lean02,lean03 from cut where c_date=@currentTime";

                using (NpgsqlCommand cmd1 = new NpgsqlCommand(leanchat, conn))

                {
                    cmd1.Parameters.AddWithValue("@currentTime", currentTime);

                    using (NpgsqlDataReader reader1 = cmd1.ExecuteReader())

                    {
                        while (reader1.Read())
                        {
                            Dictionary<string, object> row1 = new Dictionary<string, object>();
                            for (int i = 0; i < reader1.FieldCount; i++)
                            {
                                string columnName = reader1.GetName(i);
                                object columnValue = reader1[i];
                                row1[columnName] = columnValue;
                            }
                            if (row1["lean01"] is int intValue)
                                lean1chat = intValue;
                            else
                                lean1chat = 0;
                            //lean2chat = (int?)row1["lean02"]??0;//lean2線開了解除注釋
                            //lean3chat = (int?)row1["lean03"]??0;//lean3線開了解除注釋
                            message1 = "Lean1線產量:   " + lean1chat + "雙"; //+ "Lean2線產量:   " + row1["lean02"] + "雙" + "\n" + "Lean3線產量:   " + row1["lean03"] + "雙"+"\n";
                            go(message1);
                        }
                    }
                }
            }
        }
        protected internal async Task gogo2()
        {
            int sum;
            string message2;
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                DateTime currentTime = DateTime.Now.Date;
                conn.Open();
                string leanstitch = "select lean1線,lean2線,lean3線 from stitch where c_date=@currentTime";
                using (NpgsqlCommand cmd2 = new NpgsqlCommand(leanstitch, conn))
                {
                    cmd2.Parameters.AddWithValue("@currentTime", currentTime);
                    using (NpgsqlDataReader reader2 = cmd2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            Dictionary<string, object> row2 = new Dictionary<string, object>();
                            for (int i = 0; i < reader2.FieldCount; i++)
                            {
                                string columnName = reader2.GetName(i);
                                object columnValue = reader2[i];
                                row2[columnName] = columnValue;
                            }
                            sum = (int)row2["lean1線"];//+(int)row2["lean2線"]+(int)row2["lean3線"];
                            message2 = "大家好！" + DateTime.Now.ToString("yyyy-MM-dd") + "Lean" + "產量如下:" + "\n" + "Lean1線針車:          " + row2["lean1線"] + "雙";/*+ "\n" + "Lean2線針車:   " + row2["lean2線"]+ "\n" + "Lean3線針車:   " + row2["lean3線"]*///+ "\n" + "   合計:                  " + sum + "雙";
                            go(message2);
                        }


                    }
                }
            }

        }
        protected internal async Task gogo3()
        {
            string message3;
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                DateTime currentTime = DateTime.Now.Date;
                conn.Open();
                string leanassemble = "select lean01,lean02,lean03 from assemble where c_date=@currentTime";
                using (NpgsqlCommand cmd3 = new NpgsqlCommand(leanassemble, conn))
                {
                    cmd3.Parameters.AddWithValue("@currentTime", currentTime);
                    using (NpgsqlDataReader reader3 = cmd3.ExecuteReader())
                    {
                        while (reader3.Read())
                        {
                            Dictionary<string, object> row3 = new Dictionary<string, object>();
                            for (int i = 0; i < reader3.FieldCount; i++)
                            {
                                string columnName = reader3.GetName(i);
                                object columnValue = reader3[i];
                                row3[columnName] = columnValue;
                            }

                            message3 = "Lean1線成型產量:    " + row3["lean01"] + "雙";//+ "\n" + "lean02:   " + row3["lean02"] + "\n" + "lean03:   " + row3["lean03"];
                            go(message3);
                        }
                    }
                }
            }
        }
        protected internal async Task gogo4()
        {
            string messagecomment;
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                DateTime currentTime = DateTime.Now.Date;
                conn.Open();
                string sqlcomment = "select leancomment from cut where c_date=@currentTime";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sqlcomment, conn))
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
                            if (row["leancomment"] is string stringValue)
                                messagecomment = stringValue;
                            else
                                messagecomment = "Lean今日無補充說明";
                            go(messagecomment);
                        }
                    }
                }
            }
        }
        private void Formlean_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.Resize += new EventHandler(Formlean_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
        }

        private void initializedata()
        {
            textcheck[0] = textBoxstitch1;
            textcheck[1] = textBoxstitch2;
            textcheck[2] = textBoxstitch3;
            textcheck[3] = textBoxstitch4;
            textcheck[4] = textBoxassemble;
            boolarray[0] = radioButtonlean1.Checked;
            boolarray[1] = radioButtonlean2.Checked;
            boolarray[2] = radioButtonlean3.Checked;
            boolarray[3] = string.IsNullOrEmpty(textcheck[0].Text.Trim());
            boolarray[4] = string.IsNullOrEmpty(textcheck[1].Text.Trim());
            boolarray[5] = string.IsNullOrEmpty(textcheck[2].Text.Trim());
            boolarray[6] = string.IsNullOrEmpty(textcheck[3].Text.Trim());
            boolarray[7] = string.IsNullOrEmpty(textcheck[4].Text.Trim());
        }
        private bool judgenull()
        {

            for (int i = 0; i < 3; i++)
            {
                result = (result || boolarray[i]);
            }
            if (!result)
            {
                MessageBox.Show("請選擇lean線線別");
            }
            else
            {
                for (int j = 3; j < boolarray.Length; j++)
                {
                    if (boolarray[j])
                    {
                        MessageBox.Show("有欄為空");
                        textcheck[j - 3].Focus();
                        flag = true;
                        k = 0;
                        break;
                    }
                    else
                        k++;
                }
                if (result && (k == textcheck.Length))
                {
                    judgement = true;
                    result = false;
                    k = 0;
                }
            }
            return judgement;
        }
        private int merge()
        {
            //合併針一針二針三針四
            int leanstitch;
            int leanstitch1 = int.Parse(textBoxstitch1.Text.Trim());
            int leanstitch2 = int.Parse(textBoxstitch2.Text.Trim());
            int leanstitch3 = int.Parse(textBoxstitch3.Text.Trim());
            int leanstitch4 = int.Parse(textBoxstitch4.Text.Trim());
            leanstitch = leanstitch1 + leanstitch2 + leanstitch3 + leanstitch4;
            return leanstitch;
        }
        private void buttonsubmit_Click(object sender, EventArgs e)
        {
            initializedata();
            judgenull();
            if (judgement)
            {
                string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        DateTime currentTime = DateTime.Now.Date;
                        if (radioButtonlean1.Checked)
                        {
                            string checkExistingdate_S = "SELECT COUNT(*) FROM stitch WHERE c_date=@c_date";
                            string checkExistingdata_S = "SELECT lean1線 FROM stitch";
                            string checkExistingdate_A = "SELECT COUNT(*) FROM assemble WHERE c_date = @c_date";
                            string checkExistingdata_A = "SELECT lean01 FROM assemble ";
                            using (NpgsqlCommand checkCommanddate_A = new NpgsqlCommand(checkExistingdate_A, conn))
                            using (NpgsqlCommand checkCommanddata_A = new NpgsqlCommand(checkExistingdata_A, conn))
                            using (NpgsqlCommand checkCommanddate_S = new NpgsqlCommand(checkExistingdate_S, conn))
                            using (NpgsqlCommand checkCommanddata_S = new NpgsqlCommand(checkExistingdata_S, conn))
                            {
                                checkCommanddate_A.Parameters.AddWithValue("c_date", currentTime);
                                checkCommanddate_S.Parameters.AddWithValue("c_date", currentTime);
                                // 检查是否存在相同时间戳的记录
                                int count_A = Convert.ToInt32(checkCommanddate_A.ExecuteScalar());
                                int count_S = Convert.ToInt32(checkCommanddate_S.ExecuteScalar());
                                object result_A = checkCommanddata_A.ExecuteScalar();
                                object result_S = checkCommanddata_S.ExecuteScalar();
                                if (count_A > 0)
                                {
                                    // 如果存在相同时间戳的记录，执行更新操作
                                    string updateSql_A = "UPDATE assemble SET lean01 = @lean01 WHERE c_date = @c_date";
                                    using (NpgsqlCommand updateCommand_A = new NpgsqlCommand(updateSql_A, conn))
                                    {
                                        updateCommand_A.Parameters.AddWithValue("lean01", int.Parse(textBoxassemble.Text.Trim()));
                                        updateCommand_A.Parameters.AddWithValue("c_date", currentTime);
                                        updateCommand_A.ExecuteNonQuery();
                                    }

                                }
                                else
                                {
                                    // 如果不存在相同时间戳的记录，执行插入操作
                                    string insertSql_A = "INSERT INTO assemble (c_date,lean01) VALUES (@c_date, @lean01)";
                                    using (NpgsqlCommand insertCommand_A = new NpgsqlCommand(insertSql_A, conn))
                                    {
                                        insertCommand_A.Parameters.AddWithValue("c_date", currentTime);
                                        insertCommand_A.Parameters.AddWithValue("lean01", int.Parse(textBoxassemble.Text.Trim()));
                                        insertCommand_A.ExecuteNonQuery();
                                    }

                                }
                                //-------------------------------------------------------------------
                                // 检查是否存在相同时间戳的记录
                                if (count_S > 0)
                                {
                                    // 如果存在相同时间戳的记录，执行更新操作
                                    string updateSql_S = "UPDATE stitch SET lean1線 = @lean1線 WHERE c_date = @c_date";
                                    using (NpgsqlCommand updateCommand_S = new NpgsqlCommand(updateSql_S, conn))
                                    {
                                        updateCommand_S.Parameters.AddWithValue("lean1線", merge());
                                        updateCommand_S.Parameters.AddWithValue("c_date", currentTime);
                                        updateCommand_S.ExecuteNonQuery();
                                    }

                                }
                                else
                                {
                                    // 如果不存在相同时间戳的记录，执行插入操作
                                    string insertSql_S = "INSERT INTO stitch (c_date,lean1線) VALUES (@c_date, @lean1線)";
                                    using (NpgsqlCommand insertCommand_S = new NpgsqlCommand(insertSql_S, conn))
                                    {
                                        insertCommand_S.Parameters.AddWithValue("c_date", currentTime);
                                        insertCommand_S.Parameters.AddWithValue("lean1線", merge());
                                        insertCommand_S.ExecuteNonQuery();
                                    }

                                }
                                conn.Close();
                                MessageBox.Show("數據提交成功");
                                judgement = false;
                                flag = false;
                                foreach (TextBox a in textcheck)
                                {
                                    a.Text = "";
                                }
                                textBoxstitch1.Focus();
                            }
                        }

                        if (radioButtonlean2.Checked)
                        {
                            string checkExistingdate_S2 = "SELECT COUNT(*) FROM stitch WHERE c_date=@c_date";
                            string checkExistingdata_S2 = "SELECT lean2線 FROM stitch";
                            string checkExistingdate_A2 = "SELECT COUNT(*) FROM assemble WHERE c_date = @c_date";
                            string checkExistingdata_A2 = "SELECT lean02 FROM assemble ";
                            using (NpgsqlCommand checkCommanddate_A = new NpgsqlCommand(checkExistingdate_A2, conn))
                            using (NpgsqlCommand checkCommanddata_A = new NpgsqlCommand(checkExistingdata_A2, conn))
                            using (NpgsqlCommand checkCommanddate_S = new NpgsqlCommand(checkExistingdate_S2, conn))
                            using (NpgsqlCommand checkCommanddata_S = new NpgsqlCommand(checkExistingdata_S2, conn))
                            {
                                checkCommanddate_A.Parameters.AddWithValue("c_date", currentTime);
                                checkCommanddate_S.Parameters.AddWithValue("c_date", currentTime);
                                // 检查是否存在相同时间戳的记录
                                int count_A = Convert.ToInt32(checkCommanddate_A.ExecuteScalar());
                                int count_S = Convert.ToInt32(checkCommanddate_S.ExecuteScalar());
                                object result_A = checkCommanddata_A.ExecuteScalar();
                                object result_S = checkCommanddata_S.ExecuteScalar();
                                if (count_A > 0)
                                {
                                    // 如果存在相同时间戳的记录，执行更新操作
                                    string updateSql_A = "UPDATE assemble SET lean02 = @lean02 WHERE c_date = @c_date";
                                    using (NpgsqlCommand updateCommand_A = new NpgsqlCommand(updateSql_A, conn))
                                    {
                                        updateCommand_A.Parameters.AddWithValue("lean02", int.Parse(textBoxassemble.Text.Trim()));
                                        updateCommand_A.Parameters.AddWithValue("c_date", currentTime);
                                        updateCommand_A.ExecuteNonQuery();
                                    }

                                }
                                else
                                {
                                    // 如果不存在相同时间戳的记录，执行插入操作
                                    string insertSql_A = "INSERT INTO assemble (c_date,lean02) VALUES (@c_date, @lean02)";
                                    using (NpgsqlCommand insertCommand_A = new NpgsqlCommand(insertSql_A, conn))
                                    {
                                        insertCommand_A.Parameters.AddWithValue("c_date", currentTime);
                                        insertCommand_A.Parameters.AddWithValue("lean02", int.Parse(textBoxassemble.Text.Trim()));
                                        insertCommand_A.ExecuteNonQuery();
                                    }

                                }
                                //-------------------------------------------------------------------
                                // 检查是否存在相同时间戳的记录
                                if (count_S > 0)
                                {
                                    // 如果存在相同时间戳的记录，执行更新操作
                                    string updateSql_S = "UPDATE stitch SET lean2線 = @lean2線 WHERE c_date = @c_date";
                                    using (NpgsqlCommand updateCommand_S = new NpgsqlCommand(updateSql_S, conn))
                                    {
                                        updateCommand_S.Parameters.AddWithValue("lean2線", merge());
                                        updateCommand_S.Parameters.AddWithValue("c_date", currentTime);
                                        updateCommand_S.ExecuteNonQuery();
                                    }

                                }
                                else
                                {
                                    // 如果不存在相同时间戳的记录，执行插入操作
                                    string insertSql_S = "INSERT INTO stitch (c_date,lean2線) VALUES (@c_date, @lean2線)";
                                    using (NpgsqlCommand insertCommand_S = new NpgsqlCommand(insertSql_S, conn))
                                    {
                                        insertCommand_S.Parameters.AddWithValue("c_date", currentTime);
                                        insertCommand_S.Parameters.AddWithValue("lean2線", merge());
                                        insertCommand_S.ExecuteNonQuery();
                                    }

                                }
                                conn.Close();
                                MessageBox.Show("數據提交成功");
                                judgement = false;
                                flag = false;
                                foreach (TextBox a in textcheck)
                                {
                                    a.Text = "";
                                }
                                textBoxstitch1.Focus();
                            }
                        }
                        if (radioButtonlean3.Checked)
                        {
                            string checkExistingdate_S3 = "SELECT COUNT(*) FROM stitch WHERE c_date=@c_date";
                            string checkExistingdata_S3 = "SELECT lean1線 FROM stitch";
                            string checkExistingdate_A3 = "SELECT COUNT(*) FROM assemble WHERE c_date = @c_date";
                            string checkExistingdata_A3 = "SELECT lean01 FROM assemble ";
                            using (NpgsqlCommand checkCommanddate_A = new NpgsqlCommand(checkExistingdate_A3, conn))
                            using (NpgsqlCommand checkCommanddata_A = new NpgsqlCommand(checkExistingdata_A3, conn))
                            using (NpgsqlCommand checkCommanddate_S = new NpgsqlCommand(checkExistingdate_S3, conn))
                            using (NpgsqlCommand checkCommanddata_S = new NpgsqlCommand(checkExistingdata_S3, conn))
                            {
                                checkCommanddate_A.Parameters.AddWithValue("c_date", currentTime);
                                checkCommanddate_S.Parameters.AddWithValue("c_date", currentTime);
                                // 检查是否存在相同时间戳的记录
                                int count_A = Convert.ToInt32(checkCommanddate_A.ExecuteScalar());
                                int count_S = Convert.ToInt32(checkCommanddate_S.ExecuteScalar());
                                object result_A = checkCommanddata_A.ExecuteScalar();
                                object result_S = checkCommanddata_S.ExecuteScalar();
                                if (count_A > 0)
                                {
                                    // 如果存在相同时间戳的记录，执行更新操作
                                    string updateSql_A = "UPDATE assemble SET lean03 = @lean03 WHERE c_date = @c_date";
                                    using (NpgsqlCommand updateCommand_A = new NpgsqlCommand(updateSql_A, conn))
                                    {
                                        updateCommand_A.Parameters.AddWithValue("lean03", int.Parse(textBoxassemble.Text.Trim()));
                                        updateCommand_A.Parameters.AddWithValue("c_date", currentTime);
                                        updateCommand_A.ExecuteNonQuery();
                                    }

                                }
                                else
                                {
                                    // 如果不存在相同时间戳的记录，执行插入操作
                                    string insertSql_A = "INSERT INTO assemble (c_date,lean03) VALUES (@c_date, @lean03)";
                                    using (NpgsqlCommand insertCommand_A = new NpgsqlCommand(insertSql_A, conn))
                                    {
                                        insertCommand_A.Parameters.AddWithValue("c_date", currentTime);
                                        insertCommand_A.Parameters.AddWithValue("lean03", int.Parse(textBoxassemble.Text.Trim()));
                                        insertCommand_A.ExecuteNonQuery();
                                    }

                                }
                                //-------------------------------------------------------------------
                                // 检查是否存在相同时间戳的记录
                                if (count_S > 0)
                                {
                                    // 如果存在相同时间戳的记录，执行更新操作
                                    string updateSql_S = "UPDATE stitch SET lean3線 = @lean3線 WHERE c_date = @c_date";
                                    using (NpgsqlCommand updateCommand_S = new NpgsqlCommand(updateSql_S, conn))
                                    {
                                        updateCommand_S.Parameters.AddWithValue("lean3線", merge());
                                        updateCommand_S.Parameters.AddWithValue("c_date", currentTime);
                                        updateCommand_S.ExecuteNonQuery();
                                    }

                                }
                                else
                                {
                                    // 如果不存在相同时间戳的记录，执行插入操作
                                    string insertSql_S = "INSERT INTO stitch (c_date,lean3線) VALUES (@c_date, @lean3線)";
                                    using (NpgsqlCommand insertCommand_S = new NpgsqlCommand(insertSql_S, conn))
                                    {
                                        insertCommand_S.Parameters.AddWithValue("c_date", currentTime);
                                        insertCommand_S.Parameters.AddWithValue("lean3線", merge());
                                        insertCommand_S.ExecuteNonQuery();
                                    }

                                }
                                conn.Close();
                                MessageBox.Show("數據提交成功");
                                judgement = false;
                                flag = false;
                                foreach (TextBox a in textcheck)
                                {
                                    a.Text = "";
                                }
                                textBoxstitch1.Focus();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("數據庫連接失敗: " + ex.Message);
                    foreach (TextBox a in textcheck)
                    {
                        a.Text = "";
                    }
                    textBoxstitch1.Focus();
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("yyyy-MM-dd" + "產量");
        }
        private void textBoxstitch1_KeyDown(object sender, KeyEventArgs e)
        {
            initializedata();
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        if (!flag)
                        {
                            SendKeys.Send("{tab}");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(textBoxstitch1.Text))
                                SendKeys.Send("{tab}");
                            else
                            {
                                for (int i = 3; i < boolarray.Length; i++)
                                {
                                    if (boolarray[i])
                                    {
                                        textcheck[i - 3].Focus();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.Up: radioButtonlean3.Focus(); break;
                case Keys.Down: textBoxstitch2.Focus(); break;
            }
        }

        private void textBoxstitch2_KeyDown(object sender, KeyEventArgs e)
        {
            initializedata();
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        if (!flag)
                        {
                            SendKeys.Send("{tab}");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(textBoxstitch2.Text))
                                SendKeys.Send("{tab}");
                            else
                            {
                                for (int i = 3; i < boolarray.Length; i++)
                                {
                                    if (boolarray[i])
                                    {
                                        textcheck[i - 3].Focus();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.Up: textBoxstitch1.Focus(); break;
                case Keys.Down: textBoxstitch3.Focus(); break;
                case Keys.Back: { if (string.IsNullOrEmpty(textBoxstitch2.Text)) { textBoxstitch1.Focus(); } } break;
            }
        }
        private void textBoxstitch3_KeyDown(object sender, KeyEventArgs e)
        {
            initializedata();
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        if (!flag)
                        {
                            SendKeys.Send("{tab}");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(textBoxstitch3.Text))
                                SendKeys.Send("{tab}");
                            else
                            {
                                for (int i = 3; i < boolarray.Length; i++)
                                {
                                    if (boolarray[i])
                                    {
                                        textcheck[i - 3].Focus();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.Up: textBoxstitch2.Focus(); break;
                case Keys.Down: textBoxstitch4.Focus(); break;
                case Keys.Back: { if (string.IsNullOrEmpty(textBoxstitch3.Text)) { textBoxstitch2.Focus(); } } break;
            }
        }

        private void textBoxstitch4_KeyDown(object sender, KeyEventArgs e)
        {
            initializedata();
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        if (!flag)
                        {
                            SendKeys.Send("{tab}");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(textBoxstitch4.Text))
                                SendKeys.Send("{tab}");
                            else
                            {
                                for (int i = 3; i < boolarray.Length; i++)
                                {
                                    if (boolarray[i])
                                    {
                                        textcheck[i - 3].Focus();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Keys.Up: textBoxstitch3.Focus(); break;
                case Keys.Down: textBoxassemble.Focus(); break;
                case Keys.Back: { if (string.IsNullOrEmpty(textBoxstitch4.Text)) { textBoxstitch3.Focus(); } } break;
            }
        }
        private void textBoxassemble_KeyDown(object sender, KeyEventArgs e)
        {
            initializedata();
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    {
                        if (!flag)
                        {
                            SendKeys.Send("{tab}");
                        }
                        else
                        {
                            for (int i = 3; i < boolarray.Length; i++)
                            {
                                if (boolarray[i])
                                {
                                    textcheck[i - 3].Focus();
                                    break;
                                }
                            }
                        }
                    }
                    break;
                case Keys.Up: textBoxstitch4.Focus(); break;
                case Keys.Down: buttonsubmit.Focus(); break;
                case Keys.Back: { if (string.IsNullOrEmpty(textBoxassemble.Text)) { textBoxstitch4.Focus(); } } break;
            }
        }
        private void Formlean_KeyDown(object sender, KeyEventArgs e)
        {
            initializedata();
            counted = 3;
            for (int count = 3; count < boolarray.Length; count++)
            {
                if (boolarray[count])
                    break;
                else
                    counted++;
            }
            switch (e.KeyCode)
            {
                case Keys.Escape: Close(); break;
                case Keys.Enter: { if (counted == boolarray.Length) { buttonsubmit_Click(sender, e); counted = 3; } } break;
            }
        }

        private void buttonclear_Click(object sender, EventArgs e)
        {
            initializedata();
            foreach (TextBox a in textcheck)
            {
                a.Text = "";
            }
            textBoxstitch1.Focus();
        }

        private void buttonmessage_Click(object sender, EventArgs e)
        {
            gogo2();
            gogo3();
            gogo4();
            MessageBox.Show("lean線今日產量發送成功！");
        }

        private void buttoncomment_Click(object sender, EventArgs e)
        {
            Formchat formchat = new Formchat();
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
                                string checkExistingcomment = "SELECT leancomment FROM cut WHERE c_date=@currentTime ";
                                using (NpgsqlCommand checkCommandcomment = new NpgsqlCommand(checkExistingcomment, conn))
                                {
                                    checkCommandcomment.Parameters.AddWithValue("@currentTime", currentTime);
                                    object result = checkCommandcomment.ExecuteScalar();
                                    if ((count > 0) && (result != null && result != DBNull.Value))
                                    {
                                        string updateSql = "UPDATE cut SET leancomment = @leancomment WHERE c_date = @currentTime";
                                        using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                        {
                                            updateCommand.Parameters.AddWithValue("@leancomment", textBoxcomment.Text.Trim());
                                            updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                            updateCommand.ExecuteNonQuery();
                                        }
                                        MessageBox.Show("今日補充說明已更新");
                                        formchat.DataGridViewChat();
                                        conn.Close();
                                        textBoxcomment.Text = "";
                                        textBoxcomment.Focus();
                                    }
                                    else
                                    {
                                        string updateSql = "UPDATE cut SET leancomment = @leancomment WHERE c_date=@currentTime";
                                        using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                        {
                                            updateCommand.Parameters.AddWithValue("@leancomment", textBoxcomment.Text.Trim());
                                            updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                            updateCommand.ExecuteNonQuery();
                                        }
                                        MessageBox.Show("補充說明提交成功");
                                        formchat.DataGridViewChat();
                                        conn.Close();
                                        textBoxcomment.Text = "";
                                        textBoxcomment.Focus();
                                    }
                                }
                            }
                            else
                            {
                                string insertSql = "INSERT INTO cut (c_date, leancomment) VALUES (@currentTime, @leancomment)";
                                using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                                {
                                    insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                    insertCommand.Parameters.AddWithValue("@leancomment", textBoxcomment.Text.Trim());
                                    insertCommand.ExecuteNonQuery();
                                }
                                MessageBox.Show("補充說明提交成功");
                                formchat.DataGridViewChat();
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
                textBoxcomment.Text = "";
            }
        }

        private void textBoxcomment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttoncomment_Click(sender, e);
        }

        private void Formlean_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
        }
    }
}
