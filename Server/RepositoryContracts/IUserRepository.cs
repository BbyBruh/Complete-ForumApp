using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);

    Task<User?> GetSingleAsync(int id);

    Task<User?> GetByUserNameAsync(string userName);

    IQueryable<User> GetMany();
}