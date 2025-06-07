using DotNet_API.Data;
using DotNet_API.Domain;

namespace DotNet_API.Services
{
    public class MemberServices
    {
        private readonly MembersDAO _membersDao;

        public MemberServices(MembersDAO membersDao)
        {
            _membersDao = membersDao;
        }

        public async Task<List<Member>> get_all_members()
        {
            return await _membersDao.GetAllAsync();
        }

        public async Task<Member?> get_member_by_id(int id)
        {
            return await _membersDao.GetByIdAsync(id);
        }

        public async Task<Member?> get_member_by_email(string email)
        {
            var allMembers = await _membersDao.GetAllAsync();
            return allMembers.FirstOrDefault(m => m.Email == email);
        }

        public async Task<List<Member>> get_active_members()
        {
            var allMembers = await _membersDao.GetAllAsync();
            return allMembers.Where(m => m.isActive).ToList();
        }

        public async Task<Member> add_member(Member member)
        {
            return await _membersDao.CreateAsync(member);
        }

        public async Task update_member(Member member)
        {
            await _membersDao.UpdateAsync(member);
        }

        public async Task delete_member(int id)
        {
            await _membersDao.DeleteAsync(id);
        }
    }
}
}
