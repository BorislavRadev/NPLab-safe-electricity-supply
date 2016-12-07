using System;
using System.Collections.Generic;

namespace NPLab.Models
{
    public class Installation_El_4
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string InstallationName { get; set; }

        public virtual InstallationItem Item { get; set; }
        
        public int NumberIn { get; set; }
        
        public int TypeProtector { get; set; }

        public double MaxDT { get; set; }
        
        public double MaxDN { get; set; }
        
        public double DT { get; set; }
        
        public double DN { get; set; }

        public bool FollowsRequirements { get; set; }

        public int Rooms_El_4Id { get; set; }

        public virtual Rooms_El_4 Rooms_El_4 { get; set; }
    }
}
