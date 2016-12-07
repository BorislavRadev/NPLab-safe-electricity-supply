using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPLab.Models
{
    public class Protectors
    {
        public int Number { get; set; }

        public string TypeProtector { get; set; }

        public string Name { get; set; }

        public double MaxDT { get; set; }

        public double MaxDN { get; set; }

        public double DT { get; set; }

        public double DN { get; set; }

        public bool FollowsRequirements { get; set; }

        
    }
}
