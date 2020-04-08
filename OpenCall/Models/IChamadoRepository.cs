using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.Models
{
    public interface IChamadoRepository
    {
        void Adicionar(Chamado chamado);
        void Atualizar(Chamado chamado);
        void Deletar(int id);
        IList<Chamado> Listar();
        Chamado Get(int id);
    }
}
