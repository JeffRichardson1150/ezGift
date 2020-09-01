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
    public class RegistryEventDetail
    {
        [Display(Name = "Event ID")]
        public int RegistryEventId { get; set; }

        [Display(Name = "User Profile ID")]
        public int UserProfileId { get; set; }

        [ForeignKey(nameof(UserProfileId))]
        public virtual UserProfile UserProfile { get; set; }

        [Display(Name = "Event Title")]
        public string RegistryEventTitle { get; set; }

        [Display(Name = "Event Description")]
        public string RegistryEventDescription { get; set; }

        [Display(Name = "Event Location")]
        public string EventLocation { get; set; }

        [Display(Name = "Event Date")]
        public DateTimeOffset EventDate { get; set; }

    }
}
