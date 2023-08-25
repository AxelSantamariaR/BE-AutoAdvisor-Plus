using Domains;
using DTO.Asesor;
using DTO.Usuarios;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Shared;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml.Linq;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly AplicationDbContext _context;
        private readonly CryptoService _cryptoService;

        public AuthenticationController(AplicationDbContext context, IConfiguration configuration, CryptoService cryptoService)
        {
            _context = context;
            _cryptoService = cryptoService;
            Configuration = configuration;
        }


        [HttpPost]
        public async Task<ActionResult<Usuario>> Login([FromBody] UsuarioLoginDTO usuario)
        {
            try
            {
                var usuarioExiste = await _context.Usuarios.Include(u => u.Roles) 
                                    .Where(u => u.Username == usuario.Username).FirstOrDefaultAsync();

                if (usuarioExiste is null)
                {
                    return Ok(new Respuesta("Error", "Credenciales incorrectas"));
                }

                if (!_cryptoService.VerifyPassword(usuario.Password, usuarioExiste.Password))
                {
                    return Ok(new Respuesta("Error", "Credenciales incorrectas"));
                }

                return Ok(JsonConvert.SerializeObject(CrearToken(usuarioExiste)));

            }
            catch (Exception ex)
            {
                return BadRequest(new Respuesta("Error", ex.Message));
            }
        }


        private string CrearToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nombres),
                new Claim(ClaimTypes.Name, usuario.Roles.Modulo),
            };
            var appSettingsToken = Configuration.GetSection("AppSettings:Token").Value;
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appSettingsToken));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
