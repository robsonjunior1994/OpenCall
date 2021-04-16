using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.Interface
{
    public interface IChamadoRepository
    {
        void Adicionar(Chamado chamado);
        void Atualizar(Chamado chamado);
        void Deletar(int id);
        IList<Chamado> Listar();
        IList<Chamado> ListarComFiltro(string status);
        Chamado Get(int id);
        IList<Chamado> ListarPorUsuario(int idUsuario);
    }
}
