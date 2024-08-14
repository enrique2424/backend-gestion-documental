using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class PersonaContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public PersonaContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ConexionPersona");
        }
        public IDbConnection CreateConnection()
            => new NpgsqlConnection(_connectionString);
    }
}
