using gRPC.Models;

namespace gRPC.Services.Interfaces
{
    public interface IPostgresServices
    {
        Task<List<SampleUsers>> GetgRPCListAsync();
        Task<SampleUsers> GetgRPCSingleAsync(int id);
        Task<string> GetgRPCSingleNameAsync(int id);
        Task<int> PostgRPCSingleAsync(SampleUsers sampleUsers);
    }
}
