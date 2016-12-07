using System;
using System.Collections.Generic;

namespace NPLab.Models
{
    public class Objects
    {
        private ICollection<EL_1> el_1;
        private ICollection<El_2> el_2;
        private ICollection<EL_3> el_3;
        private ICollection<EL_4> el_4;
        private ICollection<EL_3m> el_3m;

        public Objects()
        {
            this.el_1 = new HashSet<EL_1>();
            this.el_2 = new HashSet<El_2>();
            this.el_3 = new HashSet<EL_3>();
            this.el_4 = new HashSet<EL_4>();
            this.el_3m = new HashSet<EL_3m>();
        }

        public int Id { get; set; }

        public string ClientFistName { get; set; }

        public string ClientLastName { get; set; }

        public virtual ICollection<EL_1> El_1
        {
            get { return this.el_1; }
            set { this.el_1 = value; }
        }

        public virtual ICollection<El_2> El_2
        {
            get { return this.el_2; }
            set { this.el_2 = value; }
        }

        public virtual ICollection<EL_3> El_3
        {
            get { return this.el_3; }
            set { this.el_3 = value; }
        }

        public virtual ICollection<EL_4> El_4
        {
            get { return this.el_4; }
            set { this.el_4 = value; }
        }

        public virtual ICollection<EL_3m> El_3m
        {
            get { return this.el_3m; }
            set { this.el_3m = value; }
        }

    }
}
