using System;
using System.Collections.Generic;

namespace NPLab.Models
{
    public class EL_4
    {
        private ICollection<Sectors_El_4> SectorsOfEl4;

        public EL_4()
        {
            this.SectorsOfEl4 = new HashSet<Sectors_El_4>();
        }

        public int Id { get; set; }

        public string NameOfEngineer { get; set; }

        public DateTime Date { get; set; }

        public double DTHighest { get; set; }
        
        public double DTLowest { get; set; }
        
        public double DNHighest { get; set; }
        
        public double DNLowest { get; set; }
        
        public double maxDT { get; set; }
        
        public double maxDN { get; set; }

        public virtual ICollection<Sectors_El_4> ListOfSectors
        {
            get { return this.SectorsOfEl4; }
            set { this.SectorsOfEl4 = value; }
        }

        public int ObjectsId { get; set; }

        public virtual Objects Object { get; set; }
    }
}
