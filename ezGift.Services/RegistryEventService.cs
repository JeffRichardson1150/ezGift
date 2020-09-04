using ezGift.Data;
using ezGift.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ezGift.Services
{
    public class RegistryEventService
    {
        private readonly Guid _userId;
        private readonly int? _thisUserProfileId;

        public RegistryEventService(Guid userid)
        {
            _userId = userid;
            _thisUserProfileId = GetUserProfileId(_userId);

        }

        public bool CheckForUserProfile()
        {
            if (_thisUserProfileId == null)
            {
                return false;
            }
            return true;
        }
        public bool CreateRegistryEvent(RegistryEventCreate model)
        {
            var entity =
                new RegistryEvent()
                {
                    UserProfileId = (int)_thisUserProfileId,
                    RegistryEventTitle = model.RegistryEventTitle,
                    RegistryEventDescription = model.RegistryEventDescription,
                    EventLocation = model.EventLocation,
                    EventDate = model.EventDate
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.RegistryEvents.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<RegistryEventListItem> GetRegistryEvents()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .RegistryEvents
                    .Where(e => e.UserProfileId == _thisUserProfileId)
                    .Select(
                        e =>
                            new RegistryEventListItem
                            {

                                RegistryEventId = e.RegistryEventId,
                                UserProfileId = e.UserProfileId,
                                UserProfile = e.UserProfile,
                                RegistryEventTitle = e.RegistryEventTitle,
                                RegistryEventDescription = e.RegistryEventDescription,
                                EventLocation = e.EventLocation,
                                EventDate = e.EventDate
                            }
                        );
                return query.ToArray();
            }
        }

        public IEnumerable<RegistryEventListItem> GetRegistryEventsForMyUserProfileId()
        {
            int? ThisUserProfileId = GetUserProfileId(_userId);
            if (ThisUserProfileId == null)
            {
                // error processing : return error that this Account has no Profile
                // present the UserProfileCreate
                return null;
            }
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .RegistryEvents
                    .Where(e => e.UserProfileId == ThisUserProfileId)
                    .Select(
                        e =>
                            new RegistryEventListItem
                            {

                                RegistryEventId = e.RegistryEventId,
                                UserProfileId = e.UserProfileId,
                                UserProfile = e.UserProfile,
                                RegistryEventTitle = e.RegistryEventTitle,
                                RegistryEventDescription = e.RegistryEventDescription,
                                EventLocation = e.EventLocation,
                                EventDate = e.EventDate
                            }
                        );
                return query.ToArray();
            }
        }


        public RegistryEventDetail GetRegistryEventById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .RegistryEvents
                        .Single(e => e.RegistryEventId == id && e.UserProfileId == _thisUserProfileId);
                return
                    new RegistryEventDetail
                    {
                        RegistryEventId = entity.RegistryEventId,
                        UserProfileId = entity.UserProfileId,
                        UserProfile = entity.UserProfile,
                        RegistryEventTitle = entity.RegistryEventTitle,
                        RegistryEventDescription = entity.RegistryEventDescription,
                        EventLocation = entity.EventLocation,
                        EventDate = entity.EventDate
                    };
            }
        }


            // I want to ensure that only Accounts who have Profiles can create events
            // Therefore, I don't use Guid OwnerId in the event
            // I use the UserProfileId. If no UserProfileId, no event can be created
            public int? GetUserProfileId(Guid id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                            .UserProfiles
                            .FirstOrDefault(e => e.OwnerId == id);
                    if (entity != null)
                    {

                        return entity.UserProfileId;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
        public bool UpdateRegistryEvent(RegistryEventEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .RegistryEvents
                        .Single(e => e.RegistryEventId == model.RegistryEventId && e.UserProfileId == _thisUserProfileId);

                entity.UserProfileId = model.UserProfileId;
                entity.UserProfile = model.UserProfile;
                entity.RegistryEventId = model.RegistryEventId;
                entity.RegistryEventTitle = model.RegistryEventTitle;
                entity.RegistryEventDescription = model.RegistryEventDescription;
                entity.EventLocation = model.EventLocation;
                entity.EventDate = model.EventDate;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteRegistryEvent(int registryEventId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .RegistryEvents
                        .Single(e => e.RegistryEventId == registryEventId);

                ctx.RegistryEvents.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}
