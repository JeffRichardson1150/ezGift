using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ezGift.WebMVC.Models
{
    public class UserProfileListItem
    {
        [Display(Name = "User Profile ID")]
        public int UserProfileId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}