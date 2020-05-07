using Microsoft.AspNetCore.Mvc;
using OpenCall.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject1
{
    public class TestesUsuario : UnitTest1
    {
        [Fact]
        public void DeveriaFalharPorFaltaDeAlgumaPropriedadeNaoPreenchidaDoUsuario()
        {
            //Arrange
            CriarMock();
            CriarChamadoController();

            IList<Usuario> usuarios = new List<Usuario>()
            {
                new Usuario() { Id = 1, Nome = "Robson", Sobrenome = "Junior", Email = "robsonjunior1994@gmail.com" },
                new Usuario() { Id = 2, Nome = "Robson", Sobrenome = "Junior", Senha = "123456" },
                new Usuario() { Id = 3, Nome = "Robson", Email = "robsonjunior1994@gmail.com", Senha = "123456" },
                new Usuario() { Id = 4, Sobrenome = "Junior", Email = "robsonjunior1994@gmail.com", Senha = "123456" },
                new Usuario() { Nome = "Robson", Sobrenome = "Junior", Email = "robsonjunior1994@gmail.com", Senha = "123456" },
            };

            foreach (Usuario usuario in usuarios)
            {
                //Act
                usuarioRepositoryMock.Setup(x => x.Adicionar(usuario));
                var retorno = sutUsuario.Post(usuario) as BadRequestResult;

                //assert
                Assert.True(retorno.StatusCode == 400);
            }
        }

        [Fact]
        public void DeveriaFalharPorTerPassadoParametrosInvalidosNaUrl()
        {
            //Arrange
            CriarMock();
            CriarChamadoController();

            //Act
            usuarioRepositoryMock.Setup(x => x.Get(1 + 'a'));
            var retorno = sutUsuario.Get('a' + 1) as NotFoundResult;

            //assert
            Assert.True(retorno.StatusCode == 404);

        }

        [Fact]
        public void DeveriaFazerLoginComSucesso()
        {
            //Arrange
            CriarMock();
            CriarChamadoController();

            Usuario userDeRetorno = new Usuario()
            {
                Id = 1,
                Nome = "Robson",
                Sobrenome = "Junior",
                Email = "robsonjunior1994@gmail.com",
                Senha = "123456"
            };

            Usuario user = new Usuario()
            {
                Email = "robsonjunior1994@gmail.com",
                Senha = "123456"
            };

            //Act
            usuarioRepositoryMock.Setup(x => x.Login(user)).Returns(userDeRetorno);
            var retorno = sutUsuario.Login("robsonjunior1994@gmail.com", "123456") as OkObjectResult;

            //assert
            Assert.True(retorno.StatusCode == 200);

        }
    }
}
