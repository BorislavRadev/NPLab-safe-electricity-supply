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
        bool CheckboxUpdated = false, isNewIsol = true;
        List<TextBox> Names = new List<TextBox>();
        List<ComboBox> TypeCabs = new List<ComboBox>();
        List<NumericUpDown> Count = new List<NumericUpDown>();
        List<NumericUpDown> Surface = new List<NumericUpDown>();
        List<NumericUpDown> MeasuredForm = new List<NumericUpDown>();
        List<Panel> Conductors = new List<Panel>();
        List<Button> Rem = new List<Button>();
        public EL_1 tempIsol;
        public DateTime currDateIsol = DateTime.Now;
        
        private void ButtonForAdd_Click(object sender, EventArgs e)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());

            //var db = new NPLabDbContext();

            tempIsol.ListOfCabels.Add(new Cabel());

            TextBox name = new TextBox();
            name.Size = textBoxName.Size;
            name.Left = textBoxName.Left;
            name.Top = Names[Names.Count - 1].Top + 40;
            name.Name = "textBox" + Names.Count.ToString();
            name.TextChanged += new EventHandler(textBoxName_TextChanged);
            name.Visible = true;
            IsolationTable.Controls.Add(name);
            Names.Add(name);
            Names[Names.Count - 1].Text = "Захр. кабел ел. табло";
            Names[Names.Count - 1].Select(name.Text.IndexOf("ел. табло"), "ел. табло".Length);


            ComboBox cabel = new ComboBox();
            cabel.Size = TypeCabel.Size;
            cabel.Left = TypeCabel.Left;
            cabel.Top = TypeCabs[TypeCabs.Count - 1].Top + 40;
            cabel.Items.AddRange(Main.Stats.TypesCabels);
            cabel.Name = "ComboBox" + TypeCabs.Count.ToString();
            cabel.Visible = true;
            cabel.SelectedIndexChanged += new EventHandler(cabel_SelectedIndexChanged);
            IsolationTable.Controls.Add(cabel);
            TypeCabs.Add(cabel);
            TypeCabs[TypeCabs.Count - 1].SelectedIndex = TypeCabs[TypeCabs.Count - 2].SelectedIndex;

            NumericUpDown count = new NumericUpDown();
            count.Size = Number.Size;
            count.Left = Number.Left;
            count.Top = Count[Count.Count - 1].Top + 40;
            count.ValueChanged += new EventHandler(Number_ValueChanged);
            count.Name = "num" + Names.Count.ToString();
            count.Visible = true;
            IsolationTable.Controls.Add(count);
            Count.Add(count);

            NumericUpDown area = new NumericUpDown();
            area.Size = CabelArea.Size;
            area.Left = CabelArea.Left;
            area.Top = Surface[Surface.Count - 1].Top + 40;
            area.Name = "area" + Surface.Count.ToString();
            area.ValueChanged += new EventHandler(CabelArea_ValueChanged);
            area.Visible = true;
            area.Increment = 0.01M;
            area.DecimalPlaces = 2;
            IsolationTable.Controls.Add(area);
            Surface.Add(area);
            area.Minimum = 0.25M;
            area.Maximum = 500;
            Surface[Surface.Count - 1].Value = System.Convert.ToDecimal(Surface[Surface.Count - 2].Value);

            int min = tempIsol.MinMeasured;
            int max = tempIsol.MaxMeasured;
            int sign = System.Convert.ToInt32(Signs.Value);
            Random r = new Random();

            NumericUpDown measured = new NumericUpDown();
            measured.Size = Measured.Size;
            measured.Left = Measured.Left;
            measured.Top = MeasuredForm[MeasuredForm.Count - 1].Top + 40;
            measured.Name = "measured" + MeasuredForm.Count.ToString();
            measured.ValueChanged += new EventHandler(Measured_ValueChanged);
            measured.Visible = true;
            measured.Increment = 10;
            measured.Minimum = 0;
            measured.Maximum = 1500;
            IsolationTable.Controls.Add(measured);
            MeasuredForm.Add(measured);
            MeasuredForm[MeasuredForm.Count - 1].Value = System.Convert.ToDecimal((r.Next(min, max) / sign) * sign);

            string[] TypeCon = new string[6] { "L1", "L2", "L3", "PE", "N", "PEN" };
            CheckBox[] Conds = new CheckBox[6];
            for (int i = 0; i < Conds.Length; i++)
            {
                Conds[i] = new CheckBox
                {
                    Name = "Check" + i.ToString(),
                    Tag = i,
                    AutoSize = true,
                    Text = TypeCon[i],
                    Checked = (i == 0),
                    Visible = true,
                };
                Conds[i].CheckedChanged += new EventHandler(Main_CheckedChanged);
            }
            Panel cond = new Panel();
            cond.Size = Cond.Size;
            cond.Left = Cond.Left;
            cond.Top = Cond.Top + 40 * Conductors.Count;
            cond.Controls.AddRange(Conds);
            cond.Name = "cond" + Conductors.Count.ToString();
            cond.Visible = true;

            Label x = new Label();
            x.AutoSize = true;
            x.Left = X.Left;
            x.Top = X.Top + 40 * Conductors.Count;
            x.Text = "x";
            cond.Name = "x" + Conductors.Count.ToString();
            cond.Visible = true;

            for (int i = 0; i < 6; i++)
            {
                cond.Controls[i].Left = Cond.Controls[i].Left;
                cond.Controls[i].Top = Cond.Controls[i].Top;
            }
            IsolationTable.Controls.Add(cond);
            Conductors.Add(cond);
            Count[Count.Count - 1].Maximum = 5;
            Count[Count.Count - 1].Minimum = 2;
            Count[Count.Count - 1].Value = System.Convert.ToDecimal(Count[Count.Count - 2].Value);

            Button Del = new Button();
            Del.Size = Delete.Size;
            Del.Left = Delete.Left;
            Del.Top = Delete.Top + 40 * Rem.Count;
            Del.Name = "Delete" + Rem.Count.ToString();
            Del.BackColor = Delete.BackColor;
            Del.Text = "Изтрий";
            Del.Click += new EventHandler(Delete_Click);
            Del.Visible = true;
            IsolationTable.Controls.Add(Del);
            Rem.Add(Del);

            Label newX = new Label();
            newX.AutoSize = true;
            newX.Left = X.Left;
            newX.Top = X.Top + 40 * Rem.Count;
            x.Text = "x";
            x.Visible = true;
            IsolationTable.Controls.Add(x);
            

        }

        public Engineers GetById(string NameOfEngineer, NPLabDbContext context)
        {
            return context.Engineers.Find(NameOfEngineer);
        }
        public ICollection<Engineers> GetAll(NPLabDbContext context)
        {
            return context.Engineers.ToList();
        }

        private void cabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox tempCabel = sender as ComboBox;
            int indexCabel = TypeCabs.IndexOf(tempCabel);
            this.tempIsol.ListOfCabels.ElementAt(indexCabel).CabelModel = tempCabel.SelectedItem.ToString();
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            TextBox tempCabel = sender as TextBox;
            int indexCabel = Names.IndexOf(tempCabel);
            this.tempIsol.ListOfCabels.ElementAt(indexCabel).CabelType = tempCabel.Text;
            this.tempIsol.ListOfCabels.ElementAt(indexCabel).Name = tempCabel.Text.ToString() + " тип " +
                tempIsol.ListOfCabels.ElementAt(indexCabel).CabelModel + " " +
                tempIsol.ListOfCabels.ElementAt(indexCabel).ConductorsCount.ToString() + "x" +
                tempIsol.ListOfCabels.ElementAt(indexCabel).Thickness.ToString() + "mm²";
        }

        private void Number_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown tempCabel = sender as NumericUpDown;
            int indexCabel = Count.IndexOf(tempCabel);
            int value = System.Convert.ToInt32(tempCabel.Value);
            tempIsol.ListOfCabels.ElementAt(indexCabel).ConductorsCount = value;
            textBoxName_TextChanged(Names[indexCabel], new EventArgs());
            bool grounded = (Conductors[indexCabel].Controls[3] as CheckBox).Checked;
            if (CheckboxUpdated)
            {
                CheckboxUpdated = false;
                return;
            }
            bool[][] checkModel = new bool[4][];
            checkModel[0] = new bool[6] { true, false, false, false, grounded, !grounded };
            checkModel[1] = new bool[6] { true, false, false, true, true, false };
            checkModel[2] = new bool[6] { true, true, true, false, grounded, !grounded };
            checkModel[3] = new bool[6] { true, true, true, true, true, false };
            for (int i = 0; i < 6; i++)
            {
                if ((Conductors[indexCabel].Controls[i] as CheckBox).Checked != checkModel[value - 2][i])
                    (Conductors[indexCabel].Controls[i] as CheckBox).Checked = checkModel[value - 2][i];
            }
        }

        private void CabelArea_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown temp = sender as NumericUpDown;
            double val = System.Convert.ToDouble(temp.Value);
            int indexCabel = Surface.IndexOf(temp);
            double sign = 0;
            if (Stats.CabelSizes.IndexOf(System.Math.Round(val - 0.01, 2)) != -1) sign = 0.01;
            if (Stats.CabelSizes.IndexOf(System.Math.Round(val + 0.01, 2)) != -1) sign = -0.01;
            if (sign == 0)
            {
                tempIsol.ListOfCabels.ElementAt(indexCabel).Thickness = val;
                textBoxName_TextChanged(Names[indexCabel], new EventArgs());
                return;
            }
            int index = Stats.CabelSizes.IndexOf(System.Math.Round(val - sign, 2));
            double newVal = 0;
            if (index < Stats.CabelSizes.Count - 1)
                newVal = Stats.CabelSizes[(int)(index + sign * 100)];
            else newVal = 0.25;
            (sender as NumericUpDown).Value = System.Convert.ToDecimal(newVal);
        }

        private void Measured_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown temp = sender as NumericUpDown;
            int sign = System.Convert.ToInt32(Signs.Value);
            int val = System.Convert.ToInt32(temp.Value);
            if (val % sign != 0) { temp.Value = (val / sign) * sign; return; }
            int indexCabel = MeasuredForm.IndexOf(temp);
            tempIsol.ListOfCabels.ElementAt(indexCabel).Measured = val;
            if (val < tempIsol.Min) tempIsol.MeetsRequirements = false;
        }

        private void Main_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox temp = sender as CheckBox;
            int val = 0;
            int indGr = 0, indCh = 0;
            bool l1 = false, l2 = false, l3 = false, N = false, Pe = false, Pen = false;
            foreach (Panel gr in Conductors)
            {
                if (gr.Controls.IndexOf(temp) != -1)
                {
                    indGr = Conductors.IndexOf(gr);
                    indCh = gr.Controls.IndexOf(temp);
                    l1 = (gr.Controls[0] as CheckBox).Checked;
                    l2 = (gr.Controls[1] as CheckBox).Checked;
                    l3 = (gr.Controls[2] as CheckBox).Checked;
                    Pe = (gr.Controls[3] as CheckBox).Checked;
                    N = (gr.Controls[4] as CheckBox).Checked;
                    Pen = (gr.Controls[5] as CheckBox).Checked;
                    switch (indCh)
                    {
                        case (5):
                            if (Pe == Pen) (gr.Controls[3] as CheckBox).Checked = false;
                            if (N == Pen) (gr.Controls[4] as CheckBox).Checked = !Pen;
                            break;
                        //#недовършено да се добави за N
                        case (4):
                            if (Pen == N) (gr.Controls[5] as CheckBox).Checked = !N;
                            break;
                        case (3):
                            if (Pe == Pen) (gr.Controls[5] as CheckBox).Checked = !Pe;
                            if (Pe == N) (gr.Controls[4] as CheckBox).Checked = !Pen;
                            break;
                        case (2):
                            if (l2 != l3) (gr.Controls[1] as CheckBox).Checked = l3; break;
                        case (1):
                            if (l2 != l3) (gr.Controls[2] as CheckBox).Checked = l2; break;
                        case (0):
                            if (!l1) (gr.Controls[0] as CheckBox).Checked = true; break;
                    }
                    l1 = (gr.Controls[0] as CheckBox).Checked;
                    l2 = (gr.Controls[1] as CheckBox).Checked;
                    l3 = (gr.Controls[2] as CheckBox).Checked;
                    Pe = (gr.Controls[3] as CheckBox).Checked;
                    N = (gr.Controls[4] as CheckBox).Checked;
                    Pen = (gr.Controls[5] as CheckBox).Checked;
                    foreach (CheckBox ch in gr.Controls)
                    {
                        if (ch.Checked) val++;
                    }
                }
            }

            if (Count[indGr].Value != System.Convert.ToDecimal(val) && val > 1 && val < 6)
            {
                CheckboxUpdated = true;
                Count[indGr].Value = System.Convert.ToDecimal(val);
            }
            this.tempIsol.ListOfCabels.ElementAt(indGr).L1 = l1;
            this.tempIsol.ListOfCabels.ElementAt(indGr).L2 = l2;
            this.tempIsol.ListOfCabels.ElementAt(indGr).L3 = l3;
            this.tempIsol.ListOfCabels.ElementAt(indGr).PE = Pe;
            this.tempIsol.ListOfCabels.ElementAt(indGr).N = N;
            this.tempIsol.ListOfCabels.ElementAt(indGr).PEN = Pen;
        }

        private void TypeCabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox temp = sender as ComboBox;
            int indexCabel = TypeCabs.IndexOf(temp);
            this.tempIsol.ListOfCabels.ElementAt(indexCabel).CabelModel = temp.SelectedItem.ToString(); ;
            this.tempIsol.ListOfCabels.ElementAt(indexCabel).Name =
                tempIsol.ListOfCabels.ElementAt(indexCabel).CabelType + " тип " +
                temp.SelectedItem.ToString() + " " +
                tempIsol.ListOfCabels.ElementAt(indexCabel).ConductorsCount.ToString() + "x" +
                tempIsol.ListOfCabels.ElementAt(indexCabel).Thickness.ToString() + "mm²";
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            Button temp = sender as Button;

            int indexCabel = Rem.IndexOf(temp); DialogResult dialogResult;
            if (dyn) dialogResult = DialogResult.Yes;
            else dialogResult = MessageBox.Show(
                 "Сигурни ли сте че искате да изтриете " + Names[indexCabel].Text + "?",
                 "Изтриване на кабел", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            IsolationTable.Controls.Remove(Names[indexCabel]);
            IsolationTable.Controls.Remove(TypeCabs[indexCabel]);
            IsolationTable.Controls.Remove(Count[indexCabel]);
            IsolationTable.Controls.Remove(Surface[indexCabel]);
            IsolationTable.Controls.Remove(MeasuredForm[indexCabel]);
            IsolationTable.Controls.Remove(Conductors[indexCabel]);
            Names.RemoveAt(indexCabel);
            TypeCabs.RemoveAt(indexCabel);
            Count.RemoveAt(indexCabel);
            Surface.RemoveAt(indexCabel);
            MeasuredForm.RemoveAt(indexCabel);
            Conductors.RemoveAt(indexCabel);
            List<Cabel> listCabelsTemp = new List<Cabel>();
            foreach (Cabel cab in tempIsol.ListOfCabels) listCabelsTemp.Add(cab);
            listCabelsTemp.RemoveAt(indexCabel);
            tempIsol.ListOfCabels = new Collection<Cabel>();
            foreach (Cabel cab in listCabelsTemp) tempIsol.ListOfCabels.Add(cab);
            for (int i = indexCabel; i < tempIsol.ListOfCabels.Count; i++)
            {
                Names[i].Top -= 40;
                TypeCabs[i].Top -= 40;
                Count[i].Top -= 40;
                Surface[i].Top -= 40;
                MeasuredForm[i].Top -= 40;
                Conductors[i].Top -= 40;
                Rem[i + 1].Top -= 40;
            }
            IsolationTable.Controls.Remove(temp);
            Rem.Remove(temp);
            
        }

        private void Meas_ValueChanged(object sender, EventArgs e)
        {
            int valMin = System.Convert.ToInt32(MinMeas.Value);
            int valMax = System.Convert.ToInt32(MaxMeas.Value);
            tempIsol.MaxMeasured = valMax;
            tempIsol.MinMeasured = valMin;
            Random r = new Random();
            foreach (NumericUpDown meas in MeasuredForm)
            {
                if (System.Convert.ToInt32(meas.Value) >= valMin && System.Convert.ToInt32(meas.Value) <= valMax) continue;
                int newVal = r.Next(valMin, valMax);
                newVal /= 10;
                newVal *= 10;
                meas.Value = System.Convert.ToDecimal(newVal);
            }
        }

        private void Norm_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown temp = sender as NumericUpDown;
            double val = System.Convert.ToDouble(temp.Value);
            tempIsol.Min = val;
        }

        private void Naprejenie_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown temp = sender as NumericUpDown;
            int val = System.Convert.ToInt32(temp.Value);
            tempIsol.SourceVoltage = val;
        }

        private void EngineerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            tempIsol.NameOfEngineer = EngineerNameIsol.SelectedItem.ToString();
        }

        private void IsolationDate_ValueChanged(object sender, EventArgs e)
        {
            tempIsol.Date = IsolationDate.Value;
            currDateIsol = IsolationDate.Value;
        }

        private void ConvertIsol_Click(object sender, EventArgs e)
        {
            SaveFileDialog FD = new SaveFileDialog();
            FD.CheckPathExists = true;
            FD.CreatePrompt = true;
            FD.OverwritePrompt = true;
            FD.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            FD.DefaultExt = "xls";
            FD.Filter = "Microsoft Office Excel Tables (*.xls)|*.xls|All files (*.*)|*.*";
            string URL = "";
            DialogResult result = FD.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK) URL = FD.FileName;
            ConvertFile.ConvertFileExcel ConvInst = new ConvertFile.ConvertFileExcel();
            if (URL != "") ConvInst.ConvertIsolExcel(URL, tempIsol, DiffCond.Checked);
        }

        private void Signs_ValueChanged(object sender, EventArgs e)
        {
            int val = System.Convert.ToInt32(Signs.Value);
            foreach (NumericUpDown num in MeasuredForm)
            {
                int valH = (System.Convert.ToInt32(num.Value) / val) * val; 
                num.Increment = val;
                num.Value = System.Convert.ToDecimal(valH);
                num.Minimum = System.Convert.ToDecimal(MinMeas.Value);
                num.Maximum = System.Convert.ToDecimal(MaxMeas.Value);
            }
        }

        private void NewActIsol_Click(object sender, EventArgs e)
        {
            foreach (Control con in Isolation.Controls)
            {
                con.Enabled = true;
            }
            tempIsol.Date = IsolationDate.Value;
            currDateIsol = tempIsol.Date;
            //currObj.El_1.Add(tempIsol);
            SwitchVisIsol();
            isNewIsol = true;
        }

        private void EditActIsol_Click(object sender, EventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            //var db = new NPLabDbContext();
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            foreach (Control con in Isolation.Controls)
            {
                con.Enabled = true;
            }
            ChooseAct ch = new ChooseAct(currObj.El_1, this, objId);
            ch.Show();
            isNewIsol = false;
        }

        private void SaveIsol_Click(object sender, EventArgs e)
        {
            
            if (!isNewIsol) SaveLoad.UpdateEl_1(tempIsol, IsolationDate.Value, objId, db);
            else
            {
                if (currObj.El_1.Any<EL_1>(p => p.Date == IsolationDate.Value))
                {
                    MessageBox.Show("Вече съществува актуализация със същата дата!");
                    return;
                }
                SaveLoad.SaveEl_1(tempIsol, objId, db);
                //currObj.El_1.Add(tempIsol);
            }
            
            
            //db.SaveChanges();
            foreach (Control con in Isolation.Controls)
            {
                if (con == NewActIsol || con == EditActIsol || con == SaveIsol || con == BackIsol || con == EngineerNameIsol || con == IsolationDate)
                    con.Enabled = true;
                else con.Enabled = false;
            }
            SwitchVisIsol();
            EL_1 t = tempIsol;
            tempIsol = new EL_1();
            foreach (Cabel cab in t.ListOfCabels)
            {
                Cabel tempC = new Cabel();
                tempC.CabelModel = cab.CabelModel;
                tempC.CabelType = cab.CabelType;
                tempC.ConductorsCount = cab.ConductorsCount;
                tempC.L1 = cab.L1;
                tempC.L2 = cab.L2;
                tempC.L3 = cab.L3;
                tempC.Measured = cab.Measured;
                tempC.N = cab.N;
                tempC.Name = cab.Name;
                tempC.PE = cab.PE;
                tempC.PEN = cab.PEN;
                tempC.Thickness = cab.Thickness;
                tempIsol.ListOfCabels.Add(tempC);
            }
            tempIsol.MaxMeasured = t.MaxMeasured;
            tempIsol.Min = t.Min;
            tempIsol.NameOfEngineer = t.NameOfEngineer;
            tempIsol.SourceVoltage = t.SourceVoltage;
            tempIsol.MinMeasured = t.MaxMeasured;
            tempIsol.Date = t.Date;
        }

        public void SwitchVisIsol()
        {
            NewActIsol.Visible = !NewActIsol.Visible;
            EditActIsol.Visible = !EditActIsol.Visible;
            SaveIsol.Visible = !SaveIsol.Visible;
            BackIsol.Visible = !BackIsol.Visible;
        }

        public void BackIsol_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            if (sender is ChooseAct) dr = System.Windows.Forms.DialogResult.Yes;
            else dr = MessageBox.Show("Сигурни ли сте, че искате да изтриете промените и да се върнете?", "Изтриване на промени", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No) return;
            foreach (Control con in Isolation.Controls)
            {
                if (con == NewActIsol || con == EditActIsol || con == SaveIsol || con == BackIsol || con == EngineerNameIsol || con == IsolationDate)
                    con.Enabled = true;
                else con.Enabled = false;
            }
            SwitchVisIsol();
            //currObj.El_1.Remove(tempIsol);
        }
    }
}