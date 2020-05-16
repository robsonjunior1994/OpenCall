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
        private readonly IUsuarioRepository _usuarioRepository;

        public ChamadoController(IChamadoRepository chamadoRepository, IUsuarioRepository usuarioRepository)
        {
            _chamadoRepository = chamadoRepository;
            _usuarioRepository = usuarioRepository;
        }

        // GET api/chamado
        [HttpGet]
        public ActionResult Get(string status, [FromHeader]string UserKey)
        {
            UsuarioService usuarioService = new UsuarioService(_usuarioRepository);

            if (usuarioService.ValidaKey(UserKey))
            {
                IList<Chamado> lista = null;

                if (string.IsNullOrEmpty(status) == false)
                {
                    if (
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

                if (lista == null)
                {
                    return NotFound();
                }

                if (lista.Count <= 0)
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
            } else
            {
                return BadRequest();
            }


        }

        //GET api/chamado/2
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] int id, [FromHeader]string UserKey)
        {
            UsuarioService usuarioService = new UsuarioService(_usuarioRepository);

            if (usuarioService.ValidaKey(UserKey))
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
            UsuarioService usuarioService = new UsuarioService(_usuarioRepository);

            if (usuarioService.ValidaKey(UserKey))
            {

                Usuario usuarioDoBanco = usuarioService.GetForKey(UserKey);

                if (usuarioDoBanco != null)
                {
                    chamado.User = usuarioDoBanco;

                    if (chamado.EhValido())
                    {
                        _chamadoRepository.Adicionar(chamado);
                        return CreatedAtAction("Get", new { id = chamado.Id }, chamado);
                    }
                }

                return BadRequest(chamado);
            } else
            {
                return StatusCode(403);
            }
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