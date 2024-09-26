using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using System.Threading.Tasks;
using DoubleV.Controllers;
using DoubleV.Modelos;
using DoubleV.Servicios;
using Microsoft.AspNetCore.Mvc;
using DoubleV.Interfaces;
using DoubleV.DTOs;

namespace DoubleVPruebas.Controladores.UsuariosControllerPruebas
{
    public class ObtenerTodosLosUsuariosAsync
    {
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly UsuariosController _usuariosController;

        //public ObtenerTodosLosUsuariosAsync()
        //{
        //    _mockUsuarioService = new Mock<IUsuarioService>();
        //    _usuariosController = new UsuariosController(_mockUsuarioService.Object);
        //}

        //[Fact]
        //public async Task ObtenerTodosLosUsuariosAsync_NoUsuarios_ReturnsOkWithMessage()
        //{            
        //    _mockUsuarioService.Setup(s => s.ObtenerTodosLosUsuariosAsync())
        //        .ReturnsAsync(new List<UsuarioConRol>()); 
            
        //    var result = await _usuariosController.ObtenerTodosLosUsuariosAsync();
            
        //    var actionResult = Assert.IsType<ActionResult<UsuariosConRolResponse>>(result);
        //    var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        //    var response = Assert.IsType<UsuariosConRolResponse>(okResult.Value);

        //    Assert.Equal("No se encontraron usuarios", response.Message);
        //    Assert.NotNull(response.Usuarios);
        //    Assert.Empty(response.Usuarios);
        //}


    }
}
