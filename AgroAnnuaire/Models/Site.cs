using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroAnnuaire.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string Ville { get; set; } = null!;
        public string Type { get; set; } = null!;

        public virtual ICollection<Collaborateur> Collaborateurs { get; set; }
    }
}
