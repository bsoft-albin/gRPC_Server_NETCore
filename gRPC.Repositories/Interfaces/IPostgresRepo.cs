using gRPC.Models;

namespace gRPC.Repositories.Interfaces
{
    public interface IPostgresRepo
    {
        Task<List<SampleUsers>> GetgRPCListAsync();
        Task<SampleUsers> GetgRPCSingleAsync(int id);
        Task<string> GetgRPCSingleNameAsync(int id);
        Task<int> PostgRPCSingleAsync(SampleUsers sampleUsers);
    }
}
