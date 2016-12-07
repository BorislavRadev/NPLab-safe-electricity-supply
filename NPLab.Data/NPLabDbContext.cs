
namespace NPLab.Data
{
    using System.Data.Entity;
    using NPLab.Models;

    public class NPLabDbContext : DbContext 
    {
        public NPLabDbContext()
            : base("NPLab")
        {
        }

        public virtual IDbSet<Engineers> Engineers { get; set; }

        public virtual IDbSet<Objects> Object { get; set; }
        
        public virtual IDbSet<EL_1> EL_1 { get; set; }

        public virtual IDbSet<Cabel> Cabel { get; set; }

        public virtual IDbSet<El_2> EL_2 { get; set; }

        public virtual IDbSet<Sectors> Sectors { get; set; }

        public virtual IDbSet<Floors> Floors { get; set; }

        public virtual IDbSet<Rooms> Rooms { get; set; }

        public virtual IDbSet<Installations> Instalations { get; set; }

        public virtual IDbSet<InstallationItem> InstalationItem { get; set; }

        public virtual IDbSet<EL_3> EL_3 { get; set; }

        public virtual IDbSet<Grounding> Grounding { get; set; }

        public virtual IDbSet<EL_4> EL_4 { get; set; }

        public virtual IDbSet<Sectors_El_4> Sectors_EL_4 { get; set; }

        public virtual IDbSet<Floors_El_4> Floors_EL_4 { get; set; }

        public virtual IDbSet<Rooms_El_4> Rooms_EL_4 { get; set; }

        public virtual IDbSet<Installation_El_4> Instalations_EL_4 { get; set; }

        public virtual IDbSet<EL_3m> EL_3m{ get; set; }

        public virtual IDbSet<LightningGrounding> LightningGrounding { get; set; }
    }
}
