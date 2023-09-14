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
    public partial class Formassemble : Form
    {
        public Formassemble()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("yyyy-MM-dd" + "產量");
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

        private void Formassemble_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            timer1.Start();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
