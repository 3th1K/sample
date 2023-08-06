﻿using AutoMapper;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using UserService.Api.Interfaces;
using UserService.Api.Models;

namespace UserService.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(IMapper mapper, ApplicationDbContext context, ILogger<UserRepository> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }
        public async Task<UserResponse> CreateUser(User user)
        {
            user.UserId = Guid.NewGuid().ToString();
            user.RegisteredOn = DateTime.Now;
            user.Admin = false;
            user.Vendor = false;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var addedUser = await _context.Users.SingleOrDefaultAsync(u => u.UserId == user.UserId);
            return _mapper.Map<UserResponse>(addedUser);
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (user != null) 
            { 
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user!;
        }
        public async Task<User> DeleteUser(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user!;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            var userResponses = _mapper.Map<List<UserResponse>>(users);
            return userResponses;
        }
        public async Task<IEnumerable<User>> GetAllUsersDetails()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<UserResponse> GetUserById(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == id);
            var userResponse = _mapper.Map<UserResponse>(user);
            return userResponse;
        }

        public async Task<User> GetUserDetailsById(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == id);
            return user!;
        }
    }
}
