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

namespace DoubleVPruebas.Controladores.TareasControllerPruebas
{
    public class CrearTarea
    {
        private readonly Mock<ITareaService> _mockTareaService;
        private readonly TareasController _tareasController;

        public CrearTarea()
        {
            _mockTareaService = new Mock<ITareaService>();
            //_tareasController = new TareasController(_mockTareaService.Object);
        }

        //[Fact]
        //public async Task CrearTarea_ShouldReturnCreatedAtAction_WhenTareaIsCreatedSuccessfully()
        //{            
        //    var tarea = new Tarea
        //    {
        //        TareaId = 1,
        //        Descripcion = "Nueva tarea de prueba",
        //        UsuarioId = 123,
        //        Estado = "Pendiente"
        //    };

        //    _mockTareaService.Setup(s => s.CrearTareaAsync(It.IsAny<Tarea>()))
        //                     .ReturnsAsync(true);
           
        //    var result = await _tareasController.CrearTarea(tarea);
            
        //    var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        //    var apiResponse = Assert.IsType<ApiResponse>(createdAtActionResult.Value);
        //    Assert.Equal("Tarea creada exitosamente.", apiResponse.Message);
        //    Assert.Equal(tarea, apiResponse.Data);
        //}
    }
}
