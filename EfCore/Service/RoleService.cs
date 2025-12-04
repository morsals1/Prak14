using EfCore.data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace EfCore.Service
{
    public class RoleService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public ObservableCollection<data.Role> Roles { get; set; } = new();

        public RoleService()
        {
            GetAll();
        }

        public void GetAll()
        {
            var roles = _db.Roles
                .Include(r => r.Users)
                .ToList();

            Roles.Clear();
            foreach (var role in roles)
            {
                Roles.Add(role);
            }
        }
    }
}