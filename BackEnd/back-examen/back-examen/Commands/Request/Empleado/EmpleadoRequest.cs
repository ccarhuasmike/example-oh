using back_examen.Commands.Response.Empleado;
using MediatR;
using back_examen.Commands.Response.Common;
namespace back_examen.Commands.Request.Empleado;
public class EmpleadoRequest: IRequest<Response<ResponseEmpleado>>
{
    public Model.Empleado? Empleado { get; set; } 
}