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


        public string NameFloor_El_4 { get; set; }

        public ICollection<Rooms_El_4> ListOfRooms_El_4
        {
            get { return this.RoomsOfEl4; }
            set { this.RoomsOfEl4 = value; }
        }

        public int Sectors_El_4Id { get; set; }

        public virtual Sectors_El_4 Sectors_El_4 { get; set; }
    }
}
