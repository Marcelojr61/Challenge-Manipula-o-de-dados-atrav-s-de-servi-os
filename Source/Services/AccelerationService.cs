using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class AccelerationService : IAccelerationService
    {
        private CodenationContext _context;

        public AccelerationService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Acceleration> FindByCompanyId(int companyId)
        {
            return _context.Candidates.Where(x => x.CompanyId == companyId)
                .Select(x => x.Acceleration)
                .Distinct()
                .ToList();
        }

        public Acceleration FindById(int id)
        {
            return _context.Accelerations.First(x => x.Id == id);
        }

        public Acceleration Save(Acceleration acceleration)
        {
            if (acceleration.Id == 0)
            {
                _context.Accelerations.Add(acceleration);
            }
            else
            {
                _context.Accelerations.Update(acceleration);
            }
            _context.SaveChanges();

            return acceleration;
        }
    }
}
