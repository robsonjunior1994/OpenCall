using OpenCall.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.Models
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IChamadoRepository _chamadoRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, IChamadoRepository chamadoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _chamadoRepository = chamadoRepository;

        }

        public Usuario Login(Usuario usuario)
        {
            if(!string.IsNullOrEmpty(usuario.Email) && !string.IsNullOrEmpty(usuario.Senha))
            {
                var user = _usuarioRepository.Login(usuario);
                Random randNum = new Random(2);
                user.DataKey = DateTime.Now.AddDays(1);
                user.Key = MD5Hash.Hash.Content(usuario.Email+DateTime.Now.ToString("HH" + "mm" + "ss" + "ff" + "d" + "M" + "yyyy"));
                _usuarioRepository.Atualizar(user);
                return user;
            } else
            {
                return null;
            }

        }

        public bool ValidaKey(string key)
        {
            Usuario user = _usuarioRepository.GetUsuarioForKey(key);
            if(user == null || user.DataKey < DateTime.Now)
            {
                return false;
            } else
            {
                return true;
            }

        }

        public Usuario GetForKey(string key)
        {
            Usuario user = _usuarioRepository.GetUsuarioForKey(key);
            return user;
        }

        public IList<Chamado> PegarPorTipoDeStatus(string status, string userKey)
        {
            if (this.ValidaKey(userKey))
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
            if (this.ValidaKey(userKey))
            {

                Usuario usuarioDoBanco = this.GetForKey(userKey);

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
            if (this.ValidaKey(userKey))
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
            if (this.ValidaKey(userKey))
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
    }
}
