using AutoMapper;
using Domains;
using DTO;
using DTO.AutosUsados;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Transactions;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsadosController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsadosController(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsadoGetDTO>>> GetAutosUsados()
        {
            try
            {
                var usados = await _context.Autos
                    .Join(_context.Usados,
                        auto                => auto.Id_Auto,
                        usado               => usado.Id_Auto,
                        (auto, usado)       => new { Auto = auto, Usado = usado })
                    .Join(_context.Estados,
                        joined              => joined.Usado.EstadoUsadoId,
                        estado              => estado.Id,
                        (joined, estado)    => new UsadoGetDTO
                        {
                            Id_Auto         = joined.Usado.Id_Auto,
                            Nombre          = joined.Auto.Nombre,
                            Anio            = joined.Auto.Anio,
                            Precio          = joined.Auto.Precio,
                            Descripcion     = joined.Auto.Descripcion,
                            Imagen          = joined.Auto.Imagen,
                            NombreVendedor  = joined.Usado.NombreVendedor,
                            Telefono        = joined.Usado.Telefono,
                            Correo          = joined.Usado.Correo,
                            Estado          = estado.Descripcion
                        })
                    .ToListAsync();


                var listUsadosDto = _mapper.Map<List<UsadoGetDTO>>(usados);

                return Ok(listUsadosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Respuesta>> PostAutoUsado([FromBody] UsadoPostDTO usado)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var autoNew = _mapper.Map<Auto>(usado);
                    _context.Autos.Add(autoNew);
                    await _context.SaveChangesAsync();


                    var autoId = autoNew.Id_Auto;
                    var idEsperando = await _context.Estados
                        .Where(x => x.Descripcion == "esperando")
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();                  

                    var usadoNew                = _mapper.Map<Usado>(usado);
                    usadoNew.Id_Auto            = autoId;
                    usadoNew.EstadoUsadoId      = idEsperando;
                    _context.Usados.Add(usadoNew);
                    await _context.SaveChangesAsync();

                    scope.Complete();

                    return Ok(new Respuesta("Enhorabuena", "Pronto nos comunicaremos contigo."));
                }
                catch (Exception ex)
                {
                    return BadRequest(new Respuesta("Error", ex.Message));
                }
            }
        }


        [Authorize]
        [HttpDelete("aceptar/{id}")]
        public async Task<ActionResult<Respuesta>> PutAutoUsado([FromRoute] long id)
        {

            try
            {                
                var usadoExiste = await _context.Usados
                                        .Where(x => x.Id_Auto == id)
                                        .FirstOrDefaultAsync();

                if (usadoExiste == null)
                {
                    return NotFound(new Respuesta("Error", "Auto no existe"));
                }

                if (usadoExiste.EstadoUsadoId != 1)
                {
                    return NotFound(new Respuesta("Error", "Auto no disponible"));
                }

                var idAceptado = await _context.Estados
                    .Where(x => x.Descripcion == "aceptado")
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();

                usadoExiste.EstadoUsadoId = idAceptado;
                await _context.SaveChangesAsync();

                return Ok(new Respuesta("Enhorabuena", "Auto aceptado."));
            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta("Error", ex.Message));
            }
        }


        [Authorize]
        [HttpDelete("rechazar/{id}")]
        public async Task<ActionResult<Respuesta>> PutAutoRechazado([FromRoute] long id)
        {

            try
            {
                var usadoExiste = await _context.Usados
                                        .Where(x => x.Id_Auto == id)
                                        .FirstOrDefaultAsync();

                if (usadoExiste == null)
                {
                    return NotFound(new Respuesta("Error", "Auto no existe"));
                }

                if (usadoExiste.EstadoUsadoId != 1)
                {
                    return NotFound(new Respuesta("Error", "Auto no disponible"));
                }


                var idRechazado = await _context.Estados
                    .Where(x => x.Descripcion == "rechazado")
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();

                usadoExiste.EstadoUsadoId = idRechazado;
                await _context.SaveChangesAsync();

                return Ok(new Respuesta("Enhorabuena", "Auto rechazado."));
            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta("Error", ex.Message));
            }
        }

    }
}
