using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroAnnuaire.Models
{
    public class Collaborateur
    {
        public int Id { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string MobilePhoneNumber { get; set; }
        public string Email { get; set; }
        public int ServiceId { get; set; }
        public int SiteId { get; set; }

        //public Service service { get; set; } = null!;
        //public Site site { get; set; } = null!;
        public virtual Service service { get; set; } = null!;
        public virtual Site site { get; set; } = null!;
    }
}
