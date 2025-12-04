using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.data
{
    public class UserInterestGroup : ObservableObject
    {
        private int _userId;
        private int _interestGroupId;
        private string _joinedAt;
        private bool _isModerator;
        private InterestGroup _interestGroup;
        private User _user;

        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        public int InterestGroupId
        {
            get => _interestGroupId;
            set => SetProperty(ref _interestGroupId, value);
        }

        public string JoinedAt
        {
            get => _joinedAt;
            set => SetProperty(ref _joinedAt, value);
        }

        public bool IsModerator
        {
            get => _isModerator;
            set => SetProperty(ref _isModerator, value);
        }

        public InterestGroup InterestGroup
        {
            get => _interestGroup;
            set => SetProperty(ref _interestGroup, value);
        }

        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
    }
}
