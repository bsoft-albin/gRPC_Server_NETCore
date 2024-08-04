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
    }
}
