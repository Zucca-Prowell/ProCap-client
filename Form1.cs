using static System.Windows.Forms.DataFormats;

namespace PROCAP_CLIENT
{
    public partial class Formmain : Form
    {
        private bool isFormOpen=false;
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
                case 0: {
                        if (!isFormOpen)
                        {
                            isFormOpen = true;
                            var formchat = new Formchat();
                            formchat.FormClosed += (s, args) => isFormOpen = false;
                            formchat.Show();
                            formchat.textBox1.Focus();
                        }
                      } break;
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
                    } break;
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
                    } break;
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
                    } break;
                    //case 4«Ý²K¥[
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
                    } break;
            }
        }

        private void Formmain_Load(object sender, EventArgs e)
        {
            comboBoxdp.SelectedIndex = 0;
        }
    }
}