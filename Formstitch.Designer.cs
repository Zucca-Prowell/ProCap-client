namespace PROCAP_CLIENT
{
    partial class Formstitch
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
            buttonsubmit = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            dataGridView1 = new DataGridView();
            comboBox1 = new ComboBox();
            timer1 = new System.Windows.Forms.Timer(components);
            buttonmessage = new Button();
            textBoxcomment = new TextBox();
            label2 = new Label();
            buttoncomment = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // buttonsubmit
            // 
            buttonsubmit.Location = new Point(556, 149);
            buttonsubmit.Name = "buttonsubmit";
            buttonsubmit.Size = new Size(75, 23);
            buttonsubmit.TabIndex = 0;
            buttonsubmit.Text = "提交";
            buttonsubmit.UseVisualStyleBackColor = true;
            buttonsubmit.Click += buttonsubmit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(584, 73);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 1;
            label1.Text = "------";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(575, 120);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(122, 23);
            textBox1.TabIndex = 2;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Left;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(550, 461);
            dataGridView1.TabIndex = 3;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "針一課", "針二課", "針三課", "針四課", "針五課" });
            comboBox1.Location = new Point(575, 91);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(122, 23);
            comboBox1.TabIndex = 4;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // buttonmessage
            // 
            buttonmessage.Location = new Point(643, 149);
            buttonmessage.Name = "buttonmessage";
            buttonmessage.Size = new Size(75, 23);
            buttonmessage.TabIndex = 5;
            buttonmessage.Text = "發送";
            buttonmessage.UseVisualStyleBackColor = true;
            buttonmessage.Click += buttonmessage_Click;
            // 
            // textBoxcomment
            // 
            textBoxcomment.Location = new Point(556, 230);
            textBoxcomment.Multiline = true;
            textBoxcomment.Name = "textBoxcomment";
            textBoxcomment.Size = new Size(162, 135);
            textBoxcomment.TabIndex = 6;
            textBoxcomment.KeyDown += textBoxcomment_KeyDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(605, 212);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 7;
            label2.Text = "補充說明:";
            // 
            // buttoncomment
            // 
            buttoncomment.Location = new Point(604, 371);
            buttoncomment.Name = "buttoncomment";
            buttoncomment.Size = new Size(75, 23);
            buttoncomment.TabIndex = 8;
            buttoncomment.Text = "提交";
            buttoncomment.UseVisualStyleBackColor = true;
            buttoncomment.Click += buttoncomment_Click;
            // 
            // Formstitch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(724, 461);
            Controls.Add(buttoncomment);
            Controls.Add(label2);
            Controls.Add(textBoxcomment);
            Controls.Add(buttonmessage);
            Controls.Add(comboBox1);
            Controls.Add(dataGridView1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(buttonsubmit);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            Name = "Formstitch";
            Text = "針車產量";
            Load += Formstitch_Load;
            KeyDown += Formstitch_KeyDown;
            Resize += Formstitch_Resize;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonsubmit;
        private Label label1;
        private DataGridView dataGridView1;
        private ComboBox comboBox1;
        private System.Windows.Forms.Timer timer1;
        protected internal TextBox textBox1;
        private Button buttonmessage;
        private TextBox textBoxcomment;
        private Label label2;
        private Button buttoncomment;
    }
}