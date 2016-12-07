using System;
using System.Collections.Generic;
namespace NPLab.Models
{
    public class Sectors_El_4
    {
        private ICollection<Floors_El_4> FloorsOfEl4;

        public Sectors_El_4()
        {
            this.FloorsOfEl4 = new HashSet<Floors_El_4>();
        }
        public int Id { get; set; }

        public string SectorName { get; set; }

        public ICollection<Floors_El_4> ListOfFloors_El_4
        {
            get { return this.FloorsOfEl4; }
            set { this.FloorsOfEl4 = value; }
        }

        public int EL_4Id { get; set; }

        public virtual EL_4 El_4 { get; set; }
    }
}
