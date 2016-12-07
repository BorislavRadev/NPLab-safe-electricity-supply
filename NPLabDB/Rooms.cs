using System.Collections.Generic;
using System.Linq;

namespace NPLab.Models
{
    public class Rooms
    {
        private ICollection<Installations> installations;

        public Rooms()
        {
            this.installations = new HashSet<Installations>();
        }

        public int Id { get; set; }

        public string RoomName { get; set; }

        public ICollection<Installations> ListOfInstallations
        {
            get { return this.installations; }
            set { this.installations = value; }
        }

        public int FloorsId { get; set; }

        public virtual Floors Floors { get; set; }

        public Installations LastOfType(int type)
        {
            return this.ListOfInstallations.LastOrDefault<Installations>(inst => inst.Item.Name == Objects.Stats.Items[type].Name);
        }

        public Installations FirstOfType(int type)
        {
            return this.ListOfInstallations.FirstOrDefault<Installations>(inst => inst.Item.Name == Objects.Stats.Items[type].Name);
        }

        public int FirstIndex(int type)
        {
            List<Installations> TempList = new List<Installations>();
            TempList.AddRange(this.ListOfInstallations);
            return TempList.IndexOf(FirstOfType(type));
        }

        public int LastIndex(int type)
        {
            List<Installations> TempList = new List<Installations>();
            TempList.AddRange(this.ListOfInstallations);
            return TempList.LastIndexOf(LastOfType(type));
        }

        public int CountOfType(int type)
        {
            return FirstIndex(type) - LastIndex(type);    
        }

        public void Insert(int index, Installations inst)
        {
            List<Installations> TempList = new List<Installations>();
            TempList.AddRange(this.ListOfInstallations);
            TempList.Insert(index, inst);
            this.ListOfInstallations = new List<Installations>();
            foreach (Installations instal in TempList) this.ListOfInstallations.Add(instal);
        }

        public void RemoveAt(int index)
        {
            List<Installations> TempList = new List<Installations>();
            TempList.AddRange(this.ListOfInstallations);
            TempList.RemoveAt(index);
            this.ListOfInstallations = new List<Installations>();
            foreach (Installations instal in TempList) this.ListOfInstallations.Add(instal);
        }

        public void RemoveType(int type)
        {
            List<Installations> TempList = new List<Installations>();
            TempList.AddRange(this.ListOfInstallations);
            for (int i = this.FirstIndex(type); i <= this.LastIndex(type); i++ ) TempList.RemoveAt(i);
            this.ListOfInstallations = new List<Installations>();
            foreach (Installations instal in TempList) this.ListOfInstallations.Add(instal);
        }
    }
}
