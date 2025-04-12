using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi0;

namespace WebApi0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracingResearchController : ControllerBase
    {
        private readonly SimpleDbContext _context;

        public TracingResearchController(SimpleDbContext context)
        {
            _context = context;
        }

        [HttpPost(nameof(PostCustomTracing))]
        public async Task<IActionResult> PostCustomTracing()
        {
            var parentContext = Activity.Current?.Context ?? default(ActivityContext);
            using var activity1 = ActivityExample.Source.StartActivity(ActivityExample.Example1 , ActivityKind.Internal, parentContext);
            using var activity2 = ActivityExample.Source.StartActivity("My Custom activity2", ActivityKind.Internal, parentContext);
            activity1?.SetTag("my-tag", "my-value");
            activity2?.SetTag("my-tag-2", "my-value-2");
            activity2?.SetStatus(ActivityStatusCode.Error);
            await Task.Delay(1000);
            return Ok();
        }
    }
}
