using LanguageExt.Common;
using MediatR;
using UserService.Api.Models;

namespace UserService.Api.Queries
{
    public class GetUserDetailsByIdQuery : IRequest<Result<User>>
    {
        public readonly string? Id = null;
        public GetUserDetailsByIdQuery(string id)
        {
            Id = id;
        }
    }
}
