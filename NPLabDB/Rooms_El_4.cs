using System;
using System.Collections.Generic;
using System.Linq;

namespace NPLab.Models
{
    public class Rooms_El_4
    {
        private ICollection<Installation_El_4> InstallationsOfEl4;

        public Rooms_El_4()
        {
            this.InstallationsOfEl4 = new HashSet<Installation_El_4>();
        }

        public int Id { get; set; }

        public string RoomName { get; set; }

        public ICollection<Installation_El_4> ListOfInstallations
        {
            get { return this.InstallationsOfEl4; }
            set { this.InstallationsOfEl4 = value; }
        }

        public int Floors_El_4Id { get; set; }

        public virtual Floors_El_4 Floors_El_4 { get; set; }

        public Installation_El_4 LastOfType(int type)
        {
            return this.ListOfInstallations.LastOrDefault<Installation_El_4>(inst => inst.Item.Name == Objects.Stats.Items[type].Name);
        }

        public Installation_El_4 FirstOfType(int type)
        {
            return this.ListOfInstallations.FirstOrDefault<Installation_El_4>(inst => inst.Item.Name == Objects.Stats.Items[type].Name);
        }

        public int FirstIndex(int type)
        {
            List<Installation_El_4> TempList = new List<Installation_El_4>();
            TempList.AddRange(this.ListOfInstallations);
            return TempList.IndexOf(FirstOfType(type));
        }

        public int LastIndex(int type)
        {
            List<Installation_El_4> TempList = new List<Installation_El_4>();
            TempList.AddRange(this.ListOfInstallations);
            return TempList.LastIndexOf(LastOfType(type));
        }

        public int CountOfType(int type)
        {
            return FirstIndex(type) - LastIndex(type);
        }

        public void Insert(int index, Installation_El_4 inst)
        {
            List<Installation_El_4> TempList = new List<Installation_El_4>();
            TempList.AddRange(this.ListOfInstallations);
            TempList.Insert(index, inst);
            this.ListOfInstallations = new List<Installation_El_4>();
            foreach (Installation_El_4 instal in TempList) this.ListOfInstallations.Add(instal);
        }

        public void RemoveAt(int index)
        {
            List<Installation_El_4> TempList = new List<Installation_El_4>();
            TempList.AddRange(this.ListOfInstallations);
            TempList.RemoveAt(index);
            this.ListOfInstallations = new List<Installation_El_4>();
            foreach (Installation_El_4 instal in TempList) this.ListOfInstallations.Add(instal);
        }

        public void RemoveType(int type)
        {
            List<Installation_El_4> TempList = new List<Installation_El_4>();
            TempList.AddRange(this.ListOfInstallations);
            for (int i = this.FirstIndex(type); i <= this.LastIndex(type); i++) TempList.RemoveAt(i);
            this.ListOfInstallations = new List<Installation_El_4>();
            foreach (Installation_El_4 instal in TempList) this.ListOfInstallations.Add(instal);
        }
    }
}
