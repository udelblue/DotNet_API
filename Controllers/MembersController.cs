using DotNet_API.Domain;
using DotNet_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNet_API.Controllers
{


    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private MemberServices _memberServices;

        public MembersController(ILogger<MembersController> logger, MemberServices memberServices)
        {
            _logger = logger;
            _memberServices = memberServices;
        }

        [Route("GetAllMembers")]
        [HttpGet]
        public IEnumerable<Member> GetAllMembers()
        {
            return _memberServices.get_all_members().Result;
        }

        [Route("GetByID/{id}")]
        [HttpGet]
        public ActionResult<Member> GetMemberByID(int id)
        {
            var member = _memberServices.get_member_by_id(id).Result;
            if (member == null)
            {
                return NotFound($"Member with ID {id} not found.");
            }

            return Ok(member);
        }


        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult<Member>> AddMember([FromBody] Member member)
        {
            if (member == null)
            {
                return BadRequest("Member data is required.");
            }

            try
            {
                var createdMember = await _memberServices.add_member(member);
                return CreatedAtRoute("GetMemberByID", new { id = createdMember.Id }, createdMember);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding member.");
                return StatusCode(500, "An error occurred while adding the member.");
            }
        }


        [Route("Delete/{id}")]
        [HttpPut]
        public async Task<ActionResult> UpdateMember(int id, [FromBody] Member member)
        {
            if (member == null || member.Id != id)
            {
                return BadRequest("Member data is invalid or ID mismatch.");
            }

            try
            {
                var existingMember = await _memberServices.get_member_by_id(id);
                if (existingMember == null)
                {
                    return NotFound($"Member with ID {id} not found.");
                }

                await _memberServices.update_member(member);
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