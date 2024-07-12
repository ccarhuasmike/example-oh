using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_examen.Commands.Request.Empleado;
using back_examen.Commands.Request.Usuario;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace back_examen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost("ValidarUsuario")]
        public async Task<IActionResult> ValidarUsuario([FromBody] ListUsuarioRequest req)
        {
            var result = await _mediator.Send(req);
            return Ok(result);
        }
    }
}
