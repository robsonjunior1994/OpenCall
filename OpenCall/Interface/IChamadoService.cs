using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.Interface
{
    public interface IChamadoService
    {
        IList<Chamado> PegarPorStatus(string status, string userKey);
        bool Cadastrar(Chamado chamado, string UserKey);
        bool Atualizar(Chamado chamado, string userKey);
        bool Deletar(int id, string userKey);
        object PegarPorId(int id, string userKey);
    }
}
