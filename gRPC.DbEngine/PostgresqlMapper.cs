using static Dapper.SqlMapper;
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace gRPC.DbEngine
{
    public interface IPostgresMapper
    {
        IDbConnection Connection { get; }

        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text);  // return the Single row Data table values

        Task<T> QuerySingleAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text); // return the Data table with singel row oblject  with SP 
        Task<T> ExecuteScalarAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text);  // return the object values

        Task ExecuteAsync(string sql, object? parameters = null, CommandType commandType = CommandType.Text); // Insert, Update and Delete

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text);   // return the data table with more the one rows

        Task<GridReader> QueryMultipleAsync(string sql, object? parameters = null, CommandType commandType = CommandType.Text);  // return the Data Set values

        void ExecuteScript(string script);

        object ExecuteScalar(string script);
    }

    public class PostgresMapper : IPostgresMapper
    {
        private readonly IConfiguration _configuration;
        //private readonly IDbConnection _connection;
        public PostgresMapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection Connection
        {
            get
            {
               // return _connection;
               NpgsqlConnection postgres = new(_configuration.GetConnectionString("PostgresConnString"));
               return postgres;
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            using (Connection)
            {
#pragma warning disable
                return await Connection.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType);
            }
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            using (Connection)
            {
                return await Connection.QueryAsync<T>(sql, parameters, commandType: commandType, commandTimeout: 600);
            }
        }
        public async Task<T> QuerySingleAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text)
        {

            using (Connection)
            {
                return await Connection.QuerySingleAsync<T>(sql, parameters, commandType: commandType);
            }
        }
        public async Task<T> ExecuteScalarAsync<T>(string sql, object? parameters = null, CommandType commandType = CommandType.Text)
        {

            using (Connection)
            {
                return await Connection.ExecuteScalarAsync<T>(sql, parameters, commandType: commandType);
            }
        }
        public async Task ExecuteAsync(string sql, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            using (Connection)
            {
                await Connection.ExecuteAsync(sql, parameters, commandType: commandType);
            }
        }

        public async Task<GridReader> QueryMultipleAsync(string sql, object? parameters = null, CommandType commandType = CommandType.Text)
        {
            using (Connection)
            {
                return await Connection.QueryMultipleAsync(sql, parameters, commandType: commandType, commandTimeout: 180);
            }
        }

        public void ExecuteScript(string script)
        {
            try
            {
                using (Connection)
                {
                    Connection.Open();
                    Connection.Execute(script);
                }
            }
            catch (Exception x)
            {
                throw;
            }
        }
        public object ExecuteScalar(string script)
        {
            object? result = null;
            try
            {
                // code to execute script file in dapper
                using (Connection)
                {
                    Connection.Open();
                    result = Connection.ExecuteScalar(script);
                }
            }
            catch (Exception x)
            {
                throw;
            }
            return result;
        }


    }
}
