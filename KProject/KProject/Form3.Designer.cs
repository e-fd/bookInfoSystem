namespace KProject
{
    partial class About
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
            labelProductName = new Label();
            labelVersion = new Label();
            labelCopyright = new Label();
            labelCompanyName = new Label();
            textBoxDescription = new TextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // labelProductName
            // 
            labelProductName.AutoSize = true;
            labelProductName.Location = new Point(3, 0);
            labelProductName.Name = "labelProductName";
            labelProductName.Size = new Size(144, 20);
            labelProductName.TabIndex = 0;
            labelProductName.Text = "Название продукта";
            // 
            // labelVersion
            // 
            labelVersion.AutoSize = true;
            labelVersion.Location = new Point(3, 36);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new Size(59, 20);
            labelVersion.TabIndex = 1;
            labelVersion.Text = "Версия";
            // 
            // labelCopyright
            // 
            labelCopyright.AutoSize = true;
            labelCopyright.Location = new Point(3, 72);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new Size(128, 20);
            labelCopyright.TabIndex = 2;
            labelCopyright.Text = "Авторские права";
            // 
            // labelCompanyName
            // 
            labelCompanyName.AutoSize = true;
            labelCompanyName.Location = new Point(3, 114);
            labelCompanyName.Name = "labelCompanyName";
            labelCompanyName.Size = new Size(166, 20);
            labelCompanyName.TabIndex = 3;
            labelCompanyName.Text = "Название органиации";
            // 
            // textBoxDescription
            // 
            textBoxDescription.Location = new Point(12, 187);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.ReadOnly = true;
            textBoxDescription.ScrollBars = ScrollBars.Both;
            textBoxDescription.Size = new Size(450, 192);
            textBoxDescription.TabIndex = 4;
            textBoxDescription.TabStop = false;
            textBoxDescription.Text = "Описание";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(labelProductName, 0, 0);
            tableLayoutPanel1.Controls.Add(labelVersion, 0, 1);
            tableLayoutPanel1.Controls.Add(labelCompanyName, 0, 3);
            tableLayoutPanel1.Controls.Add(labelCopyright, 0, 2);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.Size = new Size(450, 159);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(474, 391);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(textBoxDescription);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "About";
            Text = "О программе";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelProductName;
        private Label labelVersion;
        private Label labelCopyright;
        private Label labelCompanyName;
        private TextBox textBoxDescription;
        private TableLayoutPanel tableLayoutPanel1;
    }
}