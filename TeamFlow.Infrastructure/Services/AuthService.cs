using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamFlow.Application.DTOs.Auth;
using TeamFlow.Application.Interfaces;
using TeamFlow.Application.Interfaces.Services;
using TeamFlow.Domain.Entities;
using TeamFlow.Domain.Enums;

namespace TeamFlow.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TokenService _tokenService;
        private readonly JwtSettings _jwtSettings;
        public AuthService(
       UserManager<User> userManager,
       IUnitOfWork unitOfWork,
       TokenService tokenService,
       IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            // Email daha önce alınmış mı?
            var existingUser = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (existingUser != null)
                throw new Exception("Bu email adresi zaten kullanımda.");

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                Role = UserRole.Member, // Yeni kullanıcı varsayılan olarak Member
                CreatedAt = DateTime.UtcNow
            };

            // Identity şifreyi hashleyerek kaydeder
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Kullanıcı oluşturulamadı: {errors}");
            }

            return await GenerateTokensAsync(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (user == null)
                throw new Exception("Email veya şifre hatalı.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
                throw new Exception("Email veya şifre hatalı.");

            if (!user.IsActive)
                throw new Exception("Hesabınız aktif değil.");

            return await GenerateTokensAsync(user);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string refreshToken)
        {
            // Refresh token ile kullanıcıyı bul
            var users = await _unitOfWork.Users.GetAllAsync();
            var user = users.FirstOrDefault(x =>
                x.RefreshToken == refreshToken &&
                x.RefreshTokenExpiry > DateTime.UtcNow);

            if (user == null)
                throw new Exception("Geçersiz veya süresi dolmuş refresh token.");

            return await GenerateTokensAsync(user);
        }

        public async Task RevokeTokenAsync(string refreshToken)
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            var user = users.FirstOrDefault(x => x.RefreshToken == refreshToken);

            if (user == null)
                throw new Exception("Geçersiz refresh token.");

            user.RefreshToken = null;
            user.RefreshTokenExpiry = null;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }

        // Token üretip kullanıcıya kaydet
        private async Task<AuthResponseDto> GenerateTokensAsync(User user)
        {
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(
                _jwtSettings.RefreshTokenExpirationDays);

            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(
                    _jwtSettings.AccessTokenExpirationMinutes)
            };
        }
    }
}
