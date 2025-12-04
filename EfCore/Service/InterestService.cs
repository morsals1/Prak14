using EfCore.data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace EfCore.Service
{
    public class InterestService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;

        public ObservableCollection<InterestGroup> InterestGroups { get; set; } = new();

        public int Commit() => _db.SaveChanges();

        public void Add(InterestGroup interestGroup)
        {
            var exists = _db.InterestGroups
                .Any(g => g.Title.ToLower() == interestGroup.Title.Trim().ToLower());

            if (exists)
            {
                throw new Exception("Группа с таким названием уже существует");
            }

            var _interestGroup = new InterestGroup
            {
                Title = interestGroup.Title.Trim(),
                Description = interestGroup.Description?.Trim()
            };

            _db.Add(_interestGroup);
            Commit();
            GetAll();
        }

        public void GetAll()
        {
            var interestGroups = _db.InterestGroups
                .Include(ig => ig.UserInterestGroups)
                .OrderBy(g => g.Title)
                .ToList();

            InterestGroups.Clear();
            foreach (var ig in interestGroups)
            {
                InterestGroups.Add(ig);
            }
        }

        public void Update(InterestGroup interestGroup)
        {
            var existing = _db.InterestGroups.Find(interestGroup.Id);
            if (existing != null)
            {
                var nameExists = _db.InterestGroups
                    .Any(g => g.Id != interestGroup.Id &&
                             g.Title.ToLower() == interestGroup.Title.Trim().ToLower());

                if (nameExists)
                {
                    throw new Exception("Группа с таким названием уже существует");
                }

                existing.Title = interestGroup.Title.Trim();
                existing.Description = interestGroup.Description?.Trim();

                _db.Update(existing);
                Commit();
                GetAll();
            }
        }

        public void Remove(InterestGroup interestGroup)
        {
            var existing = _db.InterestGroups.Find(interestGroup.Id);
            if (existing != null)
            {
                var hasUsers = _db.UserInterestGroups
                    .Any(uig => uig.InterestGroupId == interestGroup.Id);

                if (hasUsers)
                {
                    throw new Exception("Нельзя удалить группу, в которой есть участники");
                }

                _db.Remove(existing);
                if (Commit() > 0)
                {
                    GetAll();
                }
            }
        }

        public InterestService()
        {
            GetAll();
        }
    }
}