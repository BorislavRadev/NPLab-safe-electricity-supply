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
        public static Objects currObj = new Objects();
        public static StaticClass Stats;
        int objId;
        public bool dyn = false, permit = false;
        NPLabDbContext db = new NPLabDbContext();
        public Home H;
        public Main(int id, Home h)
        {
            InitializeComponent();
            this.H = h;
            this.objId = id;
        }

        //private void IsolationTable_Scroll(object sender, EventArgs e) { IsolationTable.VerticalScroll.Value = (sender as ScrollBar).Value; }

        private void Main_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nPLabDataSet.Engineers' table. You can move, or remove it, as needed.
            //this.engineersTableAdapter.Fill(this.nPLabDataSet.Engineers);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            
            currObj = (from p in db.Object
                       where p.Id == this.objId
                       select p).FirstOrDefault();
            //
            //var db = new NPLabDbContext();

            //Създаване на скролбар
            IsolationTable.AutoScroll = true;

            //Инициализация на статичните данни
            Stats = new StaticClass();

            // 
            tempIsol = new EL_1
            {
                Date = DateTime.Now,
                SourceVoltage = System.Convert.ToInt32(Naprejenie.Value),
                Min = System.Convert.ToDouble(Norm.Value),
                MinMeasured = System.Convert.ToInt32(MinMeas.Value),
                MaxMeasured = System.Convert.ToInt32(MaxMeas.Value),
            };

            EngineerNameIsol.Items.AddRange(Stats.Engineers);
            EngineerNameIsol.SelectedIndex = 0;
            EngineerNameImp.Items.AddRange(Stats.Engineers);
            EngineerNameImp.SelectedIndex = 0;
            EngineerNameGr.Items.AddRange(Stats.Engineers);
            EngineerNameGr.SelectedIndex = 0;
            EngineerNameLiPro.Items.AddRange(Stats.Engineers);
            EngineerNameLiPro.SelectedIndex = 0;
            EngineerNameDef.Items.AddRange(Stats.Engineers);
            EngineerNameDef.SelectedIndex = 0;

            //Добавяне на вече съществуващите контроли към списъците с тях
            Names.Add(textBoxName);
            TypeCabs.Add(TypeCabel);
            Count.Add(Number);
            Surface.Add(CabelArea);
            MeasuredForm.Add(Measured);
            Conductors.Add(Cond);
            Rem.Add(Delete);

            //По подразбиране се избира тип кабел "СВТ"
            tempIsol.ListOfCabels.Add(new Cabel());
            TypeCabel.SelectedIndex = 0;
            textBoxName_TextChanged(textBoxName, new EventArgs());
            Number_ValueChanged(Number, new EventArgs());
            CabelArea_ValueChanged(CabelArea, new EventArgs());
            Measured_ValueChanged(Measured, new EventArgs());

            tempImp.Coefficent = System.Convert.ToInt32(Coefficient.Value);
            tempImp.MaxMeasured = System.Convert.ToDouble(Maximum.Value);
            tempImp.MinMeasured = System.Convert.ToDouble(Minimum.Value);

            Grounding gr = new Models.Grounding
            {
                AuxiliaryGrounding = System.Convert.ToDouble(AuxGr1.Value),
                Probe = System.Convert.ToDouble(Probe1.Value),
                Measured = System.Convert.ToDouble(Meas1.Value),
                Adjusted = System.Convert.ToDouble(Adj1.Value),
                Name = GroundingName.Text
            };
            tempGr.ListOfGroundings.Add(gr);

            LightningGrounding LiGr = new Models.LightningGrounding
            {
                AuxiliaryGrounding = System.Convert.ToDouble(AuxLi1.Value),
                Probe = System.Convert.ToDouble(ProbeLi1.Value),
                Measured = System.Convert.ToDouble(MeasLi1.Value),
                Adjusted = System.Convert.ToDouble(AdjLi1.Value),
                Name = LightningName.Text
            };
            tempLiPro.ListOfGroundings_El_3m.Add(LiGr);
            LoadObj(currObj);
            tempDef.DNHighest = System.Convert.ToDouble(MaxIN.Value);
            tempDef.DNLowest = System.Convert.ToDouble(MinIN.Value);
            tempDef.DTHighest = System.Convert.ToDouble(Maxt.Value);
            tempDef.DTLowest = System.Convert.ToDouble(Mint.Value);
            tempDef.maxDN = System.Convert.ToDouble(NormIN.Value);
            tempDef.maxDT = System.Convert.ToDouble(Normt.Value);
        }

        private void LoadObj(Objects currObj)
        {
            MainInspector.SelectedItem = currObj.Inspector;
            Client.Text = currObj.Client;
            ObjectName.Text = currObj.ObjectName;
        }

        //EL_4
        private void Mint_ValueChanged(object sender, EventArgs e)
        {
            double valueMin = System.Convert.ToDouble(Mint.Value);
            double valueMax = System.Convert.ToDouble(Maxt.Value);
            Random r = new Random();
            tempDef.DTLowest = valueMin;
            tempDef.DTHighest = valueMax;
            foreach (Sectors_El_4 sect in tempDef.ListOfSectors)
            {
                foreach (Floors_El_4 fl in sect.ListOfFloors)
                {
                    foreach (Rooms_El_4 room in fl.ListOfRooms)
                    {
                        foreach (Installation_El_4 inst in room.ListOfInstallations)
                        {
                            inst.DT = r.Next() * (valueMax - valueMin) + valueMin;
                        }
                    }
                }
            }
        }

        private void MinIN_ValueChanged(object sender, EventArgs e)
        {
            double valueMin = System.Convert.ToDouble(MinIN.Value);
            double valueMax = System.Convert.ToDouble(MaxIN.Value);
            Random r = new Random();
            tempDef.DNLowest = valueMin;
            tempDef.DNHighest = valueMax;
            foreach (Sectors_El_4 sect in tempDef.ListOfSectors)
            {
                foreach (Floors_El_4 fl in sect.ListOfFloors)
                {
                    foreach (Rooms_El_4 room in fl.ListOfRooms)
                    {
                        foreach (Installation_El_4 inst in room.ListOfInstallations)
                        {
                            inst.DN = r.Next() * (valueMax - valueMin) + valueMin;
                        }
                    }
                }
            }
        }

        private void Normt_ValueChanged(object sender, EventArgs e)
        {
            double t = System.Convert.ToDouble(Normt.Value);
            double IN = System.Convert.ToDouble(NormIN.Value);
            tempDef.maxDN = IN;
            tempDef.maxDT = t;
        }

        private void DefEng_SelectedIndexChanged(object sender, EventArgs e)
        {
            tempDef.NameOfEngineer = EngineerNameDef.SelectedItem.ToString();
        }

        private void EditProts_Click(object sender, EventArgs e)
        {
            EditProtectors ed = new EditProtectors(this);
            ed.Show();
        }

        private void MaxLi_ValueChanged(object sender, EventArgs e)
        {
            tempLiPro.Max = System.Convert.ToDouble(MaxLi.Value);
        }

        private void MaxGr_ValueChanged(object sender, EventArgs e)
        {
            tempGr.Max = System.Convert.ToDouble(MaxGr.Value);
        }

        private void Client_TextChanged(object sender, EventArgs e)
        {
            currObj.Client = Client.Text;
        }

        private void ObjectName_TextChanged(object sender, EventArgs e)
        {
            currObj.ObjectName = ObjectName.Text;
        }

        private void ConvertImp_Click(object sender, EventArgs e)
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
            if (URL != "") ConvInst.ConvertImpExcel(URL, tempImp);
        }
        private void ConvertGr_Click(object sender, EventArgs e)
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
            if (URL != "") ConvInst.ConvertGrExcel(URL, tempGr);
        }
        private void ConvertLiPro_Click(object sender, EventArgs e)
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
            if (URL != "") ConvInst.ConvertLiProExcel(URL, tempLiPro);
        }
        private void ConvertDef_Click(object sender, EventArgs e)
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
            if (URL != "") ConvInst.ConvertDefExcel(URL, tempDef);
        }

        public void LoadIsol(EL_1 e, bool clear)
        {
            EngineerNameIsol.SelectedItem = e.NameOfEngineer;
            IsolationDate.Value = e.Date;
            Norm.Value = System.Convert.ToDecimal(e.Min);
            MinMeas.Value = e.MinMeasured;
            MaxMeas.Value = e.MaxMeasured;
            Naprejenie.Value = e.SourceVoltage;
            if (clear)
            {
                for (int j = 1; j < Rem.Count; j++) Rem[1].PerformClick();
            }
            int i;
            for (i = 0; i < e.ListOfCabels.Count; i++)
            {
                Cabel cab = e.ListOfCabels.ElementAt(i);
                Names[i].Text = cab.CabelType;
                TypeCabs[i].SelectedItem = cab.CabelModel;
                Count[i].Value = cab.ConductorsCount;
                Surface[i].Value = System.Convert.ToDecimal(cab.Thickness);
                MeasuredForm[i].Value = System.Convert.ToDecimal(cab.Measured);
                if(Names.Count < e.ListOfCabels.Count) ButtonForAdd.PerformClick();
            }
            //Rem[i-1].PerformClick();
            //Rem[i-2].PerformClick();
            tempIsol = e;
        }
        public void LoadImp(El_2 e, bool clear)
        {
            EngineerNameImp.SelectedItem = e.NameOfEngineer;
            ImpedanceDate.Value = e.ControlDate;
            Coefficient.Value = e.Coefficent;
            Minimum.Value = System.Convert.ToDecimal(e.MinMeasured);
            Maximum.Value = System.Convert.ToDecimal(e.MaxMeasured);
            if(clear) ImpTreeView.Nodes.Clear();
            foreach (Sectors sec in e.ListOfSectors)
            {
                ImpTreeView.Nodes.Add(sec.SectorName);
                foreach (Floors f in sec.ListOfFloors)
                {
                    ImpTreeView.Nodes[ImpTreeView.Nodes.Count - 1].Nodes.Add(f.NameFloor);
                    foreach (Rooms r in f.ListOfRooms)
                    {
                        ImpTreeView.Nodes[ImpTreeView.Nodes.Count - 1].Nodes[ImpTreeView.Nodes[ImpTreeView.Nodes.Count - 1].Nodes.Count - 1].Nodes.Add(r.RoomName);
                    }
                }
            }
            tempImp = e;
        }
        public void LoadGr(EL_3 e, bool clear)
        {
            EngineerNameGr.SelectedItem = e.NameOfEngineer;
            GroundingDate.Value = e.Date;
            if (e.WetSeason) Fi.Value = 1.3M;
            else Fi.Value = 1.15M;
            MaxGr.Value = System.Convert.ToDecimal(e.Max);
            wet.Checked = e.WetSeason;
            if (clear)
            {
                for (int j = 1; j < DelButtons.Controls.Count; j++) (DelButtons.Controls[1] as Button).PerformClick();
            }
            int i;
            for (i = 0; i < e.ListOfGroundings.Count; i++)
            {
                Grounding gr = e.ListOfGroundings.ElementAt(i);
                (GrName.Controls[i] as TextBox).Text = gr.Name;
                (AuxGr.Controls[i] as NumericUpDown).Value = System.Convert.ToDecimal(gr.AuxiliaryGrounding);
                (Probe.Controls[i] as NumericUpDown).Value = System.Convert.ToDecimal(gr.Probe);
                (GrMeas.Controls[i] as NumericUpDown).Value = System.Convert.ToDecimal(gr.Measured);
                if (GrName.Controls.Count < e.ListOfGroundings.Count) NewGr.PerformClick();
            }

            tempGr = e;
        }
        public void LoadLiPro(EL_3m e, bool clear)
        {
            EngineerNameLiPro.SelectedItem = e.NameOfEngineer;
            LiProDate.Value = e.Date;
            if (e.WetSeason) FiLi.Value = 1.3M;
            else FiLi.Value = 1.15M;
            MaxLi.Value = System.Convert.ToDecimal(e.Max);
            wetLi.Checked = e.WetSeason;
            if (clear)
            {
                for (int j = 1; j < DelButtonsLi.Controls.Count; j++) (DelButtonsLi.Controls[1] as Button).PerformClick();
            }
            int i;
            for (i = 0; i < e.ListOfGroundings_El_3m.Count; i++)
            {
                LightningGrounding LiPro = e.ListOfGroundings_El_3m.ElementAt(i);
                (LiName.Controls[i] as TextBox).Text = LiPro.Name;
                (AuxLi.Controls[i] as NumericUpDown).Value = System.Convert.ToDecimal(LiPro.AuxiliaryGrounding);
                (ProbeLi.Controls[i] as NumericUpDown).Value = System.Convert.ToDecimal(LiPro.Probe);
                (LiMeas.Controls[i] as NumericUpDown).Value = System.Convert.ToDecimal(LiPro.Measured);
                if (LiName.Controls.Count < e.ListOfGroundings_El_3m.Count) NewLi.PerformClick();
            }

            
            tempLiPro = e;
        }

        public void LoadDef(EL_4 e, bool clear)
        {
            EngineerNameDef.SelectedItem = e.NameOfEngineer;
            DefectiveProtectionDate.Value = e.Date;
            Mint.Value = System.Convert.ToDecimal(e.DTLowest);
            Maxt.Value = System.Convert.ToDecimal(e.DTHighest);
            MinIN.Value = System.Convert.ToDecimal(e.DNLowest);
            MaxIN.Value = System.Convert.ToDecimal(e.DNLowest);
            Normt.Value = System.Convert.ToDecimal(e.maxDT);
            NormIN.Value = System.Convert.ToDecimal(e.maxDN);
            if(clear) DefTreeView.Nodes.Clear();
            foreach (Sectors_El_4 sec in e.ListOfSectors)
            {
                DefTreeView.Nodes.Add(sec.SectorName);
                foreach (Floors_El_4 f in sec.ListOfFloors)
                {
                    DefTreeView.Nodes[DefTreeView.Nodes.Count - 1].Nodes.Add(f.NameFloor);
                    foreach (Rooms_El_4 r in f.ListOfRooms)
                    {
                        DefTreeView.Nodes[DefTreeView.Nodes.Count - 1].Nodes[DefTreeView.Nodes[DefTreeView.Nodes.Count - 1].Nodes.Count - 1].Nodes.Add(r.RoomName);
                    }
                }
            }
            tempDef = e;
        }

        public void RemoveImpRoom(int sector, int floor, int room)
        {
            ImpTreeView.Nodes[sector].Nodes[floor].Nodes[room].Remove();
        }

        private void DefTreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Level != 2) return;
            DefRoom f = new DefRoom(this, typicalDefRoom, tempDef);
        }

        private void DefTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void SaveObj_Click(object sender, EventArgs e)
        {
            if (MainInspector.SelectedItem == null)
            {
                MessageBox.Show("Моля изберете инженер!");
                return;
            }
            currObj.Client = Client.Text;
            currObj.Inspector = MainInspector.SelectedItem.ToString();
            currObj.ObjectName = ObjectName.Text;
            db.SaveChanges();
            permit = true;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            H.Show();
            H.Focus();
        }

        private void ImpTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ImpTreeView.SelectedNode.Level == 0)
            {
                sector = ImpTreeView.SelectedNode.Index;
                floor = 0;
                room = 0;
            }
            if (ImpTreeView.SelectedNode.Level == 1)
            {
                sector = ImpTreeView.SelectedNode.Parent.Index;
                floor = ImpTreeView.SelectedNode.Index;
                room = 0;
            }
            if (ImpTreeView.SelectedNode.Level == 2)
            {
                sector = ImpTreeView.SelectedNode.Parent.Parent.Index;
                floor = ImpTreeView.SelectedNode.Parent.Index;
                room = ImpTreeView.SelectedNode.Index;
            }
        }

        private void Project_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Project.SelectedTab != ObjectData && !permit) Project.SelectedTab = ObjectData; 
        }

    }
}