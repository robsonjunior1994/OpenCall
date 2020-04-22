using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject1
{
    public class TestesChamado : UnitTest1
    {
        [Fact]
        public void DeveRetornarUmaListaComDoisChamados()
        {
            //arrange
            CriarMock();
            CriarChamadoController();

            IList<Chamado> chamados = new List<Chamado>();

            var chamado1 = new Chamado()
            {
                Id = 01,
                Protocolo = "202004211705",
                Tipo = "água",
                Endereco = "Rua de exemplo",
                Descricao = "Descrição do chamado",
                Status = "Aberto",
                Data = DateTime.Now
            };

            var chamado2 = new Chamado()
            {
                Id = 02,
                Protocolo = "202004211706",
                Tipo = "esgoto",
                Endereco = "Rua de exemplo 2",
                Descricao = "Descrição do chamado 2",
                Status = "Aberto",
                Data = DateTime.Now
            };

            chamados.Add(chamado1);
            chamados.Add(chamado2);
 
            //act
            chamadoRepositoryMock.Setup(x => x.Listar()).Returns(chamados);
            var retorno = sutChamado.Get("") as OkObjectResult;
            var teste = retorno.Value as List<Chamado>;

            //assert
            Assert.True(teste.Count == 2);
        }

        public void PassandoUmStatusQueNaoExisteDeveRetornarUmaListaComTodosOsChamados()
        {

        }
    }
}
