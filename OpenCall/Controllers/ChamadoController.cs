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
        public ActionResult Get(string status)
        {
            IList<Chamado> lista = null;

            if(string.IsNullOrEmpty(status) == false) { 
                if( 
                    status == "aberto" ||
                    status == "fechado" || 
                    status == "emandamento"
                  )
                    {
                         lista = _chamadoRepository.ListarComFiltro(status);
                    }
            }
            else
            {
                 lista = _chamadoRepository.Listar();
            }

            if(lista == null)
            {
                return NotFound();
            }

            if(lista.Count <= 0)
            {
                Chamado chamadoExemplo = new Chamado()
                {
                    Tipo = "água",
                    Endereco = "Rua de exemplo",
                    Descricao = "Descrição do chamado",
                    Status = "Aberto",
                    Data = DateTime.Now
                };

                lista.Add(chamadoExemplo);

            }

            return Ok(lista);
        }

        //GET api/chamado/2
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id)
        {
            var chamado = _chamadoRepository.Get(id);

            if (chamado == null)
            {
                return NotFound();
            }

            return Ok(chamado);
        }

        //POST api/chamado
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

        //PUT api/chamado
        [HttpPut]
        public ActionResult Update([FromBody] Chamado chamado)
        {

            Chamado chamadoDoBanco = _chamadoRepository.Get(chamado.Id);

            chamadoDoBanco.Status = chamado.Status;
            chamadoDoBanco.Descricao = chamado.Descricao;
            chamadoDoBanco.Endereco = chamado.Endereco;

            if (chamadoDoBanco != null && chamado.EhValido())
            {
                _chamadoRepository.Atualizar(chamadoDoBanco);
                return Ok(chamado);
            }

            return BadRequest();

        }

        //DELETE api/chamado/1
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