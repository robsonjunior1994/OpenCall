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
using Newtonsoft.Json;
using OpenCall.ReponseRequest;

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
        public async Task<IList<Chamado>> ListarPorStatus(string status, string userKey)
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

        public async Task<object> Cadastrar(RequestChamado RequestChamado, string userKey)
        {
            var response = await _autenticacaoService.BuscarUsuario(userKey);

            if (response != null)
            {
                var usuarioResponse = response.Content.ReadFromJsonAsync<ReponseUsuario>();

                Chamado chamado = new Chamado(RequestChamado);
                chamado.IdUser = usuarioResponse.Result.Id;

                if (chamado.EhValido())
                {
                    _chamadoRepository.Adicionar(chamado);
                    return chamado;
                }
            }

            return null;
        }

        public async Task<bool> Atualizar(RequestChamado requestChamado, string userKey)
        {
            var response = await _autenticacaoService.ValidarKey(userKey);

            if (response != false)
            {
                var chamadoDoBanco = _chamadoRepository.Get(requestChamado.Id);

                if(chamadoDoBanco != null)
                {
                    Chamado chamado = chamadoDoBanco;

                    chamado.Descricao = requestChamado.Descricao;
                    chamado.Endereco = requestChamado.Endereco;
                    chamado.Tipo = requestChamado.Tipo;

                    if (chamado.EhValido())
                    {
                        _chamadoRepository.Atualizar(chamado);
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> Deletar(int id, string userKey)
        {
            ReponseUsuario usuarioDoBanco = await _autenticacaoService.RetornarUsuario(userKey);

            if (usuarioDoBanco != null)
            {
                Chamado chamadoDoBanco = _chamadoRepository.Get(id);
                if (chamadoDoBanco.IdUser == usuarioDoBanco.Id)
                {
                    _chamadoRepository.Deletar(id);
                    return true;
                }
            }

            return false;
        }

        public async Task<object> PegarPorId(int id, string userKey)
        {
            var response = await _autenticacaoService.BuscarUsuario(userKey);
            var usuarioDoBanco = response.Content.ReadFromJsonAsync<ReponseUsuario>().Result;

            if (response != null)
            {
                var chamado = _chamadoRepository.Get(id);

                if (chamado.IdUser == usuarioDoBanco.Id)
                {
                    return chamado;
                }
            }

            return null;
        }

        public async Task<IList<Chamado>> GetTodosChamadoDeUmUsuario(string userKey)
        {
            ReponseUsuario usuario = await _autenticacaoService.RetornarUsuario(userKey);
            var ListaDechamadoDeUmUsuario = _chamadoRepository.ListarPorUsuario(usuario.Id);
            return ListaDechamadoDeUmUsuario;
        }
    }
}
