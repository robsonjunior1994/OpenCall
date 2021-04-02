using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenCall.Interface;
using OpenCall.Models;

namespace OpenCall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioRepository usuarioRepository, IUsuarioService usuarioService)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
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

        //POST api/usuario/login
        [Route("login")]
        [HttpPost]
        public ActionResult Login([FromBody] Usuario user)
        {
            //UsuarioService usuarioService = new UsuarioService(_usuarioRepository);

            if (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Senha))
            {
                Usuario usuario = _usuarioService.Login(user);
                var key = usuario.Key;

                if (usuario != null)
                {
                    return Ok(new { key = key });
                }
                return StatusCode(403);

            }
            else
            {
                return BadRequest();
            }
        }

        //PUT api/usuario/id
        [HttpPatch]
        public ActionResult Iditar([FromBody] Usuario usuario, [FromHeader] string UserKey)
        {
            //UsuarioService usuarioService = new UsuarioService(_usuarioRepository);

            if (_usuarioService.ValidaKey(UserKey))
            {
                if (usuario.EhValido())
                {
                    Usuario usuarioDoBanco = _usuarioRepository.GetUsuarioForKey(UserKey);
                    usuarioDoBanco.Nome = usuario.Nome;
                    usuarioDoBanco.Sobrenome = usuario.Sobrenome;
                    usuarioDoBanco.Email = usuario.Email;
                    usuarioDoBanco.Senha = usuario.Senha;

                    _usuarioRepository.Atualizar(usuarioDoBanco);

                    return Ok(usuarioDoBanco);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return StatusCode(403);
            }

        }


        // Funções do administrador do sistema.. 
        //GET api/usuario/id
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

        
    }
}