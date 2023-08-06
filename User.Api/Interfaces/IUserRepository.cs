using LanguageExt.Common;
using UserService.Api.Models;

namespace UserService.Api.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<UserResponse>> GetAllUsers();
        public Task<IEnumerable<User>> GetAllUsersDetails();
        public Task<UserResponse> GetUserById(string id);
        public Task<User> GetUserDetailsById(string id);
        public Task<UserResponse> CreateUser(User user);
        public Task<User> DeleteUser(int id);
        public Task<User> DeleteUser(string id);
    }
}
