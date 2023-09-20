using Microsoft.VisualBasic;
using Npgsql;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
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
            }
        }

        //private void DataGridViewStitch()
        //{
        //    string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
        //    try
        //    {
        //        using (NpgsqlConnection conn = new NpgsqlConnection(connString))
        //        {
        //            conn.Open();
        //            string sqlstitch = "select c_date,stitch1,stitch2,stitch3,stitch4,stitch5,lean1線,lean2線,lean3線 from stitch";
        //            NpgsqlCommand cmd = new NpgsqlCommand(sqlstitch, conn);
        //            NpgsqlDataAdapter adp = new NpgsqlDataAdapter(cmd);
        //            DataTable dataTable_S = new DataTable();
        //            adp.Fill(dataTable_S);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("數據庫連接失敗: " + ex.Message);

        //    }

        //}

        private void Formmain_Load(object sender, EventArgs e)
        {

            comboBoxdp.SelectedIndex = 0;

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
                string sql_c = "Select c_date,capacity,lean01,lean02,lean03 from cut where c_date BETWEEN @endtime AND @date";
                using (NpgsqlCommand command = new NpgsqlCommand(sql_c, conn))
                {
                    command.Parameters.AddWithValue("@endtime", endtime);
                    command.Parameters.AddWithValue("@date", date);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;

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
                string sql_st = "Select c_date,stitch1,stitch2,stitch3,stitch4,stitch5,lean1線,lean2線,lean3線 from stitch where c_date BETWEEN @endtime AND @date";
                using (NpgsqlCommand command = new NpgsqlCommand(sql_st, conn))
                {
                    command.Parameters.AddWithValue("@endtime", endtime);
                    command.Parameters.AddWithValue("@date", date);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;

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
                string sql_so = "Select c_date,sole1,sole2,sole3,sole4,sole5,sole6 from sole where c_date BETWEEN @endtime AND @date";
                using (NpgsqlCommand command = new NpgsqlCommand(sql_so, conn))
                {
                    command.Parameters.AddWithValue("@endtime", endtime);
                    command.Parameters.AddWithValue("@date", date);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;

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
                string sql_a = "Select c_date,assemble01,assemble02,assemble03,assemble04,assemble07,lean01,lean02,lean03 from assemble where c_date BETWEEN @endtime AND @date";
                using (NpgsqlCommand command = new NpgsqlCommand(sql_a, conn))
                {
                    command.Parameters.AddWithValue("@endtime", endtime);
                    command.Parameters.AddWithValue("@date", date);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;

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
                    case 4:MessageBox.Show("lean線數據已歸類至各個部門，請查閱相關部門");break;
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