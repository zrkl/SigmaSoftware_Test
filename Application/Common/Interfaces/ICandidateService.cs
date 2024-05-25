using Application.DTO.Candidates;
using Application.Features.Candidates.Commands;

namespace Application.Common.Interfaces
{
    public interface ICandidateService
    {
        /// <summary>
        /// Service to Create or Update Candidates via test unit
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        Task<CandidatesDTO?> SubmitCandidate(SubmitCandidatesCommand command);
    }
}
