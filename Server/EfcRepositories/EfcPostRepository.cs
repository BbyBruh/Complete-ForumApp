using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcPostRepository : IPostRepository
{
    private readonly AppDbContext ctx;

    public EfcPostRepository(AppDbContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Post> AddAsync(Post post)
    {
        await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return post;
    }

    public async Task<Post?> GetSingleAsync(int id)
    {
        return await ctx.Posts
            .Include(p => p.User)
            .Include(p => p.Comments)
            .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public IQueryable<Post> GetMany()
    {
        return ctx.Posts.Include(p => p.User);
    }
    
    public async Task UpdateAsync(Post post)
    {
        ctx.Posts.Update(post);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(Post post)
    {
        ctx.Posts.Remove(post);
        await ctx.SaveChangesAsync();
    }

}