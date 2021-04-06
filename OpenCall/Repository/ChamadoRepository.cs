using OpenCall.Interface;
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
            _appContext.Chamados.AddAsync(chamado);
            _appContext.SaveChanges();
        }

        public void Atualizar(Chamado chamado)
        {
            _appContext.Chamados.Update(chamado);
            _appContext.SaveChanges();
        }

        public void Deletar(int id)
        {
            Chamado chamado = Get(id);
            chamado.Ativo = false;
            _appContext.Chamados.Update(chamado);
            _appContext.SaveChanges();
        }

        public Chamado Get(int id)
        {
           return _appContext.Chamados.FirstOrDefault(chamado => chamado.Id == id);
        }

        public IList<Chamado> Listar()
        {
            return _appContext.Chamados.Where(c => c.Ativo == true).ToList();
        }

        public IList<Chamado> ListarComFiltro(string status)
        {
            var Lista = Listar().Where(chamado => chamado.Status == status).ToList();
            return Lista;
        }
    }
}
