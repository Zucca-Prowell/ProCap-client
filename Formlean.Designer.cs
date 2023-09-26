namespace PROCAP_CLIENT
{
    partial class Formlean
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
            radioButtonlean1 = new RadioButton();
            radioButtonlean2 = new RadioButton();
            radioButtonlean3 = new RadioButton();
            timer1 = new System.Windows.Forms.Timer(components);
            label1 = new Label();
            textBoxstitch1 = new TextBox();
            textBoxassemble = new TextBox();
            label3 = new Label();
            label4 = new Label();
            textBoxstitch2 = new TextBox();
            textBoxstitch3 = new TextBox();
            textBoxstitch4 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            buttonclear = new Button();
            buttonmessage = new Button();
            textBoxcomment = new TextBox();
            buttoncomment = new Button();
            label8 = new Label();
            SuspendLayout();
            // 
            // buttonsubmit
            // 
            buttonsubmit.Location = new Point(123, 299);
            buttonsubmit.Name = "buttonsubmit";
            buttonsubmit.Size = new Size(75, 23);
            buttonsubmit.TabIndex = 9;
            buttonsubmit.Text = "提交";
            buttonsubmit.UseVisualStyleBackColor = true;
            buttonsubmit.Click += buttonsubmit_Click;
            // 
            // radioButtonlean1
            // 
            radioButtonlean1.AutoSize = true;
            radioButtonlean1.Location = new Point(105, 47);
            radioButtonlean1.Name = "radioButtonlean1";
            radioButtonlean1.Size = new Size(69, 19);
            radioButtonlean1.TabIndex = 0;
            radioButtonlean1.Text = "Lean1線";
            radioButtonlean1.UseVisualStyleBackColor = true;
            // 
            // radioButtonlean2
            // 
            radioButtonlean2.AutoSize = true;
            radioButtonlean2.Location = new Point(105, 72);
            radioButtonlean2.Name = "radioButtonlean2";
            radioButtonlean2.Size = new Size(69, 19);
            radioButtonlean2.TabIndex = 1;
            radioButtonlean2.Text = "Lean2線";
            radioButtonlean2.UseVisualStyleBackColor = true;
            // 
            // radioButtonlean3
            // 
            radioButtonlean3.AutoSize = true;
            radioButtonlean3.Location = new Point(105, 97);
            radioButtonlean3.Name = "radioButtonlean3";
            radioButtonlean3.Size = new Size(69, 19);
            radioButtonlean3.TabIndex = 2;
            radioButtonlean3.Text = "Lean3線";
            radioButtonlean3.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(87, 19);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 18;
            label1.Text = "-------";
            // 
            // textBoxstitch1
            // 
            textBoxstitch1.Location = new Point(87, 122);
            textBoxstitch1.Name = "textBoxstitch1";
            textBoxstitch1.Size = new Size(100, 23);
            textBoxstitch1.TabIndex = 4;
            textBoxstitch1.KeyDown += textBoxstitch1_KeyDown;
            // 
            // textBoxassemble
            // 
            textBoxassemble.Location = new Point(87, 241);
            textBoxassemble.Name = "textBoxassemble";
            textBoxassemble.Size = new Size(100, 23);
            textBoxassemble.TabIndex = 8;
            textBoxassemble.KeyDown += textBoxassemble_KeyDown;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(42, 125);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 15;
            label3.Text = "針1組";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(48, 244);
            label4.Name = "label4";
            label4.Size = new Size(33, 15);
            label4.TabIndex = 11;
            label4.Text = "成型";
            // 
            // textBoxstitch2
            // 
            textBoxstitch2.Location = new Point(87, 151);
            textBoxstitch2.Name = "textBoxstitch2";
            textBoxstitch2.Size = new Size(100, 23);
            textBoxstitch2.TabIndex = 5;
            textBoxstitch2.KeyDown += textBoxstitch2_KeyDown;
            // 
            // textBoxstitch3
            // 
            textBoxstitch3.Location = new Point(87, 180);
            textBoxstitch3.Name = "textBoxstitch3";
            textBoxstitch3.Size = new Size(100, 23);
            textBoxstitch3.TabIndex = 6;
            textBoxstitch3.KeyDown += textBoxstitch3_KeyDown;
            // 
            // textBoxstitch4
            // 
            textBoxstitch4.Location = new Point(87, 209);
            textBoxstitch4.Name = "textBoxstitch4";
            textBoxstitch4.Size = new Size(100, 23);
            textBoxstitch4.TabIndex = 7;
            textBoxstitch4.KeyDown += textBoxstitch4_KeyDown;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(42, 154);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 14;
            label5.Text = "針2組";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(42, 183);
            label6.Name = "label6";
            label6.Size = new Size(39, 15);
            label6.TabIndex = 13;
            label6.Text = "針3組";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(42, 212);
            label7.Name = "label7";
            label7.Size = new Size(39, 15);
            label7.TabIndex = 12;
            label7.Text = "針4組";
            // 
            // buttonclear
            // 
            buttonclear.Location = new Point(42, 299);
            buttonclear.Name = "buttonclear";
            buttonclear.Size = new Size(75, 23);
            buttonclear.TabIndex = 10;
            buttonclear.Text = "清除";
            buttonclear.UseVisualStyleBackColor = true;
            buttonclear.Click += buttonclear_Click;
            // 
            // buttonmessage
            // 
            buttonmessage.Location = new Point(77, 328);
            buttonmessage.Name = "buttonmessage";
            buttonmessage.Size = new Size(75, 23);
            buttonmessage.TabIndex = 19;
            buttonmessage.Text = "發送";
            buttonmessage.UseVisualStyleBackColor = true;
            buttonmessage.Click += buttonmessage_Click;
            // 
            // textBoxcomment
            // 
            textBoxcomment.Location = new Point(213, 122);
            textBoxcomment.Multiline = true;
            textBoxcomment.Name = "textBoxcomment";
            textBoxcomment.Size = new Size(100, 163);
            textBoxcomment.TabIndex = 20;
            textBoxcomment.KeyDown += textBoxcomment_KeyDown;
            // 
            // buttoncomment
            // 
            buttoncomment.Location = new Point(228, 299);
            buttoncomment.Name = "buttoncomment";
            buttoncomment.Size = new Size(75, 23);
            buttoncomment.TabIndex = 21;
            buttoncomment.Text = "提交";
            buttoncomment.UseVisualStyleBackColor = true;
            buttoncomment.Click += buttoncomment_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(228, 101);
            label8.Name = "label8";
            label8.Size = new Size(62, 15);
            label8.TabIndex = 22;
            label8.Text = "補充說明:";
            // 
            // Formlean
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 361);
            Controls.Add(label8);
            Controls.Add(buttoncomment);
            Controls.Add(textBoxcomment);
            Controls.Add(buttonmessage);
            Controls.Add(buttonclear);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(textBoxstitch4);
            Controls.Add(textBoxstitch3);
            Controls.Add(textBoxstitch2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBoxassemble);
            Controls.Add(textBoxstitch1);
            Controls.Add(label1);
            Controls.Add(radioButtonlean3);
            Controls.Add(radioButtonlean2);
            Controls.Add(radioButtonlean1);
            Controls.Add(buttonsubmit);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "Formlean";
            Text = "Lean線產量";
            Load += Formlean_Load;
            KeyDown += Formlean_KeyDown;
            Resize += Formlean_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonsubmit;
        private RadioButton radioButtonlean1;
        private RadioButton radioButtonlean2;
        private RadioButton radioButtonlean3;
        private System.Windows.Forms.Timer timer1;
        private Label label1;
        private TextBox textBoxassemble;
        private Label label3;
        private Label label4;
        private TextBox textBoxstitch2;
        private TextBox textBoxstitch3;
        private TextBox textBoxstitch4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button buttonclear;
        private Button buttonmessage;
        private TextBox textBoxcomment;
        private Button buttoncomment;
        private Label label8;
        protected internal TextBox textBoxstitch1;
    }
}