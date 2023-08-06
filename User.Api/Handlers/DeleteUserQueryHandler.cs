using LanguageExt.Common;
using MediatR;
using UserService.Api.Interfaces;
using UserService.Api.Models;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class DeleteUserQueryHandler : IRequestHandler<DeleteUserQuery, Result<User>>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<User>> Handle(DeleteUserQuery request, CancellationToken cancellationToken)
        {
            var type = request.Id.GetType().Name.ToString();
            
            if (type == "String") 
            {
                return await _userRepository.DeleteUser((string)request.Id);
            }
            else if (type == "Int32")
            {
                return await _userRepository.DeleteUser((int)request.Id);
            }
            return null!;
        }
    }
}
