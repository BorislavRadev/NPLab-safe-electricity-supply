
namespace NPLab.Models
{
    public class Cabel
    {
        public int Id { get; set; }

        public int ConductorsCount { get; set; }

        public double Measured { get; set; }

        public string Name { get; set; }

        public string CabelType { get; set; }

        public string CabelModel { get; set; }

        public bool L1 { get; set; }

        public bool L2 { get; set; }

        public bool L3 { get; set; }

        public bool PE { get; set; }

        public bool N { get; set; }

        public bool PEN { get; set; }

        public double Thickness { get; set; }

        public int EL_1Id { get; set; }

        public virtual EL_1 El_1 { get; set; }
    }
}
