using Academic.API.Application.Commands;
using Academic.API.Application.Queries.CourseAllocationAggregate;
using Academic.Domain.CourseAllocationAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
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
        [SwaggerOperation(Summary = "Creates a new course")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Course created succesfully")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Bad Request")]
        public async Task<ActionResult<bool>> CreateCourse(
            [FromBody]CreateCourseCommandHandler.Command course
        )
        {
            return await _mediator.Send(course);
        }

        [HttpGet]
        [Route("course/{id}")]
        [SwaggerOperation(Summary = "Gets a specific course by a given Id")]
        public async Task<Course> GetCourseById([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetCourseByIdQueryHandler.Query(id));
        }

        [HttpGet]
        [Route("course/details")]
        [SwaggerOperation(Summary = "Gets all courses list")]
        public async Task<IEnumerable<Course>> GetCourseDetails()
        {
            return await _mediator.Send(new GetAllCoursesWithDetailsQueryHandler.Query());
        }

        [HttpGet]
        [Route("course/{id}/details")]
        [SwaggerOperation(Summary = "Gets specific course details by a given Id")]
        public async Task<Course> GetCourseDetailsById([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetCourseDetailsByIdQueryHandler.Query(id));
        }

    }
}
