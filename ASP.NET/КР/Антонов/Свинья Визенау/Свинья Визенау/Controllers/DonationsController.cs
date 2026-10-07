using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Свинья_Визенау.Data;
using Свинья_Визенау.Models;

namespace Свинья_Визенау.Controllers
{
    [ApiController]
    [Route("api/donations")]
    public class DonationsController : ControllerBase
    {
        private readonly ZooContext context;

        public DonationsController(ZooContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await context.Donations
                .OrderByDescending(d => d.CreatedAt)
                .Take(50)
                .Select(d => new { d.Id, d.Amount, d.Purpose, d.DonorName, d.Message, d.CreatedAt })
                .ToListAsync();

            return Ok(list);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] DonationDto dto)
        {
            if (dto.Amount <= 0) return BadRequest("Amount must be > 0");

            var donation = new Donation
            {
                Amount = dto.Amount,
                Purpose = dto.Purpose ?? "Free donation",
                DonorName = dto.DonorName,
                Message = dto.Message,
                CreatedAt = DateTime.UtcNow,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            context.Donations.Add(donation);
            await context.SaveChangesAsync();

            return Ok(new { donation.Id });
        }

        public class DonationDto
        {
            public decimal Amount { get; set; }
            public string? Purpose { get; set; }
            public string? DonorName { get; set; }
            public string? Message { get; set; }
        }
    }
}