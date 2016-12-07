using System.Collections.Generic;
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
    }
}
