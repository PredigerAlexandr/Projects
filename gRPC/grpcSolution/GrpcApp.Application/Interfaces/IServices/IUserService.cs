using grpcApp.Application.Interfaces.Models;

namespace grpcApp.Application.Interfaces.IServices;

public interface IUserService
{
    public Task<Guid> AddAsync(User user);

    public Task DeleteAsync(Guid id);

    public Task<List<User>> ListAsync();

    public Task UpdateAsync(User user);
}