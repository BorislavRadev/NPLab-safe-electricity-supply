namespace NPLab
{
    partial class EditProtectors
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditProtectors));
            this.CurrProts = new System.Windows.Forms.DataGridView();
            this.Add = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.NameInst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxDN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.CurrProts)).BeginInit();
            this.SuspendLayout();
            // 
            // CurrProts
            // 
            this.CurrProts.AllowUserToAddRows = false;
            this.CurrProts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.CurrProts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CurrProts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Number,
            this.Type,
            this.NameInst,
            this.DT,
            this.DN,
            this.MaxDT,
            this.MaxDN});
            this.CurrProts.Location = new System.Drawing.Point(14, 14);
            this.CurrProts.Name = "CurrProts";
            this.CurrProts.Size = new System.Drawing.Size(675, 252);
            this.CurrProts.TabIndex = 0;
            this.CurrProts.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.CurrProts_CellBeginEdit);
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(14, 271);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(225, 27);
            this.Add.TabIndex = 1;
            this.Add.Text = "Добави";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(459, 272);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(230, 27);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "Отказ";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(245, 271);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(208, 27);
            this.Save.TabIndex = 3;
            this.Save.Text = "Запиши";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Number
            // 
            this.Number.DataPropertyName = "Number";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = "0";
            this.Number.DefaultCellStyle = dataGridViewCellStyle1;
            this.Number.FillWeight = 41.86603F;
            this.Number.HeaderText = "№";
            this.Number.Name = "Number";
            this.Number.Width = 25;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "TypeProtector";
            this.Type.FillWeight = 279.1308F;
            this.Type.HeaderText = "Тип";
            this.Type.Name = "Type";
            this.Type.Width = 167;
            // 
            // NameInst
            // 
            this.NameInst.DataPropertyName = "Name";
            this.NameInst.FillWeight = 147.9484F;
            this.NameInst.HeaderText = "Име";
            this.NameInst.Name = "NameInst";
            this.NameInst.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NameInst.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NameInst.Width = 200;
            // 
            // DT
            // 
            this.DT.DataPropertyName = "DT";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.DT.DefaultCellStyle = dataGridViewCellStyle2;
            this.DT.FillWeight = 49.41753F;
            this.DT.HeaderText = "t. изкл.";
            this.DT.Name = "DT";
            this.DT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DT.Width = 50;
            // 
            // DN
            // 
            this.DN.DataPropertyName = "DN";
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0";
            this.DN.DefaultCellStyle = dataGridViewCellStyle3;
            this.DN.FillWeight = 55.30331F;
            this.DN.HeaderText = "I ΔN";
            this.DN.Name = "DN";
            this.DN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DN.Width = 50;
            // 
            // MaxDT
            // 
            this.MaxDT.DataPropertyName = "MaxDT";
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = "0";
            this.MaxDT.DefaultCellStyle = dataGridViewCellStyle4;
            this.MaxDT.FillWeight = 60.69626F;
            this.MaxDT.HeaderText = "Макс t. изкл.";
            this.MaxDT.Name = "MaxDT";
            this.MaxDT.Width = 50;
            // 
            // MaxDN
            // 
            this.MaxDN.DataPropertyName = "MaxDN";
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0";
            this.MaxDN.DefaultCellStyle = dataGridViewCellStyle5;
            this.MaxDN.FillWeight = 65.63766F;
            this.MaxDN.HeaderText = "Макс. I ΔN";
            this.MaxDN.Name = "MaxDN";
            this.MaxDN.Width = 50;
            // 
            // EditProtectors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 306);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.CurrProts);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditProtectors";
            this.Text = "Прекъсвачи за защитно изключване";
            this.Load += new System.EventHandler(this.EditProts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CurrProts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView CurrProts;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.DataGridViewTextBoxColumn Number;
        private System.Windows.Forms.DataGridViewComboBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameInst;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DN;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxDN;
    }
}