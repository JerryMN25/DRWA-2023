using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models;

public class PresensiMengajar
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [Required]
    public decimal? NIP { get; set; } = null!;
    
    public string Tgl { get; set; } = null!;

    public string Kehadiran { get; set; } = null!;

    public string Class { get; set; } = null!;
}