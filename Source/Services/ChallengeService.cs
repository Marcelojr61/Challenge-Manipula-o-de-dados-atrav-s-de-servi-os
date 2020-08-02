using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class ChallengeService : IChallengeService
    {
        private CodenationContext _context;

        public ChallengeService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Models.Challenge> FindByAccelerationIdAndUserId(int accelerationId, int userId)
        {
            return _context.Candidates.Where(x => x.UserId == userId && x.AccelerationId == accelerationId)
                .Select(x => x.Acceleration)
                .Select(x => x.Challenges)
                .Distinct()
                .ToList();
        }

        public Models.Challenge Save(Models.Challenge challenge)
        {
            if (challenge.Id == 0)
            {
                _context.Challenges.Add(challenge);
            }
            else
            {
                _context.Challenges.Update(challenge);
            }
            _context.SaveChanges();

            return challenge;
        }
    }
}