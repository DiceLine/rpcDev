namespace WinFormsRPC
{
    public partial class MainForm
    {
        bool isFilterMenuOpen;
        bool haveSqlMode;
        bool haveTransferMode;
        bool haveRandomMode;

        private List<int> originalArray = new List<int>();
        private List<int> sortedArray = new List<int>();

        private DatabaseConfig dbConfig;
        private string configFilePath = "config.xml";

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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dataGridViewArrayA = new DataGridView();
            dataGridViewArrayB = new DataGridView();
            comboBoxArrayList = new ComboBox();
            buttonSorting = new Button();
            groupBoxArrayA = new GroupBox();
            buttonDeleteA = new Button();
            groupBoxCreate = new GroupBox();
            maskedTextBoxArraySize = new MaskedTextBox();
            groupBoxRandom = new GroupBox();
            maskedTextBoxRandBegin = new MaskedTextBox();
            maskedTextBoxRandEnd = new MaskedTextBox();
            labelRandom = new Label();
            pictureBoxA = new PictureBox();
            buttonRandom = new Button();
            textBoxArrayA = new TextBox();
            buttonCreate = new Button();
            groupBoxArrayB = new GroupBox();
            buttonDeleteB = new Button();
            pictureBoxB = new PictureBox();
            textBoxArrayB = new TextBox();
            buttonSave = new Button();
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
            groupBoxSql = new GroupBox();
            buttonSqlDelete = new Button();
            errorProvider = new ErrorProvider(components);
            toolTip = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dataGridViewArrayA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewArrayB).BeginInit();
            groupBoxArrayA.SuspendLayout();
            groupBoxCreate.SuspendLayout();
            groupBoxRandom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxA).BeginInit();
            groupBoxArrayB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxB).BeginInit();
            groupBoxArrayList.SuspendLayout();
            groupBoxTransfer.SuspendLayout();
            groupBoxFilter.SuspendLayout();
            groupBoxSql.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewArrayA
            // 
            dataGridViewArrayA.AllowUserToDeleteRows = false;
            dataGridViewArrayA.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewArrayA.Location = new Point(8, 259);
            dataGridViewArrayA.Margin = new Padding(4);
            dataGridViewArrayA.Name = "dataGridViewArrayA";
            dataGridViewArrayA.RowTemplate.Height = 25;
            dataGridViewArrayA.Size = new Size(338, 355);
            dataGridViewArrayA.TabIndex = 7;
            // 
            // dataGridViewArrayB
            // 
            dataGridViewArrayB.AllowUserToAddRows = false;
            dataGridViewArrayB.AllowUserToDeleteRows = false;
            dataGridViewArrayB.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewArrayB.Location = new Point(8, 259);
            dataGridViewArrayB.Margin = new Padding(4);
            dataGridViewArrayB.Name = "dataGridViewArrayB";
            dataGridViewArrayB.ReadOnly = true;
            dataGridViewArrayB.RowTemplate.Height = 25;
            dataGridViewArrayB.Size = new Size(342, 355);
            dataGridViewArrayB.TabIndex = 8;
            // 
            // comboBoxArrayList
            // 
            comboBoxArrayList.FormattingEnabled = true;
            comboBoxArrayList.Items.AddRange(new object[] { "weqwdWQD", "qwdwQWDQW", "QWDqwdwQ", "qwdQWDqwdQWD", "qwdQWDqw", "WQDqwdQWDqw", "1``2342443eqwdd3qeasfdgsfsadssdfbddasdfdsdfdDFADDAFDDasdsfasas", "wqdqawd" });
            comboBoxArrayList.Location = new Point(9, 31);
            comboBoxArrayList.Margin = new Padding(4);
            comboBoxArrayList.Name = "comboBoxArrayList";
            comboBoxArrayList.Size = new Size(333, 29);
            comboBoxArrayList.TabIndex = 9;
            // 
            // buttonSorting
            // 
            buttonSorting.AutoSize = true;
            buttonSorting.BackgroundImageLayout = ImageLayout.None;
            buttonSorting.FlatStyle = FlatStyle.Flat;
            buttonSorting.Image = Resource1.sort_icon;
            buttonSorting.Location = new Point(731, 403);
            buttonSorting.Margin = new Padding(4);
            buttonSorting.Name = "buttonSorting";
            buttonSorting.Size = new Size(56, 56);
            buttonSorting.TabIndex = 13;
            buttonSorting.UseVisualStyleBackColor = true;
            buttonSorting.Click += buttonSorting_Click;
            // 
            // groupBoxArrayA
            // 
            groupBoxArrayA.Controls.Add(buttonDeleteA);
            groupBoxArrayA.Controls.Add(groupBoxCreate);
            groupBoxArrayA.Controls.Add(groupBoxRandom);
            groupBoxArrayA.Controls.Add(pictureBoxA);
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
            // buttonDeleteA
            // 
            buttonDeleteA.AutoSize = true;
            buttonDeleteA.FlatStyle = FlatStyle.Flat;
            buttonDeleteA.Image = Resource1.delete_icon;
            buttonDeleteA.Location = new Point(273, 34);
            buttonDeleteA.Margin = new Padding(4);
            buttonDeleteA.Name = "buttonDeleteA";
            buttonDeleteA.Size = new Size(72, 72);
            buttonDeleteA.TabIndex = 29;
            buttonDeleteA.UseVisualStyleBackColor = true;
            buttonDeleteA.Click += buttonDeleteA_Click;
            // 
            // groupBoxCreate
            // 
            groupBoxCreate.Controls.Add(maskedTextBoxArraySize);
            groupBoxCreate.Location = new Point(47, 113);
            groupBoxCreate.Name = "groupBoxCreate";
            groupBoxCreate.Size = new Size(111, 100);
            groupBoxCreate.TabIndex = 28;
            groupBoxCreate.TabStop = false;
            groupBoxCreate.Text = "Размер";
            // 
            // maskedTextBoxArraySize
            // 
            maskedTextBoxArraySize.Location = new Point(16, 45);
            maskedTextBoxArraySize.Name = "maskedTextBoxArraySize";
            maskedTextBoxArraySize.Size = new Size(44, 29);
            maskedTextBoxArraySize.TabIndex = 0;
            // 
            // groupBoxRandom
            // 
            groupBoxRandom.Controls.Add(maskedTextBoxRandBegin);
            groupBoxRandom.Controls.Add(maskedTextBoxRandEnd);
            groupBoxRandom.Controls.Add(labelRandom);
            groupBoxRandom.Location = new Point(176, 113);
            groupBoxRandom.Name = "groupBoxRandom";
            groupBoxRandom.Size = new Size(169, 100);
            groupBoxRandom.TabIndex = 27;
            groupBoxRandom.TabStop = false;
            groupBoxRandom.Text = "Диапазон";
            // 
            // maskedTextBoxRandBegin
            // 
            maskedTextBoxRandBegin.Location = new Point(6, 45);
            maskedTextBoxRandBegin.Name = "maskedTextBoxRandBegin";
            maskedTextBoxRandBegin.Size = new Size(53, 29);
            maskedTextBoxRandBegin.TabIndex = 28;
            // 
            // maskedTextBoxRandEnd
            // 
            maskedTextBoxRandEnd.Location = new Point(100, 45);
            maskedTextBoxRandEnd.Name = "maskedTextBoxRandEnd";
            maskedTextBoxRandEnd.Size = new Size(54, 29);
            maskedTextBoxRandEnd.TabIndex = 1;
            // 
            // labelRandom
            // 
            labelRandom.AutoSize = true;
            labelRandom.Location = new Point(65, 48);
            labelRandom.Name = "labelRandom";
            labelRandom.Size = new Size(29, 21);
            labelRandom.TabIndex = 0;
            labelRandom.Text = "до";
            // 
            // pictureBoxA
            // 
            pictureBoxA.Image = Resource1.arrayA_icon50;
            pictureBoxA.Location = new Point(8, 29);
            pictureBoxA.Name = "pictureBoxA";
            pictureBoxA.Size = new Size(50, 50);
            pictureBoxA.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxA.TabIndex = 26;
            pictureBoxA.TabStop = false;
            // 
            // buttonRandom
            // 
            buttonRandom.AutoSize = true;
            buttonRandom.FlatStyle = FlatStyle.Flat;
            buttonRandom.Image = Resource1.random_generation;
            buttonRandom.Location = new Point(176, 34);
            buttonRandom.Margin = new Padding(4);
            buttonRandom.Name = "buttonRandom";
            buttonRandom.Size = new Size(72, 72);
            buttonRandom.TabIndex = 25;
            buttonRandom.UseVisualStyleBackColor = true;
            buttonRandom.Click += buttonRandom_Click;
            // 
            // textBoxArrayA
            // 
            textBoxArrayA.Location = new Point(8, 222);
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
            buttonCreate.Location = new Point(86, 34);
            buttonCreate.Margin = new Padding(4);
            buttonCreate.Name = "buttonCreate";
            buttonCreate.Size = new Size(72, 72);
            buttonCreate.TabIndex = 18;
            buttonCreate.UseVisualStyleBackColor = true;
            buttonCreate.Click += buttonCreate_Click;
            // 
            // groupBoxArrayB
            // 
            groupBoxArrayB.Controls.Add(buttonDeleteB);
            groupBoxArrayB.Controls.Add(pictureBoxB);
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
            // buttonDeleteB
            // 
            buttonDeleteB.AutoSize = true;
            buttonDeleteB.FlatStyle = FlatStyle.Flat;
            buttonDeleteB.Image = Resource1.delete_icon;
            buttonDeleteB.Location = new Point(277, 34);
            buttonDeleteB.Margin = new Padding(4);
            buttonDeleteB.Name = "buttonDeleteB";
            buttonDeleteB.Size = new Size(72, 72);
            buttonDeleteB.TabIndex = 30;
            buttonDeleteB.UseVisualStyleBackColor = true;
            buttonDeleteB.Click += buttonDeleteB_Click;
            // 
            // pictureBoxB
            // 
            pictureBoxB.Image = Resource1.arrayB_icon50;
            pictureBoxB.Location = new Point(8, 29);
            pictureBoxB.Name = "pictureBoxB";
            pictureBoxB.Size = new Size(50, 50);
            pictureBoxB.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBoxB.TabIndex = 21;
            pictureBoxB.TabStop = false;
            // 
            // textBoxArrayB
            // 
            textBoxArrayB.Location = new Point(8, 222);
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
            buttonSave.Location = new Point(84, 29);
            buttonSave.Margin = new Padding(4);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(56, 56);
            buttonSave.TabIndex = 18;
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
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
            checkBoxNotSorted.CheckedChanged += checkBoxNotSorted_CheckedChanged;
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
            checkBoxSorted.CheckedChanged += checkBoxSorted_CheckedChanged;
            // 
            // groupBoxArrayList
            // 
            groupBoxArrayList.Controls.Add(groupBoxTransfer);
            groupBoxArrayList.Controls.Add(buttonTransfer);
            groupBoxArrayList.Controls.Add(groupBoxFilter);
            groupBoxArrayList.Controls.Add(buttonFilter);
            groupBoxArrayList.Controls.Add(comboBoxArrayList);
            groupBoxArrayList.Location = new Point(8, 126);
            groupBoxArrayList.Margin = new Padding(4);
            groupBoxArrayList.Name = "groupBoxArrayList";
            groupBoxArrayList.Padding = new Padding(4);
            groupBoxArrayList.Size = new Size(350, 184);
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
            buttonB.Click += buttonB_Click;
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
            buttonA.Click += buttonA_Click;
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
            buttonSqlMode.Location = new Point(9, 29);
            buttonSqlMode.Margin = new Padding(4);
            buttonSqlMode.Name = "buttonSqlMode";
            buttonSqlMode.Size = new Size(56, 56);
            buttonSqlMode.TabIndex = 24;
            buttonSqlMode.UseVisualStyleBackColor = true;
            buttonSqlMode.Click += buttonSqlMode_Click;
            // 
            // groupBoxSql
            // 
            groupBoxSql.Controls.Add(buttonSqlDelete);
            groupBoxSql.Controls.Add(buttonSqlMode);
            groupBoxSql.Controls.Add(buttonSave);
            groupBoxSql.Location = new Point(8, 13);
            groupBoxSql.Name = "groupBoxSql";
            groupBoxSql.Size = new Size(350, 97);
            groupBoxSql.TabIndex = 25;
            groupBoxSql.TabStop = false;
            groupBoxSql.Text = "Взаимодействие с базой данных";
            // 
            // buttonSqlDelete
            // 
            buttonSqlDelete.AutoSize = true;
            buttonSqlDelete.FlatStyle = FlatStyle.Flat;
            buttonSqlDelete.Image = Resource1.delete_icon;
            buttonSqlDelete.Location = new Point(160, 27);
            buttonSqlDelete.Margin = new Padding(4);
            buttonSqlDelete.Name = "buttonSqlDelete";
            buttonSqlDelete.Size = new Size(58, 58);
            buttonSqlDelete.TabIndex = 30;
            buttonSqlDelete.UseVisualStyleBackColor = true;
            buttonSqlDelete.Click += buttonSqlDelete_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(1157, 644);
            Controls.Add(groupBoxSql);
            Controls.Add(groupBoxArrayList);
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
            groupBoxCreate.ResumeLayout(false);
            groupBoxCreate.PerformLayout();
            groupBoxRandom.ResumeLayout(false);
            groupBoxRandom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxA).EndInit();
            groupBoxArrayB.ResumeLayout(false);
            groupBoxArrayB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxB).EndInit();
            groupBoxArrayList.ResumeLayout(false);
            groupBoxArrayList.PerformLayout();
            groupBoxTransfer.ResumeLayout(false);
            groupBoxTransfer.PerformLayout();
            groupBoxFilter.ResumeLayout(false);
            groupBoxFilter.PerformLayout();
            groupBoxSql.ResumeLayout(false);
            groupBoxSql.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
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
        private TextBox textBoxArrayA;
        private TextBox textBoxArrayB;
        private CheckBox checkBoxNotSorted;
        private CheckBox checkBoxSorted;
        private GroupBox groupBoxArrayList;
        private Button buttonFilter;
        private GroupBox groupBoxFilter;
        private Button buttonRandom;
        private Button buttonSqlMode;
        private PictureBox pictureBoxA;
        private PictureBox pictureBoxB;
        private Button buttonTransfer;
        private GroupBox groupBoxTransfer;
        private Button buttonB;
        private Button buttonA;
        private GroupBox groupBoxSql;
        private GroupBox groupBoxRandom;
        private MaskedTextBox maskedTextBoxRandBegin;
        private MaskedTextBox maskedTextBoxRandEnd;
        private Label labelRandom;
        private GroupBox groupBoxCreate;
        private MaskedTextBox maskedTextBoxArraySize;
        private Button buttonDeleteA;
        private Button buttonDeleteB;
        private Button buttonSqlDelete;
        private ErrorProvider errorProvider;
        private ToolTip toolTip;
    }
}