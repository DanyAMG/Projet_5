using Microsoft.AspNetCore.Mvc;
using Projet_5.Models;
using Projet_5.Services;

namespace Projet_5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            var announcements = await _announcementService.GetAllAnnouncementsAsync();
            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncementById(int id)
        {
            var announcement = await _announcementService.GetAnnouncementByIdAsync(id);
            if (announcement == null)
            {
                return NotFound();
            }
            return Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement([FromBody] Announcement newAnnouncement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdAnnouncement = await _announcementService.AddAnnouncementAsync(newAnnouncement);
            return CreatedAtAction(nameof(GetAnnouncementById), new { id = createdAnnouncement.Id }, createdAnnouncement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(int id, [FromBody] Announcement updatedAnnouncement)
        {
            if (id != updatedAnnouncement.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _announcementService.UpdateAnnouncementAsync(updatedAnnouncement);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var success = await _announcementService.DeleteAnnouncementAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("disponibility")]
        public async Task<IActionResult> SetDisponibility([FromQuery] bool disponibility, [FromQuery] bool selled)
        {
            var success = await _announcementService.SetDisponibilityAsync(disponibility, selled);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("archive")]
        public IActionResult ArchiveAnnouncement([FromQuery] bool selled)
        {
            _announcementService.ArchiveAnnoucement(selled);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDescription(int id, [FromBody] string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return BadRequest();
            }

            var success = await _announcementService.UpdateDescriptionAsync(id, description);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePhoto(int id, [FromBody] string photo)
        {
            if (string.IsNullOrWhiteSpace(photo))
            {
                return BadRequest();
            }

            var success = await _announcementService.UpdatePhotoAsync(id, photo);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
