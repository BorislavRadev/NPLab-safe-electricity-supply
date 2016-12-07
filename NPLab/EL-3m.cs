using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPLab.Data;
using NPLab.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.ObjectModel;
using NPLab.Data.Migrations;

namespace NPLab
{
    public partial class Main : Form
    {
        public EL_3m tempLiPro = new EL_3m();
        public DateTime currDateLiPro = DateTime.Now;
        bool isNewLiPro = true;

        private void LightningGroundingName_TextChanged(object sender, EventArgs e)
        {
            TextBox temp = sender as TextBox;
            int ind = LiName.Controls.IndexOf(temp);
            tempLiPro.ListOfGroundings_El_3m.ElementAt(ind).Name = temp.Text;
        }

        private void AuxLi_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown temp = sender as NumericUpDown;
            int ind = AuxLi.Controls.IndexOf(temp);
            tempLiPro.ListOfGroundings_El_3m.ElementAt(ind).AuxiliaryGrounding = System.Convert.ToDouble(temp.Value);
        }

        private void ProbeLi_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown temp = sender as NumericUpDown;
            int ind = ProbeLi.Controls.IndexOf(temp);
            tempLiPro.ListOfGroundings_El_3m.ElementAt(ind).Probe = System.Convert.ToDouble(temp.Value);
        }

        private void MeasLi_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown temp = sender as NumericUpDown;
            int ind = LiMeas.Controls.IndexOf(temp);
            tempLiPro.ListOfGroundings_El_3m.ElementAt(ind).Measured = System.Convert.ToDouble(temp.Value);
            if (ind < AdjLi.Controls.Count) (AdjLi.Controls[ind] as NumericUpDown).Value = temp.Value * FiLi.Value;

        }

        private void AdjLi_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown temp = sender as NumericUpDown;
            int ind = AdjLi.Controls.IndexOf(temp);
            tempLiPro.ListOfGroundings_El_3m.ElementAt(ind).Adjusted = System.Convert.ToDouble(temp.Value);
            (ImpLi.Controls[ind] as NumericUpDown).Value = temp.Value * gama.Value;
        }

        private void NewLi_Click(object sender, EventArgs e)
        {
            NumericUpDown newAdjLi = new NumericUpDown(), newMeas = new NumericUpDown(), newProbeLi = new NumericUpDown(), newAux = new NumericUpDown(), newImp = new NumericUpDown();
            TextBox newName = new TextBox();
            tempLiPro.ListOfGroundings_El_3m.Add(new LightningGrounding());
            NumericUpDown lastAdjLi = AdjLi.Controls[AdjLi.Controls.Count - 1] as NumericUpDown;
            NumericUpDown lastAux = AuxLi.Controls[AuxLi.Controls.Count - 1] as NumericUpDown;
            NumericUpDown lastProbeLi = ProbeLi.Controls[ProbeLi.Controls.Count - 1] as NumericUpDown;
            NumericUpDown lastMeas = LiMeas.Controls[LiMeas.Controls.Count - 1] as NumericUpDown;
            NumericUpDown lastImp = ImpLi.Controls[LiMeas.Controls.Count - 1] as NumericUpDown;
            TextBox lastName = LiName.Controls[LiName.Controls.Count - 1] as TextBox;
            newAdjLi = new NumericUpDown
            {
                Value = lastAdjLi.Value,
                Size = lastAdjLi.Size,
                Top = lastAdjLi.Top,
                Left = lastAdjLi.Left,
                Increment = lastAdjLi.Increment,
                DecimalPlaces = lastAdjLi.DecimalPlaces,
                Visible = true,
                Name = "AdjLi" + LiName.Controls.Count
            };
            newAux = new NumericUpDown
            {
                Minimum = lastAux.Minimum,
                Maximum = lastAux.Maximum,
                Value = lastAux.Value,
                Size = lastAux.Size,
                Top = lastAux.Top,
                Left = lastAux.Left,
                Increment = lastAux.Increment,
                DecimalPlaces = lastAux.DecimalPlaces,
                Visible = true,
                Name = "Aux" + LiName.Controls.Count
            };
            newProbeLi = new NumericUpDown
            {
                Minimum = lastProbeLi.Minimum,
                Maximum = lastProbeLi.Maximum,
                Value = lastProbeLi.Value,
                Size = lastProbeLi.Size,
                Top = lastProbeLi.Top,
                Left = lastProbeLi.Left,
                Increment = lastProbeLi.Increment,
                DecimalPlaces = lastProbeLi.DecimalPlaces,
                Visible = true,
                Name = "ProbeLi" + LiName.Controls.Count
            };
            newMeas = new NumericUpDown
            {
                Value = lastMeas.Value,
                Size = lastMeas.Size,
                Top = lastMeas.Top,
                Left = lastMeas.Left,
                Increment = lastMeas.Increment,
                DecimalPlaces = lastMeas.DecimalPlaces,
                Visible = true,
                Name = "Meas" + LiName.Controls.Count
            }; 
            newImp = new NumericUpDown
            {
                Value = lastImp.Value,
                Size = lastImp.Size,
                Top = lastImp.Top,
                Left = lastImp.Left,
                Increment = lastImp.Increment,
                DecimalPlaces = lastImp.DecimalPlaces,
                Visible = true,
                Name = "Imp" + LiName.Controls.Count
            };
            newName = new TextBox
            {
                Size = lastName.Size,
                Top = lastName.Top,
                Left = lastName.Left,
                Text = lastName.Text,
                Visible = true,
                Name = "Name" + LiName.Controls.Count
            };
            AdjLi.Controls.Add(newAdjLi);
            LiMeas.Controls.Add(newMeas);
            ProbeLi.Controls.Add(newProbeLi);
            AuxLi.Controls.Add(newAux);
            LiName.Controls.Add(newName);
            ImpLi.Controls.Add(newImp);
            newAdjLi.Top += 40;
            newMeas.Top += 40;
            newProbeLi.Top += 40;
            newAux.Top += 40;
            newName.Top += 40;
            newImp.Top += 40;
            newAdjLi.ValueChanged += new EventHandler(AdjLi_ValueChanged);
            newMeas.ValueChanged += new EventHandler(MeasLi_ValueChanged);
            newProbeLi.ValueChanged += new EventHandler(ProbeLi_ValueChanged);
            newAux.ValueChanged += new EventHandler(AuxLi_ValueChanged);
            newName.TextChanged += new EventHandler(LightningGroundingName_TextChanged);
            newImp.ValueChanged += new EventHandler(ImpLi_ValueChanged);
            (AdjLi.Controls[AdjLi.Controls.Count - 1] as NumericUpDown).Value = newMeas.Value * FiLi.Value;
            Button lastD = DelButtonsLi.Controls[DelButtonsLi.Controls.Count - 1] as Button;
            Button DelLi = new Button
            {
                Size = lastD.Size,
                Top = lastD.Top + 40,
                Left = lastD.Left,
                Text = lastD.Text,
                Visible = true,
                Name = "Name" + LiName.Controls.Count
            };
            DelLi.Click += new EventHandler(Del_Click);
            DelButtonsLi.Controls.Add(DelLi);
        }

        private void DelLi_Click(object sender, EventArgs e)
        {
            Button temp = sender as Button;
            int ind = DelButtonsLi.Controls.IndexOf(temp);
            AuxLi.Controls.RemoveAt(ind);
            ProbeLi.Controls.RemoveAt(ind);
            LiMeas.Controls.RemoveAt(ind);
            AdjLi.Controls.RemoveAt(ind);
            LiName.Controls.RemoveAt(ind);
            List<LightningGrounding> listCabelsTemp = new List<LightningGrounding>();
            foreach (LightningGrounding gr in tempLiPro.ListOfGroundings_El_3m) listCabelsTemp.Add(gr);
            listCabelsTemp.RemoveAt(ind);
            tempLiPro.ListOfGroundings_El_3m = new Collection<LightningGrounding>();
            foreach (LightningGrounding gr in listCabelsTemp) tempLiPro.ListOfGroundings_El_3m.Add(gr);
            for (int i = ind; i < tempLiPro.ListOfGroundings_El_3m.Count; i++)
            {
                LiName.Controls[i].Top -= 40;
                AdjLi.Controls[i].Top -= 40;
                AuxLi.Controls[i].Top -= 40;
                ProbeLi.Controls[i].Top -= 40;
                LiMeas.Controls[i].Top -= 40;
                ImpLi.Controls[i].Top -= 40;
                DelButtonsLi.Controls[i + 1].Top -= 40;
            }
            DelButtonsLi.Controls.RemoveAt(ind);
        }
        private void dryLi_CheckedChanged(object sender, EventArgs e)
        {
            tempLiPro.WetSeason = wetLi.Checked;
            if (tempLiPro.WetSeason) FiLi.Value = 1.3M;
            else FiLi.Value = 1.15M;
        }

        private void FiLi_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control con in AdjLi.Controls) AdjLi_ValueChanged(con, new EventArgs());
            for (int ind = 0; ind < ImpLi.Controls.Count; ind++ ) (ImpLi.Controls[ind] as NumericUpDown).Value = (AdjLi.Controls[ind] as NumericUpDown).Value * gama.Value;
        }


        private void LiDate_ValueChanged(object sender, EventArgs e)
        {
            tempLiPro.Date = LiProDate.Value;
            if (tempLiPro.Date.Month < 3 || tempLiPro.Date.Month > 9) dryLi.Checked = true;
            else wetLi.Checked = true;
        }

        private void NewActLiPro_Click(object sender, EventArgs e)
        {
            foreach (Control con in LightningProtection.Controls)
            {
                con.Enabled = true;
            }
            tempLiPro.Date = LiProDate.Value;
            currDateLiPro = tempLiPro.Date;
            //currObj.El_3m.Add(tempLiPro);
            SwitchVisLiPro();
            isNewLiPro = true;
        }

        private void EditActLiPro_Click(object sender, EventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            //var db = new NPLabDbContext();
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            foreach (Control con in LightningProtection.Controls)
            {
                con.Enabled = true;
            }
            ChooseAct ch = new ChooseAct(currObj.El_3m, this, objId);
            ch.Show();
            isNewLiPro = false;
        }

        private void SaveLiPro_Click(object sender, EventArgs e)
        {
            if (!isNewLiPro) SaveLoad.UpdateEl_3m(tempLiPro, LiProDate.Value, objId, db);
            else
            {
                if (currObj.El_3m.Any<EL_3m>(p => p.Date == LiProDate.Value))
                {
                    MessageBox.Show("Вече съществува актуализация със същата дата!");
                    return;
                }
                SaveLoad.SaveEl_3m(tempLiPro, objId, db);
                //currObj.El_1.Add(tempIsol);
            }
            foreach (Control con in LightningProtection.Controls)
            {
                if (con == NewActLiPro || con == EditActLiPro || con == SaveLiPro || con == BackLiPro || con == EngineerNameLiPro || con == LiProDate)
                    con.Enabled = true;
                else con.Enabled = false;
            }
            //db.SaveChanges();
            SwitchVisLiPro();

            EL_3m t = tempLiPro;
            tempLiPro = new EL_3m();
            foreach (LightningGrounding cab in t.ListOfGroundings_El_3m)
            {
                LightningGrounding tempC = new LightningGrounding();
                tempC.Adjusted = cab.Adjusted;
                tempC.AuxiliaryGrounding = cab.AuxiliaryGrounding;
                tempC.Measured = cab.Measured;
                tempC.Name = cab.Name;
                tempC.Probe = cab.Probe;
                tempLiPro.ListOfGroundings_El_3m.Add(tempC);
            }
            tempLiPro.Date = t.Date;
            tempLiPro.Max = t.Max;
            tempLiPro.NameOfEngineer = t.NameOfEngineer;
            tempLiPro.WetSeason = t.WetSeason;
        }
        public void SwitchVisLiPro()
        {
            NewActLiPro.Visible = !NewActLiPro.Visible;
            EditActLiPro.Visible = !EditActLiPro.Visible;
            SaveLiPro.Visible = !SaveLiPro.Visible;
            BackLiPro.Visible = !BackLiPro.Visible;
        }

        public void BackLiPro_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Сигурни ли сте, че искате да изтриете промените и да се върнете?", "Изтриване на промени", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes) SwitchVisLiPro();
            currObj.El_3m.Remove(tempLiPro);
        }

        private void ImpLi_ValueChanged(object sender, EventArgs e)
        {
            DialogResult dr;
            if (sender is ChooseAct) dr = System.Windows.Forms.DialogResult.Yes;
            else dr = MessageBox.Show("Сигурни ли сте, че искате да изтриете промените и да се върнете?", "Изтриване на промени", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No) return;
            foreach (Control con in LightningProtection.Controls)
            {
                if (con == NewActLiPro || con == EditActLiPro || con == SaveLiPro || con == BackLiPro || con == EngineerNameLiPro || con == LiProDate)
                    con.Enabled = true;
                else con.Enabled = false;
            }
            SwitchVisLiPro();
        }
    }
}