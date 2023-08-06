using LanguageExt.Common;
using MediatR;
using UserService.Api.Models;

namespace UserService.Api.Queries
{
    public class GetUserByIdQuery : IRequest<Result<UserResponse>>
    {
        public readonly string? Id = null;
        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
    }
}
