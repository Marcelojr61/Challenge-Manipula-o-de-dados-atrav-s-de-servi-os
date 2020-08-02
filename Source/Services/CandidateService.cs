using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class CandidateService : ICandidateService
    {
        private CodenationContext _context;

        public CandidateService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Candidate> FindByAccelerationId(int accelerationId)
        {

            return _context.Candidates.Where(x => x.AccelerationId== accelerationId).Distinct().ToList();
        }

        public IList<Candidate> FindByCompanyId(int companyId)
        {
            return _context.Candidates.Where(x => x.CompanyId == companyId).Distinct().ToList();
        }

        public Candidate FindById(int userId, int accelerationId, int companyId)
        {
            return _context.Candidates.Where(x => x.UserId == userId && x.AccelerationId == accelerationId && x.CompanyId == companyId).FirstOrDefault();
        }

        public Candidate Save(Candidate candidate)
        {
            var hasCandidate = _context.Candidates
                .Any(x => x.AccelerationId == candidate.AccelerationId &&
                x.CompanyId == candidate.CompanyId 
                && x.UserId == candidate.UserId);

            if (!hasCandidate)
            {
                _context.Candidates.Add(candidate);
            }
            else 
            {
                _context.Candidates.Update(candidate);
            }
            _context.SaveChanges();

            return candidate;
        }
    }
}
