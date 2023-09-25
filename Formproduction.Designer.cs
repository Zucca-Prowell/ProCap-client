namespace PROCAP_CLIENT
{
    partial class Formproduction
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
            textBoxinp = new TextBox();
            buttonsubmit = new Button();
            textBoxout = new TextBox();
            textBoxtotalout = new TextBox();
            textBoxinventory = new TextBox();
            textBoxfullorder = new TextBox();
            textBoxfetched = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            buttonclear = new Button();
            buttonmessage = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            textBoxcomment = new TextBox();
            label8 = new Label();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // textBoxinp
            // 
            textBoxinp.Location = new Point(500, 70);
            textBoxinp.Name = "textBoxinp";
            textBoxinp.Size = new Size(93, 23);
            textBoxinp.TabIndex = 0;
            // 
            // buttonsubmit
            // 
            buttonsubmit.Location = new Point(581, 415);
            buttonsubmit.Name = "buttonsubmit";
            buttonsubmit.Size = new Size(75, 23);
            buttonsubmit.TabIndex = 7;
            buttonsubmit.Text = "提交";
            buttonsubmit.UseVisualStyleBackColor = true;
            buttonsubmit.Click += buttonsubmit_Click;
            // 
            // textBoxout
            // 
            textBoxout.Location = new Point(644, 70);
            textBoxout.Name = "textBoxout";
            textBoxout.Size = new Size(93, 23);
            textBoxout.TabIndex = 1;
            // 
            // textBoxtotalout
            // 
            textBoxtotalout.Location = new Point(500, 135);
            textBoxtotalout.Name = "textBoxtotalout";
            textBoxtotalout.Size = new Size(93, 23);
            textBoxtotalout.TabIndex = 2;
            // 
            // textBoxinventory
            // 
            textBoxinventory.Location = new Point(644, 135);
            textBoxinventory.Name = "textBoxinventory";
            textBoxinventory.Size = new Size(93, 23);
            textBoxinventory.TabIndex = 3;
            // 
            // textBoxfullorder
            // 
            textBoxfullorder.Location = new Point(500, 198);
            textBoxfullorder.Name = "textBoxfullorder";
            textBoxfullorder.Size = new Size(93, 23);
            textBoxfullorder.TabIndex = 4;
            // 
            // textBoxfetched
            // 
            textBoxfetched.Location = new Point(644, 198);
            textBoxfetched.Name = "textBoxfetched";
            textBoxfetched.Size = new Size(93, 23);
            textBoxfetched.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(522, 50);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 12;
            label1.Text = "入庫";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(671, 50);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 13;
            label2.Text = "出貨";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(522, 116);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 14;
            label3.Text = "累計出貨";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(587, 28);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 11;
            label4.Text = "label4";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(671, 116);
            label5.Name = "label5";
            label5.Size = new Size(33, 15);
            label5.TabIndex = 15;
            label5.Text = "庫存";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(522, 180);
            label6.Name = "label6";
            label6.Size = new Size(46, 15);
            label6.TabIndex = 16;
            label6.Text = "已滿單";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(671, 180);
            label7.Name = "label7";
            label7.Size = new Size(33, 15);
            label7.TabIndex = 17;
            label7.Text = "已拿";
            // 
            // buttonclear
            // 
            buttonclear.Location = new Point(500, 415);
            buttonclear.Name = "buttonclear";
            buttonclear.Size = new Size(75, 23);
            buttonclear.TabIndex = 9;
            buttonclear.Text = "清空";
            buttonclear.UseVisualStyleBackColor = true;
            buttonclear.Click += buttonclear_Click;
            // 
            // buttonmessage
            // 
            buttonmessage.Location = new Point(662, 415);
            buttonmessage.Name = "buttonmessage";
            buttonmessage.Size = new Size(75, 23);
            buttonmessage.TabIndex = 8;
            buttonmessage.Text = "發送";
            buttonmessage.UseVisualStyleBackColor = true;
            buttonmessage.Click += buttonmessage_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // textBoxcomment
            // 
            textBoxcomment.Location = new Point(500, 265);
            textBoxcomment.Multiline = true;
            textBoxcomment.Name = "textBoxcomment";
            textBoxcomment.Size = new Size(237, 137);
            textBoxcomment.TabIndex = 6;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(587, 245);
            label8.Name = "label8";
            label8.Size = new Size(59, 15);
            label8.TabIndex = 18;
            label8.Text = "補充說明";
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Left;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(470, 450);
            dataGridView1.TabIndex = 10;
            // 
            // Formproduction
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(749, 450);
            Controls.Add(dataGridView1);
            Controls.Add(label8);
            Controls.Add(textBoxcomment);
            Controls.Add(buttonmessage);
            Controls.Add(buttonclear);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxfetched);
            Controls.Add(textBoxfullorder);
            Controls.Add(textBoxinventory);
            Controls.Add(textBoxtotalout);
            Controls.Add(textBoxout);
            Controls.Add(buttonsubmit);
            Controls.Add(textBoxinp);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            Name = "Formproduction";
            Text = "成品倉";
            Load += Formproduction_Load;
            KeyDown += Formproduction_KeyDown;
            Resize += Formproduction_Resize;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonsubmit;
        private TextBox textBoxout;
        private TextBox textBoxtotalout;
        private TextBox textBoxinventory;
        private TextBox textBoxfullorder;
        private TextBox textBoxfetched;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button buttonclear;
        private Button buttonmessage;
        private System.Windows.Forms.Timer timer1;
        private TextBox textBoxcomment;
        private Label label8;
        protected internal TextBox textBoxinp;
        private DataGridView dataGridView1;
    }
}