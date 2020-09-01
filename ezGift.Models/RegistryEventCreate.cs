using ezGift.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezGift.Models
{
    public class RegistryEventCreate
    {

        [Required]
        [Display(Name = "Event Title")]

        public string RegistryEventTitle { get; set; }

        [Required]
        [Display(Name = "Event Description")]
        public string RegistryEventDescription { get; set; }

        [Required]
        [Display(Name = "Event Location")]
        public string EventLocation { get; set; }

        [Required]
        [Display(Name = "Event Date")]

        public DateTimeOffset EventDate { get; set; }

    }
}
