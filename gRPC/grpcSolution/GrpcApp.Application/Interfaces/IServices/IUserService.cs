using grpcApp.Application.Interfaces.Models;

namespace grpcApp.Application.Interfaces.IServices;

public interface IUserService
{
    public Task<User> AddAsync(User user);

    public Task<User> DeleteAsync(Guid id);

    public Task<List<User>> ListAsync();

    public Task<User> UpdateAsync(User user);

    public Task<User> GetAsync(Guid id);
}