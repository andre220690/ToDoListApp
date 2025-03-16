using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoListApp.Auth.Models;
using ToDoListApp.Core.Models;
using ToDoListApp.Dal.Repositories.Interfaces;

namespace ToDoListApp.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly AuthOptions _authOptions;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IUserRepository userRepository, AuthOptions authOptions, ILogger<AuthService> logger)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _authOptions = authOptions ?? throw new ArgumentNullException(nameof(authOptions));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<AuthResponse> CreateUserAsync(User user)
    {
        _logger.LogInformation("Попытка регистрации пользователя {0}", user.UserName);

        if (await _userRepository.ExistsAsync(user))
        {
            _logger.LogInformation("Отказ в регистрации пользователя {0}", user.UserName);
            return new AuthResponse() { IsAuthorized = false };
        }

        await _userRepository.CreateAsync(user);
        var token = BuildToken(user);

        _logger.LogInformation("Успешная регистрация пользователя {0}", user.UserName);

        return new AuthResponse()
        {
            IsAuthorized = true,
            Token = token,
        };
    }

    public async Task<AuthResponse> LoginAsync(User user)
    {
        _logger.LogInformation("Попытка аутентификации пользователя {0}", user.UserName);

        if (!await _userRepository.ExistsAsync(user))
        {
            _logger.LogInformation("Отказ аутентификации пользователя {0}", user.UserName);

            return new AuthResponse() { IsAuthorized = false };
        }

        var token = BuildToken(user);

        _logger.LogInformation("Успешная аутентификация пользователя {0}", user.UserName);

        return new AuthResponse()
        {
            IsAuthorized = true,
            Token = token,
        };
    }

    private string BuildToken(User user)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };

        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authOptions.Key)); // Нужно вставить ключ из appsettings
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_authOptions.ExpireMinutes),
            signingCredentials: credentials);
        var key = new JwtSecurityTokenHandler().WriteToken(jwt);

        return key;
    }

}
