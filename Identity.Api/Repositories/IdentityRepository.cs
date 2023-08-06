﻿using Identity.Api.Exceptions;
using Identity.Api.Interfaces;
using Identity.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Api.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private const string TokenSecret = "ahdbhdcbudababubaufdafudbudahfouabcuda";
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(6);
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IdentityRepository> _logger;
        public IdentityRepository(ApplicationDbContext context, ILogger<IdentityRepository> logger)
        {
            _context = context;
            _logger = logger;

        }

        public async Task<User> ValidateUser(string username, string password) 
        {
            User? user = await _context.Users.SingleOrDefaultAsync(u => u.Username == username || u.Email == username);
            if (user == null)
            {
                _logger.LogError($"{username} is not an regestred user");
                throw new UserNotFoundException(username);
            }
            if (user.Password != password)
            {
                _logger.LogError($"Login attemted with invalid credentials {username} : {password}");
                throw new UserNotAuthorizedException(password);
            }
            return user;
        }

        public async Task<string> GetToken(string username, string password)
        {
            try
            {
                _logger.LogInformation("Validating Input Credentials");
                User user = await ValidateUser(username, password);
                var x = GenerateJwtToken(user);
                _logger.LogInformation("Successfully Validated Credentials, Token Generated");
                return x;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GenerateJwtToken(User user)
        {
            var role = (bool)user.Admin! ? "Admin" : (bool)user.Vendor! ? "Vendor" : "";
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(TokenSecret);
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email!));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
            claims.Add(new Claim(ClaimTypes.Role, role));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.Firstname} {user.Lastname}"));
            claims.Add(new Claim("userId", user.UserId!));

            var identity = new ClaimsIdentity(claims);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.Add(TokenLifetime),
                Issuer = "mehedi-somesite",
                Audience = "janina-bhai",
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
