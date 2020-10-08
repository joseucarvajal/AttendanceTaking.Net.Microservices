using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academic.API.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Academic.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CourseAllocationController : Controller
    {
        private readonly IMediator _mediator;

        public CourseAllocationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("course")]
        public async Task<IActionResult> CreateCourse(
            [FromBody]CreateOrderCommandHandler.Command command
        )
        {
            await _mediator.Send(command);
            return Ok();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
