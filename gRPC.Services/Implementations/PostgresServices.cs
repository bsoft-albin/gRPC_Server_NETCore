using gRPC.Models;
using gRPC.Repositories.Interfaces;
using gRPC.Services.Interfaces;

namespace gRPC.Services.Implementations
{
    public class PostgresServices(IPostgresRepo _repo) : IPostgresServices
    {
        private readonly IPostgresRepo repo = _repo;
        public async Task<List<SampleUsers>> GetgRPCListAsync()
        {
           return await repo.GetgRPCListAsync();
        }

        public async Task<SampleUsers> GetgRPCSingleAsync(int id)
        {
            return await repo.GetgRPCSingleAsync(id);
        }

        public async Task<int> PostgRPCSingleAsync(SampleUsers sampleUsers)
        {
            return await repo.PostgRPCSingleAsync(sampleUsers);
        }

        public async Task<string> GetgRPCSingleNameAsync(int id)
        {
            return await repo.GetgRPCSingleNameAsync(id);
        }
    }
}
