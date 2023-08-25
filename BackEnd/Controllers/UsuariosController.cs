using AutoMapper;
using Domains;
using DTO.Usuarios;
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
    public class UsuariosController : ControllerBase
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly CryptoService _cryptoService;

        public UsuariosController(AplicationDbContext context, IMapper mapper, CryptoService cryptoService)
        {
            _context = context;
            _mapper = mapper;
            _cryptoService = cryptoService;
        }


        [HttpGet]
        public async Task<ActionResult<List<UsuarioGetDTO>>> GetUsuarios()
        {
            try
            {

                var usuarios = await _context.Usuarios
                    .Where(usuario => usuario.Estado && usuario.RolId != 1)
                    .Join(_context.Roles,
                        usuario => usuario.RolId,
                        rol => rol.Id,
                        (usuario, rol) => new UsuarioGetDTO
                        {
                            Id = usuario.Id,
                            Cedula = usuario.Cedula,
                            Nombres = usuario.Nombres,
                            Username = usuario.Username,
                            Modulo = rol.Modulo,
                            Estado = usuario.Estado
                        }).ToListAsync();

                var listUsuariosDto = _mapper.Map<List<UsuarioGetDTO>>(usuarios);

                return Ok(listUsuariosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Respuesta>> PostUsuario([FromBody] UsuarioPostDTO nuevo)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    bool cedulaExiste = await _context.Usuarios.AnyAsync(x => x.Cedula == nuevo.Cedula);
                    if (cedulaExiste)
                    {
                        return Ok(new Respuesta("Error", "Cédula ya registrada"));
                    }

                    bool usernameExiste = await _context.Usuarios.AnyAsync(x => x.Username.ToUpper() == nuevo.Username.ToUpper());
                    if (usernameExiste)
                    {
                        return Ok(new Respuesta("Error", "Username ya registrado"));
                    }

                    var rolExiste = await _context.Roles.FindAsync(nuevo.RolId);
                    if (rolExiste == null)
                    {
                        return Ok(new Respuesta("Error", "Rol no existe"));
                    }

                    nuevo.Password = _cryptoService.HashPassword(nuevo.Password);

                    var usuarioNew = _mapper.Map<Usuario>(nuevo);
                    _context.Usuarios.Add(usuarioNew);
                    await _context.SaveChangesAsync();

                    scope.Complete();

                    return Ok(new Respuesta("Enhorabuena", "Usuario ingresado con éxito."));
                }
                catch (Exception ex)
                {
                    return BadRequest(new Respuesta("Error", ex.Message));
                }
            }
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Respuesta>> DeleteUsuario([FromRoute] int id)
        {
            try
            {
                var usuarioItem = await _context.Usuarios.FindAsync(id);
                if (usuarioItem is null)
                {
                    return NotFound(new Respuesta("Error", "Usuario no existe"));
                }

                if (!usuarioItem.Estado)
                {
                    return NotFound(new Respuesta("Error", "Usuario no disponible"));
                }

                usuarioItem.Estado = false;                
                await _context.SaveChangesAsync();

                return Ok(new Respuesta("Enhorabuena", "Usuario eliminado con éxito."));
            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta("Error", ex.Message));
            }
        }


    }
}
