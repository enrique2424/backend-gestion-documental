using Domain.Models.gestionDocumental;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Application.Services.gestionDocumental
{
    public class ArchivoMongoService
    {
        private readonly IMongoCollection<ArchivoMongo> archivos;
        internal IGridFSBucket bucket;
        private IConfiguration appSettingsInstance;
        private MongoClient client;
       
        public ArchivoMongoService()
        {
            appSettingsInstance = new ConfigurationBuilder()
                                      .SetBasePath(Directory.GetCurrentDirectory())
                                      .AddJsonFile("appsettings.json").Build();

            client = new MongoClient(appSettingsInstance.GetConnectionString("mongoDb"));
            IMongoDatabase database = client.GetDatabase("gestion-documental");
            bucket = new GridFSBucket(database);
            archivos = database.GetCollection<ArchivoMongo>("fs.files");
        }

        // public List<ArchivoMongo> Get()
        // {
        //     return archivos.Find(archivo => true).ToList();
        // }

        public async Task<byte[]> Get(string nombreArchivo)
        {
            // return (await archivos.FindAsync(archivo => archivo.nombre == id)).FirstOrDefault();
            return await this.bucket.DownloadAsBytesByNameAsync(nombreArchivo);
        }

        public async Task<bool> CreateArchivo(ArchivoMongo archivo)
        {
            try
            {
                // archivos.InsertOne(archivo);
                ObjectId? idCreado = await this.bucket.UploadFromBytesAsync(archivo.nombre, archivo.binary_data);


                // usar en caso de actualizacion e insercion si no existe
                //archivos.ReplaceOne(doc => doc.num_sec == archivo.num_sec, archivo, new ReplaceOptions { IsUpsert = true });
                if (idCreado != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                //  throw ex;
                return false;
            }

        }
        public async Task<bool> CreateAsync(ArchivoMongo archivo)
        {
            //try
            //{
            //    RespuestaDB respuesta = new RespuestaDB();
            //    await archivos.InsertOneAsync(archivo);


            //    return respuesta;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            using (var session = await client.StartSessionAsync())
            {
                // Begin transaction
                session.StartTransaction();

                try
                {
                    // await archivos.InsertOneAsync(archivo);
                    await archivos.ReplaceOneAsync(doc => doc.num_sec == archivo.num_sec, archivo, new ReplaceOptions { IsUpsert = true });
                    await session.CommitTransactionAsync();

                }
                catch (Exception e)
                {
                    Console.WriteLine("Error writing to MongoDB: " + e.Message);
                    await session.AbortTransactionAsync();
                    return false;
                }

                return true;
            }

        }

        public void Update(string id, ArchivoMongo archivoIn)
        {
            archivos.ReplaceOne(archivo => archivo.Id == id, archivoIn);
        }

        public void Remove(ArchivoMongo archivoIn)
        {
            archivos.DeleteOne(car => car.Id == archivoIn.Id);
        }

        public void Remove(string id)
        {
            archivos.DeleteOne(archivo => archivo.Id == id);
        }
    }
}
