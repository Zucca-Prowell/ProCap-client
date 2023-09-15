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

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("yyyy-MM-dd" + "產量");
        }

        private void Formstitch_Load(object sender, EventArgs e)
        {
            timer1.Start();
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void buttonsubmit_Click(object sender, EventArgs e)
        {
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
                        {   //上傳數據
                            MessageBox.Show("提交成功");
                            textBox1.Text = "";
                            comboBox1.SelectedIndex++;
                            textBox1.Focus();
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
                        {   //上傳數據
                            MessageBox.Show("提交成功");
                            textBox1.Text = "";
                            comboBox1.SelectedIndex++;
                            textBox1.Focus();
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
                        {   //上傳數據
                            MessageBox.Show("提交成功");
                            textBox1.Text = "";
                            comboBox1.SelectedIndex++;
                            textBox1.Focus();
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
                        {   //上傳數據
                            MessageBox.Show("提交成功");
                            textBox1.Text = "";
                            comboBox1.SelectedIndex++;
                            textBox1.Focus();
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
                        {   //上傳數據
                            MessageBox.Show("提交成功");
                            textBox1.Text = "";
                            textBox1.Focus();
                        }
                    }
                    break;
            }
        }

        private void Formstitch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
                buttonsubmit_Click(sender, e);  
        }
    }
}
