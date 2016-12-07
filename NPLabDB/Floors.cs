
using System.Collections.Generic;
namespace NPLab.Models
{
    public class Floors
    {
        private ICollection<Rooms> rooms;

        public Floors()
        {
            this.rooms = new HashSet<Rooms>();
        }

        public int Id { get; set; }

        public double Level { get; set; }

        public string NameFloor { get; set; }

        public ICollection<Rooms> ListOfRooms
        {
            get { return this.rooms; }
            set { this.rooms = value; }
        }
        
        public int SectorsId { get; set; }

        public virtual Sectors Sectors { get; set; }
    }
}
