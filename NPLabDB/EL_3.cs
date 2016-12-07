using System;
using System.Collections.Generic;

namespace NPLab.Models
{
    public class EL_3
    {
        private ICollection<Grounding> groundings;

        public EL_3()
        {
            this.Max = 30;
            this.groundings = new HashSet<Grounding>();
        }

        public bool WetSeason { get; set; }

        public int Id { get; set; }

        public string NameOfEngineer { get; set; }

        public DateTime Date { get; set; }

        public double Max { get; set; }

        public virtual ICollection<Grounding> ListOfGroundings
        {
            get { return this.groundings; }
            set { this.groundings = value; }
        }

        public int ObjectsId { get; set; }

        public virtual Objects Object { get; set; }
    }
}
