using OpenCall.Models;
using OpenCall.ReponseRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.Interface
{
    public interface IChamadoService
    {
        Task<IList<Chamado>> ListarPorStatus(string status, string userKey);
        Task<object> Cadastrar(RequestChamado requestChamado, string UserKey);
        Task<bool> Atualizar(RequestChamado RequestChamado, string userKey);
        Task<bool> Deletar(int id, string userKey);
        Task<object> PegarPorId(int id, string userKey);
        Task<IList<Chamado>> GetTodosChamadoDeUmUsuario(string userKey);
    }
}
