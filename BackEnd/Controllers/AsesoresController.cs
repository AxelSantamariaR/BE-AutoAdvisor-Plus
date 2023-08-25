using AutoMapper;
using Domains;
using DTO.Asesor;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsesoresController : Controller
    {

        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public AsesoresController(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("page")]
        public async Task<ActionResult<List<AsesorGetPageDTO>>> GetAsesores()
        {
            try
            {
                var listAsesores = await _context.Asesores.Where(x => x.Estado).ToListAsync();
                var listAsesoresDto = _mapper.Map<List<AsesorGetPageDTO>>(listAsesores);
                return Ok(listAsesoresDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("admin")]
        public async Task<ActionResult<List<AsesorGetAdminDTO>>> GetAsesoresAdmin()
        {
            try
            {
                var listAsesores = await _context.Asesores.ToListAsync();
                var listAsesoresDto = _mapper.Map<List<AsesorGetAdminDTO>>(listAsesores);
                return Ok(listAsesoresDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Respuesta>> PostAsesor([FromBody] AsesorGetPageDTO asesor)
        {
            try
            {
                var asesorNew = _mapper.Map<Asesor>(asesor);

                _context.Asesores.Add(asesorNew);
                await _context.SaveChangesAsync();

                return Ok(new Respuesta("Enhorabuena", "Asesor creado con éxito."));
            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta("Error", ex.Message));
            }
        }
        

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Respuesta>> PutAsesor([FromBody] AsesorPutDTO asesorDTO)
        {
            try
            {
                var asesor = _mapper.Map<Asesor>(asesorDTO);

                var asesorItem = await _context.Asesores.FindAsync(asesorDTO.Id_Asesor);
                if (asesorItem == null)
                {
                    return NotFound(new Respuesta("Error","Asesor no existe"));
                }

                if (!asesorItem.Estado)
                {
                    return BadRequest(new Respuesta("Error", "Asesor no disponible"));
                }

                asesor.Estado = true;
                _context.Entry(asesorItem).CurrentValues.SetValues(asesor);

                await _context.SaveChangesAsync();
                return Ok(new Respuesta("Enhorabuena", "Asesor actualizado con éxito."));
            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta("Error", ex.Message));
            }
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Respuesta>> DeleteAsesor([FromRoute] long id)
        {
            try
            {
                var asesorItem = await _context.Asesores.FindAsync(id);
                if (asesorItem == null)
                {
                    return NotFound(new Respuesta("Error", "Asesor no existe"));
                }

                if (!asesorItem.Estado)
                {
                    return BadRequest(new Respuesta("Error", "Asesor no disponible"));
                }

                asesorItem.Estado = false; 
                await _context.SaveChangesAsync();

                return Ok(new Respuesta("Enhorabuena", "Asesor eliminado con éxito."));
            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta("Error", ex.Message));
            }
        }


    }
}
