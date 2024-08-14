using Application.DTOs.GestionDocumental;
using Application.Interfaces.IRepositories.GestionDocumental;
using Application.Interfaces.IServices.GestionDocumental;
using Application.Services.Common;
using Application.Services.gestionDocumental;
using Application.Utils;
using Domain.Models.Data;
using Domain.Models.gestionDocumental;
using Domain.Models.GestionDocumental;
using Microsoft.AspNetCore.Http;
using SharpCompress.Archives;

namespace Application.Services.GestionDocumental
{

    public class AdjuntoService : GenericService<Adjunto>, IAdjuntoService
    {
        private readonly IAdjuntoRepository _adjuntoRepository;
        private readonly ArchivoMongoService _archivoMongo;
        private readonly IFichero _fichero;
        private readonly IDetAdjuntoTablaRepository _DetalleAdjuntoArchivoRepository;
        private readonly IDetSeccionFiltroBusquedaRepository _DetSeccionFiltroBusquedaRepository;

        public AdjuntoService(IAdjuntoRepository adjuntoRepository, ArchivoMongoService archivoMongo, IFichero fichero, IDetAdjuntoTablaRepository detalleArchivoTablaRepository, IDetSeccionFiltroBusquedaRepository detSeccionFiltroBusquedaRepository) : base(adjuntoRepository)
        {
            _adjuntoRepository = adjuntoRepository;
            this._archivoMongo = archivoMongo;
            this._fichero = fichero;
            this._DetalleAdjuntoArchivoRepository = detalleArchivoTablaRepository;
            this._DetSeccionFiltroBusquedaRepository = detSeccionFiltroBusquedaRepository;
        }

        public async Task<RespuestaListado<AdjuntoDto>> BuscarListado(string? valor, string? parametro, int numeroPagina, int cantidadMostrar)
        {
            var respuestaListado = new RespuestaListado<AdjuntoDto>()
            {
                response = await _adjuntoRepository.BuscarListado(valor, parametro, numeroPagina, cantidadMostrar),
                status = Status.Success
            };

            if (respuestaListado.response.Count() > 0)
            {
                int elementosTotales = respuestaListado.response.ElementAt(0).total;
                respuestaListado.total = elementosTotales;
            }

            return respuestaListado;
        }

        public async Task<RespuestaDB> PostArchivoMongo(PostArchivo datos) //aqui formData del modal
        {
            RespuestaDB respuestaBD = new RespuestaDB();
            RespuestaDB respuestaBDdetArchivo = new RespuestaDB();
            RespuestaDB respuestaBD_detalle = new RespuestaDB();

            
            var archivo = new Adjunto();
            
            if (datos.archivo is null)
            {
                archivo.num_sec = 0;
                archivo.nsec_usuario_registro = datos.nsec_usuario;
                archivo.estado = "AC";
                archivo.nombre = null;// nombre_documento?[0].ToString(); 
                archivo.nombre_fisico = null;
                archivo.tamano = 0;
                archivo.nsec_volumen = datos?.nsec_volumen;
                archivo.content_type = null;
                archivo.foja = datos.foja;
              



            }
            


            else
            {
                
                string nombre_documento = datos?.archivo![0].FileName!;
                string[] _name = nombre_documento.Split('.');
                archivo.num_sec = 0;
                archivo.nsec_usuario_registro = datos!.nsec_usuario;
                archivo.estado = "AC";
                archivo.nombre = _name[0].ToString();// nombre_documento?[0].ToString(); 
                archivo.nombre_fisico = datos?.archivo?[0].FileName;
                archivo.tamano = (int)datos?.archivo![0].Length!;
                archivo.nsec_volumen = datos?.nsec_volumen;
                archivo.content_type = datos?.archivo?[0].ContentType;
                archivo.foja = datos.foja;
            }



            respuestaBD = await this.PostArchivo(archivo);
            // nsec_tbla =  nsec_seccion, numsec = nsec_adjunto
            if (respuestaBD.status == "error")
            {
                return respuestaBD;
            }

            var nsec_seccion = respuestaBD.nsec_tabla;
            var detalle = new DetAdjuntoTabla();
            detalle.nsec_adjunto = long.Parse(respuestaBD.numsec!);
            detalle.nsec_tabla = 1;//sga_seccion
            detalle.nsec_seccion_documento = long.Parse(nsec_seccion!);
            detalle.nsec_usuario_registro = datos!.nsec_usuario;
            detalle.num_sec = 0;
            detalle.estado = "AC";
            respuestaBDdetArchivo = await this._DetalleAdjuntoArchivoRepository.Guardar(detalle);

            if (respuestaBDdetArchivo.status == "error")
            {
                return respuestaBDdetArchivo;
            }

            //guardar del_seccion_filtro_busqueda
            foreach (DetSeccionFiltroBusqueda Det in datos.arrayFiltroBusqueda)
            {
                DetSeccionFiltroBusqueda det_eccion = new DetSeccionFiltroBusqueda();
                det_eccion.num_sec = "0";
                det_eccion.nsec_seccion = nsec_seccion;
                det_eccion.nsec_filtro_busqueda = Det.nsec_filtro_busqueda;
                det_eccion.descripcion = "";
                det_eccion.valor = Det.valor;
                det_eccion.nsec_usuario_registro = datos.nsec_usuario;
                det_eccion.estado = "AC";
                respuestaBD_detalle = await _DetSeccionFiltroBusquedaRepository.Guardar(det_eccion);
                if (respuestaBD_detalle.status == "error")
                {
                    return respuestaBD_detalle;
                }

            }
            //Guardar Archivo mongoDB
            if (datos.archivo is null)
            {
                return respuestaBD;
            }
            string extension = _fichero.ObtenerExtensionArchivo(datos.archivo[0].FileName);
            string nombreArchivo = respuestaBD.nsec_tabla + "." + extension; ;
            ArchivoMongo archivoMongo = new ArchivoMongo();
            archivoMongo.num_sec = respuestaBD.numsec;
            archivoMongo.binary_data = fileToArrayByte(datos.archivo[0]);
            archivoMongo.nsec_seccion_documento = "123";
            archivoMongo.content_type = datos.archivo?[0].ContentType;
            archivoMongo.nombre = nombreArchivo;

            bool resp = await this._archivoMongo.CreateArchivo(archivoMongo);
            if (!resp)
            {
                return new RespuestaDB
                {
                    status = "error",
                    response = "Error al guardar el archivo"
                };
            }

            return respuestaBD;

        }
        public static byte[] fileToArrayByte(IFormFile file)
        {
            byte[] fileByte = null;
            if (file != null)
            {
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                fileByte = ms.ToArray();
            }
            return fileByte;
        }
        public Task<RespuestaDB> PostArchivo(Adjunto datos)
        {
            
            return _adjuntoRepository.PostArchivo(datos);
        }

        public async Task<RespuestaDB> PutArchivoMongo(PostArchivo datos)
        {
            RespuestaDB respuestaBD = new RespuestaDB();
            RespuestaDB respuestaBDdetArchivo = new RespuestaDB();
            RespuestaDB respuestaBD_detalle = new RespuestaDB();

            var nsec_adjunto = datos.arrayFiltroBusqueda[0].num_sec;
            var archivo = new Adjunto();
            if (datos.archivo is not null)
            {
                string nombre_documento = datos?.archivo![0].FileName!;
                string[] _name = nombre_documento.Split('.');
                archivo.num_sec = long.Parse(nsec_adjunto!);
                archivo.nsec_usuario_registro = datos!.nsec_usuario;
                archivo.estado = "AC";
                archivo.nombre = _name[0]?.ToString();// nombre_documento?[0].ToString(); 
                archivo.nombre_fisico = datos?.archivo?[0].FileName;
                archivo.tamano = (int)datos?.archivo![0].Length!;
                archivo.nsec_volumen = datos?.nsec_volumen;
                archivo.content_type = datos?.archivo?[0].ContentType;
                archivo.foja = datos?.foja;

                //respuestaBD = await _adjuntoRepository.PutArchivo(archivo);
                //// nsec_tbla =  nsec_seccion, numsec = nsec_adjunto
                //if (respuestaBD.status == "error")
                //{
                //    return respuestaBD;
                //}
            }

            else
            {
                archivo.num_sec = long.Parse(nsec_adjunto!);
                archivo.nsec_usuario_registro = datos.nsec_usuario;
                archivo.estado = "AC";
                archivo.nombre = null;// nombre_documento?[0].ToString(); 
                archivo.nombre_fisico = null;
                archivo.tamano = 0;
                archivo.nsec_volumen = datos?.nsec_volumen;
                archivo.content_type = null;
                archivo.foja = datos.foja;
                archivo.nsec_seccion = datos?.nsec_seccion;


            }

            respuestaBD = await _adjuntoRepository.PutArchivo(archivo);
            // nsec_tbla =  nsec_seccion, numsec = nsec_adjunto
            if (respuestaBD.status == "error")
            {
                return respuestaBD;
            }



            //var nsec_seccion = respuestaBD.nsec_tabla;
            //var detalle = new DetAdjuntoTabla();
            //detalle.nsec_adjunto = long.Parse(respuestaBD.numsec!);
            //detalle.nsec_tabla = 1;//sga_seccion
            //detalle.nsec_seccion_documento = long.Parse(nsec_seccion!);
            //detalle.nsec_usuario_registro = datos!.nsec_usuario;
            //detalle.num_sec = 0;
            //detalle.estado = "AC";
            //respuestaBDdetArchivo = await this._DetalleAdjuntoArchivoRepository.Guardar(detalle);

            //if (respuestaBDdetArchivo.status == "error")
            //{
            //    return respuestaBDdetArchivo;
            //}

            //guardar del_seccion_filtro_busqueda
            foreach (DetSeccionFiltroBusqueda Det in datos!.arrayFiltroBusqueda)
            {
                
                DetSeccionFiltroBusqueda det_eccion = new DetSeccionFiltroBusqueda();
                det_eccion.num_sec = "0";
                det_eccion.nsec_seccion = Det.nsec_seccion;
                det_eccion.nsec_filtro_busqueda = Det.nsec_filtro_busqueda;
                det_eccion.descripcion = "";
                det_eccion.valor = Det.valor;
                det_eccion.nsec_usuario_registro = datos.nsec_usuario;
                det_eccion.estado = "AC";
                respuestaBD_detalle = await _DetSeccionFiltroBusquedaRepository.Modificar(det_eccion);
                if (respuestaBD_detalle.status == "error")
                {
                    return respuestaBD_detalle;
                }
                respuestaBD.nsec_tabla = Det.nsec_seccion;
                respuestaBD.numsec = nsec_adjunto;

            }
            respuestaBD.status = "true";
            //Guardar Archivo mongoDB
            if (datos.archivo is null)
            {
                return respuestaBD;
            }
            string extension = _fichero.ObtenerExtensionArchivo(datos.archivo[0].FileName);
            string nombreArchivo = respuestaBD.nsec_tabla + "." + extension; ;
            ArchivoMongo archivoMongo = new ArchivoMongo();
            archivoMongo.num_sec = respuestaBD.numsec;
            archivoMongo.binary_data = fileToArrayByte(datos.archivo[0]);
            archivoMongo.nsec_seccion_documento = nsec_adjunto;
            archivoMongo.content_type = datos.archivo?[0].ContentType;
            archivoMongo.nombre = nombreArchivo;

            bool resp = await this._archivoMongo.CreateArchivo(archivoMongo);
            if (!resp)
            {
                return new RespuestaDB
                {
                    status = "error",
                    response = "Error al guardar el archivo"
                };
            }

            return respuestaBD;
        }

       
    }

}

