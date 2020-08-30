using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezGift.Data
{
    public class RegistryEvent
    {
        [Key]
        public int RegistryEventId { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        [ForeignKey(nameof(UserProfileId))]
        public virtual UserProfile UserProfile { get; set; }

        [Required]
        public string RegistryEventTitle { get; set; }

        [Required]
        public DateTimeOffset EventDate { get; set; }
    }
}
