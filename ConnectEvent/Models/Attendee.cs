using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectEvent.Models
{
    public class Attendee
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Company")]
        public string Company { get; set; }

        [Display(Name = "Platform")]
        public string Platform { get; set; }

        public DateTime? ResponseDate { get; set; }

        public bool Attending { get; set; }

        public bool PresentAtEvent { get; set; }

    }
}
