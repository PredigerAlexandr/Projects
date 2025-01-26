using AutoMapper;
using grpcApp.Application.Interfaces.IRepositories;
using grpcApp.Application.Interfaces.IServices;
using grpcApp.Application.Interfaces.Models;
using grpcApp.Domain.Entities;

namespace GrpcApp.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<User> AddAsync(User user)
    {
        var userEntity = _mapper.Map<UserEntity>(user);
        
        await _userRepository.AddAsync(userEntity);
        
        var newUser = await _userRepository.GetAsync(user.Id);
        
        return _mapper.Map<User>(newUser);
    }

    public async Task<User> DeleteAsync(Guid id)
    {
        var deleteEntity = await _userRepository.GetAsync(id);
        
        await _userRepository.DeleteAsync(id);
        
        return _mapper.Map<User>(deleteEntity);
    }

    public async Task<List<User>> ListAsync()
    {
        var entities = await _userRepository.ListAsync();
        
        var result = _mapper.Map<List<User>>(entities);
        
        return result;
    }

    public async Task<User> UpdateAsync(User user)
    {
        var updateUserEntity = _mapper.Map<UserEntity>(user);
        
        await _userRepository.UpdateAsync(updateUserEntity);
        
        var updateModel = await _userRepository.GetAsync(user.Id);
        
        var result = _mapper.Map<User>(updateModel);
        
        return result;
    }

    public async Task<User> GetAsync(Guid id)
    {
        var user = await _userRepository.GetAsync(id);
        
        var result = _mapper.Map<User>(user);
        
        return result;
    }
}