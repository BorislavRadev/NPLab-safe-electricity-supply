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
        public EL_4 tempDef = new EL_4();
        public Rooms_El_4 refRoomDef = new Rooms_El_4(), typicalDefRoom = new Rooms_El_4();
        public bool DefSuccRoom = false, isNewDef = true;
        public DateTime currDateDef = DateTime.Now;
        
        private void NewRoomDef_Click(object sender, EventArgs e)
        {
            if (DefTreeView.SelectedNode == null)
            {
                MessageBox.Show("Моля изберете етаж!");
                return;
            }
            if (DefTreeView.SelectedNode.Level != 1)
            {
                MessageBox.Show("Моля изберете етаж!");
                return;
            }
            DefRoom f = new DefRoom(this, refRoomDef, tempDef);
            f.Show();
        }

        private void NewSectorDef_Click(object sender, EventArgs e)
        {
            Sectors_El_4 sec = new Sectors_El_4
            {
                Id = tempDef.ListOfSectors.Count,
                SectorName = "(Нов сектор)"
            };
            tempDef.ListOfSectors.Add(sec);
            TreeNode treeNode = new TreeNode("(Нов сектор)");
            DefTreeView.Nodes.Add(treeNode);
            treeNode.BeginEdit();
            
        }

        private void DefTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true;
                e.Node.BeginEdit();
            }
            else
            {
                if (e.Node.Level == 0)
                    tempDef.ListOfSectors.ElementAt(e.Node.Index).SectorName = e.Label;
                if (e.Node.Level == 1)
                    tempDef.ListOfSectors.ElementAt(e.Node.Parent.Index).ListOfFloors.ElementAt(e.Node.Index).NameFloor = e.Label;
                if (e.Node.Level == 2)
                    tempDef.ListOfSectors.ElementAt(e.Node.Parent.Parent.Index).ListOfFloors.ElementAt(e.Node.Parent.Index).ListOfRooms.ElementAt(e.Node.Index).RoomName = e.Label;
                e.Node.EndEdit(false);

            }
        }

        private void NewFloorDef_Click(object sender, EventArgs e)
        {
            if (DefTreeView.SelectedNode == null)
            {
                MessageBox.Show("Моля изберете сектор!");
                return;
            }
            if (DefTreeView.SelectedNode.Level != 0)
            {
                MessageBox.Show("Моля изберете сектор!");
                return;
            }
            Floors_El_4 fl = new Floors_El_4
            {
                Id = tempDef.ListOfSectors.Count,
                NameFloor = "(Нов етаж)"
            };
            tempDef.ListOfSectors.ElementAt(DefTreeView.SelectedNode.Index).ListOfFloors.Add(fl);
            TreeNode treeNode = new TreeNode("Нов етаж");
            DefTreeView.SelectedNode.Nodes.Add(treeNode);
            DefTreeView.SelectedNode = treeNode;
            treeNode.BeginEdit();
        }

        public void DefRoom_FormClosing(object sender, EventArgs e)
        {
            string toAdd;
            if (!DefSuccRoom) return;
            tempDef.ListOfSectors.ElementAt(DefTreeView.SelectedNode.Parent.Index).
                ListOfFloors.ElementAt(DefTreeView.SelectedNode.Index).
                ListOfRooms.Add(refRoomDef);
            toAdd = tempDef.ListOfSectors.ElementAt(DefTreeView.SelectedNode.Parent.Index).ListOfFloors.ElementAt(DefTreeView.SelectedNode.Index).ListOfRooms.ElementAt(tempDef.ListOfSectors.ElementAt(DefTreeView.SelectedNode.Parent.Index).ListOfFloors.ElementAt(DefTreeView.SelectedNode.Index).ListOfRooms.Count - 1).RoomName;
            TreeNode NodeToAdd = new TreeNode(toAdd);
            DefTreeView.SelectedNode.Nodes.Add(NodeToAdd);
            DefTreeView.SelectedNode = NodeToAdd;
        }

        private void DeleteDef_Click(object sender, EventArgs e)
        {
            if (DefTreeView.SelectedNode == null) return;
            if (DefTreeView.SelectedNode.Level == 0)
                tempDef.ListOfSectors.Remove(tempDef.ListOfSectors.ElementAt(DefTreeView.SelectedNode.Index));
            if (DefTreeView.SelectedNode.Level == 1)
                tempDef.ListOfSectors.ElementAt(DefTreeView.SelectedNode.Parent.Index).ListOfFloors.Remove(tempDef.ListOfSectors.ElementAt(DefTreeView.SelectedNode.Parent.Index).ListOfFloors.ElementAt(DefTreeView.SelectedNode.Index));
            if (DefTreeView.SelectedNode.Level == 2)
                tempDef.ListOfSectors.ElementAt(DefTreeView.SelectedNode.Parent.Parent.Index).ListOfFloors.ElementAt(DefTreeView.SelectedNode.Parent.Index).ListOfRooms.Remove(tempDef.ListOfSectors.ElementAt(DefTreeView.SelectedNode.Parent.Parent.Index).ListOfFloors.ElementAt(DefTreeView.SelectedNode.Parent.Index).ListOfRooms.ElementAt(DefTreeView.SelectedNode.Index));
            DefTreeView.Nodes.Remove(DefTreeView.SelectedNode);
        }

        private void DefDate_ValueChanged(object sender, EventArgs e)
        {
            tempDef.Date = DefectiveProtectionDate.Value;
        }


        private void NewActDef_Click(object sender, EventArgs e)
        {
            foreach (Control con in DefectiveProtection.Controls)
            {
                con.Enabled = true;
            }
            tempDef.Date = DefectiveProtectionDate.Value;
            currDateDef = tempDef.Date;
            //currObj.El_4.Add(tempDef);
            SwitchVisDef();
            isNewDef = true;
        }

        private void EditActDef_Click(object sender, EventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NPLabDbContext, Configuration>());
            //var db = new NPLabDbContext();
            currObj = (from p in db.Object
                       where p.Id == objId
                       select p).FirstOrDefault();
            foreach (Control con in DefectiveProtection.Controls)
            {
                con.Enabled = true;
            }
            ChooseAct ch = new ChooseAct(currObj.El_4, this, objId);
            ch.Show();
            isNewDef = false;
        }

        private void SaveDef_Click(object sender, EventArgs e)
        {
            if (!isNewDef) SaveLoad.UpdateEl_4(tempDef, DefectiveProtectionDate.Value, objId, db);
            else
            {
                if (currObj.El_4.Any<EL_4>(p => p.Date == DefectiveProtectionDate.Value))
                {
                    MessageBox.Show("Вече съществува актуализация със същата дата!");
                    return;
                }
                SaveLoad.SaveEl_4(tempDef, objId, db);
                //currObj.El_1.Add(tempIsol);
            }
            foreach (Control con in DefectiveProtection.Controls)
            {
                if (con == NewActDef || con == EditActDef || con == SaveDef || con == BackDef || con == EngineerNameDef || con == DefectiveProtectionDate)
                    con.Enabled = true;
                else con.Enabled = false;
            }
            //db.SaveChanges();
            SwitchVisDef();
            //EL_4 t = tempDef;
            //tempDef = new EL_4();
            //foreach (Sectors_El_4 sec in t.ListOfSectors)
            //{
            //    Sectors_El_4 tempS = new Sectors_El_4();
            //    tempS.SectorName = sec.SectorName;
            //    foreach (Floors_El_4 f in sec.ListOfFloors)
            //    {
            //        Floors_El_4 tempF = new Floors_El_4();
            //        tempF.NameFloor = f.NameFloor;
            //        foreach (Rooms_El_4 r in f.ListOfRooms)
            //        {
            //            Rooms_El_4 tempR = new Rooms_El_4();
            //            tempR.RoomName = r.RoomName;
            //            foreach (Installation_El_4 inst in r.ListOfInstallations)
            //            {
            //                Installation_El_4 tempI = new Installation_El_4();
            //                tempI.Date = inst.Date;
            //                tempI.DN = inst.DN;
            //                tempI.DT = inst.DT;
            //                tempI.FollowsRequirements = inst.FollowsRequirements;
            //                tempI.InstallationName = inst.InstallationName;
            //                tempI.MaxDN = inst.MaxDN;
            //                tempI.MaxDT = inst.MaxDT; 
            //                tempI.NumberIn = inst.NumberIn;
            //                tempR.ListOfInstallations.Add(tempI);
            //            }
            //            tempF.ListOfRooms.Add(tempR);
            //        }
            //        tempS.ListOfFloors.Add(tempF);
            //    }
            //    tempDef.ListOfSectors.Add(tempS);
            //}
            //tempDef.Date = tempDef.Date;
            //tempDef.DNHighest = tempDef.DNHighest;
            //tempDef.DNLowest = tempDef.DNLowest;
            //tempDef.DTHighest = tempDef.DTHighest;
            //tempDef.DTLowest = tempDef.DTLowest;
            //tempDef.maxDN = tempDef.maxDN;
            //tempDef.maxDT = tempDef.maxDT;
            //tempDef.NameOfEngineer = tempDef.NameOfEngineer;
        }
        public void SwitchVisDef()
        {
            NewActDef.Visible = !NewActDef.Visible;
            EditActDef.Visible = !EditActDef.Visible;
            SaveDef.Visible = !SaveDef.Visible;
            BackDef.Visible = !BackDef.Visible;
        }

        public void BackDef_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            if (sender is ChooseAct) dr = System.Windows.Forms.DialogResult.Yes;
            else dr = MessageBox.Show("Сигурни ли сте, че искате да изтриете промените и да се върнете?", "Изтриване на промени", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No) return;
            foreach (Control con in DefectiveProtection.Controls)
            {
                if (con == NewActDef || con == EditActDef || con == SaveDef || con == BackDef || con == EngineerNameDef || con == DefectiveProtectionDate)
                    con.Enabled = true;
                else con.Enabled = false;
            }
            SwitchVisDef();
        }
    }
}