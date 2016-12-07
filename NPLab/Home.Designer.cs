namespace NPLab
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.NewObject = new System.Windows.Forms.Button();
            this.OpenEx = new System.Windows.Forms.Button();
            this.Help = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // NewObject
            // 
            this.NewObject.Location = new System.Drawing.Point(14, 396);
            this.NewObject.Name = "NewObject";
            this.NewObject.Size = new System.Drawing.Size(310, 44);
            this.NewObject.TabIndex = 0;
            this.NewObject.Text = "Нов обект";
            this.NewObject.UseVisualStyleBackColor = true;
            this.NewObject.Click += new System.EventHandler(this.NewObject_Click);
            // 
            // OpenEx
            // 
            this.OpenEx.Location = new System.Drawing.Point(331, 396);
            this.OpenEx.Name = "OpenEx";
            this.OpenEx.Size = new System.Drawing.Size(310, 44);
            this.OpenEx.TabIndex = 1;
            this.OpenEx.Text = "Зареди обект";
            this.OpenEx.UseVisualStyleBackColor = true;
            this.OpenEx.Click += new System.EventHandler(this.OpenEx_Click);
            // 
            // Help
            // 
            this.Help.Location = new System.Drawing.Point(14, 447);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(310, 42);
            this.Help.TabIndex = 4;
            this.Help.Text = "Помощ";
            this.Help.UseVisualStyleBackColor = true;
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(331, 447);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(310, 42);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "Затвори";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Image = global::NPLab.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(14, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(628, 378);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 498);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.Help);
            this.Controls.Add(this.OpenEx);
            this.Controls.Add(this.NewObject);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Home";
            this.Text = "Начало - NPLab";
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NewObject;
        private System.Windows.Forms.Button OpenEx;
        private System.Windows.Forms.Button Help;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}