using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VillaApi.Data;
using VillaApi.Models.DTO;

namespace VillaApi.Controllers;

[ApiController]
[Route("api/VillaAPI")]
public class VillaAPIController : ControllerBase
{
    [HttpGet]
    public IEnumerable<VillaDto> GetVillas()
    {
        return VillaStore.VillaList;
    }

    [HttpGet("{id:int}", Name = "GetVilla")]
    public ActionResult<VillaDto> GetVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        return Ok(villa);
    }

    [HttpPost]
    public IActionResult Create([FromBody] VillaDto villaDto)
    {
        if (villaDto == null)
        {
            return BadRequest();
        }

        if (VillaStore.VillaList.Any(v => v.Name == villaDto.Name))
        {
            ModelState.AddModelError("Custom Error", "Villa Already Exist");
            return BadRequest(ModelState);
        }

        var lastId = VillaStore.VillaList.OrderByDescending(v => v.Id).FirstOrDefault()!.Id;

        villaDto.Id = lastId + 1;
        VillaStore.VillaList.Add(villaDto);

        return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);
    }

    [HttpPut]
    public IActionResult UpdateVilla(int id, VillaDto villaDto)
    {
        if (id == 0 || id != villaDto.Id)
        {
            return BadRequest();
        }

        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);
        if (villa == null)
        {
            return BadRequest();
        }

        villa.Name = villaDto.Name;
        villa.SqFt = villaDto.SqFt;
        villa.Occupancy = villaDto.Occupancy;

        return NoContent();
    }

    [HttpDelete]
    public IActionResult DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }

        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);

        if (villa is null)
        {
            return BadRequest();
        }

        VillaStore.VillaList.Remove(villa);

        return NoContent();
    }

    [HttpPatch]
    public IActionResult PartialUpdate(int id, JsonPatchDocument<VillaDto> patchDocument)
    {
        if(id == 0)
        {
            return BadRequest();
        }

        var villa = VillaStore.VillaList.FirstOrDefault(v => v.Id == id);

        if(villa is null)
        {
            return BadRequest();
        }

        patchDocument.ApplyTo(villa, ModelState);

        return NoContent();
    }
}
