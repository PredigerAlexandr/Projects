using grpcApp.Application.Interfaces.IRepositories;
using grpcApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace grpcApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GrpcApp.Infrastructure.AppContext _context;

    public UserRepository(GrpcApp.Infrastructure.AppContext appContext)
    {
        _context = appContext;
    }

    public async Task<Guid> AddAsync(UserEntity user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
        var deleteEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (deleteEntity != null)
        {
            _context.Users.Remove(deleteEntity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<UserEntity>> ListAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task UpdateAsync(UserEntity user)
    {
        var updateEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

        if (updateEntity != null)
        {
            updateEntity.Name = user.Name;
            updateEntity.Age = user.Age;

            await _context.SaveChangesAsync();
        }
    }

    public async Task<UserEntity> GetAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
}