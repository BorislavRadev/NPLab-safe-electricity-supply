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
using System.Reflection;

namespace NPLab
{
    public partial class Main : Form
    {
         public EL_3 tempGr = new EL_3();
         public DateTime currDateGr = DateTime.Now;
         bool isNewGr = true;
        
         private void GroundingName_TextChanged(object sender, EventArgs e)
         {
             TextBox temp = sender as TextBox;
             int ind = GrName.Controls.IndexOf(temp);
             tempGr.ListOfGroundings.ElementAt(ind).Name = temp.Text;
         }

         private void AuxGr_ValueChanged(object sender, EventArgs e)
         {
             NumericUpDown temp = sender as NumericUpDown;
             int ind = AuxGr.Controls.IndexOf(temp);
             tempGr.ListOfGroundings.ElementAt(ind).AuxiliaryGrounding = System.Convert.ToDouble(temp.Value);
         }

         private void Probe_ValueChanged(object sender, EventArgs e)
         {
             NumericUpDown temp = sender as NumericUpDown;
             int ind = Probe.Controls.IndexOf(temp);
             tempGr.ListOfGroundings.ElementAt(ind).Probe = System.Convert.ToDouble(temp.Value);
         }

         private void MeasGr_ValueChanged(object sender, EventArgs e)
         {
             NumericUpDown temp = sender as NumericUpDown;
             int ind = GrMeas.Controls.IndexOf(temp);
             tempGr.ListOfGroundings.ElementAt(ind).Measured = System.Convert.ToDouble(temp.Value);
             if(ind < Adj.Controls.Count)(Adj.Controls[ind] as NumericUpDown).Value = temp.Value * Fi.Value;

         }

         private void Adj_ValueChanged(object sender, EventArgs e)
         {
             NumericUpDown temp = sender as NumericUpDown;
             int ind = Adj.Controls.IndexOf(temp);
             tempGr.ListOfGroundings.ElementAt(ind).Adjusted = System.Convert.ToDouble(temp.Value);
         }

         private void NewGr_Click(object sender, EventArgs e)
         {
             NumericUpDown newAdj = new NumericUpDown(), newMeas = new NumericUpDown(), newProbe = new NumericUpDown(), newAux = new NumericUpDown();
             TextBox newName = new TextBox();
             tempGr.ListOfGroundings.Add(new Grounding());
             NumericUpDown lastAdj = Adj.Controls[Adj.Controls.Count - 1] as NumericUpDown;
             NumericUpDown lastAux = AuxGr.Controls[AuxGr.Controls.Count - 1] as NumericUpDown;
             NumericUpDown lastProbe = Probe.Controls[Probe.Controls.Count - 1] as NumericUpDown;
             NumericUpDown lastMeas = GrMeas.Controls[GrMeas.Controls.Count - 1] as NumericUpDown;
             TextBox lastName = GrName.Controls[GrName.Controls.Count - 1] as TextBox;
             newAdj = new NumericUpDown
             {
                 Value = lastAdj.Value,
                 Size = lastAdj.Size,
                 Top = lastAdj.Top,
                 Left = lastAdj.Left,
                 Increment = lastAdj.Increment,
                 DecimalPlaces = lastAdj.DecimalPlaces,
                 Visible = true,
                 Name = "Adj" + GrName.Controls.Count
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
                 Name = "Aux" + GrName.Controls.Count
             };
             newProbe = new NumericUpDown
             {
                 Minimum = lastProbe.Minimum,
                 Maximum = lastProbe.Maximum,
                 Value = lastProbe.Value,
                 Size = lastProbe.Size,
                 Top = lastProbe.Top,
                 Left = lastProbe.Left,
                 Increment = lastProbe.Increment,
                 DecimalPlaces = lastProbe.DecimalPlaces,
                 Visible = true,
                 Name = "Probe" + GrName.Controls.Count
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
                 Name = "Meas" + GrName.Controls.Count
             };
             newName = new TextBox
             {
                 Size = lastName.Size,
                 Top = lastName.Top,
                 Left = lastName.Left,
                 Text = lastName.Text,
                 Visible = true,
                 Name = "Name" + GrName.Controls.Count
             };
             Adj.Controls.Add(newAdj);
             GrMeas.Controls.Add(newMeas);
             Probe.Controls.Add(newProbe);
             AuxGr.Controls.Add(newAux);
             GrName.Controls.Add(newName);
             newAdj.Top += 40;
             newMeas.Top += 40;
             newProbe.Top += 40;
             newAux.Top += 40;
             newName.Top += 40;
             newAdj.ValueChanged +=new EventHandler(Adj_ValueChanged);
             newMeas.ValueChanged += new EventHandler(MeasGr_ValueChanged);
             newProbe.ValueChanged += new EventHandler(Probe_ValueChanged);
             newAux.ValueChanged += new EventHandler(AuxGr_ValueChanged);
             newName.TextChanged += new EventHandler(GroundingName_TextChanged);
             (Adj.Controls[Adj.Controls.Count - 1] as NumericUpDown).Value = newMeas.Value * Fi.Value;
             Button lastD = DelButtons.Controls[DelButtons.Controls.Count - 1] as Button;
             Button DelGr = new Button
             {
                 Size = lastD.Size,
                 Top = lastD.Top + 40,
                 Left = lastD.Left,
                 Text = lastD.Text,
                 Visible = true,
                 Name = "Name" + GrName.Controls.Count
             };
             DelGr.Click += new EventHandler(Del_Click);
             DelButtons.Controls.Add(DelGr);
         }

         private void Del_Click(object sender, EventArgs e)
         {
             Button temp = sender as Button;
             int ind = DelButtons.Controls.IndexOf(temp);
             AuxGr.Controls.RemoveAt(ind);
             Probe.Controls.RemoveAt(ind);
             GrMeas.Controls.RemoveAt(ind);
             Adj.Controls.RemoveAt(ind);
             GrName.Controls.RemoveAt(ind);
             List<Grounding> listCabelsTemp = new List<Grounding>();
             foreach (Grounding gr in tempGr.ListOfGroundings) listCabelsTemp.Add(gr);
             listCabelsTemp.RemoveAt(ind);
             tempGr.ListOfGroundings = new Collection<Grounding>();
             foreach (Grounding gr in listCabelsTemp) tempGr.ListOfGroundings.Add(gr);
             for (int i = ind; i < tempGr.ListOfGroundings.Count; i++)
             {
                 GrName.Controls[i].Top -= 40;
                 Adj.Controls[i].Top -= 40;
                 AuxGr.Controls[i].Top -= 40;
                 Probe.Controls[i].Top -= 40;
                 GrMeas.Controls[i].Top -= 40;
                 DelButtons.Controls[i + 1].Top -= 40;
             }
             DelButtons.Controls.RemoveAt(ind);
         }
         private void dry_CheckedChanged(object sender, EventArgs e)
         {
             tempGr.WetSeason = wet.Checked;
             if (tempGr.WetSeason) Fi.Value = 1.3M;
             else Fi.Value = 1.15M;
         }

         private void Fi_ValueChanged(object sender, EventArgs e)
         {
             for(int i = 0; i < Adj.Controls.Count; i++) (Adj.Controls[i] as NumericUpDown).Value = Fi.Value * (GrMeas.Controls[i] as NumericUpDown).Value;
         }

         private void GrDate_ValueChanged(object sender, EventArgs e)
         {
             tempGr.Date = GroundingDate.Value;
             if (tempGr.Date.Month < 3 || tempGr.Date.Month > 9) dry.Checked = true;
             else wet.Checked = true;
         }

         private void NewActGr_Click(object sender, EventArgs e)
         {
             foreach (Control con in Grounding.Controls)
             {
                 con.Enabled = true;
             }
             tempGr.Date = GroundingDate.Value;
             currDateGr = tempGr.Date;
             //currObj.El_3.Add(tempGr);
             SwitchVisGr();
             isNewGr = true;
         }

         private void EditActGr_Click(object sender, EventArgs e)
         {
             Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
             //var db = new NPLabDbContext();
             currObj = (from p in db.Object
                        where p.Id == objId
                        select p).FirstOrDefault();
             foreach (Control con in Grounding.Controls)
             {
                 con.Enabled = true;
             }
             ChooseAct ch = new ChooseAct(currObj.El_3, this, objId);
             ch.Show();
             isNewGr = false;
         }

         private void SaveGr_Click(object sender, EventArgs e)
         {
             if (!isNewGr) SaveLoad.UpdateEl_3(tempGr, GroundingDate.Value, objId, db);
             else
             {
                 if (currObj.El_3.Any<EL_3>(p => p.Date == GroundingDate.Value))
                 {
                     MessageBox.Show("Вече съществува актуализация със същата дата!");
                     return;
                 }
                 SaveLoad.SaveEl_3(tempGr, objId, db);
                 //currObj.El_1.Add(tempIsol);
             }
             foreach (Control con in Grounding.Controls)
             {
                 if (con == NewActGr || con == EditActGr || con == SaveGr || con == BackGr || con == EngineerNameGr || con == GroundingDate)
                     con.Enabled = true;
                 else con.Enabled = false;
             } 
             
             //db.SaveChanges();
             SwitchVisGr();

             EL_3 t = tempGr;
             tempGr = new EL_3();
             foreach (Grounding cab in t.ListOfGroundings)
             {
                 Grounding tempC = new Grounding();
                 tempC.Adjusted = cab.Adjusted;
                 tempC.AuxiliaryGrounding = cab.AuxiliaryGrounding;
                 tempC.Measured = cab.Measured;
                 tempC.Name = cab.Name;
                 tempC.Probe = cab.Probe;
                 tempGr.ListOfGroundings.Add(tempC);
             }
             tempGr.Date = t.Date;
             tempGr.Max = t.Max;
             tempGr.NameOfEngineer = t.NameOfEngineer;
             tempGr.WetSeason = t.WetSeason;
         }
        public void SwitchVisGr()
        {
            NewActGr.Visible = !NewActGr.Visible;
            EditActGr.Visible = !EditActGr.Visible;
            SaveGr.Visible = !SaveGr.Visible;
            BackGr.Visible = !BackGr.Visible;
        }

        public void BackGr_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            if (sender is ChooseAct) dr = System.Windows.Forms.DialogResult.Yes;
            else dr = MessageBox.Show("Сигурни ли сте, че искате да изтриете промените и да се върнете?", "Изтриване на промени", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No) return;
            foreach (Control con in Grounding.Controls)
            {
                if (con == NewActGr || con == EditActGr || con == SaveGr || con == BackGr || con == EngineerNameGr || con == GroundingDate)
                    con.Enabled = true;
                else con.Enabled = false;
            }
            SwitchVisGr();
        }
    }
}