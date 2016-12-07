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
    public partial class DefRoom : Form
    {
        Rooms_El_4 currRoom = new Rooms_El_4();
        EL_4 EL4Ref;
        BindingSource ImpBindingSource = new BindingSource();
        Main M;
        public DefRoom(Main M, Rooms_El_4 currRoom, EL_4 El4Ref)
        {
            this.currRoom = currRoom;
            this.EL4Ref = El4Ref;
            this.M = M;
            InitializeComponent();
        }
        public DefRoom()
        {
            InitializeComponent();
        }

        private void DefPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox temp = sender as ComboBox;
            int type = ContAmp.Controls.IndexOf(temp);
            if (ContAmp.Controls.IndexOf(temp) == -1) type = ContAmp.Controls.Count;
            if (LightAmp.Controls.IndexOf(temp) == -1 && type >= ContAmp.Controls.Count) type += LightAmp.Controls.Count;
            if (LightAmp.Controls.IndexOf(temp) != -1 && type >= ContAmp.Controls.Count) type += LightAmp.Controls.IndexOf(temp);
            if (PowAmperage.Controls.IndexOf(temp) != -1) type += PowAmpCollection.Items.IndexOf(PowAmpList.Items[PowAmperage.Controls.IndexOf(temp)]);
            if (type == -1) type += PowAmpCollection.Items.Count + LumAmp.Controls.IndexOf(temp);
            int last = currRoom.LastIndex(type);
            int first = currRoom.FirstIndex(type);
            for (int i = first; i <= last; i++)
            {
                currRoom.ListOfInstallations.ElementAt(i).TypeProtector = temp.SelectedIndex;
                (CurrProg.Rows[i].Cells[5] as DataGridViewComboBoxCell).Value = (CurrProg.Rows[i].Cells[5] as DataGridViewComboBoxCell).Items[temp.SelectedIndex];
            }
            CurrProg.Update();
            CurrProg.Refresh();
        }


        private void DefRoom_Load(object sender, EventArgs e)
        {
            foreach (Control con in ContAmp.Controls)
            {
                foreach (Protectors p in Main.Stats.Protectors) (con as ComboBox).Items.Add(p.Name);
            } 
            foreach (Control con in LightAmp.Controls)
            {
                foreach (Protectors p in Main.Stats.Protectors) (con as ComboBox).Items.Add(p.Name);
            } 
            foreach (Control con in PowAmperage.Controls)
            {
                foreach (Protectors p in Main.Stats.Protectors) (con as ComboBox).Items.Add(p.Name);
            }
            foreach (InstallationItem item in Main.Stats.Items)
            {
                if ((int)item.Type == 0) { ContList.Items.Add(item.Name); ContList.Items.Add(""); }
                if ((int)item.Type == 1 && item.Id < 4000) { LightList.Items.Add(item.Name); LightList.Items.Add(""); }
                if ((int)item.Type == 1 && item.Id >= 4000) { LumList.Items.Add(item.Name); LumList.Items.Add(""); }
                if ((int)item.Type == 2) PowAmpCollection.Items.Add(item.Name);
            }
            CurrProg.AutoGenerateColumns = false;
            ImpBindingSource = new BindingSource();
            foreach (Installation_El_4 inst in currRoom.ListOfInstallations) ImpBindingSource.Add(inst);
            CurrProg.DataSource = ImpBindingSource;
            CurrProg.Columns[0].DataPropertyName = "InstallationName";
            CurrProg.Columns[1].DataPropertyName = "DT";
            CurrProg.Columns[2].DataPropertyName = "DN";
            CurrProg.Columns[3].DataPropertyName = "MaxDN";
            CurrProg.Columns[4].DataPropertyName = "MaxDT"; 
            for (int j = 0; j < CurrProg.Rows.Count; j++)
            {
                (CurrProg.Rows[j].Cells[5] as DataGridViewComboBoxCell).Items.Clear();
                foreach (Protectors p in Main.Stats.Protectors) (CurrProg.Rows[j].Cells[5] as DataGridViewComboBoxCell).Items.Add(p.Name);
            } 
            //Prot.ValueMember = "Main.Stats.Protectors[]"; CurrProg.Columns[5].DataPropertyName = "TypeProtector";
            foreach (Control con in this.Controls)
            {
                if (con is Panel) { (con as Panel).AutoScroll = false; (con as Panel).AutoSize = true; }
                //if (con is GroupBox) (con as GroupBox).Sc = true;
            }
        }

        private void Count_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown temp = sender as NumericUpDown;
            int value = System.Convert.ToInt32(temp.Value);
            int type = ContCount.Controls.IndexOf(temp);
            if (ContCount.Controls.IndexOf(temp) == -1) type = ContCount.Controls.Count;
            if (LightCount.Controls.IndexOf(temp) == -1 && type >= ContCount.Controls.Count) type += LightCount.Controls.Count;
            if (LightCount.Controls.IndexOf(temp) != -1 && type >= ContCount.Controls.Count) type += LightCount.Controls.IndexOf(temp);
            if (PowAmp.Controls.IndexOf(temp) != -1) type += PowAmpCollection.Items.IndexOf(PowAmpList.Items[PowAmp.Controls.IndexOf(temp)]);
            int prim = (int)Main.Stats.Items[type].Type;
            if (Main.Stats.Items[type].Id >= 4000) prim = 3;
            //Panel[] 
            int last = currRoom.LastIndex(type);
            int first = currRoom.FirstIndex(type);
            int count = last - first + 1;
            Random r = new Random();
            double maxDN = EL4Ref.DNHighest;
            double minDN = EL4Ref.DNLowest;
            double maxDt = EL4Ref.DTHighest;
            double minDt = EL4Ref.DTLowest;
            if (value < count)
            {
                DialogResult dialogResult = MessageBox.Show("По този начин ще изтриете вече въведени съоръжения. Сигурни ли сте, че искате да продължите?", "Изтриване на съоръжения", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = first + value; i <= last; i++)
                    {
                        currRoom.RemoveAt(first + value);
                        ImpBindingSource.RemoveAt(first + value);
                        CurrProg.DataSource = ImpBindingSource;
                    }
                }
            }
            else
            {
                Installation_El_4 tempInst = new Installation_El_4();
                Control ContainerCount = new Control(), ContainerType = new Control(), ContainerAmp = new Control();
                if (prim == 0) { ContainerCount = ContCount; ContainerAmp = ContAmp; }
                if (prim == 1) { ContainerCount = LightCount; ContainerAmp = LightAmp; }
                if (prim == 2) { ContainerCount = PowAmp; ContainerAmp = PowAmperage; }
                if (prim == 3) { ContainerCount = Rows; ContainerAmp = LumAmp; }
                int typeThis = ContainerCount.Controls.IndexOf(temp);
                if (last == -1)
                {
                    for (int i = 0; i < value; i++)
                    {
                        tempInst.Item = Main.Stats.Items[type];
                        tempInst.DN = r.NextDouble() * (maxDN - minDN) + minDN;
                        tempInst.DT = r.NextDouble() * (maxDt - minDt) + minDt;
                        tempInst.MaxDN = EL4Ref.maxDN;
                        tempInst.MaxDT = EL4Ref.maxDT;
                        tempInst.FollowsRequirements = (tempInst.DN < tempInst.MaxDN) && (tempInst.DT < tempInst.MaxDT);
                        tempInst.NumberIn = i + 1;
                        tempInst.TypeProtector = (ContainerAmp.Controls[typeThis] as ComboBox).SelectedIndex;
                        tempInst.Id = currRoom.ListOfInstallations.Count;
                        tempInst.InstallationName = Main.Stats.Items[type].Name + " №" + tempInst.NumberIn.ToString();
                        currRoom.ListOfInstallations.Add(tempInst);
                        ImpBindingSource.Add(tempInst);
                        CurrProg.DataSource = ImpBindingSource;
                        CurrProg.Columns[0].DataPropertyName = "InstallationName";
                        CurrProg.Columns[1].DataPropertyName = "DT";
                        CurrProg.Columns[2].DataPropertyName = "DN";
                        CurrProg.Columns[3].DataPropertyName = "MaxDN";
                        CurrProg.Columns[4].DataPropertyName = "MaxDT";
                        CurrProg.Update();
                        CurrProg.Refresh();
                        for (int j = 0; j < CurrProg.Rows.Count; j++)
                        {
                            (CurrProg.Rows[j].Cells[5] as DataGridViewComboBoxCell).Items.Clear();
                            foreach(Protectors p in Main.Stats.Protectors)(CurrProg.Rows[j].Cells[5] as DataGridViewComboBoxCell).Items.Add(p.Name);
                        } 
                        //Prot.ValueMember = "Main.Stats.Protectors[]"; CurrProg.Columns[5].DataPropertyName = "TypeProtector";
                    }
                }
                else
                {
                    for (int i = 0; i < value - count; i++)
                    {
                        tempInst.Item = Main.Stats.Items[type];
                        tempInst.DN = r.NextDouble() * (maxDN - minDN) + minDN;
                        tempInst.DT = r.NextDouble() * (maxDt - minDt) + minDt;
                        tempInst.MaxDN = EL4Ref.maxDN;
                        tempInst.MaxDT = EL4Ref.maxDT;
                        tempInst.FollowsRequirements = (tempInst.DN < tempInst.MaxDN) && (tempInst.DT < tempInst.MaxDT);
                        tempInst.NumberIn = i + 1 + currRoom.ListOfInstallations.ElementAt(last).NumberIn;
                        tempInst.TypeProtector = (ContainerAmp.Controls[typeThis] as ComboBox).SelectedIndex;
                        tempInst.Id = currRoom.ListOfInstallations.Count;
                        tempInst.InstallationName = Main.Stats.Items[type].Name + " №" + tempInst.NumberIn.ToString();
                        currRoom.Insert(last + i + 1, tempInst);
                        ImpBindingSource.Insert(last + i + 1, tempInst);
                        CurrProg.DataSource = ImpBindingSource;
                        CurrProg.Update();
                        CurrProg.Refresh();
                        for (int j = 0; j < CurrProg.Rows.Count; j++)
                        {
                            (CurrProg.Rows[j].Cells[5] as DataGridViewComboBoxCell).Items.Clear();
                            foreach (Protectors p in Main.Stats.Protectors) (CurrProg.Rows[j].Cells[5] as DataGridViewComboBoxCell).Items.Add(p.Name);
                        } 
                    }
                }
            }
            CurrProg.Update();
            CurrProg.Refresh();
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
            for (int i = ind; i < PowAmp.Controls.Count; i++)
            {
                PowAmp.Controls[i].Top -= 26;
                PowAmperage.Controls[i].Top -= 26;
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
            ComboBox pro = new ComboBox
            {
                Top = ind * 26,
                Left = 2,
                Size = MonoPhase.Size,
                Visible = true
            };
            foreach (Protectors p in Main.Stats.Protectors) pro.Items.Add(p.Name);
            pro.SelectedIndexChanged += new EventHandler(DefPro_SelectedIndexChanged);
            PowAmperage.Controls.Add(pro);
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
            int ind = LumList.Items.Count / 2 - 1;
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
            ComboBox pro = new ComboBox
            {
                Top = ind * 26,
                Left = 2,
                Size = MonoPhase.Size,
                Visible = true
            };

            foreach (Protectors p in Main.Stats.Protectors) pro.Items.Add(p.Name); 
            pro.SelectedIndexChanged += new EventHandler(DefPro_SelectedIndexChanged);
            LumAmp.Controls.Add(pro);

        }
        private void rows_TextChanged(object sender, EventArgs e)
        {
            TextBox temp = sender as TextBox;
            if (temp.Text.IndexOf("\r\n") == -1 || temp.Text == "") return;
            temp.Text = temp.Text.Replace("\r\n", "");
            temp.Text = temp.Text.Replace(" ", "");
            int type = Rows.Controls.IndexOf(temp);
            string[] sp = temp.Text.Split(',');
            Installation_El_4 lum = new Installation_El_4();
            double maxDN = EL4Ref.DNHighest;
            double minDN = EL4Ref.DNLowest;
            double maxDt = EL4Ref.DTHighest;
            double minDt = EL4Ref.DNLowest;
            Random r = new Random();
            try
            {
                foreach (Installation_El_4 inst in currRoom.ListOfInstallations)
                {
                    if (inst.Item.Id > 4000) currRoom.ListOfInstallations.Remove(inst);
                }
                foreach (string s in sp)
                {
                    currRoom.ListOfInstallations.Add(new Installation_El_4());
                    for (int i = 0; i < System.Convert.ToInt32(s); i++)
                    {
                        lum.Item = Main.Stats.Items[type];
                        lum.DN = r.NextDouble() * (maxDN - minDN) + minDN;
                        lum.DT = r.NextDouble() * (maxDt - minDt) + minDt;
                        lum.MaxDN = EL4Ref.maxDN;
                        lum.MaxDT = EL4Ref.maxDT;
                        lum.FollowsRequirements = (lum.DN < lum.MaxDN) && (lum.DT < lum.MaxDT);
                        lum.NumberIn = i + 1;
                        lum.TypeProtector = (ContAmp.Controls[type] as ComboBox).SelectedIndex;
                        lum.Id = currRoom.ListOfInstallations.Count;
                        lum.InstallationName = Main.Stats.Items[type].Name + " №" + lum.NumberIn.ToString();
                        lum.Id = currRoom.ListOfInstallations.Count;
                        lum.InstallationName = Main.Stats.Items[type + ContCount.Controls.Count + LightCount.Controls.Count + PowAmpCollection.Controls.Count].Name + " №" + lum.NumberIn.ToString();
                        lum.Item = Main.Stats.Items[type + ContCount.Controls.Count + LightCount.Controls.Count + PowAmpCollection.Controls.Count];
                        currRoom.ListOfInstallations.Add(lum);
                    }

                }
            }
            catch
            {
                MessageBox.Show("Грешно въведени данни за редовете. Използвайте числа, разделени със запетаи!");
                temp.Text = "";
            }
        }


        private void LoadRoom(Rooms_El_4 r)
        {
            RoomName.Text = r.RoomName;
            Panel[] counts = new Panel[3] { ContCount, LightCount, PowAmp };
            Panel[] amps = new Panel[3] { ContAmp, LightAmp, PowAmperage };
            foreach (Installation_El_4 inst in r.ListOfInstallations)
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
                if ((counts[type].Controls[sec] as NumericUpDown).Value == 1)
                    (amps[type].Controls[sec] as ComboBox).SelectedIndex = inst.TypeProtector;
            }
        }


        private void CurrProg_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                List<Installation_El_4> list = new List<Installation_El_4>();
                foreach (Installation_El_4 inst in currRoom.ListOfInstallations) list.Add(inst);
                int index = e.RowIndex;
                int type = Main.Stats.Items.IndexOf(currRoom.ListOfInstallations.ElementAt(index).Item);
                for (int i = index; i <= currRoom.LastIndex(type); i++)
                {
                    currRoom.ListOfInstallations.ElementAt(i).TypeProtector = currRoom.ListOfInstallations.ElementAt(index).TypeProtector;
                }
                CurrProg.Update();
                CurrProg.Refresh();
            }
        }

        private void RoomName_TextChanged(object sender, EventArgs e)
        {
            currRoom.RoomName = RoomName.Text;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            M.refRoomDef = currRoom;
            M.DefSuccRoom = true;
            this.FormClosing += new FormClosingEventHandler(M.DefRoom_FormClosing);
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            M.SuccRoom = false;
        }

        private void CurrProg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
