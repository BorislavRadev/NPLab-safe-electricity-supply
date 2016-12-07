using System;
namespace NPLab.Models
{
    public class Installations
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string InstallationName { get; set; }

        public virtual InstallationItem Item { get; set; }

        public int NumberOfinstallation { get; set; }
       
        public int? NumberOfProtector { get; set; }

        public int Coefficient { get; set; }

        public double Amperage { get; set; }

        public double Impedance { get; set; }

        public double Max { get; set; }

        public bool isAutomaticProtector { get; set; }

        public bool Ofazen { get; set; }

        public bool Reset { get; set; }

        public bool FollowsRequirements { get; set; }

        public int RoomsId { get; set; }

        public virtual Rooms Rooms { get; set; }
    }
}
