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
    public class ChamadoController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public ChamadoController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult Get()
        {
            return Ok(new { Mensagem = "pong"});
        }

        // GET api/chamado
        [HttpGet]
        public ActionResult Get(string status, [FromHeader]string userKey)
        {
            var ListaDechamados = _usuarioService.PegarPorTipoDeStatus(status, userKey);

            if (ListaDechamados == null)
            {
                return BadRequest();
            }

            return Ok(ListaDechamados);
        }

        //GET api/chamado/2
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id, [FromHeader]string UserKey)
        {
            
            if (_usuarioService.ValidaKey(UserKey))
            {
                var chamado = _chamadoRepository.Get(id);

                if (chamado == null)
                {
                    return NotFound();
                }

                return Ok(chamado);
            } else
            {
                return StatusCode(403);
            }
            
        }

        //POST api/chamado
        [HttpPost]
        public ActionResult Post([FromBody]Chamado chamado, [FromHeader]string UserKey)
        {
            if(_usuarioService.Cadastrar(chamado, UserKey) == true)
            {
                return CreatedAtAction("Get", new { id = chamado.Id }, chamado);
            }
            else
            {
                return BadRequest(chamado);
            }

        }

        //PUT api/chamado
        [HttpPut]
        public ActionResult Update([FromBody] Chamado chamado, [FromHeader] string UserKey)
        {
            if(_usuarioService.Atualizar(chamado, UserKey))
            {
                return Ok(chamado);
            } else
            {
                return BadRequest();
            } 
        }

        //DELETE api/chamado/1
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id, [FromHeader] string userKey)
        {
            if(_usuarioService.Deletar(id, userKey))
            {
                return NoContent();
            } else
            {
                return NotFound();
            }
        }
    }
}