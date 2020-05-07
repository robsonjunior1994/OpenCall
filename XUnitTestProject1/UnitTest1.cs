using Moq;
using OpenCall.Controllers;
using OpenCall.Models;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.ApiExplorer;


namespace XUnitTestProject1
{
    public class UnitTest1
    {
        internal ChamadoController sutChamado;
        internal Mock<IChamadoRepository> chamadoRepositoryMock;

        internal UsuarioController sutUsuario;
        internal Mock<IUsuarioRepository> usuarioRepositoryMock;

        public void CriarChamadoController()
        {
            sutChamado = new ChamadoController(chamadoRepositoryMock.Object);
            sutUsuario = new UsuarioController(usuarioRepositoryMock.Object);
        }

        public void CriarMock()
        {
            this.chamadoRepositoryMock = new Mock<IChamadoRepository>();
            this.usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        }
    }
}
