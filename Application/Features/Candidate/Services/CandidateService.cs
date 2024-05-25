using Application.Common.Interfaces;
using Application.DTO.Candidates;
using Application.Features.Candidates.Commands;
using Application.Utility;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Candidate.Services
{
    public class CandidateService : ICandidateService
    {
        /// <summary>
        /// Application DbContext
        /// </summary>
        private IApplicationDbContext _dbContext { get; set; }

        /// <summary>
        /// Variable used to Map Entity to DTO in both ways
        /// </summary>
        private Mapper _mapper;

        /// <summary>
        /// default contructor
        /// </summary>
        /// <param name="dbContext"></param>
        public CandidateService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = MapperHelper.InitializeAutomapper();
        }

        /// <summary>
        /// Service to Create or Update Candidates via test unit
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CandidatesDTO?> SubmitCandidate(SubmitCandidatesCommand command)
        {
            CandidatesDTO? result = null;

            try
            {
                var item = await _dbContext.Candidates
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == command.Id);

                if (item == null)//if the Candidates not found we add it
                {
                    // Map DTO into Entity
                    var _command = _mapper.Map<SubmitCandidatesCommand, Domain.Entities.Candidates>(command);

                    // Add record to db
                    await _dbContext.Candidates.AddAsync(_command);
                    await _dbContext.SaveChangesAsync();

                    // Reture saved record
                    result = _mapper.Map<Domain.Entities.Candidates, CandidatesDTO>(_command);
                    return result;
                }
                else//if the Candidates found we update it
                {
                    // Map DTO into Entity
                    item = _mapper.Map(command, item);

                    // Add record to db
                    _dbContext.Candidates.Update(item);
                    await _dbContext.SaveChangesAsync();

                    // Reture saved record
                    result = _mapper.Map<Domain.Entities.Candidates, CandidatesDTO>(item);
                    return result;
                }
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
