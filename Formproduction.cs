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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROCAP_CLIENT
{
    public partial class Formproduction : Form
    {
        public Formproduction()
        {
            InitializeComponent();
        }
        float X, Y;
        bool[] judge = new bool[7];
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
                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void Formproduction_Load(object sender, EventArgs e)
        {
            timer1.Start();
            DataGridViewProduction();
            showdata();
            this.Resize += new EventHandler(Formproduction_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
        }

        private void Formproduction_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
        }
        private void DataGridViewProduction()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    string sqlpro = "select c_date,inp,out,totalout,inventory,fullorder,fetched,comment from pwh";
                    NpgsqlCommand cmd = new NpgsqlCommand(sqlpro, conn);
                    NpgsqlDataAdapter adp = new NpgsqlDataAdapter(cmd);
                    DataTable dataTable_P = new DataTable();
                    adp.Fill(dataTable_P);
                    dataGridView1.DataSource = dataTable_P;
                    dataGridView1.Sort(dataGridView1.Columns["c_date"], ListSortDirection.Descending);
                    dataGridView1.Columns["c_date"].HeaderText = "日期";
                    dataGridView1.Columns["inp"].HeaderText = "入庫";
                    dataGridView1.Columns["out"].HeaderText = "出貨";
                    dataGridView1.Columns["totalout"].HeaderText = "累計出貨";
                    dataGridView1.Columns["inventory"].HeaderText = "庫存";
                    dataGridView1.Columns["fullorder"].HeaderText = "已滿單";
                    dataGridView1.Columns["fetched"].HeaderText = "已拿";
                    dataGridView1.Columns["comment"].HeaderText = "補充說明";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }
        }
        private void inpDB()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    string checkExistingdate = "SELECT COUNT(*) FROM pwh WHERE c_date = @currentTime";
                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                    {
                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                        if (count > 0)
                        {
                            string checkExistingdata = "SELECT inp FROM pwh WHERE c_date=@currentTime ";
                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                            {
                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                object result = checkCommanddata.ExecuteScalar();
                                if ((count > 0) && (result != null && result != DBNull.Value))
                                {
                                    string updateSql = "UPDATE pwh SET inp = @inp WHERE c_date = @currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@inp", int.Parse(textBoxinp.Text.Trim()));
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[0] = true;
                                    //MessageBox.Show("今日數據已更新");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                                else
                                {
                                    string updateSql = "UPDATE pwh SET inp = @inp WHERE c_date=@currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@inp", int.Parse(textBoxinp.Text.Trim()));
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[0] = false;
                                    //MessageBox.Show("數據提交成功");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                            }
                        }

                        else
                        {
                            string insertSql = "INSERT INTO pwh (c_date, inp) VALUES (@currentTime, @inp)";
                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                            {
                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                insertCommand.Parameters.AddWithValue("@inp", int.Parse(textBoxinp.Text.Trim()));
                                insertCommand.ExecuteNonQuery();
                            }
                            judge[0] = false;
                            //MessageBox.Show("數據提交成功");
                            DataGridViewProduction();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }
        }
        private void outDB()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    string checkExistingdate = "SELECT COUNT(*) FROM pwh WHERE c_date = @currentTime";
                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                    {
                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                        if (count > 0)
                        {
                            string checkExistingdata = "SELECT out FROM pwh WHERE c_date=@currentTime ";
                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                            {
                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                object result = checkCommanddata.ExecuteScalar();
                                if ((count > 0) && (result != null && result != DBNull.Value))
                                {
                                    string updateSql = "UPDATE pwh SET out = @out WHERE c_date = @currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@out", int.Parse(textBoxout.Text.Trim()));
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[1] = true;
                                    //MessageBox.Show("今日數據已更新");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                                else
                                {
                                    string updateSql = "UPDATE pwh SET out = @out WHERE c_date=@currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@out", int.Parse(textBoxout.Text.Trim()));
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[1] = false;
                                    //MessageBox.Show("數據提交成功");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                            }
                        }

                        else
                        {
                            string insertSql = "INSERT INTO pwh (c_date, out) VALUES (@currentTime, @out)";
                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                            {
                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                insertCommand.Parameters.AddWithValue("@out", int.Parse(textBoxout.Text.Trim()));
                                insertCommand.ExecuteNonQuery();
                            }
                            judge[1] = false;
                            //MessageBox.Show("數據提交成功");
                            DataGridViewProduction();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }

        }
        private void totaloutDB()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    string checkExistingdate = "SELECT COUNT(*) FROM pwh WHERE c_date = @currentTime";
                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                    {
                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                        if (count > 0)
                        {
                            string checkExistingdata = "SELECT totalout FROM pwh WHERE c_date=@currentTime ";
                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                            {
                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                object result = checkCommanddata.ExecuteScalar();
                                if ((count > 0) && (result != null && result != DBNull.Value))
                                {
                                    string updateSql = "UPDATE pwh SET totalout = @totalout WHERE c_date = @currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@totalout", int.Parse(textBoxtotalout.Text.Trim()));
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[2] = true;
                                    //MessageBox.Show("今日數據已更新");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                                else
                                {
                                    string updateSql = "UPDATE pwh SET totalout = @totalout WHERE c_date=@currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@totalout", int.Parse(textBoxtotalout.Text.Trim()));
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[2] = false;
                                    //MessageBox.Show("數據提交成功");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                            }
                        }

                        else
                        {
                            string insertSql = "INSERT INTO pwh (c_date, totalout) VALUES (@currentTime, @totalout)";
                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                            {
                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                insertCommand.Parameters.AddWithValue("@totalout", int.Parse(textBoxtotalout.Text.Trim()));
                                insertCommand.ExecuteNonQuery();
                            }
                            judge[2] = false;
                            //MessageBox.Show("數據提交成功");
                            DataGridViewProduction();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }

        }
        private void inventoryDB()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    string checkExistingdate = "SELECT COUNT(*) FROM pwh WHERE c_date = @currentTime";
                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                    {
                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                        if (count > 0)
                        {
                            string checkExistingdata = "SELECT inventory FROM pwh WHERE c_date=@currentTime ";
                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                            {
                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                object result = checkCommanddata.ExecuteScalar();
                                if ((count > 0) && (result != null && result != DBNull.Value))
                                {
                                    string updateSql = "UPDATE pwh SET inventory = @inventory WHERE c_date = @currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@inventory", int.Parse(textBoxinventory.Text.Trim()));
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[3] = true;
                                    //MessageBox.Show("今日數據已更新");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                                else
                                {
                                    string updateSql = "UPDATE pwh SET inventory = @inventory WHERE c_date=@currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@inventory", int.Parse(textBoxinventory.Text.Trim()));
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[3] = false;
                                    //MessageBox.Show("數據提交成功");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                            }
                        }

                        else
                        {
                            string insertSql = "INSERT INTO pwh (c_date, inventory) VALUES (@currentTime, @inventory)";
                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                            {
                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                insertCommand.Parameters.AddWithValue("@inventory", int.Parse(textBoxinventory.Text.Trim()));
                                insertCommand.ExecuteNonQuery();
                            }
                            judge[3] = true;
                            //MessageBox.Show("數據提交成功");
                            DataGridViewProduction();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }


        }
        private void fullorderDB()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    string checkExistingdate = "SELECT COUNT(*) FROM pwh WHERE c_date = @currentTime";
                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                    {
                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                        if (count > 0)
                        {
                            string checkExistingdata = "SELECT fullorder FROM pwh WHERE c_date=@currentTime ";
                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                            {
                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                object result = checkCommanddata.ExecuteScalar();
                                if ((count > 0) && (result != null && result != DBNull.Value))
                                {
                                    string updateSql = "UPDATE pwh SET fullorder = @fullorder WHERE c_date = @currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@fullorder", int.Parse(textBoxfullorder.Text.Trim()));
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[4] = true;
                                    //MessageBox.Show("今日數據已更新");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                                else
                                {
                                    string updateSql = "UPDATE pwh SET fullorder = @fullorder WHERE c_date=@currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@fullorder", int.Parse(textBoxfullorder.Text.Trim()));
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[4] = false;
                                    //MessageBox.Show("數據提交成功");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                            }
                        }

                        else
                        {
                            string insertSql = "INSERT INTO pwh (c_date, fullorder) VALUES (@currentTime, @fullorder)";
                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                            {
                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                insertCommand.Parameters.AddWithValue("@fullorder", int.Parse(textBoxfullorder.Text.Trim()));
                                insertCommand.ExecuteNonQuery();
                            }
                            judge[4] = true;
                            //MessageBox.Show("數據提交成功");
                            DataGridViewProduction();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }

        }
        private void fetchedDB()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    string checkExistingdate = "SELECT COUNT(*) FROM pwh WHERE c_date = @currentTime";
                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                    {
                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                        if (count > 0)
                        {
                            string checkExistingdata = "SELECT fetched FROM pwh WHERE c_date=@currentTime ";
                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                            {
                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                object result = checkCommanddata.ExecuteScalar();
                                if ((count > 0) && (result != null && result != DBNull.Value))
                                {
                                    string updateSql = "UPDATE pwh SET fetched = @fetched WHERE c_date = @currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@fetched", int.Parse(textBoxfetched.Text.Trim()));
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[5] = true;
                                    //MessageBox.Show("今日數據已更新");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                                else
                                {
                                    string updateSql = "UPDATE pwh SET fetched = @fetched WHERE c_date=@currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@fetched", int.Parse(textBoxfetched.Text.Trim()));
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[5] = false;
                                    //MessageBox.Show("數據提交成功");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                            }
                        }

                        else
                        {
                            string insertSql = "INSERT INTO pwh (c_date, fetched) VALUES (@currentTime, @fetched)";
                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                            {
                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                insertCommand.Parameters.AddWithValue("@fetched", int.Parse(textBoxfetched.Text.Trim()));
                                insertCommand.ExecuteNonQuery();
                            }
                            judge[5] = false;
                            //MessageBox.Show("數據提交成功");
                            DataGridViewProduction();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }

        }
        private void commentDB()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    string checkExistingdate = "SELECT COUNT(*) FROM pwh WHERE c_date = @currentTime";
                    using (NpgsqlCommand checkCommanddate = new NpgsqlCommand(checkExistingdate, conn))
                    {
                        checkCommanddate.Parameters.AddWithValue("@currentTime", currentTime);
                        int count = Convert.ToInt32(checkCommanddate.ExecuteScalar());
                        if (count > 0)
                        {
                            string checkExistingdata = "SELECT comment FROM pwh WHERE c_date=@currentTime ";
                            using (NpgsqlCommand checkCommanddata = new NpgsqlCommand(checkExistingdata, conn))
                            {
                                checkCommanddata.Parameters.AddWithValue("@currentTime", currentTime);
                                object result = checkCommanddata.ExecuteScalar();
                                if ((count > 0) && (result != null && result != DBNull.Value))
                                {
                                    string updateSql = "UPDATE pwh SET comment = @comment WHERE c_date = @currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@comment", textBoxcomment.Text.Trim());
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[6] = true;
                                    //MessageBox.Show("今日數據已更新");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                                else
                                {
                                    string updateSql = "UPDATE pwh SET comment = @comment WHERE c_date=@currentTime";
                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(updateSql, conn))
                                    {
                                        updateCommand.Parameters.AddWithValue("@comment", textBoxcomment.Text.Trim());
                                        updateCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                        updateCommand.ExecuteNonQuery();
                                    }
                                    judge[6] = false;
                                    //MessageBox.Show("數據提交成功");
                                    DataGridViewProduction();
                                    conn.Close();
                                }
                            }
                        }

                        else
                        {
                            string insertSql = "INSERT INTO pwh (c_date, comment) VALUES (@currentTime, @comment)";
                            using (NpgsqlCommand insertCommand = new NpgsqlCommand(insertSql, conn))
                            {
                                insertCommand.Parameters.AddWithValue("@currentTime", currentTime);
                                insertCommand.Parameters.AddWithValue("@comment", textBoxcomment.Text.Trim());
                                insertCommand.ExecuteNonQuery();
                            }
                            judge[6] = false;
                            //MessageBox.Show("數據提交成功");
                            DataGridViewProduction();
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }

        }
        private void showdata()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    DateTime currentTime = DateTime.Now.Date;
                    string sqlpro = "select c_date,inp,out,totalout,inventory,fullorder,fetched,comment from pwh WHERE c_date=@currentTime";
                    using NpgsqlCommand cmd = new NpgsqlCommand(sqlpro, conn);
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
                                textBoxinp.Text = row["inp"].ToString();
                                textBoxout.Text = row["out"].ToString();
                                textBoxtotalout.Text = row["totalout"].ToString();
                                textBoxinventory.Text = row["inventory"].ToString();
                                textBoxfullorder.Text = row["fullorder"].ToString();
                                textBoxfetched.Text = row["fetched"].ToString();
                                textBoxcomment.Text = row["comment"].ToString();
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("數據庫連接失敗: " + ex.Message);
            }
        }

        private void buttonsubmit_Click(object sender, EventArgs e)
        {
            inpDB();
            outDB();
            totaloutDB();
            inventoryDB();
            fullorderDB();
            fetchedDB();
            commentDB();
            bool judgement = false;
            foreach (bool jud in judge)
            {
                judgement = jud || judgement;
                if (judgement)
                {
                    MessageBox.Show("今日數據已更新");
                    break;
                }
            }
            if (judgement == false)
                MessageBox.Show("數據提交成功");
            DataGridViewProduction();
            textBoxinp.Focus();
        }

        private void buttonclear_Click(object sender, EventArgs e)
        {
            textBoxinp.Text = "";
            textBoxout.Text = "";
            textBoxtotalout.Text = "";
            textBoxinventory.Text = "";
            textBoxfullorder.Text = "";
            textBoxfetched.Text = "";
            textBoxcomment.Text = "";
            textBoxinp.Focus();
        }

        private void Formproduction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
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
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                DateTime currentTime = DateTime.Now.Date;
                conn.Open();
                string assemblemessage = "select inp,out,totalout,inventory,fullorder,fetched,comment from pwh where c_date=@currentTime";
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
                                message1 = "成品倉今日無補充說明";
                            message = "大家好！" + DateTime.Now.ToString("yyyy-MM-dd") + "成型課入成品倉產量明細:" + "\n" + "合計:   " + row["inp"] + "雙" + "\n" + "出貨:" + "\n" + DateTime.Now.ToString("yyyy-MM-dd") + "出貨:   " + row["out"] + "雙" + "\n" + DateTime.Now.ToString("MM") + "月累計出貨:     " + row["totalout"] + "雙" + "\n" + "成品倉庫存:       " + row["inventory"] + "雙" + "\n" + "其中已滿單:       " + row["fullorder"] + "雙" + "\n" + "已拿:            " + row["fetched"] + "雙" + "\n" + message1;
                            go(message);
                        }
                    }
                }
            }
        }

        private void buttonmessage_Click(object sender, EventArgs e)
        {
            gogo();
            MessageBox.Show("成品倉今日資料發送成功！");
        }
    }
}
