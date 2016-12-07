using System;
using System.Collections.Generic;

namespace NPLab.Models
{
    public class EL_3m
    {
        private ICollection<LightningGrounding> LightningGroundings;

        public EL_3m()
        {
            this.LightningGroundings = new HashSet<LightningGrounding>();
        }

        public int Id { get; set; }

        public string NameOfEngineer { get; set; }

        public DateTime Date { get; set; }

        public double Max { get; set; }

        public virtual ICollection<LightningGrounding> ListOfGroundings_El_3m
        {
            get { return this.LightningGroundings; }
            set { this.LightningGroundings = value; }
        }

        public int ObjectsId { get; set; }

        public virtual Objects Object { get; set; }
    }
}
