using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace Codenation.Challenge.Services
{
    public class UserService : IUserService
    {
        private CodenationContext _context;

        public UserService(CodenationContext context)
        {
            _context = context;
        }

        public IList<User> FindByAccelerationName(string name)
        {
            return _context.Candidates.Where(x => x.Acceleration.Name == name)
                .Select(x => x.User)
                .Distinct()
                .ToList();

        }

        public IList<User> FindByCompanyId(int companyId)
        {
            return _context.Candidates.Where(x => x.Company.Id == companyId)
                .Select(x => x.User)
                .Distinct()
                .ToList();
        }

        public User FindById(int id)
        {
            return _context.Users.Find(id);

        }

        public User Save(User user)
        {
            if (user.Id == 0)
            {
                _context.Users.Add(user);
            }
            else
            {
                _context.Users.Update(user);
            }

            _context.SaveChanges();

            return user;
        }

    }
}
