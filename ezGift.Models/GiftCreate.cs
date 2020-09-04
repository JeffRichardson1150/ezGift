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
    public class GiftCreate
    {
        [Required]
        [Display(Name = "Gift Name")]
        public string GiftName { get; set; }

        [Display(Name = "Gift Description")]
        public string GiftDescription { get; set; }

        public int RegistryEventId { get; set; }
        //public virtual RegistryEvent RegistryEvent { get; set; }

    }
}
