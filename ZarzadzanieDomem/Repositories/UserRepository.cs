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

        public User GetUser(int id) => _context.Users.FirstOrDefault(u => u.UserId == id);

        public User GetUserByEmail(string email) => _context.Users.FirstOrDefault(u => u.Email.Equals(email.Trim()));

        public void Save()
        {
            _context.SaveChanges();
        }
        public void AddUser(User value)
        {
            _context.Users.Add(value);
        }
        public void Update(User user, User changedUser)
        {
                user.FirstName = changedUser.FirstName;
                user.LastName = changedUser.LastName;
                user.Email = changedUser.Email;
                user.Password = changedUser.Password;
                user.DateOfBirth = changedUser.DateOfBirth;
                user.Saldo = changedUser.Saldo;
                user.UserId = changedUser.UserId;
                _context.Users.Update(user);
        }
        public void Delete(User user)
        {           
                _context.Users.Remove(user);
        }
       
    }
}
