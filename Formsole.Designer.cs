namespace PROCAP_CLIENT
{
    partial class Formsole
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
            textBox1 = new TextBox();
            comboBox1 = new ComboBox();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // buttonsubmit
            // 
            buttonsubmit.Location = new Point(292, 297);
            buttonsubmit.Name = "buttonsubmit";
            buttonsubmit.Size = new Size(75, 23);
            buttonsubmit.TabIndex = 0;
            buttonsubmit.Text = "提交";
            buttonsubmit.UseVisualStyleBackColor = true;
            buttonsubmit.Click += buttonsubmit_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(145, 297);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(121, 23);
            textBox1.TabIndex = 1;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "組底1組", "組底2組", "組底3組", "組底4組", "組底5組", "組底6組" });
            comboBox1.Location = new Point(145, 267);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 2;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(85, 96);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(240, 150);
            dataGridView1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(48, 300);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 4;
            label1.Text = "------";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // Formsole
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(456, 450);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(comboBox1);
            Controls.Add(textBox1);
            Controls.Add(buttonsubmit);
            Name = "Formsole";
            Text = "組底產量";
            Load += Formsole_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonsubmit;
        private ComboBox comboBox1;
        private DataGridView dataGridView1;
        private Label label1;
        private System.Windows.Forms.Timer timer1;
        protected internal TextBox textBox1;
    }
}