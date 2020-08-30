using ezGift.Data;
using ezGift.Models;
using ezGift.WebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace ezGift.Services
{
    public class UserProfileService
    {
        private readonly Guid _userId;

        public UserProfileService(Guid userid)
        {
            _userId = userid;
        }

        public bool CreateUserProfile(UserProfileCreate model)
        {
            var entity =
                new UserProfile()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Email = model.Email,
                    OwnerId = model.OwnerId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.UserProfiles.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<UserProfileListItem> GetUserProfiles()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .UserProfiles
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                            new UserProfileListItem
                            {
                                UserProfileId = e.UserProfileId,
                                FirstName = e.FirstName,
                                LastName = e.LastName,
                                Email = e.Email
                            }
                        );
                return query.ToArray();
            }
        }
    }
}
