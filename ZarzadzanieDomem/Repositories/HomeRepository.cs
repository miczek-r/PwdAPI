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
    public class HomeRepository : IHomeRepository
    {
        private readonly DatabaseContext _context;
        public HomeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Home> GetHomes()
        {
            return _context.Homes.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public void AddHome(Home value)
        {
            _context.Homes.Add(value);
        }
        public void Update(Home value)
        {
            Home home = _context.Homes.Where(home => home.HomeId == value.HomeId).FirstOrDefault();
            if (home != null)
            {
                home.City = value.City;
                home.HomeId = value.HomeId;
                home.PostCode = value.PostCode;
                home.Street = value.Street;
                home.HouseNumber = value.HouseNumber;
                home.HomeName = value.HomeName;
                _context.Homes.Update(home);
            }
            else
            {
                throw new Exception();
            }
        }
        public void Delete(int id)
        {
            Home home = _context.Homes.Find(id);
            if (home != null)
            {
                _context.Homes.Remove(home);
            }
            else
            {
                throw new Exception();
            }

        }
        public Home GetHomeById(int id)
        {
            Home home = _context.Homes.Find(id);
            if (home != null)
            {
               return _context.Homes.Where(home => home.HomeId == id).FirstOrDefault();
            }
            else
            {
                throw new Exception();
            }

        }
    }
}
