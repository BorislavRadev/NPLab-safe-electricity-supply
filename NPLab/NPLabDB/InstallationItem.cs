using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPLab.Models
{
    public class InstallationItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual InstallationType Type { get; set; }
    }
}
