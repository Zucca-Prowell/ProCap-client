namespace PROCAP_CLIENT
{
    partial class Formmain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonadd = new Button();
            buttonsearch = new Button();
            buttonmodify = new Button();
            comboBoxdp = new ComboBox();
            textBoxsearch = new TextBox();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // buttonadd
            // 
            buttonadd.Location = new Point(677, 140);
            buttonadd.Name = "buttonadd";
            buttonadd.Size = new Size(75, 23);
            buttonadd.TabIndex = 0;
            buttonadd.Text = "添加";
            buttonadd.UseVisualStyleBackColor = true;
            buttonadd.Click += buttonadd_Click;
            // 
            // buttonsearch
            // 
            buttonsearch.Location = new Point(677, 169);
            buttonsearch.Name = "buttonsearch";
            buttonsearch.Size = new Size(75, 23);
            buttonsearch.TabIndex = 1;
            buttonsearch.Text = "搜索";
            buttonsearch.UseVisualStyleBackColor = true;
            buttonsearch.Click += buttonsearch_Click;
            // 
            // buttonmodify
            // 
            buttonmodify.Location = new Point(677, 198);
            buttonmodify.Name = "buttonmodify";
            buttonmodify.Size = new Size(75, 23);
            buttonmodify.TabIndex = 2;
            buttonmodify.Text = "修改";
            buttonmodify.UseVisualStyleBackColor = true;
            buttonmodify.Click += buttonmodify_Click;
            // 
            // comboBoxdp
            // 
            comboBoxdp.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxdp.FormattingEnabled = true;
            comboBoxdp.Items.AddRange(new object[] { "裁加", "針車", "組底", "成型", "Lean線" });
            comboBoxdp.Location = new Point(550, 141);
            comboBoxdp.Name = "comboBoxdp";
            comboBoxdp.Size = new Size(121, 23);
            comboBoxdp.TabIndex = 3;
            // 
            // textBoxsearch
            // 
            textBoxsearch.Location = new Point(550, 170);
            textBoxsearch.Name = "textBoxsearch";
            textBoxsearch.Size = new Size(121, 23);
            textBoxsearch.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(511, 405);
            dataGridView1.TabIndex = 5;
            // 
            // Formmain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(textBoxsearch);
            Controls.Add(comboBoxdp);
            Controls.Add(buttonmodify);
            Controls.Add(buttonsearch);
            Controls.Add(buttonadd);
            Name = "Formmain";
            Text = "產量登記";
            Load += Formmain_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonadd;
        private Button buttonsearch;
        private Button buttonmodify;
        private TextBox textBoxsearch;
        private DataGridView dataGridView1;
        protected internal ComboBox comboBoxdp;
    }
}