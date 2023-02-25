using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.ResponseEntities
{
    public class UserRepositoryResponse
    {
        public UserRepositoryResponse(User? user, EAuthorizationStatus authorizationStatus)
        {
            User = user;
            AuthorizationStatus = authorizationStatus;
        }

        public User? User { get; set; }

        public EAuthorizationStatus AuthorizationStatus { get; set; }
    }
}
