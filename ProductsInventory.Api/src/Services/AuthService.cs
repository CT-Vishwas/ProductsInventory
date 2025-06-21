using AutoMapper;
using ProductsInventory.Api.Data.DTOs;
using ProductsInventory.Api.Data.Entities;
using ProductsInventory.Api.Data.Repositories;
using ProductsInventory.Api.Data.Requests;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductsInventory.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(IAuthRepository authRepository, IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null");
            _configuration = configuration;
            _authRepository = authRepository;
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {

            var user = await _authRepository.GetUserByUsernameAsync(username);
            return _mapper.Map<UserDto>(user);
        }

        public Task<bool> IsUserExistsAsync(string username)
        {
            return _authRepository.IsUserExistsAsync(username);
        }

        public async Task<UserDto> LoginAsync(UserRequest userRequest)
        {
            var user =  _authRepository.GetUserByUsernameAsync(userRequest.Username).Result;

            if (user == null || !BCrypt.Net.BCrypt.Verify(userRequest.Password, user.PasswordHash))
            {
                throw new KeyNotFoundException("User not found");
            }

            var token = GenrateJwtToken(user);
            var userDto = new UserDto { Id = user.Id, Role = user.Role, Username = user.Username, token = token };
            return userDto;
        }

        public async Task<UserDto> RegisterAsync(UserRequest userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto), "User DTO cannot be null");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var newuser = new User
            {
                Username = userDto.Username,
                PasswordHash = hashedPassword,
                Role = userDto.Role
            };

            var createdUser = await _authRepository.RegisterAsync(newuser);

            return _mapper.Map<UserDto>(createdUser);

        }


        public async Task<UserDto> GetById(Guid id) {
            var user = await _authRepository.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        
         private string GenrateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["key"]));
            
            
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        
    }
}