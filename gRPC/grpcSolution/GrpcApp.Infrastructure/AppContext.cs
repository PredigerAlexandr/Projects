using grpcApp.DataAccess.Utility;
using grpcApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace grpcApp.DataAccess;

public class AppContext:DbContext
{
    private readonly string _connectionString;
    public AppContext(IOptions<ContextSettings> settings)
    {
        _connectionString = settings.Value.ConnectionString;
        Database.EnsureCreated();
    }
    
    public DbSet<UserEntity> Users;
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_connectionString);
    }
}