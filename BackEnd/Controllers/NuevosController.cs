using AutoMapper;
using Domains;
using DTO;
using DTO.AutoNuevo;
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

    public class NuevosController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public NuevosController(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet("page")]
        public async Task<ActionResult<List<NuevoGetDTO>>> GetAutosNuevosPage()
        {
            try
            {
                var autos = await _context.Autos.Join(_context.Nuevos,
                                auto => auto.Id_Auto,
                                nuevo => nuevo.Id_Auto,
                                (auto, nuevo) => new NuevoGetDTO
                                {
                                    Id_Auto     = nuevo.Id_Auto,
                                    Nombre      = auto.Nombre,
                                    Anio        = auto.Anio,
                                    Precio      = auto.Precio,
                                    Descripcion = auto.Descripcion,
                                    Imagen      = auto.Imagen,
                                    Marca       = nuevo.Marca,
                                    Tipo        = nuevo.Tipo,
                                    Edicion     = nuevo.Edicion,
                                    Estado      = nuevo.Estado
                                })
                                .Where(nuevo => nuevo.Estado)
                                .ToListAsync();

                var listAutosDto = _mapper.Map<List<NuevoGetDTO>>(autos);

                return Ok(listAutosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<NuevoGetDTO>> GetAutoNuevoPage([FromRoute] long id)
        {
            try
            {

                var auto = await _context.Autos.Join(_context.Nuevos,
                                auto => auto.Id_Auto,
                                nuevo => nuevo.Id_Auto,
                                (auto, nuevo) => new NuevoGetDTO
                                {
                                    Id_Auto = nuevo.Id_Auto,
                                    Nombre = auto.Nombre,
                                    Anio = auto.Anio,
                                    Precio = auto.Precio,
                                    Descripcion = auto.Descripcion,
                                    Imagen = auto.Imagen,
                                    Marca = nuevo.Marca,
                                    Tipo = nuevo.Tipo,
                                    Edicion = nuevo.Edicion,
                                    Estado = nuevo.Estado
                                })
                                .Where(nuevo => nuevo.Estado && nuevo.Id_Auto == id)
                                .FirstOrDefaultAsync();

                var listAutosDto = _mapper.Map<NuevoGetDTO>(auto);

                return Ok(listAutosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<NuevoGetDTO>>> GetAutosNuevos()
        {
            try
            {
                var autos = await _context.Autos.Join(_context.Nuevos,
                                auto => auto.Id_Auto,
                                nuevo => nuevo.Id_Auto,
                                (auto, nuevo) => new NuevoGetDTO
                                {
                                    Id_Auto = nuevo.Id_Auto,
                                    Nombre = auto.Nombre,
                                    Anio = auto.Anio,
                                    Precio = auto.Precio,
                                    Descripcion = auto.Descripcion,
                                    Imagen = auto.Imagen,
                                    Marca = nuevo.Marca,
                                    Tipo = nuevo.Tipo,
                                    Edicion = nuevo.Edicion,
                                    Estado = nuevo.Estado
                                }).ToListAsync();

                var listAutosDto = _mapper.Map<List<NuevoGetDTO>>(autos);

                return Ok(listAutosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Respuesta>> PostAutoNuevo([FromBody] NuevoPostDTO nuevo)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var autoNew = _mapper.Map<Auto>(nuevo);
                    _context.Autos.Add(autoNew);
                    await _context.SaveChangesAsync();

                    var autoId = autoNew.Id_Auto;

                    var nuevoNew = _mapper.Map<Nuevo>(nuevo);
                    nuevoNew.Id_Auto = autoId;
                    _context.Nuevos.Add(nuevoNew);
                    await _context.SaveChangesAsync();

                    scope.Complete();

                    return Ok(new Respuesta("Enhorabuena", "Auto ingresado con éxito."));
                }
                catch (Exception ex)
                {
                    return BadRequest(new Respuesta("Error", ex.Message));
                }
            }
        }


        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Respuesta>> PutAutoNuevo([FromBody] NuevoPutDTO nuevoDTO)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var autoExiste = await _context.Autos.FindAsync(nuevoDTO.Id_Auto);

                    if (autoExiste == null)
                    {
                        return NotFound(new Respuesta("Error", "Auto no existe"));
                    }

                    if (!autoExiste.Estado)
                    {
                        return NotFound(new Respuesta("Error", "Auto no disponible"));
                    }

                    var autoUpdate = _mapper.Map<Auto>(nuevoDTO);
                    autoUpdate.Estado = true;
                    _context.Entry(autoExiste).CurrentValues.SetValues(autoUpdate);
                    await _context.SaveChangesAsync();


                    var nuevoExiste = await _context.Nuevos
                        .Where(x => x.Id_Auto == nuevoDTO.Id_Auto)
                        .FirstOrDefaultAsync();
                    if (nuevoExiste == null)
                    {
                        return NotFound(new Respuesta("Error", "Auto no existe"));
                    }

                    nuevoExiste.Marca   = nuevoDTO.Marca;
                    nuevoExiste.Tipo    = nuevoDTO.Tipo;
                    nuevoExiste.Edicion = nuevoDTO.Edicion;
                    nuevoExiste.Estado  = true;
                    await _context.SaveChangesAsync();

                    scope.Complete();

                    return Ok(new Respuesta("Enhorabuena", "Auto actualizado con éxito."));
                }
                catch (Exception ex)
                {
                    return BadRequest(new Respuesta("Error", ex.Message));
                }
            }
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Respuesta>> DeleteAutoNuevo([FromRoute] long id)
        {
            try
            {
                var autoItem    = await _context.Autos.FindAsync(id);
                var nuevoItem   = await _context.Nuevos.FirstOrDefaultAsync(x=> x.Id_Auto == id);
                if (autoItem == null || nuevoItem == null)
                {
                    return NotFound(new Respuesta("Error", "Auto no existe"));
                }

                if (!autoItem.Estado)
                {
                    return NotFound(new Respuesta("Error", "Auto no disponible"));
                }

                autoItem.Estado = false;
                nuevoItem.Estado = false;
                await _context.SaveChangesAsync();

                return Ok(new Respuesta("Enhorabuena", "Auto eliminado con éxito."));
            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta("Error", ex.Message));
            }
        }

    }
}
