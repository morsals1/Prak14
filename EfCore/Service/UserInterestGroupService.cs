using EfCore.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace EfCore.Service
{
    public class UserInterestGroupService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;

        public ObservableCollection<UserInterestGroup> UserInterestGroups { get; set; } = new();

        public int Commit() => _db.SaveChanges();

        public void Add(UserInterestGroup userInterestGroup)
        {
            var _userInterestGroup = new UserInterestGroup
            {
                UserId = userInterestGroup.UserId,
                InterestGroupId = userInterestGroup.InterestGroupId,
                JoinedAt = userInterestGroup.JoinedAt,
                IsModerator = userInterestGroup.IsModerator
            };

            _db.Add(_userInterestGroup);
            Commit();
            GetAll();
        }

        public void GetAll()
        {
            var userInterestGroups = _db.UserInterestGroups
                .Include(uig => uig.User)
                .Include(uig => uig.InterestGroup)
                .ToList();

            UserInterestGroups.Clear();
            foreach (var uig in userInterestGroups)
            {
                UserInterestGroups.Add(uig);
            }
        }

        public void Remove(UserInterestGroup userInterestGroup)
        {
            var existing = _db.UserInterestGroups
                .FirstOrDefault(uig => uig.UserId == userInterestGroup.UserId &&
                                      uig.InterestGroupId == userInterestGroup.InterestGroupId);

            if (existing != null)
            {
                _db.Remove(existing);
                if (Commit() > 0)
                {
                    GetAll();
                }
            }
        }

        public UserInterestGroupService()
        {
            GetAll();
        }
    }
}