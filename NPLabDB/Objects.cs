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
        public static StaticClass Stats = new StaticClass();
        public Objects()
        {
            this.el_1 = new HashSet<EL_1>();
            this.el_2 = new HashSet<El_2>();
            this.el_3 = new HashSet<EL_3>();
            this.el_4 = new HashSet<EL_4>();
            this.el_3m = new HashSet<EL_3m>();
        }
 
        public int Id { get; set; }

        public string Client { get; set; }

        public string ObjectName { get; set; }

        public string Inspector { get; set; }

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

        public static string toRoman(int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + toRoman(number - 1000);
            if (number >= 900) return "CM" + toRoman(number - 900); 
            if (number >= 500) return "D" + toRoman(number - 500);
            if (number >= 400) return "CD" + toRoman(number - 400);
            if (number >= 100) return "C" + toRoman(number - 100);
            if (number >= 90) return "XC" + toRoman(number - 90);
            if (number >= 50) return "L" + toRoman(number - 50);
            if (number >= 40) return "XL" + toRoman(number - 40);
            if (number >= 10) return "X" + toRoman(number - 10);
            if (number >= 9) return "IX" + toRoman(number - 9);
            if (number >= 5) return "V" + toRoman(number - 5);
            if (number >= 4) return "IV" + toRoman(number - 4);
            if (number >= 1) return "I" + toRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }
    }
}
