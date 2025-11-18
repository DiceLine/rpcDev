namespace WinFormsRPC
{
    public partial class MainForm
    {
        bool isFilterMenuOpen;
        bool haveSqlMode;
        bool haveTransferMode;

        //int arrayA
        //int arrayB

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
            dataGridViewArrayA = new DataGridView();
            dataGridViewArrayB = new DataGridView();
            comboBoxArrayList = new ComboBox();
            buttonSorting = new Button();
            groupBoxArrayA = new GroupBox();
            pictureBox2 = new PictureBox();
            buttonRandom = new Button();
            textBoxArrayA = new TextBox();
            buttonCreate = new Button();
            groupBoxArrayB = new GroupBox();
            pictureBox1 = new PictureBox();
            textBoxArrayB = new TextBox();
            buttonSave = new Button();
            label1 = new Label();
            checkBoxNotSorted = new CheckBox();
            checkBoxSorted = new CheckBox();
            groupBoxArrayList = new GroupBox();
            groupBoxTransfer = new GroupBox();
            buttonB = new Button();
            buttonA = new Button();
            buttonTransfer = new Button();
            groupBoxFilter = new GroupBox();
            buttonFilter = new Button();
            buttonSqlMode = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewArrayA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewArrayB).BeginInit();
            groupBoxArrayA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            groupBoxArrayB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBoxArrayList.SuspendLayout();
            groupBoxTransfer.SuspendLayout();
            groupBoxFilter.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewArrayA
            // 
            dataGridViewArrayA.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewArrayA.Location = new Point(8, 189);
            dataGridViewArrayA.Margin = new Padding(4);
            dataGridViewArrayA.Name = "dataGridViewArrayA";
            dataGridViewArrayA.RowTemplate.Height = 25;
            dataGridViewArrayA.Size = new Size(338, 425);
            dataGridViewArrayA.TabIndex = 7;
            // 
            // dataGridViewArrayB
            // 
            dataGridViewArrayB.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewArrayB.Location = new Point(8, 189);
            dataGridViewArrayB.Margin = new Padding(4);
            dataGridViewArrayB.Name = "dataGridViewArrayB";
            dataGridViewArrayB.RowTemplate.Height = 25;
            dataGridViewArrayB.Size = new Size(342, 425);
            dataGridViewArrayB.TabIndex = 8;
            // 
            // comboBoxArrayList
            // 
            comboBoxArrayList.FormattingEnabled = true;
            comboBoxArrayList.Items.AddRange(new object[] { "weqwdWQD", "qwdwQWDQW", "QWDqwdwQ", "qwdQWDqwdQWD", "qwdQWDqw", "WQDqwdQWDqw", "1``2342443eqwdd3qeasfdgsfsadssdfbddasdfdsdfdDFADDAFDDasdsfasas", "wqdqawd" });
            comboBoxArrayList.Location = new Point(4, 31);
            comboBoxArrayList.Margin = new Padding(4);
            comboBoxArrayList.Name = "comboBoxArrayList";
            comboBoxArrayList.Size = new Size(338, 29);
            comboBoxArrayList.TabIndex = 9;
            // 
            // buttonSorting
            // 
            buttonSorting.AutoSize = true;
            buttonSorting.BackgroundImageLayout = ImageLayout.None;
            buttonSorting.FlatStyle = FlatStyle.Flat;
            buttonSorting.Image = Resource1.sort_icon;
            buttonSorting.Location = new Point(731, 371);
            buttonSorting.Margin = new Padding(4);
            buttonSorting.Name = "buttonSorting";
            buttonSorting.Size = new Size(56, 56);
            buttonSorting.TabIndex = 13;
            buttonSorting.UseVisualStyleBackColor = true;
            buttonSorting.Click += buttonSorting_Click;
            // 
            // groupBoxArrayA
            // 
            groupBoxArrayA.Controls.Add(pictureBox2);
            groupBoxArrayA.Controls.Add(buttonRandom);
            groupBoxArrayA.Controls.Add(textBoxArrayA);
            groupBoxArrayA.Controls.Add(buttonCreate);
            groupBoxArrayA.Controls.Add(dataGridViewArrayA);
            groupBoxArrayA.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxArrayA.Location = new Point(366, 13);
            groupBoxArrayA.Margin = new Padding(4);
            groupBoxArrayA.Name = "groupBoxArrayA";
            groupBoxArrayA.Padding = new Padding(4);
            groupBoxArrayA.Size = new Size(357, 627);
            groupBoxArrayA.TabIndex = 16;
            groupBoxArrayA.TabStop = false;
            groupBoxArrayA.Text = "Первичный массив";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Resource1.arrayA_icon50;
            pictureBox2.Location = new Point(8, 29);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(50, 50);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 26;
            pictureBox2.TabStop = false;
            // 
            // buttonRandom
            // 
            buttonRandom.AutoSize = true;
            buttonRandom.FlatStyle = FlatStyle.Flat;
            buttonRandom.Image = Resource1.random_generation;
            buttonRandom.Location = new Point(196, 48);
            buttonRandom.Margin = new Padding(4);
            buttonRandom.Name = "buttonRandom";
            buttonRandom.Size = new Size(72, 72);
            buttonRandom.TabIndex = 25;
            buttonRandom.UseVisualStyleBackColor = true;
            buttonRandom.Click += buttonRandom_Click;
            // 
            // textBoxArrayA
            // 
            textBoxArrayA.Location = new Point(8, 138);
            textBoxArrayA.Margin = new Padding(4);
            textBoxArrayA.Name = "textBoxArrayA";
            textBoxArrayA.Size = new Size(337, 29);
            textBoxArrayA.TabIndex = 19;
            textBoxArrayA.TextChanged += textBoxArrayA_TextChanged;
            // 
            // buttonCreate
            // 
            buttonCreate.AutoSize = true;
            buttonCreate.FlatStyle = FlatStyle.Flat;
            buttonCreate.Image = Resource1.create_icon;
            buttonCreate.Location = new Point(95, 48);
            buttonCreate.Margin = new Padding(4);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(72, 72);
            buttonCreate.TabIndex = 18;
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // groupBoxArrayB
            // 
            groupBoxArrayB.Controls.Add(pictureBox1);
            groupBoxArrayB.Controls.Add(textBoxArrayB);
            groupBoxArrayB.Controls.Add(dataGridViewArrayB);
            groupBoxArrayB.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxArrayB.Location = new Point(795, 13);
            groupBoxArrayB.Margin = new Padding(4);
            groupBoxArrayB.Name = "groupBoxArrayB";
            groupBoxArrayB.Padding = new Padding(4);
            groupBoxArrayB.Size = new Size(357, 627);
            groupBoxArrayB.TabIndex = 17;
            groupBoxArrayB.TabStop = false;
            groupBoxArrayB.Text = "Отсортированный";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Resource1.arrayB_icon50;
            pictureBox1.Location = new Point(8, 29);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // textBoxArrayB
            // 
            textBoxArrayB.Location = new Point(8, 138);
            textBoxArrayB.Margin = new Padding(4);
            textBoxArrayB.Name = "textBoxArrayB";
            textBoxArrayB.Size = new Size(341, 29);
            textBoxArrayB.TabIndex = 20;
            // 
            // buttonSave
            // 
            buttonSave.AutoSize = true;
            buttonSave.BackgroundImageLayout = ImageLayout.None;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Image = Resource1.save_button;
            buttonSave.Location = new Point(277, 13);
            buttonSave.Margin = new Padding(4);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(56, 56);
            buttonSave.TabIndex = 18;
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(104, 540);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(51, 21);
            label1.TabIndex = 19;
            label1.Text = "label1";
            // 
            // checkBoxNotSorted
            // 
            checkBoxNotSorted.AutoSize = true;
            checkBoxNotSorted.CheckAlign = ContentAlignment.MiddleRight;
            checkBoxNotSorted.Location = new Point(8, 30);
            checkBoxNotSorted.Margin = new Padding(4);
            checkBoxNotSorted.Name = "checkBoxNotSorted";
            checkBoxNotSorted.Size = new Size(118, 25);
            checkBoxNotSorted.TabIndex = 12;
            checkBoxNotSorted.Text = "Первичные";
            checkBoxNotSorted.UseVisualStyleBackColor = true;
            // 
            // checkBoxSorted
            // 
            checkBoxSorted.AutoSize = true;
            checkBoxSorted.CheckAlign = ContentAlignment.MiddleRight;
            checkBoxSorted.Location = new Point(8, 62);
            checkBoxSorted.Margin = new Padding(4);
            checkBoxSorted.Name = "checkBoxSorted";
            checkBoxSorted.Size = new Size(169, 25);
            checkBoxSorted.TabIndex = 20;
            checkBoxSorted.Text = "Отсортированныe";
            checkBoxSorted.UseVisualStyleBackColor = true;
            // 
            // groupBoxArrayList
            // 
            groupBoxArrayList.Controls.Add(groupBoxTransfer);
            groupBoxArrayList.Controls.Add(buttonTransfer);
            groupBoxArrayList.Controls.Add(groupBoxFilter);
            groupBoxArrayList.Controls.Add(buttonFilter);
            groupBoxArrayList.Controls.Add(comboBoxArrayList);
            groupBoxArrayList.Location = new Point(4, 77);
            groupBoxArrayList.Margin = new Padding(4);
            groupBoxArrayList.Name = "groupBoxArrayList";
            groupBoxArrayList.Padding = new Padding(4);
            groupBoxArrayList.Size = new Size(354, 315);
            groupBoxArrayList.TabIndex = 23;
            groupBoxArrayList.TabStop = false;
            groupBoxArrayList.Text = "Список массивов";
            // 
            // groupBoxTransfer
            // 
            groupBoxTransfer.Controls.Add(buttonB);
            groupBoxTransfer.Controls.Add(buttonA);
            groupBoxTransfer.Location = new Point(212, 68);
            groupBoxTransfer.Name = "groupBoxTransfer";
            groupBoxTransfer.Size = new Size(48, 98);
            groupBoxTransfer.TabIndex = 25;
            groupBoxTransfer.TabStop = false;
            // 
            // buttonB
            // 
            buttonB.AutoSize = true;
            buttonB.FlatStyle = FlatStyle.Flat;
            buttonB.Image = Resource1.arrayB_icon24;
            buttonB.Location = new Point(6, 57);
            buttonB.Name = "buttonB";
            buttonB.Size = new Size(35, 33);
            buttonB.TabIndex = 1;
            buttonB.Text = "\r\n";
            buttonB.UseVisualStyleBackColor = true;
            // 
            // buttonA
            // 
            buttonA.AutoSize = true;
            buttonA.FlatStyle = FlatStyle.Flat;
            buttonA.Image = Resource1.arrayA_icon24;
            buttonA.Location = new Point(6, 18);
            buttonA.Name = "buttonA";
            buttonA.Size = new Size(35, 33);
            buttonA.TabIndex = 0;
            buttonA.Text = "\r\n";
            buttonA.UseVisualStyleBackColor = true;
            // 
            // buttonTransfer
            // 
            buttonTransfer.AutoSize = true;
            buttonTransfer.FlatStyle = FlatStyle.Flat;
            buttonTransfer.Image = Resource1.transfer_icon;
            buttonTransfer.Location = new Point(266, 85);
            buttonTransfer.Name = "buttonTransfer";
            buttonTransfer.Size = new Size(32, 32);
            buttonTransfer.TabIndex = 23;
            buttonTransfer.UseVisualStyleBackColor = true;
            buttonTransfer.Click += buttonTransfer_Click;
            // 
            // groupBoxFilter
            // 
            groupBoxFilter.Controls.Add(checkBoxNotSorted);
            groupBoxFilter.Controls.Add(checkBoxSorted);
            groupBoxFilter.Location = new Point(9, 68);
            groupBoxFilter.Margin = new Padding(4);
            groupBoxFilter.Name = "groupBoxFilter";
            groupBoxFilter.Padding = new Padding(4);
            groupBoxFilter.Size = new Size(188, 95);
            groupBoxFilter.TabIndex = 22;
            groupBoxFilter.TabStop = false;
            // 
            // buttonFilter
            // 
            buttonFilter.AutoSize = true;
            buttonFilter.FlatStyle = FlatStyle.Flat;
            buttonFilter.Image = Resource1.filter_icon;
            buttonFilter.Location = new Point(305, 85);
            buttonFilter.Margin = new Padding(4);
            buttonFilter.Name = "buttonFilter";
            buttonFilter.Size = new Size(32, 32);
            buttonFilter.TabIndex = 21;
            buttonFilter.UseVisualStyleBackColor = true;
            buttonFilter.Click += buttonFilter_Click;
            // 
            // buttonSqlMode
            // 
            buttonSqlMode.AutoSize = true;
            buttonSqlMode.FlatStyle = FlatStyle.Flat;
            buttonSqlMode.Image = Resource1.database_icon;
            buttonSqlMode.Location = new Point(202, 13);
            buttonSqlMode.Margin = new Padding(4);
            buttonSqlMode.Name = "buttonSqlMode";
            buttonSqlMode.Size = new Size(56, 56);
            buttonSqlMode.TabIndex = 24;
            buttonSqlMode.UseVisualStyleBackColor = true;
            buttonSqlMode.Click += buttonSqlMode_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1463, 654);
            Controls.Add(buttonSqlMode);
            Controls.Add(groupBoxArrayList);
            Controls.Add(label1);
            Controls.Add(buttonSave);
            Controls.Add(groupBoxArrayB);
            Controls.Add(groupBoxArrayA);
            Controls.Add(buttonSorting);
            Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "Массивы";
            ((System.ComponentModel.ISupportInitialize)dataGridViewArrayA).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewArrayB).EndInit();
            groupBoxArrayA.ResumeLayout(false);
            groupBoxArrayA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            groupBoxArrayB.ResumeLayout(false);
            groupBoxArrayB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBoxArrayList.ResumeLayout(false);
            groupBoxArrayList.PerformLayout();
            groupBoxTransfer.ResumeLayout(false);
            groupBoxTransfer.PerformLayout();
            groupBoxFilter.ResumeLayout(false);
            groupBoxFilter.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridViewArrayA;
        private DataGridView dataGridViewArrayB;
        private ComboBox comboBoxArrayList;
        private Button buttonSorting;
        private GroupBox groupBoxArrayA;
        private GroupBox groupBoxArrayB;
        private Button buttonCreate;
        private Button buttonSave;
        private Label label1;
        private TextBox textBoxArrayA;
        private TextBox textBoxArrayB;
        private CheckBox checkBoxNotSorted;
        private CheckBox checkBoxSorted;
        private GroupBox groupBoxArrayList;
        private Button buttonFilter;
        private GroupBox groupBoxFilter;
        private Button buttonRandom;
        private Button buttonSqlMode;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Button buttonTransfer;
        private GroupBox groupBoxTransfer;
        private Button buttonB;
        private Button buttonA;
    }
}