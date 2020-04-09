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
    public class ChamadoController : ControllerBase
    {
        private readonly IChamadoRepository _chamadoRepository;

        public ChamadoController(IChamadoRepository chamadoRepository)
        {
            _chamadoRepository = chamadoRepository;
        }

        // GET api/chamado
        [HttpGet]
        public ActionResult Get()
        {
            var lista = _chamadoRepository.Listar();


            Chamado chamadoExemplo = new Chamado()
            {
                Tipo = "água",
                Endereco = "Rua de exemplo",
                Descricao = "Descrição do chamado",
                Status = "Aberto",
                Data = DateTime.Now
            };

            if(lista == null)
            {
                return NotFound();
                //return BadRequest(); não sei qual o melhor para uma lista que não foi preenchida
            }
            lista.Add(chamadoExemplo);
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public ActionResult Get([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chamado = _chamadoRepository.Get(id);

            if (chamado == null)
            {
                return NotFound();
            }

            return Ok(chamado);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Chamado chamado)
        {
            chamado.Data = DateTime.Now;
            chamado.Protocolo = DateTime.Now.ToString();
            chamado.Status = "aberto";

            if (chamado.EhValido())
            {
                _chamadoRepository.Adicionar(chamado);
                return CreatedAtAction("Get", new { id = chamado.Id }, chamado);
            }

            return BadRequest(chamado);
            
        }

        [HttpPut]
        public ActionResult Update([FromBody] Chamado chamado)
        {

            Chamado chamadoDoBanco = _chamadoRepository.Get(chamado.Id);

            if (chamadoDoBanco != null && chamado.EhValido())
            {
                _chamadoRepository.Atualizar(chamado);
                return Ok(chamado);
            }

            return BadRequest();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if (Convert.ToString(id).All(char.IsDigit))
            {
                Chamado chamadoDoBanco = _chamadoRepository.Get(id);
                if (chamadoDoBanco != null)
                { 
                    _chamadoRepository.Deletar(id);
                    return NoContent();
                }
            }

            return NotFound();
        }
    }
}