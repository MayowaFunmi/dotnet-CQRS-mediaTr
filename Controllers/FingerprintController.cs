using Microsoft.AspNetCore.Mvc;
using SourceAFIS;
using Microsoft.AspNetCore.Hosting;
using CQRSMediaTr.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CQRSMediaTr.DTO;
using CQRSMediaTr.Data;

namespace CQRSMediaTr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FingerprintController : ControllerBase
    {

        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DataContext _context;

        public FingerprintController(IWebHostEnvironment env, UserManager<ApplicationUser> userManager, DataContext context)
        {
            _userManager = userManager;
            _env = env;
            _context = context;
        }
        // comparing two fingerprints
        [Authorize]
        [HttpGet("compare_fingerprints")]
        public IActionResult Compare()
        {
            var path1 = Path.Combine(_env.ContentRootPath, "Images/finger2.png");
            var path2 = Path.Combine(_env.ContentRootPath, "Images/finger2.png");
            var probe = new FingerprintTemplate(
                new FingerprintImage(System.IO.File.ReadAllBytes(path1))
            );
            var candidate = new FingerprintTemplate(
                new FingerprintImage(System.IO.File.ReadAllBytes(path2))
            );
            Console.WriteLine("probe = " + probe);
            Console.WriteLine("candidate = " + candidate);

            var matcher = new FingerprintMatcher(probe);
            Console.WriteLine("matcher = " + matcher);

            double similarity = matcher.Match(candidate);
            Console.WriteLine("similarity = " + similarity);

            double threshold = 30;
            bool matches = similarity >= threshold;
            return Ok(matches);
        }

        // upload fingerprint
        [Authorize]
        [HttpPost("{userId}/fingerprint")]
        public async Task<IActionResult> UploadFingerprintTemplate(string userId, [FromBody] FingerprintTemplate template)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user ==  null)
                return NotFound();
            user.FingerprintTemplate = template.ToByteArray();
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return StatusCode(200);
        }

        // view users
        [Authorize]
        [HttpGet("GetUser/{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        //update user
        [Authorize]
        [HttpPut("update-user/{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] ApplicationUser updatedUser)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            user.UserName = updatedUser.UserName;
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return StatusCode(200);
        }

        // search fingerprints List<Subject> subjects = await _context.Subjects.ToListAsync();
        [HttpPost("search_fingerprint")]
        public async Task<IActionResult> SearchFingerprint([FromBody] FingerprintTemplateDto templateDto)
        {
            var candidates = await _context.FingerprintTemplateModels.ToList();
        }
    }
}