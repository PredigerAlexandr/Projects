using grpcApp.Domain.Entities;

namespace grpcApp.Application.Interfaces.IRepositories;

public interface IUserRepository
{
    public Task<Guid> AddAsync(UserEntity user);

    public Task DeleteAsync(Guid id);

    public Task<List<UserEntity>> ListAsync();

    public Task UpdateAsync(UserEntity user);
    
    public Task<UserEntity> GetAsync(Guid id);
}