using gRPC.DbEngine;
using gRPC.Frameworks.CommonMeths;
using gRPC.Models;
using gRPC.Repositories.Interfaces;

namespace gRPC.Repositories.Implementations
{
    public class PostgresRepo(IPostgresMapper _postgres) : IPostgresRepo
    {
        private readonly IPostgresMapper postgres = _postgres;
        public async Task<List<SampleUsers>> GetgRPCListAsync()
        {
            List<SampleUsers> users = new List<SampleUsers>();  
            try
            {
                users = (await postgres.QueryAsync<SampleUsers>("select * from public.users;")).ToList();
            }
            catch (Exception x)
            {
               await ErrorLogger.WriteLog(x);
            }

            return users;
        }
    }
}
