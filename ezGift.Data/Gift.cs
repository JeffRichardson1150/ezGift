using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezGift.Data
{
    public class Gift
    {
        [Key]
        public int GiftId { get; set; }

        [Required]
        public int RegistryEventId { get; set; }

        [ForeignKey(nameof(RegistryEventId))]
        public virtual RegistryEvent RegistryEvent { get; set; }

        [Required]
        public string GiftName { get; set; }

        public string GiftDescription { get; set; }
    }
}
