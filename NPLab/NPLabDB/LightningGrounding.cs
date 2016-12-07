
namespace NPLab.Models
{
    public class LightningGrounding
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public string Name { get; set; }

        public bool WetSeason { get; set; }

        public double AuxiliaryGrounding { get; set; }

        public double Probe { get; set; }

        public double Coef { get; set; }

        public double Measured { get; set; }

        public double Adjusted { get; set; }
        
        public int EL_3mId { get; set; }

        public virtual EL_3m El_3m { get; set; }
    }
}
