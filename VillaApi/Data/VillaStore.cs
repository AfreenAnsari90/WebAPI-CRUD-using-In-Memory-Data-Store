using VillaApi.Models.DTO;

namespace VillaApi.Data;

public static class VillaStore
{
    public static List<VillaDto> VillaList = new List<VillaDto>()
    {
        new VillaDto() { Id = 1, Name = "Shamim Kausar Villa", SqFt = 300, Occupancy = 10 },
        new VillaDto() { Id = 2, Name = "Fayyaz Momin Villa", SqFt = 500, Occupancy = 20 },
        new VillaDto() { Id = 3, Name = "Faisal Momin Villa", SqFt = 700, Occupancy = 30 }
    };
}
