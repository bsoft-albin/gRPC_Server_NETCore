using gRPC.Models;

namespace gRPC.Services.Interfaces
{
    public interface IPostgresServices
    {
        Task<List<SampleUsers>> GetgRPCListAsync();
    }
}
