using System;
using System.Collections.Generic;

namespace NPLab.Models
{
    public class Floors_El_4
    {
        private ICollection<Rooms_El_4> RoomsOfEl4;

        public Floors_El_4()
        {
            this.RoomsOfEl4 = new HashSet<Rooms_El_4>();
        }

        public int Id { get; set; }

        public double Level { get; set; }

        public string NameFloor { get; set; }

        public ICollection<Rooms_El_4> ListOfRooms
        {
            get { return this.RoomsOfEl4; }
            set { this.RoomsOfEl4 = value; }
        }

        public int SectorsId { get; set; }

        public virtual Sectors_El_4 Sectors { get; set; }
    }
}
