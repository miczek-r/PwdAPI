using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ZarzadzanieDomem.IRepositories;
using ZarzadzanieDomem.Models;
using ZarzadzanieDomem.Models.Context;

namespace ZarzadzanieDomem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public void AddUser(User value)
        {
            _context.Users.Add(value);
        }
        public void Update(User value)
        {
            _context.Users.Update(value);
        }
        public void Delete(int id)
        {
            User user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                return;
            }
            else
            {
                return; 
            }

        }
        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
            
        }
    }
}
