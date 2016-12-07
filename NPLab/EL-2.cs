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
        public El_2 tempImp = new El_2();
        public Rooms refRoom = new Rooms();
        public bool SuccRoom = false, editDef = false, isNewImp = true;
        public DateTime currDateImp = DateTime.Now;
        public int sector = -1, room = -1, floor = -1;
        private void NewRoom_Click(object sender, EventArgs e)
        {
            if (ImpTreeView.SelectedNode == null)
            {
                MessageBox.Show("Моля изберете етаж!");
                return;
            }
            if (ImpTreeView.SelectedNode.Level != 1)
            {
                MessageBox.Show("Моля изберете етаж!");
                return;
            }
            room = tempImp.ListOfSectors.ElementAt(sector).ListOfFloors.ElementAt(floor).ListOfRooms.Count;
            tempImp.ListOfSectors.ElementAt(sector).ListOfFloors.ElementAt(floor).ListOfRooms.Add(new Rooms());
            ImpRoom f = new ImpRoom(this, refRoom, tempImp, sector, floor, room, true);
            f.Show();
        }

        public void ImpTreeSelect(int sector, int floor, int room, bool isNew, string name)
        {
            if(isNew)ImpTreeView.Nodes[sector].Nodes[floor].Nodes.Add(name);
            ImpTreeView.Select();
            ImpTreeView.SelectedNode = ImpTreeView.Nodes[sector].Nodes[floor].Nodes[room];
        }

        private void EditRoom_Click(object sender, EventArgs e)
        {
            if(ImpTreeView.SelectedNode == null)
            {
                MessageBox.Show("Моля изберете помещение!");
                return;
            }
            if (ImpTreeView.SelectedNode.Level != 2)
            {
                MessageBox.Show("Моля изберете помещение!");
                return;
            }
            room = ImpTreeView.SelectedNode.Index;
            ImpRoom f = new ImpRoom(this, refRoom, tempImp, sector, floor, room, false);
            f.Show();
        }

        private void NewSector_Click(object sender, EventArgs e)
        {
            Sectors sec = new Sectors
            {
                Id = tempImp.ListOfSectors.Count,
                SectorName = "(Нов сектор)"
            };
            tempImp.ListOfSectors.Add(sec);
            TreeNode treeNode = new TreeNode("Нов сектор");
            ImpTreeView.Nodes.Add(treeNode);
            //if (edit) { treeNode.EndEdit(true); return; }
            //ImpTreeView.SelectedNode = null;
            treeNode.BeginEdit();
            //edit = true;
            //ImpTreeView.SelectedNode = null;
        }

        private void ImpTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true;
                //edit = false;
                e.Node.BeginEdit();
            }
            else
            {
                if (e.Node.Level == 0)
                    tempImp.ListOfSectors.ElementAt(e.Node.Index).SectorName = e.Label;
                if (e.Node.Level == 1)
                    tempImp.ListOfSectors.ElementAt(e.Node.Parent.Index).ListOfFloors.ElementAt(e.Node.Index).NameFloor = e.Label;
                if (e.Node.Level == 2)
                    tempImp.ListOfSectors.ElementAt(e.Node.Parent.Parent.Index).ListOfFloors.ElementAt(e.Node.Parent.Index).ListOfRooms.ElementAt(e.Node.Index).RoomName = e.Label;
                e.Node.EndEdit(false);
                //edit = false;
            }
        }

        private void NewFloor_Click(object sender, EventArgs e)
        {
            if (ImpTreeView.SelectedNode == null)
            {
                MessageBox.Show("Моля изберете сектор!");
                return;
            }
            if (ImpTreeView.SelectedNode.Level != 0)
            {
                MessageBox.Show("Моля изберете сектор!");
                return;
            }
            Floors fl = new Floors
            {
                Id = tempImp.ListOfSectors.Count,
                NameFloor = "(Нов етаж)"
            };
            tempImp.ListOfSectors.ElementAt(ImpTreeView.SelectedNode.Index).ListOfFloors.Add(fl);
            TreeNode treeNode = new TreeNode("Нов етаж");
            ImpTreeView.SelectedNode.Nodes.Add(treeNode);
            ImpTreeView.SelectedNode = treeNode;
            treeNode.BeginEdit();
        }


        private void DeleteImp_Click(object sender, EventArgs e)
        {
            if (ImpTreeView.SelectedNode == null) return;
            if (ImpTreeView.SelectedNode.Level == 0)
                tempImp.ListOfSectors.Remove(tempImp.ListOfSectors.ElementAt(ImpTreeView.SelectedNode.Index));
            if (ImpTreeView.SelectedNode.Level == 1)
                tempImp.ListOfSectors.ElementAt(ImpTreeView.SelectedNode.Parent.Index).ListOfFloors.Remove(tempImp.ListOfSectors.ElementAt(ImpTreeView.SelectedNode.Parent.Index).ListOfFloors.ElementAt(ImpTreeView.SelectedNode.Index));
            if (ImpTreeView.SelectedNode.Level == 2)
                tempImp.ListOfSectors.ElementAt(ImpTreeView.SelectedNode.Parent.Parent.Index).ListOfFloors.ElementAt(ImpTreeView.SelectedNode.Parent.Index).ListOfRooms.Remove(tempImp.ListOfSectors.ElementAt(ImpTreeView.SelectedNode.Parent.Parent.Index).ListOfFloors.ElementAt(ImpTreeView.SelectedNode.Parent.Index).ListOfRooms.ElementAt(ImpTreeView.SelectedNode.Index));
            ImpTreeView.Nodes.Remove(ImpTreeView.SelectedNode);
        }

        private void Coefficient_ValueChanged(object sender, EventArgs e)
        {
            tempImp.Coefficent = System.Convert.ToInt32(Coefficient.Value);
            foreach (Sectors sect in tempImp.ListOfSectors)
            {
                foreach (Floors fl in sect.ListOfFloors)
                {
                    foreach (Rooms room in fl.ListOfRooms)
                    {
                        foreach (Installations inst in room.ListOfInstallations)
                        {
                            inst.Max = (Main.Stats.lambda * inst.Coefficient) / inst.Amperage;
                        }
                    }
                }
            }
        }

        private void Minimum_ValueChanged(object sender, EventArgs e)
        {
            double minValue = System.Convert.ToDouble(Minimum.Value);
            double maxValue = System.Convert.ToDouble(Maximum.Value);
            tempImp.MaxMeasured = maxValue;
            tempImp.MinMeasured = minValue;
            Random r = new Random();
            foreach (Sectors sect in tempImp.ListOfSectors)
            {
                foreach (Floors fl in sect.ListOfFloors)
                {
                    foreach (Rooms room in fl.ListOfRooms)
                    {
                        foreach (Installations inst in room.ListOfInstallations)
                        {
                            if (inst.Impedance > maxValue || inst.Impedance < minValue) inst.Impedance = r.NextDouble() * (maxValue - minValue) + minValue;

                        }
                    }
                }
            }
        }

        private void ControlDate_ValueChanged(object sender, EventArgs e)
        {
            tempImp.ControlDate = ImpedanceDate.Value;
        }

        private void NewActImp_Click(object sender, EventArgs e)
        {
            foreach (Control con in Impedance.Controls)
            {
                con.Enabled = true;
            }
            tempImp.ControlDate = ImpedanceDate.Value;
            currDateImp = tempImp.ControlDate;
            //currObj.El_2.Add(tempImp);
            SwitchVisImp();
            isNewImp = true;
        }
        private void EditActImp_Click(object sender, EventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            //var db = new NPLabDbContext();
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            foreach (Control con in Impedance.Controls)
            {
                con.Enabled = true;
            }
            ChooseAct ch = new ChooseAct(currObj.El_2, this, objId);
            ch.Show();
            isNewImp = false;
        }

        private void SaveImp_Click(object sender, EventArgs e)
        {
            if (!isNewImp) SaveLoad.UpdateEl_2(tempImp, ImpedanceDate.Value, objId, db);
            else
            {
                if (currObj.El_2.Any<El_2>(p => p.ControlDate == IsolationDate.Value))
                {
                    MessageBox.Show("Вече съществува актуализация със същата дата!");
                    return;
                }
                SaveLoad.SaveEl_2(tempImp, objId, db);
                //currObj.El_1.Add(tempIsol);
            }
            foreach (Control con in Impedance.Controls)
            {
                if (con == NewActImp || con == EditActImp || con == SaveImp || con == BackImp || con == EngineerNameImp || con == ImpedanceDate)
                    con.Enabled = true;
                else con.Enabled = false;
            }
            //db.SaveChanges();
            SwitchVisImp();
            El_2 t = tempImp;
            tempImp = new El_2();
            foreach (Sectors sec in t.ListOfSectors)
            {
                Sectors tempS = new Sectors();
                tempS.SectorName = sec.SectorName;
                foreach (Floors f in sec.ListOfFloors)
                {
                    Floors tempF = new Floors();
                    tempF.NameFloor = f.NameFloor;
                    foreach (Rooms r in f.ListOfRooms)
                    {
                        Rooms tempR = new Rooms();
                        tempR.RoomName = r.RoomName;
                        foreach (Installations inst in r.ListOfInstallations)
                        {
                            Installations tempI = new Installations();
                            tempI.Amperage = inst.Amperage;
                            tempI.Coefficient = inst.Coefficient;
                            tempI.FollowsRequirements = inst.FollowsRequirements;
                            tempI.Impedance = inst.Impedance;
                            tempI.InstallationName = inst.InstallationName;
                            tempI.isAutomaticProtector = inst.isAutomaticProtector;
                            tempI.Item = inst.Item;
                            tempI.Max = inst.Max;
                            tempI.NumberOfInstallation = inst.NumberOfInstallation;
                            tempI.Ofazen = inst.Ofazen;
                            tempI.Reset = inst.Reset;
                            tempR.ListOfInstallations.Add(tempI);
                        }
                        tempF.ListOfRooms.Add(tempR);
                    }
                    tempS.ListOfFloors.Add(tempF);
                }
                tempImp.ListOfSectors.Add(tempS);
            }
            tempImp.Coefficent = t.Coefficent;
            tempImp.ControlDate = t.ControlDate;
            tempImp.isAuto = t.isAuto;
            tempImp.MaxMeasured = t.MaxMeasured;
            tempImp.MinMeasured = t.MinMeasured;
            tempImp.NameOfEngineer = t.NameOfEngineer;
            tempImp.ObjectName = t.ObjectName;
        }

        public void SwitchVisImp()
        {
            NewActImp.Visible = !NewActImp.Visible;
            EditActImp.Visible = !EditActImp.Visible;
            SaveImp.Visible = !SaveImp.Visible;
            BackImp.Visible = !BackImp.Visible;
        }

        public void BackImp_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            if (sender is ChooseAct) dr = System.Windows.Forms.DialogResult.Yes;
            else dr = MessageBox.Show("Сигурни ли сте, че искате да изтриете промените и да се върнете?", "Изтриване на промени", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No) return;
            foreach (Control con in Impedance.Controls)
            {
                if (con == NewActImp || con == EditActImp || con == SaveImp || con == BackImp || con == EngineerNameImp || con == ImpedanceDate)
                    con.Enabled = true;
                else con.Enabled = false;
            }
            SwitchVisImp();
            
        }
    }
}