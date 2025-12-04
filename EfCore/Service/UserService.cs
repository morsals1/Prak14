using EfCore.data;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Service
{
    public class UserService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public UserService()
        {
            GetAll();
        }

        public void Add(User user)
        {
            var _user = new User
            {
                Login = user.Login,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = user.CreatedAt,
                RoleId = user.RoleId,
                Profile = new UserProfile
                {
                    AvatarUrl = user.Profile?.AvatarUrl ?? "",
                    Phone = user.Profile?.Phone ?? "",
                    Birthday = user.Profile?.Birthday ?? "",
                    Bio = user.Profile?.Bio ?? "",
                    UserId = 0
                }
            };

            _db.Add<User>(_user);
            Commit();
            Users.Add(_user);
        }

        public int Commit() => _db.SaveChanges();

        public void GetAll()
        {
            var users = _db.Users
                .Include(u => u.Profile)
                .Include(u => u.Role)
                .ToList();

            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        public void Remove(User user)
        {
            _db.Remove<User>(user);
            if (Commit() > 0)
            {
                if (Users.Contains(user))
                    Users.Remove(user);
            }
        }

        public void Update(User user)
        {
            Commit();
        }
    }
}