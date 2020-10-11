using Academic.API.Application.Commands;
using Academic.API.Application.Queries.CourseAllocationAggregate;
using Academic.Domain.CourseAllocationAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Academic.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CourseAllocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseAllocationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost]
        [Route("course")]
        public async Task<ActionResult<bool>> CreateCourse(
            [FromBody]CreateCourseCommandHandler.Command command
        )
        {
            return await _mediator.Send(command);
        }

        [HttpGet]
        [Route("course/{id}")]
        public async Task<Course> GetCourseById([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetCourseByIdQueryHandler.Query(id));
        }
    }
}
