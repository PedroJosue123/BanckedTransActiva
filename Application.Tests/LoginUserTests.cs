/*
using Application.CaseUse;
using Domain.Dtos;
using Domain.Interface;
using Infraestructure.Models;
using Moq;
using MockQueryable.Moq; // <<<<< Asegúrate de agregar esto
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MockQueryable;
using Xunit;

namespace Application.Tests;

public class LoginUserTests
{
    [Fact]
    public async Task Login_CredencialesValidas_RetornaToken()
    {
        // 1. UserType de prueba
        var userType = new Usertype { UserTypeId = 1, Name = "Comprador" };

        // 2. Lista mock de usuarios
        var users = new List<User>
        {
            new User
            {
                UserId = 1,
                Email = "correo@ejemplo.com",
                Password = "hashed-password",
                FailedLoginAttempts = 0,
                LockoutUntil = null,
                UserType = userType
            }
        };

        // 3. Simular IQueryable con soporte async
        var mockUserQueryable = users.AsQueryable().BuildMock();

        // 4. Mock de repositorio
        var mockUserRepo = new Mock<IGenericRepository<User>>();
        mockUserRepo.Setup(r => r.GetAll()).Returns(mockUserQueryable.Object);

        // 5. Mock UnitOfWork
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(u => u.Repository<User>()).Returns(mockUserRepo.Object);
        mockUnitOfWork.Setup(u => u.SaveChange()).ReturnsAsync(1);

        // 6. Mock AuthService
        var mockAuthService = new Mock<IAuthService>();
        mockAuthService.Setup(a => a.VerifyPassword("1234", "hashed-password")).Returns(true);
        mockAuthService.Setup(a => a.GenerateToken(1, "correo@ejemplo.com", "Comprador")).Returns("fake-token");

        // 7. Instancia del caso de uso
        var useCase = new LoginUser(mockUnitOfWork.Object, mockAuthService.Object);

        // 8. Ejecutar con input
        var request = new LoginRequestDto
        {
            Email = "correo@ejemplo.com",
            Password = "1234"
        };

        var result = await useCase.Execute(request);

        // 9. Afirmación
        Assert.Equal("fake-token", result);
    }
}
*/
