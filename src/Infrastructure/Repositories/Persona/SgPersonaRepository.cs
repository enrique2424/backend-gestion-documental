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
    public class SgPersonaRepository : ISgPersonaRepository
    {

        private readonly PersonaContext _personaContext;
        public SgPersonaRepository(PersonaContext personaContext) 
        {
            _personaContext = personaContext;
        }

        public async Task<IEnumerable<SgPersonaDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            try
            {
                IEnumerable<SgPersonaDto>? arrayDatos = new SgPersonaDto[] { };
                string nombreProcedimiento = "sp_listado_persona";
           

                using (var cnx = this._personaContext.CreateConnection())
                {
                    arrayDatos = await cnx.QueryAsync<SgPersonaDto>(
                        sql: nombreProcedimiento,
                        param: new
                        {
                            valor_bus = valor == null ? "" : valor,
                            parametro_bus =  parametro,
                            numeropaginaactual = numeroPagina,
                            cantidadmostrar = cantidadMostrar
                        },
                        commandType: CommandType.StoredProcedure
                    ) ;
                    cnx.Close();
                }

                return arrayDatos;


            }
            catch (Exception)
            {
                throw;
            }

            
        }

        public async Task<SgPersona> BuscarPorNumSec(string numSec)
        {
            try
            {
                SgPersona datos = new SgPersona();
                string nombreFuncion = "sp_traer_sg_persona";

                using (IDbConnection cnx = this._personaContext.CreateConnection())
                {
                    datos = await cnx.QuerySingleOrDefaultAsync<SgPersona>(
                        sql: nombreFuncion,
                        commandType: CommandType.StoredProcedure,
                        param: new
                        {
                            _num_sec = long.Parse(numSec)
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

        public Task<RespuestaDB> Eliminar(long numSec)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaDB> Guardar(SgPersona datos)
        {
            try
            {
                RespuestaDB respuesta = new RespuestaDB();
                string nombreFuncion = "sp_abm_sg_persona";

                using (IDbConnection cnx = this._personaContext.CreateConnection())
                {

                    respuesta = await cnx.QuerySingleOrDefaultAsync<RespuestaDB>(
                      sql: nombreFuncion,
                      commandType: CommandType.StoredProcedure,
                      param: new
                      {
                          accion = 1,
                          _num_sec          = long.Parse("0"),
                          _nombre           = datos.nombre,
                          _primer_apellido  = datos.primer_apellido,
                          _segundo_apellido = datos.segundo_apellido,
                          _tercer_apellido  = datos.tercer_apellido,
                          _sexo             = datos.sexo,
                          _fecha_nacimiento = datos.fecha_nacimiento,
                          _direccion        = datos.direccion,
                          _descripcion_direccion =datos.descripcion_direccion,
                          _celular          = datos.celular,
                          _telefono_domicilio = datos.telefono_domicilio,
                          _correo             = datos.correo,
                          _numero_documento_identificacion = datos.numero_documento_identificacion,
                          _nsec_documento_identificacion   =long.Parse("1"),
                          _nsec_estado_civil = long.Parse("4"),
                          _nsec_usuario_creacion=long.Parse("1"),
                          _estado = datos.estado,
                          _codigo_funcionario = datos.codigo_funcionario

                      }
                  );
                    cnx.Close();
                }

                return respuesta;

            }
            catch (Exception )
            {
                throw ;
            }
        }
    }
}

