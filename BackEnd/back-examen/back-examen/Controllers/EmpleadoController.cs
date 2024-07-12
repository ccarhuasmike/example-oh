using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_examen.Commands.Request.Empleado;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using back_examen.Commands.Response.Empleado;
using back_examen.Commands.Response.Common;


namespace back_examen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmpleadoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("ActualizarEmpleado")]
        public async Task<IActionResult> ActualizarEmpleado([FromBody] EmpleadoRequest req)
        {
            var result = await _mediator.Send(req);
            return Ok(result);
        }

        [HttpPost("ListarEmpleado")]
        public async Task<IActionResult> ListarEmpleado([FromBody] ListEmpleadoRequest req)
        {
            var result = await _mediator.Send(req);
            return Ok(result);
        }
    }
}
