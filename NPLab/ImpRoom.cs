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
    public partial class ImpRoom : Form
    {
        Rooms currRoom = new Rooms();
        El_2 EL2Ref;
        Main M;
        BindingSource ImpBindingSource = new BindingSource();
        Panel[] counts = new Panel[3];
        Panel[] amps = new Panel[3];
        ListBox[] names = new ListBox[3];
        int sector, floor, room;
        bool isNew, showMsg = false;
        public ImpRoom(Main M, Rooms currRoom, El_2 El2Ref, int sector, int floor, int room, bool isNew)
        {
            this.currRoom = currRoom;
            //this.currRoom.ListOfInstallations.Clear();
            this.EL2Ref = El2Ref;
            this.M = M;
            this.sector = sector;
            this.floor = floor;
            this.room = room;
            this.isNew = isNew;
            InitializeComponent();
        }

        private void ImpRoom_Load(object sender, EventArgs e)
        {
            //EditContacts.DataSource = currRoom.ListOfInstallations;
            //Main.Stats = new StaticClass();
            foreach (InstallationItem item in Main.Stats.Items)
            {
                if ((int)item.Type == 0) { ContList.Items.Add(item.Name); ContList.Items.Add(""); }
                if ((int)item.Type == 1 && item.Id < 4000) { LightList.Items.Add(item.Name); LightList.Items.Add(""); }
                if ((int)item.Type == 1 && item.Id >= 4000) { LumList.Items.Add(item.Name); LumList.Items.Add(""); }
                if ((int)item.Type == 2) PowAmpCollection.Items.Add(item.Name);
            }
            CurrProg.AutoGenerateColumns = false;
            ImpBindingSource = new BindingSource();
            foreach(Installations inst in currRoom.ListOfInstallations) ImpBindingSource.Add(inst);
            CurrProg.DataSource = ImpBindingSource;
            CurrProg.Columns[0].DataPropertyName = "InstallationName";
            CurrProg.Columns[1].DataPropertyName = "Amperage";
            CurrProg.Columns[2].DataPropertyName = "Coefficient";
            CurrProg.Columns[3].DataPropertyName = "Impedance";
            CurrProg.Columns[4].DataPropertyName = "Max";
            foreach (Control con in this.Controls)
            {
                if (con is Panel) { (con as Panel).AutoScroll = false; (con as Panel).AutoSize = true; }
                //if (con is GroupBox) (con as GroupBox).Sc = true;
            }
            Rooms t = currRoom;
            M.SuccRoom = false;
            currRoom = M.tempImp.ListOfSectors.ElementAt(sector).ListOfFloors.ElementAt(floor).ListOfRooms.ElementAt(room);
            LoadRoom(currRoom);
            showMsg = true;
            //currRoom = t;
            CurrProg.Update();
            CurrProg.Refresh();
        }
        private void LoadRoom(Rooms r)
        {
            CurrProg.DataSource = new BindingSource();
            RoomName.Text = r.RoomName;
            counts = new Panel[3]{ ContCount, LightCount, PowAmp };
            amps = new Panel[3]{ ContAmp, LightAmp, PowAmperage };
            names = new ListBox[3] { ContList, LightList, PowAmpList };
            foreach(Installations inst in r.ListOfInstallations)
            {
                InstallationItem it = inst.Item; 
                int type = (int)inst.Item.Type;
                int sec = Main.Stats.Items.FindAll(x => (int)x.Type == type).IndexOf(it);
                if (type == 2)
                {
                    PowAmpCollection.SetItemChecked(sec, true);
                    sec = PowAmpList.Items.IndexOf(it.Name);
                }
                (counts[type].Controls[sec] as NumericUpDown).Value++;
                if((counts[type].Controls[sec] as NumericUpDown).Value == 1)
                    (amps[type].Controls[sec] as NumericUpDown).Value = System.Convert.ToDecimal(inst.Amperage);
            }
            ImpBindingSource = new BindingSource();
            foreach (Installations inst in r.ListOfInstallations)
            {
                ImpBindingSource.Add(inst);
                CurrProg.Columns[0].DataPropertyName = "InstallationName";
                CurrProg.Columns[1].DataPropertyName = "Amperage";
                CurrProg.Columns[2].DataPropertyName = "Coefficient";
                CurrProg.Columns[3].DataPropertyName = "Impedance";
                CurrProg.Columns[4].DataPropertyName = "Max";
                CurrProg.DataSource = ImpBindingSource;
            }
            CurrProg.Update();
            CurrProg.Refresh();
        }

        private void Count_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown temp = sender as NumericUpDown;
            int value = System.Convert.ToInt32(temp.Value);
            Panel container = counts.First<Panel>(p => p.Controls.IndexOf(temp) > -1);
            int prim = counts.ToList<Panel>().IndexOf(container);
            int sec = container.Controls.IndexOf(temp);
            string name = names[prim].Items[sec * 2].ToString();
            InstallationItem item = Main.Stats.Items.First<InstallationItem>(i => i.Name == name);
            int type = Main.Stats.Items.IndexOf(item);
            //if (Main.Stats.Items[type].Id >= 4000) prim = 3;
            //Panel[] 
            Installations firstInst = currRoom.ListOfInstallations.FirstOrDefault(i => i.Item == item);
            int first = currRoom.FirstIndex(type);
            if(firstInst == null) first = -1;
            int count = currRoom.ListOfInstallations.Count(i => i.Item == item);
            int last = first + count - 1;
            List<Installations> ofType = (from p in currRoom.ListOfInstallations
                                              where p.Item == item
                                              select p).ToList<Installations>();
            Random r = new Random();
            if (value < count)
            {
                DialogResult dialogResult;
                if (showMsg) dialogResult = MessageBox.Show("По този начин ще изтриете вече въведени съоръжения. Сигурни ли сте, че искате да продължите?", "Изтриване на съоръжения", MessageBoxButtons.YesNo);
                else dialogResult = System.Windows.Forms.DialogResult.Yes;
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = value; i < count; i++)
                    {
                        Installations instToDel = ofType.ElementAt(value);
                        currRoom.ListOfInstallations.Remove(instToDel);
                        //ImpBindingSource.RemoveAt(first + value);
                        CurrProg.DataSource = ImpBindingSource;
                    }
                    ImpBindingSource.Clear();
                    foreach (Installations inst in currRoom.ListOfInstallations) ImpBindingSource.Add(inst);
                    CurrProg.DataSource = ImpBindingSource;
                    CurrProg.Columns[0].DataPropertyName = "InstallationName";
                    CurrProg.Columns[1].DataPropertyName = "Amperage";
                    CurrProg.Columns[2].DataPropertyName = "Coefficient";
                    CurrProg.Columns[3].DataPropertyName = "Impedance";
                    CurrProg.Columns[4].DataPropertyName = "Max";
                    CurrProg.Update();
                    CurrProg.Refresh();
                }
            }
            else
            {
                Installations tempInst = new Installations();
                Control ContainerCount = new Control(), ContainerType = new Control(), ContainerAmp = new Control(); 
                if (prim == 0) { ContainerCount = ContCount; ContainerType = TypePro; ContainerAmp = ContAmp; }
                if (prim == 1) { ContainerCount = LightCount; ContainerType = LightTypePro; ContainerAmp = LightAmp; }
                if (prim == 2) { ContainerCount = PowAmp; ContainerType = PowIsAuto; ContainerAmp = PowAmperage; }
                if (prim == 3) { ContainerCount = Rows; ContainerType = LumIsAuto; ContainerAmp = LumAmp; }
                int typeThis = ContainerCount.Controls.IndexOf(temp);
                if (count == 0)
                {
                    for (int i = 0; i < value; i++)
                    {
                        tempInst = new Installations();
                        tempInst.Item = Main.Stats.Items[type];
                        tempInst.isAutomaticProtector = ((ContainerType.Controls[typeThis] as GroupBox).Controls[1] as RadioButton).Checked;
                        tempInst.Impedance = r.NextDouble() * (EL2Ref.MaxMeasured - EL2Ref.MinMeasured) + EL2Ref.MinMeasured;
                        tempInst.Amperage = System.Convert.ToDouble((ContainerAmp.Controls[typeThis] as NumericUpDown).Value);
                        tempInst.Coefficient = EL2Ref.Coefficent;
                        tempInst.Ofazen = false;
                        tempInst.Reset = true;
                        tempInst.Max = Main.Stats.lambda / (tempInst.Amperage * tempInst.Coefficient);
                        tempInst.FollowsRequirements = (tempInst.Impedance < tempInst.Max) && tempInst.Reset && !tempInst.Ofazen;
                        tempInst.NumberOfInstallation = i + 1;
                        tempInst.NumberOfProtector = -1;
                        //tempInst.Id = currRoom.ListOfInstallations.Count;
                        if (value > 1) tempInst.InstallationName = Main.Stats.Items[type].Name + " №" + (tempInst.NumberOfInstallation).ToString();
                        else tempInst.InstallationName = Main.Stats.Items[type].Name;
                        AddInst(tempInst);
                        ImpBindingSource.Add(tempInst);
                        CurrProg.DataSource = ImpBindingSource;
                        CurrProg.Columns[0].DataPropertyName = "InstallationName";
                        CurrProg.Columns[1].DataPropertyName = "Amperage";
                        CurrProg.Columns[2].DataPropertyName = "Coefficient";
                        CurrProg.Columns[3].DataPropertyName = "Impedance";
                        CurrProg.Columns[4].DataPropertyName = "Max";
                    }
                }
                else
                {
                    for (int i = last; i < last + value - count; i++)
                    {
                        tempInst = new Installations();
                        tempInst.Item = Main.Stats.Items[type];
                        tempInst.isAutomaticProtector = ((ContainerType.Controls[typeThis] as GroupBox).Controls[1] as RadioButton).Checked;
                        tempInst.Impedance = r.NextDouble() * (EL2Ref.MaxMeasured - EL2Ref.MinMeasured) + EL2Ref.MinMeasured;
                        tempInst.Amperage = System.Convert.ToDouble((ContainerAmp.Controls[typeThis] as NumericUpDown).Value);
                        tempInst.Coefficient = EL2Ref.Coefficent;
                        tempInst.Ofazen = false;
                        tempInst.Reset = true;
                        tempInst.Max = Main.Stats.lambda / (tempInst.Amperage * tempInst.Coefficient);
                        tempInst.FollowsRequirements = (tempInst.Impedance < tempInst.Max) && tempInst.Reset && !tempInst.Ofazen;
                        tempInst.NumberOfInstallation = i + 1 - first;
                        tempInst.NumberOfProtector = -1;
                        //tempInst.Id = currRoom.ListOfInstallations.Count;
                        if (value > 1) tempInst.InstallationName = Main.Stats.Items[type].Name + " №" + (tempInst.NumberOfInstallation).ToString();
                        else tempInst.InstallationName = Main.Stats.Items[type].Name;
                        currRoom.Insert(i + 1, tempInst);
                        ImpBindingSource.Insert(i + 1, tempInst);
                        CurrProg.DataSource = ImpBindingSource;
                        CurrProg.Columns[0].DataPropertyName = "InstallationName";
                        CurrProg.Columns[1].DataPropertyName = "Amperage";
                        CurrProg.Columns[2].DataPropertyName = "Coefficient";
                        CurrProg.Columns[3].DataPropertyName = "Impedance";
                        CurrProg.Columns[4].DataPropertyName = "Max";
                    }
                }
            }
            CurrProg.Update();
            CurrProg.Refresh();
        }

        private void Amperage_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown temp = sender as NumericUpDown;
            double value = System.Convert.ToDouble(temp.Value);
            int sign = 1;
            int ind = Main.Stats.Prots.IndexOf(System.Math.Round(value - 0.1, 1));
            if (ind == -1) { ind = Main.Stats.Prots.IndexOf(value + 0.1); sign = -1; }
            int type = ContAmp.Controls.IndexOf(temp);
            if (ContAmp.Controls.IndexOf(temp) == -1) type = ContAmp.Controls.Count;
            if (LightAmp.Controls.IndexOf(temp) == -1 && type >= ContAmp.Controls.Count) type += LightAmp.Controls.Count;
            if (LightAmp.Controls.IndexOf(temp) != -1 && type >= ContAmp.Controls.Count) type += LightAmp.Controls.IndexOf(temp);
            if (PowAmperage.Controls.IndexOf(temp) != -1) type += PowAmpCollection.Items.IndexOf(PowAmpList.Items[PowAmperage.Controls.IndexOf(temp)]);
            if (type == -1) type += PowAmpCollection.Items.Count + LumAmp.Controls.IndexOf(temp);

            int last = currRoom.LastIndex(type);
            int first = currRoom.FirstIndex(type);
            if (first < 0) { (sender as NumericUpDown).Value = System.Convert.ToDecimal(Main.Stats.Prots[ind]); return; }
            int count = last - first + 1;
            if (Main.Stats.Prots.IndexOf(value) != -1)
            {
                for (int i = first; i <= last; i++ ) currRoom.ListOfInstallations.ElementAt(i).Amperage = value;
                return;
            }
            (sender as NumericUpDown).Value = System.Convert.ToDecimal(Main.Stats.Prots[ind + sign]);
            CurrProg.Update();
            CurrProg.Refresh();
        }

        private void isAutoCheckedChanged(object sender, EventArgs e)
        {
            RadioButton rad = sender as RadioButton;
            GroupBox temp = rad.Parent as GroupBox;
            bool isChecked = rad.Checked;
            int type = TypePro.Controls.IndexOf(temp);
            if (TypePro.Controls.IndexOf(temp) == -1) type = TypePro.Controls.Count;
            if (LightTypePro.Controls.IndexOf(temp) == -1 && type >= TypePro.Controls.Count) type += TypePro.Controls.Count;
            if (LightTypePro.Controls.IndexOf(temp) != -1 && type >= TypePro.Controls.Count) type += LightTypePro.Controls.IndexOf(temp);
            if (PowIsAuto.Controls.IndexOf(temp) != -1) type += PowAmpCollection.Items.IndexOf(PowAmpList.Items[PowIsAuto.Controls.IndexOf(temp)]);
            int last = currRoom.LastIndex(type);
            int first = currRoom.FirstIndex(type);
            int count = last - first + 1;
            if (last < 0) { return; }
            for (int i = first; i < count; i++) currRoom.ListOfInstallations.ElementAt(i).isAutomaticProtector = isChecked;
        }

        private void PowAmpCollection_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int indexChanging = e.Index;
            if (e.NewValue == CheckState.Checked)
            {
                NewPow(PowAmp.Controls.Count - 1);
                PowAmpList.Items.Add(PowAmpCollection.Items[indexChanging]);
                PowAmpList.Items.Add("");
            }
            else
            {
                int ind = PowAmpList.Items.IndexOf(PowAmpCollection.Items[indexChanging]);
                DelPow(ind / 2);
                PowAmpList.Items.RemoveAt(ind + 1);
                PowAmpList.Items.Remove(PowAmpCollection.Items[indexChanging]);
            }
        }

        private void DelPow(int ind)
        {
            int type = ind + ContAmp.Controls.Count + Light.Controls.Count;
            if (currRoom.FirstIndex(type) != -1) currRoom.RemoveType(type);
            PowAmp.Controls.RemoveAt(ind);
            PowAmperage.Controls.RemoveAt(ind);
            PowIsAuto.Controls.RemoveAt(ind);
            for (int i = ind; i < PowAmp.Controls.Count; i++)
            {
                PowAmp.Controls[i].Top -= 26;
                PowAmperage.Controls[i].Top -= 26;
                PowIsAuto.Controls[i].Top -= 26;
            }
        }

        private void NewPow(int ind)
        {
            NumericUpDown num = new NumericUpDown
            {
                Top = (ind + 1) * 26,
                Left = 2,
                Size = MonoPhase.Size,
                Minimum = 0,
                Maximum = 500,
                Increment = 1,
                Value = 0,
                DecimalPlaces = 0,
                Visible = true
            };
            num.ValueChanged += new EventHandler(Count_ValueChanged);

            PowAmp.Controls.Add(num);
            NumericUpDown amp = new NumericUpDown
            {
                Top = (ind + 1) * 26,
                Left = 2,
                Size = MonoPhase.Size,
                Minimum = 4,
                Maximum = 500,
                Increment = 0.1M,
                Value = 25,
                DecimalPlaces = 0,
                Visible = true
            };
            amp.ValueChanged += new EventHandler(Amperage_ValueChanged);
            PowAmperage.Controls.Add(amp);

            GroupBox gr = new GroupBox
            {
                Top = (ind + 1) * 26,
                Left = 2,
                Size = groupBox8.Size,
                Visible = true
            };
            PowIsAuto.Controls.Add(gr);

            RadioButton rad1 = new RadioButton
            {
                Top = 0,
                Left = 2,
                AutoSize = true,
                Text = "Автоматичен прекъсвач",
                Visible = true,
                Checked = EL2Ref.isAuto
            };
            rad1.CheckedChanged += new EventHandler(isAutoCheckedChanged);

            RadioButton rad2 = new RadioButton
            {
                Top = 0,
                Left = 156,
                AutoSize = true,
                Text = "Стопяем предпазител",
                Visible = true,
                Checked = !EL2Ref.isAuto
            };

            gr.Controls.Add(rad1);
            gr.Controls.Add(rad2);
        }

        private void NewLot_Click(object sender, EventArgs e)
        {
            string text = (sender as Button).Text;
            string lum = "";
            if (text == "Нов ЛОТ") lum = "ЛОТ ";
            else lum = "Осв. тяло ";
            LumList.Items.Add(lum + System.Convert.ToInt32(StickNum.Value) + "x" + System.Convert.ToInt32(Power.Value) + "W");
            LumList.Items.Add("");
            InstallationItem newIt = new InstallationItem
            {
                Name = LumList.Items[LumList.Items.Count - 1].ToString(),
                Id = 4000 + LumList.Items.Count,
                Type = (InstallationType)1
            };
            Main.Stats.Items.Add(newIt);
            int ind = LumList.Items.Count/2 - 1;
            TextBox num = new TextBox
            {
                Name = "Lum" + LumList.Items.Count,
                Size = MonoPhase.Size,
                Top = ind * 26,
                Left = 2,
                Multiline = true,
                Text = "0",
                Visible = true
            };
            num.TextChanged += new EventHandler(rows_TextChanged);

            Rows.Controls.Add(num);
            NumericUpDown amp = new NumericUpDown
            {
                Top = ind * 26,
                Left = 2,
                Size = MonoPhase.Size,
                Minimum = 4,
                Maximum = 500,
                Increment = 0.1M,
                Value = 25,
                DecimalPlaces = 0,
                Visible = true
            };
            amp.ValueChanged += new EventHandler(Amperage_ValueChanged);
            LumAmp.Controls.Add(amp);

            GroupBox gr = new GroupBox
            {
                Top = ind * 26,
                Left = 2,
                Size = groupBox8.Size,
                Visible = true
            };
            LumIsAuto.Controls.Add(gr);

            RadioButton rad1 = new RadioButton
            {
                Top = 0,
                Left = 2,
                AutoSize = true,
                Text = "Автоматичен прекъсвач",
                Visible = true,
                Checked = EL2Ref.isAuto
            };
            rad1.CheckedChanged += new EventHandler(isAutoCheckedChanged);

            RadioButton rad2 = new RadioButton
            {
                Top = 0,
                Left = 156,
                AutoSize = true,
                Text = "Стопяем предпазител",
                Visible = true,
                Checked = !EL2Ref.isAuto
            };

            gr.Controls.Add(rad1);
            gr.Controls.Add(rad2);
        }
        private void rows_TextChanged(object sender, EventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp.Text.IndexOf("\r\n") == -1 || temp.Text == "") return;
            temp.Text = temp.Text.Replace("\r\n", "");
            temp.Text = temp.Text.Replace(" ", "");
            int type = Rows.Controls.IndexOf(temp);
            string[] sp = temp.Text.Split(',');
            Installations lum = new Installations();
            Random r = new Random();
            //try
            //{
                foreach (Installations inst in currRoom.ListOfInstallations)
                {
                    if (inst.Item.Id > 4000) currRoom.ListOfInstallations.Remove(inst);
                }
                foreach (string s in sp)
                {
                    currRoom.ListOfInstallations.Add(new Installations());
                    for (int i = 0; i < System.Convert.ToInt32(s); i++ )
                    {
                        lum.isAutomaticProtector = ((LumIsAuto.Controls[type] as GroupBox).Controls[1] as RadioButton).Checked;
                        lum.Impedance = r.NextDouble() * (EL2Ref.MaxMeasured - EL2Ref.MaxMeasured) + EL2Ref.MaxMeasured;
                        lum.Amperage = System.Convert.ToDouble((LumAmp.Controls[type] as NumericUpDown).Value);
                        lum.Coefficient = EL2Ref.Coefficent;
                        lum.Ofazen = false;
                        lum.Reset = true;
                        lum.Max = (Main.Stats.lambda * lum.Coefficient) / lum.Amperage;
                        lum.FollowsRequirements = (lum.Impedance < lum.Max) && lum.Reset && !lum.Ofazen;
                        lum.NumberOfInstallation = i;
                        lum.NumberOfProtector = -1;
                        lum.Id = currRoom.ListOfInstallations.Count;
                        lum.InstallationName = Main.Stats.Items[type + ContCount.Controls.Count + LightCount.Controls.Count + PowAmpCollection.Controls.Count].Name + " №" + lum.NumberOfInstallation.ToString();
                        lum.Item = Main.Stats.Items[type + ContCount.Controls.Count + LightCount.Controls.Count + PowAmpCollection.Controls.Count];
                        currRoom.ListOfInstallations.Add(lum);
                    }

                }
            //}
            //catch
            //{
            //    MessageBox.Show("Грешно въведени данни за редовете. Използвайте числа, разделени със запетаи!");
            //    temp.Text = "";
            //}
        }

        private void AddInst(Installations inst)
        {
            currRoom.ListOfInstallations.Add(inst);
        }

        private void CurrProg_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                List<Installations> list = new List<Installations>();
                foreach (Installations inst in currRoom.ListOfInstallations) list.Add(inst);
                int index = e.RowIndex;
                int type = Main.Stats.Items.IndexOf(currRoom.ListOfInstallations.ElementAt(index).Item);
                for (int i = index; i <= currRoom.LastIndex(type); i++)
                {
                    currRoom.ListOfInstallations.ElementAt(i).Amperage = currRoom.ListOfInstallations.ElementAt(index).Amperage;
                }
                CurrProg.Update();
                CurrProg.Refresh();
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            //M.refRoom = currRoom;
            M.SuccRoom = true;
            //this.FormClosing += new FormClosingEventHandler(M.ImpRoom_FormClosing);
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            M.SuccRoom = false;
            this.Close();
        }

        private void Light_Enter(object sender, EventArgs e)
        {

        }

        private void Contacts_Enter(object sender, EventArgs e)
        {

        }

        private void RoomName_TextChanged(object sender, EventArgs e)
        {
            currRoom.RoomName = RoomName.Text;
        }

        private void Luiminaires_Enter(object sender, EventArgs e)
        {

        }

        private void PowerEl_Enter(object sender, EventArgs e)
        {

        }

        private void newLum_Enter(object sender, EventArgs e)
        {

        }

        private void ImpRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!M.SuccRoom)
            {
                if(isNew)
                {
                    Rooms r = M.tempImp.ListOfSectors.ElementAt(sector).ListOfFloors.ElementAt(floor).ListOfRooms.ElementAt(room);
                    M.tempImp.ListOfSectors.ElementAt(sector).ListOfFloors.ElementAt(floor).ListOfRooms.Remove(r);
                    M.RemoveImpRoom(sector, floor, room);
                }
                return;
            }
            M.tempImp.ListOfSectors.ElementAt(sector).ListOfFloors.ElementAt(floor).ListOfRooms.ElementAt(room).ListOfInstallations = currRoom.ListOfInstallations;
            M.tempImp.ListOfSectors.ElementAt(sector).ListOfFloors.ElementAt(floor).ListOfRooms.ElementAt(room).RoomName = currRoom.RoomName;
            M.ImpTreeSelect(sector, floor, room, isNew, currRoom.RoomName);
        }
    }
}

