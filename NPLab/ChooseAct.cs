using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.ObjectModel;
using Microsoft.Office.Interop.Word;
using NPLab.Data.Migrations;
using NPLab.Data;
using NPLab.Models;

namespace NPLab
{
    public partial class ChooseAct : Form
    {
        public List<DateTime> dates = new List<DateTime>();
        public Main M;
        public Objects currObj = new Objects();
        public int el, objId;
        public ChooseAct(ICollection<EL_1> l, Main M, int objId)
        {
            foreach (EL_1 p in l) dates.Add(p.Date);
            this.M = M;
            this.objId = objId;
            el = 0;
            InitializeComponent();
        }
        public ChooseAct(ICollection<El_2> l, Main M, int objId)
        {
            foreach (El_2 p in l) dates.Add(p.ControlDate);
            this.M = M;
            this.objId = objId;
            el = 1;
            InitializeComponent();
        }
        public ChooseAct(ICollection<EL_3> l, Main M, int objId)
        {
            foreach (EL_3 p in l) dates.Add(p.Date);
            this.M = M;
            this.objId = objId;
            el = 2;
            InitializeComponent();
        }
        public ChooseAct(ICollection<EL_3m> l, Main M, int objId)
        {
            foreach (EL_3m p in l) dates.Add(p.Date);
            this.M = M;
            this.objId = objId;
            el = 3;
            InitializeComponent();
        }
        public ChooseAct(ICollection<EL_4> l, Main M, int objId)
        {
            foreach (EL_4 p in l) dates.Add(p.Date);
            this.M = M;
            this.objId = objId;
            el = 4;
            InitializeComponent();
        }

        private void ChooseAct_Load(object sender, EventArgs e)
        {
            foreach (DateTime d in dates) Dates.Items.Add(d.ToString());
        }

        private DateTime Round(DateTime d)
        {
            string med = d.ToString();
            return System.Convert.ToDateTime(med);
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            var db = new NPLabDbContext();
            if (Dates.SelectedIndex == -1) { MessageBox.Show("Не сте избрали актуализация"); return; }
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).First();
            DateTime chosenDate = System.Convert.ToDateTime(Dates.SelectedItem.ToString());
            M.dyn = true;
            switch (el)
            {
                case (0):
                    EL_1 act1 = (from p in currObj.El_1
                                      where p.Date.ToString() == Dates.SelectedItem.ToString()
                                      select p).FirstOrDefault();
                    M.LoadIsol(act1, true);
                    M.SwitchVisIsol();
                    break;
                case (1):
                   El_2 act2 = (from p in currObj.El_2
                                      where p.ControlDate.ToString() == Dates.SelectedItem.ToString()
                                      select p).FirstOrDefault();
                    M.LoadImp(act2, true);
                    M.SwitchVisImp();
                    break;
                case (2):
                    EL_3 act3 = (from p in currObj.El_3
                                      where p.Date.ToString() == Dates.SelectedItem.ToString()
                                      select p).FirstOrDefault();
                    M.LoadGr(act3, true);
                    M.SwitchVisGr();
                    break;
                case (3):
                    EL_3m act3m = (from p in currObj.El_3m
                                      where p.Date.ToString() == Dates.SelectedItem.ToString()
                                      select p).FirstOrDefault();
                    M.LoadLiPro(act3m, true);
                    M.SwitchVisLiPro();
                    break;
                case (4):
                   EL_4 act4 = (from p in currObj.El_4
                                      where p.Date.ToString() == Dates.SelectedItem.ToString()
                                      select p).FirstOrDefault();
                    M.LoadDef(act4, true);
                    M.SwitchVisDef();
                    break;
            }
            M.dyn = false;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            switch (el)
            {
                case (0): M.BackIsol_Click(this, new EventArgs()); M.SwitchVisIsol(); break;
                case (1): M.BackImp_Click(this, new EventArgs()); M.SwitchVisImp(); break;
                case (2): M.BackGr_Click(this, new EventArgs()); M.SwitchVisGr(); break;
                case (3): M.BackLiPro_Click(this, new EventArgs()); M.SwitchVisLiPro(); break;
                case (4): M.BackDef_Click(this, new EventArgs()); M.SwitchVisDef(); break;
            }
            this.Close();
        }

        
    }
}
