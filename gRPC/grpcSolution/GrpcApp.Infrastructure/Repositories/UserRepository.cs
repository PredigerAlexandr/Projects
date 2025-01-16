using grpcApp.Application.Interfaces.IRepositories;
using grpcApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace grpcApp.DataAccess.Repositories;

public class UserRepository:IUserRepository
{
    private readonly AppContext _context;

    public UserRepository(AppContext appContext)
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
        
    }

    public async Task<List<UserEntity>> ListAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task UpdateAsync(UserEntity user)
    {
       
    }
}