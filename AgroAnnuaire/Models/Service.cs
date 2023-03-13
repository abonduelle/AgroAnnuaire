using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroAnnuaire.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Collaborateur> Collaborateurs { get; set; }
    }
}
