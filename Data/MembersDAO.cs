
using DotNet_API.Domain;
using Microsoft.EntityFrameworkCore;

namespace DotNet_API.Data
{
    public class MembersDAO
    {
        private readonly MemberContext _context;

        public MembersDAO(MemberContext context)
        {
            _context = context;
        }

        // Create
        public async Task<Member> CreateAsync(Member member)
        {
            _context.Set<Member>().Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        // Read single entity
        public async Task<Member?> GetByIdAsync(int id)
        {
            return await _context.Set<Member>().FindAsync(id);
        }

        // Read all entities
        public async Task<List<Member>> GetAllAsync()
        {
            return await _context.Set<Member>().ToListAsync();
        }

        // Update
        public async Task UpdateAsync(Member member)
        {
            _context.Set<Member>().Update(member);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(int id)
        {
            var member = await _context.Set<Member>().FindAsync(id);
            if (member != null)
            {
                _context.Set<Member>().Remove(member);
                await _context.SaveChangesAsync();
            }
        }
    }


}