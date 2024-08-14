using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;
using Infrastructure.Persistence;
using Dapper;
using Domain.Models.Data;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class SgDocumentoIdentificacionRepository : ISgDocumentoIdentificacionRepository
    {
        private readonly PersonaContext _personaContext;
        public SgDocumentoIdentificacionRepository(PersonaContext personaContext)
        {
            _personaContext = personaContext;
        }

        public async Task<IEnumerable<SgDocumentoIdentificacionDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<SgDocumentoIdentificacionDto>? arrayDatos = new SgDocumentoIdentificacionDto[] { };
                string nombreProcedimiento = "sp_listado_sg_documento_identificacion";

                using (var cnx = this._personaContext.CreateConnection())
                {
                    arrayDatos = await cnx.QueryAsync<SgDocumentoIdentificacionDto>(
                        sql: nombreProcedimiento,
                        param: new
                        {
                            valor_bus = valor == null ? "" : valor,
                            parametro_bus = parametro,
                            numeropaginaactual = numeroPagina,
                            cantidadmostrar = cantidadMostrar
                        },
                        commandType: CommandType.StoredProcedure
                    );
                    cnx.Close();
                }

                return arrayDatos;


            }
            catch (Exception)
            {
                throw;
            }


        }

        public async Task<SgDocumentoIdentificacion> BuscarPorNumSec(long numSec)
        {
            try
            {
                SgDocumentoIdentificacion datos = new SgDocumentoIdentificacion();
                string nombreFuncion = "sp_traer_sg_documento_identificacion";

                using (IDbConnection cnx = this._personaContext.CreateConnection())
                {
                    datos = await cnx.QuerySingleOrDefaultAsync<SgDocumentoIdentificacion>(
                        sql: nombreFuncion,
                        commandType: CommandType.StoredProcedure,
                        param: new
                        {
                            _num_sec = numSec
                        }
                    );

                    cnx.Close();

                    return datos;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public Task<RespuestaDB> delete(long numSec)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDB> Guardar(SgDocumentoIdentificacion datos)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDB> Modificar(long numsec, SgDocumentoIdentificacion datos)
        {
            throw new NotImplementedException();
        }
    }
}

