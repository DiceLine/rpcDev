namespace WinFormsRPC
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dataGridView2 = new DataGridView();
            dataGridView1 = new DataGridView();
            comboBox1 = new ComboBox();
            checkBox1 = new CheckBox();
            checkBox3 = new CheckBox();
            button1 = new Button();
            checkBox2 = new CheckBox();
            groupBox1 = new GroupBox();
            button2 = new Button();
            groupBox2 = new GroupBox();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(9, 117);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(263, 495);
            dataGridView2.TabIndex = 7;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(6, 117);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(266, 495);
            dataGridView1.TabIndex = 8;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "weqwdWQD", "qwdwQWDQW", "QWDqwdwQ", "qwdQWDqwdQWD", "qwdQWDqw", "WQDqwdQWDqw", "1``2342443eqwdd3qe", "wqdqawd" });
            comboBox1.Location = new Point(442, 121);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(264, 23);
            comboBox1.TabIndex = 9;
            // 
            // checkBox1
            // 
            checkBox1.CheckAlign = ContentAlignment.BottomCenter;
            checkBox1.Image = Resource1.database_icon;
            checkBox1.Location = new Point(475, 12);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(79, 93);
            checkBox1.TabIndex = 10;
            checkBox1.TextAlign = ContentAlignment.MiddleCenter;
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.CheckAlign = ContentAlignment.MiddleRight;
            checkBox3.Location = new Point(442, 190);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(83, 19);
            checkBox3.TabIndex = 12;
            checkBox3.Text = "checkBox3";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.BackgroundImageLayout = ImageLayout.None;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Image = Resource1.sort_icon;
            button1.Location = new Point(990, 351);
            button1.Name = "button1";
            button1.Size = new Size(91, 83);
            button1.TabIndex = 13;
            button1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.Image = Resource1.random_generation;
            checkBox2.Location = new Point(160, 33);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(84, 81);
            checkBox2.TabIndex = 15;
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(dataGridView2);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.Location = new Point(712, 33);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(278, 618);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            groupBox1.Text = "Первичный массив";
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Image = Resource1.create_icon;
            button2.Location = new Point(52, 36);
            button2.Name = "button2";
            button2.Size = new Size(75, 72);
            button2.TabIndex = 18;
            button2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dataGridView1);
            groupBox2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox2.Location = new Point(1085, 33);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(278, 618);
            groupBox2.TabIndex = 17;
            groupBox2.TabStop = false;
            groupBox2.Text = "Отсортированный";
            // 
            // button3
            // 
            button3.BackgroundImageLayout = ImageLayout.None;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Image = Resource1.save_button;
            button3.Location = new Point(585, 12);
            button3.Name = "button3";
            button3.Size = new Size(83, 80);
            button3.TabIndex = 18;
            button3.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1385, 663);
            Controls.Add(button3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Controls.Add(checkBox3);
            Controls.Add(checkBox1);
            Controls.Add(comboBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Массивы";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridView2;
        private DataGridView dataGridView1;
        private ComboBox comboBox1;
        private CheckBox checkBox1;
        private CheckBox checkBox3;
        private Button button1;
        private CheckBox checkBox2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button button2;
        private Button button3;
    }
}