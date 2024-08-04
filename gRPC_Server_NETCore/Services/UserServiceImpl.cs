using gRPC.Models;
using gRPC.Services.Interfaces;
using Grpc.Core;
using gRPC_Server_NETCore.Protos;

namespace gRPC_Server_NETCore.Services
{
    public class UserServiceImpl(IPostgresServices services) : UserService.UserServiceBase
    {
        private readonly IPostgresServices _services = services;

        public override async Task<GetUserListResponse> GetUserListFromDB(GetUserListRequest request, ServerCallContext context)
        {
            List<SampleUsers> sampleUsers = await _services.GetgRPCListAsync();
            var response = new GetUserListResponse();

            // Map the data to the gRPC response format
            foreach (var sampleUser in sampleUsers)
            {
                response.Users.Add(new User
                {
                    Id = sampleUser.id,
                    Name = sampleUser.name,
                    Age = sampleUser.age
                });
            }

            return response;
        }

        public override async Task<GetUserResponse> GetSingleUserFromDB(GetUserRequest request, ServerCallContext context)
        {
            SampleUsers sampleUser = await _services.GetgRPCSingleAsync(request.UserId);
            var response = new GetUserResponse();
            response.User = new User();
            // Map the data to the gRPC response format
            response.User.Id = sampleUser.id;
            response.User.Name = sampleUser.name;
            response.User.Age = sampleUser.age;

            return response;
        }

        public override async Task<GetUserNameResponse> GetSingleUserNameFromDB(GetUserRequest request, ServerCallContext context)
        {
            string sampleUser = await _services.GetgRPCSingleNameAsync(request.UserId);
            var response = new GetUserNameResponse();

            // Map the data to the gRPC response format
            response.UserName = sampleUser;

            return response;
        }

        public override async Task<PostUserResponse> PostNewUserToDb(User request, ServerCallContext context)
        {
            var obj = new SampleUsers
            {
                age = request.Age,
                name = request.Name,
                id = request.Id,
            };
            int sampleUser = await _services.PostgRPCSingleAsync(obj);
            var response = new PostUserResponse();

            // Map the data to the gRPC response format
            response.UserMyId = sampleUser;

            return response;
        }

        // Sample methods with static data
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

        public override Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            // Example implementation
            var user = new User { Id = request.UserId, Name = "John Doe", Age = 30 };

            var response = new GetUserResponse { User = user };
            return Task.FromResult(response);
        }
    }
}
