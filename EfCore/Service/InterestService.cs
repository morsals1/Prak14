using EfCore.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Service
{
    public class InterestService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public ObservableCollection<InterestGroup> InterestGroups { get; set; } = new ObservableCollection<InterestGroup>();
        public int Commit() => _db.SaveChanges();

        public void Add(InterestGroup interestGroup)
        {
            var _interestGroup = new InterestGroup
            {
                Id = interestGroup.Id,
                Title = interestGroup.Title,
            };
            _db.Add<InterestGroup>(_interestGroup);
            Commit();
            InterestGroups.Add(_interestGroup);
        }

        public void GetAll()
        {
            var interestGroups = _db.InterestGroups.ToList();

            InterestGroups.Clear();
            foreach (var interestGroup in interestGroups)
            {
                InterestGroups.Add(interestGroup);
            }
        }

        public InterestService()
        {
            GetAll();
        }

        public void Remove(InterestGroup interestGroup)
        {
            _db.Remove<InterestGroup>(interestGroup);
            if (Commit() > 0)
                if (InterestGroups.Contains(interestGroup))
                    InterestGroups.Remove(interestGroup);
        }

        public void Update(InterestGroup interestGroup)
        {
            Commit();
        }
    }
}
