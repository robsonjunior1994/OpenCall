using OpenCall.Interface;
using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenCall.Services
{
    public class ChamadoService : IChamadoService
    {
        private IUsuarioService _usuarioService;
        private IChamadoRepository _chamadoRepository;

        public ChamadoService(IUsuarioService usuarioService, IChamadoRepository chamadoRepository)
        {
            _usuarioService = usuarioService;
            _chamadoRepository = chamadoRepository;
                       
        }
        public async Task<IList<Chamado>> PegarPorStatusAsync(string status, string userKey)
        {
            AutenticacaoService autenticacaoService = new AutenticacaoService(new HttpClient());

            //if (_usuarioService.ValidaKey(userKey))
            if (await autenticacaoService.ValidarKey(userKey))
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
                    return null;
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

                return lista;
            }
            else
            {
                return null;
            }
        }

        public bool Cadastrar(Chamado chamado, string userKey)
        {
            if (_usuarioService.ValidaKey(userKey))
            {

                Usuario usuarioDoBanco = _usuarioService.GetForKey(userKey);

                if (usuarioDoBanco != null)
                {
                    chamado.User = usuarioDoBanco;

                    if (chamado.EhValido())
                    {
                        _chamadoRepository.Adicionar(chamado);
                        //return CreatedAtAction("Get", new { id = chamado.Id }, chamado);
                        return true;
                    }
                }

                //return BadRequest(chamado);
                return false;
            }
            else
            {
                //return StatusCode(403);
                return false;
            }
        }

        public bool Atualizar(Chamado chamado, string userKey)
        {
            if (_usuarioService.ValidaKey(userKey))
            {
                Chamado chamadoDoBanco = _chamadoRepository.Get(chamado.Id);

                chamadoDoBanco.Status = chamado.Status;
                chamadoDoBanco.Descricao = chamado.Descricao;
                chamadoDoBanco.Endereco = chamado.Endereco;

                if (chamadoDoBanco != null && chamado.EhValido())
                {
                    _chamadoRepository.Atualizar(chamadoDoBanco);
                    //return Ok(chamado);
                    return true;
                }

                //return BadRequest();
                return false;
            }
            else
            {
                //return StatusCode(403);
                return false;
            }
        }

        public bool Deletar(int id, string userKey)
        {
            if (_usuarioService.ValidaKey(userKey))
            {
                if (Convert.ToString(id).All(char.IsDigit))
                {
                    Chamado chamadoDoBanco = _chamadoRepository.Get(id);
                    if (chamadoDoBanco != null)
                    {
                        _chamadoRepository.Deletar(id);
                        //return NoContent();
                        return true;
                    }
                }

                //return NotFound();
                return false;
            }
            else
            {
                //return StatusCode(403);
                return false;
            }
        }

        public object PegarPorId(int id, string userKey)
        {
            if (_usuarioService.ValidaKey(userKey))
            {
                var chamado = _chamadoRepository.Get(id);

                if (chamado == null)
                {
                    //return NotFound();
                    return null;
                }

                return chamado;
            }
            else
            {
                //return StatusCode(403);
                return null;
            }
        }
    }
}
