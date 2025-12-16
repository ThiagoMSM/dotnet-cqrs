using Domain.Entities;
using Domain.Repositories.Users;
using Domain.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    // write:
    public async Task AddAsync(User user, CancellationToken ct)
    {
        await _context.Users.AddAsync(user, ct);
    }

    // update:
    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    // read:
    public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken ct)
    {
        // Tracking seria pra pegar mudança e commitar junto, meio inútil em 99.99% dos casos de fetching data
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == email, ct);
    }

    public async Task<User?> GetByIdAsync(long id, CancellationToken ct)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, ct);
    }
    public async Task<User?> GetByGuidAsync(Guid id, CancellationToken ct)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserIdentifier == id, ct);
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken ct)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, ct);
    }
}