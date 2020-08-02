using System.Collections.Generic;
using System.Linq;
using Codenation.Challenge.Models;

namespace Codenation.Challenge.Services
{
    public class SubmissionService : ISubmissionService
    {
        private CodenationContext _context;

        public SubmissionService(CodenationContext context)
        {
            _context = context;
        }

        public IList<Submission> FindByChallengeIdAndAccelerationId(int challengeId, int accelerationId)
        {
            return _context.Submissions.Where(x => x.User.Candidates.Any
            (can => can.AccelerationId == accelerationId 
            && x.ChallengeId == challengeId))
                .ToList();
        }

        public decimal FindHigherScoreByChallengeId(int challengeId)
        {
            return _context.Submissions.Where(x => x.ChallengeId == challengeId)
                .Select(x => x.Score)
                .OrderByDescending(x =>x)
                .First();
        }

        public Submission Save(Submission submission)
        {
            var hasSubmission = _context.Submissions
                .Any(x => x.ChallengeId == submission.ChallengeId && 
                x.UserId == x.UserId);

            if (!hasSubmission)
            {
                _context.Submissions.Add(submission);
            }
            else
            {
                _context.Submissions.Update(submission);
            }

            _context.SaveChanges();

            return submission;
        }
    }
}
