
using System;
using System.Collections.Generic;
namespace NPLab.Models
{
    public class El_2
    {
        private ICollection<Sectors> sectors;

        public El_2()
        {
            this.sectors = new HashSet<Sectors>();
        }

        public int Id { get; set; }

        public string NameOfEngineer { get; set; }

        public DateTime Time { get; set; }

        public string ObjectName { get; set; }

        public virtual ICollection<Sectors> ListOfSectors 
        {
            get { return this.sectors; }
            set {this.sectors = value; }
        }

        public int ObjectsId { get; set; }

        public virtual Objects Object { get; set; }
    }
}
