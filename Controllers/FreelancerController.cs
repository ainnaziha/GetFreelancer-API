using GetFreelancer_API.Data;
using GetFreelancer_API.Interface;
using GetFreelancer_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GetFreelancer_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IApiKeyValidation _apiKeyValidation;

        public FreelancerController(ApplicationDbContext context, IApiKeyValidation apiKeyValidation)
        {
            _context = context;
            _apiKeyValidation = apiKeyValidation;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Freelancer>>> GetFreelancers()
        {
            if (!Authorize())
            {
                return Unauthorized();
            }
            return Ok(await _context.Freelancers.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Freelancer>> PostFreelancer(Freelancer freelancer)
        {
            try
            {
                if (!Authorize())
                {
                    return Unauthorized();
                }
                var validationErrors = await ValidateFreelancerUniqueness(freelancer);

                if (validationErrors.Count > 0)
                {
                    return BadRequest(validationErrors);
                }

                _context.Freelancers.Add(freelancer);
                await _context.SaveChangesAsync();

                var freelancers = await _context.Freelancers.ToListAsync();

                return Ok(freelancers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Freelancer>> DeleteFreelancer(int id)
        {
            if (!Authorize())
            {
                return Unauthorized();
            }

            var freelancer = await _context.Freelancers.FindAsync(id);

            if (freelancer == null)
            {
                return NotFound();
            }

            _context.Freelancers.Remove(freelancer);
            await _context.SaveChangesAsync();

            return Ok("User deleted");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFreelancer(int id, [FromBody] Freelancer updatedFreelancer)
        {
            if (!Authorize())
            {
                return Unauthorized();
            }

            var existingFreelancer = await _context.Freelancers.FindAsync(id);

            if (existingFreelancer == null)
            {
                return NotFound("Freelancer not found");
            }

            existingFreelancer.Username = updatedFreelancer.Username;
            existingFreelancer.Email = updatedFreelancer.Email;
            existingFreelancer.PhoneNo = updatedFreelancer.PhoneNo;
            existingFreelancer.Skillset = updatedFreelancer.Skillset;
            existingFreelancer.hobby = updatedFreelancer.hobby;

            try
            {
                await _context.SaveChangesAsync(); 
                var updatedFreelancers = await _context.Freelancers.ToListAsync();

                return Ok(new { Message = "Freelancer updated successfully", Freelancers = updatedFreelancers });
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Concurrency conflict while updating the freelancer.");
            }
        }

        //vaidation
        private async Task<Dictionary<string, string>> ValidateFreelancerUniqueness(Freelancer freelancer)
{
        var validationErrors = new Dictionary<string, string>();

        var existingFreelancerEmail = await _context.Freelancers.FirstOrDefaultAsync(f => f.Email == freelancer.Email);
        if (existingFreelancerEmail != null)
        {
            validationErrors["Email"] = "Email already exists";
        }

        var existingFreelancerUsername = await _context.Freelancers.FirstOrDefaultAsync(f => f.Username == freelancer.Username);
        if (existingFreelancerUsername != null)
        {
            validationErrors["Username"] = "Username already exists";
        }

        var existingFreelancerPhoneNo = await _context.Freelancers.FirstOrDefaultAsync(f => f.PhoneNo == freelancer.PhoneNo);
        if (existingFreelancerPhoneNo != null)
        {
            validationErrors["PhoneNo"] = "Phone number already exists";
        }

        return validationErrors;
        }

        //authorization
        private bool Authorize()
        {
            string? apiKey = Request.Headers[Constants.ApiKeyHeaderName];
            if (string.IsNullOrWhiteSpace(apiKey) || !_apiKeyValidation.IsValidApiKey(apiKey))
            {
                return false;
            }

            return true;
        }

    }
}
