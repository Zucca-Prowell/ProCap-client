using Npgsql;
using System.Drawing;
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

        private void buttonmodify_Click(object sender, EventArgs e)
        {

        }

        private void buttonadd_Click(object sender, EventArgs e)
        {
            //if()
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
                //case 4待添加
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

        private void Formmain_Load(object sender, EventArgs e)
        {
            comboBoxdp.SelectedIndex = 0;
            //string connString = "Server=192.168.7.198;Port=5432;Database=postgres;Username=joe;Password=Joe@6666";
            //try
            //{
            //    using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            //    {
            //        conn.Open();
            //        //System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            //        //DateTime currentTime = DateTime.Now;     
            //        //string insertSql = "INSERT INTO cut(capacity) VALUES (1980)";
            //        //using (NpgsqlCommand cmd = new NpgsqlCommand(insertSql, conn))
            //        //{
            //        //    cmd.Parameters.AddWithValue("capacity", "1980");
            //        //    int rowsAffected = cmd.ExecuteNonQuery();
            //        //    MessageBox.Show($"插入了 {rowsAffected} 行数据.");
            //        //    if (rowsAffected > 0)
            //        //    {
            //        //        MessageBox.Show($"成功插入 {rowsAffected} 行数据。");
            //        //    }
            //        //    else
            //        //    {
            //        //        MessageBox.Show("未插入任何数据。");
            //        //    }
            //        //}

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("數據庫連接失敗: " + ex.Message);
            //}
        }

        private void buttonsearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxsearch.Text.Trim()))
            {
                MessageBox.Show("搜索不能为空");
            }
            else
            {
                //正则表达式+显示DB相应数据
            }
        }
    }
}