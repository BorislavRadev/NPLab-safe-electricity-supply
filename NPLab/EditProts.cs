using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPLab.Models;

namespace NPLab
{
    public partial class EditProtectors : Form
    {
        BindingSource bind = new BindingSource();
        Main M;
        public EditProtectors(Main M)
        {
            this.M = M;
            InitializeComponent();
        }

        private void EditProts_Load(object sender, EventArgs e)
        {
            foreach(Protectors pro in Main.Stats.Protectors) bind.Add(pro);
            Protectors First = new Protectors();
            First.Number = 1;
            //First.DN = r.NextDouble() * ()
            if (Main.Stats.Protectors.Count == 0)
            {
                Main.Stats.Protectors.Add(First);
                bind.Add(First);
            }
            CurrProts.DataSource = bind;
            (CurrProts.Columns[1] as DataGridViewComboBoxColumn).Items.AddRange(Main.Stats.TypesProtectors);
            CurrProts.Update();
            CurrProts.Refresh();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            Protectors pro = new Protectors();
            pro.Number = Main.Stats.Protectors.Count + 1;
            pro.TypeProtector = Main.Stats.Protectors[Main.Stats.Protectors.Count - 1].TypeProtector;
            pro.DN = (r.Next((int)M.tempDef.DNLowest * 10, (int)M.tempDef.DNHighest * 10)) / 10;
            pro.DT = (r.Next((int)M.tempDef.DTLowest * 10, (int)M.tempDef.DTHighest * 10)) / 10;
            pro.MaxDN = M.tempDef.maxDN;
            pro.MaxDT = M.tempDef.maxDT;
            Main.Stats.Protectors.Add(pro);
            bind.Add(pro);
            CurrProts.DataSource = bind;
            CurrProts.Update();
            CurrProts.Refresh();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Objects.Stats.Protectors = Main.Stats.Protectors;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Main.Stats.Protectors = Objects.Stats.Protectors;
            this.Close();
        }

        private void CurrProts_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            Random r = new Random();
            if (e.ColumnIndex == 1 || e.ColumnIndex == 3)
            {
                Main.Stats.Protectors[e.RowIndex].Name =
                    "Прекъсвач за защитно изключване №" +
                    Main.Stats.Protectors[e.RowIndex].Number.ToString() + " тип " +
                    Main.Stats.Protectors[e.RowIndex].TypeProtector + ",  I∆n=" +
                    Main.Stats.Protectors[e.RowIndex].DN.ToString() + "А";
            }
            if (e.ColumnIndex == 1 && e.RowIndex == 0)
            {
                Main.Stats.Protectors[e.RowIndex].DN = (r.Next((int)M.tempDef.DNLowest * 10, (int)M.tempDef.DNHighest * 10)) / 10;
                Main.Stats.Protectors[e.RowIndex].DT = (r.Next((int)M.tempDef.DTLowest * 10, (int)M.tempDef.DTHighest * 10)) / 10;
                Main.Stats.Protectors[e.RowIndex].MaxDN = M.tempDef.maxDN;
                Main.Stats.Protectors[e.RowIndex].MaxDT = M.tempDef.maxDT;
            }
            CurrProts.Update();
            CurrProts.Refresh();
        }

    }
}
