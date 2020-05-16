using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenCall.Models;

namespace OpenCall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        //POST api/usuario
        [HttpPost]
        public ActionResult Post([FromBody] Usuario usuario)
        {
            if (usuario.EhValido())
            {
                _usuarioRepository.Adicionar(usuario);
                return Ok();
            } else
            {
                return BadRequest();
            }
        }

        //GET api/usuario
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            var usuario = _usuarioRepository.Get(id);
            if(usuario == null)
            {
                return NotFound();
            } else
            {
                return Ok(usuario);
            }
        }

        //POST api/usuario/login
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody] Usuario user)
        {
            UsuarioService usuarioService = new UsuarioService(_usuarioRepository);

            if (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Senha))
            {
                Usuario usuario = usuarioService.Login(user);
                var key = usuario.Key;

                if (usuario != null)
                {                  
                    return Ok(new { key = key });
                }
                return StatusCode(403);
                
            } else
            {
                return BadRequest();
            }
        }
    }
}