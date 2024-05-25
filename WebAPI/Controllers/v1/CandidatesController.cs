using Application.Common.Interfaces;
using Application.Features.Candidate.Services;
using Application.Features.Candidates.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers.Bases;

namespace WebAPI.Controllers.v1
{
    /// <summary>
    /// Candidates Controller
    /// </summary>
    [ApiVersion("1.0")]
    public class CandidatesController : BaseApiController
    {
        /// <summary>
        /// Mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Candidate Service
        /// </summary>
        private ICandidateService _candidateService;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public CandidatesController(IMediator mediator, ICandidateService candidateService)
        {
            _mediator = mediator;
            _candidateService = candidateService;
        }

        /// <summary>
        /// API to Create or Update Candidates via MediatR
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("Submit")]
        public async Task<IActionResult> Submit(SubmitCandidatesCommand query)
        {
            try
            {
                var searchResult = await _mediator.Send(query);

                return Ok(searchResult);
            }
            catch (Exception ex)
            {
                // Retun error
                return BadRequest("An error has occurred");
            }
        }

        /// <summary>
        /// API to Create or Update Candidates via Service
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost("SubmitTest")]
        public async Task<IActionResult> SubmitTest(SubmitCandidatesCommand query)
        {
            try
            {
                var searchResult = await _candidateService.SubmitCandidate(query);

                return Ok(searchResult);
            }
            catch (Exception ex)
            {
                // Retun error
                return BadRequest("An error has occurred");
            }
        }

    }
}
