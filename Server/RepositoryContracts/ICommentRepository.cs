using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> AddAsync(Comment comment);

    Task<Comment?> GetSingleAsync(int id);

    IQueryable<Comment> GetManyByPost(int postId);
    
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(Comment comment);

}