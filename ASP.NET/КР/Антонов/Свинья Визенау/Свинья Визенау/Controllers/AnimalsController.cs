using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Свинья_Визенау.Data;

namespace Свинья_Визенау.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalsController : ControllerBase
    {
        private readonly ZooContext context;

        public AnimalsController(ZooContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAll()
        {
            var list = await context.Animals
                .Select(a => new
                {
                    a.Id,
                    a.Slug,
                    a.Name,
                    a.Description,
                    a.PhotoUrl,
                    a.VideoUrl,
                    a.WebcamUrl,
                    a.ArrivedAt
                })
                .ToListAsync();

            return Ok(list);
        }

        [HttpGet("{slug}")]
        public async Task<ActionResult<object>> GetBySlug(string slug)
        {
            var animal = await context.Animals
                .Where(a => a.Slug == slug)
                .Select(a => new
                {
                    a.Id,
                    a.Slug,
                    a.Name,
                    a.Description,
                    a.PhotoUrl,
                    a.VideoUrl,
                    a.WebcamUrl,
                    a.ArrivedAt
                })
                .FirstOrDefaultAsync();

            if (animal == null) return NotFound();
            return Ok(animal);
        }
    }
}