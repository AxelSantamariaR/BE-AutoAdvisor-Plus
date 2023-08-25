using AutoMapper;
using Domains;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DTO.Combos;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombosController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public CombosController(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("horarios")]
        public async Task<ActionResult<List<HorarioComboDTO>>> GetHorarios()
        {
            try
            {
                var listHorario = await _context.Horarios.ToListAsync();
                var listHorarioDto = _mapper.Map<List<HorarioComboDTO>>(listHorario);
                return Ok(listHorarioDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("asesores")]
        public async Task<ActionResult<List<AsesorComboDTO>>> GetAsesores()
        {
            try
            {
                var listAsesores = await _context.Asesores.Where(x => x.Estado).ToListAsync();
                var listAsesoresDto = _mapper.Map<List<AsesorComboDTO>>(listAsesores);
                return Ok(listAsesoresDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("roles")]
        public async Task<ActionResult<List<RolComboDTO>>> GetRoles()
        {
            try
            {
                var listRoles = await _context.Roles.Where(x => x.Estado).ToListAsync();
                var listlistRolesDto = _mapper.Map<List<RolComboDTO>>(listRoles);
                return Ok(listlistRolesDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
