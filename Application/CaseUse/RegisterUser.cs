using Application.ICaseUse;
using Application.Mappers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;


namespace Application.CaseUse;

public class RegisterUser : IRegisterUser
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public RegisterUser(IUnitOfWork unitOfWork, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task<bool> Execute(RegisterRequestDto request)
    {
        if (!_authService.IsPasswordSecure(request.Password))
            throw new Exception("Contraseña insegura");

        var exists = await _unitOfWork.Repository<user>()
            .GetAll()
            .AnyAsync(u => u.Email == request.Email);
        
        if (exists) throw new Exception("Email ya registrado");

        var user = new User(0, request.Email, _authService.HashPassword(request.Password), DateTime.UtcNow, request.UserTypeId);
        var userEntity = UserMapper.ToEntity(user);

        await _unitOfWork.Repository<user>().AddAsync(userEntity);
        await _unitOfWork.SaveChange();

        var profile = new UserProfile(0, userEntity.UserId, request.Name, request.Ruc, request.ManagerName, request.ManagerDni, request.ManagerEmail, request.Phone, request.Address);
        var profileEntity = UserProfileMapper.ToEntity(profile);

        await _unitOfWork.Repository<userprofile>().AddAsync(profileEntity);
        await _unitOfWork.SaveChange();

        return true;
    }
}
