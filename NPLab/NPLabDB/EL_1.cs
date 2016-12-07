using System;
using System.Collections.Generic;

namespace NPLab.Models
{
    public class EL_1
    {
        private ICollection<Cabel> cabels;

        public EL_1()
        {
            this.Min = 0.5;
            this.cabels = new HashSet<Cabel>();
        }
        public int Id { get; set; }

        public string NameOfEngineer { get; set; }

        public DateTime Date { get; set; }

        public int SourceVoltage { get; set; }

        public double Min { get; set; }

        public virtual ICollection<Cabel> ListOfCabels 
        {
            get { return this.cabels; }
            set { this.cabels = value; }
        }

        public int ObjectsId { get; set; }

        public virtual Objects Object { get; set; }
    }
}
