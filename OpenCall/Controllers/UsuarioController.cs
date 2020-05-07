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
        [HttpPost]
        public ActionResult Login([FromBody] string email, string senha)
        {
            Usuario user = new Usuario()
            {
                Email = email,
                Senha = senha
            };
            if(!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(senha))
            {
               user = _usuarioRepository.Login(user);
                return Ok(user);
            } else
            {
                return NotFound();
            }
            

        }
    }
}