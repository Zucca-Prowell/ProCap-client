using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private void buttonsubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                MessageBox.Show("產量不能為空");
            }
            else
            {
                MessageBox.Show("提交成功");
                textBox1.Text = "";
                //將當天的產量錄入Database
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
    }
}
