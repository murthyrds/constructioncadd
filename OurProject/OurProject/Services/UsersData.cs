using OurProject.Entities;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using PagedList.Core.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OurProject.Services
{
    public interface IUserData
    {
        IEnumerable<User> GetAll();
        User Get(string id);
        void Commit();
    }
    public class SqlIUserData : IUserData
    {
        private OurProjectDbContext _context;

        public SqlIUserData(OurProjectDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public User Get(string id)
        {
            return _context.Users.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            var dropdrown = new SelectList(_context.Roles.Where(u => !u.Name.Contains(""))
                                            .ToList(), "Name", "Name");

            List<string> usrids = _context.UserRoles.Where(a => a.RoleId == "745e9fb4-8cfe-4338-8eb7-dd50495f5532")
                .Select(b => b.UserId).Distinct().ToList();
            List<User> listUsers = _context.Users.Where(a => usrids.Any(c => c == a.Id)).ToList();          
            return listUsers;
        }       
    }
}
