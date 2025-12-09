using Domain.Entities;

namespace Domain.Repositories.Users
{
    public interface IUserUpdateOnlyRepository
    {
        void Update(User user);
    }
}
