using Domain.Entities;

namespace Domain.Repositories.Users
{
    public interface IUserWriteOnlyRepository
    {
        Task AddAsync(User user, CancellationToken ct = default);
    }
}
