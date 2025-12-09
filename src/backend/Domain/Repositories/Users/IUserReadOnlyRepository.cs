using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories.Users
{
    public interface IUserReadOnlyRepository
    {
        Task<User?> GetByIdAsync(long id, CancellationToken ct = default);
        Task<User?> GetByGuidAsync(Guid id, CancellationToken ct = default);
        Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default);
        Task<bool> ExistsByEmailAsync(Email email, CancellationToken ct = default);
    }
}
