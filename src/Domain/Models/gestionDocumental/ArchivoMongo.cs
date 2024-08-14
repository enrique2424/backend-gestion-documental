using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.gestionDocumental
{
    public class ArchivoMongo
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("num_sec")]
        [Required]
        public string? num_sec { get; set; }

        [BsonElement("nombre")]
        [Required]
        public string? nombre { get; set; }


        [BsonElement("content_type")]
        [Required]
        public string? content_type { get; set; }

        [BsonElement("nsec_seccion_documento")]
        [Required]
        public string? nsec_seccion_documento { get; set; }

        

        [BsonElement("binary_data")]
        [Required]
        public byte[]? binary_data { get; set; }

    }
}
