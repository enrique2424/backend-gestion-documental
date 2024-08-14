using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IData;
using Domain.Models.GestionDocumental;
using Infrastructure.Repositories.Common;
using System.Data;
using System.Collections;
using Infrastructure.Persistence;
using Dapper;

namespace Infrastructure.Repositories.GestionDocumental
{
    public class SgEstadoCivilRepository: ISgEstadoCivilRepository
    {
        private readonly PersonaContext _personaContext;
        public SgEstadoCivilRepository(PersonaContext personaContext)
        {
            _personaContext = personaContext;
        }

        public async Task<IEnumerable<SgEstadoCivilDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<SgEstadoCivilDto>? arrayDatos = new SgEstadoCivilDto[] { };
                string nombreProcedimiento = "sp_listado_sg_estado_civil";

                using (var cnx = this._personaContext.CreateConnection())
                {
                    arrayDatos = await cnx.QueryAsync<SgEstadoCivilDto>(
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

        public async  Task<SgEstadoCivil> BuscarPorNumSec(long numSec)
        {
            try
            {
                SgEstadoCivil datos = new SgEstadoCivil();
                string nombreFuncion = "sp_traer_sg_estado_civil";

                using (IDbConnection cnx = this._personaContext.CreateConnection())
                {
                    datos = await cnx.QuerySingleOrDefaultAsync<SgEstadoCivil>(
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
    }
}

