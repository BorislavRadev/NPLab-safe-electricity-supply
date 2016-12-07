
namespace NPLab.Models
{
    public class Cabel
    {
        public int Id { get; set; }

        public int ConductorsCount { get; set; }

        public int MinMeasured { get; set; } 

        public int MaxMeasured { get; set; } 

        public bool Monophase { get; set; } 

        public bool Grounded { get; set; }

        public string Name { get; set; }

        public string CabelType { get; set; }

        public string CabelModel { get; set; }

        public double Thickness { get; set; }

        public int EL_1Id { get; set; }

        public virtual EL_1 El_1 { get; set; }
    }
}
