namespace PROCAP_CLIENT
{
    partial class Formchat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dataGridView1 = new DataGridView();
            buttonsubmit = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            buttonmessage = new Button();
            textBoxcomment = new TextBox();
            label2 = new Label();
            buttoncomment = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Left;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 30;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(519, 461);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // buttonsubmit
            // 
            buttonsubmit.Location = new Point(524, 151);
            buttonsubmit.Name = "buttonsubmit";
            buttonsubmit.Size = new Size(75, 25);
            buttonsubmit.TabIndex = 2;
            buttonsubmit.Text = "提交";
            buttonsubmit.UseVisualStyleBackColor = true;
            buttonsubmit.Click += buttonsubmit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonFace;
            label1.Location = new Point(549, 104);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 3;
            label1.Text = "--------";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(538, 122);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(123, 23);
            textBox1.TabIndex = 1;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // buttonmessage
            // 
            buttonmessage.Location = new Point(602, 151);
            buttonmessage.Name = "buttonmessage";
            buttonmessage.Size = new Size(75, 25);
            buttonmessage.TabIndex = 4;
            buttonmessage.Text = "發送";
            buttonmessage.UseVisualStyleBackColor = true;
            buttonmessage.Click += buttonmessage_Click;
            // 
            // textBoxcomment
            // 
            textBoxcomment.Location = new Point(524, 233);
            textBoxcomment.Multiline = true;
            textBoxcomment.Name = "textBoxcomment";
            textBoxcomment.Size = new Size(153, 147);
            textBoxcomment.TabIndex = 5;
            textBoxcomment.KeyDown += textBoxcomment_KeyDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(571, 215);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 6;
            label2.Text = "補充說明:";
            // 
            // buttoncomment
            // 
            buttoncomment.Location = new Point(565, 386);
            buttoncomment.Name = "buttoncomment";
            buttoncomment.Size = new Size(75, 23);
            buttoncomment.TabIndex = 7;
            buttoncomment.Text = "提交";
            buttoncomment.UseVisualStyleBackColor = true;
            buttoncomment.Click += buttoncomment_Click;
            // 
            // Formchat
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(buttoncomment);
            Controls.Add(label2);
            Controls.Add(textBoxcomment);
            Controls.Add(buttonmessage);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(buttonsubmit);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            Name = "Formchat";
            Text = "裁加產量";
            Load += Formchat_Load;
            KeyDown += Formchat_KeyDown;
            Resize += Formchat_Resize;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button buttonsubmit;
        private Label label1;
        protected internal TextBox textBox1;
        protected internal System.Windows.Forms.Timer timer1;
        private Button buttonmessage;
        private TextBox textBoxcomment;
        private Label label2;
        private Button buttoncomment;
    }
}