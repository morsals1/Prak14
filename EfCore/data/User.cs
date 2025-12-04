using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.data
{
    public class User : ObservableObject
    {
        private int _id;
        private string _login;
        private string _name;
        private string _email;
        private string _password;
        private string _createdAt;
        private UserProfile _profile;
        private int _roleId;
        private Role _role;

        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string CreatedAt
        {
            get => _createdAt;
            set => SetProperty(ref _createdAt, value);
        }

        public UserProfile Profile
        {
            get => _profile;
            set => SetProperty(ref _profile, value);
        }

        public int RoleId
        {
            get => _roleId;
            set => SetProperty(ref _roleId, value);
        }

        public Role Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }

        private ObservableCollection<UserInterestGroup> _userInterestGroups;
        public ObservableCollection<UserInterestGroup> UserInterestGroups
        {
            get => _userInterestGroups;
            set => SetProperty(ref _userInterestGroups, value);
        }
    }
}