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
        IEnumerable<User> GetDetailer();
        IEnumerable<User> GetClient();
        IEnumerable<User> GetVendor();        
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
            //Admin Details
            List<string> usrids = _context.UserRoles.Where(a => a.RoleId == "cb4417c7-c833-497c-b34d-9a561db2a706")
                .Select(b => b.UserId).Distinct().ToList();
            List<User> listUsers = _context.Users.Where(a => usrids.Any(c => c == a.Id)).ToList();          
            return listUsers;
        }

        public IEnumerable<User> GetDetailer()
        {
            var dropdrown = new SelectList(_context.Roles.Where(u => !u.Name.Contains(""))
                                            .ToList(), "Name", "Name");

            //Detailer Details.
            List<string> usrids = _context.UserRoles.Where(a => a.RoleId == "477a59e7-7282-40e7-9b92-611a4def68f1")
                .Select(b => b.UserId).Distinct().ToList();
            List<User> listUsers = _context.Users.Where(a => usrids.Any(c => c == a.Id)).ToList();
            return listUsers;
        }

        //Vendor Details
        public IEnumerable<User> GetVendor()
        {
            var dropdrown = new SelectList(_context.Roles.Where(u => !u.Name.Contains(""))
                                            .ToList(), "Name", "Name");

            //Vendor Details.
            List<string> usrids = _context.UserRoles.Where(a => a.RoleId == "385bfef5-a839-4390-95b1-2f754b77facf")
                .Select(b => b.UserId).Distinct().ToList();
            List<User> listUsers = _context.Users.Where(a => usrids.Any(c => c == a.Id)).ToList();
            return listUsers;
        }

        //Client Details
        public IEnumerable<User> GetClient()
        {
            var dropdrown = new SelectList(_context.Roles.Where(u => !u.Name.Contains(""))
                                            .ToList(), "Name", "Name");

            //Client Details.
            List<string> usrids = _context.UserRoles.Where(a => a.RoleId == "745e9fb4-8cfe-4338-8eb7-dd50495f5532")
                .Select(b => b.UserId).Distinct().ToList();
            List<User> listUsers = _context.Users.Where(a => usrids.Any(c => c == a.Id)).ToList();
            return listUsers;
        }
    }
}
