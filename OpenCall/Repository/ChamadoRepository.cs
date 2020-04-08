using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCall.Repository
{
    public class ChamadoRepository : IChamadoRepository
    {
        private AppContext _appContext;

        public ChamadoRepository(AppContext appContext)
        {
            _appContext = appContext;
        }

        public void Adicionar(Chamado chamado)
        {
            _appContext.Chamados.Add(chamado);
            _appContext.SaveChanges();
        }

        public void Atualizar(Chamado chamado)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public Chamado Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Chamado> Listar()
        {
            return _appContext.Chamados.ToList();
        }
    }
}
