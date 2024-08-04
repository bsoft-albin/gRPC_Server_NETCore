using gRPC.Services.Interfaces;
using Grpc.Core;
using gRPC_Server_NETCore.Protos;

namespace gRPC_Server_NETCore.Services
{
    public class UserServiceImpl(IPostgresServices services) : UserService.UserServiceBase
    {
        private readonly IPostgresServices _services = services;
        public override Task<GetUserListResponse> GetUserList(GetUserListRequest request, ServerCallContext context)
        {
            // Example implementation
            var users = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Age = 22 },
                new User { Id = 2, Name = "Jane Doe", Age = 25 }
            };

            var response = new GetUserListResponse();
            response.Users.AddRange(users);

            return Task.FromResult(response);
        }

        //public override async Task<GetUserListResponse> GetUserListFromDB(GetUserListRequest request, ServerCallContext context)
        //{
        //    // Example implementation
        //    var users = new List<User>
        //    {
        //        new User { Id = 1, Name = "John Doe", Age = 22 },
        //        new User { Id = 2, Name = "Jane Doe", Age = 25 }
        //    };

        //    //await _services.GetgRPCListAsync();

        //    var response = new GetUserListResponse();
        //    response.Users.AddRange(users);

        //    return Task.FromResult(response);
        //}

        public override Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            // Example implementation
            var user = new User { Id = request.UserId, Name = "John Doe", Age = 30 };

            var response = new GetUserResponse { User = user };
            return Task.FromResult(response);
        }
    }
}
