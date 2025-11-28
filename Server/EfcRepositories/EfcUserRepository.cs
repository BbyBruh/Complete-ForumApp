using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcUserRepository : IUserRepository
{
    private readonly AppDbContext ctx;

    public EfcUserRepository(AppDbContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<User> AddAsync(User user)
    {
        await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetSingleAsync(int id)
    {
        return await ctx.Users.FindAsync(id);
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await ctx.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public IQueryable<User> GetMany()
    {
        return ctx.Users.AsQueryable();
    }
}