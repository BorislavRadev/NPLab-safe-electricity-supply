
using System.Collections.Generic;
namespace NPLab.Models
{
    public class Sectors
    {
        private ICollection<Floors> floors;

        public Sectors()
        {
            this.floors = new HashSet<Floors>();
        }

        
        public int Id { get; set; }

        public string SectorName { get; set; }

        public ICollection<Floors> ListOfFloors
        {
            get { return this.floors; }
            set { this.floors = value; }
        }

        public int El_2Id { get; set; }

        public virtual El_2 Object { get; set; }
    }
}
