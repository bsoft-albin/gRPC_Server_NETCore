using gRPC.Models;

namespace gRPC.Repositories.Interfaces
{
    public interface IPostgresRepo
    {
        Task<List<SampleUsers>> GetgRPCListAsync();
    }
}
