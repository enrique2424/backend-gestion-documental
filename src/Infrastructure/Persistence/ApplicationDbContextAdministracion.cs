
using Application.Interfaces.IData;
using Dapper;
using Domain.Enums;
using Domain.Models.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections;
using System.Data;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContextAdministracion : IApplicationDbContextAdministracion
    {
        private IConfiguration _appSettingsInstance;

        public ApplicationDbContextAdministracion(IConfiguration appSettingsInstance)
        {
            _appSettingsInstance = appSettingsInstance;

        }

        public async Task<T> TraerObjeto<T>(string nombreProcedimiento, Hashtable parametros)
        {
            try
            {
                var datos = default(T);


                DynamicParameters dynamicParameters = new DynamicParameters();

                foreach (DictionaryEntry item in parametros ?? new Hashtable())
                {
                    dynamicParameters.Add(item.Key.ToString(), item.Value);
                }

                using (IDbConnection cnx = this.ConexionPGSQLAdministracion)
                {
                    datos = await cnx.QuerySingleOrDefaultAsync<T>(
                        sql: nombreProcedimiento,
                        param: dynamicParameters,
                        commandType: CommandType.StoredProcedure
                    );
                    cnx.Close();
                }

                return datos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> TraerArrayObjeto<T>(string? nombreProcedimiento, Hashtable? parametros)
        {
            try
            {
                IEnumerable<T> datos = new T[] { };

                DynamicParameters dynamicParameters = new DynamicParameters();

                foreach (DictionaryEntry item in parametros ?? new Hashtable())
                {
                    dynamicParameters.Add(item.Key.ToString(), item.Value);
                }

                using (IDbConnection cnx = this.ConexionPGSQLAdministracion)
                {
                    datos = await cnx.QueryAsync<T>(
                        sql: nombreProcedimiento,
                        param: dynamicParameters,
                        commandType: CommandType.StoredProcedure
                    );
                    cnx.Close();

                }

                return datos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<RespuestaDB> AbmObjeto<T>(string nombreProcedimiento, AbmAccion accion, T objeto)
        {
            try
            {
                RespuestaDB respuestaDB = new RespuestaDB();
                DynamicParameters dynamicParameters = new DynamicParameters();
                PropertyInfo[] arrayProperties = typeof(T).GetProperties();

                Hashtable parametros = new Hashtable();
                // parametros.Add("_num_sec", numSec);


                dynamicParameters.Add("accion", (int)accion);

                foreach (PropertyInfo item in arrayProperties)
                {
                    object? obj = item.GetValue(objeto, null);
                    // dynamicParameters.Add($"_{item.Name}", item.GetValue(objeto, null));
                    dynamicParameters.Add($"_{item.Name}", obj);
                }

                using (IDbConnection cnx = this.ConexionPGSQLAdministracion)
                {
                        respuestaDB = await cnx.QuerySingleOrDefaultAsync<RespuestaDB>(
                        sql: nombreProcedimiento,
                        param: dynamicParameters,
                        commandType: CommandType.StoredProcedure
                    );
                    cnx.Close();
                }

                return respuestaDB;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<RespuestaDB> EjecutarProcConParametros(string nombreProcedimiento, object parametros)
        {
            try
            {
                RespuestaDB respuestaDB = new RespuestaDB();
                DynamicParameters dynamicParameters = new DynamicParameters();
                Type auxType = parametros.GetType();
                IList<PropertyInfo> arrayProperties = new List<PropertyInfo>(auxType.GetProperties());

                foreach (PropertyInfo item in arrayProperties)
                {
                    dynamicParameters.Add(item.Name, item.GetValue(parametros, null));
                }

                using (IDbConnection cnx = this.ConexionPGSQLAdministracion)
                {
                    respuestaDB = await cnx.QuerySingleOrDefaultAsync<RespuestaDB>(
                        sql: nombreProcedimiento,
                        param: dynamicParameters,
                        commandType: CommandType.StoredProcedure
                    );
                    cnx.Close();
                }

                return respuestaDB;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<RespuestaDB> AbmEliminar<T>(string nombreProcedimiento, AbmAccion accion, long num_sec)
        {
            try
            {
                RespuestaDB respuestaDB = new RespuestaDB();
                DynamicParameters dynamicParameters = new DynamicParameters();
                PropertyInfo[] arrayProperties = typeof(T).GetProperties();

                Hashtable parametros = new Hashtable();

                dynamicParameters.Add("accion", (int)accion);
                dynamicParameters.Add("_num_sec", num_sec);


                using (IDbConnection cnx = this.ConexionPGSQLAdministracion)
                {
                    respuestaDB = await cnx.QuerySingleOrDefaultAsync<RespuestaDB>(
                        sql: nombreProcedimiento,
                        param: dynamicParameters,
                        commandType: CommandType.StoredProcedure
                    );
                    cnx.Close();
                }

                return respuestaDB;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IDbConnection ConexionPGSQLAdministracion
        {
            get
            {
                return new NpgsqlConnection(_appSettingsInstance.GetConnectionString("ConexionAdmin"));

            }
        }
    }
}
