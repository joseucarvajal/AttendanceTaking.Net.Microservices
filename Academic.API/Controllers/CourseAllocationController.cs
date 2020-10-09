using Academic.API.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            _mediator = mediator;
        }


        [HttpPost]
        [Route("course")]
        public async Task<ActionResult<bool>> CreateCourse(
            [FromBody]CreateCourseCommandHandler.Command command
        )
        {
            return await _mediator.Send(command);
        }
    }
}
