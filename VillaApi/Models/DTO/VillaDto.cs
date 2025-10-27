using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VillaApi.Models.DTO;

public class VillaDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [JsonIgnore]
    public DateTime CreatedDate { get; set; }

    public int SqFt { get; set; }

    public int Occupancy { get; set; }
}
