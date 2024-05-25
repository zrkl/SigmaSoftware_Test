using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Bases
{
    /// <summary>
    /// Base API
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Mediator declaration
        /// </summary>
        private IMediator _mediator;

        /// <summary>
        /// Project configuration
        /// </summary>
        private IConfiguration _config;

        /// <summary>
        /// 
        /// </summary>
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// 
        /// </summary>
        protected IConfiguration Config => _config ??= HttpContext.RequestServices.GetService<IConfiguration>();
    }
}