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
        public void DeveRetornarUmaListaComChamados()
        {
            //arrange
            CriarMock();


            //assert
            chamadoRepositoryMock.Setup(c => c.Get(It.IsAny<IList<Chamado>>))).Returns(Chamados);

            //act
        }
    }
}
