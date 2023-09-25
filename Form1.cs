using Microsoft.VisualBasic;
using Npgsql;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Windows.Forms.DataFormats;


namespace PROCAP_CLIENT
{


    public partial class Formmain : Form
    {

        private bool isFormOpen = false;
        public Formmain()
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


        private void buttonadd_Click(object sender, EventArgs e)
        {

            switch (comboBoxdp.SelectedIndex)
            {
                case 0:
                    {
                        if (!isFormOpen)
                        {
                            isFormOpen = true;
                            var formchat = new Formchat();
                            formchat.FormClosed += (s, args) => isFormOpen = false;
                            formchat.Show();
                            formchat.textBox1.Focus();
                        }
                    }
                    break;
                case 1:
                    {
                        if (!isFormOpen)
                        {
                            isFormOpen = true;
                            var formstitch = new Formstitch();
                            formstitch.FormClosed += (s, args) => isFormOpen = false;
                            formstitch.Show();
                            formstitch.textBox1.Focus();
                        }
                    }
                    break;
                case 2:
                    {
                        if (!isFormOpen)
                        {
                            isFormOpen = true;
                            var formsole = new Formsole();
                            formsole.FormClosed += (s, args) => isFormOpen = false;
                            formsole.Show();
                            formsole.textBox1.Focus();
                        }
                    }
                    break;
                case 3:
                    {
                        if (!isFormOpen)
                        {
                            isFormOpen = true;
                            var formassemble = new Formassemble();
                            formassemble.FormClosed += (s, args) => isFormOpen = false;
                            formassemble.Show();
                            formassemble.textBox1.Focus();
                        }
                    }
                    break;

                case 4:
                    {
                        if (!isFormOpen)
                        {
                            isFormOpen = true;
                            var formlean = new Formlean();
                            formlean.FormClosed += (s, args) => isFormOpen = false;
                            formlean.Show();
                            formlean.textBoxchat.Focus();
                        }
                    }
                    break;
                case 5:
                    {
                        if (!isFormOpen)
                        {
                            isFormOpen = true;
                            var formproduction = new Formproduction();
                            formproduction.FormClosed += (s, args) => isFormOpen = false;
                            formproduction.Show();
                            formproduction.textBoxinp.Focus();
                        }
                    }
                    break;
            }
        }


        private void Formmain_Load(object sender, EventArgs e)
        {

            comboBoxdp.SelectedIndex = 0;
            this.Resize += new EventHandler(Formmain_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
        }
        private void Formmain_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
        }
        private void searchchatdate()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                DateTime date = DateTime.Parse(textBoxsearch.Text.Trim());
                DateTime endtime = date;
                endtime = endtime.AddDays(-5);
                string sql_c = "Select c_date,cutsum,capacity,lean01,lean02,lean03,comment,leancomment from cut where c_date BETWEEN @endtime AND @date";
                using (NpgsqlCommand command = new NpgsqlCommand(sql_c, conn))
                {
                    command.Parameters.AddWithValue("@endtime", endtime);
                    command.Parameters.AddWithValue("@date", date);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                        dataGridView1.Sort(dataGridView1.Columns["c_date"], ListSortDirection.Descending);
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.Columns["comment"].DisplayIndex = 6;
                        dataGridView1.Columns["c_date"].HeaderText = "日期";
                        dataGridView1.Columns["capacity"].HeaderText = "裁加(大線)";
                        dataGridView1.Columns["leancomment"].HeaderText = "lean線補充說明";
                        dataGridView1.Columns["comment"].HeaderText = "補充說明";
                        dataGridView1.Columns["lean01"].HeaderText = "lean1線";
                        dataGridView1.Columns["lean02"].HeaderText = "lean2線";
                        dataGridView1.Columns["lean03"].HeaderText = "lean3線";
                        dataGridView1.Columns["cutsum"].HeaderText = "裁加(總和)";
                    }
                }
            }
        }


        private void searchstitchdate()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                DateTime date = DateTime.Parse(textBoxsearch.Text.Trim());
                DateTime endtime = date;
                endtime = endtime.AddDays(-5);
                string sql_st = "Select c_date,stitchsum,stitch1,stitch2,stitch3,stitch4,stitch5,lean1線,lean2線,lean3線,comment from stitch where c_date BETWEEN @endtime AND @date";
                using (NpgsqlCommand command = new NpgsqlCommand(sql_st, conn))
                {
                    command.Parameters.AddWithValue("@endtime", endtime);
                    command.Parameters.AddWithValue("@date", date);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                        dataGridView1.Sort(dataGridView1.Columns["c_date"], ListSortDirection.Descending);
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        dataGridView1.Columns["comment"].DisplayIndex = 10;
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
        }
        private void searchsoledate()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                DateTime date = DateTime.Parse(textBoxsearch.Text.Trim());
                DateTime endtime = date;
                endtime = endtime.AddDays(-5);
                string sql_so = "Select c_date,solesum,sole1,sole2,sole3,sole4,sole5,sole6,comment from sole where c_date BETWEEN @endtime AND @date";
                using (NpgsqlCommand command = new NpgsqlCommand(sql_so, conn))
                {
                    command.Parameters.AddWithValue("@endtime", endtime);
                    command.Parameters.AddWithValue("@date", date);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                        dataGridView1.Sort(dataGridView1.Columns["c_date"], ListSortDirection.Descending);
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.Columns["comment"].DisplayIndex = 8;
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
        }
        private void searchassembledate()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                DateTime date = DateTime.Parse(textBoxsearch.Text.Trim());
                DateTime endtime = date;
                endtime = endtime.AddDays(-5);
                string sql_a = "Select c_date,assemblesum,assemble01,assemble02,assemble03,assemble04,assemble07,lean01,lean02,lean03,comment from assemble where c_date BETWEEN @endtime AND @date";
                using (NpgsqlCommand command = new NpgsqlCommand(sql_a, conn))
                {
                    command.Parameters.AddWithValue("@endtime", endtime);
                    command.Parameters.AddWithValue("@date", date);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                        dataGridView1.Sort(dataGridView1.Columns["c_date"], ListSortDirection.Descending);
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        dataGridView1.Columns["comment"].DisplayIndex = 10;
                        dataGridView1.Columns["c_date"].HeaderText = "日期";
                        dataGridView1.Columns["comment"].HeaderText = "補充說明";
                        dataGridView1.Columns["assemble01"].HeaderText = "成1組";
                        dataGridView1.Columns["assemble02"].HeaderText = "成2組";
                        dataGridView1.Columns["assemble03"].HeaderText = "成3組";
                        dataGridView1.Columns["assemble04"].HeaderText = "成4組";
                        dataGridView1.Columns["assemble07"].HeaderText = "成7組";
                        dataGridView1.Columns["lean01"].HeaderText = "lean1線";
                        dataGridView1.Columns["lean02"].HeaderText = "lean2線";
                        dataGridView1.Columns["lean03"].HeaderText = "lean3線";
                        dataGridView1.Columns["assemblesum"].HeaderText = "成型合计";
                    }
                }

            }
        }
        private void searchproductiondate()
        {
            string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                DateTime date = DateTime.Parse(textBoxsearch.Text.Trim());
                DateTime endtime = date;
                endtime = endtime.AddDays(-5);
                string sql_p = "Select c_date,inp,out,totalout,inventory,fullorder,fetched,comment from pwh where c_date BETWEEN @endtime AND @date";
                using (NpgsqlCommand command = new NpgsqlCommand(sql_p, conn))
                {
                    command.Parameters.AddWithValue("@endtime", endtime);
                    command.Parameters.AddWithValue("@date", date);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                        dataGridView1.Sort(dataGridView1.Columns["c_date"], ListSortDirection.Descending);
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            }
        }

        private void buttonsearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxsearch.Text.Trim()))
            {
                MessageBox.Show("搜索不能为空");
            }
            else
            {
                switch (comboBoxdp.SelectedIndex)
                {
                    case 0: searchchatdate(); break;
                    case 1: searchstitchdate(); break;
                    case 2: searchsoledate(); break;
                    case 3: searchassembledate(); break;
                    case 4: MessageBox.Show("lean線數據已歸類至各個部門，請查閱相關部門"); break;
                    case 5: searchproductiondate(); break;
                }

            }
        }

        private void textBoxsearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonsearch_Click(sender, e);
        }


    }
}