namespace NPLab
{
    partial class ChooseObj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseObj));
            this.ListObjects = new System.Windows.Forms.ListBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SearchField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ListObjects
            // 
            this.ListObjects.FormattingEnabled = true;
            this.ListObjects.ItemHeight = 15;
            this.ListObjects.Location = new System.Drawing.Point(14, 59);
            this.ListObjects.Name = "ListObjects";
            this.ListObjects.Size = new System.Drawing.Size(452, 244);
            this.ListObjects.TabIndex = 0;
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(14, 310);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(224, 31);
            this.LoadButton.TabIndex = 1;
            this.LoadButton.Text = "Зареди";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.Load_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(245, 310);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(224, 31);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Отказ";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SearchField
            // 
            this.SearchField.Location = new System.Drawing.Point(14, 29);
            this.SearchField.Name = "SearchField";
            this.SearchField.Size = new System.Drawing.Size(452, 21);
            this.SearchField.TabIndex = 3;
            this.SearchField.TextChanged += new System.EventHandler(this.SearchField_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(324, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Потърсете името на обекта и го изберете от списъка:";
            // 
            // ChooseObj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 345);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchField);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.ListObjects);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChooseObj";
            this.Text = "Изберете обект";
            this.Load += new System.EventHandler(this.ChooseObj_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListObjects;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.TextBox SearchField;
        private System.Windows.Forms.Label label1;
    }
}