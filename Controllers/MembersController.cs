using DotNet_API.Domain;
using DotNet_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_API.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private MemberServices _memberServices;

        public MembersController(ILogger<MembersController> logger, MemberServices memberServices)
        {
            _logger = logger;
            _memberServices = memberServices;
        }

        [HttpGet("GetAllMembers")]
        public IEnumerable<Member> GetAllMembers()
        {
            _logger.LogInformation("GetAllMembers was called.");
            return _memberServices.get_all_members().Result;
        }

        [HttpGet("GetByID/{id}", Name = "GetMemberByID")]
        public ActionResult<Member> GetMemberByID(int id)
        {

            var member = _memberServices.get_member_by_id(id).Result;
            if (member == null)
            {
                return NotFound($"Member with ID {id} not found.");
            }

            _logger.LogInformation("GetByID called for ID {id}.", id.ToString());

            return Ok(member);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<Member>> AddMember([FromBody] Member member)
        {
            if (member == null)
            {
                return BadRequest("Member data is required.");
            }

            try
            {
                var createdMember = await _memberServices.add_member(member);

                _logger.LogInformation("Add called for  {json}.", createdMember.ToJson());
                // This will now resolve correctly
                return CreatedAtRoute("GetMemberByID", new { id = createdMember.Id }, createdMember);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding member.");
                return StatusCode(500, "An error occurred while adding the member.");
            }
        }

        [HttpPut("Delete/{id}")]
        public async Task<ActionResult> UpdateMember(int id)
        {



            try
            {
                var existingMember = await _memberServices.get_member_by_id(id);
                if (existingMember == null)
                {
                    return NotFound($"Member with ID {id} not found.");
                }

                _logger.LogInformation("Delete called for ID {id}.", id.ToString());
                await _memberServices.delete_member(existingMember.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating member with ID {id}.");
                return StatusCode(500, "An error occurred while updating the member.");
            }
        }
    }
}