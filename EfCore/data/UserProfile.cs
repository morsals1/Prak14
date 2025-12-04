using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.data
{
    public class UserProfile : ObservableObject
    {
        private int _id;
        private string _avatarUrl;
        private string _phone;
        private string _birthday;
        private string _bio;
        private User _user;
        private int _userId;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string AvatarUrl
        {
            get => _avatarUrl;
            set => SetProperty(ref _avatarUrl, value);
        }

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public string Birthday
        {
            get => _birthday;
            set => SetProperty(ref _birthday, value);
        }

        public string Bio
        {
            get => _bio;
            set => SetProperty(ref _bio, value);
        }

        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }
    }
}
