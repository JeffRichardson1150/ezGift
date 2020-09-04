using ezGift.Data;
using ezGift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezGift.Services
{
    public class GiftService
    {
        //private readonly int? _eventId;
        private readonly int _eventId;

        public GiftService()
        {

        }
        //public GiftService(int? eventId)
        public GiftService(int eventId)
        {
            _eventId = eventId;

        }
        public IEnumerable<GiftListItem> GetGiftsByEvent()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Gifts
                    .Where(e => e.RegistryEvent.RegistryEventId == _eventId)
                    .Select(
                        e =>
                            new GiftListItem
                            {
                                GiftId = e.GiftId,
                                GiftName = e.GiftName,
                                GiftDescription = e.GiftDescription,
                                RegistryEventId = e.RegistryEventId,
                                RegistryEvent = e.RegistryEvent
                            }
                        );
                return query.ToArray();
            }
        }
        public bool CreateGift(GiftCreate model)
        {
            var entity =
                new Gift()
                {
                    GiftName = model.GiftName,
                    GiftDescription = model.GiftDescription,
                    RegistryEventId = model.RegistryEventId,
                    //RegistryEvent = model.RegistryEvent
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Gifts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public GiftDetail GetGiftById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Gifts
                        .Single(e => e.GiftId == id);
                return
                    new GiftDetail
                    {
                        GiftId = entity.GiftId,
                        GiftName = entity.GiftName,
                        GiftDescription = entity.GiftDescription,
                        RegistryEventId = entity.RegistryEventId,
                        RegistryEvent = entity.RegistryEvent
                    };
            }
        }

        public bool UpdateGift(GiftEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Gifts
                        .Single(e => e.GiftId == model.GiftId);

                entity.GiftId = model.GiftId;
                entity.GiftName = model.GiftName;
                entity.GiftDescription = model.GiftDescription;
                entity.RegistryEventId = model.RegistryEventId;
                entity.RegistryEvent = model.RegistryEvent;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteGift(int giftId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Gifts
                        .Single(e => e.GiftId == giftId);

                ctx.Gifts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        //public IEnumerable<GiftListItem> GetGifts()
        //{
        //    using (var ctx = new ApplicationDbContext())
        //    {
        //        var query =
        //            ctx
        //            .Gifts
        //            .Where(e => e.UserProfileId == _thisUserProfileId)
        //            .Select(
        //                e =>
        //                    new GiftListItem
        //                    {

        //                        GiftId = e.GiftId,
        //                        UserProfileId = e.UserProfileId,
        //                        UserProfile = e.UserProfile,
        //                        GiftTitle = e.GiftTitle,
        //                        GiftDescription = e.GiftDescription,
        //                        EventLocation = e.EventLocation,
        //                        EventDate = e.EventDate
        //                    }
        //                );
        //        return query.ToArray();
        //    }
        //}


    }
}
