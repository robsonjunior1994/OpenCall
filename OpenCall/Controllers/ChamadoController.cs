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

            if(lista == null)
            {
                return BadRequest();
            }
            return Ok(lista);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Chamado chamado)
        {
            chamado.Data = DateTime.Now;
            chamado.Protocolo = DateTime.Now.ToString();
            chamado.Status = "aberto";

            if (!chamado.EhValido() && !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _chamadoRepository.Adicionar(chamado);
            return Ok();
        }
    }
}