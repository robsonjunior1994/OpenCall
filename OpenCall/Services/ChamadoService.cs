using OpenCall.Interface;
using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Primitives;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;


namespace OpenCall.Services
{
    public class ChamadoService : IChamadoService
    {
        private IUsuarioService _usuarioService;
        private IChamadoRepository _chamadoRepository;
        private IAutenticacaoService _autenticacaoService;

        public ChamadoService(IUsuarioService usuarioService, IChamadoRepository chamadoRepository, IAutenticacaoService autenticacaoService)
        {
            _usuarioService = usuarioService;
            _chamadoRepository = chamadoRepository;
            _autenticacaoService = autenticacaoService;

        }
        public async Task<IList<Chamado>> PegarPorStatusAsync(string status, string userKey)
        {
            IList<Chamado> lista = null;

            if (await _autenticacaoService.ValidarKey(userKey))
            {
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

                    else
                    {
                        lista = _chamadoRepository.Listar();
                    }
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
            }

            return lista;
        }

        public async Task<object> Cadastrar(Chamado chamado, string userKey)
        {
            if (await _autenticacaoService.ValidarKey(userKey))
            {

                var response = await _autenticacaoService.BuscarUsuario(userKey);

                if (response != null)
                {
                    var usuarioResponse = response.Content.ReadFromJsonAsync<Usuario>().Result;
                    chamado.User.Id = usuarioResponse.Id;
                    chamado.User.Email = usuarioResponse.Email;
                    chamado.User.DataKey = usuarioResponse.DataKey;
                    chamado.User.Nome = usuarioResponse.Nome;
                    chamado.User.Senha = usuarioResponse.Senha;
                    chamado.User.Sobrenome = usuarioResponse.Sobrenome;

                    if (chamado.EhValido())
                    {
                        _chamadoRepository.Adicionar(chamado);
                        //return CreatedAtAction("Get", new { id = chamado.Id }, chamado);
                        return chamado;
                    }
                }

                //return BadRequest(chamado);
                return null;
            }
            else
            {
                //return StatusCode(403);
                return null;
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
