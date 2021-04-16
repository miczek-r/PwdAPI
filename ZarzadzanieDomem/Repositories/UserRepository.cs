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
            User user = _context.Users.Where(user => user.UserId == value.UserId).FirstOrDefault();

            if (user!=null)
            {
                user.HomeId = value.HomeId;
                user.FirstName = value.FirstName;
                user.LastName = value.LastName;
                user.HomeId = value.HomeId;
                user.email = value.email;
                user.login = value.login;
                user.password = value.password;
                user.saldo = value.saldo;
                user.UserId = value.UserId;
                _context.Users.Update(user);
            }
            else
            {
                throw new Exception();
            }
        }
        public void Delete(int id)
        {
            User user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            else
            {
                throw new Exception();
            }
        }
        public User GetUserById(int id)
        {
            User user = _context.Users.Find(id);
            if (user != null)
            {
                return _context.Users.Where(user => user.UserId == id).FirstOrDefault();
            }
            else
            {
                throw new Exception();
            }

        }
    }
}
