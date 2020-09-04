using ezGift.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezGift.Models
{
    public class GiftListItem
    {
        [Display(Name = "Gift ID")]
        public int GiftId { get; set; }

        [Display(Name = "Event ID")]
        public int RegistryEventId { get; set; }

        [Display(Name = "Event")]
        public virtual RegistryEvent RegistryEvent { get; set; }

        [Display(Name = "Gift Name")]
        public string GiftName { get; set; }

        [Display(Name = "Gift Description")]
        public string GiftDescription { get; set; }

    }
}
