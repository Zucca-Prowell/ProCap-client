namespace PROCAP_CLIENT
{
    partial class Formassemble
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
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            label1 = new Label();
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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Left;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(550, 461);
            dataGridView1.TabIndex = 0;
            // 
            // buttonsubmit
            // 
            buttonsubmit.Location = new Point(556, 164);
            buttonsubmit.Name = "buttonsubmit";
            buttonsubmit.Size = new Size(75, 23);
            buttonsubmit.TabIndex = 1;
            buttonsubmit.Text = "提交";
            buttonsubmit.UseVisualStyleBackColor = true;
            buttonsubmit.Click += buttonsubmit_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "成1組", "成2組", "成3組", "成4組", "成7組" });
            comboBox1.Location = new Point(570, 106);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(570, 135);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(121, 23);
            textBox1.TabIndex = 3;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(580, 86);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 4;
            label1.Text = "-------";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // buttonmessage
            // 
            buttonmessage.Location = new Point(637, 164);
            buttonmessage.Name = "buttonmessage";
            buttonmessage.Size = new Size(75, 23);
            buttonmessage.TabIndex = 5;
            buttonmessage.Text = "發送";
            buttonmessage.UseVisualStyleBackColor = true;
            buttonmessage.Click += buttonmessage_Click;
            // 
            // textBoxcomment
            // 
            textBoxcomment.Location = new Point(556, 234);
            textBoxcomment.Multiline = true;
            textBoxcomment.Name = "textBoxcomment";
            textBoxcomment.Size = new Size(156, 148);
            textBoxcomment.TabIndex = 6;
            textBoxcomment.KeyDown += textBoxcomment_KeyDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(603, 216);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 7;
            label2.Text = "補充說明:";
            // 
            // buttoncomment
            // 
            buttoncomment.Location = new Point(600, 390);
            buttoncomment.Name = "buttoncomment";
            buttoncomment.Size = new Size(75, 23);
            buttoncomment.TabIndex = 8;
            buttoncomment.Text = "提交";
            buttoncomment.UseVisualStyleBackColor = true;
            buttoncomment.Click += buttoncomment_Click;
            // 
            // Formassemble
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(720, 461);
            Controls.Add(buttoncomment);
            Controls.Add(label2);
            Controls.Add(textBoxcomment);
            Controls.Add(buttonmessage);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(comboBox1);
            Controls.Add(buttonsubmit);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            Name = "Formassemble";
            Text = "成型產量";
            Load += Formassemble_Load;
            KeyDown += Formassemble_KeyDown;
            Resize += Formassemble_Resize;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button buttonsubmit;
        private ComboBox comboBox1;
        private Label label1;
        private System.Windows.Forms.Timer timer1;
        protected internal TextBox textBox1;
        private Button buttonmessage;
        private TextBox textBoxcomment;
        private Label label2;
        private Button buttoncomment;
    }
}