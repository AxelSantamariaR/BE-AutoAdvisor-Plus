using AutoMapper;
using Domains;
using DTO;
using DTO.AutoNuevo;
using DTO.AutosUsados;
using DTO.Citas;
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
    public class CitasController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public CitasController(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            ActualizarEstadoCitasPendientes().Wait();
        }

        private async Task ActualizarEstadoCitasPendientes()
        {
            DateTime fechaActual = DateTime.Now;
            var citasPendientes = await _context.Citas
                .Where(c => c.Fecha < fechaActual && c.Estado)
                .ToListAsync();

            foreach (var cita in citasPendientes)
            {
                cita.Estado = false;
            }

            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<ActionResult<List<CitaGetDTO>>> GetCitas()
        {
            try
            {
                var citas = await _context.Citas
                    .Join(_context.Asesores,
                        cita => cita.AsesorId_Asesor,
                        asesor => asesor.Id_Asesor,
                        (cita, asesor) => new { Cita = cita, Asesor = asesor })
                    .Join(_context.Horarios,
                        joined => joined.Cita.HorarioId_Horario,
                        horario => horario.Id_Horario,
                        (joined, horario) => new { joined.Cita, joined.Asesor, Horario = horario })
                    .Join(_context.Autos,
                        joined => joined.Cita.AutoId_Auto,
                        auto => auto.Id_Auto,
                        (joined, auto) => new CitaGetDTO
                        {
                            Id_Cita = joined.Cita.Id_Cita,
                            Auto = auto.Nombre,
                            NombresCliente = joined.Cita.NombresCliente,
                            Telefono = joined.Cita.Telefono,
                            Correo = joined.Cita.Correo,
                            Hora = joined.Horario.Hora,
                            Fecha = joined.Cita.Fecha,
                            NombresAsesor = joined.Asesor.Nombres,
                            Estado = joined.Cita.Estado
                        })
                    .ToListAsync();

                var listCitasDto = _mapper.Map<List<CitaGetDTO>>(citas);

                return Ok(listCitasDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Respuesta>> PostCita([FromBody] CitaPostDTO cita)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var asesor = await _context.Asesores.FindAsync(cita.AsesorId_Asesor);
                    if (asesor == null)
                    {
                        return NotFound(new Respuesta("Error", "Asesor no existe"));
                    }

                    var auto = await _context.Autos.FindAsync(cita.AutoId_Auto);
                    if (auto == null)
                    {
                        return NotFound(new Respuesta("Error", "Auto no existe"));
                    }

                    var horario = await _context.Horarios.FindAsync(cita.HorarioId_Horario);
                    if (horario == null)
                    {
                        return NotFound(new Respuesta("Error", "Horario no existe"));
                    }

                    var citaNew = _mapper.Map<Cita>(cita);
                    _context.Citas.Add(citaNew);
                    await _context.SaveChangesAsync();

                    scope.Complete();

                    return Ok(new Respuesta("Enhorabuena", "Te esperamos pronto."));
                }
                catch (Exception ex)
                {
                    return BadRequest(new Respuesta("Error", ex.Message));
                }
            }
        }


        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Respuesta>> PutCita([FromBody] CitaPutDTO cita)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var citaExiste = await _context.Citas.FindAsync(cita.Id_Cita);
                    if (citaExiste == null)
                    {
                        return NotFound(new Respuesta("Error", "Cita no existe"));
                    }

                    if (!citaExiste.Estado)
                    {
                        return BadRequest(new Respuesta("Error","Cita no disponible"));
                    }

                    var horario = await _context.Horarios.FindAsync(cita.Id_Horario);
                    if (horario == null)
                    {
                        return NotFound(new Respuesta("Error", "Horario no existe"));
                    }

                    citaExiste.HorarioId_Horario    = cita.Id_Horario;
                    citaExiste.Fecha                = cita.Fecha;
                    citaExiste.Estado               = true;
                    await _context.SaveChangesAsync();

                    scope.Complete();

                    return Ok(new Respuesta("Enhorabuena", "Actualizado exitosamente."));
                }
                catch (Exception ex)
                {
                    return BadRequest(new Respuesta("Error", ex.Message));
                }
            }
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Respuesta>> DeleteCita([FromRoute] long id)
        {

            try
            {
                var citaExiste = await _context.Citas
                                        .Where(x => x.Id_Cita == id)
                                        .FirstOrDefaultAsync();

                if (citaExiste == null)
                {
                    return NotFound(new Respuesta("Error", "Cita no existe"));
                }

                if (!citaExiste.Estado)
                {
                    return NotFound(new Respuesta("Error", "Cita no disponible"));
                }


                citaExiste.Estado = false;
                await _context.SaveChangesAsync();

                return Ok(new Respuesta("Enhorabuena", "Cita eliminada."));
            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta("Error", ex.Message));
            }
        }


    }
}
