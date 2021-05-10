using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id) => _context.Users.FirstOrDefault(u => u.UserId == id);

        public User GetUserByEmail(string email) => _context.Users.FirstOrDefault(u => u.Email.Equals(email.Trim()));

        public IEnumerable<User> GetByHomeId(int HomeId) => _context.Users.Where(u => u.HomeId == HomeId);

        public void Save()
        {
            _context.SaveChanges();
        }
        public void Create(User value)
        {
            _context.Users.Add(value);
        }
        public void Update(User user, User changedUser)
        {
            user.FirstName = changedUser.FirstName;
            user.LastName = changedUser.LastName;
            user.DateOfBirth = changedUser.DateOfBirth;
            user.Saldo = changedUser.Saldo;
            user.ExpenseLimit = changedUser.ExpenseLimit;
            user.HomeId = changedUser.HomeId;
            _context.Users.Update(user);
        }
        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }
        
    }
}
