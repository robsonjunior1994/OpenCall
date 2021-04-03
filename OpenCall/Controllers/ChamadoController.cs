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
        private readonly IChamadoService _chamadoService;

        public ChamadoController(IChamadoService chamadoService)
        {
            _chamadoService = chamadoService;
        }

        [HttpGet]
        [Route("ping")]
        public IActionResult Get()
        {
            return Ok(new { Mensagem = "pong"});
        }

        // GET api/chamado
        [HttpGet]
        public async Task<ActionResult> Get(string status, [FromHeader]string userKey)
        {
            var ListaDechamados = await _chamadoService.PegarPorStatusAsync(status, userKey);

            if (ListaDechamados == null)
            {
                return BadRequest();
            }

            return Ok(ListaDechamados);
        }

        //GET api/chamado/2
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id, [FromHeader]string userKey)
        {
            var chamadoRetornado = _chamadoService.PegarPorId(id, userKey);

            if (chamadoRetornado != null)
            {
                return Ok(chamadoRetornado);
            } else
            {
                return NotFound();
            }

        }

        //POST api/chamado
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Chamado chamado, [FromHeader]string UserKey)
        {

            if (await _chamadoService.Cadastrar(chamado, UserKey) != null)
            {
                return CreatedAtAction("Get", new { id = chamado.Id }, chamado);
            }
            else
            {
                return BadRequest();
            }

        }

        //PUT api/chamado
        [HttpPut]
        public ActionResult Update([FromBody] Chamado chamado, [FromHeader] string UserKey)
        {
            if(_chamadoService.Atualizar(chamado, UserKey))
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
            if(_chamadoService.Deletar(id, userKey))
            {
                return NoContent();
            } else
            {
                return NotFound();
            }
        }
    }
}