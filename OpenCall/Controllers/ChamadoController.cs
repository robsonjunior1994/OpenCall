using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenCall.Interface;
using OpenCall.Models;
using OpenCall.ReponseRequest;

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
        //Pega todos os chamados ou pega chamados por filtro
        // GET api/chamado
        [HttpGet]
        public async Task<ActionResult> Get(string status, [FromHeader]string userKey)
        {
            var ListaDechamados = await _chamadoService.ListarPorStatus(status, userKey);

            if (ListaDechamados == null)
            {
                return BadRequest();
            }

            return Ok(ListaDechamados);
        }

        //Pega todos os chamados de um usuário
        // GET api/chamado
        [HttpGet]
        public async Task<ActionResult> Get([FromHeader] string userKey)
        {
            var ListaDechamados = await _chamadoService.GetTodosChamadoDeUmUsuario(userKey);

            if (ListaDechamados == null)
            {
                return BadRequest();
            }

            return Ok(ListaDechamados);
        }

        //Pega um chamado por id
        //GET api/chamado/2
        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute] int id, [FromHeader]string userKey)
        {
            var chamadoRetornado = await _chamadoService.PegarPorId(id, userKey);

            if (chamadoRetornado != null)
            {
                return Ok(chamadoRetornado);
            } else
            {
                return NotFound();
            }

        }

        //Registar um novo chamado
        //POST api/chamado
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]RequestChamado requestChamado, [FromHeader]string UserKey)
        {
            var resultado = await _chamadoService.Cadastrar(requestChamado, UserKey);

            if (resultado != null)
            {
                //return CreatedAtAction("Get", new { id = chamado.Id }, chamado);
                return Ok(resultado);
            }
            else
            {
                return BadRequest();
            }

        }

        //Atualiza um chamado
        //PUT api/chamado
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] RequestChamado RequestChamado, [FromHeader] string UserKey)
        {
            if(await _chamadoService.Atualizar(RequestChamado, UserKey))
            {
                return Ok();
            } else
            {
                return BadRequest();
            } 
        }

        //Deleta um chamado
        //DELETE api/chamado/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id, [FromHeader] string userKey)
        {
            if(await _chamadoService.Deletar(id, userKey))
            {
                return NoContent();
            } else
            {
                return NotFound();
            }
        }
    }
}