using System.Drawing.Text;
using System.Drawing;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROCAP_CLIENT
{
    public partial class Formlean : Form
    {
        
        private bool hasfocus = false;
        public Formlean()
        {
            InitializeComponent();
            
            this.KeyDown += Formlean_KeyDown;
            this.Paint += Formlean_Paint;
            this.LostFocus += Formlean_LostFocus;
            this.GotFocus += Formlean_GotFocus;
            this.MouseDown += Formlean_MouseDown;
        }
        private void Formlean_Paint(object sender, PaintEventArgs e)
        {
            if (hasfocus)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);
            }
        }

        private void Formlean_GotFocus(object sender, EventArgs e)
        {
            hasfocus = true;
            this.Invalidate(); // 重新绘制窗体以显示焦点框
        }

        private void Formlean_LostFocus(object sender, EventArgs e)
        {
            hasfocus = false;
            this.Invalidate(); // 重新绘制窗体以隐藏焦点框
        }

        private void Formlean_Load(object sender, EventArgs e)
        {
            timer1.Start();
           
        }

        private void radioButtonlean1_CheckedChanged(object sender, EventArgs e)
        {

        }
       
        private void buttonsubmit_Click(object sender, EventArgs e)
        {
            
            int k = 0;
            TextBox[] textcheck = new TextBox[6];
            textcheck[0] = textBoxchat;
            textcheck[1] = textBoxstitch1;
            textcheck[2] = textBoxstitch2;
            textcheck[3] = textBoxstitch3;
            textcheck[4] = textBoxstitch4;
            textcheck[5] = textBoxassemble;
            bool[] boolarray = new bool[9];
            boolarray[0] = radioButtonlean1.Checked;
            boolarray[1] = radioButtonlean2.Checked;
            boolarray[2] = radioButtonlean3.Checked;
            boolarray[3] = string.IsNullOrEmpty(textcheck[0].Text.Trim());
            boolarray[4] = string.IsNullOrEmpty(textcheck[1].Text.Trim());
            boolarray[5] = string.IsNullOrEmpty(textcheck[2].Text.Trim());
            boolarray[6] = string.IsNullOrEmpty(textcheck[3].Text.Trim());
            boolarray[7] = string.IsNullOrEmpty(textcheck[4].Text.Trim());
            boolarray[8] = string.IsNullOrEmpty(textcheck[5].Text.Trim());
            bool result = false;
            for (int i = 0; i < 3; i++)
            {
                result = (result || boolarray[i]);
            }
            if (!result)
            { MessageBox.Show("請選擇lean線線別"); }
            else
            {
                for (int j = 3; j < boolarray.Length; j++)
                {

                    if (boolarray[j])
                    {
                        MessageBox.Show("有欄為空");
                        textcheck[j - 3].Focus();
                        break;
                    }
                    else
                        k++;
                }
            }
            if (result == true && k == textcheck.Length)
            {
                //上傳數據到DATABASE
                MessageBox.Show("提交成功");
                foreach (TextBox a in textcheck)
                {
                    a.Text = "";
                }
                textBoxchat.Focus();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("yyyy-MM-dd" + "產量");
        }

        private void textBoxchat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                SendKeys.Send("{tab}");
        }

        private void textBoxstitch1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                SendKeys.Send("{tab}");
        }

        private void textBoxstitch2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                SendKeys.Send("{tab}");
        }

        private void textBoxstitch3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                SendKeys.Send("{tab}");
        }

        private void textBoxstitch4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                SendKeys.Send("{tab}");
        }

        private void textBoxassemble_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                buttonsubmit_Click(sender, e);
        }

        private void radioButtonlean1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Formlean_MouseDown(object sender, MouseEventArgs e)
        {
            Focus();
        }

        private void Formlean_KeyDown(object sender, KeyEventArgs e)
        {
          
            if (e.KeyCode== Keys.Escape)
            {
                Close();
            }
        }
    }
}
