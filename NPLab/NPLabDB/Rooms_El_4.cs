using System;
using System.Collections.Generic;

namespace NPLab.Models
{
    public class Rooms_El_4
    {
        private ICollection<Installation_El_4> InstallationsOfEl4;

        public Rooms_El_4()
        {
            this.InstallationsOfEl4 = new HashSet<Installation_El_4>();
        }

        public int Id { get; set; }

        public string RoomName { get; set; }

        public ICollection<Installation_El_4> ListOfInstallation_El_4
        {
            get { return this.InstallationsOfEl4; }
            set { this.InstallationsOfEl4 = value; }
        }

        public int Floors_El_4Id { get; set; }

        public virtual Floors_El_4 Floors_El_4 { get; set; }
    }
}
