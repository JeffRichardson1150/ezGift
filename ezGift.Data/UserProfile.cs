using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezGift.Data
{
    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;

            }
        }
        public string Address { get; set; }
        public string Email { get; set; }
        public Guid OwnerId { get; set; }
    }
}
