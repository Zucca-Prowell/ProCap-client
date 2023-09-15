using System.Drawing.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection.Metadata;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PROCAP_CLIENT
{
    public partial class Formlean : Form
    {
        TextBox[] textcheck = new TextBox[6];
        bool[] boolarray = new bool[9];
        bool judgement = false;
        int k = 0;
        bool flag = false; 
        bool result = false;
        int counted;
        public Formlean()
        {
            InitializeComponent();
        }
        private void Formlean_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void initializedata()
        {
            textcheck[0] = textBoxchat;
            textcheck[1] = textBoxstitch1;
            textcheck[2] = textBoxstitch2;
            textcheck[3] = textBoxstitch3;
            textcheck[4] = textBoxstitch4;
            textcheck[5] = textBoxassemble;
            boolarray[0] = radioButtonlean1.Checked;
            boolarray[1] = radioButtonlean2.Checked;
            boolarray[2] = radioButtonlean3.Checked;
            boolarray[3] = string.IsNullOrEmpty(textcheck[0].Text.Trim());
            boolarray[4] = string.IsNullOrEmpty(textcheck[1].Text.Trim());
            boolarray[5] = string.IsNullOrEmpty(textcheck[2].Text.Trim());
            boolarray[6] = string.IsNullOrEmpty(textcheck[3].Text.Trim());
            boolarray[7] = string.IsNullOrEmpty(textcheck[4].Text.Trim());
            boolarray[8] = string.IsNullOrEmpty(textcheck[5].Text.Trim());
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
        private void buttonsubmit_Click(object sender, EventArgs e)
        {
            initializedata();
            judgenull();
            if (judgement)
            {
                //上傳數據到DATABASE
                MessageBox.Show("提交成功");
                judgement = false;
                flag = false;
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

        private void textBoxchat_KeyDown(object sender, KeyEventArgs e)
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
                            if((string.IsNullOrEmpty(textBoxchat.Text)))
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
                    } break;
                case Keys.Up:radioButtonlean3.Focus();break;
                case Keys.Down:textBoxstitch1.Focus();break;               
            }
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
                case Keys.Up: textBoxchat.Focus(); break;
                case Keys.Down: textBoxstitch2.Focus(); break;
                case Keys.Back: { if (string.IsNullOrEmpty(textBoxstitch1.Text)) { textBoxchat.Focus(); } } break;
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
                case Keys.Escape:Close(); break;
                case Keys.Enter: { if (counted == boolarray.Length) { buttonsubmit_Click(sender, e);counted = 3; } } break;
            }
        }

        private void buttonclear_Click(object sender, EventArgs e)
        {
            initializedata();
            foreach (TextBox a in textcheck)
            {
                a.Text = "";
            }
        }
    }
}
