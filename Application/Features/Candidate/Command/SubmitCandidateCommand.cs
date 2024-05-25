using Application.Common.Interfaces;
using Application.DTO.Candidates;
using Application.Utility;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Candidates.Commands
{
    /// <summary>
    /// Class That handles Create or Update Candidates Request
    /// </summary>
    public class SubmitCandidatesCommand : IRequest<CandidatesDTO?>
    {
        /// <summary>
        /// First Name of the Candidates
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// First Name of the Candidates
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of the Candidates
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Phone Nmber of the Candidates
        /// </summary>
        public string? PhoneNmber { get; set; }

        /// <summary>
        /// Call Time prefered by the Candidates
        /// </summary>
        public string? CallTime { get; set; }

        /// <summary>
        /// LinkedIn URL of the Candidates
        /// </summary>
        public string? LinkedInURL { get; set; }

        /// <summary>
        /// GitHub URL of the Candidates
        /// </summary>
        public string? GitHubURL { get; set; }

        /// <summary>
        /// Text Written by the Candidates
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SubmitCandidatesCommandHandler : IRequestHandler<SubmitCandidatesCommand, CandidatesDTO?>
    {
        /// <summary>
        /// 
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Variable used to Map Entity to DTO in both ways
        /// </summary>
        private Mapper _mapper;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public SubmitCandidatesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = MapperHelper.InitializeAutomapper();
        }

        public async Task<CandidatesDTO?> Handle(SubmitCandidatesCommand command, CancellationToken cancellationToken)
        {
            CandidatesDTO? result = null;

            try
            {
                var item = await _unitOfWork.Repository<Domain.Entities.Candidates>()
                    .Entities
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == command.Id);

                if (item == null)//if the Candidates not found we add it
                {
                    // Map DTO into Entity
                    var _command = _mapper.Map<SubmitCandidatesCommand, Domain.Entities.Candidates>(command);

                    // Add record to db
                    await _unitOfWork.Repository<Domain.Entities.Candidates>().AddAsync(_command);
                    await _unitOfWork.Commit(cancellationToken);

                    // Reture saved record
                    result = _mapper.Map<Domain.Entities.Candidates, CandidatesDTO>(_command);
                    return result;
                }
                else//if the Candidates found we update it
                {
                    // Map DTO into Entity
                    item = _mapper.Map(command, item);

                    // Add record to db
                    await _unitOfWork.Repository<Domain.Entities.Candidates>().UpdateAsync(item);
                    await _unitOfWork.Commit(cancellationToken);

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
