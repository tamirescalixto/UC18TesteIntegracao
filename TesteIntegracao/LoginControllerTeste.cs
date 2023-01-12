using ChapterSenaiUC17.Controllers;
using ChapterSenaiUC17.Interfaces;
using ChapterSenaiUC17.Models;
using ChapterSenaiUC17.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IdentityModel.Tokens.Jwt;

namespace TesteIntegracao
{
    public class LoginControllerTeste
    {
        [Fact]
        public void LoginController_Retornar_Usuario_Invalido()
        {

            // Arrange - Prepara��o
            var repositoryEspelhado = new Mock<IUsuarioRepository>();

            repositoryEspelhado.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);

            var controller = new LoginController(repositoryEspelhado.Object);

            LoginViewModel dadosUsuario = new LoginViewModel();
            dadosUsuario.Email = "tamires@email.com";
            dadosUsuario.Senha = "senha";

            // Act - A��o
            var result = controller.Login(dadosUsuario);


            // Assert - Verifica��o
            Assert.IsType<UnauthorizedObjectResult>(result);

        }

        [Fact]

        public void LoginController_Retornar_Token()
        {
            // Arrange - Prepara��o
            Usuario usuarioRetornado = new Usuario();
            usuarioRetornado.Email = "email@email.com";
            usuarioRetornado.Senha = "1234";
            usuarioRetornado.Tipo = "0";
            usuarioRetornado.id = 1;

            var repositoryEspelhado = new Mock<IUsuarioRepository>();

            repositoryEspelhado.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioRetornado);

            LoginViewModel dadosUsuario = new LoginViewModel();
            dadosUsuario.Email = "tamires@email.com";
            dadosUsuario.Senha = "senha";

            var controller = new LoginController(repositoryEspelhado.Object);
            string issuerValido = "chapter.webapi";


            // Act - A��o
            OkObjectResult result = (OkObjectResult)controller.Login(dadosUsuario);

            string tokenString = result.Value.ToString().Split(' ')[3];

            var jwtHandler = new JwtSecurityTokenHandler();

            var tokenJwt = jwtHandler.ReadJwtToken(tokenString);


            // Assert - Verifica��o
            Assert.Equal(issuerValido, tokenJwt.Issuer);
        }
    }
}