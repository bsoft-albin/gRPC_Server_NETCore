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
            List<SampleUsers> users = new();  
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

        public async Task<SampleUsers> GetgRPCSingleAsync(int id)
        {
            SampleUsers sampleUsers = new SampleUsers();
            try
            {
                sampleUsers = await postgres.QuerySingleAsync<SampleUsers>($"select * from public.users where id = {id};");
            }
            catch (Exception x)
            {
               await ErrorLogger.WriteLog(x);
            }

            return sampleUsers;
        }

        public async Task<int> PostgRPCSingleAsync(SampleUsers sampleUsers)
        {
            int result = 0;
            try
            {
                result = await postgres.ExecuteScalarAsync<int>($"insert into public.users ('name', 'age') values ({sampleUsers.name},{sampleUsers.age});");
            }
            catch (Exception x)
            {
                await ErrorLogger.WriteLog(x);
            }

            return result;
        }

        public async Task<string> GetgRPCSingleNameAsync(int id)
        {
            string UserName = "";
            try
            {
                UserName = await postgres.ExecuteScalarAsync<string>($"select name from public.users where id = {id};");
            }
            catch (Exception x)
            {
                await ErrorLogger.WriteLog(x);
            }

            return UserName;
        }
    }
}
