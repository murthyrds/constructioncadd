using OurProject.Entities;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using PagedList.Core.Mvc;
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
            return _context.Users;
        }       
    }
}
