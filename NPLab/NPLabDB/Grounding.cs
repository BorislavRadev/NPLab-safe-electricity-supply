
namespace NPLab.Models
{
    public class Grounding
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool WetSeason { get; set; }

        public double AuxiliaryGrounding { get; set; }

        public double Probe { get; set; }

        public double Measured { get; set; }

        public double Adjusted { get; set; }

        public int EL_3Id { get; set; }

        public virtual EL_3 El_3 { get; set; }
    }
}
